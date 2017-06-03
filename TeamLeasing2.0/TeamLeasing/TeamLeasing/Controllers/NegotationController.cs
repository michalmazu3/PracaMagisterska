using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeamLeasing.Infrastructure;
using TeamLeasing.Models;
using TeamLeasing.ViewModels;

namespace TeamLeasing.Controllers
{
    public class NegotationController : Controller
    {
        private readonly OptimizedDbManager _manager;
        private readonly IMapper _mapper;

        public NegotationController(OptimizedDbManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        [Authorize]
        public async Task<IActionResult> Negotiate(int offerId)
        {
            return View("_Negotation", new NegotiationViewModel {OfferId = offerId});
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Negotiate(NegotiationViewModel vm)
        {
            if (ModelState.IsValid)
                try
                {
                    var user = await _manager.GetUser(_manager.GetUserId(HttpContext.User));
                    int result;
                    var negotation = _mapper.Map<Negotiation>(vm);
                    if (user.DeveloperUser != null)
                        result = await _manager.AddOrUpdateNegotiation(negotation
                            , Enums.NegotiationStatus.WaitingForEmployeeResponse
                            , Enums.NegotiationStatus.Consider);
                    else
                        result = await _manager.AddOrUpdateNegotiation(negotation
                            , Enums.NegotiationStatus.Consider
                            , Enums.NegotiationStatus.WaitingForDeveloperResponse);
                    if (result > 0)
                        return RedirectToAction("Negotiate", "Negotation");
                    return View("_Error", new ErrorViewModel
                    {
                        Message = "Wystąpił nieoczekiwany błąd związany z wysłaniem nowej propozycji",
                        ReturnUrl = Url.Action("Negotiate", "Negotation")
                    });
                }
                catch (Exception e)
                {
                    return View("_Error", new ErrorViewModel
                    {
                        Message = "Wystąpił niekoreślony błąd w aplikacji",
                        ReturnUrl = Url.Action("Index", "Home")
                    });
                }
            return RedirectToAction("Negotiate");
        }
    }
}
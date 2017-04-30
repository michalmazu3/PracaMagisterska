using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TeamLeasing.Infrastructure;
using TeamLeasing.ViewModels;
using TeamLeasing.ViewModels.Employee.Account;

namespace TeamLeasing.Controllers
{
    [Route("account/employee/")]
    public class AccountEmployeeController : Controller
    {
        private readonly OptimizedDbManager _manager;

        public AccountEmployeeController(OptimizedDbManager manager)
        {
            _manager = manager;
        }

        public IActionResult Edit()
        {
            return View("EditProfile", new EditEmployeeAccountViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditEmployeeAccountViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var userId =  _manager.GetUserId(HttpContext.User);
                var result = await _manager.UpdateEmployeeUser(vm, userId);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View("_Error", new ErrorViewModel()
                    {
                        ReturnUrl = UrlHelperExtensions.Action(Url, "Edit", "AccountEmployee"),
                        Message = result.Errors.ToString(),
                    });
                }
            }

            return View("EditProfile", vm);
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeamLeasing.DAL;
using TeamLeasing.Models;
using TeamLeasing.ViewModels;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TeamLeasing.Controllers
{
     public class LoginController : Controller
    {
        private readonly UserManager<User> _manager;
        private readonly SignInManager<User> _signInManager;
        private readonly TeamLeasingContext _teamLeasingContext;

        public LoginController(UserManager<User> manager, SignInManager<User> signInManager ,TeamLeasingContext teamLeasingContext)
        {
            _manager = manager;
            _signInManager = signInManager;
            _teamLeasingContext = teamLeasingContext;
        }
        // GET: /<controller>/
        [HttpGet]
         public IActionResult Login(string returnUrl)
        {
            return View("Login"  , new LoginViewModel(){ ReturnUrl =  returnUrl}) ;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            if (ModelState.IsValid){
            
                var result = await _signInManager.PasswordSignInAsync(vm.Username, vm.Pasword, false, false);
                if (result.Succeeded)
                {
                    if (string.IsNullOrWhiteSpace(vm.ReturnUrl))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                            return Redirect(vm.ReturnUrl);
                    }
                 
                }
                else
                {
                    ModelState.AddModelError("", "Nieprawidłowy login lub hasło !");
                }
            }
            return View("Login");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
             return  RedirectToAction("Index", "Home");
        }

    }

}

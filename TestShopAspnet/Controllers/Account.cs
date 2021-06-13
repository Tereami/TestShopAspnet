using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using DomainModel.Identity;
using TestShopAspnet.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestShopAspnet.Controllers
{
    public class Account : Controller
    {
        private readonly UserManager<User> _UserManager;
        private readonly SignInManager<User> _SignInManager;

        public Account(UserManager<User> UserManager, SignInManager<User> SignInManager)
        {
            _UserManager = UserManager;
            _SignInManager = SignInManager;
        }

        public IActionResult Register() => View(new RegisterUserViewModel());

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            User user = new User
            {
                UserName = model.Username,
            };

            IdentityResult register_result = await _UserManager.CreateAsync(user, model.Password);
            if(register_result.Succeeded)
            {
                await _SignInManager.SignInAsync(user, false); //временный вход без пароля

                RedirectToAction("Index", "Main");
            }

            foreach(IdentityError error in register_result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View(model);
        }
       

        public IActionResult Login() => View();

        public IActionResult Logout() => RedirectToAction("Index", "Main");

        public IActionResult AccessDenied() => View();
    }
}

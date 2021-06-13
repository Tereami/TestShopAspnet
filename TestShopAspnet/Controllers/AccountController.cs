using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using DomainModel.Identity;
using TestShopAspnet.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace TestShopAspnet.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _UserManager;
        private readonly SignInManager<User> _SignInManager;
        private readonly ILogger<AccountController> _Logger;

        public AccountController(
            UserManager<User> UserManager, 
            SignInManager<User> SignInManager,
            ILogger<AccountController> Logger)
        {
            _UserManager = UserManager;
            _SignInManager = SignInManager;
            _Logger = Logger;
        }

        #region Register

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

            IdentityResult registerResult = await _UserManager.CreateAsync(user, model.Password);
            if(registerResult.Succeeded)
            {
                await _SignInManager.SignInAsync(user, false); //временный вход без пароля
                _Logger.LogInformation("Зарегистрирован пользователь {0}", model.Username);
                return RedirectToAction("Index", "Main");
            }

            foreach(IdentityError error in registerResult.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            _Logger.LogWarning("Ошибка при регистрации пользователя {0}: {1}", 
                model.Username,
                string.Join(",", registerResult.Errors.Select(err => err.Description)));
            return View(model);
        }

        #endregion

        #region Login

        public IActionResult Login(string ReturnUrl)
        {
            LoginViewModel model = new LoginViewModel
            {
                ReturnUrl = ReturnUrl
            };

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            bool blockOnPasswordFailures = true;
#if DEBUG
            blockOnPasswordFailures = false;
#endif

            var loginResult = await _SignInManager
                .PasswordSignInAsync(model.Username, model.Password, model.Remember, blockOnPasswordFailures);

            if(loginResult.Succeeded)
            {
                if (model.ReturnUrl is null)
                    model.ReturnUrl = "/";

                _Logger.LogInformation("Успешный вход пользователя {0}", model.Username);
                return LocalRedirect(model.ReturnUrl);
            }

            ModelState.AddModelError("", "Неверный логин или пароль");
            _Logger.LogWarning("Не удалось войти");
            return View(model);
        }

        #endregion

        public async Task<IActionResult> Logout()
        {
            await _SignInManager.SignOutAsync();

            _Logger.LogInformation("Выход пользователя");
            return RedirectToAction("Index", "Main");
        }

        public IActionResult AccessDenied() => View();
    }
}

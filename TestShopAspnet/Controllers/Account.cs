using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestShopAspnet.Controllers
{
    public class Account : Controller
    {
        public IActionResult Register() => View();

        public IActionResult Login() => View();

        public IActionResult Logout() => RedirectToAction("Index", "Main");

        public IActionResult AccessDenied() => View();
    }
}

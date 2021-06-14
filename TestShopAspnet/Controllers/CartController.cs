using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestShopAspnet.Services.Interfaces;

namespace TestShopAspnet.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService CartService)
        {
            _cartService = CartService;
        }

        public IActionResult Index()
        {
            return View(_cartService.GetViewModel());
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestShopAspnet.Models;
using TestShopAspnet.Services.Interfaces;
using DomainModel.Filters;
using TestShopAspnet.ViewModels;

namespace TestShopAspnet.Controllers
{
    public class MainController : Controller
    {
       

        private IConfiguration Configuration;
        private IProductData _productsService;

        public MainController(IConfiguration conf, IProductData productsService)
        {
            Configuration = conf;
            _productsService = productsService;
        }


        public IActionResult Index()
        {
            ProductFilter filter = new ProductFilter
            {
                Limit = 4
            };

            IEnumerable<ProductViewModel> products = _productsService
                .GetProducts(filter)
                .Select(i => new ProductViewModel(i));
            return View(products);
        }

       

        public IActionResult Blog()
        {
            return View();
        }

        public IActionResult BlogSingle()
        {
            return View();
        }

        public IActionResult Cart()
        {
            return View();
        }

        public IActionResult Checkout()
        {
            return View();
        }

        public IActionResult ContactUs()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult ProductDetails()
        {
            return View();
        }

        public IActionResult Shop()
        {
            return View();
        }
    }
}

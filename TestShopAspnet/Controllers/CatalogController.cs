using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestShopAspnet.Services.Interfaces;

namespace TestShopAspnet.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IProductData _service;

        public CatalogController(IProductData service)
        {
            _service = service;
        }


        public IActionResult Index(int? BrandId, int? SectionId)
        {
            return View();
        }
    }
}

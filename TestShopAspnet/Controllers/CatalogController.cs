using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestShopAspnet.Services.Interfaces;
using DomainModel.Filters;
using TestShopAspnet.ViewModels;
using DomainModel.Enitities;

namespace TestShopAspnet.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IProductData _service;

        public CatalogController(IProductData service)
        {
            _service = service;
        }


        public IActionResult Index(int? BrandId, int? SectionId, int? Limit)
        {
            ProductFilter filter = new ProductFilter
            {
                BrandId = BrandId,
                SectionId = SectionId,
                Limit = Limit
            };

            IEnumerable<ProductViewModel> products = _service
                .GetProducts(filter)
                .Select(i => new ProductViewModel(i));

            CatalogViewModel catVm = new CatalogViewModel
            {
                BrandId = BrandId,
                SectionId = SectionId,
                Products = products
            };

            return View(catVm);
        }

        public IActionResult Details(int Id)
        {
            Product p = _service.GetProductById(Id);
            if (p is null)
                return NotFound();

            ProductViewModel vm = new ProductViewModel(p);
            return View(vm);
        }
    }
}

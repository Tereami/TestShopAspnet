using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestShopAspnet.Services.Interfaces;
using TestShopAspnet.ViewModels;

namespace TestShopAspnet.Components
{
    public class BrandsViewComponent : ViewComponent
    {
        IProductData _service;

        public BrandsViewComponent(IProductData service)
        {
            _service = service;
        }

        public IViewComponentResult Invoke()
        {
            List<BrandViewModel> brands = _service.GetBrands()
                .OrderBy(i => i.Order)
                .Select(i => new BrandViewModel
                {
                    Id = i.Id,
                    Name = i.Name
                })
                .ToList();
            return View(brands);
        }
    }
}

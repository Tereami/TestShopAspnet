using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestShopAspnet.Services.Interfaces;
using TestShopAspnet.ViewModels;
using DomainModel.Enitities;

namespace TestShopAspnet.Components
{
    public class SectionsViewComponent : ViewComponent
    {
        IProductData _service;

        public SectionsViewComponent(IProductData service)
        {
            _service = service;
        }

        public IViewComponentResult Invoke()
        {
            IEnumerable<Section> sections = _service.GetSections();
            IEnumerable<Section> parentSections = sections
                .Where(i => i.ParentId == null)
                .OrderBy(i => i.Order);

            List<SectionViewModel> sectionsVMs = parentSections
                .Select(i => GetSectionViewModel(i))
                .ToList();

            foreach(SectionViewModel parentSect in sectionsVMs)
            {
                parentSect.ChildSections = sections
                    .Where(i => i.ParentId == parentSect.Id)
                    .Select(i => GetSectionViewModel(i))
                    .OrderBy(i => i.Order)
                    .ToList();
            }

            return View(sectionsVMs);
        }

        public SectionViewModel GetSectionViewModel(Section sec)
        {
            return new SectionViewModel
            {
                Id = sec.Id,
                Name = sec.Name,
                Order = sec.Order
            };
        }
    }
}

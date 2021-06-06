﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestShopAspnet.ViewModels;

namespace TestShopAspnet.ViewModels
{
    public class CatalogViewModel
    {
        public int? SectionId { get; set; }

        public int? BrandId { get; set; }

        public IEnumerable<ProductViewModel> Products { get; set; }
    }
}

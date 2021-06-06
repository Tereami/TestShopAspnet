using DomainModel.Enitities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestShopAspnet.Data;
using TestShopAspnet.Services.Interfaces;

namespace TestShopAspnet.Services
{
    public class InMemoryProductData : IProductData
    {
        public IEnumerable<Brand> GetBrands()
        {
            return TestData.Brands;
        }

        public IEnumerable<Section> GetSections()
        {
            return TestData.Sections;
        }
    }
}

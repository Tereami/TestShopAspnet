using DomainModel.Enitities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestShopAspnet.Services.Interfaces;

namespace TestShopAspnet.Services
{
    public class InMemoryProductData : IProductData
    {
        public IEnumerable<Brand> GetBrands()
        {
            
        }

        public IEnumerable<Section> GetSections()
        {
            
        }
    }
}

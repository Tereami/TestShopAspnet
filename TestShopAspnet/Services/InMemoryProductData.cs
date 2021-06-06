using DomainModel.Enitities;
using DomainModel.Filters;
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

        public IEnumerable<Product> GetProducts(ProductFilter filter = null)
        {
            IEnumerable<Product> products = TestData.Products;

            if(filter != null)
            {
                if(filter.BrandId != null)
                    products = products.Where(i => i.BrandId == filter.BrandId);
                if (filter.SectionId != null)
                    products = products.Where(i => i.SectionId == filter.SectionId);
                if (filter.Limit != null)
                {
                    int limit2 = (int)filter.Limit;
                    products = products.Take(limit2);
                }
            }

            products = products.OrderBy(i => i.Order);

            return products;
        }
    }
}

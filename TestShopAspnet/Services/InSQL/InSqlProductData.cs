using DomainModel.Enitities;
using DomainModel.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestShopAspnet.Services.Interfaces;
using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;

namespace TestShopAspnet.Services.InSQL
{
    public class InSqlProductData : IProductData
    {
        private readonly DB _db;

        public InSqlProductData(DB db)
        {
            _db = db;
        }

        public IEnumerable<Brand> GetBrands()
        {
            return _db.Brands;
        }

        public IEnumerable<Section> GetSections()
        {
            return _db.Sections;
        }

        public IEnumerable<Product> GetProducts(ProductFilter filter = null)
        {
            IQueryable<Product> products = _db.Products
                .Include(p => p.Brand)
                .Include(p => p.Section);

            if (filter != null)
            {
                if (filter.Ids != null && filter.Ids.Length > 0)
                {
                    products.Where(p => filter.Ids.Contains(p.Id));
                }
                else
                {
                    if (filter.BrandId != null)
                        products = products.Where(i => i.BrandId == filter.BrandId);
                    if (filter.SectionId != null)
                        products = products.Where(i => i.SectionId == filter.SectionId);
                    if (filter.Limit != null)
                    {
                        int limit2 = (int)filter.Limit;
                        products = products.Take(limit2);
                    }
                }
            }

            products = products.OrderBy(i => i.Order);

            return products;
        }

        public Product GetProductById(int id)
        {
            return _db.Products
                .Include(p => p.Brand)
                .Include(p => p.Section)
                .FirstOrDefault(p => p.Id == id);
        }
    }
}

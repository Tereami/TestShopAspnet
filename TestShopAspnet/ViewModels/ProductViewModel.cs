using DomainModel.Enitities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestShopAspnet.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string ImageName { get; set; }

        public ProductViewModel()
        {

        }

        public ProductViewModel(Product p)
        {
            Id = p.Id;
            Name = p.Name;
            Price = p.Price;
            ImageName = p.ImageName;
        }
    }
}

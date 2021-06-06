using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Enitities.Base;
using DomainModel.Enitities.Base.Interfaces;

namespace DomainModel.Enitities
{
    public class Product : NamedEntity, IOrderedEntity
    {
        public int Order { get; set; }

        public int SectionId { get; set; }

        public int? BrandId { get; set; }

        public string ImageName { get; set; }

        public decimal Price { get; set; }
    }
}

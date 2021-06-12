using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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

        [ForeignKey(nameof(SectionId))]
        public Section Section { get; set; }

        public int? BrandId { get; set; }

        [ForeignKey(nameof(BrandId))]
        public Brand Brand { get; set; }

        public string ImageName { get; set; }

        [Column(TypeName = "decimal(18,2")]
        public decimal Price { get; set; }
    }
}

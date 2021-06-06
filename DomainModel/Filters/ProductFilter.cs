using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Filters
{
    public class ProductFilter
    {
        public int? SectionId { get; set; }
        public int? BrandId { get; set; }
    }
}

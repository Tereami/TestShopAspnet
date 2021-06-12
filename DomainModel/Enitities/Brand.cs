using DomainModel.Enitities.Base;
using DomainModel.Enitities.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Enitities
{
    public class Brand : NamedEntity, IOrderedEntity
    {
        public int Order { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}

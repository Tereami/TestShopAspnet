using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Enitities.Base;
using DomainModel.Enitities;

namespace DomainModel.Enitities
{
    public class CartItem
    {
        public int ProductId { get; set; }

        public int Quantity { get; set; } = 1;
    }
}

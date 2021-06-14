using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Enitities.Base;
using DomainModel.Enitities;

namespace DomainModel.Enitities
{
    public class Cart
    {
        public ICollection<CartItem> Items { get; set; } = new List<CartItem>();

        public int ItemsCount()
        {
            int count = Items.Sum(i => i.Quantity);
            return count;
        }
    }
}

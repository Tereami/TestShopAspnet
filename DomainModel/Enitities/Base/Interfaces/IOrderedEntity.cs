using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Enitities.Base.Interfaces
{
    interface IOrderedEntity : IEntity
    {
        int Order { get; }
    }
}

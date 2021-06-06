using DomainModel.Enitities.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Enitities.Base
{
    public abstract class Entity : IEntity
    {
        public int Id { get; set; }
    }
}

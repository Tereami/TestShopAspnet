using DomainModel.Enitities.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Enitities.Base
{
    public abstract class NamedEntity : Entity, INamedEntity
    {
        public string Name { get; set; }
    }
}

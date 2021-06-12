using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomainModel.Enitities;

namespace TestShopAspnet.Services.Interfaces
{
    public interface IPersonsData
    {
        IEnumerable<Person> GetAll();

        Person Get(int id);

        int Add(Person pers);

        void Update(Person pers);

        bool Delete(int id);
    }
}

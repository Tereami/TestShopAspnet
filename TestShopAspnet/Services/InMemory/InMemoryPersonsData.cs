using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestShopAspnet.Services.Interfaces;
using DomainModel.Enitities;

namespace TestShopAspnet.Services.InMemory
{
    public class InMemoryPersonsData : IPersonsData
    {
        public Person Get(int id)
        {
            var pers = Data.TestData._persons.SingleOrDefault(p => p.Id == id);
            return pers;
        }

        public IEnumerable<Person> GetAll()
        {
            return Data.TestData._persons;
        }

        public int Add(Person pers)
        {
            if (pers is null) throw new ArgumentNullException(nameof(pers));

            if (Data.TestData._persons.Contains(pers)) return pers.Id;

            Data.TestData._persons.Add(pers);
            return pers.Id;
        }

        public void Update(Person pers)
        {
            if (pers is null) throw new ArgumentNullException(nameof(pers));

            if (Data.TestData._persons.Contains(pers)) return;

            Person dbPers = Get(pers.Id);
            if (dbPers is null) return;

            //этот костыль можно убрать через библиотеку AutoMapper
            dbPers.Name = pers.Name;
            dbPers.Surname = pers.Surname;
            dbPers.Age = pers.Age;
            dbPers.Position = pers.Position;
        }

        public bool Delete(int id)
        {
            Person dbPers = Get(id);
            if (dbPers is null) return false;
            return Data.TestData._persons.Remove(dbPers);
        }
    }
}

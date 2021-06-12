using DomainModel.Enitities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestShopAspnet.Services.Interfaces;
using DataAccessLayer.Context;

namespace TestShopAspnet.Services.InSQL
{
    public class InSqlPersonsData : IPersonsData
    {
        private readonly DB _db;

        public InSqlPersonsData(DB db)
        {
            _db = db;
        }


        public int Add(Person pers)
        {
            if (pers is null) throw new ArgumentNullException(nameof(pers));

            if (_db.Persons.Contains(pers)) return pers.Id;

            _db.Persons.Add(pers);
            return pers.Id;
        }

        public bool Delete(int id)
        {
            Person dbPers = Get(id);
            if (dbPers is null) return false;
            var result = _db.Persons.Remove(dbPers);

            if (result != null)
                return true;
            else
                return false;
        }

        public Person Get(int id)
        {
            Person pers = _db.Persons.SingleOrDefault(p => p.Id == id);
            return pers;
        }

        public IEnumerable<Person> GetAll()
        {
            return _db.Persons;
        }

        public void Update(Person pers)
        {
            if (pers is null) throw new ArgumentNullException(nameof(pers));

            //if (_db.Persons.Contains(pers)) return;

            Person dbPers = Get(pers.Id);
            if (dbPers is null) return;

            //этот костыль можно убрать через библиотеку AutoMapper
            dbPers.Name = pers.Name;
            dbPers.Surname = pers.Surname;
            dbPers.Age = pers.Age;
            dbPers.Position = pers.Position;
        }
    }
}

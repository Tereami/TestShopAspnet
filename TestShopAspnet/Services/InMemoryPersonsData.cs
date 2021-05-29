using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestShopAspnet.Models;
using TestShopAspnet.Services.Interfaces;

namespace TestShopAspnet.Services
{
    public class InMemoryPersonsData : IPersonsData
    {
        private readonly List<Person> _persons = new List<Person>
        {
            new Person("Иван", "Иванов", 21, "Инженер"),
            new Person("Пётр", "Петров", 31, "Директор"),
            new Person("Сидор", "Сидоров", 41)
        };

        private int _CurrentMaxId = -1;

        public InMemoryPersonsData()
        {
            _CurrentMaxId = _persons.Max(i => i.Id);
        }

        public Person Get(int id)
        {
            var pers = _persons.SingleOrDefault(p => p.Id == id);
            return pers;
        }

        public IEnumerable<Person> GetAll()
        {
            return _persons;
        }

        public int Add(Person pers)
        {
            if (pers is null) throw new ArgumentNullException(nameof(pers));

            if (_persons.Contains(pers)) return pers.Id;

            pers.Id = ++_CurrentMaxId;
            _persons.Add(pers);
            return pers.Id;
        }

        public void Update(Person pers)
        {
            if (pers is null) throw new ArgumentNullException(nameof(pers));

            if (_persons.Contains(pers)) return;

            Person dbPers = Get(pers.Id);
            if (dbPers is null) return;

            dbPers.Name = pers.Name;
            dbPers.Surname = pers.Surname;
            dbPers.Age = pers.Age;
            dbPers.Position = pers.Position;
        }

        public bool Delete(int id)
        {
            Person dbPers = Get(id);
            if (dbPers is null) return false;
            return _persons.Remove(dbPers);
        }
    }
}

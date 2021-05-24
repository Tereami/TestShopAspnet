using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestShopAspnet.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string Position { get; set; }

        private static int Increment = 1;

        public Person(string name, string surname, int age, string pos = "Нет информации...")
        {
            Name = name;
            Surname = surname;
            Age = age;
            Position = pos;
            Id = Person.Increment;
            Person.Increment++;
        }
    }
}

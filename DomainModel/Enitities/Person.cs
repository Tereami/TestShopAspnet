using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using DomainModel.Enitities.Base;
using DomainModel.Enitities.Base.Interfaces;


namespace DomainModel.Enitities
{
    public class Person : NamedEntity
    {
        public string Surname { get; set; }
        public int Age { get; set; }
        public string Position { get; set; }

        public Person() { }

        public Person(string name, string surname, int age, string pos = "Нет информации...")
        {
            Name = name;
            Surname = surname;
            Age = age;
            Position = pos;
        }
    }
}

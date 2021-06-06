using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestShopAspnet.Models;

namespace TestShopAspnet.Data
{
    public static class TestData
    {
        public static List<Person> _persons = new List<Person>
        {
            new Person("Иван", "Иванов", 21, "Инженер"),
            new Person("Пётр", "Петров", 31, "Директор"),
            new Person("Сидор", "Сидоров", 41)
        };
    }
}

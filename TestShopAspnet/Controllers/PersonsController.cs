using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestShopAspnet.Models;

namespace TestShopAspnet.Controllers
{
    public class PersonsController : Controller
    {
        private static readonly List<Person> _persons = new List<Person>
        {
            new Person("Иван", "Иванов", 21, "Инженер"),
            new Person("Пётр", "Петров", 31, "Директор"),
            new Person("Сидор", "Сидоров", 41)
        };


        public IActionResult Index()
        {
            return View(_persons);
        }

        public IActionResult Info(int id)
        {
            Person checkPers = _persons.FirstOrDefault(i => i.Id == id);
            if (checkPers == null)
            {
                return new NotFoundResult();
            }
            return View(checkPers);
        }

        public IActionResult Edit(int id)
        {
            Person checkPers = _persons.FirstOrDefault(i => i.Id == id);
            if (checkPers == null)
            {
                return new NotFoundResult();
            }
            return View(checkPers);
        }
    }
}

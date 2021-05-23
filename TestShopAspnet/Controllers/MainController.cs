using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestShopAspnet.Models;

namespace TestShopAspnet.Controllers
{
    public class MainController : Controller
    {
        private static readonly List<Person> _persons = new List<Person>
        {
            new Person("Иван", "Иванов", 21, "Инженер"),
            new Person("Пётр", "Петров", 31, "Директор"),
            new Person("Сидор", "Сидоров", 41)
        };

        private IConfiguration Configuration;

        public MainController(IConfiguration conf)
        {
            Configuration = conf;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Personal()
        {
            return View(_persons);
        }

        public IActionResult Card(int id)
        {
            var checkPers = _persons.Where(i => i.Id == id).ToList();
            if(checkPers.Count == 0)
            {
                throw new Exception("404! User not found");
            }
            return View(checkPers.First());
        }
    }
}

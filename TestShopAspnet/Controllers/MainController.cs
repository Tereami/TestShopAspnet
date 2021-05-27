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

        public IActionResult PersonInfo(int id)
        {
            Person checkPers = _persons.FirstOrDefault(i => i.Id == id);
            if(checkPers == null)
            {
                return new NotFoundResult();
            }
            return View(checkPers);
        }

        public IActionResult PersonEdit(int id)
        {
            Person checkPers = _persons.FirstOrDefault(i => i.Id == id);
            if (checkPers == null)
            {
                return new NotFoundResult();
            }
            return View(checkPers);
        }

        public IActionResult Blog()
        {
            return View();
        }

        public IActionResult BlogSingle()
        {
            return View();
        }

        public IActionResult Cart()
        {
            return View();
        }

        public IActionResult Checkout()
        {
            return View();
        }

        public IActionResult ContactUs()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult ProductDetails()
        {
            return View();
        }

        public IActionResult Shop()
        {
            return View();
        }
    }
}

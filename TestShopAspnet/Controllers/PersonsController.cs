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

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestShopAspnet.Models;
using TestShopAspnet.Services.Interfaces;

namespace TestShopAspnet.Controllers
{
    public class PersonsController : Controller
    {
        private IPersonsData _personsService;

        public PersonsController(IPersonsData persService)
        {
            _personsService = persService;
        }


        public IActionResult Index()
        {
            return View(_personsService.GetAll());
        }

        public IActionResult Info(int id)
        {
            Person checkPers = _personsService.Get(id);
            if (checkPers == null)
            {
                return new NotFoundResult();
            }
            return View(checkPers);
        }

        public IActionResult Edit(int id)
        {
            Person checkPers = _personsService.Get(id);
            if (checkPers == null)
            {
                return new NotFoundResult();
            }
            return View(checkPers);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestShopAspnet.Models;
using TestShopAspnet.Services.Interfaces;
using TestShopAspnet.ViewModels;

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

        public IActionResult Create()
        {
            return View("Edit", new PersonViewModel());
        }

        public IActionResult Edit(int id)
        {
            Person checkPers = _personsService.Get(id);
            if (checkPers == null)            
                return new NotFoundResult();
            PersonViewModel persvm = new PersonViewModel()
            {
                Id = checkPers.Id,
                Name = checkPers.Name,
                Surname = checkPers.Surname,
                Age = checkPers.Age,
                Position = checkPers.Position
            };
            return View(persvm);
        }

        [HttpPost]
        public IActionResult Edit(PersonViewModel vm)
        {
            if (vm.Name == "XXX")
                ModelState.AddModelError("", "Всё плохо!");

            if(!ModelState.IsValid)
                return View(vm);

            Person p = new Person(vm.Name, vm.Surname, vm.Age, vm.Position);
            p.Id = vm.Id;

            if (p.Id == 0)
                _personsService.Add(p);
            else
                _personsService.Update(p);

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            if (id < 1) return BadRequest();
            Person checkPers = _personsService.Get(id);
            if (checkPers is null)
                return NotFound();

            PersonViewModel persvm = new PersonViewModel()
            {
                Id = checkPers.Id,
                Name = checkPers.Name,
                Surname = checkPers.Surname,
                Age = checkPers.Age,
                Position = checkPers.Position
            };
            return View(persvm);
        }

        [HttpPost]
        public IActionResult DeleteConfirm(int id)
        {
            _personsService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}

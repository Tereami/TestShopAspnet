using DomainModel.Enitities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestShopAspnet.Services.Interfaces;
using TestShopAspnet.ViewModels;

namespace TestShopAspnet.Controllers
{
    public class PersonsController : Controller
    {
        private IPersonsData _personsService;
        private ILogger<PersonsController> _logger;

        public PersonsController(IPersonsData persService, ILogger<PersonsController> Logger)
        {
            _personsService = persService;
            _logger = Logger;
        }


        public IActionResult Index()
        {
            var personsViewModels = _personsService.GetAll()
                .Select(i => new PersonViewModel(i));
            _logger.LogInformation("Отображение списка сотрудников, всего {0} человек", personsViewModels.Count());
            return View(personsViewModels);
        }

        public IActionResult Info(int id)
        {
            Person checkPers = _personsService.Get(id);
            if (checkPers == null)
            {
                _logger.LogWarning("Не найден сотрудник для вывода информации, id{0}", id);
                return new NotFoundResult();
            }
            _logger.LogInformation("Выведена информация о сотруднике id{0}", id);
            return View(checkPers);
        }

        public IActionResult Create()
        {
            _logger.LogInformation("Запрос на добавление сотрудника");
            return View("Edit", new PersonViewModel());
        }

        public IActionResult Edit(int id)
        {
            _logger.LogInformation("Запрос на редактирование сотрудника id{0}", id);
            Person checkPers = _personsService.Get(id);
            if (checkPers == null)
            {
                _logger.LogWarning("Не найден сотрудник для редактирования, id{0}", id);
                return new NotFoundResult();
            }
            PersonViewModel persvm = new PersonViewModel(checkPers);

            _logger.LogInformation("Отправлена форма для редактирования сотрудника id{0}", id);
            return View(persvm);
        }

        [HttpPost]
        public IActionResult Edit(PersonViewModel vm)
        {
            _logger.LogInformation("Редактирование сотрудника id{0}", vm.Id);
            if (vm.Name == "XXX")
                ModelState.AddModelError("", "Всё плохо!");

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Некорректная форма, отправлено обратно клиенту");
                return View(vm);
            }

            Person p = new Person(vm.Name, vm.Surname, vm.Age, vm.Position);
            p.Id = vm.Id;

            if (p.Id == 0)
            {
                _logger.LogInformation("Добавление сотрудника id{0} {1} {2}", vm.Id, vm.Name, vm.Surname);
                _personsService.Add(p);
            }
            else
            {
                _logger.LogInformation("Обновление сотрудника id{0}", vm.Id);
                _personsService.Update(p);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            _logger.LogInformation("Запрос на удаление сотрудника id{0}", id);
            if (id < 1) return BadRequest();

            Person checkPers = _personsService.Get(id);
            if (checkPers is null)
            {
                _logger.LogWarning("Не найден сотрудник для удаления id{0}", id);
                return NotFound();
            }

            PersonViewModel persvm = new PersonViewModel(checkPers);

            _logger.LogInformation("Отправлена форма потверждения удаления");
            return View(persvm);
        }

        [HttpPost]
        public IActionResult DeleteConfirm(int id)
        {
            _personsService.Delete(id);
            _logger.LogInformation("Сотрудник удален id{0}", id);
            return RedirectToAction("Index");
        }
    }
}

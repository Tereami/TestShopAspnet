using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Context;
using DomainModel.Enitities;
using DomainModel.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace TestShopAspnet.Data
{
    public class DbInitializer
    {
        private readonly DB _db;
        private readonly ILogger<DbInitializer> _logger;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public DbInitializer(
            DB db, 
            ILogger<DbInitializer> Logger,
            UserManager<User> UserManager,
            RoleManager<Role> RoleManager)
        {
            _db = db;
            _logger = Logger;
            _userManager = UserManager;
            _roleManager = RoleManager;
        }

        public void Initialize()
        {
            _logger.LogInformation("Инициализация БД");
            System.Diagnostics.Stopwatch timer = Stopwatch.StartNew();
            if (_db.Database.GetPendingMigrations().Any())
            {
                _logger.LogInformation("Применение миграций");
                _db.Database.Migrate();
                _logger.LogInformation("Миграция БД выполнена, прошло {0} секунд", timer.Elapsed.TotalSeconds);
            }
            else
            {
                _logger.LogInformation("Миграция БД не требуется");
            }

            try
            {
                InitializeProducts();
                InitializePersons();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Не удалось инициализировать БД");
            }


            try
            {
                InitializeIdentity().GetAwaiter().GetResult();
            }
            catch(Exception e)
            {
                _logger.LogError(e, "Не удалось инициализировать БД Identity");
            }

            _logger.LogInformation("Инициализация БД выполнена, прошло {0} секунд", timer.Elapsed.TotalSeconds);
        }

        private void InitializeProducts()
        {
            if (_db.Products.Any())
            {
                _logger.LogInformation("Инициализация товаров не требуется");
                return;
            }

            Dictionary<int, Section> sectionsPool = TestData.Sections.ToDictionary(s => s.Id);
            Dictionary<int, Brand> brandsPool = TestData.Brands.ToDictionary(b => b.Id);

            foreach (Section s in TestData.Sections)
            {
                if (s.ParentId == null) continue;
                s.Parent = sectionsPool[(int)s.ParentId];
            }

            foreach (Product p in TestData.Products)
            {
                p.Section = sectionsPool[p.SectionId];
                if (!(p.BrandId is null))
                    p.Brand = brandsPool[(int)p.BrandId];
                p.Id = 0;
                p.BrandId = null;
                p.SectionId = 0;
            }

            foreach (Section s in TestData.Sections)
            {
                s.Id = 0;
                s.ParentId = null;
            }
            foreach (Brand b in TestData.Brands)
                b.Id = 0;


            using (_db.Database.BeginTransaction())
            {
                _logger.LogInformation("Инициализация бд");
                _db.Sections.AddRange(TestData.Sections);
                _db.Brands.AddRange(TestData.Brands);
                _db.Products.AddRange(TestData.Products);
                _db.SaveChanges();
                _db.Database.CommitTransaction();
                _logger.LogInformation("Инициализация бд выполнена");
            }
        }

        private void InitializePersons()
        {
            if (!_db.Persons.Any())
            {
                using (_db.Database.BeginTransaction())
                {
                    _logger.LogInformation("Инициализация сотрудников");
                    _db.Persons.AddRange(TestData._persons);
                    _db.SaveChanges();
                    //для сотрудников id вручную не задается, поэтому IDENTITY_INSERT трогать не надо
                    _db.Database.CommitTransaction();
                    _logger.LogInformation("Инициализация сотрудников выполнена");
                }
            }
            else
            {
                _logger.LogInformation("Инициализация сотрудников не требуется");
            }
        }

        private async Task InitializeIdentity()
        {
            _logger.LogInformation("Инициализация БД Identity");
            Stopwatch timer = Stopwatch.StartNew();

            async Task CheckRole(string RoleName)
            {
                if (!await _roleManager.RoleExistsAsync(RoleName))
                {
                    _logger.LogInformation("Роль {0} отсутствует", RoleName);
                    await _roleManager.CreateAsync(new Role { Name = RoleName });
                    _logger.LogInformation("Роль {0} создана", RoleName);
                }

            }

            await CheckRole(Role.Administrators);
            await CheckRole(Role.Users);

            if(await _userManager.FindByNameAsync(User.Administrator) is null)
            {
                _logger.LogInformation("Пользователь {0} отсутствует", User.Administrator);

                User admin = new User
                {
                    UserName = User.Administrator
                };

                IdentityResult creationResult = await _userManager.CreateAsync(admin, User.DefaultAdminPassword);
                if(creationResult.Succeeded)
                {
                    _logger.LogInformation("Пользователь {0} создан", User.Administrator);

                    await _userManager.AddToRoleAsync(admin, Role.Administrators);
                    _logger.LogInformation("Пользователь {0} наделен ролью {1}", 
                        User.Administrator, Role.Administrators);
                }
                else
                {
                    string errors = string.Join(",", creationResult.Errors.Select(e => e.Description));
                    _logger.LogError("Учетная запись администратора не создана: {0}", errors);
                    throw new InvalidOperationException($"Ошибка при создании пользователя {User.Administrator}: {errors}");
                }
            }


            _logger.LogInformation("Инициализация БД Identity завершена за {0} с", timer.Elapsed.TotalSeconds);
        }
    }
}

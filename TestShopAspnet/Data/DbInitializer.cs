using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Context;
using DomainModel.Enitities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace TestShopAspnet.Data
{
    public class DbInitializer
    {
        private readonly DB _db;
        private readonly ILogger<DbInitializer> _logger;
        public DbInitializer(DB db, ILogger<DbInitializer> Logger)
        {
            _db = db;
            _logger = Logger;
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
    }
}

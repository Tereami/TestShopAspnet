using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Context;
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
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Не удалось инициализировать БД");
            }
            _logger.LogInformation("Инициализация БД выполнена, прошло {0} секунд", timer.Elapsed.TotalSeconds);
        }

        private void InitializeProducts()
        {
            if (!_db.Sections.Any())
            {
                using (_db.Database.BeginTransaction())
                {
                    _logger.LogInformation("Инициализация секций");
                    _db.Sections.AddRange(TestData.Sections);
                    _db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Sections] ON");
                    _db.SaveChanges();
                    _db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Sections] OFF");

                    _db.Database.CommitTransaction();
                    _logger.LogInformation("Инициализация секций выполнена");
                }
            }
            else
            {
                _logger.LogInformation("Инициализация секций не требуется");
            }

            if (!_db.Brands.Any())
            {
                using (_db.Database.BeginTransaction())
                {
                    _logger.LogInformation("Инициализация брендов");
                    _db.Brands.AddRange(TestData.Brands);
                    _db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Brands] ON");
                    _db.SaveChanges();
                    _db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Brands] OFF");

                    _db.Database.CommitTransaction();
                    _logger.LogInformation("Инициализация брендов выполнена");
                }
            }
            else
            {
                _logger.LogInformation("Инициализация брендов не требуется");
            }

            if (!_db.Products.Any())
            {
                using (_db.Database.BeginTransaction())
                {
                    _logger.LogInformation("Инициализация товаров");
                    _db.Products.AddRange(TestData.Products);
                    _db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Products] ON");
                    _db.SaveChanges();
                    _db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Products] OFF");

                    _db.Database.CommitTransaction();
                    _logger.LogInformation("Инициализация товаров выполнена");
                }
            }
            else
            {
                _logger.LogInformation("Инициализация товаров не требуется");
            }


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

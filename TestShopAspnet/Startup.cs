using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.Runtime;
using TestShopAspnet.Services.Interfaces;
using TestShopAspnet.Services.InMemory;
using TestShopAspnet.Services.InSQL;
using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using DomainModel.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;


namespace TestShopAspnet
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration conf)
        {
            this.Configuration = conf;
        }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DB>(opt => opt.UseSqlServer(Configuration.GetConnectionString("MSSQL")));
            services.AddTransient<Data.DbInitializer>();

            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<DB>()
                .AddDefaultTokenProviders();
            services.Configure<IdentityOptions>(opt =>
            {
#if DEBUG
                opt.Password.RequireDigit = false;
                opt.Password.RequiredLength = 3;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequiredUniqueChars = 3;
#endif

                opt.Password.RequireNonAlphanumeric = false;
                opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ-_";

                opt.Lockout.AllowedForNewUsers = false; //не блокировать новых юзеров
                opt.Lockout.MaxFailedAccessAttempts = 10; //блокриовать учетку после 10 неудачных ввода пароля
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15); //...на 15 минут

                opt.User.RequireUniqueEmail = false; // а вот это не понял зачем
            }
            );

            services.ConfigureApplicationCookie(opt =>
            {
                opt.Cookie.Name = "TestShopAspNet";
                opt.Cookie.HttpOnly = true; //для повышения безопасности
                opt.ExpireTimeSpan = TimeSpan.FromDays(10); //хранить куки не более 10 дней

                opt.LoginPath = "/Account/Login"; //контроллер у меня будет называться Account
                opt.LogoutPath = "/Account/Logout";
                opt.AccessDeniedPath = "/Account/AccessDenied";

                opt.SlidingExpiration = true; //выдать новые коки после авторизации
            }
            );

            services.AddScoped<IPersonsData, InSqlPersonsData>();

            services.AddScoped<IProductData, InSqlProductData>();

            services.AddScoped<ICartService, Services.InCookies.InCookiesCartService>();

            services.AddControllersWithViews()
                .AddRazorRuntimeCompilation();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider services)
        {
            using (var scope = services.CreateScope())
            {
                scope.ServiceProvider.GetRequiredService<Data.DbInitializer>().Initialize();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.Use(
              async (context, next) =>
              {
                  await next();
              });

            app.Map("/testmap", opt => opt.Run(async context =>
            {
                await Task.Delay(100);
            }));

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/mymessage", async context =>
                {
                    await context.Response.WriteAsync(Configuration["MyMessage"]);
                });

                endpoints.MapControllerRoute("default", "{controller=Main}/{action=Index}/{id?}");
            });
        }
    }
}

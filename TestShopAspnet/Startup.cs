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
            services.AddControllersWithViews()
                .AddRazorRuntimeCompilation();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting();

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

                //endpoints.MapDefaultControllerRoute();
                endpoints.MapControllerRoute("default", "{controller=Main}/{action=Index}/{id?}");
            });
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestShopAspnet.Controllers
{
    public class MainController : Controller
    {
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
            return Content(Configuration["MyMessage"]);
        }
    }
}

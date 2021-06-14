using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TestShopAspnet.Components
{
    public class UserInfoViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            if(User.Identity != null)
            {
                if(User.Identity.IsAuthenticated)
                {
                    return View("UserInfo");
                }
            }
            return View("Default");
        }
    }
}

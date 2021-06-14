using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestShopAspnet.Services.Interfaces;
using TestShopAspnet.ViewModels;

namespace TestShopAspnet.Services.InCookies
{
    public class InCookiesCartService : ICartService
    {
        private readonly IHttpContextAccessor _httpAccessor;
        private readonly IProductData _productData;
        private readonly string _cartName;

        public InCookiesCartService(
            IHttpContextAccessor HttpAccessor, 
            IProductData ProductData
            )
        {
            _httpAccessor = HttpAccessor;
            _productData = ProductData;

            var user = HttpAccessor.HttpContext.User;
            string username = "-Anonymous";
            if (user.Identity.IsAuthenticated)
                username = "-" + user.Identity.Name;
            _cartName = "TestShopAspnet.Cart" + username;
        }

        public void Add(int id)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public void Decrement(int id)
        {
            throw new NotImplementedException();
        }

        public CartViewModel GetViewModel()
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestShopAspnet.Services.Interfaces;
using TestShopAspnet.ViewModels;
using DomainModel.Enitities;
using Newtonsoft.Json;

namespace TestShopAspnet.Services.InCookies
{
    public class InCookiesCartService : ICartService
    {
        private readonly IHttpContextAccessor _httpAccessor;
        private readonly IProductData _productData;
        private readonly string _cartName;

        private Cart Cart
        {
            get
            {
                var context = _httpAccessor.HttpContext;
                var cookies = context.Response.Cookies;

                var cartCookie = context.Request.Cookies[_cartName];
                if(cartCookie is null)
                {
                    Cart newCart = new Cart();
                    cookies.Append(_cartName, JsonConvert.SerializeObject(newCart));
                    return newCart;
                }

                replaceCookies(cookies, cartCookie);
                return JsonConvert.DeserializeObject<Cart>(cartCookie);
            }
            set
            {
                string cookie = JsonConvert.SerializeObject(value);
                replaceCookies(_httpAccessor.HttpContext.Response.Cookies, cookie);
            }
        }

        private void replaceCookies(IResponseCookies cookies, string cookie)
        {
            cookies.Delete(_cartName);
            cookies.Append(_cartName, cookie);
        }

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
            Cart cart = Cart;

            CartItem item = Cart.Items.FirstOrDefault(i => i.ProductId == id);
            if(item == null)
            {
                cart.Items.Add(new CartItem { ProductId = id, Quantity = 1 });
            }
            else
            {
                item.Quantity++;
            }

            Cart = cart;
        }

        public void Clear(int id)
        {
            Cart cart = Cart;
            cart.Items.Clear();
            Cart = cart;
        }

        public void Decrement(int id)
        {
            Cart cart = Cart;

            CartItem item = Cart.Items.FirstOrDefault(i => i.ProductId == id);
            if (item == null) return;

            if (item.Quantity > 0)
                item.Quantity--;

            if (item.Quantity <= 0)
                cart.Items.Remove(item);

            Cart = cart;
        }

        public void Remove(int id)
        {
            Cart cart = Cart;

            CartItem item = Cart.Items.FirstOrDefault(i => i.ProductId == id);
            if (item == null) return;

            cart.Items.Remove(item);

            Cart = cart;
        }

        public CartViewModel GetViewModel()
        {

        }
    }
}

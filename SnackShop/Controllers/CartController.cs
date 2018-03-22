using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SnackShop.Core.Models;
using SnackShop.Core.Repositories;
using SnackShop.Core.Services;

namespace SnackShop.Controllers
{
    public class CartController : Controller
    {
        private CartService CartService;
        private CartModel Cart;

        public CartController(IConfiguration configuration)
        {
            this.CartService = new CartService(
                new CartRepository(
                    configuration.GetConnectionString("ConnectionString")));
        }

        public IActionResult Index()
        {
            var cookie = Request.Cookies["CartID"];
            this.Cart = this.CartService.GetCart(cookie);
            return View(Cart);
        }

        [Route("cart/add/{productId?}")]
        public IActionResult Add(int productId)
        {

            var cartId = this.GetCartCookie();
            var result = this.CartService.AddToCart(productId, cartId);

            return RedirectToAction("Index");
        }

        [Route("cart/remove/{productId?}")]
        public IActionResult Remove(int productId)
        {
            var result = this.CartService.RemoveFromCart(productId, this.Cart.Id);

            return RedirectToAction("Index");
        }

        public string GetCartCookie()
        {
            var cartId = Request.Cookies["CartID"];
            if (cartId == null)
            {
                Guid guid = Guid.NewGuid();
                Response.Cookies.Append("CartID", guid.ToString(), new CookieOptions { Expires = DateTime.Now.AddDays(14) });
                return guid.ToString();
            }

            return cartId;
        }
    }
}
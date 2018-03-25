using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SnackShop.Core.Models;
using SnackShop.Core.Services;
using SnackShop.Core.Repositories;
using System.Dynamic;
using SnackShop.Core.Repositories.Implementations;
using Microsoft.AspNetCore.Http;

namespace SnackShop.Controllers
{
    public class CheckoutController : Controller
    {
        private CartService CartService;
        private CheckoutService CheckoutService;
        private CartModel Cart;
        private OrderModel FinalOrder;

        public CheckoutController(IConfiguration configuration)
        {
            this.CartService = new CartService(
                new CartRepository(
                    configuration.GetConnectionString("ConnectionString")));
            this.CheckoutService = new CheckoutService(
                new CheckoutRepository(
                    configuration.GetConnectionString("ConnectionString")));
        }

        [HttpPost]
        public IActionResult Index(string cartId)
        {
            this.Cart = this.CartService.GetCart(cartId);
            return View(this.Cart);
        }

        [HttpPost]
        public IActionResult Order(OrderModel order)
        {
            this.FinalOrder = order;
            this.FinalOrder.Id = this.GetRandomOrderNumber();
            var result = this.CheckoutService.PlaceOrder(this.FinalOrder);
            Response.Cookies.Append("CartID", "", new CookieOptions { Expires = DateTime.Now.AddDays(-1) });
            ViewBag.Order = this.FinalOrder;
            ViewBag.Cart = this.CartService.GetCart(this.FinalOrder.CartId);
            return View(new { order, cart = this.CartService.GetCart(order.CartId) });
        }

        public string GetRandomOrderNumber()
        {
            string numberString = "";
            for (var i = 0; i < 8; i++)
            {
                numberString += new Random().Next(0, 9);
            }

            return numberString;
        }
    }
}
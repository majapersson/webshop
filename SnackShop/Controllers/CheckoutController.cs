using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SnackShop.Core.Models;
using SnackShop.Core.Services;
using SnackShop.Core.Repositories;

namespace SnackShop.Controllers
{
    public class CheckoutController : Controller
    {
        private CartService CartService;
        private CartModel Cart;

        public CheckoutController(IConfiguration configuration)
        {
            this.CartService = new CartService(
                new CartRepository(
                    configuration.GetConnectionString("ConnectionString")));
        }

        [HttpGet]
        public IActionResult Index()
        {
            var cookie = Request.Cookies["CartID"];
            this.Cart = this.CartService.GetCart(cookie);

            return View(this.Cart);
        }

        [HttpPost]
        public IActionResult Index(OrderModel order)
        {
            return View();
        }
    }
}
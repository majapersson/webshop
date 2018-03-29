﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SnackShop.Core.Models;
using SnackShop.Core.Services;
using SnackShop.Core.Repositories;
using SnackShop.Core.Repositories.Implementations;
using Microsoft.AspNetCore.Http;

namespace SnackShop.Controllers
{
    public class CheckoutController : Controller
    {
        private CartService CartService;
        private OrderService OrderService;

        public CheckoutController(IConfiguration configuration)
        {
            this.CartService = new CartService(
                new CartRepository(
                    configuration.GetConnectionString("ConnectionString")));
            this.OrderService = new OrderService(
                new OrderRepository(
                    configuration.GetConnectionString("ConnectionString")));
        }

        [HttpPost]
        public IActionResult Index(string cartId)
        {
            var cart = this.CartService.GetCart(cartId);
            return View(cart);
        }

        [HttpPost]
        public IActionResult Order(OrderModel order)
        {
            var Order = this.OrderService.PlaceOrder(order);

            if (Order != null)
            {
                Response.Cookies.Append("CartID", "", new CookieOptions { Expires = DateTime.Now.AddDays(-1) });
                ViewBag.Order = Order;
                ViewBag.Cart = this.CartService.GetCart(Order.CartId);
                return View();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
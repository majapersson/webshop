﻿using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SnackShop.Core.Repositories;
using SnackShop.Core.Services;
using SnackShop.Core.Models;
using SnackShop.Models;

namespace SnackShop.Controllers
{
    public class HomeController : Controller
    {
        private ProductService ProductService;
        private string CartId;

        public HomeController(IConfiguration configuration)
        {
            this.ProductService = new ProductService(
                new ProductRepository(
                    configuration.GetConnectionString("ConnectionString")));
        }

        public string GetCartCookie()
        {
            var cartId = Request.Cookies["CartID"];
            if (cartId == null)
            {
                Guid guid = Guid.NewGuid();
                Response.Cookies.Append("CartID", guid.ToString());
                return guid.ToString();
            }

            return cartId;
        }

        public IActionResult Index()
        {
            this.CartId = this.GetCartCookie();
            var products = this.ProductService.GetAll();
            return View(products);
        }

        [Route("product/{slug?}")]
        public IActionResult Product(string slug)
        {
            var product = this.ProductService.Get(slug);

            return View(product);
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

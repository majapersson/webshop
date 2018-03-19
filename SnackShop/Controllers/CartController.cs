using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SnackShop.Core.Repositories;
using SnackShop.Core.Services;

namespace SnackShop.Controllers
{
    public class CartController : Controller
    {
        private CartService CartService;

        public CartController(IConfiguration configuration)
        {
            this.CartService = new CartService(
                new CartRepository(
                    configuration.GetConnectionString("ConnectionString")));
        }

        public IActionResult Index()
        {
            var cookie = Request.Cookies["CartID"];
            var cart = this.CartService.GetCart(cookie);
            return View(cart);
        }

        [Route("cart/add/{productId?}")]
        public void Add(int productId)
        {
            var cartId = Request.Cookies["CartID"];
            var result = this.CartService.AddToCart(productId, cartId);
            
            if (result)
            {
                ViewBag.AddToCartMessage = "The product was added to your cart.";
            }
            else
            {
                ViewBag.AddToCartMessage = "An error occurred, your product was not added to your cart.";
            }
        }

        [Route("cart/remove/{productId?}")]
        public void Remove(int productId)
        {
            var cartId = Request.Cookies["CartID"];
            var result = this.CartService.RemoveFromCart(productId, cartId);
        }
    }
}
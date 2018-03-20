using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

            return RedirectToAction("Index");
        }

        [Route("cart/remove/{productId?}")]
        public IActionResult Remove(int productId)
        {
            var cartId = Request.Cookies["CartID"];
            var result = this.CartService.RemoveFromCart(productId, cartId);

            if (result)
            {
                ViewBag.RemoveFromCartMessage = "The product was removed to your cart.";
            }
            else
            {
                ViewBag.RemoveFromCartMessage = "An error occurred, your product has not been removed from your cart.";
            }

            return RedirectToAction("Index");
        }
    }
}
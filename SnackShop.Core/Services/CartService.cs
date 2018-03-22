using System;
using System.Web;
using System.Net;
using System.Collections.Generic;
using System.Text;
using SnackShop.Core.Models;
using SnackShop.Core.Repositories;

namespace SnackShop.Core.Services
{
    public class CartService
    {
        private readonly CartRepository CartRepository;

        public CartService(CartRepository cartRepository)
        {
            this.CartRepository = cartRepository;
        }

        public CartModel GetCart(string cartId)
        {
            return new CartModel(cartId, this.CartRepository.GetCart(cartId));
        }

        public bool EmptyCart(string cartId)
        {
            return this.CartRepository.EmptyCart(cartId);
        }

        public bool AddToCart(int productId, string cartId)
        {
            return this.CartRepository.AddProduct(productId, cartId);
        }

        public bool RemoveFromCart(int productId, string cartId)
        {
            return this.CartRepository.RemoveProduct(productId, cartId);
        }
    }
}

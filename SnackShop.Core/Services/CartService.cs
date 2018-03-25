using SnackShop.Core.Models;
using SnackShop.Core.Repositories;
using System.Linq;

namespace SnackShop.Core.Services
{
    public class CartService
    {
        private readonly CartRepository CartRepository;
        private readonly ProductRepository ProductRepository;

        public CartService(CartRepository cartRepository)
        {
            this.CartRepository = cartRepository;
            this.ProductRepository = new ProductRepository(this.CartRepository.GetConnectionString());
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
            // Removes any old carts still in database where cookie has expired
            this.CartRepository.RemoveOldCarts();

            // Checks if product exists before adding to cart
            var product = this.ProductRepository.GetAll().SingleOrDefault(x => x.Id == productId);
            
            if (product == null)
            {
                return false;
            }

            return this.CartRepository.AddProduct(productId, cartId);
        }

        public bool RemoveFromCart(int productId, string cartId)
        {
            // Checks if product exists in cart before removing
            var cartProduct = this.CartRepository.GetCart(cartId).SingleOrDefault(x => x.Id == productId);

            if (cartProduct == null)
            {
                return false;
            }
            return this.CartRepository.RemoveProduct(productId, cartId);
        }
    }
}

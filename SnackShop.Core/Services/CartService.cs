using SnackShop.Core.Models;
using SnackShop.Core.Repositories;
using System.Linq;

namespace SnackShop.Core.Services
{
    public class CartService
    {
        private readonly ICartRepository CartRepository;
        private readonly IProductRepository ProductRepository;

        public CartService(ICartRepository cartRepository, IProductRepository productRepository)
        {
            this.CartRepository = cartRepository;
            this.ProductRepository = productRepository;
        }

        public CartModel GetCart(string cartId)
        {
            if(string.IsNullOrWhiteSpace(cartId))
            {
                return null;
            }

            return new CartModel(cartId, this.CartRepository.GetCart(cartId));
        }

        public bool EmptyCart(string cartId)
        {
            if (string.IsNullOrWhiteSpace(cartId))
            {
                return false;
            }

            return this.CartRepository.EmptyCart(cartId);
        }

        public bool AddToCart(int productId, string cartId)
        {
            this.CartRepository.RemoveOldCarts();

            if (productId <= 0 || string.IsNullOrWhiteSpace(cartId))
            {
                return false;
            }
            
            var productExists = this.ProductRepository.GetAll().Any(x => x.Id == productId);
            
            if (!productExists)
            {
                return false;
            }

            return this.CartRepository.AddProduct(productId, cartId);
        }

        public bool RemoveFromCart(int productId, string cartId)
        {
            if (productId <= 0 || string.IsNullOrWhiteSpace(cartId))
            {
                return false;
            }

            var productExistsInCart = this.CartRepository.GetCart(cartId).Any(x => x.ProductId == productId);

            if (!productExistsInCart)
            {
                return false;
            }
            return this.CartRepository.RemoveProduct(productId, cartId);
        }
    }
}

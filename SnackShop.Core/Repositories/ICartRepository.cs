using SnackShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SnackShop.Core.Repositories
{
    public interface ICartRepository
    {
        List<CartProductModel> GetCart(string cartId);
        bool EmptyCart(string cartId);
        bool AddProduct(int productId, string cartId);
        bool RemoveProduct(int productId, string cartId);
        void RemoveOldCarts();
    }
}

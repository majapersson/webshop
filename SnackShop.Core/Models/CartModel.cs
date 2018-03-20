using System;
using System.Collections.Generic;
using System.Text;

namespace SnackShop.Core.Models
{
    public class CartModel
    {
        public string CartId { get; }
        public List<CartProductModel> Items { get; }

        public CartModel(string cartId, List<CartProductModel> items)
        {
            this.CartId = cartId;
            this.Items = items;
        }

        public decimal GetTotals()
        {
            decimal totalPrice = new decimal();
            foreach (CartProductModel product in this.Items)
            {
                totalPrice += product.Price * product.Qty;
            }

            return totalPrice;
        }
    }
}

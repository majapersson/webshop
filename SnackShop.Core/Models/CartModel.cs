﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnackShop.Core.Models
{
    public class CartModel
    {
        public string Id { get; set; }
        public List<CartProductModel> Items { get; set; }

        public CartModel(string cartId, List<CartProductModel> items)
        {
            this.Id = cartId;
            this.Items = items;
        }

        public bool IsEmpty()
        {
            return (string.IsNullOrEmpty(this.Id) && !Items.Any());
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

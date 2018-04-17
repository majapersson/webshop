using System;
using System.Collections.Generic;
using System.Text;

namespace SnackShop.Core.Models
{
    public class OrderModel
    {
        public string OrderId { get; set; }
        public string CartId { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public bool IsOpen { get; set; }
        public DateTime Date { get; set; }
        public List<CartProductModel> Items { get; set; }

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

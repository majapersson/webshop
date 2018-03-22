using System;
using System.Collections.Generic;
using System.Text;

namespace SnackShop.Core.Models
{
    public class CartProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Qty { get; set; }
        public decimal Price { get; set; }
    }
}

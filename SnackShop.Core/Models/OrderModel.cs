using System;
using System.Collections.Generic;
using System.Text;

namespace SnackShop.Core.Models
{
    public class OrderModel
    {
        public string Id;
        public CartModel Cart;
        public string Name;
        public string Address;
        public string Email;
        public string Phone;
    }
}

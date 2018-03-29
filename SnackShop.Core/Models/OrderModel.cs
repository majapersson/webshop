using System;
using System.Collections.Generic;
using System.Text;

namespace SnackShop.Core.Models
{
    public class OrderModel
    {
        public string Id { get; set; }
        public string CartId { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime Date { get; set; }
        public bool IsOpen { get; set; }
    }
}

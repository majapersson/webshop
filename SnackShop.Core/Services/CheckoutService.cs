using SnackShop.Core.Models;
using SnackShop.Core.Repositories.Implementations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SnackShop.Core.Services
{
    public class CheckoutService
    {
        private CheckoutRepository CheckoutRepository;

        public CheckoutService(CheckoutRepository checkoutRepository)
        {
            this.CheckoutRepository = checkoutRepository;
        }

        public bool PlaceOrder(OrderModel order)
        {
            return this.CheckoutRepository.PlaceOrder(order);
        }
    }
}

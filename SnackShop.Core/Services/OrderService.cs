using SnackShop.Core.Models;
using SnackShop.Core.Repositories.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SnackShop.Core.Services
{
    public class OrderService
    {
        private OrderRepository OrderRepository;

        public OrderService(OrderRepository orderRepository)
        {
            this.OrderRepository = orderRepository;
        }

        public OrderModel PlaceOrder(OrderModel order)
        {
            // Checks if cartId already exists
            var existingOrderFromCart = this.OrderRepository.GetOrders().Any(x => x.CartId == order.CartId);
            if ( existingOrderFromCart == true )
            {
                return null;
            }

            order.Id = this.GetRandomOrderNumber();
            order.Date = new DateTime();

            if (this.OrderRepository.PlaceOrder(order))
            {
                return order;
            }

            return null;
        }

        public bool CloseOrder(string orderId)
        {
            return this.OrderRepository.CloseOrder(orderId);
        }

        public List<OrderModel> GetOpenOrders()
        {
            return this.OrderRepository.GetOpenOrders();
        }

        public List<OrderModel> GetClosedOrders()
        {
            return this.OrderRepository.GetClosedOrders();
        }

        public string GetRandomOrderNumber()
        {
            string numberString;
            OrderModel existingOrder;

            do
            {
                numberString = "";
                for (var i = 0; i < 8; i++)
                {
                    numberString += new Random().Next(0, 9);
                }

                // Checks if numberString already exists in database before returning
                existingOrder = this.OrderRepository.GetOrders().SingleOrDefault(x => x.Id == numberString);
            } while (existingOrder != null);

            return numberString;
        }
    }
}

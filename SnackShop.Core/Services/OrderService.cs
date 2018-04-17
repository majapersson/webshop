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

        public OrderModel PlaceOrder(OrderModel order, CartModel cart)
        {
            var orderAlreadyExists = this.OrderRepository.GetAllOrders().Any(x => x.CartId == order.CartId);
            if ( orderAlreadyExists )
            {
                return null;
            }

            order.OrderId = this.GetRandomOrderNumber();

            if (this.OrderRepository.InsertOrderInfo(order))
            {
                foreach (CartProductModel product in cart.Items)
                {
                    this.OrderRepository.InsertOrderProduct(order.OrderId, product);
                }

                return this.OrderRepository.GetOrder(order.OrderId);
            }

            return null;
        }

        public List<OrderModel> GetOrders()
        {
            return this.OrderRepository.GetAllOrders();
        }

        public OrderModel GetOrder(string Id)
        {
            return this.OrderRepository.GetOrder(Id);
        }

        public bool CloseOrder(string orderId)
        {
            return this.OrderRepository.CloseOrder(orderId);
        }

        public string GetRandomOrderNumber()
        {
            string numberString;
            OrderModel orderNumberAlreadyExists;

            do
            {
                numberString = "";
                for (var i = 0; i < 8; i++)
                {
                    numberString += new Random().Next(0, 9);
                }
                
               orderNumberAlreadyExists = this.OrderRepository.GetAllOrders().SingleOrDefault(x => x.OrderId == numberString);
            } while (orderNumberAlreadyExists != null);

            return numberString;
        }
    }
}

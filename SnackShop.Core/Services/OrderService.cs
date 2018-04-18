using SnackShop.Core.Models;
using SnackShop.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SnackShop.Core.Services
{
    public class OrderService
    {
        private IOrderRepository OrderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            this.OrderRepository = orderRepository;
        }

        public OrderModel PlaceOrder(OrderModel order, CartModel cart)
        {
            var orderAlreadyExists = this.OrderRepository.GetAll().Any(x => x.CartId == order.CartId);
            if ( orderAlreadyExists )
            {
                return null;
            }

            order.OrderId = this.GetRandomOrderNumber();

            if (this.OrderRepository.InsertInfo(order))
            {
                foreach (CartProductModel product in cart.Items)
                {
                    this.OrderRepository.InsertProduct(order.OrderId, product);
                }

                return this.OrderRepository.Get(order.OrderId);
            }

            return null;
        }

        public List<OrderModel> GetOrders()
        {
            return this.OrderRepository.GetAll();
        }

        public OrderModel GetOrder(string Id)
        {
            if (string.IsNullOrWhiteSpace(Id))
            {
                return null;
            }

            return this.OrderRepository.Get(Id);
        }

        public bool CloseOrder(string orderId)
        {
            if (string.IsNullOrWhiteSpace(orderId))
            {
                return false;
            }

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
                
               orderNumberAlreadyExists = this.OrderRepository.GetAll().SingleOrDefault(x => x.OrderId == numberString);
            } while (orderNumberAlreadyExists != null);

            return numberString;
        }
    }
}

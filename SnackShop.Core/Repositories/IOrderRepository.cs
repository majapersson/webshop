using SnackShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SnackShop.Core.Repositories
{
    public interface IOrderRepository
    {
        List<OrderModel> GetAll();
        OrderModel Get(string orderId);
        bool InsertInfo(OrderModel order);
        bool InsertProduct(string orderId, CartProductModel product);
        bool CloseOrder(string orderId);
    }
}

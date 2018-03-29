using Dapper;
using SnackShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace SnackShop.Core.Repositories.Implementations
{
    public class OrderRepository
    {
        private readonly string ConnectionString;

        public OrderRepository(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public string GetConnectionString()
        {
            return this.ConnectionString;
        }

        public bool PlaceOrder(OrderModel order)
        {
            try
            {
                using (var connection = new SqlConnection(this.ConnectionString))
                {
                    var sql = "INSERT INTO orders (Id, CartId, Name, Street, ZipCode, City, Phone) " +
                        "VALUES (@id, @cartId, @name, @street, @zipcode, @city, @phone)";
                    connection.Execute(sql, new
                    {
                        id = order.Id,
                        cartId = order.CartId,
                        name = order.Name,
                        street = order.Street,
                        zipcode = order.ZipCode,
                        city = order.City,
                        phone = order.Phone
                    });
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool CloseOrder(string orderId)
        {
            try
            {
                using (var connection = new SqlConnection(this.ConnectionString))
                {
                    var sql = "UPDATE orders SET IsOpen = 0 WHERE Id = @orderId";
                    connection.Execute(sql, new { id = orderId });
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public List<OrderModel> GetOrders()
        {
            using (var connection = new SqlConnection(this.ConnectionString))
            {
                var sql = "SELECT * FROM orders";
                return connection.Query<OrderModel>(sql).ToList();
            }
        }

        public List<OrderModel> GetOpenOrders()
        {
            using (var connection = new SqlConnection(this.ConnectionString))
            {
                var sql = "SELECT * FROM orders WHERE isOpen = 1";
                return connection.Query<OrderModel>(sql).ToList();
            }
        }

        public List<OrderModel> GetClosedOrders()
        {
            using (var connection = new SqlConnection(this.ConnectionString))
            {
                var sql = "SELECT * FROM orders WHERE isOpen = 0";
                return connection.Query<OrderModel>(sql).ToList();
            }
        }
    }
}

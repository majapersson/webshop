using Dapper;
using SnackShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace SnackShop.Core.Repositories.Implementations
{
    public class OrderRepository : IOrderRepository
    {
        private readonly string ConnectionString;

        public OrderRepository(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public List<OrderModel> GetAll()
        {
            using (var connection = new SqlConnection(this.ConnectionString))
            {
                var sql = "SELECT * FROM orders ORDER BY IsOpen DESC";
                return connection.Query<OrderModel>(sql).ToList();
            }
        }

        public OrderModel Get(string orderId)
        {
            OrderModel order;
            try
            {
                using (var connection = new SqlConnection(this.ConnectionString))
                {
                    var sql = "SELECT * FROM Orders WHERE OrderId = @orderId";
                    order = connection.QuerySingleOrDefault<OrderModel>(sql, new { orderId });

                    sql = "SELECT * FROM OrderContent WHERE OrderId = @orderId";
                    order.Items = connection.Query<CartProductModel>(sql, new { orderId }).ToList();
                }
            }
            catch (Exception)
            {
                return null;
            }

            return order;
        }

        public bool InsertInfo(OrderModel order)
        {
            try
            {
                using (var connection = new SqlConnection(this.ConnectionString))
                {
                    var sql = "INSERT INTO orders (OrderId, CartId, Name, Street, ZipCode, City, Email) " +
                        "VALUES (@id, @cartId, @name, @street, @zipcode, @city, @email)";
                    connection.Execute(sql, new
                    {
                        id = order.OrderId,
                        cartId = order.CartId,
                        name = order.Name,
                        street = order.Street,
                        zipcode = order.ZipCode,
                        city = order.City,
                        email = order.Email
                    });
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool InsertProduct(string orderId, CartProductModel product)
        {
            try
            {
                using (var connection = new SqlConnection(this.ConnectionString))
                {
                    var sql = "INSERT INTO OrderContent (OrderId, ProductId, Name, Price, Qty) " +
                        "VALUES (@orderId, @productId, @name, @price, @qty)";
                    connection.Execute(sql, new
                    {
                        orderId,
                        productId = product.ProductId,
                        name = product.Name,
                        price = product.Price,
                        qty = product.Qty,
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
                    var sql = "UPDATE orders SET IsOpen = 0 WHERE OrderId = @orderId";
                    connection.Execute(sql, new { orderId });
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}

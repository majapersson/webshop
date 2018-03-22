using Dapper;
using SnackShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace SnackShop.Core.Repositories.Implementations
{
    public class CheckoutRepository
    {
        private readonly string ConnectionString;

        public CheckoutRepository(string connectionString)
        {
            this.ConnectionString = connectionString;
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

        public List<OrderModel> GetOrders()
        {
            return new List<OrderModel>();
        }
    }
}

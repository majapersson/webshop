using Dapper;
using System;
using System.Data.SqlClient;
using SnackShop.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace SnackShop.Core.Repositories
{
    public class CartRepository
    {
        private readonly string ConnectionString;

        public CartRepository(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public string GetConnectionString()
        {
            return this.ConnectionString;
        }

        public List<CartProductModel> GetCart(string cartId)
        {
            using (var connection = new SqlConnection(this.ConnectionString))
            {
                var sql = "SELECT carts.ProductId AS Id, count(carts.ProductId) AS Qty, products.Name, products.Price " +
                    "FROM carts " +
                    "INNER JOIN  products on carts.productId=products.Id " +
                    "WHERE cartId = @cartId GROUP BY carts.ProductID, products.Name, products.Price";
                var result = connection.Query<CartProductModel>(sql, new { cartId }).ToList();
                return result;
            }
        }

        public bool EmptyCart(string cartId)
        {
            try
            {
                using (var connection = new SqlConnection(this.ConnectionString))
                {
                    var sql = "DELETE FROM carts WHERE cartId = @cartId";
                    connection.Execute(sql, new { cartId });
                }
            }
            catch
            {
                return false;
            }

            return true;
        }

        public bool AddProduct(int productId, string cartId)
        {
            try
            {
                using (var connection = new SqlConnection(this.ConnectionString))
                {
                    var sql = "INSERT INTO carts (cartId, productId) VALUES (@cartId, @productId)";
                    connection.Execute(sql, new { cartId, productId });
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool RemoveProduct(int productId, string cartId)
        {
            try
            {
                using (var connection = new SqlConnection(this.ConnectionString))
                {
                    var sql = "DELETE TOP (1) FROM carts WHERE cartId = @cartId AND productId = @productId";
                    connection.Execute(sql, new { cartId, productId });
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public void RemoveOldCarts()
        {
            using (var connection = new SqlConnection(this.ConnectionString))
            {
                var sql = "DELETE FROM carts WHERE DATEDIFF(day, Date, GETDATE()) > 14 AND NOT EXISTS (SELECT * FROM orders WHERE orders.cartId = carts.cartid)";
                connection.Execute(sql);
            }

        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using SnackShop.Core.Models;

namespace SnackShop.Core.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly string ConnectionString;

        public ProductRepository(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public ProductModel Get(string slug)
        {
            using (var connection = new SqlConnection(this.ConnectionString))
            {
                var sql = "SELECT * FROM products WHERE slug = @slug";
                return connection.QuerySingleOrDefault<ProductModel>(sql, new { slug });
            }
        }

        public List<ProductModel> GetAll()
        {
            using (var connection = new SqlConnection(this.ConnectionString))
            {
                var sql = "SELECT * FROM products";
                return connection.Query<ProductModel>(sql).ToList();
            }
        }

        public bool Add(ProductModel product)
        {
            try
            { 
                using (var connection = new SqlConnection(this.ConnectionString))
                {
                    var sql = "INSERT INTO products (name, description, slug, price, imageUrl) " +
                        "VALUES (@name, @desc, @slug, @price, @imageUrl)";
                    connection.Execute(sql, 
                        new { name = product.Name, desc = product.Description, slug = product.Slug,
                            price = product.Price , imageUrl = product.ImageUrl});
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool Edit(ProductModel product)
        {
            try
            {
                using (var connection = new SqlConnection(this.ConnectionString))
                {
                    var sql = "UPDATE products SET name = @name, description = @desc, " +
                        "slug = @slug, price = @price, imageUrl = @imageUrl WHERE id = @id";
                    connection.Execute(sql, 
                        new { name = product.Name, desc = product.Description, slug = product.Slug,
                            price = product.Price, imageUrl = product.ImageUrl, id = product.Id });
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool Delete(int productId)
        {
            try
            { 
                using (var connection = new SqlConnection(this.ConnectionString))
                {
                    var sql = "DELETE FROM products WHERE Id = @productId";
                    connection.Execute(sql, new { productId });
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

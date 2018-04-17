using Slugify;
using SnackShop.Core.Models;
using SnackShop.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnackShop.Core.Services
{
    public class ProductService
    {
        private readonly SlugHelper SlugHelper;
        private readonly IProductRepository ProductRepository;

        public ProductService(IProductRepository productRepository)
        {
            this.ProductRepository = productRepository;
            this.SlugHelper = new SlugHelper();
        }

        public List<ProductModel> GetAll()
        {
            return this.ProductRepository.GetAll();
        }

        public ProductModel Get(string slug)
        {
            if (string.IsNullOrWhiteSpace(slug))
            {
                return null;
            }

            return this.ProductRepository.Get(slug);
        }

        public bool Add(ProductModel product)
        {
            var productAlreadyExists = this.ProductRepository.GetAll().Any(x => x.Name == product.Name);

            if (productAlreadyExists)
            {
                return false;
            }

            product.Slug = this.SlugHelper.GenerateSlug(product.Name);
            return this.ProductRepository.Add(product);
        }

        public bool Edit(ProductModel product)
        {
            product.Slug = this.SlugHelper.GenerateSlug(product.Name);
            return this.ProductRepository.Edit(product);
        }

        public bool Delete(int productId)
        {
            if (productId <= 0)
            {
                return false;
            }

            return this.ProductRepository.Delete(productId);
        }
    }
}

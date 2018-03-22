using Slugify;
using SnackShop.Core.Models;
using SnackShop.Core.Repositories;
using System;
using System.Collections.Generic;
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

        public ProductModel Get(string slug)
        {
            return this.ProductRepository.Get(slug);
        }

        public List<ProductModel> GetAll()
        {
            return this.ProductRepository.GetAll();
        }

        public bool Add(ProductModel product)
        {
            product.Slug = this.SlugHelper.GenerateSlug(product.Name);
            return this.ProductRepository.Add(product);
        }

        public bool Edit(ProductModel product)
        {
            product.Slug = this.SlugHelper.GenerateSlug(product.Name);
            return this.ProductRepository.Edit(product);
        }
    }
}

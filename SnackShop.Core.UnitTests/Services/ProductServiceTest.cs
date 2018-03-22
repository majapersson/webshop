using System;
using System.Collections.Generic;
using System.Text;
using FakeItEasy;
using NUnit.Framework;
using SnackShop.Core.Models;
using SnackShop.Core.Repositories;
using SnackShop.Core.Services;

namespace SnackShop.Core.UnitTests.Services
{
    public class ProductServiceTest
    {
        private ProductService ProductService;
        private IProductRepository ProductRepository;

        [SetUp]
        public void SetUp()
        {
            this.ProductRepository = A.Fake<IProductRepository>();
            this.ProductService = new ProductService(this.ProductRepository);
        }

        [Test]
        public void GetAll_ReturnsExpectedProducts()
        {
            // Arrange
            var products = new List<ProductModel>
            {
                new ProductModel()
            };

            A.CallTo(() => this.ProductRepository.GetAll()).Returns(products);

            // Act
            var result = this.ProductService.GetAll();

            // Assert
            Assert.That(result, Is.EqualTo(products));
        }

        [Test]
        public void Get_GivenValidString_ReturnsExpectedProduct()
        {
            // Arrange
            const string Id = "string";
            var expectedProduct = new ProductModel();

            A.CallTo(() => this.ProductRepository.Get(Id)).Returns(expectedProduct);

            // Act
            var result = this.ProductService.Get(Id);

            // Assert
            Assert.That(result, Is.EqualTo(expectedProduct));
        }
    }
}

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

        [TestCase("")]
        [TestCase(" ")]
        public void Get_GivenInvalidString_ReturnsNull(string slug)
        {
            // Arrange

            // Act
            var result = this.ProductService.Get(slug);

            // Assert
            Assert.That(result, Is.Null);
            A.CallTo(() => this.ProductRepository.Get(A<string>.Ignored)).MustNotHaveHappened();
        }

        [Test]
        public void Add_GivenNewProductModel_ReturnsTrue()
        {
            // Arrange
            var product = new ProductModel { Name = "New Product" };
            var expectedProducts = new List<ProductModel>
            {
                new ProductModel { Name = "Old Product" }
            };

            A.CallTo(() => this.ProductRepository.GetAll()).Returns(expectedProducts);
            A.CallTo(() => this.ProductRepository.Add(product)).Returns(true);

            // Act
            var result = this.ProductService.Add(product);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void Add_GivenExistingProduct_ReturnsFalse()
        {
            // Arrange
            var product = new ProductModel { Name = "existingName" };
            var expectedProducts = new List<ProductModel>
            {
                new ProductModel { Name = "existingName" }
            };

            A.CallTo(() => this.ProductRepository.GetAll()).Returns(expectedProducts);

            // Act
            var result = this.ProductService.Add(product);

            // Assert
            Assert.That(result, Is.False);
            A.CallTo(() => this.ProductRepository.Add(A<ProductModel>.Ignored)).MustNotHaveHappened();
        }

        [Test]
        public void Edit_GivenProduct_ReturnsTrue()
        {
            // Arrange
            var product = new ProductModel { Name = "New Product" };

            A.CallTo(() => this.ProductRepository.Edit(product)).Returns(true);

            // Act
            var result = this.ProductService.Edit(product);

            // Assert
            Assert.That(result, Is.True);
        }

        [TestCase(-1)]
        [TestCase(0)]
        public void Delete_GivenInvalidId_ReturnsFalse(int Id)
        {
            // Act
            var result = this.ProductService.Delete(Id);

            // Assert
            Assert.That(result, Is.False);
            A.CallTo(() => this.ProductRepository.Delete(A<int>.Ignored)).MustNotHaveHappened();
        }
    }
}

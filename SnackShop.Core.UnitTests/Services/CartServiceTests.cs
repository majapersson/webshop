using FakeItEasy;
using NUnit.Framework;
using SnackShop.Core.Models;
using SnackShop.Core.Repositories;
using SnackShop.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace SnackShop.Core.UnitTests.Services
{
    public class CartServiceTests
    {
        private CartService CartService;
        private ICartRepository CartRepository;
        private IProductRepository ProductRepository;

        [SetUp]
        public void SetUp()
        {
            this.ProductRepository = A.Fake<IProductRepository>();
            this.CartRepository = A.Fake<ICartRepository>();
            this.CartService = new CartService(this.CartRepository, this.ProductRepository);
        }

        [Test]
        public void GetCart_GivenValidString_ReturnsExpectedResult()
        {
            // Arrange
            const string Id = "guid";
            var expectedCartItems = new List<CartProductModel>();

            A.CallTo(() => this.CartRepository.GetCart(Id)).Returns(expectedCartItems);

            // Act
            var result = this.CartService.GetCart(Id);

            // Assert
            Assert.That(result.Id, Is.EqualTo(Id));
            Assert.That(result.Items, Is.EqualTo(expectedCartItems));
        }

        [TestCase("")]
        [TestCase(" ")]
        public void GetCart_GivenInvalidString_ReturnsNull(string cartId)
        {
            // Arrange

            // Act
            var result = this.CartService.GetCart(cartId);

            // Assert
            Assert.That(result, Is.Null);
            A.CallTo(() => this.CartRepository.GetCart(A<string>.Ignored)).MustNotHaveHappened();
        }

        [Test]
        public void EmptyCart_GivenValidString_ReturnsTrue()
        {
            // Arrange
            const string Id = "guid";

            A.CallTo(() => this.CartRepository.EmptyCart(Id)).Returns(true);

            // Act
            var result = this.CartService.EmptyCart(Id);

            // Assert
            Assert.That(result, Is.True);
        }

        [TestCase("")]
        [TestCase(" ")]
        public void EmptyCart_GivenInvalidString_ReturnsFalse(string cartId)
        {
            // Arrange

            // Act
            var result = this.CartService.EmptyCart(cartId);

            // Assert
            Assert.That(result, Is.False);
            A.CallTo(() => this.CartRepository.EmptyCart(A<string>.Ignored)).MustNotHaveHappened();
        }

        [Test]
        public void AddToCart_GivenValidParametersAndExistingProduct_ReturnsTrue()
        {
            // Arrange
            const int productId = 1;
            const string cartId = "guid";
            var expectedProducts = new List<ProductModel>
            {
                new ProductModel
                {
                    Id = 1
                }
            };

            A.CallTo(() => this.ProductRepository.GetAll()).Returns(expectedProducts);
            A.CallTo(() => this.CartRepository.AddProduct(productId, cartId)).Returns(true);

            // Act
            var result = this.CartService.AddToCart(productId, cartId);

            // Assert
            Assert.That(result, Is.True);
        }

        [TestCase(0, "")]
        [TestCase(0, " ")]
        [TestCase(0, "guid")]
        [TestCase(1, "")]
        [TestCase(1, " ")]
        public void AddToCart_GivenInvalidParameters_ReturnsFalse(int productId, string cartId)
        {
            // Arrange

            // Act
            var result = this.CartService.AddToCart(productId, cartId);

            // Assert
            Assert.That(result, Is.False);
            A.CallTo(() => this.CartRepository.AddProduct(A<int>.Ignored, A<string>.Ignored)).MustNotHaveHappened();
        }

        [Test]
        public void AddToCart_GivenNonExistingProduct_ReturnsFalse()
        {
            // Arrange
            const int productId = 15;
            const string cartId = "guid";
            var expectedProducts = new List<ProductModel>
            {
                new ProductModel
                {
                    Id = 1
                }
            };

            A.CallTo(() => this.ProductRepository.GetAll()).Returns(expectedProducts);

            // Act
            var result = this.CartService.AddToCart(productId, cartId);

            // Assert
            Assert.That(result, Is.False);
            A.CallTo(() => this.CartRepository.AddProduct(A<int>.Ignored, A<string>.Ignored)).MustNotHaveHappened();
        }

        [Test]
        public void RemoveFromCart_GivenValidParametersAndExistingProduct_ReturnsTrue()
        {
            // Arrange
            const int productId = 1;
            const string cartId = "guid";
            var productList = new List<CartProductModel>
                {
                    new CartProductModel
                    {
                        ProductId = 1,
                    }
                };

            A.CallTo(() => this.CartRepository.GetCart(cartId)).Returns(productList);
            A.CallTo(() => this.CartRepository.RemoveProduct(productId, cartId)).Returns(true);

            // Act
            var result = this.CartService.RemoveFromCart(productId, cartId);

            // Assert
            Assert.That(result, Is.True);

        }

        [TestCase(0, "")]
        [TestCase(0, " ")]
        [TestCase(0, "guid")]
        [TestCase(1, "")]
        [TestCase(1, " ")]
        public void RemoveFromCart_GivenInvalidParameters_ReturnsFalse(int productId, string cartId)
        {
            // Arrange

            // Act
            var result = this.CartService.RemoveFromCart(productId, cartId);

            // Assert
            Assert.That(result, Is.False);
            A.CallTo(() => this.CartRepository.RemoveProduct(A<int>.Ignored, A<string>.Ignored)).MustNotHaveHappened();
        }

        [Test]
        public void RemoveFromCart_GivenProductNotInCart_ReturnsFalse()
        {
            // Arrange
            const int productId = 2;
            const string cartId = "guid";
            var productList = new List<CartProductModel>
            {
                new CartProductModel
                {
                    ProductId = 1,
                }
            };

            A.CallTo(() => this.CartRepository.GetCart(cartId)).Returns(productList);

            // Act
            var result = this.CartService.RemoveFromCart(productId, cartId);

            // Assert
            Assert.That(result, Is.False);
            A.CallTo(() => this.CartRepository.RemoveProduct(A<int>.Ignored, A<string>.Ignored)).MustNotHaveHappened();
        }
    }
}

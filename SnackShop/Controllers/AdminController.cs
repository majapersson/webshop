using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SnackShop.Core.Models;
using SnackShop.Core.Repositories;
using SnackShop.Core.Repositories.Implementations;
using SnackShop.Core.Services;

namespace SnackShop.Controllers
{
    public class AdminController : Controller
    {
        public ProductService ProductService;
        public OrderService OrderService;

        public AdminController(IConfiguration configuration)
        {
            this.ProductService = new ProductService(
                new ProductRepository(
                    configuration.GetConnectionString("ConnectionString")));
            this.OrderService = new OrderService(
                new OrderRepository(
                    configuration.GetConnectionString("ConnectionString")));
        }

        public IActionResult Index()
        {
            var products = this.ProductService.GetAll();
            return View(products);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(ProductModel model)
        {
            var result = this.ProductService.Add(model);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            var product = this.ProductService.Get(id);
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(ProductModel product)
        {
            var result = this.ProductService.Edit(product);

            var viewProduct = this.ProductService.Get(product.Slug);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int productId)
        {
            var result = this.ProductService.Delete(productId);

            return RedirectToAction("Index");
        }

        public IActionResult Orders()
        {
            var orders = this.OrderService.GetOrders();
            return View(orders);
        }

        public IActionResult Order(string id)
        {
            var order = this.OrderService.GetOrder(id);
            return View(order);
        }

        [HttpPost]
        public IActionResult Close(string id)
        {
            this.OrderService.CloseOrder(id);
            return RedirectToAction("Orders");
        }
    }
}
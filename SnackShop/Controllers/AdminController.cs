using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SnackShop.Core.Models;
using SnackShop.Core.Repositories;
using SnackShop.Core.Services;

namespace SnackShop.Controllers
{
    public class AdminController : Controller
    {
        public ProductService ProductService;

        public AdminController(IConfiguration configuration)
        {
            this.ProductService = new ProductService(
                new ProductRepository(
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

            return View(model);
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
    }
}
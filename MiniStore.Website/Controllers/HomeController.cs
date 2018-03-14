using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MiniStore.Application;
using MiniStore.Website.Models;
using MiniStore.Website.ViewModels;

namespace MiniStore.Website.Controllers
{
    public class HomeController : Controller
    {
        private readonly CategoryService _applicationService;
        private readonly ProductService _productService;

        public HomeController(CategoryService applicationService, ProductService productService)
        {
            _applicationService = applicationService;
            _productService = productService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Category(Guid id, int? page, int? count)
        {
            var category = _applicationService.GetCategory(id);
            var products = await _productService.GetProductsForCategory(id, page ?? 1, count ?? 5);
            return View(new CategoryViewModel
            {
                Category = category,
                Products = products.Items
            });
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MiniStore.Application;
using MiniStore.Domain;
using MiniStore.Website.Models;

namespace MiniStore.Website.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationService _applicationService;

        public HomeController(ApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        public IActionResult Index()
        {
            ViewBag.CategoryTree = _applicationService.GetCategoryTree();

            return View();
        }

        public IActionResult Category(Guid id)
        {
            var category = _applicationService.GetCategory(id);
            return View(category);
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

using Microsoft.AspNetCore.Mvc;
using MiniStore.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniStore.Website.ViewComponents
{
    public class CategoryTree : ViewComponent
    {
        private readonly CategoryService _applicationService;

        public CategoryTree(CategoryService applicationService)
        {
            _applicationService = applicationService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int maxPriority, bool isDone)
        {
            return View(_applicationService.GetCategoryTree());
        }
    }
}

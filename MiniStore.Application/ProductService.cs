using MiniStore.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniStore.Application
{
    public class ProductHeaderViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

    public class ProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly CategoryService _categoryService;

        public ProductService(IProductRepository productRepository, CategoryService categoryService)
        {
            _productRepository = productRepository;
            _categoryService = categoryService;
        }

        public IReadOnlyCollection<ProductHeaderViewModel> GetProductsForCategory(Guid categoryId)
        {
            var category = _categoryService.GetCategory(categoryId);
            var query = new Query<Product>()
        }
    }
}

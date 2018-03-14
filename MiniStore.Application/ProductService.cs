using MiniStore.Application.Dto;
using MiniStore.Common;
using MiniStore.Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniStore.Application
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly CategoryService _categoryService;

        public ProductService(IProductRepository productRepository, CategoryService categoryService)
        {
            _productRepository = productRepository;
            _categoryService = categoryService;
        }

        public async Task<PagedResult<ProductHeader>> GetProductsForCategory(Guid categoryId, int page, int count)
        {
            var category = _categoryService.GetCategory(categoryId);
            var query = new Query<Product>(x => category.ProductIds.Contains(x.Id),
                new PagingSettings(page, count),
                new SortingSettings<Product>(x => x.Id, false));

            var data = await _productRepository.Search(query);

            return new PagedResult<ProductHeader>(data.Items.Select(x => new ProductHeader(x)).ToList(),
                data.TotalCount,
                new SortingSettings<ProductHeader>(x => x.Id, false),
                data.PagingSettings);
        }
    }
}

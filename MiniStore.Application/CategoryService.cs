using MiniStore.Domain;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace MiniStore.Application
{
    public class CategoryService
    {
        private static CategoryTreeViewModel _categoryTree;
        private static IDictionary<Guid, Category> _categories;

        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public CategoryTreeViewModel GetCategoryTree()
        {
            if (_categoryTree == null)
            {
                var rootCategories = _categoryRepository.GetRootCategories();

                _categoryTree = new CategoryTreeViewModel
                {
                    Categories = rootCategories.Select(x => new CategoryViewModel(x))
                };
            }

            return _categoryTree;
        }

        public CategoryViewModel GetCategory(Guid id)
        {
            if (_categories == null)
            {
                var rootCategories = _categoryRepository.GetRootCategories();
                _categories = rootCategories
                    .SelectMany(x => x.GetAllChildCategories())
                    .Concat(rootCategories)
                    .ToDictionary(x => x.Id);
            }
            
            if (!_categories.ContainsKey(id))
            {
                return null;
            }

            return new CategoryViewModel(_categories[id]);
        }
    }
}

using MiniStore.Application.Dto;
using MiniStore.Domain;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace MiniStore.Application
{
    public class CategoryService
    {
        private static CategoryTree _categoryTree;
        private static IDictionary<Guid, Domain.Category> _categories;

        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public CategoryTree GetCategoryTree()
        {
            if (_categoryTree == null)
            {
                var rootCategories = _categoryRepository.GetRootCategories();

                _categoryTree = new CategoryTree
                {
                    Categories = rootCategories.Select(x => new Dto.CategoryDto(x))
                };
            }

            return _categoryTree;
        }

        public Dto.CategoryDto GetCategory(Guid id)
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

            return new Dto.CategoryDto(_categories[id]);
        }
    }
}

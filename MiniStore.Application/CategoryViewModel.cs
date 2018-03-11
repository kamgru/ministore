using MiniStore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MiniStore.Application
{
    public class CategoryViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IReadOnlyCollection<CategoryViewModel> Categories => _categories.AsReadOnly();
        public IReadOnlyCollection<Guid> ProductIds => _productIds.AsReadOnly();

        private List<CategoryViewModel> _categories;
        private List<Guid> _productIds;

        public CategoryViewModel(Category category)
        {
            Id = category.Id;
            Name = category.Name;

            _categories = new List<CategoryViewModel>();
            _productIds = category.ProductIds.ToList();

            foreach (var child in category.Categories)
            {
                _categories.Add(new CategoryViewModel(child));
            }
        }
    }
}

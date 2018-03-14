using MiniStore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MiniStore.Application.Dto
{
    public class CategoryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IReadOnlyCollection<CategoryDto> Categories => _categories.AsReadOnly();
        public IReadOnlyCollection<Guid> ProductIds { get; private set; }
        private List<CategoryDto> _categories;

        public CategoryDto(Domain.Category category)
        {
            Id = category.Id;
            Name = category.Name;

            _categories = new List<CategoryDto>();
            ProductIds = category.GetAllProductIdsInHierarchy();

            foreach (var child in category.Categories)
            {
                _categories.Add(new CategoryDto(child));
            }
        }
    }
}

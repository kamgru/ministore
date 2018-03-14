using System;
using System.Collections.Generic;
using System.Linq;

namespace MiniStore.Domain
{
    public class Category
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public bool IsRootCategory { get; private set; }

        private List<Category> _childCategories = new List<Category>();
        private List<Guid> _productIds = new List<Guid>();
        private List<Type> _characteristicTypes = new List<Type>();

        public IReadOnlyCollection<Category> Categories => _childCategories.AsReadOnly();

        /// <summary>
        /// Collection of product ids added to this category
        /// </summary>
        public IReadOnlyCollection<Guid> ProductIds => _productIds.AsReadOnly();

        public Category(Guid id, string name) : this(id, name, false) { }

        public Category(Guid id, string name, bool isRoot)
        {
            Id = id;
            Name = name;
            IsRootCategory = isRoot;
        }

        public static Category Create(string name, bool isRoot = false)
        {
            return new Category(Guid.NewGuid(), name, isRoot);
        }

        /// <summary>
        /// Returns product ids added to this category and all child categories
        /// </summary>
        /// <returns></returns>
        public IReadOnlyCollection<Guid> GetAllProductIdsInHierarchy()
        {
            var childCategoriesProductIds = _childCategories.SelectMany(x => x.GetAllProductIdsInHierarchy());
            return _productIds.Concat(childCategoriesProductIds)
                .ToList()
                .AsReadOnly();
        }

        public void AddChildCategory(Category category)
        {
            if (category.IsRootCategory)
            {
                throw new CategoryRelationshipException();
            }
            
            _childCategories.Add(category);
        }

        public void RemoveChildCategory(Category category)
        {
            if (_childCategories.FirstOrDefault(cat => cat.Id == category.Id) == null)
            {
                throw new EntityNotFoundException();
            }

            _childCategories.Remove(category);
        }

        public void AddProduct(Product product)
        {
            _productIds.Add(product.Id);
        }

        public IReadOnlyCollection<Category> GetAllChildCategories()
        {
            return _childCategories.SelectMany(x => x.GetAllChildCategories())
                .Concat(_childCategories)
                .ToList()
                .AsReadOnly();
        }
    }
}
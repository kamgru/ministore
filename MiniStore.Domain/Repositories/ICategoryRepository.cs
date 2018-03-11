using System;
using System.Collections.Generic;

namespace MiniStore.Domain
{
    public interface ICategoryRepository
    {
        void Add(Category category);
        IReadOnlyCollection<Category> GetRootCategories();
    }
}
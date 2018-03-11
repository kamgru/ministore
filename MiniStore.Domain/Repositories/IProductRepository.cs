using MiniStore.Common;
using System;

namespace MiniStore.Domain
{
    public interface IProductRepository
    {
        Product Get(Guid id);
        void Add(Product product);
        void Update(Product product);
        PagedResult<Product> Search(Query<Product> query);
        PagedResult<Product> SearchByCategory(Category category, Query<Product> query);
    }
}
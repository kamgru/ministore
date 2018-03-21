using MiniStore.Domain;
using MongoDB.Driver;
using System;
using System.Linq;
using System.Collections;
using System.Linq.Expressions;
using MiniStore.Common;
using System.Threading.Tasks;

namespace MiniStore.Infrastructure.Persistence
{
    public class ProductRepository : IProductRepository
    {
        private const string _collectionName = "Products";
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<Product> _collection;

        public ProductRepository(IMongoDatabase database)
        {
            _database = database;
            _collection = _database.GetCollection<Product>(_collectionName);
        }

        public void Add(Product product)
        {
            _collection.InsertOne(product);
        }

        public Product Get(Guid id)
        {
            return _collection
                .Find(x => x.Id == id)
                .FirstOrDefault();
        }

        public async Task<PagedResult<Product>> Search(Query<Product> query)
        {
            var products = _collection
                .Find(query.Predicate);

            var sorting = query.SortingSettings;
            products = sorting.DescendingOrder
                ? products.SortByDescending(sorting.SortBy)
                : products.SortBy(sorting.SortBy);

            var count = products.Count();

            var paging = query.PagingSettings;
            products = products.Skip((paging.Page - 1) * paging.Count)
                .Limit(paging.Count);

            var items = await products.ToListAsync();

            return new PagedResult<Product>(items, count, sorting, paging);
        }

        public void Update(Product product)
        {
            _collection
                .ReplaceOne(x => x.Id == product.Id, product);
        }
    }
}

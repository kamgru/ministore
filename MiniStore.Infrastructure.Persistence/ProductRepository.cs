using MiniStore.Domain;
using MongoDB.Driver;
using System;
using System.Linq;
using System.Collections;
using System.Linq.Expressions;
using MiniStore.Common;

namespace MiniStore.Infrastructure.Persistence
{
    public class ProductRepository : IProductRepository
    {
        private const string _collectionName = "Products";
        private readonly IMongoDatabase _database;

        public ProductRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public void Add(Product product)
        {
            _database.GetCollection<Product>(_collectionName)
                .InsertOne(product);
        }

        public Product Get(Guid id)
        {
            return _database.GetCollection<Product>(_collectionName)
                .Find(x => x.Id == id)
                .FirstOrDefault();
        }

        public PagedResult<Product> Search(Query<Product> query)
        {
            var products = _database.GetCollection<Product>(_collectionName)
                .Find(query.Predicate);

            var sorting = query.SortingSettings;
            products = sorting.DescendingOrder
                ? products.SortByDescending(sorting.SortBy)
                : products.SortBy(sorting.SortBy);

            var count = products.Count();

            var paging = query.PagingSettings;
            products = products.Skip((paging.Page - 1) * paging.Count)
                .Limit(paging.Count);

            return new PagedResult<Product>(products.ToList(), count, sorting, paging);
        }

        public PagedResult<Product> SearchByCategory(Category category, Query<Product> query)
        {
            var cat = Builders<Product>.Filter.In(x => x.Id, category.GetAllProductIdsInHierarchy());

            var q = Builders<Product>.Filter.Where(query.Predicate);

            var and = Builders<Product>.Filter.And(cat, q);

            var items = _database.GetCollection<Product>(_collectionName).Find(and).ToList();
            return new PagedResult<Product>(items, 10, query.SortingSettings, query.PagingSettings);
        }

        public void Update(Product product)
        {
            _database.GetCollection<Product>(_collectionName)
                .ReplaceOne(x => x.Id == product.Id, product);
        }
    }
}

using MiniStore.Domain;
using MongoDB.Driver;
using System;
using System.Linq;
using System.Collections.Generic;
using MongoDB.Bson.Serialization;

namespace MiniStore.Infrastructure.Persistence
{
    public class CategoryRepository : ICategoryRepository
    {
        private const string _collectionName = "Categories";
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<Category> _collection;

        public CategoryRepository(IMongoDatabase database)
        {
            _database = database;
            if (!BsonClassMap.GetRegisteredClassMaps().Any(x => x.ClassType == typeof(Category)))
            {
                BsonClassMap.RegisterClassMap<Category>(x =>
                {
                    x.AutoMap();
                    x.MapField("_childCategories");
                    x.MapField("_productIds");
                });
            }

            _collection = _database.GetCollection<Category>(_collectionName);

        }

        public void Add(Category category)
        {
            if (!category.IsRootCategory)
            {
                throw new Exception("Can only add root category");
            }

            Collection().InsertOne(category);
        }

        public IReadOnlyCollection<Category> GetRootCategories()
        {
            return Collection().Find(_ => true).ToList();
        }

        private IMongoCollection<Category> Collection()
        {
            return _collection;
        }
    }
}

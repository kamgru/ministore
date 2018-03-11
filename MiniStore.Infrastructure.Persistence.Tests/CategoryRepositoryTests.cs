using MiniStore.Domain;
using System;
using Xunit;
using MongoDB.Driver;

namespace MiniStore.Infrastructure.Persistence.Tests
{
    public class CategoryRepositoryTests
    {
        private readonly IMongoDatabase _database = new MongoClient("mongodb://localhost:27017").GetDatabase("MiniStore");

        [Fact]
        public void Test1()
        {
            _database.DropCollection("Categories");

            var repo = new CategoryRepository(_database);

            var rootCategory = new Category(Guid.NewGuid(), "root1", true);
            var cat1 = new Category(Guid.NewGuid(), "cat1");
            var cat2 = new Category(Guid.NewGuid(), "cat2");
            var cat1_1 = new Category(Guid.NewGuid(), "cat1_1");

            var p1 = new Product(Guid.NewGuid(), "p1");
            cat1.AddProduct(p1);

            var p2 = new Product(Guid.NewGuid(), "p2");
            cat2.AddProduct(p2);

            var p1_1 = new Product(Guid.NewGuid(), "p1_1");
            cat1_1.AddProduct(p1_1);

            var root2 = new Category(Guid.NewGuid(), "root2", true);
            var cat3 = new Category(Guid.NewGuid(), "cat3");
            var cat3_1 = new Category(Guid.NewGuid(), "cat3_1");
            var cat3_2 = new Category(Guid.NewGuid(), "cat3_2");

            root2.AddChildCategory(cat3);
            cat3.AddChildCategory(cat3_1);
            cat3.AddChildCategory(cat3_2);

            rootCategory.AddChildCategory(cat1);
            rootCategory.AddChildCategory(cat2);
            cat1.AddChildCategory(cat1_1);

            repo.Add(rootCategory);
            repo.Add(root2);
        }
    }
}

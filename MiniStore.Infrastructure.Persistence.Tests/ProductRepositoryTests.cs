using MiniStore.Domain;
using System;
using Xunit;
using MiniStore.Infrastructure.Persistence;
using MongoDB.Driver;
using System.Linq;
using MiniStore.Common;

namespace MiniStore.Infrastructure.Persistence.Tests
{
    public class ProductRepositoryTests
    {
        private readonly IMongoDatabase _database = new MongoClient("mongodb://localhost:27017").GetDatabase("Tests");

        [Fact]
        public void Test1()
        {
            _database.DropCollection("Products");
            var repository = new ProductRepository(_database);

            var p1 = new Product(Guid.NewGuid(), "test product 1");
            repository.Add(p1);

            var p2 = new Product(Guid.NewGuid(), "test product 2");
            repository.Add(p2);

            var rootCategory = new Category(Guid.NewGuid(), "root", true);
            rootCategory.AddProduct(p1);
            rootCategory.AddProduct(p2);

            var res = repository.SearchByCategory(rootCategory, new Query<Product>(x => x.Name == "test product 2", new PagingSettings(1, 10), new SortingSettings<Product>(x => x.Id, false)));

            Assert.True(res.Items.Count == 1);
            Assert.True(res.Items.First().Id == p2.Id);

        }
    }
}

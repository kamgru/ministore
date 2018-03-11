using FluentAssertions;
using System;
using System.Linq;
using Xunit;

namespace MiniStore.Domain.Tests
{
    public class CategoryTests
    {
        [Fact]
        public void Test1()
        {
            var root = new Category(Guid.NewGuid(), "root", true);
            var cat1 = new Category(Guid.NewGuid(), "cat 1");
            var cat2 = new Category(Guid.NewGuid(), "cat 2");
            var cat1_1 = new Category(Guid.NewGuid(), "cat 1 - 1");

            root.AddChildCategory(cat1);
            root.AddChildCategory(cat2);
            cat1.AddChildCategory(cat1_1);

            var proot1 = new Product(Guid.NewGuid(), "proot1");
            root.AddProduct(proot1);

            var pcat1 = new Product(Guid.NewGuid(), "pcat1");
            cat1.AddProduct(pcat1);

            var pcat2 = new Product(Guid.NewGuid(), "pcat2");
            cat2.AddProduct(pcat2);

            var pcat1_1 = new Product(Guid.NewGuid(), "pcat1_1");
            cat1_1.AddProduct(pcat1_1);

            var allProducts = root.GetProductIds();

            allProducts.Should().HaveCount(4);
            allProducts.Should().BeEquivalentTo(new[] { proot1, pcat1, pcat2, pcat1_1 }.Select(x => x.Id));
        }

        [Fact]
        public void Test2()
        {
            var root = Category.Create("root", true);
            var cat1 = Category.Create("cat1");
            var cat1_1 = Category.Create("cat1_1");
            var cat2 = Category.Create("cat2");
            var cat2_1 = Category.Create("cat2_1");
            cat1.AddChildCategory(cat1_1);
            root.AddChildCategory(cat1);
            cat2.AddChildCategory(cat2_1);
            root.AddChildCategory(cat2);

            var result = root.GetAllChildCategories();

            result.Should().HaveCount(4);
        }
    }
}

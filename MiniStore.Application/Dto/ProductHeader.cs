using MiniStore.Domain;
using System;

namespace MiniStore.Application.Dto
{
    public class ProductHeader
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public ProductHeader(Product product)
        {
            Id = product.Id;
            Name = product.Name;
            Price = product.Price;
        }
    }
}

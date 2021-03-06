﻿using MiniStore.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiniStore.Domain
{
    public interface IProductRepository
    {
        Product Get(Guid id);
        void Add(Product product);
        void Update(Product product);
        Task<PagedResult<Product>> Search(Query<Product> query);
    }
}
using MiniStore.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniStore.Website.ViewModels
{
    public class CategoryViewModel
    {
        public CategoryDto Category { get; set; }
        public IReadOnlyCollection<ProductHeader> Products { get; set; }
    }
}

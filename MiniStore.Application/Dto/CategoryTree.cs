using System.Collections.Generic;

namespace MiniStore.Application.Dto
{
    public class CategoryTree
    {
        public IEnumerable<CategoryDto> Categories { get; set; }
    }
}

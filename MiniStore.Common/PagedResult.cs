using System.Collections.Generic;

namespace MiniStore.Common
{
    public class PagedResult<T>
    {
        public IReadOnlyCollection<T> Items { get; }
        public long TotalCount { get; }
        public SortingSettings<T> SortingSettings { get; }
        public PagingSettings PagingSettings { get; }

        public PagedResult(
            IReadOnlyCollection<T> items, 
            long totalCount, 
            SortingSettings<T> sortingSettings, 
            PagingSettings pagingSettings)
        {
            Items = items;
            TotalCount = totalCount;
            SortingSettings = sortingSettings;
            PagingSettings = pagingSettings;
        }
    }
}
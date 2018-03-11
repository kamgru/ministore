using System;
using System.Linq.Expressions;

namespace MiniStore.Common
{
    public class SortingSettings<T>
    {
        public Expression<Func<T, object>> SortBy { get; }
        public bool DescendingOrder { get; }

        public SortingSettings(Expression<Func<T, object>> sortBy, bool descendingOrder)
        {
            SortBy = sortBy;
            DescendingOrder = descendingOrder;
        }
    }
}
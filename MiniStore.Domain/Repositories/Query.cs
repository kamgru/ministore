using MiniStore.Common;
using System;
using System.Linq.Expressions;

namespace MiniStore.Domain
{
    public class Query<T>
    {
        private Func<Product, bool> p;

        public PagingSettings PagingSettings { get; }
        public SortingSettings<T> SortingSettings { get; }
        public Expression<Func<T, bool>> Predicate { get; }

        public Query(
            Expression<Func<T, bool>> predicate, 
            PagingSettings pagingSettings, 
            SortingSettings<T> sortingSettings)
        {
            PagingSettings = pagingSettings;
            SortingSettings = sortingSettings;
            Predicate = predicate;
        }
    }
}
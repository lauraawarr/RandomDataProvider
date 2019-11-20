using System;
using System.Linq;

namespace RandomApi.Helpers
{
    public static class QueryHelper
    {
        public static IQueryable<T> OrderByRandom<T>(this IQueryable<T> query)
        {
            return (from q in query
                orderby Guid.NewGuid()
                select q);
        }
    }
}
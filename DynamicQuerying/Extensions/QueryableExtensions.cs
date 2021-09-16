using System.Linq;

namespace DynamicQuerying.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> Where<T>(this IQueryable<T> query, Filter filter)
        {
            return query;
        }
    }
}
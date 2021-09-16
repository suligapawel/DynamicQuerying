using System;
using System.Linq;
using System.Linq.Expressions;

namespace DynamicQuerying.Extensions
{
    public static class WhereExtensions
    {
        public static IQueryable<T> Where<T>(this IQueryable<T> query, Filter filter)
        {
            if (filter == null)
                return query;

            var parameter = Expression.Parameter(typeof(T), "entity");
            var property = parameter.CallToString(filter);

            var readyExpression = Expression.Equal(property, Expression.Constant(filter.Value));
            var predicate = Expression.Lambda<Func<T, bool>>(readyExpression, parameter);

            return query.Where(predicate);
        }

        private static Expression CallToString(this Expression parameter, Filter filter)
        {
            var property = Expression.PropertyOrField(parameter, filter.Field);
            return Expression.Call(property, "ToString", null);
        }
    }
}
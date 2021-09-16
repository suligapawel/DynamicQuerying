using System;
using System.Linq;
using System.Linq.Expressions;

namespace DynamicQuerying.Extensions
{
    public static class WhereExtensions
    {
        public static IQueryable<T> Where<T>(this IQueryable<T> query, Filter filter)
        {
            if (filter == null || HasNoField<T>(filter.Field))
                return query;

            var parameter = Expression.Parameter(typeof(T), "entity");
            var property = parameter.CallToUpperString(filter);

            var readyExpression = Expression.Equal(property, Expression.Constant(filter.UpperValue));
            var predicate = Expression.Lambda<Func<T, bool>>(readyExpression, parameter);

            return query.Where(predicate);
        }

        private static bool HasNoField<T>(string filterField)
        {
            var type = typeof(T);
            return type.GetProperty(filterField) == null;
        }

        private static Expression CallToUpperString(this Expression parameter, Filter filter)
        {
            var property = Expression.Property(parameter, filter.Field);
            var asString = Expression.Call(property, "ToString", null);
            return Expression.Call(asString, "ToUpper", null);
        }
    }
}
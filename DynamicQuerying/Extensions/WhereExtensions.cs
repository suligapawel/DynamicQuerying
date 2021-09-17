using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using DynamicQuerying.Handlers;

namespace DynamicQuerying.Extensions
{
    public static class WhereExtensions
    {
        public static IQueryable<T> Where<T>(this IQueryable<T> query, Filter filter)
        {
            if (filter == null || HasNoField<T>(filter.Field))
                return query;

            Expression expression = Expression.Constant(false);
            var parameter = Expression.Parameter(typeof(T), "entity");

            foreach (var value in filter.Values)
            {
                var property = Expression.Property(parameter, filter.Field);
                var propertyType = ((PropertyInfo) property.Member).PropertyType;
                var handler = ObjectHandler.Init(propertyType.Name);

                var parseResult = handler.TryParse(value, out var parsed);
                if (!parseResult) continue;

                var comparableValue = Expression.Constant(parsed);
                expression = Expression.Or(expression, Expression.Equal(property, comparableValue));
            }

            var predicate = Expression.Lambda<Func<T, bool>>(expression, parameter);

            return query.Where(predicate);
        }

        private static bool HasNoField<T>(string filterField)
        {
            var type = typeof(T);
            return type.GetProperty(filterField) == null;
        }
    }
}
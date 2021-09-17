using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;

namespace DynamicQuerying.Extensions
{
    public static class WhereExtensions
    {
        public static IQueryable<T> Where<T>(this IQueryable<T> query, Filter filter)
        {
            if (filter == null || HasNoField<T>(filter.Field))
                return query;

            var parameter = Expression.Parameter(typeof(T), "entity");
            Expression expression = Expression.Constant(false);

            foreach (var value in filter.Values)
            {
                var property = Expression.Property(parameter, filter.Field); // .CallToUpperString(filter);

                var pt = ((PropertyInfo) property.Member).PropertyType;
                var pppp = ConvertType(pt, value);


                var comparableValue = Expression.Constant(pppp);
                expression = Expression.Or(expression, Expression.Equal(property, comparableValue));
            }

            var predicate = Expression.Lambda<Func<T, bool>>(expression, parameter);

            return query.Where(predicate);
        }

        private static object ConvertType(MemberInfo pt, object value)
        {
            if (value == null) return null;

            var valueAsString = value?.ToString();
            if (string.IsNullOrEmpty(valueAsString)) return null;

            return pt.Name switch
            {
                "Guid" => new Guid(valueAsString),
                "Boolean" => bool.Parse(valueAsString),
                "Int32" => int.Parse(valueAsString),
                "Double" => double.Parse(valueAsString),
                "Decimal" => decimal.Parse(valueAsString),
                "String" => valueAsString.ToUpper(),
                "DateTime" => DateTime.Parse(valueAsString),
                _ => throw new NotSupportedException()
            };
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
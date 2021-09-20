using System;
using System.Linq;
using System.Linq.Expressions;
using DynamicQuerying.Dictionaries;
using DynamicQuerying.Handlers;
using DynamicQuerying.Models;

namespace DynamicQuerying.Extensions
{
    public static class WhereExtensions
    {
        static WhereExtensions()
        {
        }

        public static IQueryable<T> Where<T>(this IQueryable<T> query, Filter filter)
        {
            if (filter == null || HasNoField<T>(filter.Field))
                return query;

            var expression = new Expressions<T>(filter.Field);
            var handler = ObjectHandler.Init(!expression.Property.Type.IsEnum ? expression.PropertyType : "Enum");

            foreach (var value in filter.Values)
            {
                var parseResult = handler.TryParse(value, out var parsed);
                if (!parseResult) continue;

                var comparableValue = Expression.Constant(parsed);

                var expressionFromFilterMethod =
                    GetFilterMethod(filter.ComparisonType, expression.Property, comparableValue, handler);

                expression.Concatenate(expressionFromFilterMethod, filter.Operator);
            }

            var predicate = Expression.Lambda<Func<T, bool>>(expression.ResultExpression, expression.Parameter);

            return query.Where(predicate);
        }

        private static Expression GetFilterMethod(ComparisonType filterComparisonType, Expression property,
            Expression comparableValue, ObjectHandler handler)
        {
            return (Expression) handler
                .GetType()
                .GetMethod(filterComparisonType.ToString())
                ?.Invoke(handler, new object[] {property, comparableValue});
        }

        private static bool HasNoField<T>(string filterField)
        {
            var type = typeof(T);
            return type.GetProperty(filterField) == null;
        }
    }
}
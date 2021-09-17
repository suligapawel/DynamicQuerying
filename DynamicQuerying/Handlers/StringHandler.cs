using System.Linq.Expressions;

namespace DynamicQuerying.Handlers
{
    internal class StringHandler : ObjectHandler
    {
        public override object Parse(string value) => value;

        public override bool TryParse(string value, out object result)
        {
            result = value;
            return true;
        }

        private static Expression CallToUpperString(Expression parameter, Filter filter)
        {
            var property = Expression.Property(parameter, filter.Field);
            var asString = Expression.Call(property, "ToString", null);
            return Expression.Call(asString, "ToUpper", null);
        }
    }
}
using System.Linq.Expressions;

namespace DynamicQuerying.Handlers
{
    internal class StringHandler : ObjectHandler
    {
        protected override bool TrySpecifyParse(string value, out object result)
        {
            result = value;
            return true;
        }

        public override Expression Equal(Expression property, Expression value)
            => Expression.Equal(ToUpper(property), ToUpper(value));

        public override Expression NotEqual(Expression property, Expression value)
            => Expression.NotEqual(ToUpper(property), ToUpper(value));

        public override Expression StartsWith(Expression property, Expression value)
            => EqualExpression(property, value, nameof(StartsWith));

        public override Expression Contains(Expression property, Expression value)
            => EqualExpression(property, value, nameof(Contains));

        public override Expression GreaterThan(Expression parameter, Expression value)
            => Expression.Constant(false);

        public override Expression GreaterOrEqual(Expression parameter, Expression value)
            => Expression.Constant(false);

        public override Expression LessThan(Expression parameter, Expression value)
            => Expression.Constant(false);

        public override Expression LessOrEqual(Expression parameter, Expression value)
            => Expression.Constant(false);

        private static Expression EqualExpression(Expression property, Expression value, string methodName)
            => Expression.Call(
                ToUpper(property),
                methodName,
                null,
                ToUpper(value));

        private static Expression ToUpper(Expression property)
            => Expression.Call(property, "ToUpper", null);
    }
}
using System.Linq.Expressions;

namespace DynamicQuerying.Handlers
{
    internal class BooleanHandler : ObjectHandler
    {
        protected override bool TrySpecifyParse(string value, out object result)
        {
            var parseResult = bool.TryParse(value , out bool r);
            result = r;

            return parseResult;
        }
        
        public override Expression GreaterThan(Expression parameter, Expression value)
            => Expression.Constant(false);

        public override Expression GreaterOrEqual(Expression parameter, Expression value)
            => Expression.Constant(false);

        public override Expression LessThan(Expression parameter, Expression value)
            => Expression.Constant(false);

        public override Expression LessOrEqual(Expression parameter, Expression value)
            => Expression.Constant(false);
    }
}
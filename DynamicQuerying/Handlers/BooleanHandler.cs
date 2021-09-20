using System.Linq.Expressions;
using DynamicQuerying.Extensions;

namespace DynamicQuerying.Handlers
{
    internal class BooleanHandler : ObjectHandler
    {
        public override bool TryParse(object value, out object result)
        {
            var parseResult = bool.TryParse(value.AsString() , out bool r);
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
using System.Linq.Expressions;
using DynamicQuerying.Extensions;

namespace DynamicQuerying.Handlers
{
    internal class EnumHandler : ObjectHandler
    {
        public override bool TryParse(object value, out object result)
        {
            var parseResult = int.TryParse(((int) value).AsString(), out var enumValue);
            result = enumValue;

            return parseResult;
        }

        public override Expression Equal(Expression parameter, Expression value) 
            => base.Equal(ConvertToInt(parameter), value);

        public override Expression NotEqual(Expression parameter, Expression value) 
            => base.NotEqual(ConvertToInt(parameter), value);

        public override Expression GreaterThan(Expression parameter, Expression value) 
            => base.GreaterThan(ConvertToInt(parameter), value);

        public override Expression GreaterOrEqual(Expression parameter, Expression value) 
            => base.GreaterOrEqual(ConvertToInt(parameter), value);

        public override Expression LessThan(Expression parameter, Expression value) 
            => base.LessThan(ConvertToInt(parameter), value);

        public override Expression LessOrEqual(Expression parameter, Expression value) 
            => base.LessOrEqual(ConvertToInt(parameter), value);

        private static UnaryExpression ConvertToInt(Expression parameter) 
            => Expression.Convert(parameter, typeof(int));
    }
}
using System.Linq.Expressions;
using System.Reflection;
using DynamicQuerying.Dictionaries;

namespace DynamicQuerying.Models
{
    internal class Expressions<T>
    {
        public ParameterExpression Parameter { get; init; }
        public MemberExpression Property { get; init; }
        public string PropertyType { get; init; }
        public Expression ResultExpression { get; private set; }


        public Expressions(string field)
        {
            ResultExpression = Expression.Constant(false);
            Parameter = Expression.Parameter(typeof(T), "entity");
            Property = Expression.Property(Parameter, field);
            PropertyType = ((PropertyInfo) Property.Member).PropertyType.Name;
        }

        public void Concatenate(Expression expression, Operator @operator)
        {
            ResultExpression = @operator switch
            {
                Operator.And => Expression.And(ResultExpression, expression),
                Operator.Or => Expression.Or(ResultExpression, expression)
            };
        }
    }
}
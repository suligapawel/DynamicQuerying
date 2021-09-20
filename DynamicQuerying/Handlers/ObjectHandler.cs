using System;
using System.Linq.Expressions;

namespace DynamicQuerying.Handlers
{
    internal abstract class ObjectHandler
    {
        private const string SUFFIX = "Handler";

        protected ObjectHandler()
        {
        }

        public static ObjectHandler Init(string handlerName)
        {
            var @namespace = typeof(ObjectHandler).Namespace;

            var type = Type.GetType($"{@namespace}.{handlerName}{SUFFIX}");
            if (type == null) return null;

            return (ObjectHandler) Activator.CreateInstance(type, null);
        }
        
        public virtual Expression Equal(Expression parameter, Expression value)
            => Expression.Equal(parameter, value);
        
        public virtual Expression NotEqual(Expression parameter, Expression value)
            => Expression.NotEqual(parameter, value);
        
        public virtual Expression StartsWith(Expression parameter, Expression value) 
            => Expression.Constant(false);
        
        public virtual Expression Contains(Expression parameter, Expression value) 
            => Expression.Constant(false);
            
        public virtual Expression Between(Expression parameter, Expression value) 
            => Expression.Constant(false);
        
        public virtual Expression GreaterThan(Expression parameter, Expression value) 
            => Expression.GreaterThan(parameter, value);
        
        public virtual Expression GreaterOrEqual(Expression parameter, Expression value) 
            => Expression.GreaterThanOrEqual(parameter, value);
        
        public virtual Expression LessThan(Expression parameter, Expression value) 
            => Expression.LessThan(parameter, value);
        
        public virtual Expression LessOrEqual(Expression parameter, Expression value) 
            => Expression.LessThanOrEqual(parameter, value);
        
        public abstract bool TryParse(object value, out object result);
    }
}

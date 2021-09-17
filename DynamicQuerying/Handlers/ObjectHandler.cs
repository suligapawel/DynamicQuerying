using System;
using System.Linq.Expressions;
using DynamicQuerying.Extensions;

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
        
        public bool TryParse(object value, out object result)
        {
            var valueAsString = value.AsString();

            var parseResult = TrySpecifyParse(valueAsString, out var r);
            result = r;

            return parseResult;
        }

        public virtual Expression Equal(Expression parameter, Expression value)
            => Expression.Equal(parameter, value);
        
        public virtual Expression StartWith(Expression parameter, Expression value) 
            => Expression.Constant(false);
        
        public virtual Expression Contains(Expression parameter, Expression value) 
            => Expression.Constant(false);
            

        protected abstract bool TrySpecifyParse(string value, out object result);
    }
}
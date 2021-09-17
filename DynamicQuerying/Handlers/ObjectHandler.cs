using System;
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

        protected abstract bool TrySpecifyParse(string value, out object result);
    }
}
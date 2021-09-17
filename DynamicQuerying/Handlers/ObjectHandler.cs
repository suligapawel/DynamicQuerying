using System;

namespace DynamicQuerying.Handlers
{
    internal abstract class ObjectHandler
    {
        private const string SUFFIX = "Handler";
        
        public abstract object Parse(string value);
        public abstract bool TryParse(string value, out object result);
        
        public static ObjectHandler Init(string handlerName)
        {
            var @namespace = typeof(ObjectHandler).Namespace;

            var type = Type.GetType($"{@namespace}.{handlerName}{SUFFIX}");
            if (type == null) return null;
            
            return (ObjectHandler) Activator.CreateInstance(type, null);
        }
    }
}
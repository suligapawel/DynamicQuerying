using System;

namespace DynamicQuerying.Handlers
{
    internal class GuidHandler : ObjectHandler
    {
        public override object Parse(string value) => new Guid(value);
        
        public override bool TryParse(string value, out object result)
        {
            var parseResult = Guid.TryParse(value, out var guid);
            result = guid;

            return parseResult;
        }
    }
}
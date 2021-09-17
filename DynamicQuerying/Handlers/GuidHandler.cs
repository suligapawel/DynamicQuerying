using System;

namespace DynamicQuerying.Handlers
{
    internal class GuidHandler : ObjectHandler
    {
        protected override bool TrySpecifyParse(string value, out object result)
        {
            var parseResult = Guid.TryParse(value, out var guid);
            result = guid;

            return parseResult;
        }
    }
}
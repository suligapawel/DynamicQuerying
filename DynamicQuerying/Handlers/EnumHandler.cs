using System;

namespace DynamicQuerying.Handlers
{
    internal class EnumHandler : ObjectHandler
    {
        protected override bool TrySpecifyParse(string value, out object result)
        {
            var parseResult = int.TryParse(value, out var enumValue);
            result = enumValue;

            return parseResult;
        }
    }
}
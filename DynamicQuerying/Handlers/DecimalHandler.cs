using DynamicQuerying.Extensions;

namespace DynamicQuerying.Handlers
{
    internal class DecimalHandler : ObjectHandler
    {
        public override bool TryParse(object value, out object result)
        {
            var parseResult = decimal.TryParse(value.AsString(), out var @decimal);
            result = @decimal;

            return parseResult;
        }
    }
}
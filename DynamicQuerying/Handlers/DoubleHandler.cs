using DynamicQuerying.Extensions;

namespace DynamicQuerying.Handlers
{
    internal class DoubleHandler : ObjectHandler
    {
        public override bool TryParse(object value, out object result)
        {
            var parseResult = double.TryParse(value.AsString(), out var @double);
            result = @double;

            return parseResult;
        }
    }
}
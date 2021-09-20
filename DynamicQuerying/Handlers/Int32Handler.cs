using DynamicQuerying.Extensions;

namespace DynamicQuerying.Handlers
{
    internal class Int32Handler : ObjectHandler
    {
        public override bool TryParse(object value, out object result)
        {
            var parseResult = int.TryParse(value.AsString(), out var @int);
            result = @int;

            return parseResult;
        }
    }
}
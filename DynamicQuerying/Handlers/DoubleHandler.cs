namespace DynamicQuerying.Handlers
{
    internal class DoubleHandler : ObjectHandler
    {
        protected override bool TrySpecifyParse(string value, out object result)
        {
            var parseResult = double.TryParse(value, out var @double);
            result = @double;

            return parseResult;
        }
    }
}
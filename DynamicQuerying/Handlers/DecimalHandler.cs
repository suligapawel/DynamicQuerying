namespace DynamicQuerying.Handlers
{
    internal class DecimalHandler : ObjectHandler
    {
        protected override bool TrySpecifyParse(string value, out object result)
        {
            var parseResult = decimal.TryParse(value, out var @decimal);
            result = @decimal;

            return parseResult;
        }
    }
}
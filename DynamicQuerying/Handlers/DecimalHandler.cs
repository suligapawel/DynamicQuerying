namespace DynamicQuerying.Handlers
{
    internal class DecimalHandler : ObjectHandler
    {
        public override object Parse(string value) => decimal.Parse(value);
        
        public override bool TryParse(string value, out object result)
        {
            var parseResult = decimal.TryParse(value, out var @decimal);
            result = @decimal;

            return parseResult;
        }
    }
}
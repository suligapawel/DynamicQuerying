namespace DynamicQuerying.Handlers
{
    internal class DoubleHandler : ObjectHandler
    {
        public override object Parse(string value) => double.Parse(value);
        
        public override bool TryParse(string value, out object result)
        {
            var parseResult = double.TryParse(value, out var @double);
            result = @double;

            return parseResult;
        }
    }
}
namespace DynamicQuerying.Handlers
{
    internal class Int32Handler : ObjectHandler
    {
        public override object Parse(string value) => int.Parse(value);
        
        public override bool TryParse(string value, out object result)
        {
            var parseResult = int.TryParse(value, out var @int);
            result = @int;

            return parseResult;
        }
    }
}
namespace DynamicQuerying.Handlers
{
    internal class Int32Handler : ObjectHandler
    {
        protected override bool TrySpecifyParse(string value, out object result)
        {
            var parseResult = int.TryParse(value, out var @int);
            result = @int;

            return parseResult;
        }
    }
}
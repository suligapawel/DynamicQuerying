namespace DynamicQuerying.Handlers
{
    internal class BooleanHandler : ObjectHandler
    {
        protected override bool TrySpecifyParse(string value, out object result)
        {
            var parseResult = bool.TryParse(value , out bool r);
            result = r;

            return parseResult;
        }
    }
}
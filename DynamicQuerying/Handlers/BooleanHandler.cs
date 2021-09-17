namespace DynamicQuerying.Handlers
{
    internal class BooleanHandler : ObjectHandler
    {
        public override object Parse(string value) => bool.Parse(value);

        public override bool TryParse(string value, out object result)
        {
            var parseResult = bool.TryParse(value, out var @bool);
            result = @bool;

            return parseResult;
        }
    }
}
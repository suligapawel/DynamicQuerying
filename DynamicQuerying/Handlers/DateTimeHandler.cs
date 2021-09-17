using System;

namespace DynamicQuerying.Handlers
{
    internal class DateTimeHandler : ObjectHandler
    {
        public override object Parse(string value) => DateTime.Parse(value);
        
        public override bool TryParse(string value, out object result)
        {
            var parseResult = DateTime.TryParse(value, out var dateTime);
            result = dateTime;

            return parseResult;
        }
    }
}
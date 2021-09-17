using System;

namespace DynamicQuerying.Handlers
{
    internal class DateTimeHandler : ObjectHandler
    {
        protected override bool TrySpecifyParse(string value, out object result)
        {
            var parseResult = DateTime.TryParse(value, out var dateTime);
            result = dateTime;

            return parseResult;
        }
    }
}
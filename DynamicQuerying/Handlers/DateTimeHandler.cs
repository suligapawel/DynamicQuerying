using System;
using DynamicQuerying.Extensions;

namespace DynamicQuerying.Handlers
{
    internal class DateTimeHandler : ObjectHandler
    {
        public override bool TryParse(object value, out object result)
        {
            var parseResult = DateTime.TryParse(value.AsString(), out var dateTime);
            result = dateTime;

            return parseResult;
        }
    }
}
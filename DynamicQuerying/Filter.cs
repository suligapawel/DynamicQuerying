using System;
using System.Collections.Generic;

namespace DynamicQuerying
{
    public class Filter
    {
        public string Field { get; init; }
        public string Value { get; init; }
        public string UpperValue => Value.ToUpper();
    }

    public enum Operator
    {
        And,
        Or
    }
}
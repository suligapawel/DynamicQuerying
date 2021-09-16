using System.Collections.Generic;
using DynamicQuerying.Dictionaries;

namespace DynamicQuerying
{
    public class Filter
    {
        public string Field { get; init; }
        public IReadOnlyCollection<string> Values { get; init; }
        public ComparisonType ComparisonType { get; init; } = ComparisonType.Equal;
    }
}
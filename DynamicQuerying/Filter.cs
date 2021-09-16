using System.Collections.Generic;

namespace DynamicQuerying
{
    public class Filter
    {
        public List<Param> Type { get; set; }


        public class Param
        {
            public string Field { get; set; }
            public List<string> Values { get; set; }
            public Operator Operator { get; set; }
        }
    }

    public enum Operator
    {
        And,
        Or
    }
}
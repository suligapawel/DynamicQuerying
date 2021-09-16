using System;

namespace DynamicQuerying.Tests.DAL.Entities
{
    internal class Order
    {
        public Guid Id { get; set; }
        public int PositionCounter { get; set; }
    }
}
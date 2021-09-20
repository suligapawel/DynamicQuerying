using System;

namespace DynamicQuerying.Tests.DAL.Entities
{
    internal class Order
    {
        public bool IsActive { get; set; }
        public DateTime Date { get; set; }
        public decimal Coast { get; set; }
        public double Tax { get; set; }
        public Guid Id { get; set; }
        public int PositionCounter { get; set; }
        public string Description { get; set; }
        public State State { get; set; }
    }

    public enum State
    {
        New,
        InProgress,
        Close
    }
}
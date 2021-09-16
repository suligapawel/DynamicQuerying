using System.Collections.Generic;
using DynamicQuerying.Tests.DAL;
using DynamicQuerying.Tests.DAL.Entities;
using DynamicQuerying.Tests.Fakes;

namespace DynamicQuerying.Tests.Factories
{
    internal static class OrdersFactory
    {
        public static void AddOrdersCollection(InMemoryDbContext dbContext)
        {
            var orders = new List<Order>
            {
                new() {Id = Guids.First(), PositionCounter = 1},
                new() {Id = Guids.Second(), PositionCounter = 3},
                new() {Id = Guids.Third(), PositionCounter = 1},
                new() {Id = Guids.Fourth(), PositionCounter = 2},
                new() {Id = Guids.Fifth(), PositionCounter = 5},
            };
            
            dbContext.Orders.AddRange(orders);
            dbContext.SaveChanges();
        }
    }
}
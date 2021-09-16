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
                new() {Id = Guids.First()}
            };
            
            dbContext.Orders.AddRange(orders);
            dbContext.SaveChanges();
        }
    }
}
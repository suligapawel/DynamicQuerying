using System;
using System.Collections.Generic;
using DynamicQuerying.Tests.DAL;
using DynamicQuerying.Tests.DAL.Entities;
using DynamicQuerying.Tests.Fakes;

namespace DynamicQuerying.Tests.Factories
{
    internal static class OrdersFactory
    {
        public static DateTime AnyDate => new DateTime(2021, 01, 01);

        public static void AddOrdersCollection(InMemoryDbContext dbContext)
        {
            var orders = new List<Order>
            {
                new()
                {
                    Id = Guids.First(), PositionCounter = 1, Coast = 22.6M, Date = AnyDate.AddDays(-5),
                    Description = Guids.First().ToString(), Tax = 0.23d, IsActive = true, State = State.New
                },
                new()
                {
                    Id = Guids.Second(), PositionCounter = 3, Coast = 2.3M, Date = AnyDate.AddDays(5),
                    Description = Guids.Second().ToString(), Tax = 0.5d, IsActive = false, State = State.New
                },
                new()
                {
                    Id = Guids.Third(), PositionCounter = 1, Coast = 122.3M, Date = AnyDate.AddDays(-1),
                    Description = "Equal", Tax = 0.7d, IsActive = true, State = State.InProgress
                },
                new()
                {
                    Id = Guids.Fourth(), PositionCounter = 2, Coast = 100.9M, Date = AnyDate.AddDays(1),
                    Description = "Description_for_test_start_with", Tax = 0.01d, IsActive = true, State = State.Close
                },
                new()
                {
                    Id = Guids.Fifth(), PositionCounter = 99, Coast = 22.3M, Date = AnyDate,
                    Description = "For_test_contains", Tax = 0.08d, IsActive = true, State = State.Close
                }
            };

            dbContext.Orders.AddRange(orders);
            dbContext.SaveChanges();
        }
    }
}
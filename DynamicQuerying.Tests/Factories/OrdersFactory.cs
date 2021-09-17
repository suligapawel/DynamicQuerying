using System;
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
            var date = new DateTime(2021, 01, 01);
            var orders = new List<Order>
            {
                new()
                {
                    Id = Guids.First(), PositionCounter = 1, Coast = 22.3M, Date = date,
                    Description = Guids.First().ToString(), Tax = 0.23d, IsActive = true
                },
                new()
                {
                    Id = Guids.Second(), PositionCounter = 3, Coast = 22.3M, Date = date,
                    Description = Guids.Second().ToString(), Tax = 0.23d, IsActive = false
                },
                new()
                {
                    Id = Guids.Third(), PositionCounter = 1, Coast = 22.3M, Date = date.AddDays(-1),
                    Description = Guids.Third().ToString(), Tax = 0.23d, IsActive = true
                },
                new()
                {
                    Id = Guids.Fourth(), PositionCounter = 2, Coast = 100.9M, Date = date,
                    Description = "Description_for_test_start_with", Tax = 0.23d, IsActive = true
                },
                new()
                {
                    Id = Guids.Fifth(), PositionCounter = 99, Coast = 22.3M, Date = date,
                    Description = Guids.Fifth().ToString(), Tax = 0.08d, IsActive = true
                }
            };

            dbContext.Orders.AddRange(orders);
            dbContext.SaveChanges();
        }
    }
}
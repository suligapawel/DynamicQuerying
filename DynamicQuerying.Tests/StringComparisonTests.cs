using System.Collections.Generic;
using System.Threading.Tasks;
using DynamicQuerying.Dictionaries;
using DynamicQuerying.Extensions;
using DynamicQuerying.Tests.DAL;
using DynamicQuerying.Tests.Factories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace DynamicQuerying.Tests
{
    public class ComparisonTests
    {
        private InMemoryDbContext _dbContext;

        [SetUp]
        public void Setup()
        {
            _dbContext = InMemoryDbContextFactory.Init();
            OrdersFactory.AddOrdersCollection(_dbContext);
        }

        [Test]
        public async Task Should_return_object_with_description_start_with()
        {
            Filter startWithFilter = new()
            {
                Field = "Description",
                Values = new List<object> {"Desc"},
                ComparisonType = ComparisonType.StartWith
            };

            var result = await _dbContext.Orders.Where(startWithFilter).FirstAsync();

            result.Description.Should().Be("Description_for_test_start_with");
        }
    }
}
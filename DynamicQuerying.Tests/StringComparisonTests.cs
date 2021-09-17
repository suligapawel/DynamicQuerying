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

        [TestCase("Desc")]
        [TestCase("dESC")]
        public async Task Should_return_object_with_description_when_description_start_with_value(string value)
        {
            Filter startWithFilter = new()
            {
                Field = "Description",
                Values = new List<object> {value},
                ComparisonType = ComparisonType.StartsWith
            };

            var result = await _dbContext.Orders.Where(startWithFilter).FirstAsync();

            result.Description.Should().Be("Description_for_test_start_with");
        }
        
        [TestCase("contains")]
        [TestCase("CoNtAiNs")]
        public async Task Should_return_object_with_description_when_description_contains_value(string value)
        {
            Filter startWithFilter = new()
            {
                Field = "Description",
                Values = new List<object> {value},
                ComparisonType = ComparisonType.Contains
            };

            var result = await _dbContext.Orders.Where(startWithFilter).FirstAsync();

            result.Description.Should().Be("For_test_contains");
        }
    }
}
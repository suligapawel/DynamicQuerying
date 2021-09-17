using System.Collections.Generic;
using System.Threading.Tasks;
using DynamicQuerying.Dictionaries;
using DynamicQuerying.Extensions;
using DynamicQuerying.Tests.DAL;
using DynamicQuerying.Tests.Factories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace DynamicQuerying.Tests.Tests.Comparison
{
    public class BooleanComparisonTests
    {
        private InMemoryDbContext _dbContext;

        [SetUp]
        public void Setup()
        {
            _dbContext = InMemoryDbContextFactory.Init();
            OrdersFactory.AddOrdersCollection(_dbContext);
        }

        [Test]
        public async Task Should_return_objects_with_isActive_true_when_isActive_is_true()
        {
            Filter equalFilter = new()
            {
                Field = "IsActive",
                Values = new List<object> {true},
                ComparisonType = ComparisonType.Equal
            };

            var result = await _dbContext.Orders.Where(equalFilter).ToListAsync();

            result.Should().OnlyContain(x => x.IsActive);
        }

        [Test]
        public async Task Should_return_objects_with_isActive_false_when_isActive_is_true()
        {
            Filter notEqualFilter = new()
            {
                Field = "IsActive",
                Values = new List<object> {true},
                ComparisonType = ComparisonType.NotEqual
            };

            var result = await _dbContext.Orders.Where(notEqualFilter).ToListAsync();

            result.Should().OnlyContain(x => !x.IsActive);
        }


        [TestCase(ComparisonType.Contains)]
        [TestCase(ComparisonType.StartsWith)]
        [TestCase(ComparisonType.GreaterThan)]
        [TestCase(ComparisonType.GreaterOrEqual)]
        [TestCase(ComparisonType.LessThan)]
        [TestCase(ComparisonType.LessOrEqual)]
        public async Task Should_return_nothing_when_comparisonType_is_not_implemented(ComparisonType comparisonType)
        {
            Filter notImplementedFilter = new()
            {
                Field = "IsActive",
                Values = new List<object> {true},
                ComparisonType = comparisonType
            };

            var result = await _dbContext.Orders.Where(notImplementedFilter).CountAsync();

            result.Should().Be(0);
        }
    }
}
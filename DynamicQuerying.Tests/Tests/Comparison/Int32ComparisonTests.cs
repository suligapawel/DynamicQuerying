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
    public class Int32ComparisonTests
    {
        private InMemoryDbContext _dbContext;

        [SetUp]
        public void Setup()
        {
            _dbContext = InMemoryDbContextFactory.Init();
            OrdersFactory.AddOrdersCollection(_dbContext);
        }

        [Test]
        public async Task Should_return_objects_with_same_positionCounter()
        {
            Filter equalFilter = new()
            {
                Field = "PositionCounter",
                Values = new List<object> {AnyPositionCounter()},
                ComparisonType = ComparisonType.Equal
            };

            var result = await _dbContext.Orders.Where(equalFilter).ToListAsync();

            result.Should().OnlyContain(x => x.PositionCounter == AnyPositionCounter());
        }

        [Test]
        public async Task Should_return_objects_with_other_positionCounter()
        {
            Filter notEqualFilter = new()
            {
                Field = "PositionCounter",
                Values = new List<object> {AnyPositionCounter()},
                ComparisonType = ComparisonType.NotEqual
            };

            var result = await _dbContext.Orders.Where(notEqualFilter).ToListAsync();

            result.Should().OnlyContain(x => x.PositionCounter != AnyPositionCounter());
        }

        [Test]
        public async Task Should_return_objects_with_positionCounters_greater_than_positionCounter()
        {
            Filter greaterFilter = new()
            {
                Field = "PositionCounter",
                Values = new List<object> {AnyPositionCounter()},
                ComparisonType = ComparisonType.GreaterThan
            };

            var result = await _dbContext.Orders.Where(greaterFilter).ToListAsync();

            result.Should().OnlyContain(x => x.PositionCounter > AnyPositionCounter());
            result.Should().NotContain(x => x.PositionCounter <= AnyPositionCounter());
        }

        [Test]
        public async Task Should_return_objects_with_positionCounters_greater_than_or_equal_positionCounter()
        {
            Filter greaterOrEqualFilter = new()
            {
                Field = "PositionCounter",
                Values = new List<object> {AnyPositionCounter()},
                ComparisonType = ComparisonType.GreaterOrEqual
            };

            var result = await _dbContext.Orders.Where(greaterOrEqualFilter).ToListAsync();

            result.Should().OnlyContain(x => x.PositionCounter >= AnyPositionCounter());
            result.Should().NotContain(x => x.PositionCounter < AnyPositionCounter());
        }

        [Test]
        public async Task Should_return_objects_with_positionCounters_less_than_positionCounter()
        {
            Filter lessFilter = new()
            {
                Field = "PositionCounter",
                Values = new List<object> {AnyPositionCounter()},
                ComparisonType = ComparisonType.LessThan
            };

            var result = await _dbContext.Orders.Where(lessFilter).ToListAsync();

            result.Should().OnlyContain(x => x.PositionCounter < AnyPositionCounter());
            result.Should().NotContain(x => x.PositionCounter >= AnyPositionCounter());
        }

        [Test]
        public async Task Should_return_objects_with_positionCounters_less_than_or_equal_positionCounter()
        {
            Filter lessOrEqualFilter = new()
            {
                Field = "PositionCounter",
                Values = new List<object> {AnyPositionCounter()},
                ComparisonType = ComparisonType.LessOrEqual
            };

            var result = await _dbContext.Orders.Where(lessOrEqualFilter).ToListAsync();

            result.Should().OnlyContain(x => x.PositionCounter <= AnyPositionCounter());
            result.Should().NotContain(x => x.PositionCounter > AnyPositionCounter());
        }

        [TestCase(ComparisonType.Contains)]
        [TestCase(ComparisonType.StartsWith)]
        public async Task Should_return_nothing_when_comparisonType_is_not_implemented(ComparisonType comparisonType)
        {
            Filter notImplementedFilter = new()
            {
                Field = "PositionCounter",
                Values = new List<object> {AnyPositionCounter()},
                ComparisonType = comparisonType
            };

            var result = await _dbContext.Orders.Where(notImplementedFilter).CountAsync();

            result.Should().Be(0);
        }

        private static int AnyPositionCounter() => 2;
    }
}
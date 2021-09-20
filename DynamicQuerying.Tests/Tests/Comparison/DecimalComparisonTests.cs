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
    public class DecimalComparisonTests
    {
        private InMemoryDbContext _dbContext;

        [SetUp]
        public void Setup()
        {
            _dbContext = InMemoryDbContextFactory.Init();
            OrdersFactory.AddOrdersCollection(_dbContext);
        }

        [Test]
        public async Task Should_return_objects_with_same_coast()
        {
            Filter equalFilter = new()
            {
                Field = "Coast",
                Values = new List<object> {AnyCoast()},
                ComparisonType = ComparisonType.Equal
            };

            var result = await _dbContext.Orders.Where(equalFilter).ToListAsync();

            result.Should().OnlyContain(x => x.Coast == AnyCoast());
        }

        [Test]
        public async Task Should_return_objects_with_other_coast()
        {
            Filter notEqualFilter = new()
            {
                Field = "Coast",
                Values = new List<object> {AnyCoast()},
                ComparisonType = ComparisonType.NotEqual
            };

            var result = await _dbContext.Orders.Where(notEqualFilter).ToListAsync();

            result.Should().OnlyContain(x => x.Coast != AnyCoast());
        }

        [Test]
        public async Task Should_return_objects_with_coasts_greater_than_coast()
        {
            Filter greaterFilter = new()
            {
                Field = "Coast",
                Values = new List<object> {AnyCoast()},
                ComparisonType = ComparisonType.GreaterThan
            };

            var result = await _dbContext.Orders.Where(greaterFilter).ToListAsync();

            result.Should().OnlyContain(x => x.Coast > AnyCoast());
            result.Should().NotContain(x => x.Coast <= AnyCoast());
        }

        [Test]
        public async Task Should_return_objects_with_coasts_greater_than_or_equal_coast()
        {
            Filter greaterOrEqualFilter = new()
            {
                Field = "Coast",
                Values = new List<object> {AnyCoast()},
                ComparisonType = ComparisonType.GreaterOrEqual
            };

            var result = await _dbContext.Orders.Where(greaterOrEqualFilter).ToListAsync();

            result.Should().OnlyContain(x => x.Coast >= AnyCoast());
            result.Should().NotContain(x => x.Coast < AnyCoast());
        }

        [Test]
        public async Task Should_return_objects_with_coasts_less_than_coast()
        {
            Filter lessFilter = new()
            {
                Field = "Coast",
                Values = new List<object> {AnyCoast()},
                ComparisonType = ComparisonType.LessThan
            };

            var result = await _dbContext.Orders.Where(lessFilter).ToListAsync();

            result.Should().OnlyContain(x => x.Coast < AnyCoast());
            result.Should().NotContain(x => x.Coast >= AnyCoast());
        }

        [Test]
        public async Task Should_return_objects_with_coasts_less_than_or_equal_coast()
        {
            Filter lessOrEqualFilter = new()
            {
                Field = "Coast",
                Values = new List<object> {AnyCoast()},
                ComparisonType = ComparisonType.LessOrEqual
            };

            var result = await _dbContext.Orders.Where(lessOrEqualFilter).ToListAsync();

            result.Should().OnlyContain(x => x.Coast <= AnyCoast());
            result.Should().NotContain(x => x.Coast > AnyCoast());
        }

        [TestCase(ComparisonType.Contains)]
        [TestCase(ComparisonType.StartsWith)]
        public async Task Should_return_nothing_when_comparisonType_is_not_implemented(ComparisonType comparisonType)
        {
            var coast = 22.3M;
            Filter notImplementedFilter = new()
            {
                Field = "Coast",
                Values = new List<object> {coast},
                ComparisonType = comparisonType
            };

            var result = await _dbContext.Orders.Where(notImplementedFilter).CountAsync();

            result.Should().Be(0);
        }

        private static decimal AnyCoast() => 22.3M;
    }
}
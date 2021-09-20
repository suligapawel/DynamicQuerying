using System.Collections.Generic;
using System.Threading.Tasks;
using DynamicQuerying.Dictionaries;
using DynamicQuerying.Extensions;
using DynamicQuerying.Tests.DAL;
using DynamicQuerying.Tests.DAL.Entities;
using DynamicQuerying.Tests.Factories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace DynamicQuerying.Tests.Tests.Comparison
{
    public class EnumComparisonTests
    {
        private InMemoryDbContext _dbContext;

        [SetUp]
        public void Setup()
        {
            _dbContext = InMemoryDbContextFactory.Init();
            OrdersFactory.AddOrdersCollection(_dbContext);
        }

        [Test]
        public async Task Should_return_objects_with_same_state()
        {
            Filter equalFilter = new()
            {
                Field = "State",
                Values = new List<object> {AnyState()},
                ComparisonType = ComparisonType.Equal
            };

            var result = await _dbContext.Orders.Where(equalFilter).ToListAsync();

            result.Should().OnlyContain(x => x.State == AnyState());
        }

        [Test]
        public async Task Should_return_objects_with_other_state()
        {
            Filter notEqualFilter = new()
            {
                Field = "State",
                Values = new List<object> {AnyState()},
                ComparisonType = ComparisonType.NotEqual
            };

            var result = await _dbContext.Orders.Where(notEqualFilter).ToListAsync();

            result.Should().OnlyContain(x => x.State != AnyState());
        }

        [Test]
        public async Task Should_return_objects_with_states_greater_than_state()
        {
            Filter greaterFilter = new()
            {
                Field = "State",
                Values = new List<object> {AnyState()},
                ComparisonType = ComparisonType.GreaterThan
            };

            var result = await _dbContext.Orders.Where(greaterFilter).ToListAsync();

            result.Should().OnlyContain(x => x.State > AnyState());
            result.Should().NotContain(x => x.State <= AnyState());
        }

        [Test]
        public async Task Should_return_objects_with_states_greater_than_or_equal_state()
        {
            Filter greaterOrEqualFilter = new()
            {
                Field = "State",
                Values = new List<object> {AnyState()},
                ComparisonType = ComparisonType.GreaterOrEqual
            };

            var result = await _dbContext.Orders.Where(greaterOrEqualFilter).ToListAsync();

            result.Should().OnlyContain(x => x.State >= AnyState());
            result.Should().NotContain(x => x.State < AnyState());
        }

        [Test]
        public async Task Should_return_objects_with_states_less_than_state()
        {
            Filter lessFilter = new()
            {
                Field = "State",
                Values = new List<object> {AnyState()},
                ComparisonType = ComparisonType.LessThan
            };

            var result = await _dbContext.Orders.Where(lessFilter).ToListAsync();

            result.Should().OnlyContain(x => x.State < AnyState());
            result.Should().NotContain(x => x.State >= AnyState());
        }

        [Test]
        public async Task Should_return_objects_with_states_less_than_or_equal_state()
        {
            Filter lessOrEqualFilter = new()
            {
                Field = "State",
                Values = new List<object> {AnyState()},
                ComparisonType = ComparisonType.LessOrEqual
            };

            var result = await _dbContext.Orders.Where(lessOrEqualFilter).ToListAsync();

            result.Should().OnlyContain(x => x.State <= AnyState());
            result.Should().NotContain(x => x.State > AnyState());
        }

        [TestCase(ComparisonType.Contains)]
        [TestCase(ComparisonType.StartsWith)]
        public async Task Should_return_nothing_when_comparisonType_is_not_implemented(ComparisonType comparisonType)
        {
            Filter notImplementedFilter = new()
            {
                Field = "State",
                Values = new List<object> {AnyState()},
                ComparisonType = comparisonType
            };

            var result = await _dbContext.Orders.Where(notImplementedFilter).CountAsync();

            result.Should().Be(0);
        }

        private static State AnyState() => State.InProgress;
    }
}
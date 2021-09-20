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
    public class DoubleComparisonTests
    {
        private InMemoryDbContext _dbContext;

        [SetUp]
        public void Setup()
        {
            _dbContext = InMemoryDbContextFactory.Init();
            OrdersFactory.AddOrdersCollection(_dbContext);
        }

        [Test]
        public async Task Should_return_objects_with_same_tax()
        {
            Filter equalFilter = new()
            {
                Field = "Tax",
                Values = new List<object> {AnyTax()},
                ComparisonType = ComparisonType.Equal
            };

            var result = await _dbContext.Orders.Where(equalFilter).ToListAsync();

            result.Should().OnlyContain(x => x.Tax == AnyTax());
        }

        [Test]
        public async Task Should_return_objects_with_other_tax()
        {
            Filter notEqualFilter = new()
            {
                Field = "Tax",
                Values = new List<object> {AnyTax()},
                ComparisonType = ComparisonType.NotEqual
            };

            var result = await _dbContext.Orders.Where(notEqualFilter).ToListAsync();

            result.Should().OnlyContain(x => x.Tax != AnyTax());
        }

        [Test]
        public async Task Should_return_objects_with_taxes_greater_than_tax()
        {
            Filter greaterFilter = new()
            {
                Field = "Tax",
                Values = new List<object> {AnyTax()},
                ComparisonType = ComparisonType.GreaterThan
            };

            var result = await _dbContext.Orders.Where(greaterFilter).ToListAsync();

            result.Should().OnlyContain(x => x.Tax > AnyTax());
            result.Should().NotContain(x => x.Tax <= AnyTax());
        }

        [Test]
        public async Task Should_return_objects_with_taxes_greater_than_or_equal_tax()
        {
            Filter greaterOrEqualFilter = new()
            {
                Field = "Tax",
                Values = new List<object> {AnyTax()},
                ComparisonType = ComparisonType.GreaterOrEqual
            };

            var result = await _dbContext.Orders.Where(greaterOrEqualFilter).ToListAsync();

            result.Should().OnlyContain(x => x.Tax >= AnyTax());
            result.Should().NotContain(x => x.Tax < AnyTax());
        }

        [Test]
        public async Task Should_return_objects_with_taxes_less_than_tax()
        {
            Filter lessFilter = new()
            {
                Field = "Tax",
                Values = new List<object> {AnyTax()},
                ComparisonType = ComparisonType.LessThan
            };

            var result = await _dbContext.Orders.Where(lessFilter).ToListAsync();

            result.Should().OnlyContain(x => x.Tax < AnyTax());
            result.Should().NotContain(x => x.Tax >= AnyTax());
        }

        [Test]
        public async Task Should_return_objects_with_taxes_less_than_or_equal_tax()
        {
            Filter lessOrEqualFilter = new()
            {
                Field = "Tax",
                Values = new List<object> {AnyTax()},
                ComparisonType = ComparisonType.LessOrEqual
            };

            var result = await _dbContext.Orders.Where(lessOrEqualFilter).ToListAsync();

            result.Should().OnlyContain(x => x.Tax <= AnyTax());
            result.Should().NotContain(x => x.Tax > AnyTax());
        }

        [TestCase(ComparisonType.Contains)]
        [TestCase(ComparisonType.StartsWith)]
        public async Task Should_return_nothing_when_comparisonType_is_not_implemented(ComparisonType comparisonType)
        {
            Filter notImplementedFilter = new()
            {
                Field = "Tax",
                Values = new List<object> {AnyTax()},
                ComparisonType = comparisonType
            };

            var result = await _dbContext.Orders.Where(notImplementedFilter).CountAsync();

            result.Should().Be(0);
        }

        private static double AnyTax() => 0.23d;
    }
}
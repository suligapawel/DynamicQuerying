using System;
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
    public class DateTimeComparisonTests
    {
        private InMemoryDbContext _dbContext;

        [SetUp]
        public void Setup()
        {
            _dbContext = InMemoryDbContextFactory.Init();
            OrdersFactory.AddOrdersCollection(_dbContext);
        }

        [Test]
        public async Task Should_return_objects_with_same_date()
        {
            var date = OrdersFactory.AnyDate.AddDays(-1);
            Filter equalFilter = new()
            {
                Field = "Date",
                Values = new List<object> {date},
                ComparisonType = ComparisonType.Equal
            };

            var result = await _dbContext.Orders.Where(equalFilter).ToListAsync();

            result.Should().OnlyContain(x => x.Date == date);
        }

        [Test]
        public async Task Should_return_objects_with_other_dates()
        {
            var date = OrdersFactory.AnyDate.AddDays(-1);
            Filter notEqualFilter = new()
            {
                Field = "Date",
                Values = new List<object> {date},
                ComparisonType = ComparisonType.NotEqual
            };

            var result = await _dbContext.Orders.Where(notEqualFilter).ToListAsync();

            result.Should().OnlyContain(x => x.Date != date);
        }

        [Test]
        public async Task Should_return_objects_with_dates_greater_than_date()
        {
            var date = OrdersFactory.AnyDate;
            Filter notEqualFilter = new()
            {
                Field = "Date",
                Values = new List<object> {date},
                ComparisonType = ComparisonType.GreaterThan
            };

            var result = await _dbContext.Orders.Where(notEqualFilter).ToListAsync();

            result.Should().OnlyContain(x => x.Date > date);
            result.Should().NotContain(x => x.Date <= date);
        }

        [Test]
        public async Task Should_return_objects_with_dates_greater_than_or_equal_date()
        {
            var date = OrdersFactory.AnyDate;
            Filter notEqualFilter = new()
            {
                Field = "Date",
                Values = new List<object> {date},
                ComparisonType = ComparisonType.GreaterOrEqual
            };

            var result = await _dbContext.Orders.Where(notEqualFilter).ToListAsync();

            result.Should().OnlyContain(x => x.Date >= date);
            result.Should().NotContain(x => x.Date < date);
        }

        [Test]
        public async Task Should_return_objects_with_dates_less_than_date()
        {
            var date = OrdersFactory.AnyDate;
            Filter notEqualFilter = new()
            {
                Field = "Date",
                Values = new List<object> {date},
                ComparisonType = ComparisonType.LessThan
            };

            var result = await _dbContext.Orders.Where(notEqualFilter).ToListAsync();

            result.Should().OnlyContain(x => x.Date < date);
            result.Should().NotContain(x => x.Date >= date);
        }

        [Test]
        public async Task Should_return_objects_with_dates_less_than_or_equal_date()
        {
            var date = OrdersFactory.AnyDate;
            Filter notEqualFilter = new()
            {
                Field = "Date",
                Values = new List<object> {date},
                ComparisonType = ComparisonType.LessOrEqual
            };

            var result = await _dbContext.Orders.Where(notEqualFilter).ToListAsync();

            result.Should().OnlyContain(x => x.Date <= date);
            result.Should().NotContain(x => x.Date > date);
        }

        [TestCase(ComparisonType.Contains)]
        [TestCase(ComparisonType.StartsWith)]
        public async Task Should_return_nothing_when_comparisonType_is_not_implemented(ComparisonType comparisonType)
        {
            var date = OrdersFactory.AnyDate.AddDays(-1);
            Filter notImplementedFilter = new()
            {
                Field = "Date",
                Values = new List<object> {date},
                ComparisonType = comparisonType
            };

            var result = await _dbContext.Orders.Where(notImplementedFilter).CountAsync();

            result.Should().Be(0);
        }
    }
}
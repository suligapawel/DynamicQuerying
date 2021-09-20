using System.Collections.Generic;
using System.Threading.Tasks;
using DynamicQuerying.Dictionaries;
using DynamicQuerying.Extensions;
using DynamicQuerying.Tests.DAL;
using DynamicQuerying.Tests.Factories;
using DynamicQuerying.Tests.Fakes;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace DynamicQuerying.Tests.Tests.Comparison
{
    public class GuidComparisonTests
    {
        private InMemoryDbContext _dbContext;

        [SetUp]
        public void Setup()
        {
            _dbContext = InMemoryDbContextFactory.Init();
            OrdersFactory.AddOrdersCollection(_dbContext);
        }

        [Test]
        public async Task Should_return_objects_with_same_guid()
        {
            var id = Guids.First();
            Filter equalFilter = new()
            {
                Field = "Id",
                Values = new List<object> {id},
                ComparisonType = ComparisonType.Equal
            };

            var result = await _dbContext.Orders.Where(equalFilter).ToListAsync();

            result.Should().OnlyContain(x => x.Id == id);
        }

        [Test]
        public async Task Should_return_objects_with_other_guid()
        {
            var id = Guids.First();
            Filter notEqualFilter = new()
            {
                Field = "Id",
                Values = new List<object> {id},
                ComparisonType = ComparisonType.NotEqual
            };

            var result = await _dbContext.Orders.Where(notEqualFilter).ToListAsync();

            result.Should().OnlyContain(x => x.Id != id);
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
                Field = "Id",
                Values = new List<object> {Guids.First()},
                ComparisonType = comparisonType
            };

            var result = await _dbContext.Orders.Where(notImplementedFilter).CountAsync();

            result.Should().Be(0);
        }
    }
}
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
    public class StringComparisonTests
    {
        private InMemoryDbContext _dbContext;

        [SetUp]
        public void Setup()
        {
            _dbContext = InMemoryDbContextFactory.Init();
            OrdersFactory.AddOrdersCollection(_dbContext);
        }

        [TestCase("Equal")]
        [TestCase("eQuAl")]
        public async Task Should_return_object_with_description_when_description_equal_value(string value)
        {
            Filter equalFilter = new()
            {
                Field = "Description",
                Values = new List<object> {value},
                ComparisonType = ComparisonType.Equal
            };

            var result = await _dbContext.Orders.Where(equalFilter).FirstAsync();

            result.Description.Should().Be("Equal");
        }

        [TestCase("Equal")]
        [TestCase("eQuAl")]
        public async Task Should_return_objects_with_other_description_when_description_no_equal_value(string value)
        {
            Filter notEqualFilter = new()
            {
                Field = "Description",
                Values = new List<object> {value},
                ComparisonType = ComparisonType.NotEqual
            };

            var result = await _dbContext.Orders.Where(notEqualFilter).ToListAsync();

            result.Should().NotContain(x => x.Description == "Equal");
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
            Filter containsFilter = new()
            {
                Field = "Description",
                Values = new List<object> {value},
                ComparisonType = ComparisonType.Contains
            };

            var result = await _dbContext.Orders.Where(containsFilter).FirstAsync();

            result.Description.Should().Be("For_test_contains");
        }

        [TestCase(ComparisonType.Between)]
        [TestCase(ComparisonType.GreaterThan)]
        [TestCase(ComparisonType.GreaterOrEqual)]
        [TestCase(ComparisonType.LessThan)]
        [TestCase(ComparisonType.LessOrEqual)]
        public async Task Should_return_nothing_when_comparisonType_is_not_implemented(ComparisonType comparisonType)
        {
            Filter notImplementedFilter = new()
            {
                Field = "Description",
                Values = new List<object> {"value"},
                ComparisonType = comparisonType
            };

            var result = await _dbContext.Orders.Where(notImplementedFilter).CountAsync();

            result.Should().Be(0);
        }
    }
}
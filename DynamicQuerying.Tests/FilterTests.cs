using System.Collections.Generic;
using System.Threading.Tasks;
using DynamicQuerying.Extensions;
using DynamicQuerying.Tests.DAL;
using DynamicQuerying.Tests.Factories;
using DynamicQuerying.Tests.Fakes;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace DynamicQuerying.Tests
{
    public class Tests
    {
        private InMemoryDbContext _dbContext;

        [SetUp]
        public void Setup()
        {
            _dbContext = InMemoryDbContextFactory.Init();
            OrdersFactory.AddOrdersCollection(_dbContext);
        }

        [Test]
        public async Task Should_get_all_objects_when_filter_is_null()
        {
            Filter nullFilter = null;
            int expected = await _dbContext.Orders.CountAsync();

            int result = await _dbContext.Orders.Where(nullFilter).CountAsync();

            result.Should().Be(expected);
        }

        [Test]
        public async Task Should_get_only_one_object_when_only_one_object_match()
        {
            Filter oneMatchFilter = new()
            {
                Field = "Id",
                Values = new List<object> {Guids.First()}
            };

            int result = await _dbContext.Orders.Where(oneMatchFilter).CountAsync();

            result.Should().Be(1);
        }

        [Test]
        public async Task Should_get_more_than_one_objects_when_multiple_objects_match()
        {
            Filter multipleMatchFilter = new()
            {
                Field = "PositionCounter",
                Values = new List<object> {1, 2}
            };

            int result = await _dbContext.Orders.Where(multipleMatchFilter).CountAsync();

            result.Should().Be(3);
        }

        [Test]
        public async Task Should_skip_field_when_entity_has_no_field()
        {
            Filter fieldNotExists = new() {Field = "No_field"};
            int expected = await _dbContext.Orders.CountAsync();

            var result = await _dbContext.Orders.Where(fieldNotExists).CountAsync();

            result.Should().Be(expected);
        }
        
        [Test]
        public async Task Should_try_convert_values_type_when_types_are_not_equal()
        {
            Filter notEqualFieldTypes = new()
            {
                Field = "Id",
                Values = new List<object> {"53b2a5ee-46d2-46b9-ac46-2489bd38c461"}
            };
            
            var result = await _dbContext.Orders.Where(notEqualFieldTypes).CountAsync();

            result.Should().Be(1);
        }
    }
}
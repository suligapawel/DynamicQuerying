using System;
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
                Value = Guids.First().ToString()
            };

            int result = await _dbContext.Orders.Where(oneMatchFilter).CountAsync();

            result.Should().Be(1);
        }

        [Test]
        public async Task Should_get_more_than_one_objects_when_multiple_objects_match()
        {
            Filter oneMatchFilter = new()
            {
                Field = "PositionCounter",
                Value = 1.ToString()
            };

            int result = await _dbContext.Orders.Where(oneMatchFilter).CountAsync();

            result.Should().Be(2);
        }

        [Test]
        public async Task Should_skip_field_when_entity_has_no_field()
        {
            Filter oneMatchFilter = new() {Field = "No_field"};
            int expected = await _dbContext.Orders.CountAsync();

            var result = await _dbContext.Orders.Where(oneMatchFilter).CountAsync();

            result.Should().Be(expected);
        }
    }
}
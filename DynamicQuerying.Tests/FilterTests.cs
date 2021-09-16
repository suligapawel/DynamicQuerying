using System.Linq;
using System.Threading.Tasks;
using DynamicQuerying.Extensions;
using DynamicQuerying.Tests.DAL;
using DynamicQuerying.Tests.Factories;
using NUnit.Framework;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

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
    }
}
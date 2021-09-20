using System;
using DynamicQuerying.Tests.DAL;
using Microsoft.EntityFrameworkCore;

namespace DynamicQuerying.Tests.Factories
{
    internal static class InMemoryDbContextFactory
    {
        private const string CONNECTION_STRING_NAME = "InMemory";

        public static InMemoryDbContext Init()
        {
            var dbContext = Create();

            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            return dbContext;
        }

        private static InMemoryDbContext Create()
        {
            var optionsBuilder = new DbContextOptionsBuilder<InMemoryDbContext>();
            var options = optionsBuilder.UseInMemoryDatabase(CONNECTION_STRING_NAME);

            return new InMemoryDbContext(options.Options);
        }
    }
}
using DynamicQuerying.Tests.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DynamicQuerying.Tests.DAL
{
    internal class InMemoryDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }

        public InMemoryDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
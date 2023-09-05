using Carrefour.Management.Repository.Entities;
using Carrefour.Management.Repository.Map;
using Microsoft.EntityFrameworkCore;

namespace Carrefour.Management.Repository.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OrderMap());
            modelBuilder.ApplyConfiguration(new OrderTypeMap());
        }
    }
}

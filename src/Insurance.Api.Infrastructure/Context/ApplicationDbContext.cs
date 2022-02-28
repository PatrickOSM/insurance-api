using Insurance.Api.Domain.Entities;
using Insurance.Api.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Insurance.Api.Infrastructure.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<InsurancePolicy> InsurancePolicies { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}

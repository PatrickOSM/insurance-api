using Insurance.Api.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Insurance.Api.UnitTests
{
    public class InsurancePolicyRepositoryTests
    {
        private ApplicationDbContext CreateDbContext(string name)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(name)
            .Options;
            return new ApplicationDbContext(options);
        }
    }
}

using Insurance.Api.Domain.Entities;
using Insurance.Api.Domain.Repositories;
using Insurance.Api.Infrastructure.Context;

namespace Insurance.Api.Infrastructure.Repositories
{
    public class InsurancePolicyRepository : Repository<InsurancePolicy>, IInsurancePolicyRepository
    {
        public InsurancePolicyRepository(ApplicationDbContext dbContext) : base(dbContext) { }
    }
}

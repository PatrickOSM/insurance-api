using Insurance.Api.Domain.Entities;
using Insurance.Api.Domain.Repositories;
using Insurance.Api.Infrastructure.Context;

namespace Insurance.Api.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext) { }
    }
}

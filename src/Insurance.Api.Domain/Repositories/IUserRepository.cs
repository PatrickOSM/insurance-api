using System.Threading.Tasks;
using Insurance.Api.Domain.Core.Interfaces;
using Insurance.Api.Domain.Entities;

namespace Insurance.Api.Domain.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
    }
}

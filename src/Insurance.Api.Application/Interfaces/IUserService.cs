using Insurance.Api.Domain.Entities;
using System;
using System.Threading.Tasks;


namespace Insurance.Api.Application.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<User> Authenticate(string email, string password);
    }
}

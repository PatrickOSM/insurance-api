using Insurance.Api.Application.DTOs.Auth;
using Insurance.Api.Domain.Entities;

namespace Insurance.Api.Application.Interfaces
{
    public interface IAuthService
    {
        JwtDto GenerateToken(User user);
    }
}

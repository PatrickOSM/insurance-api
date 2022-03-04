using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;
using ISession = Insurance.Api.Domain.Auth.Interfaces.ISession;

namespace Insurance.Api.Application.Auth
{
    public class Session : ISession
    {
        public Guid UserId { get; private set; }

        public DateTime Now => DateTime.Now;

        public Session(IHttpContextAccessor httpContextAccessor)
        {
            ClaimsPrincipal user = httpContextAccessor.HttpContext?.User;

            Claim nameIdentifier = user?.FindFirst(ClaimTypes.NameIdentifier);

            if (nameIdentifier != null)
            {
                UserId = new Guid(nameIdentifier.Value);
            }
        }

    }
}

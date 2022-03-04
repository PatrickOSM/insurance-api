using System;

namespace Insurance.Api.Domain.Auth.Interfaces
{
    public interface ISession
    {
        public Guid UserId { get; }

        public DateTime Now { get; }
    }
}

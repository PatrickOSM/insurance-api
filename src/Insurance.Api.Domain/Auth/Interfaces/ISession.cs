using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Api.Domain.Auth.Interfaces
{
    public interface ISession
    {
        public Guid UserId { get; }

        public DateTime Now { get; }
    }
}

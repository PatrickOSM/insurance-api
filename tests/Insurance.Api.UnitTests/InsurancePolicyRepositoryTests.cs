using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using Insurance.Api.Domain.Entities;
using Insurance.Api.Infrastructure.Context;
using Insurance.Api.Infrastructure.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

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

using Bogus;
using FluentAssertions;
using Insurance.Api.Domain.Entities;
using Insurance.Api.Infrastructure.Context;
using Insurance.Api.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
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

        [Theory]
        [InlineData("4e1a20db-0533-4838-bd97-87d2afc89832")]
        [InlineData("ff57101b-d9c6-4b8a-959e-2d64c7ae8967")]
        [InlineData("2c0176d6-47d6-4ce1-b5e8-bed9a52b9e64")]
        [InlineData("bf15a502-37db-4d4c-ba4c-e231fb5487e6")]
        [InlineData("e141a755-98d4-44d3-a84f-528e6e75f543")]
        public async Task GetById_existing_insurance_policies(Guid id)
        {
            var faker = new Faker<InsurancePolicy>()
                .RuleFor(x => x.Id, f => id)
                .RuleFor(x => x.FirstName, f => f.Name.FirstName())
                .RuleFor(x => x.LastName, f => f.Name.LastName())
                .RuleFor(x => x.DriversLicence, f => f.Random.AlphaNumeric(20))
                .RuleFor(x => x.VehicleName, f => f.Vehicle.Model())
                .RuleFor(x => x.VehicleModel, f => f.Vehicle.Model())
                .RuleFor(x => x.VehicleManufacturer, f => f.Vehicle.Manufacturer())
                .RuleFor(x => x.VehicleYear, f => Convert.ToInt16(f.Date.PastDateOnly(30).Year))
                .RuleFor(x => x.Street, f => f.Address.StreetAddress())
                .RuleFor(x => x.City, f => f.Address.City())
                .RuleFor(x => x.State, f => f.Address.State())
                .RuleFor(x => x.ZipCode, f => f.Address.ZipCode())
                .RuleFor(x => x.EffectiveDate, f => f.Date.Soon(35))
                .RuleFor(x => x.ExpirationDate, f => f.Date.Future(1))
                .RuleFor(x => x.Premium, f => f.Random.Decimal(0, 2000));
            // Arrange

            using (var context = CreateDbContext("GetById_existing_insurance_policies"))
            {
                context.Set<InsurancePolicy>().Add(faker.Generate());
                await context.SaveChangesAsync();
            }
            InsurancePolicy insurancePolicy = null;

            // Act
            using (var context = CreateDbContext("GetById_existing_insurance_policies"))
            {
                var repository = new InsurancePolicyRepository(context);
                insurancePolicy = await repository.GetById(id);
            }
            // Assert
            insurancePolicy.Should().NotBeNull();
            insurancePolicy.Id.Should().Be(id);
        }

        [Fact]
        public void NullDbContext_Throws_ArgumentNullException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() =>
            {
                new InsurancePolicyRepository(null);
            });
            exception.Should().NotBeNull();
            exception.ParamName.Should().Be("dbContext");
        }
    }
}

using Bogus;
using FluentAssertions;
using Insurance.Api.Domain.Entities;
using Insurance.Api.Infrastructure.Context;
using Insurance.Api.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        #region GetAll_insurance_policies
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(5)]
        public async Task GetAll_insurance_policies(int count)
        {
            var faker = new Faker<InsurancePolicy>()
                .RuleFor(x => x.Id, f => Guid.NewGuid())
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
            using (var context = CreateDbContext($"GetAll_insurance_policies_{count}"))
            {
                for (var i = 0; i < count; i++) context.Set<InsurancePolicy>().Add(faker.Generate());
                await context.SaveChangesAsync();
            }
            List<InsurancePolicy> insurancePolicies = null;
            // Act
            using (var context = CreateDbContext($"GetAll_insurance_policies_{count}"))
            {
                var repository = new InsurancePolicyRepository(context);
                insurancePolicies = await repository.GetAll().ToListAsync();
            }
            // Assert
            insurancePolicies.Should().NotBeNull();
            insurancePolicies.Count.Should().Be(count);
        } 
        #endregion

        #region GetById_existing_insurance_policies
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
        #endregion

        #region GetById_inexistent_insurance_policies
        [Theory]
        [InlineData("4e1a20db-0533-4838-bd97-87d2afc89832")]
        [InlineData("ff57101b-d9c6-4b8a-959e-2d64c7ae8967")]
        [InlineData("2c0176d6-47d6-4ce1-b5e8-bed9a52b9e64")]
        [InlineData("bf15a502-37db-4d4c-ba4c-e231fb5487e6")]
        [InlineData("e141a755-98d4-44d3-a84f-528e6e75f543")]
        public async Task GetById_inexistent_insurance_policies(Guid id)
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
            using (var context = CreateDbContext("GetById_inexisting_insurance_policies"))
            {
                context.Set<InsurancePolicy>().Add(faker.Generate());
                await context.SaveChangesAsync();
            }
            InsurancePolicy insurancePolicy = null;

            // Act
            using (var context = CreateDbContext("GetById_inexisting_insurance_policies"))
            {
                var repository = new InsurancePolicyRepository(context);
                insurancePolicy = await repository.GetById(Guid.NewGuid());
            }
            // Assert
            insurancePolicy.Should().BeNull();
        } 
        #endregion

        #region Create_Insurance_Policy
        [Fact]
        public async Task Create_Insurance_Policy()
        {
            int result;

            var faker = new Faker<InsurancePolicy>()
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

            using (var context = CreateDbContext("Create_Insurance_Policy"))
            {
                var repository = new InsurancePolicyRepository(context);
                repository.Create(faker);
                result = await repository.SaveChangesAsync();
            }

            // Assert
            result.Should().BeGreaterThan(0);
            result.Should().Be(1);
            // Simulate access from another context to verifiy that correct data was saved to database
            using (var context = CreateDbContext("Create_Insurance_Policy"))
            {
                (await context.InsurancePolicies.CountAsync()).Should().Be(1);
                (await context.InsurancePolicies.FirstAsync().Result).Should().BeEquivalentTo(faker.);
            }
        }
        #endregion

        #region Update_Insurance_Policy
        [Fact]
        public async Task Update_Insurance_Policy()
        {
            Faker faker = new();
            int result;
            Guid? id;

            using (var context = CreateDbContext("Update_Insurance_Policy"))
            {
                var createdInsurancePolicy = new InsurancePolicy
                {
                    FirstName = faker.Name.FirstName(),
                    LastName = faker.Name.LastName(),
                    DriversLicence = faker.Random.AlphaNumeric(20),
                    VehicleName = faker.Vehicle.Model(),
                    VehicleManufacturer = faker.Vehicle.Manufacturer(),
                    VehicleModel = faker.Vehicle.Model(),
                    VehicleYear = Convert.ToInt16(faker.Date.PastDateOnly(30).Year),
                    Street = faker.Address.StreetAddress(),
                    City = faker.Address.City(),
                    State = faker.Address.State(),
                    ZipCode = faker.Address.ZipCode(),
                    EffectiveDate = faker.Date.Soon(35),
                    ExpirationDate = faker.Date.Future(1),
                    Premium = faker.Random.Decimal(0, 2000)
                };
                context.Set<InsurancePolicy>().Add(createdInsurancePolicy);
                context.Set<InsurancePolicy>().Add(new InsurancePolicy
                {
                    FirstName = faker.Name.FirstName(),
                    LastName = faker.Name.LastName(),
                    DriversLicence = faker.Random.AlphaNumeric(20),
                    VehicleName = faker.Vehicle.Model(),
                    VehicleManufacturer = faker.Vehicle.Manufacturer(),
                    VehicleModel = faker.Vehicle.Model(),
                    VehicleYear = Convert.ToInt16(faker.Date.PastDateOnly(30).Year),
                    Street = faker.Address.StreetAddress(),
                    City = faker.Address.City(),
                    State = faker.Address.State(),
                    ZipCode = faker.Address.ZipCode(),
                    EffectiveDate = faker.Date.Soon(35),
                    ExpirationDate = faker.Date.Future(1),
                    Premium = faker.Random.Decimal(0, 2000)
                });
                await context.SaveChangesAsync();
                id = createdInsurancePolicy.Id; //receive autogenerated guid to get the entity later
            }

            InsurancePolicy updateInsurancePolicy;
            using (var context = CreateDbContext("Update_Insurance_Policy"))
            {
                updateInsurancePolicy = await context.Set<InsurancePolicy>().FirstOrDefaultAsync(x => x.Id == id);
                updateInsurancePolicy.FirstName = "Fake Name";
                updateInsurancePolicy.Premium = 1500;
                var repository = new InsurancePolicyRepository(context);
                repository.Update(updateInsurancePolicy);
                result = await repository.SaveChangesAsync();
            }

            // Assert
            result.Should().BeGreaterThan(0);
            result.Should().Be(1);
            // Simulate access from another context to verifiy that correct data was saved to database
            using (var context = CreateDbContext("Update_Insurance_Policy"))
            {
                (await context.InsurancePolicies.FirstAsync(x => x.Id == updateInsurancePolicy.Id)).Should().BeEquivalentTo(updateInsurancePolicy);
            }
        }
        #endregion

        #region Delete_Insurance_Policy
        [Fact]
        public async Task Delete_Insurance_Policy()
        {
            Faker faker = new();
            int result;
            Guid? id;
            using (var context = CreateDbContext("Delete_Insurance_Policy"))
            {
                var createdInsurancePolicy = new InsurancePolicy
                {
                    FirstName = faker.Name.FirstName(),
                    LastName = faker.Name.LastName(),
                    DriversLicence = faker.Random.AlphaNumeric(20),
                    VehicleName = faker.Vehicle.Model(),
                    VehicleManufacturer = faker.Vehicle.Manufacturer(),
                    VehicleModel = faker.Vehicle.Model(),
                    VehicleYear = Convert.ToInt16(faker.Date.PastDateOnly(30).Year),
                    Street = faker.Address.StreetAddress(),
                    City = faker.Address.City(),
                    State = faker.Address.State(),
                    ZipCode = faker.Address.ZipCode(),
                    EffectiveDate = faker.Date.Soon(35),
                    ExpirationDate = faker.Date.Future(1),
                    Premium = faker.Random.Decimal(0, 2000)
                };
                context.Set<InsurancePolicy>().Add(createdInsurancePolicy);
                context.Set<InsurancePolicy>().Add(new InsurancePolicy 
                {
                    FirstName = faker.Name.FirstName(),
                    LastName = faker.Name.LastName(),
                    DriversLicence = faker.Random.AlphaNumeric(20),
                    VehicleName = faker.Vehicle.Model(),
                    VehicleManufacturer = faker.Vehicle.Manufacturer(),
                    VehicleModel = faker.Vehicle.Model(),
                    VehicleYear = Convert.ToInt16(faker.Date.PastDateOnly(30).Year),
                    Street = faker.Address.StreetAddress(),
                    City = faker.Address.City(),
                    State = faker.Address.State(),
                    ZipCode = faker.Address.ZipCode(),
                    EffectiveDate = faker.Date.Soon(35),
                    ExpirationDate = faker.Date.Future(1),
                    Premium = faker.Random.Decimal(0, 2000)
                });
                await context.SaveChangesAsync();
                id = createdInsurancePolicy.Id; //receive autogenerated guid to get the entity later
            }

            using (var context = CreateDbContext("Delete_Insurance_Policy"))
            {
                var repository = new InsurancePolicyRepository(context);
                await repository.Delete(id.Value);
                result = await repository.SaveChangesAsync();
            }

            // Assert
            result.Should().BeGreaterThan(0);
            result.Should().Be(1);
            // Simulate access from another context to verifiy that correct data was saved to database
            using (var context = CreateDbContext("Delete_Insurance_Policy"))
            {
                (await context.Set<InsurancePolicy>().FirstOrDefaultAsync(x => x.Id == id)).Should().BeNull();
                (await context.Set<InsurancePolicy>().ToListAsync()).Should().NotBeEmpty();
            }
        } 
        #endregion

        #region NullDbContext_Throws_ArgumentNullException
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
        #endregion
    }
}

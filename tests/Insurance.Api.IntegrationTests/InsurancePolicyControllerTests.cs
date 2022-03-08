using Bogus;
using FluentAssertions;
using Insurance.Api.Application.DTOs;
using Insurance.Api.Application.DTOs.Auth;
using Insurance.Api.Application.DTOs.InsurancePolicy;
using Insurance.Api.Domain.Entities;
using Insurance.Api.IntegrationTests.Helpers;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Insurance.Api.IntegrationTests
{
    public class InsurancePolicyControllerTests : IntegrationTest, IAsyncLifetime
    {

        public InsurancePolicyControllerTests(WebApplicationFactoryFixture fixture) : base(fixture) { }

        public async Task InitializeAsync()
        {
            AdminToken ??= await GetAdminToken();
        }

        public static string AdminToken { get; private set; }

        [Fact]
        public async Task Get_AllInsurancePoliciesWithFilter_ReturnsOk()
        {
            // Arrange
            var client = Factory.RebuildDb().CreateClient();
            client.UpdateBearerToken(AdminToken);

            // Act
            var response = await client.GetAsync("/api/InsurancePolicy?driversLicence=749882582");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var json = await response.DeserializeContent<PaginatedList<GetInsurancePolicyDto>>();
            json.Should().NotBeNull();
            json.Result.Should().OnlyHaveUniqueItems();
            json.Result.Should().HaveCount(1);
            json.CurrentPage.Should().Be(1);
            json.TotalItems.Should().Be(1);
            json.TotalPages.Should().Be(1);
        }

        [Fact]
        public async Task GetByPolicyId_ExistingInsurancePolicy_ReturnsOk()
        {
            // Arrange
            var client = Factory.RebuildDb().CreateClient();
            client.UpdateBearerToken(AdminToken);

            // Act
            var response = await client.GetAsync("/api/InsurancePolicy/687d9fd5-2752-4a96-93d5-0f33a49913c6/749882582");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var json = await response.DeserializeContent<GetInsurancePolicyDto>();
            json.Should().NotBeNull();
            json.Id.Should().NotBeEmpty();
            json.DriversLicence.Should().NotBeNull();
        }

        [Fact]
        public async Task GetByPolicyId_ExistingInsurancePolicy_ReturnsNotFound()
        {
            // Arrange
            var client = Factory.RebuildDb().CreateClient();
            client.UpdateBearerToken(AdminToken);

            // Act
            var response = await client.GetAsync($"/api/InsurancePolicy/{Guid.NewGuid()}/749882582");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Post_ValidInsurancePolicy_ReturnsCreated()
        {
            // Arrange
            var client = Factory.RebuildDb().CreateClient();
            client.UpdateBearerToken(AdminToken);

            // Act
            Faker faker = new();

            var insurancePolicy = new InsurancePolicy
            {
                FirstName = faker.Name.FirstName(),
                LastName = faker.Name.LastName(),
                DriversLicence = faker.Random.AlphaNumeric(20),
                VehicleName = faker.Vehicle.Model(),
                VehicleManufacturer = faker.Vehicle.Manufacturer(),
                VehicleModel = faker.Vehicle.Model(),
                VehicleYear = 1995,
                Street = faker.Address.StreetAddress(),
                City = faker.Address.City(),
                State = faker.Address.State(),
                ZipCode = faker.Address.ZipCode(),
                EffectiveDate = DateTime.Now.AddDays(31),
                ExpirationDate = faker.Date.Future(4),
                Premium = faker.Random.Decimal(0, 2000)
            };
            var response = await client.PostAsync("/api/InsurancePolicy", insurancePolicy.GetStringContent());

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            var json = await response.DeserializeContent<GetInsurancePolicyDto>();
            json.Should().NotBeNull();
            json.Id.Should().NotBeEmpty();
            json.FirstName.Should().NotBeEmpty();
            json.LastName.Should().NotBeEmpty();
            json.DriversLicence.Should().NotBeEmpty();
            json.VehicleName.Should().NotBeEmpty();
            json.VehicleManufacturer.Should().NotBeEmpty();
            json.VehicleModel.Should().NotBeEmpty();
            json.VehicleYear.Should().NotBe(0);
            json.Street.Should().NotBeEmpty();
            json.City.Should().NotBeEmpty();
            json.State.Should().NotBeEmpty();
            json.ZipCode.Should().NotBeEmpty();
            json.Premium.Should().NotBe(0);

        }

        private async Task<string> GetAdminToken()
        {
            // Arrange
            var client = Factory.RebuildDb().CreateClient();

            // Act
            var loginData = new
            {
                Email = "admin@email.com",
                Password = "adminpassword"
            };

            var response = await client.PostAsync("/api/User/authenticate", loginData.GetStringContent());
            var json = await response.DeserializeContent<JwtDto>();

            return json.Token;
        }

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }
    }
}

using FluentAssertions;
using Insurance.Api.Application.DTOs.Auth;
using Insurance.Api.IntegrationTests.Helpers;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Insurance.Api.IntegrationTests
{
    public class UserControllerTests : IntegrationTest, IAsyncLifetime
    {
        public UserControllerTests(WebApplicationFactoryFixture fixture) : base(fixture)
        {

        }

        public async Task InitializeAsync()
        {
            AdminToken ??= await GetAdminToken();
            UserToken ??= await GetUserToken();
        }

        public static string AdminToken { get; private set; }

        public static string UserToken { get; private set; }

        [Fact]
        public async Task<string> GetAdminToken()
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
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var json = await response.DeserializeContent<JwtDto>();
            json.Should().NotBeNull();
            json.ExpDate.Should().NotBe(DateTime.MinValue);
            json.Token.Should().NotBeNullOrWhiteSpace();

            return json.Token;
        }

        [Fact]
        public async Task<string> GetUserToken()
        {
            // Arrange
            var client = Factory.RebuildDb().CreateClient();

            // Act
            var loginData = new
            {
                Email = "user@email.com",
                Password = "userpassword"
            };

            var response = await client.PostAsync("/api/User/authenticate", loginData.GetStringContent());
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var json = await response.DeserializeContent<JwtDto>();
            json.Should().NotBeNull();
            json.ExpDate.Should().NotBe(DateTime.MinValue);
            json.Token.Should().NotBeNullOrWhiteSpace();

            return json.Token;
        }

        [Theory]
        [InlineData("admin@email.com", "incorrect")]
        [InlineData("admin@incorrect.com", "adminpassword")]
        public async Task Authenticate_IncorretUserOrPassword(string email, string password)
        {
            // Arrange
            var client = Factory.RebuildDb().CreateClient();

            // Act
            var loginData = new
            {
                Email = email,
                Password = password
            };
            var response = await client.PostAsync("/api/User/authenticate", loginData.GetStringContent());
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }
    }
}

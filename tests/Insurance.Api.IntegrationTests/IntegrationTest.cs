using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Insurance.Api.IntegrationTests
{
    public abstract class IntegrationTest : IClassFixture<WebApplicationFactoryFixture>
    {
        protected readonly WebApplicationFactory<Startup> Factory;

        protected IntegrationTest(WebApplicationFactoryFixture fixture)
        {
            Factory = fixture.Factory;
        }
    }
}

using System;
using System.Net;
using System.Threading.Tasks;
using Insurance.Api.IntegrationTests.Helpers;
using Insurance.Api.Application.DTOs;
using Insurance.Api.Application.DTOs.InsurancePolicy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Insurance.Api.IntegrationTests
{
    public class InsurancePolicyControllerTests : IntegrationTest
    {

        public InsurancePolicyControllerTests(WebApplicationFactoryFixture fixture) : base(fixture) { }
    }
}

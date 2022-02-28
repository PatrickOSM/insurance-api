using System;
using System.Collections.Generic;
using System.Linq;
using Insurance.Api.Domain.Entities;
using Insurance.Api.Infrastructure.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using BC = BCrypt.Net.BCrypt;

namespace Insurance.Api.IntegrationTests.Helpers
{
    public static class Utilities
    {
        public static void InitializeDbForTests(ApplicationDbContext db)
        {
            db.Users.RemoveRange(db.Users);
            db.InsurancePolicies.RemoveRange(db.InsurancePolicies);
            db.InsurancePolicies.AddRange(GetSeedingInsurancePolicies());
            db.Users.AddRange(GetSeedingUsers());
            db.SaveChanges();
        }

        public static User[] GetSeedingUsers()
        {
            return new User[]
            {
                new User()
                {
                    Id = new Guid("2e3b7a21-f06e-4c47-b28a-89bdaa3d2a37"),
                    Password = BC.HashPassword("testpassword123"),
                    Email = "admin@email.com",
                    Role = "Admin"
                },
                new User()
                {
                    Id = new Guid("c68acd7b-9054-4dc3-b536-17a1b81fa7a3"),
                    Password = BC.HashPassword("testpassword123"),
                    Email = "user@email.com",
                    Role = "User"
                }
            };
        }

        public static void ReinitializeDbForTests(ApplicationDbContext db)
        {
            db.InsurancePolicies.RemoveRange(db.InsurancePolicies.ToList());
            db.Users.RemoveRange(db.Users.ToList());
            InitializeDbForTests(db);
        }

        public static List<InsurancePolicy> GetSeedingInsurancePolicies()
        {
            return new()
            { };
        }

        public static WebApplicationFactory<Startup> BuildApplicationFactory(this WebApplicationFactory<Startup> factory)
        {
            var connectionString = $"Data Source={Guid.NewGuid()}.db";
            return factory.WithWebHostBuilder(builder =>
            {
                builder.UseEnvironment("Testing");
                builder.ConfigureServices(services =>
                {
                    var descriptor = services.SingleOrDefault(
                        d => d.ServiceType ==
                    typeof(DbContextOptions<ApplicationDbContext>));

                    services.Remove(descriptor);

                    services.AddDbContext<ApplicationDbContext>(options =>
                    {
                        options.UseSqlite(connectionString);
                    });

                    var sp = services.BuildServiceProvider();

                    using var scope = sp.CreateScope();
                    var scopedServices = scope.ServiceProvider;
                    var context = scopedServices.GetRequiredService<ApplicationDbContext>();
                    context.Database.EnsureCreated();
                    InitializeDbForTests(context);
                });
            });
        }


        public static WebApplicationFactory<Startup> RebuildDb(this WebApplicationFactory<Startup> factory)
        {
            return factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    var serviceProvider = services.BuildServiceProvider();
                    using var scope = serviceProvider.CreateScope();
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices
                        .GetRequiredService<ApplicationDbContext>();
                    ReinitializeDbForTests(db);
                });
            });
        }
    }
}

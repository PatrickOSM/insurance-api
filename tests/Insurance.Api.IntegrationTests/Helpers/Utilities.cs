using Insurance.Api.Domain.Entities;
using Insurance.Api.Infrastructure.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
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
                    Password = BC.HashPassword("adminpassword"),
                    Email = "admin@email.com",
                    Role = "Admin"
                },
                new User()
                {
                    Id = new Guid("c68acd7b-9054-4dc3-b536-17a1b81fa7a3"),
                    Password = BC.HashPassword("userpassword"),
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

        public static InsurancePolicy[] GetSeedingInsurancePolicies()
        {
            return new InsurancePolicy[]
            {
                new InsurancePolicy
                {
                    Id = new Guid("687d9fd5-2752-4a96-93d5-0f33a49913c6"),
                    FirstName = "Cody",
                    LastName = "Jacobs",
                    DriversLicence = "749882582",
                    VehicleName = "Fiat Bravo",
                    VehicleManufacturer = "Fiat",
                    VehicleModel = "Fiat Bravo/Bravia",
                    VehicleYear = 1995,
                    Street = "2184 Preston Rd",
                    City = "Plano",
                    State = "Texas",
                    ZipCode = "75093",
                    EffectiveDate = new DateTime(2021, 02, 25, 22, 35, 5),
                    ExpirationDate = new DateTime(2022, 02, 25, 22, 35, 5),
                    Premium = 500
                },
                new InsurancePolicy
                {
                    Id = new Guid("ff2061ea-cce1-47ab-a6c0-d01a86e1f051"),
                    FirstName = "Eugene",
                    LastName = "T Jordan",
                    DriversLicence = "611312842",
                    VehicleName = "Ford Mustang GT 5.0",
                    VehicleManufacturer = "Ford",
                    VehicleModel = "Ford T5",
                    VehicleYear = 1979,
                    Street = "4468 Brooklyn Street",
                    City = "Troutville",
                    State = "Virginia",
                    ZipCode = "24175",
                    EffectiveDate = new DateTime(2020, 02, 25, 22, 35, 5),
                    ExpirationDate = new DateTime(2021, 02, 25, 22, 35, 5),
                    Premium = 800
                }
            };
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

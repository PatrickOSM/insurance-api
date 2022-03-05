using Insurance.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Insurance.Api.Infrastructure.Configuration
{
    public class InsurancePolicyConfiguration : IEntityTypeConfiguration<InsurancePolicy>
    {
        public void Configure(EntityTypeBuilder<InsurancePolicy> builder)
        {
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(254);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(254);
            builder.Property(x => x.DriversLicence).IsRequired().HasMaxLength(20);
            builder.Property(x => x.VehicleName).IsRequired().HasMaxLength(254);
            builder.Property(x => x.VehicleModel).IsRequired().HasMaxLength(254);
            builder.Property(x => x.VehicleManufacturer).IsRequired().HasMaxLength(254);
            builder.Property(x => x.VehicleYear).IsRequired();
            builder.Property(x => x.Street).IsRequired().HasMaxLength(254);
            builder.Property(x => x.City).IsRequired().HasMaxLength(254);
            builder.Property(x => x.State).IsRequired().HasMaxLength(254);
            builder.Property(x => x.ZipCode).IsRequired().HasMaxLength(15);
            builder.Property(x => x.EffectiveDate).IsRequired();
            builder.Property(x => x.ExpirationDate).IsRequired();
            builder.Property(x => x.Premium).IsRequired();

            builder.HasData(
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
            );
        }
    }
}

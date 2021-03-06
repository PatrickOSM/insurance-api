// <auto-generated />
using System;
using Insurance.Api.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Insurance.Api.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220301042435_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Insurance.Api.Domain.Entities.InsurancePolicy", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(254)
                        .HasColumnType("nvarchar(254)");

                    b.Property<string>("DriversLicence")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<DateTime>("EffectiveDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(254)
                        .HasColumnType("nvarchar(254)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(254)
                        .HasColumnType("nvarchar(254)");

                    b.Property<decimal>("Premium")
                        .HasColumnType("money");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(254)
                        .HasColumnType("nvarchar(254)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(254)
                        .HasColumnType("nvarchar(254)");

                    b.Property<string>("VehicleManufacturer")
                        .IsRequired()
                        .HasMaxLength(254)
                        .HasColumnType("nvarchar(254)");

                    b.Property<string>("VehicleModel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VehicleName")
                        .IsRequired()
                        .HasMaxLength(254)
                        .HasColumnType("nvarchar(254)");

                    b.Property<short>("VehicleYear")
                        .HasColumnType("smallint");

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("Id");

                    b.ToTable("InsurancePolicies");

                    b.HasData(
                        new
                        {
                            Id = new Guid("687d9fd5-2752-4a96-93d5-0f33a49913c6"),
                            City = "Plano",
                            DriversLicence = "749882582",
                            EffectiveDate = new DateTime(2021, 2, 25, 22, 35, 5, 0, DateTimeKind.Unspecified),
                            ExpirationDate = new DateTime(2022, 2, 25, 22, 35, 5, 0, DateTimeKind.Unspecified),
                            FirstName = "Cody",
                            LastName = "Jacobs",
                            Premium = 5000.0m,
                            State = "Texas",
                            Street = "2184 Preston Rd",
                            VehicleManufacturer = "Fiat",
                            VehicleModel = "Fiat Bravo/Bravia",
                            VehicleName = "Fiat Bravo",
                            VehicleYear = (short)1995,
                            ZipCode = "75093"
                        },
                        new
                        {
                            Id = new Guid("ff2061ea-cce1-47ab-a6c0-d01a86e1f051"),
                            City = "Troutville",
                            DriversLicence = "611312842",
                            EffectiveDate = new DateTime(2020, 2, 25, 22, 35, 5, 0, DateTimeKind.Unspecified),
                            ExpirationDate = new DateTime(2021, 2, 25, 22, 35, 5, 0, DateTimeKind.Unspecified),
                            FirstName = "Eugene",
                            LastName = "T Jordan",
                            Premium = 8000.0m,
                            State = "Virginia",
                            Street = "4468 Brooklyn Street",
                            VehicleManufacturer = "Ford",
                            VehicleModel = "Ford T5",
                            VehicleName = "Ford Mustang GT 5.0",
                            VehicleYear = (short)1979,
                            ZipCode = "24175"
                        });
                });

            modelBuilder.Entity("Insurance.Api.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(254)
                        .HasColumnType("nvarchar(254)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("687d9fd5-2752-4a96-93d5-0f33a49913c6"),
                            Email = "admin@email.com",
                            Password = "$2a$11$P2rl2dFPWd7DKPomVzZgn.YKRPpHGGTUJhKdXX6rJB2CR3hbDNxhG",
                            Role = "Admin"
                        },
                        new
                        {
                            Id = new Guid("6648c89f-e894-42bb-94f0-8fd1059c86b4"),
                            Email = "user@email.com",
                            Password = "$2a$11$7/MGkmKhMIOk.p6nRa3m0uUnIf5eHjX069NGKBxKQN6tIlcdqrSqO",
                            Role = "User"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}

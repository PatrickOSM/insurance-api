using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace Insurance.Api.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InsurancePolicies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: false),
                    DriversLicence = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    VehicleName = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: false),
                    VehicleModel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VehicleManufacturer = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: false),
                    VehicleYear = table.Column<short>(type: "smallint", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: false),
                    City = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: false),
                    State = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: false),
                    ZipCode = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    EffectiveDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Premium = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsurancePolicies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "InsurancePolicies",
                columns: new[] { "Id", "City", "DriversLicence", "EffectiveDate", "ExpirationDate", "FirstName", "LastName", "Premium", "State", "Street", "VehicleManufacturer", "VehicleModel", "VehicleName", "VehicleYear", "ZipCode" },
                values: new object[,]
                {
                    { new Guid("687d9fd5-2752-4a96-93d5-0f33a49913c6"), "Plano", "749882582", new DateTime(2021, 2, 25, 22, 35, 5, 0, DateTimeKind.Unspecified), new DateTime(2022, 2, 25, 22, 35, 5, 0, DateTimeKind.Unspecified), "Cody", "Jacobs", 5000.0m, "Texas", "2184 Preston Rd", "Fiat", "Fiat Bravo/Bravia", "Fiat Bravo", (short)1995, "75093" },
                    { new Guid("ff2061ea-cce1-47ab-a6c0-d01a86e1f051"), "Troutville", "611312842", new DateTime(2020, 2, 25, 22, 35, 5, 0, DateTimeKind.Unspecified), new DateTime(2021, 2, 25, 22, 35, 5, 0, DateTimeKind.Unspecified), "Eugene", "T Jordan", 8000.0m, "Virginia", "4468 Brooklyn Street", "Ford", "Ford T5", "Ford Mustang GT 5.0", (short)1979, "24175" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Password", "Role" },
                values: new object[,]
                {
                    { new Guid("6648c89f-e894-42bb-94f0-8fd1059c86b4"), "user@email.com", "$2a$11$7/MGkmKhMIOk.p6nRa3m0uUnIf5eHjX069NGKBxKQN6tIlcdqrSqO", "User" },
                    { new Guid("687d9fd5-2752-4a96-93d5-0f33a49913c6"), "admin@email.com", "$2a$11$P2rl2dFPWd7DKPomVzZgn.YKRPpHGGTUJhKdXX6rJB2CR3hbDNxhG", "Admin" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InsurancePolicies");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}

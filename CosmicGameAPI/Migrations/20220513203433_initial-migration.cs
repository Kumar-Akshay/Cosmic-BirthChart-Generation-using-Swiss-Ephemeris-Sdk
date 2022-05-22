using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CosmicGameAPI.Migrations
{
    public partial class initialmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActualTimeZone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameOfTimeZone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalTimeZone = table.Column<int>(type: "int", nullable: false),
                    CountOfTimeZone = table.Column<int>(type: "int", nullable: false),
                    AreasCovered = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YearFrom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDST = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DSTFromTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DSTToTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeZoneValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CurrentAddresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    State = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsActive = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TimeZone = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LatLocator = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    LngLocator = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    Longitude = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Latitude = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrentAddresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserLoginInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoginType = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    IpAddress = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsLogin = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Browser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastLoginDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLoginInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    IsActive = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsAllowMultipelLogin = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsApproved = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsDelete = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "CurrentAddresses");

            migrationBuilder.DropTable(
                name: "UserLoginInfos");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}

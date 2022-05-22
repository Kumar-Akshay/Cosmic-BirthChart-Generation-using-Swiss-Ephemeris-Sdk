using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CosmicGameAPI.Migrations
{
    public partial class addnewentities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BhavaPlanets",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChartHolderId = table.Column<int>(type: "int", nullable: false),
                    Bhavalst = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Palenetlst = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BhavaPlanets", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ChartHolders",
                columns: table => new
                {
                    ChartHolderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ChildName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FatherName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    MotherName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    BirthPlace = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    State = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TOB = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Timezone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Ayanamasa = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    HouseSystem = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    AyanamasaPolicy = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    LatLocator = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    LngLocator = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Latitude = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Longitude = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ContactPhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    IsAdd = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChartHolders", x => x.ChartHolderId);
                });

            migrationBuilder.CreateTable(
                name: "ErrorLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErrorLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VimsoMasterRegisters",
                columns: table => new
                {
                    LineNo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rasi_Id = table.Column<int>(type: "int", nullable: false),
                    Rasi = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Rasi_L = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Start_Id = table.Column<int>(type: "int", nullable: false),
                    StarName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Vimso_Pd = table.Column<int>(type: "int", nullable: false),
                    DI_GP = table.Column<int>(type: "int", nullable: false),
                    StarLord = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Pu_Gp = table.Column<int>(type: "int", nullable: false),
                    S1SL = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    An_Gp = table.Column<int>(type: "int", nullable: false),
                    S2SL = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    So_Gp = table.Column<int>(type: "int", nullable: false),
                    S3SL = table.Column<int>(type: "int", maxLength: 255, nullable: false),
                    S4SL = table.Column<int>(type: "int", maxLength: 255, nullable: false),
                    S4SL_ArcDist = table.Column<float>(type: "real", nullable: false),
                    Moving_Distance = table.Column<float>(type: "real", nullable: false),
                    DMS = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Pre_S4SL_ArcDist = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VimsoMasterRegisters", x => x.LineNo);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BhavaPlanets");

            migrationBuilder.DropTable(
                name: "ChartHolders");

            migrationBuilder.DropTable(
                name: "ErrorLogs");

            migrationBuilder.DropTable(
                name: "VimsoMasterRegisters");
        }
    }
}

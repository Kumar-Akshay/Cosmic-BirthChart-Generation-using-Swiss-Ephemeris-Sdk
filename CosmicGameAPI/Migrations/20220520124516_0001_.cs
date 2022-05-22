using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CosmicGameAPI.Migrations
{
    public partial class _0001_ : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "BirthTimeRectified",
                table: "ChartHolders",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ChartType",
                table: "ChartHolders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "RectifiedTOB",
                table: "ChartHolders",
                type: "time",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChartType",
                table: "ChartHolders");

            migrationBuilder.DropColumn(
                name: "RectifiedTOB",
                table: "ChartHolders");

            migrationBuilder.AlterColumn<string>(
                name: "BirthTimeRectified",
                table: "ChartHolders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");
        }
    }
}

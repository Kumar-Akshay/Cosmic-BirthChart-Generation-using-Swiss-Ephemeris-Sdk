using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CosmicGameAPI.Migrations
{
    public partial class thridmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pre_S4SL_ArcDist",
                table: "VimsoMasterRegisters");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "ChartHolders");

            migrationBuilder.RenameColumn(
                name: "Vimso_Pd",
                table: "VimsoMasterRegisters",
                newName: "Vimso_pd");

            migrationBuilder.RenameColumn(
                name: "Rasi_Id",
                table: "VimsoMasterRegisters",
                newName: "Rasi_id");

            migrationBuilder.RenameColumn(
                name: "DI_GP",
                table: "VimsoMasterRegisters",
                newName: "Di_Gp");

            migrationBuilder.RenameColumn(
                name: "Start_Id",
                table: "VimsoMasterRegisters",
                newName: "Star_id");

            migrationBuilder.AlterColumn<string>(
                name: "StarName",
                table: "VimsoMasterRegisters",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "StarLord",
                table: "VimsoMasterRegisters",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "S4SL_ArcDist",
                table: "VimsoMasterRegisters",
                type: "float",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<string>(
                name: "S4SL",
                table: "VimsoMasterRegisters",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "S3SL",
                table: "VimsoMasterRegisters",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "S2SL",
                table: "VimsoMasterRegisters",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "S1SL",
                table: "VimsoMasterRegisters",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Rasi_L",
                table: "VimsoMasterRegisters",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Rasi",
                table: "VimsoMasterRegisters",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Moving_Distance",
                table: "VimsoMasterRegisters",
                type: "float",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<string>(
                name: "DMS",
                table: "VimsoMasterRegisters",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "ChartHolders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "BirthTimeRectified",
                table: "ChartHolders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ChartType",
                table: "ChartHolders",
                type: "int",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "RectifiedTOB",
                table: "ChartHolders",
                type: "time",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "u_Lev4_S4SL_Registers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LineNo = table.Column<double>(type: "float", nullable: true),
                    S1SLCount = table.Column<double>(type: "float", nullable: true),
                    S2SLCount = table.Column<double>(type: "float", nullable: true),
                    S3SLCount = table.Column<double>(type: "float", nullable: true),
                    S4SLCount = table.Column<double>(type: "float", nullable: true),
                    Ext_Line = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KC_INDEX = table.Column<double>(type: "float", nullable: true),
                    KC_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KC_Rasi_Lord = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KC_Rasi_Lord_SPL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StarCount = table.Column<double>(type: "float", nullable: true),
                    StarName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StarLord = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VimsoPeriod = table.Column<double>(type: "float", nullable: true),
                    S1SL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    S2SL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    S3SL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    S4SL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    S4SL_ArcDist = table.Column<double>(type: "float", nullable: true),
                    DMS = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_u_Lev4_S4SL_Registers", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "u_Lev4_S4SL_Registers");

            migrationBuilder.DropColumn(
                name: "BirthTimeRectified",
                table: "ChartHolders");

            migrationBuilder.DropColumn(
                name: "ChartType",
                table: "ChartHolders");

            migrationBuilder.DropColumn(
                name: "RectifiedTOB",
                table: "ChartHolders");

            migrationBuilder.RenameColumn(
                name: "Vimso_pd",
                table: "VimsoMasterRegisters",
                newName: "Vimso_Pd");

            migrationBuilder.RenameColumn(
                name: "Rasi_id",
                table: "VimsoMasterRegisters",
                newName: "Rasi_Id");

            migrationBuilder.RenameColumn(
                name: "Di_Gp",
                table: "VimsoMasterRegisters",
                newName: "DI_GP");

            migrationBuilder.RenameColumn(
                name: "Star_id",
                table: "VimsoMasterRegisters",
                newName: "Start_Id");

            migrationBuilder.AlterColumn<string>(
                name: "StarName",
                table: "VimsoMasterRegisters",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "StarLord",
                table: "VimsoMasterRegisters",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "S4SL_ArcDist",
                table: "VimsoMasterRegisters",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<int>(
                name: "S4SL",
                table: "VimsoMasterRegisters",
                type: "int",
                maxLength: 255,
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "S3SL",
                table: "VimsoMasterRegisters",
                type: "int",
                maxLength: 255,
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "S2SL",
                table: "VimsoMasterRegisters",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "S1SL",
                table: "VimsoMasterRegisters",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Rasi_L",
                table: "VimsoMasterRegisters",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Rasi",
                table: "VimsoMasterRegisters",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "Moving_Distance",
                table: "VimsoMasterRegisters",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<string>(
                name: "DMS",
                table: "VimsoMasterRegisters",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Pre_S4SL_ArcDist",
                table: "VimsoMasterRegisters",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "ChartHolders",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "ChartHolders",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true);
        }
    }
}

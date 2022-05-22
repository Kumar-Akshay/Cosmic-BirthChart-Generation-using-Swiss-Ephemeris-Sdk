using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CosmicGameAPI.Migrations
{
    public partial class _0002_ : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Pre_S4SL_ArcDist",
                table: "VimsoMasterRegisters",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pre_S4SL_ArcDist",
                table: "VimsoMasterRegisters");
        }
    }
}

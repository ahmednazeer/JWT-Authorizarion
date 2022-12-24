using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class updateTeamTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Teams",
                newName: "TeamPrincipal");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Teams",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country",
                table: "Teams");

            migrationBuilder.RenameColumn(
                name: "TeamPrincipal",
                table: "Teams",
                newName: "Description");
        }
    }
}

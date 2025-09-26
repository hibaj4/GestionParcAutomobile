using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerLibrary.Data.Migrations
{
    /// <inheritdoc />
    public partial class Nv : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TypeDestination",
                table: "Trajets");

            migrationBuilder.AddColumn<bool>(
                name: "AvecChauffeur",
                table: "Voitures",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvecChauffeur",
                table: "Voitures");

            migrationBuilder.AddColumn<string>(
                name: "TypeDestination",
                table: "Trajets",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerLibrary.Data.Migrations
{
    /// <inheritdoc />
    public partial class Nv3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MotDePasse",
                table: "Utilisateurs",
                newName: "Grade");

            migrationBuilder.AddColumn<string>(
                name: "Adresse",
                table: "Utilisateurs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CIN",
                table: "Utilisateurs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Fonction",
                table: "Utilisateurs",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Adresse",
                table: "Utilisateurs");

            migrationBuilder.DropColumn(
                name: "CIN",
                table: "Utilisateurs");

            migrationBuilder.DropColumn(
                name: "Fonction",
                table: "Utilisateurs");

            migrationBuilder.RenameColumn(
                name: "Grade",
                table: "Utilisateurs",
                newName: "MotDePasse");
        }
    }
}

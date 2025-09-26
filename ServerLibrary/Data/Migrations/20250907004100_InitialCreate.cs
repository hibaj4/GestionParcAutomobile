using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerLibrary.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PleinCarburants");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PleinCarburants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UtilisateurId = table.Column<int>(type: "int", nullable: false),
                    VoitureId = table.Column<int>(type: "int", nullable: false),
                    CoutLitre = table.Column<double>(type: "float", nullable: false),
                    DatePlein = table.Column<DateTime>(type: "datetime2", nullable: false),
                    KilometrageDebut = table.Column<double>(type: "float", nullable: false),
                    KilometrageFin = table.Column<double>(type: "float", nullable: false),
                    Litres = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PleinCarburants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PleinCarburants_Utilisateurs_UtilisateurId",
                        column: x => x.UtilisateurId,
                        principalTable: "Utilisateurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PleinCarburants_Voitures_VoitureId",
                        column: x => x.VoitureId,
                        principalTable: "Voitures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PleinCarburants_UtilisateurId",
                table: "PleinCarburants",
                column: "UtilisateurId");

            migrationBuilder.CreateIndex(
                name: "IX_PleinCarburants_VoitureId",
                table: "PleinCarburants",
                column: "VoitureId");
        }
    }
}

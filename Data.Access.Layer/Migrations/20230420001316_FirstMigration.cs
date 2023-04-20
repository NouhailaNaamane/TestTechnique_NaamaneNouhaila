using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Access.Layer.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Candidats",
                columns: table => new
                {
                    IdCandidat = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prenom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telephone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NiveauEtude = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NbAnneeExpr = table.Column<int>(type: "int", nullable: false),
                    DernierEmployeur = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CvFileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidats", x => x.IdCandidat);
                });

            migrationBuilder.CreateTable(
                name: "Offres",
                columns: table => new
                {
                    IdOffres = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Titre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatePublish = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateFin = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MinSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaxSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MinNbrExperience = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offres", x => x.IdOffres);
                });

            migrationBuilder.CreateTable(
                name: "Candidatures",
                columns: table => new
                {
                    IdOffre = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdCandidat = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateCandidature = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidatures", x => new { x.IdCandidat, x.IdOffre });
                    table.ForeignKey(
                        name: "FK_Candidatures_Candidats_IdCandidat",
                        column: x => x.IdCandidat,
                        principalTable: "Candidats",
                        principalColumn: "IdCandidat",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Candidatures_Offres_IdOffre",
                        column: x => x.IdOffre,
                        principalTable: "Offres",
                        principalColumn: "IdOffres",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Candidatures_IdOffre",
                table: "Candidatures",
                column: "IdOffre");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Candidatures");

            migrationBuilder.DropTable(
                name: "Candidats");

            migrationBuilder.DropTable(
                name: "Offres");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OA_Core.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AvaliacaoMigrationCriada : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Avaliacao",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Nome = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descricao = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    NotaMaxima = table.Column<double>(type: "double", nullable: true),
                    NotaMinima = table.Column<double>(type: "double", nullable: true),
                    Tempo = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    TotalQuestoes = table.Column<int>(type: "int", nullable: true),
                    Ativa = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DataEntrega = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    AulaId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DataCriacao = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DataDelecao = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Avaliacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Avaliacao_Aula_AulaId",
                        column: x => x.AulaId,
                        principalTable: "Aula",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Avaliacao_AulaId",
                table: "Avaliacao",
                column: "AulaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Avaliacao");
        }
    }
}

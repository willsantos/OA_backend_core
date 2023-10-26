using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OA_Core.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AvaliacaoUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AvaliacaoUsuario",
                columns: table => new
                {
                    AvaliacaoId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UsuarioId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    NotaObtida = table.Column<double>(type: "double", nullable: true),
                    Aprovado = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Inicio = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Fim = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvaliacaoUsuario", x => new { x.AvaliacaoId, x.UsuarioId });
                    table.ForeignKey(
                        name: "FK_AvaliacaoUsuario_Avaliacao_AvaliacaoId",
                        column: x => x.AvaliacaoId,
                        principalTable: "Avaliacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AvaliacaoUsuario_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_AvaliacaoUsuario_UsuarioId",
                table: "AvaliacaoUsuario",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AvaliacaoUsuario");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OA_Core.Repository.Migrations
{
    /// <inheritdoc />
    public partial class CorrigindoCamposAvaliacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Avaliacao_Aula_AulaId",
                table: "Avaliacao");

            migrationBuilder.DropIndex(
                name: "IX_Avaliacao_AulaId",
                table: "Avaliacao");

            migrationBuilder.DropColumn(
                name: "AulaId",
                table: "Avaliacao");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AulaId",
                table: "Avaliacao",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Avaliacao_AulaId",
                table: "Avaliacao",
                column: "AulaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Avaliacao_Aula_AulaId",
                table: "Avaliacao",
                column: "AulaId",
                principalTable: "Aula",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

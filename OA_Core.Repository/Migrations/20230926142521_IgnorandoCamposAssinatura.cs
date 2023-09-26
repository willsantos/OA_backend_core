using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OA_Core.Repository.Migrations
{
    /// <inheritdoc />
    public partial class IgnorandoCamposAssinatura : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataAlteracao",
                table: "Assinatura");

            migrationBuilder.DropColumn(
                name: "DataDelecao",
                table: "Assinatura");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataAlteracao",
                table: "Assinatura",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataDelecao",
                table: "Assinatura",
                type: "datetime(6)",
                nullable: true);
        }
    }
}

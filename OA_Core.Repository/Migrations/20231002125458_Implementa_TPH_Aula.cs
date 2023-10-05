using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OA_Core.Repository.Migrations
{
    /// <inheritdoc />
    public partial class Implementa_TPH_Aula : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Caminho",
                table: "Aula");

            migrationBuilder.DropColumn(
                name: "Descricao",
                table: "Aula");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "Aula",
                newName: "Titulo");

            migrationBuilder.AlterColumn<int>(
                name: "Tipo",
                table: "Aula",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "AulaOnline_Url",
                table: "Aula",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "AulaVideo_Url",
                table: "Aula",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Conteudo",
                table: "Aula",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "HorarioFim",
                table: "Aula",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "HorarioInicio",
                table: "Aula",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TipoAulaEnum",
                table: "Aula",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Aula",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AulaOnline_Url",
                table: "Aula");

            migrationBuilder.DropColumn(
                name: "AulaVideo_Url",
                table: "Aula");

            migrationBuilder.DropColumn(
                name: "Conteudo",
                table: "Aula");

            migrationBuilder.DropColumn(
                name: "HorarioFim",
                table: "Aula");

            migrationBuilder.DropColumn(
                name: "HorarioInicio",
                table: "Aula");

            migrationBuilder.DropColumn(
                name: "TipoAulaEnum",
                table: "Aula");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "Aula");

            migrationBuilder.RenameColumn(
                name: "Titulo",
                table: "Aula",
                newName: "Nome");

            migrationBuilder.AlterColumn<string>(
                name: "Tipo",
                table: "Aula",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Caminho",
                table: "Aula",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Descricao",
                table: "Aula",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}

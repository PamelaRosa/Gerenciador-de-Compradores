using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebMvcMySql.Migrations
{
    /// <inheritdoc />
    public partial class AddNewColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "Buyers",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "BusinessName",
                table: "Buyers",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CNPJ",
                table: "Buyers",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CPF",
                table: "Buyers",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Buyers",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "IsFree",
                table: "Buyers",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "PersonTypeId",
                table: "Buyers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "StateDoc",
                table: "Buyers",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "Buyers");

            migrationBuilder.DropColumn(
                name: "BusinessName",
                table: "Buyers");

            migrationBuilder.DropColumn(
                name: "CNPJ",
                table: "Buyers");

            migrationBuilder.DropColumn(
                name: "CPF",
                table: "Buyers");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Buyers");

            migrationBuilder.DropColumn(
                name: "IsFree",
                table: "Buyers");

            migrationBuilder.DropColumn(
                name: "PersonTypeId",
                table: "Buyers");

            migrationBuilder.DropColumn(
                name: "StateDoc",
                table: "Buyers");
        }
    }
}

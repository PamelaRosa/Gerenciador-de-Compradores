using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebMvcMySql.Migrations
{
    /// <inheritdoc />
    public partial class AddNewColumnsPersonType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BusinessName",
                table: "Buyers");

            migrationBuilder.CreateTable(
                name: "PersonType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonType", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Buyers_PersonTypeId",
                table: "Buyers",
                column: "PersonTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Buyers_PersonType_PersonTypeId",
                table: "Buyers",
                column: "PersonTypeId",
                principalTable: "PersonType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buyers_PersonType_PersonTypeId",
                table: "Buyers");

            migrationBuilder.DropTable(
                name: "PersonType");

            migrationBuilder.DropIndex(
                name: "IX_Buyers_PersonTypeId",
                table: "Buyers");

            migrationBuilder.AddColumn<string>(
                name: "BusinessName",
                table: "Buyers",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}

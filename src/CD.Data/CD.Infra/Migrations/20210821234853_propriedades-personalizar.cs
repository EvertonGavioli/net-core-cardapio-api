using Microsoft.EntityFrameworkCore.Migrations;

namespace CD.Infra.Migrations
{
    public partial class propriedadespersonalizar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CorHex",
                table: "Estabelecimentos",
                type: "varchar(200)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "DarkMode",
                table: "Estabelecimentos",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Fonte",
                table: "Estabelecimentos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CorHex",
                table: "Estabelecimentos");

            migrationBuilder.DropColumn(
                name: "DarkMode",
                table: "Estabelecimentos");

            migrationBuilder.DropColumn(
                name: "Fonte",
                table: "Estabelecimentos");
        }
    }
}

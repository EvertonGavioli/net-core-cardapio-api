using Microsoft.EntityFrameworkCore.Migrations;

namespace CD.Infra.Migrations
{
    public partial class qrcodemediumtext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "QRCode",
                table: "Estabelecimentos",
                type: "mediumtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "QRCode",
                table: "Estabelecimentos",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "mediumtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}

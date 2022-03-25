using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FruitChips.Persistance.Identity.SqlData.Migrations
{
    public partial class ChangesAccessTokenType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AccessTokenHash",
                schema: "Auth",
                table: "UserToken",
                type: "Nvarchar(3000)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "Nvarchar(556)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AccessTokenHash",
                schema: "Auth",
                table: "UserToken",
                type: "Nvarchar(556)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "Nvarchar(3000)");
        }
    }
}

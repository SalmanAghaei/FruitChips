using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FruitChips.Persistance.SqlData.Migrations
{
    public partial class addUniqueSku : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Products_SKU",
                table: "Products",
                column: "SKU",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Products_SKU",
                table: "Products");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FruitChips.Persistance.SqlData.Migrations
{
    public partial class TAbleNameChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeatureDetail_Feature_FeatureId",
                table: "FeatureDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_FeatureProduct_Feature_FeaturesId",
                table: "FeatureProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FeatureDetail",
                table: "FeatureDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Feature",
                table: "Feature");

            migrationBuilder.RenameTable(
                name: "FeatureDetail",
                newName: "FeatureDetails");

            migrationBuilder.RenameTable(
                name: "Feature",
                newName: "Features");

            migrationBuilder.RenameIndex(
                name: "IX_FeatureDetail_FeatureId",
                table: "FeatureDetails",
                newName: "IX_FeatureDetails_FeatureId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FeatureDetails",
                table: "FeatureDetails",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Features",
                table: "Features",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FeatureDetails_Features_FeatureId",
                table: "FeatureDetails",
                column: "FeatureId",
                principalTable: "Features",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FeatureProduct_Features_FeaturesId",
                table: "FeatureProduct",
                column: "FeaturesId",
                principalTable: "Features",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeatureDetails_Features_FeatureId",
                table: "FeatureDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_FeatureProduct_Features_FeaturesId",
                table: "FeatureProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Features",
                table: "Features");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FeatureDetails",
                table: "FeatureDetails");

            migrationBuilder.RenameTable(
                name: "Features",
                newName: "Feature");

            migrationBuilder.RenameTable(
                name: "FeatureDetails",
                newName: "FeatureDetail");

            migrationBuilder.RenameIndex(
                name: "IX_FeatureDetails_FeatureId",
                table: "FeatureDetail",
                newName: "IX_FeatureDetail_FeatureId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Feature",
                table: "Feature",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FeatureDetail",
                table: "FeatureDetail",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FeatureDetail_Feature_FeatureId",
                table: "FeatureDetail",
                column: "FeatureId",
                principalTable: "Feature",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FeatureProduct_Feature_FeaturesId",
                table: "FeatureProduct",
                column: "FeaturesId",
                principalTable: "Feature",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

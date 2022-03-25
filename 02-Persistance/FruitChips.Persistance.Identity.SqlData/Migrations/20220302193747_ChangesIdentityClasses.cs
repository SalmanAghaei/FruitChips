using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FruitChips.Persistance.Identity.SqlData.Migrations
{
    public partial class ChangesIdentityClasses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "AccessTokenExpiresDateTime",
                schema: "Auth",
                table: "UserToken",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "AccessTokenHash",
                schema: "Auth",
                table: "UserToken",
                type: "Nvarchar(556)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                schema: "Auth",
                table: "User",
                type: "Nvarchar(256)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsSystemAdmin",
                schema: "Auth",
                table: "User",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                schema: "Auth",
                table: "User",
                type: "Nvarchar(256)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccessTokenExpiresDateTime",
                schema: "Auth",
                table: "UserToken");

            migrationBuilder.DropColumn(
                name: "AccessTokenHash",
                schema: "Auth",
                table: "UserToken");

            migrationBuilder.DropColumn(
                name: "FirstName",
                schema: "Auth",
                table: "User");

            migrationBuilder.DropColumn(
                name: "IsSystemAdmin",
                schema: "Auth",
                table: "User");

            migrationBuilder.DropColumn(
                name: "LastName",
                schema: "Auth",
                table: "User");
        }
    }
}

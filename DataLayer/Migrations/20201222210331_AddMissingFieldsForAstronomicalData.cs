using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.DataAccessLayer.Migrations
{
    public partial class AddMissingFieldsForAstronomicalData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "SunriseSunsets",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsProcessed",
                table: "SunriseSunsets",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "SunriseSunsets");

            migrationBuilder.DropColumn(
                name: "IsProcessed",
                table: "SunriseSunsets");
        }
    }
}

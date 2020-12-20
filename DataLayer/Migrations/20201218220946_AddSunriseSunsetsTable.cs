using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.DataAccessLayer.Migrations
{
    public partial class AddSunriseSunsetsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SunriseSunsets",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Sunrise = table.Column<DateTime>(nullable: false),
                    Sunset = table.Column<DateTime>(nullable: false),
                    SolarNoon = table.Column<DateTime>(nullable: false),
                    DayLength = table.Column<TimeSpan>(nullable: false),
                    CivilTwilightBegin = table.Column<DateTime>(nullable: false),
                    CivilTwilightEnd = table.Column<DateTime>(nullable: false),
                    NauticalTwilightBegin = table.Column<DateTime>(nullable: false),
                    NauticalTwilightEnd = table.Column<DateTime>(nullable: false),
                    AstronomicalTwilightBegin = table.Column<DateTime>(nullable: false),
                    AstronomicalTwilightEnd = table.Column<DateTime>(nullable: false),
                    Location = table.Column<string>(nullable: false),
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SunriseSunsets", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 1,
                column: "UpdatedOn",
                value: new DateTime(2020, 10, 31, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 2,
                column: "UpdatedOn",
                value: new DateTime(2020, 10, 31, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 3,
                column: "UpdatedOn",
                value: new DateTime(2020, 10, 31, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 4,
                column: "UpdatedOn",
                value: new DateTime(2020, 10, 31, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 5,
                column: "UpdatedOn",
                value: new DateTime(2020, 10, 31, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 6,
                column: "UpdatedOn",
                value: new DateTime(2020, 10, 31, 0, 0, 0, 0, DateTimeKind.Local));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SunriseSunsets");

            migrationBuilder.UpdateData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 1,
                column: "UpdatedOn",
                value: new DateTime(2020, 10, 31, 9, 52, 5, 267, DateTimeKind.Local).AddTicks(7247));

            migrationBuilder.UpdateData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 2,
                column: "UpdatedOn",
                value: new DateTime(2020, 10, 31, 9, 52, 5, 270, DateTimeKind.Local).AddTicks(2135));

            migrationBuilder.UpdateData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 3,
                column: "UpdatedOn",
                value: new DateTime(2020, 10, 31, 9, 52, 5, 270, DateTimeKind.Local).AddTicks(2231));

            migrationBuilder.UpdateData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 4,
                column: "UpdatedOn",
                value: new DateTime(2020, 10, 31, 9, 52, 5, 270, DateTimeKind.Local).AddTicks(2261));

            migrationBuilder.UpdateData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 5,
                column: "UpdatedOn",
                value: new DateTime(2020, 10, 31, 9, 52, 5, 270, DateTimeKind.Local).AddTicks(2289));

            migrationBuilder.UpdateData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 6,
                column: "UpdatedOn",
                value: new DateTime(2020, 10, 31, 9, 52, 5, 270, DateTimeKind.Local).AddTicks(2318));
        }
    }
}

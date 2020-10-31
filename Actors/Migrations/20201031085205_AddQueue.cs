using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Actors.Migrations
{
    public partial class AddQueue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Queue",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    MessageBody = table.Column<string>(nullable: true),
                    IsProcessed = table.Column<bool>(nullable: false),
                    Direction = table.Column<int>(nullable: false),
                    ActorId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Queue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Queue_Actors_ActorId",
                        column: x => x.ActorId,
                        principalTable: "Actors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Queue_ActorId",
                table: "Queue",
                column: "ActorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Queue");

            migrationBuilder.UpdateData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 1,
                column: "UpdatedOn",
                value: new DateTime(2020, 10, 29, 18, 25, 2, 199, DateTimeKind.Local).AddTicks(6451));

            migrationBuilder.UpdateData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 2,
                column: "UpdatedOn",
                value: new DateTime(2020, 10, 29, 18, 25, 2, 202, DateTimeKind.Local).AddTicks(1545));

            migrationBuilder.UpdateData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 3,
                column: "UpdatedOn",
                value: new DateTime(2020, 10, 29, 18, 25, 2, 202, DateTimeKind.Local).AddTicks(1644));

            migrationBuilder.UpdateData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 4,
                column: "UpdatedOn",
                value: new DateTime(2020, 10, 29, 18, 25, 2, 202, DateTimeKind.Local).AddTicks(1670));

            migrationBuilder.UpdateData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 5,
                column: "UpdatedOn",
                value: new DateTime(2020, 10, 29, 18, 25, 2, 202, DateTimeKind.Local).AddTicks(1694));

            migrationBuilder.UpdateData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: 6,
                column: "UpdatedOn",
                value: new DateTime(2020, 10, 29, 18, 25, 2, 202, DateTimeKind.Local).AddTicks(1720));
        }
    }
}

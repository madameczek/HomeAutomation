using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Actors.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Gateways",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    SiteName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gateways", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Actors",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Type = table.Column<string>(nullable: false),
                    GatewayId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Actors_Gateways_GatewayId",
                        column: x => x.GatewayId,
                        principalTable: "Gateways",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Configurations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UpdatedOn = table.Column<DateTimeOffset>(nullable: false),
                    ConfigurationJson = table.Column<string>(nullable: true),
                    ActorId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Configurations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Configurations_Actors_ActorId",
                        column: x => x.ActorId,
                        principalTable: "Actors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false),
                    MessageBodyJson = table.Column<string>(nullable: true),
                    IsProcessed = table.Column<bool>(nullable: false),
                    ActorId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_Actors_ActorId",
                        column: x => x.ActorId,
                        principalTable: "Actors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Gateways",
                columns: new[] { "Id", "Name", "SiteName" },
                values: new object[] { new Guid("4b77c5fd-9d06-4771-ac13-b7c79c72f85c"), "Raspberry Pi", null });

            migrationBuilder.InsertData(
                table: "Actors",
                columns: new[] { "Id", "GatewayId", "Type" },
                values: new object[,]
                {
                    { new Guid("f66394fb-4a24-4876-a5e2-1a1e2bdda432"), new Guid("4b77c5fd-9d06-4771-ac13-b7c79c72f85c"), "GsmModem" },
                    { new Guid("429060a5-7e97-4227-aa44-25999f13536f"), new Guid("4b77c5fd-9d06-4771-ac13-b7c79c72f85c"), "Relay" },
                    { new Guid("4cda556f-aeda-4c8e-a28e-5338363283c8"), new Guid("4b77c5fd-9d06-4771-ac13-b7c79c72f85c"), "Relay" },
                    { new Guid("dad5ba5d-e9af-4e54-9452-db90168b8de2"), new Guid("4b77c5fd-9d06-4771-ac13-b7c79c72f85c"), "TemperatureSensor" }
                });

            migrationBuilder.InsertData(
                table: "Configurations",
                columns: new[] { "Id", "ActorId", "ConfigurationJson", "UpdatedOn" },
                values: new object[,]
                {
                    { 1, new Guid("f66394fb-4a24-4876-a5e2-1a1e2bdda432"), "", new DateTimeOffset(new DateTime(2020, 10, 24, 1, 44, 36, 448, DateTimeKind.Unspecified).AddTicks(7171), new TimeSpan(0, 2, 0, 0, 0)) },
                    { 2, new Guid("429060a5-7e97-4227-aa44-25999f13536f"), "", new DateTimeOffset(new DateTime(2020, 10, 24, 1, 44, 36, 451, DateTimeKind.Unspecified).AddTicks(5277), new TimeSpan(0, 2, 0, 0, 0)) },
                    { 3, new Guid("4cda556f-aeda-4c8e-a28e-5338363283c8"), "", new DateTimeOffset(new DateTime(2020, 10, 24, 1, 44, 36, 451, DateTimeKind.Unspecified).AddTicks(5388), new TimeSpan(0, 2, 0, 0, 0)) },
                    { 4, new Guid("dad5ba5d-e9af-4e54-9452-db90168b8de2"), "{\"ProcessId\":2,\"DeviceId\":\"dad5ba5d-e9af-4e54-9452-db90168b8de2\",\"Interface\":\"wire-1\",\"Type\":\"TemperatureSensor\",\"Name\":\"TemperatureSensor\",\"BasePath\":\"/sys/bus/w1/devices/\",\"HWSerial\":\"28-0000005a5d8c\",\"ReadInterval\":5000,\"Attach\":true}", new DateTimeOffset(new DateTime(2020, 10, 24, 1, 44, 36, 451, DateTimeKind.Unspecified).AddTicks(5422), new TimeSpan(0, 2, 0, 0, 0)) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Actors_GatewayId",
                table: "Actors",
                column: "GatewayId");

            migrationBuilder.CreateIndex(
                name: "IX_Configurations_ActorId",
                table: "Configurations",
                column: "ActorId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ActorId",
                table: "Messages",
                column: "ActorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Configurations");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Actors");

            migrationBuilder.DropTable(
                name: "Gateways");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EventCatalogAPI.Migrations
{
    public partial class mig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "catalog_hilo3",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "Place_hilo1",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "Ticket_hilo1",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "User_hilo3",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "Place",
                columns: table => new
                {
                    PlaceId = table.Column<int>(nullable: false),
                    PlaceName = table.Column<string>(maxLength: 50, nullable: false),
                    Address = table.Column<string>(maxLength: 50, nullable: false),
                    City = table.Column<string>(maxLength: 50, nullable: false),
                    State = table.Column<string>(maxLength: 50, nullable: false),
                    ZipCode = table.Column<int>(maxLength: 50, nullable: false),
                    PlacePrice = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Place", x => x.PlaceId);
                });

            migrationBuilder.CreateTable(
                name: "Catalog",
                columns: table => new
                {
                    EventId = table.Column<int>(nullable: false),
                    EventName = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    PlaceId = table.Column<int>(nullable: false),
                    EventDate = table.Column<DateTime>(nullable: false),
                    EventPrice = table.Column<decimal>(nullable: false),
                    EventImageURL = table.Column<string>(nullable: false),
                    EventPriceType = table.Column<int>(nullable: false),
                    EventCategory = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Catalog", x => x.EventId);
                    table.ForeignKey(
                        name: "FK_Catalog_Place_PlaceId",
                        column: x => x.PlaceId,
                        principalTable: "Place",
                        principalColumn: "PlaceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ticket",
                columns: table => new
                {
                    TicketId = table.Column<int>(nullable: false),
                    EventId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ticket", x => x.TicketId);
                    table.ForeignKey(
                        name: "FK_Ticket_Catalog_EventId",
                        column: x => x.EventId,
                        principalTable: "Catalog",
                        principalColumn: "EventId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Email = table.Column<string>(maxLength: 50, nullable: false),
                    Password = table.Column<string>(maxLength: 50, nullable: false),
                    BillingAddress = table.Column<string>(maxLength: 100, nullable: false),
                    CreditCardNo = table.Column<int>(nullable: false),
                    EventId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_User_Catalog_EventId",
                        column: x => x.EventId,
                        principalTable: "Catalog",
                        principalColumn: "EventId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Catalog_PlaceId",
                table: "Catalog",
                column: "PlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_EventId",
                table: "Ticket",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_User_EventId",
                table: "User",
                column: "EventId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ticket");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Catalog");

            migrationBuilder.DropTable(
                name: "Place");

            migrationBuilder.DropSequence(
                name: "catalog_hilo3");

            migrationBuilder.DropSequence(
                name: "Place_hilo1");

            migrationBuilder.DropSequence(
                name: "Ticket_hilo1");

            migrationBuilder.DropSequence(
                name: "User_hilo3");
        }
    }
}

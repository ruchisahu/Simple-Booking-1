using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OrderProcessorAPI.Migrations
{
    public partial class AddedEventDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TotalAmount",
                table: "Ticket",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<DateTime>(
                name: "EventDate",
                table: "Event",
                nullable: false,
                defaultValue: new DateTime(2018, 7, 29, 22, 21, 17, 472, DateTimeKind.Local));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventDate",
                table: "Event");

            migrationBuilder.AlterColumn<int>(
                name: "TotalAmount",
                table: "Ticket",
                nullable: false,
                oldClrType: typeof(decimal));
        }
    }
}

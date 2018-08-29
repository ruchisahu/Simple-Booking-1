using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OrderProcessorAPI.Migrations
{
    public partial class RemovedCreditCardDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CSC",
                table: "User");

            migrationBuilder.DropColumn(
                name: "CreditCardNo",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "ExpirationDate",
                table: "User",
                newName: "BuyerId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EventDate",
                table: "Event",
                nullable: false,
                defaultValue: new DateTime(2018, 8, 28, 22, 48, 12, 758, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 7, 30, 22, 18, 29, 205, DateTimeKind.Local));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BuyerId",
                table: "User",
                newName: "ExpirationDate");

            migrationBuilder.AddColumn<int>(
                name: "CSC",
                table: "User",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "CreditCardNo",
                table: "User",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EventDate",
                table: "Event",
                nullable: false,
                defaultValue: new DateTime(2018, 7, 30, 22, 18, 29, 205, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 8, 28, 22, 48, 12, 758, DateTimeKind.Local));
        }
    }
}

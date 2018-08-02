using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OrderProcessorAPI.Migrations
{
    public partial class AddedDbSetForTransactions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Ticket_TicketId",
                table: "Transaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transaction",
                table: "Transaction");

            migrationBuilder.RenameTable(
                name: "Transaction",
                newName: "Transactions");

            migrationBuilder.RenameIndex(
                name: "IX_Transaction_TicketId",
                table: "Transactions",
                newName: "IX_Transactions_TicketId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EventDate",
                table: "Event",
                nullable: false,
                defaultValue: new DateTime(2018, 7, 30, 19, 7, 5, 974, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 7, 30, 12, 11, 3, 253, DateTimeKind.Local));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Ticket_TicketId",
                table: "Transactions",
                column: "TicketId",
                principalTable: "Ticket",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Ticket_TicketId",
                table: "Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions");

            migrationBuilder.RenameTable(
                name: "Transactions",
                newName: "Transaction");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_TicketId",
                table: "Transaction",
                newName: "IX_Transaction_TicketId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EventDate",
                table: "Event",
                nullable: false,
                defaultValue: new DateTime(2018, 7, 30, 12, 11, 3, 253, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 7, 30, 19, 7, 5, 974, DateTimeKind.Local));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transaction",
                table: "Transaction",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Ticket_TicketId",
                table: "Transaction",
                column: "TicketId",
                principalTable: "Ticket",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

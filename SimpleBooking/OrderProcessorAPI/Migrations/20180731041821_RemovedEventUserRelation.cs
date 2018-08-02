using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OrderProcessorAPI.Migrations
{
    public partial class RemovedEventUserRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Event_EventId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_EventId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "User");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EventDate",
                table: "Event",
                nullable: false,
                defaultValue: new DateTime(2018, 7, 30, 21, 18, 20, 532, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 7, 30, 19, 11, 51, 437, DateTimeKind.Local));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EventId",
                table: "User",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EventDate",
                table: "Event",
                nullable: false,
                defaultValue: new DateTime(2018, 7, 30, 19, 11, 51, 437, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 7, 30, 21, 18, 20, 532, DateTimeKind.Local));

            migrationBuilder.CreateIndex(
                name: "IX_User_EventId",
                table: "User",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Event_EventId",
                table: "User",
                column: "EventId",
                principalTable: "Event",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

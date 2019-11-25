using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JMS.API.Migrations
{
    public partial class AddDispatcher : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Journey",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "DispatcherId",
                table: "Journey",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Journey_DispatcherId",
                table: "Journey",
                column: "DispatcherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Journey_Users_DispatcherId",
                table: "Journey",
                column: "DispatcherId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Journey_Users_DispatcherId",
                table: "Journey");

            migrationBuilder.DropIndex(
                name: "IX_Journey_DispatcherId",
                table: "Journey");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Journey");

            migrationBuilder.DropColumn(
                name: "DispatcherId",
                table: "Journey");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JMS.API.Migrations
{
    public partial class isinprogress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsInProgress",
                table: "Users",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastCompletionTrip",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsInProgress",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastCompletionTrip",
                table: "Users");
        }
    }
}

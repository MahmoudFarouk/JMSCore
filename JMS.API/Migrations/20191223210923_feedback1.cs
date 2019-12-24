using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JMS.API.Migrations
{
    public partial class feedback1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notification_Users_UserId",
                table: "Notification");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Notification",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<int>(
                name: "JourneyId",
                table: "Notification",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NotificationType",
                table: "Notification",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "Notification",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsNight",
                table: "Journey",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RiskStatus",
                table: "Journey",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_Users_UserId",
                table: "Notification",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notification_Users_UserId",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "JourneyId",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "NotificationType",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "IsNight",
                table: "Journey");

            migrationBuilder.DropColumn(
                name: "RiskStatus",
                table: "Journey");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Notification",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_Users_UserId",
                table: "Notification",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

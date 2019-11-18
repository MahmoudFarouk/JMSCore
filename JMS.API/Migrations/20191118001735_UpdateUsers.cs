using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JMS.API.Migrations
{
    public partial class UpdateUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAdctive",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Lat",
                table: "Checkpoint");

            migrationBuilder.DropColumn(
                name: "Lng",
                table: "Checkpoint");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Users",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<double>(
                name: "Longitude",
                table: "JourneyUpdate",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Latitude",
                table: "JourneyUpdate",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "JourneyUpdate",
                nullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "ToLng",
                table: "Journey",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "ToLat",
                table: "Journey",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "FromLng",
                table: "Journey",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "FromLat",
                table: "Journey",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Checkpoint",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "Checkpoint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_JourneyUpdate_UserId",
                table: "JourneyUpdate",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_JourneyUpdate_Users_UserId",
                table: "JourneyUpdate",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JourneyUpdate_Users_UserId",
                table: "JourneyUpdate");

            migrationBuilder.DropIndex(
                name: "IX_JourneyUpdate_UserId",
                table: "JourneyUpdate");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "JourneyUpdate");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Checkpoint");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Checkpoint");

            migrationBuilder.AddColumn<bool>(
                name: "IsAdctive",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "Longitude",
                table: "JourneyUpdate",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Latitude",
                table: "JourneyUpdate",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ToLng",
                table: "Journey",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ToLat",
                table: "Journey",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FromLng",
                table: "Journey",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FromLat",
                table: "Journey",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Lat",
                table: "Checkpoint",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Lng",
                table: "Checkpoint",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

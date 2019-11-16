using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JMS.API.Migrations
{
    public partial class enhaceAssessmentResult : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssessmentResult_Users_SubmittedByUserId",
                table: "AssessmentResult");

            migrationBuilder.DropIndex(
                name: "IX_AssessmentResult_SubmittedByUserId",
                table: "AssessmentResult");

            migrationBuilder.DropColumn(
                name: "SubmittedBy",
                table: "AssessmentResult");

            migrationBuilder.DropColumn(
                name: "SubmittedByUserId",
                table: "AssessmentResult");

            migrationBuilder.AlterColumn<string>(
                name: "VehicleNo",
                table: "JourneyUpdate",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "AssessmentResult",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentResult_UserId",
                table: "AssessmentResult",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssessmentResult_Users_UserId",
                table: "AssessmentResult",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssessmentResult_Users_UserId",
                table: "AssessmentResult");

            migrationBuilder.DropIndex(
                name: "IX_AssessmentResult_UserId",
                table: "AssessmentResult");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AssessmentResult");

            migrationBuilder.AlterColumn<int>(
                name: "VehicleNo",
                table: "JourneyUpdate",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SubmittedBy",
                table: "AssessmentResult",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SubmittedByUserId",
                table: "AssessmentResult",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentResult_SubmittedByUserId",
                table: "AssessmentResult",
                column: "SubmittedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssessmentResult_Users_SubmittedByUserId",
                table: "AssessmentResult",
                column: "SubmittedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

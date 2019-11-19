using Microsoft.EntityFrameworkCore.Migrations;

namespace JMS.API.Migrations
{
    public partial class UpdateAssessment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "JourneyId",
                table: "AssessmentQuestion",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentQuestion_JourneyId",
                table: "AssessmentQuestion",
                column: "JourneyId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssessmentQuestion_Journey_JourneyId",
                table: "AssessmentQuestion",
                column: "JourneyId",
                principalTable: "Journey",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssessmentQuestion_Journey_JourneyId",
                table: "AssessmentQuestion");

            migrationBuilder.DropIndex(
                name: "IX_AssessmentQuestion_JourneyId",
                table: "AssessmentQuestion");

            migrationBuilder.DropColumn(
                name: "JourneyId",
                table: "AssessmentQuestion");
        }
    }
}

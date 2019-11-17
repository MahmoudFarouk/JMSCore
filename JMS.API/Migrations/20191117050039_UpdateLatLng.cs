using Microsoft.EntityFrameworkCore.Migrations;

namespace JMS.API.Migrations
{
    public partial class UpdateLatLng : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Lat",
                table: "Checkpoint");

            migrationBuilder.DropColumn(
                name: "Lng",
                table: "Checkpoint");

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Checkpoint",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "Checkpoint",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Checkpoint");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Checkpoint");

            migrationBuilder.AddColumn<double>(
                name: "Lat",
                table: "Checkpoint",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Lng",
                table: "Checkpoint",
                type: "float",
                nullable: true);
        }
    }
}

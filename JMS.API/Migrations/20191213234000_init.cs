using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JMS.API.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Checkpoint",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Latitude = table.Column<double>(nullable: true),
                    Longitude = table.Column<double>(nullable: true),
                    IsThirdParty = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Checkpoint", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CodeException",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ErrorMessage = table.Column<string>(nullable: true),
                    ExceptionTime = table.Column<DateTime>(nullable: true),
                    MachineName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    AssemblyName = table.Column<string>(nullable: true),
                    AssemblyVersion = table.Column<string>(nullable: true),
                    ClassName = table.Column<string>(nullable: true),
                    MethodName = table.Column<string>(nullable: true),
                    StackDump = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CodeException", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserWorkForces",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserWorkForces", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Username = table.Column<string>(nullable: false),
                    FullName = table.Column<string>(maxLength: 256, nullable: true),
                    UserGroupId = table.Column<Guid>(nullable: true),
                    UserWorkForceId = table.Column<Guid>(nullable: true),
                    LicenseNo = table.Column<string>(maxLength: 256, nullable: true),
                    LicenseExpiryDate = table.Column<DateTime>(nullable: true),
                    TrainingDetails = table.Column<string>(nullable: true),
                    GatePassStatus = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<byte[]>(nullable: true),
                    PasswordSalt = table.Column<byte[]>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    ChangePasswordToken = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_UserGroups_UserGroupId",
                        column: x => x.UserGroupId,
                        principalTable: "UserGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_UserWorkForces_UserWorkForceId",
                        column: x => x.UserWorkForceId,
                        principalTable: "UserWorkForces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Journey",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    IsTruckTransport = table.Column<bool>(nullable: false),
                    JourneyStatus = table.Column<int>(nullable: false),
                    FromDestination = table.Column<string>(nullable: true),
                    FromLat = table.Column<double>(nullable: true),
                    FromLng = table.Column<double>(nullable: true),
                    ToDestination = table.Column<string>(nullable: true),
                    ToLat = table.Column<double>(nullable: true),
                    ToLng = table.Column<double>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: true),
                    DeliveryDate = table.Column<DateTime>(nullable: true),
                    CargoWeight = table.Column<double>(nullable: true),
                    CargoPriority = table.Column<int>(nullable: false),
                    CargoSeverity = table.Column<int>(nullable: false),
                    CargoType = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false),
                    IsThirdParty = table.Column<bool>(nullable: false),
                    DispatcherId = table.Column<Guid>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Journey", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Journey_Users_DispatcherId",
                        column: x => x.DispatcherId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Journey_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(nullable: true),
                    IsRead = table.Column<bool>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notification_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssessmentQuestion",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Question = table.Column<string>(nullable: true),
                    Category = table.Column<int>(nullable: false),
                    Weight = table.Column<int>(nullable: true),
                    IsThirdParty = table.Column<bool>(nullable: false),
                    CheckpointId = table.Column<int>(nullable: true),
                    JourneyId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssessmentQuestion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssessmentQuestion_Checkpoint_CheckpointId",
                        column: x => x.CheckpointId,
                        principalTable: "Checkpoint",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssessmentQuestion_Journey_JourneyId",
                        column: x => x.JourneyId,
                        principalTable: "Journey",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "JourneyUpdate",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: true),
                    JourneyId = table.Column<int>(nullable: true),
                    JourneyStatus = table.Column<int>(nullable: false),
                    VehicleNo = table.Column<string>(nullable: true),
                    DriverId = table.Column<Guid>(nullable: true),
                    IsJourneyCheckpoint = table.Column<bool>(nullable: false),
                    CheckpointId = table.Column<int>(nullable: true),
                    RiskLevel = table.Column<int>(nullable: false),
                    Latitude = table.Column<double>(nullable: true),
                    Longitude = table.Column<double>(nullable: true),
                    IsDriverStatus = table.Column<bool>(nullable: false),
                    IsAlert = table.Column<bool>(nullable: false),
                    StatusMessage = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JourneyUpdate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JourneyUpdate_Checkpoint_CheckpointId",
                        column: x => x.CheckpointId,
                        principalTable: "Checkpoint",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JourneyUpdate_Journey_JourneyId",
                        column: x => x.JourneyId,
                        principalTable: "Journey",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JourneyUpdate_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AssessmentResult",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionId = table.Column<int>(nullable: true),
                    IsYes = table.Column<bool>(nullable: false),
                    Comment = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false),
                    VehicleNo = table.Column<int>(nullable: true),
                    JourneyUpdateId = table.Column<int>(nullable: true),
                    Category = table.Column<int>(nullable: true),
                    CheckPointId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssessmentResult", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssessmentResult_JourneyUpdate_JourneyUpdateId",
                        column: x => x.JourneyUpdateId,
                        principalTable: "JourneyUpdate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssessmentResult_AssessmentQuestion_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "AssessmentQuestion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssessmentResult_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentQuestion_CheckpointId",
                table: "AssessmentQuestion",
                column: "CheckpointId");

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentQuestion_JourneyId",
                table: "AssessmentQuestion",
                column: "JourneyId");

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentResult_JourneyUpdateId",
                table: "AssessmentResult",
                column: "JourneyUpdateId");

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentResult_QuestionId",
                table: "AssessmentResult",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentResult_UserId",
                table: "AssessmentResult",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Journey_DispatcherId",
                table: "Journey",
                column: "DispatcherId");

            migrationBuilder.CreateIndex(
                name: "IX_Journey_UserId",
                table: "Journey",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_JourneyUpdate_CheckpointId",
                table: "JourneyUpdate",
                column: "CheckpointId");

            migrationBuilder.CreateIndex(
                name: "IX_JourneyUpdate_JourneyId",
                table: "JourneyUpdate",
                column: "JourneyId");

            migrationBuilder.CreateIndex(
                name: "IX_JourneyUpdate_UserId",
                table: "JourneyUpdate",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_UserId",
                table: "Notification",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserId",
                table: "UserRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserGroupId",
                table: "Users",
                column: "UserGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserWorkForceId",
                table: "Users",
                column: "UserWorkForceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssessmentResult");

            migrationBuilder.DropTable(
                name: "CodeException");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "JourneyUpdate");

            migrationBuilder.DropTable(
                name: "AssessmentQuestion");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Checkpoint");

            migrationBuilder.DropTable(
                name: "Journey");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "UserGroups");

            migrationBuilder.DropTable(
                name: "UserWorkForces");
        }
    }
}

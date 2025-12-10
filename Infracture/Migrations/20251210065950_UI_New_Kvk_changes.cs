using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UI_New_Kvk_changes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReportingVideo",
                table: "KvkReports");

            migrationBuilder.DropColumn(
                name: "ReportingYear",
                table: "KvkReports");

            migrationBuilder.AddColumn<string>(
                name: "UploadVideoUrl",
                table: "KvkRecommendations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NoOfChecks",
                table: "KvkProgramDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NoOfDemos",
                table: "KvkProgramDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NoOfTrails",
                table: "KvkProgramDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NoOfVisits",
                table: "KvkProgramDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ParticipantFileUpload",
                table: "KvkProgramDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ParticipatedAsId",
                table: "KvkProgramDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StageOfCrop",
                table: "KvkProgramDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "T01",
                table: "KvkProgramDetails",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "T02",
                table: "KvkProgramDetails",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "T03",
                table: "KvkProgramDetails",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "T04",
                table: "KvkProgramDetails",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "T05",
                table: "KvkProgramDetails",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "KvkCriticalInputsDistributed",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KvkAdvisoryServicesId = table.Column<int>(type: "int", nullable: false),
                    InputName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    QuantityDistributed = table.Column<int>(type: "int", nullable: true),
                    NoOfRecipients = table.Column<int>(type: "int", nullable: true),
                    UnitLocationId = table.Column<int>(type: "int", nullable: true),
                    OrganizationId = table.Column<int>(type: "int", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KvkCriticalInputsDistributed", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KvkCriticalInputsDistributed_KvkAdvisoryServices_KvkAdvisoryServicesId",
                        column: x => x.KvkAdvisoryServicesId,
                        principalTable: "KvkAdvisoryServices",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_KvkCriticalInputsDistributed_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_KvkCriticalInputsDistributed_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "KvkFarmerScientistInteraction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KvkProgramContentAndResourcesId = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<DateOnly>(type: "date", nullable: true),
                    ScientistOfficerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TopicDiscussed = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoOfFarmersParticipated = table.Column<int>(type: "int", nullable: true),
                    PhotoUpload = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnitLocationId = table.Column<int>(type: "int", nullable: true),
                    OrganizationId = table.Column<int>(type: "int", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KvkFarmerScientistInteraction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KvkFarmerScientistInteraction_KvkProgramContentAndResources_KvkProgramContentAndResourcesId",
                        column: x => x.KvkProgramContentAndResourcesId,
                        principalTable: "KvkProgramContentAndResources",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_KvkFarmerScientistInteraction_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_KvkFarmerScientistInteraction_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "KvkFieldDay",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KvkProgramContentAndResourcesId = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<DateOnly>(type: "date", nullable: true),
                    FarmerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Place = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoOfBeneficieries = table.Column<int>(type: "int", nullable: true),
                    UnitLocationId = table.Column<int>(type: "int", nullable: true),
                    OrganizationId = table.Column<int>(type: "int", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KvkFieldDay", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KvkFieldDay_KvkProgramContentAndResources_KvkProgramContentAndResourcesId",
                        column: x => x.KvkProgramContentAndResourcesId,
                        principalTable: "KvkProgramContentAndResources",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_KvkFieldDay_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_KvkFieldDay_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "KvkFieldVisit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KvkProgramContentAndResourcesId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: true),
                    ScientistOfficerVisitedName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Purpose = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoOfFieldsCovered = table.Column<int>(type: "int", nullable: true),
                    NoOfFarmerCovered = table.Column<int>(type: "int", nullable: true),
                    PhotoUpload = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnitLocationId = table.Column<int>(type: "int", nullable: true),
                    OrganizationId = table.Column<int>(type: "int", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KvkFieldVisit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KvkFieldVisit_KvkProgramContentAndResources_KvkProgramContentAndResourcesId",
                        column: x => x.KvkProgramContentAndResourcesId,
                        principalTable: "KvkProgramContentAndResources",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_KvkFieldVisit_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_KvkFieldVisit_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_KvkProgramDetails_ParticipatedAsId",
                table: "KvkProgramDetails",
                column: "ParticipatedAsId");

            migrationBuilder.CreateIndex(
                name: "IX_KvkCriticalInputsDistributed_CreatedById",
                table: "KvkCriticalInputsDistributed",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_KvkCriticalInputsDistributed_KvkAdvisoryServicesId",
                table: "KvkCriticalInputsDistributed",
                column: "KvkAdvisoryServicesId");

            migrationBuilder.CreateIndex(
                name: "IX_KvkCriticalInputsDistributed_UpdatedById",
                table: "KvkCriticalInputsDistributed",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_KvkFarmerScientistInteraction_CreatedById",
                table: "KvkFarmerScientistInteraction",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_KvkFarmerScientistInteraction_KvkProgramContentAndResourcesId",
                table: "KvkFarmerScientistInteraction",
                column: "KvkProgramContentAndResourcesId");

            migrationBuilder.CreateIndex(
                name: "IX_KvkFarmerScientistInteraction_UpdatedById",
                table: "KvkFarmerScientistInteraction",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_KvkFieldDay_CreatedById",
                table: "KvkFieldDay",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_KvkFieldDay_KvkProgramContentAndResourcesId",
                table: "KvkFieldDay",
                column: "KvkProgramContentAndResourcesId");

            migrationBuilder.CreateIndex(
                name: "IX_KvkFieldDay_UpdatedById",
                table: "KvkFieldDay",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_KvkFieldVisit_CreatedById",
                table: "KvkFieldVisit",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_KvkFieldVisit_KvkProgramContentAndResourcesId",
                table: "KvkFieldVisit",
                column: "KvkProgramContentAndResourcesId");

            migrationBuilder.CreateIndex(
                name: "IX_KvkFieldVisit_UpdatedById",
                table: "KvkFieldVisit",
                column: "UpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_KvkProgramDetails_Participants_ParticipatedAsId",
                table: "KvkProgramDetails",
                column: "ParticipatedAsId",
                principalTable: "Participants",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KvkProgramDetails_Participants_ParticipatedAsId",
                table: "KvkProgramDetails");

            migrationBuilder.DropTable(
                name: "KvkCriticalInputsDistributed");

            migrationBuilder.DropTable(
                name: "KvkFarmerScientistInteraction");

            migrationBuilder.DropTable(
                name: "KvkFieldDay");

            migrationBuilder.DropTable(
                name: "KvkFieldVisit");

            migrationBuilder.DropIndex(
                name: "IX_KvkProgramDetails_ParticipatedAsId",
                table: "KvkProgramDetails");

            migrationBuilder.DropColumn(
                name: "UploadVideoUrl",
                table: "KvkRecommendations");

            migrationBuilder.DropColumn(
                name: "NoOfChecks",
                table: "KvkProgramDetails");

            migrationBuilder.DropColumn(
                name: "NoOfDemos",
                table: "KvkProgramDetails");

            migrationBuilder.DropColumn(
                name: "NoOfTrails",
                table: "KvkProgramDetails");

            migrationBuilder.DropColumn(
                name: "NoOfVisits",
                table: "KvkProgramDetails");

            migrationBuilder.DropColumn(
                name: "ParticipantFileUpload",
                table: "KvkProgramDetails");

            migrationBuilder.DropColumn(
                name: "ParticipatedAsId",
                table: "KvkProgramDetails");

            migrationBuilder.DropColumn(
                name: "StageOfCrop",
                table: "KvkProgramDetails");

            migrationBuilder.DropColumn(
                name: "T01",
                table: "KvkProgramDetails");

            migrationBuilder.DropColumn(
                name: "T02",
                table: "KvkProgramDetails");

            migrationBuilder.DropColumn(
                name: "T03",
                table: "KvkProgramDetails");

            migrationBuilder.DropColumn(
                name: "T04",
                table: "KvkProgramDetails");

            migrationBuilder.DropColumn(
                name: "T05",
                table: "KvkProgramDetails");

            migrationBuilder.AddColumn<string>(
                name: "ReportingVideo",
                table: "KvkReports",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReportingYear",
                table: "KvkReports",
                type: "nvarchar(4)",
                maxLength: 4,
                nullable: true);
        }
    }
}

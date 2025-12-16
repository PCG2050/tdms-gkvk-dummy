using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EEU_changes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EeuFLDs");

            migrationBuilder.DropTable(
                name: "EeuOFTs");

            migrationBuilder.DropTable(
                name: "EeuOtherActivities");

            migrationBuilder.DropTable(
                name: "EeuTrainingProgrammes");

            migrationBuilder.AddColumn<string>(
                name: "UploadVideoUrl",
                table: "StuRecommendations",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UploadVideoUrl",
                table: "SametiRecommendations",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UploadVideoUrl",
                table: "NaepRecommendations",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UploadVideoUrl",
                table: "IbtvaRecommendations",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UploadVideoUrl",
                table: "FtiRecommendations",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UploadVideoUrl",
                table: "EeuRecommendations",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NoOfChecks",
                table: "EeuProgramDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NoOfDemos",
                table: "EeuProgramDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NoOfTrails",
                table: "EeuProgramDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NoOfVisits",
                table: "EeuProgramDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ParticipantFileUpload",
                table: "EeuProgramDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ParticipatedAsId",
                table: "EeuProgramDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StageOfCrop",
                table: "EeuProgramDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "T01",
                table: "EeuProgramDetails",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "T02",
                table: "EeuProgramDetails",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "T03",
                table: "EeuProgramDetails",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "T04",
                table: "EeuProgramDetails",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "T05",
                table: "EeuProgramDetails",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UploadVideoUrl",
                table: "DeuRecommendations",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UploadVideoUrl",
                table: "AticRecommendations",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EeuCriticalInputsDistributed",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EeuAdvisoryServicesId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_EeuCriticalInputsDistributed", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EeuCriticalInputsDistributed_EeuAdvisoryServices_EeuAdvisoryServicesId",
                        column: x => x.EeuAdvisoryServicesId,
                        principalTable: "EeuAdvisoryServices",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EeuCriticalInputsDistributed_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EeuCriticalInputsDistributed_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EeuFarmerScientistInteraction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EeuProgramContentAndResourcesId = table.Column<int>(type: "int", nullable: true),
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
                    table.PrimaryKey("PK_EeuFarmerScientistInteraction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EeuFarmerScientistInteraction_EeuProgramContentAndResources_EeuProgramContentAndResourcesId",
                        column: x => x.EeuProgramContentAndResourcesId,
                        principalTable: "EeuProgramContentAndResources",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EeuFarmerScientistInteraction_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EeuFarmerScientistInteraction_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EeuFieldDay",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EeuProgramContentAndResourcesId = table.Column<int>(type: "int", nullable: true),
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
                    table.PrimaryKey("PK_EeuFieldDay", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EeuFieldDay_EeuProgramContentAndResources_EeuProgramContentAndResourcesId",
                        column: x => x.EeuProgramContentAndResourcesId,
                        principalTable: "EeuProgramContentAndResources",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EeuFieldDay_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EeuFieldDay_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EeuFieldVisit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EeuProgramContentAndResourcesId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_EeuFieldVisit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EeuFieldVisit_EeuProgramContentAndResources_EeuProgramContentAndResourcesId",
                        column: x => x.EeuProgramContentAndResourcesId,
                        principalTable: "EeuProgramContentAndResources",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EeuFieldVisit_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EeuFieldVisit_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EeuResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EeuProgramDetailsId = table.Column<int>(type: "int", nullable: false),
                    UploadExcelUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnitLocationId = table.Column<int>(type: "int", nullable: true),
                    OrganizationId = table.Column<int>(type: "int", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EeuResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EeuResults_EeuProgramDetails_EeuProgramDetailsId",
                        column: x => x.EeuProgramDetailsId,
                        principalTable: "EeuProgramDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EeuResults_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EeuResults_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EeuFLDResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EeuResultId = table.Column<int>(type: "int", nullable: false),
                    DetailsOfDemoId = table.Column<int>(type: "int", nullable: false),
                    FldNumber = table.Column<int>(type: "int", nullable: false),
                    Parameter1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Observation1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Parameter2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Observation2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Parameter3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Observation3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Parameter4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Observation4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Parameter5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Observation5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Yield = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    GrossCost = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    GrossReturns = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    NetReturns = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    BC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnitLocationId = table.Column<int>(type: "int", nullable: true),
                    OrganizationId = table.Column<int>(type: "int", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EeuFLDResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EeuFLDResults_EeuResults_EeuResultId",
                        column: x => x.EeuResultId,
                        principalTable: "EeuResults",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EeuFLDResults_FLDResults_DetailsOfDemoId",
                        column: x => x.DetailsOfDemoId,
                        principalTable: "FLDResults",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EeuFLDResults_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EeuFLDResults_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EeuOFTResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EeuResultId = table.Column<int>(type: "int", nullable: false),
                    DetailsOfDemoId = table.Column<int>(type: "int", nullable: false),
                    Parameter1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Observation1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Parameter2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Observation2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Parameter3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Observation3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Parameter4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Observation4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Parameter5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Observation5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Yield = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    GrossCost = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    GrossReturns = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    NetReturns = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    BC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EeuOFTResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EeuOFTResults_EeuResults_EeuResultId",
                        column: x => x.EeuResultId,
                        principalTable: "EeuResults",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EeuOFTResults_OFTResults_DetailsOfDemoId",
                        column: x => x.DetailsOfDemoId,
                        principalTable: "OFTResults",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EeuOFTResults_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EeuOFTResults_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EeuProgramDetails_ParticipatedAsId",
                table: "EeuProgramDetails",
                column: "ParticipatedAsId");

            migrationBuilder.CreateIndex(
                name: "IX_EeuCriticalInputsDistributed_CreatedById",
                table: "EeuCriticalInputsDistributed",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EeuCriticalInputsDistributed_EeuAdvisoryServicesId",
                table: "EeuCriticalInputsDistributed",
                column: "EeuAdvisoryServicesId");

            migrationBuilder.CreateIndex(
                name: "IX_EeuCriticalInputsDistributed_UpdatedById",
                table: "EeuCriticalInputsDistributed",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EeuFarmerScientistInteraction_CreatedById",
                table: "EeuFarmerScientistInteraction",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EeuFarmerScientistInteraction_EeuProgramContentAndResourcesId",
                table: "EeuFarmerScientistInteraction",
                column: "EeuProgramContentAndResourcesId");

            migrationBuilder.CreateIndex(
                name: "IX_EeuFarmerScientistInteraction_UpdatedById",
                table: "EeuFarmerScientistInteraction",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EeuFieldDay_CreatedById",
                table: "EeuFieldDay",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EeuFieldDay_EeuProgramContentAndResourcesId",
                table: "EeuFieldDay",
                column: "EeuProgramContentAndResourcesId");

            migrationBuilder.CreateIndex(
                name: "IX_EeuFieldDay_UpdatedById",
                table: "EeuFieldDay",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EeuFieldVisit_CreatedById",
                table: "EeuFieldVisit",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EeuFieldVisit_EeuProgramContentAndResourcesId",
                table: "EeuFieldVisit",
                column: "EeuProgramContentAndResourcesId");

            migrationBuilder.CreateIndex(
                name: "IX_EeuFieldVisit_UpdatedById",
                table: "EeuFieldVisit",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EeuFLDResults_CreatedById",
                table: "EeuFLDResults",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EeuFLDResults_DetailsOfDemoId",
                table: "EeuFLDResults",
                column: "DetailsOfDemoId");

            migrationBuilder.CreateIndex(
                name: "IX_EeuFLDResults_EeuResultId",
                table: "EeuFLDResults",
                column: "EeuResultId");

            migrationBuilder.CreateIndex(
                name: "IX_EeuFLDResults_UpdatedById",
                table: "EeuFLDResults",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EeuOFTResults_CreatedById",
                table: "EeuOFTResults",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EeuOFTResults_DetailsOfDemoId",
                table: "EeuOFTResults",
                column: "DetailsOfDemoId");

            migrationBuilder.CreateIndex(
                name: "IX_EeuOFTResults_EeuResultId",
                table: "EeuOFTResults",
                column: "EeuResultId");

            migrationBuilder.CreateIndex(
                name: "IX_EeuOFTResults_UpdatedById",
                table: "EeuOFTResults",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EeuResults_CreatedById",
                table: "EeuResults",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EeuResults_EeuProgramDetailsId",
                table: "EeuResults",
                column: "EeuProgramDetailsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EeuResults_UpdatedById",
                table: "EeuResults",
                column: "UpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_EeuProgramDetails_Participants_ParticipatedAsId",
                table: "EeuProgramDetails",
                column: "ParticipatedAsId",
                principalTable: "Participants",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EeuProgramDetails_Participants_ParticipatedAsId",
                table: "EeuProgramDetails");

            migrationBuilder.DropTable(
                name: "EeuCriticalInputsDistributed");

            migrationBuilder.DropTable(
                name: "EeuFarmerScientistInteraction");

            migrationBuilder.DropTable(
                name: "EeuFieldDay");

            migrationBuilder.DropTable(
                name: "EeuFieldVisit");

            migrationBuilder.DropTable(
                name: "EeuFLDResults");

            migrationBuilder.DropTable(
                name: "EeuOFTResults");

            migrationBuilder.DropTable(
                name: "EeuResults");

            migrationBuilder.DropIndex(
                name: "IX_EeuProgramDetails_ParticipatedAsId",
                table: "EeuProgramDetails");

            migrationBuilder.DropColumn(
                name: "UploadVideoUrl",
                table: "StuRecommendations");

            migrationBuilder.DropColumn(
                name: "UploadVideoUrl",
                table: "SametiRecommendations");

            migrationBuilder.DropColumn(
                name: "UploadVideoUrl",
                table: "NaepRecommendations");

            migrationBuilder.DropColumn(
                name: "UploadVideoUrl",
                table: "IbtvaRecommendations");

            migrationBuilder.DropColumn(
                name: "UploadVideoUrl",
                table: "FtiRecommendations");

            migrationBuilder.DropColumn(
                name: "UploadVideoUrl",
                table: "EeuRecommendations");

            migrationBuilder.DropColumn(
                name: "NoOfChecks",
                table: "EeuProgramDetails");

            migrationBuilder.DropColumn(
                name: "NoOfDemos",
                table: "EeuProgramDetails");

            migrationBuilder.DropColumn(
                name: "NoOfTrails",
                table: "EeuProgramDetails");

            migrationBuilder.DropColumn(
                name: "NoOfVisits",
                table: "EeuProgramDetails");

            migrationBuilder.DropColumn(
                name: "ParticipantFileUpload",
                table: "EeuProgramDetails");

            migrationBuilder.DropColumn(
                name: "ParticipatedAsId",
                table: "EeuProgramDetails");

            migrationBuilder.DropColumn(
                name: "StageOfCrop",
                table: "EeuProgramDetails");

            migrationBuilder.DropColumn(
                name: "T01",
                table: "EeuProgramDetails");

            migrationBuilder.DropColumn(
                name: "T02",
                table: "EeuProgramDetails");

            migrationBuilder.DropColumn(
                name: "T03",
                table: "EeuProgramDetails");

            migrationBuilder.DropColumn(
                name: "T04",
                table: "EeuProgramDetails");

            migrationBuilder.DropColumn(
                name: "T05",
                table: "EeuProgramDetails");

            migrationBuilder.DropColumn(
                name: "UploadVideoUrl",
                table: "DeuRecommendations");

            migrationBuilder.DropColumn(
                name: "UploadVideoUrl",
                table: "AticRecommendations");

            migrationBuilder.CreateTable(
                name: "EeuFLDs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApprovedById = table.Column<int>(type: "int", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    OrganizationId = table.Column<int>(type: "int", nullable: false),
                    UnitLocationId = table.Column<int>(type: "int", nullable: false),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    ApprovedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Area = table.Column<double>(type: "float", nullable: false),
                    Attachements = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    Crop = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    FormStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FormStatusRemarks = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    PercentIncreaseInYield = table.Column<double>(type: "float", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrailFemalGenCount = table.Column<int>(type: "int", nullable: false),
                    TrialFemaleScStCount = table.Column<int>(type: "int", nullable: false),
                    TrialMaleGenCount = table.Column<int>(type: "int", nullable: false),
                    TrialMaleScStCount = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    YieldCheck = table.Column<double>(type: "float", nullable: false),
                    YieldDemo = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EeuFLDs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EeuFLDs_OrganizationUnitLocations_UnitLocationId",
                        column: x => x.UnitLocationId,
                        principalTable: "OrganizationUnitLocations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EeuFLDs_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EeuFLDs_Users_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EeuFLDs_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EeuFLDs_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EeuOFTs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApprovedById = table.Column<int>(type: "int", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    OrganizationId = table.Column<int>(type: "int", nullable: false),
                    UnitLocationId = table.Column<int>(type: "int", nullable: false),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    ApprovedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Area = table.Column<double>(type: "float", nullable: false),
                    Attachements = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    Crop = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    FormStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FormStatusRemarks = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    PercentIncreaseInYield = table.Column<double>(type: "float", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrailFemalGenCount = table.Column<int>(type: "int", nullable: false),
                    TrialFemaleScStCount = table.Column<int>(type: "int", nullable: false),
                    TrialMaleGenCount = table.Column<int>(type: "int", nullable: false),
                    TrialMaleScStCount = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    YieldT1 = table.Column<double>(type: "float", nullable: false),
                    YieldT2 = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EeuOFTs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EeuOFTs_OrganizationUnitLocations_UnitLocationId",
                        column: x => x.UnitLocationId,
                        principalTable: "OrganizationUnitLocations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EeuOFTs_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EeuOFTs_Users_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EeuOFTs_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EeuOFTs_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EeuOtherActivities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApprovedById = table.Column<int>(type: "int", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    OrganizationId = table.Column<int>(type: "int", nullable: false),
                    UnitLocationId = table.Column<int>(type: "int", nullable: false),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    ActivityDetails = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApprovedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Attachements = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    FormStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FormStatusRemarks = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EeuOtherActivities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EeuOtherActivities_OrganizationUnitLocations_UnitLocationId",
                        column: x => x.UnitLocationId,
                        principalTable: "OrganizationUnitLocations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EeuOtherActivities_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EeuOtherActivities_Users_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EeuOtherActivities_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EeuOtherActivities_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EeuTrainingProgrammes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApprovedById = table.Column<int>(type: "int", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    OrganizationId = table.Column<int>(type: "int", nullable: false),
                    UnitLocationId = table.Column<int>(type: "int", nullable: false),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    ApprovedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Attachements = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    FormStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FormStatusRemarks = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ParticipantCount = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    TrainingCount = table.Column<int>(type: "int", nullable: false),
                    TrainingTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EeuTrainingProgrammes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EeuTrainingProgrammes_OrganizationUnitLocations_UnitLocationId",
                        column: x => x.UnitLocationId,
                        principalTable: "OrganizationUnitLocations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EeuTrainingProgrammes_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EeuTrainingProgrammes_Users_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EeuTrainingProgrammes_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EeuTrainingProgrammes_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EeuFLDs_ApprovedById",
                table: "EeuFLDs",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_EeuFLDs_CreatedById",
                table: "EeuFLDs",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EeuFLDs_OrganizationId",
                table: "EeuFLDs",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_EeuFLDs_UnitLocationId",
                table: "EeuFLDs",
                column: "UnitLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_EeuFLDs_UpdatedById",
                table: "EeuFLDs",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EeuOFTs_ApprovedById",
                table: "EeuOFTs",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_EeuOFTs_CreatedById",
                table: "EeuOFTs",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EeuOFTs_OrganizationId",
                table: "EeuOFTs",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_EeuOFTs_UnitLocationId",
                table: "EeuOFTs",
                column: "UnitLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_EeuOFTs_UpdatedById",
                table: "EeuOFTs",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EeuOtherActivities_ApprovedById",
                table: "EeuOtherActivities",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_EeuOtherActivities_CreatedById",
                table: "EeuOtherActivities",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EeuOtherActivities_OrganizationId",
                table: "EeuOtherActivities",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_EeuOtherActivities_UnitLocationId",
                table: "EeuOtherActivities",
                column: "UnitLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_EeuOtherActivities_UpdatedById",
                table: "EeuOtherActivities",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EeuTrainingProgrammes_ApprovedById",
                table: "EeuTrainingProgrammes",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_EeuTrainingProgrammes_CreatedById",
                table: "EeuTrainingProgrammes",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EeuTrainingProgrammes_OrganizationId",
                table: "EeuTrainingProgrammes",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_EeuTrainingProgrammes_UnitLocationId",
                table: "EeuTrainingProgrammes",
                column: "UnitLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_EeuTrainingProgrammes_UpdatedById",
                table: "EeuTrainingProgrammes",
                column: "UpdatedById");
        }
    }
}

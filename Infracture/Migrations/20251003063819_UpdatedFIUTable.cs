using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedFIUTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FIUAdvisoryServices");

            migrationBuilder.DropTable(
                name: "FIUParticipantDemographics");

            migrationBuilder.DropTable(
                name: "FIURecommendations");

            migrationBuilder.DropTable(
                name: "FIUReports");

            migrationBuilder.DropTable(
                name: "FIUResourcePersons");

            migrationBuilder.DropTable(
                name: "FIUTeachingAidsDeveloped");

            migrationBuilder.DropTable(
                name: "FIUTopicsCoveredInClass");

            migrationBuilder.DropTable(
                name: "FIUProgramContentAndResources");

            migrationBuilder.DropTable(
                name: "FIUProgramDetails");

            migrationBuilder.CreateTable(
                name: "FIUOtherActivities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UploadMediaUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FIUOtherActivities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FIUOtherActivities_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FIUOtherActivities_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FIUProgramActivities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FIUActivitiesId = table.Column<int>(type: "int", nullable: true),
                    Number = table.Column<int>(type: "int", nullable: true),
                    UploadMediaUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FIUProgramActivities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FIUProgramActivities_FIUActivities_FIUActivitiesId",
                        column: x => x.FIUActivitiesId,
                        principalTable: "FIUActivities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FIUProgramActivities_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FIUProgramActivities_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FIUOtherActivities_CreatedById",
                table: "FIUOtherActivities",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FIUOtherActivities_UpdatedById",
                table: "FIUOtherActivities",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FIUProgramActivities_CreatedById",
                table: "FIUProgramActivities",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FIUProgramActivities_FIUActivitiesId",
                table: "FIUProgramActivities",
                column: "FIUActivitiesId");

            migrationBuilder.CreateIndex(
                name: "IX_FIUProgramActivities_UpdatedById",
                table: "FIUProgramActivities",
                column: "UpdatedById");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FIUOtherActivities");

            migrationBuilder.DropTable(
                name: "FIUProgramActivities");

            migrationBuilder.CreateTable(
                name: "FIUProgramDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    AreaHa = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    BatchNo = table.Column<int>(type: "int", maxLength: 100, nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    CategoryOther = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    Duration = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FundsSanctionLetterDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FundsSanctionLetterUploadFile = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Location = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Mode = table.Column<int>(type: "int", maxLength: 100, nullable: true),
                    OrganizerAddress = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    OrganizerFileUpload = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    OrganizerInstitutionName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    PaperPosterAbstract = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PaperPosterAbstractDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PaperPosterAbstractLink = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ParticipatedAs = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Participation = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ParticipationFileLink = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ProgramTypeId = table.Column<int>(type: "int", nullable: true),
                    ProposalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProposalUploadFile = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Region = table.Column<int>(type: "int", maxLength: 150, nullable: true),
                    RegionOther = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SD = table.Column<int>(type: "int", maxLength: 100, nullable: true),
                    SourceOfInformation = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    SponsoredOrganization = table.Column<int>(type: "int", maxLength: 200, nullable: true),
                    SponsoredOrganizationName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", maxLength: 100, nullable: true),
                    TPNo = table.Column<int>(type: "int", maxLength: 100, nullable: true),
                    ThematicArea = table.Column<int>(type: "int", maxLength: 150, nullable: true),
                    ThematicAreaOther = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Theme = table.Column<int>(type: "int", maxLength: 150, nullable: true),
                    ThemeOther = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    TitleOfThesisOrProjectOrPaperOrOthers = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TotalOutlayRs = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Type = table.Column<int>(type: "int", maxLength: 100, nullable: true),
                    TypeOther = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    UniversitySanctionLetterDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UniversitySanctionLetterUploadFile = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UploadVideo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FIUProgramDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FIUProgramDetails_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FIUProgramDetails_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FIUAdvisoryServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    FIUProgramDetailsID = table.Column<int>(type: "int", nullable: false),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    NoOfAnsweredWhatsappQueries = table.Column<int>(type: "int", nullable: true),
                    NoOfBeneficiaries = table.Column<int>(type: "int", nullable: true),
                    NoOfEmailsSent = table.Column<int>(type: "int", nullable: true),
                    NoOfFaceToFaceDiscussions = table.Column<int>(type: "int", nullable: true),
                    NoOfFacebookSMS = table.Column<int>(type: "int", nullable: true),
                    NoOfGroupDiscussions = table.Column<int>(type: "int", nullable: true),
                    NoOfNewspaperCoverage = table.Column<int>(type: "int", nullable: true),
                    NoOfPhoneCalls = table.Column<int>(type: "int", nullable: true),
                    NoOfSMSSentToRegisteredFarmers = table.Column<int>(type: "int", nullable: true),
                    NoOfWhatsappGroups = table.Column<int>(type: "int", nullable: true),
                    NoOfWhatsappSMS = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FIUAdvisoryServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FIUAdvisoryServices_FIUProgramDetails_FIUProgramDetailsID",
                        column: x => x.FIUProgramDetailsID,
                        principalTable: "FIUProgramDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FIUAdvisoryServices_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FIUAdvisoryServices_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FIUParticipantDemographics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    FIUProgramDetailsID = table.Column<int>(type: "int", nullable: false),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    Female_GEN = table.Column<int>(type: "int", nullable: true),
                    Female_OBC = table.Column<int>(type: "int", nullable: true),
                    Female_SC = table.Column<int>(type: "int", nullable: true),
                    Female_ST = table.Column<int>(type: "int", nullable: true),
                    GEN_Female_StayedInHostel = table.Column<int>(type: "int", nullable: true),
                    GEN_Male_StayedInHostel = table.Column<int>(type: "int", nullable: true),
                    Male_GEN = table.Column<int>(type: "int", nullable: true),
                    Male_OBC = table.Column<int>(type: "int", nullable: true),
                    Male_SC = table.Column<int>(type: "int", nullable: true),
                    Male_ST = table.Column<int>(type: "int", nullable: true),
                    OBC_Female_StayedInHostel = table.Column<int>(type: "int", nullable: true),
                    OBC_Male_StayedInHostel = table.Column<int>(type: "int", nullable: true),
                    Participant = table.Column<int>(type: "int", maxLength: 200, nullable: true),
                    SC_Female_StayedInHostel = table.Column<int>(type: "int", nullable: true),
                    SC_Male_StayedInHostel = table.Column<int>(type: "int", nullable: true),
                    ST_Female_StayedInHostel = table.Column<int>(type: "int", nullable: true),
                    ST_Male_StayedInHostel = table.Column<int>(type: "int", nullable: true),
                    Total = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FIUParticipantDemographics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FIUParticipantDemographics_FIUProgramDetails_FIUProgramDetailsID",
                        column: x => x.FIUProgramDetailsID,
                        principalTable: "FIUProgramDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FIUParticipantDemographics_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FIUParticipantDemographics_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FIUProgramContentAndResources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    FIUProgramDetailsID = table.Column<int>(type: "int", nullable: false),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FIUProgramContentAndResources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FIUProgramContentAndResources_FIUProgramDetails_FIUProgramDetailsID",
                        column: x => x.FIUProgramDetailsID,
                        principalTable: "FIUProgramDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FIUProgramContentAndResources_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FIUProgramContentAndResources_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FIURecommendations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    FIUProgramDetailsID = table.Column<int>(type: "int", nullable: false),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    ActionTaken = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    ImpactOutcome = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ProblemsIdentified = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Recommendation = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    SignificantAchievement = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    SuccessStories = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FIURecommendations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FIURecommendations_FIUProgramDetails_FIUProgramDetailsID",
                        column: x => x.FIUProgramDetailsID,
                        principalTable: "FIUProgramDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FIURecommendations_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FIURecommendations_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FIUReports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    FIUProgramDetailsID = table.Column<int>(type: "int", nullable: false),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PhotosGeotaggedPhotoOrUploadPhoto = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ProgressReportReportingYear = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    SignificantOutcome = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UploadPhoto = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    UploadVideo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FIUReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FIUReports_FIUProgramDetails_FIUProgramDetailsID",
                        column: x => x.FIUProgramDetailsID,
                        principalTable: "FIUProgramDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FIUReports_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FIUReports_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FIUResourcePersons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    FIUProgramContentAndResourcesID = table.Column<int>(type: "int", nullable: false),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    Designation = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    InstitutionOrDepartment = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ResourceType = table.Column<int>(type: "int", maxLength: 150, nullable: true),
                    Responsibility = table.Column<int>(type: "int", maxLength: 250, nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FIUResourcePersons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FIUResourcePersons_FIUProgramContentAndResources_FIUProgramContentAndResourcesID",
                        column: x => x.FIUProgramContentAndResourcesID,
                        principalTable: "FIUProgramContentAndResources",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FIUResourcePersons_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FIUResourcePersons_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FIUTeachingAidsDeveloped",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    FIUProgramContentAndResourcesID = table.Column<int>(type: "int", nullable: false),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    Number = table.Column<int>(type: "int", nullable: true),
                    Other = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Purpose = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    TypeOfAidDeveloped = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FIUTeachingAidsDeveloped", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FIUTeachingAidsDeveloped_FIUProgramContentAndResources_FIUProgramContentAndResourcesID",
                        column: x => x.FIUProgramContentAndResourcesID,
                        principalTable: "FIUProgramContentAndResources",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FIUTeachingAidsDeveloped_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FIUTeachingAidsDeveloped_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FIUTopicsCoveredInClass",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    FIUProgramContentAndResourcesID = table.Column<int>(type: "int", nullable: false),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PhotoUpload = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FIUTopicsCoveredInClass", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FIUTopicsCoveredInClass_FIUProgramContentAndResources_FIUProgramContentAndResourcesID",
                        column: x => x.FIUProgramContentAndResourcesID,
                        principalTable: "FIUProgramContentAndResources",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FIUTopicsCoveredInClass_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FIUTopicsCoveredInClass_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FIUAdvisoryServices_CreatedById",
                table: "FIUAdvisoryServices",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FIUAdvisoryServices_FIUProgramDetailsID",
                table: "FIUAdvisoryServices",
                column: "FIUProgramDetailsID");

            migrationBuilder.CreateIndex(
                name: "IX_FIUAdvisoryServices_UpdatedById",
                table: "FIUAdvisoryServices",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FIUParticipantDemographics_CreatedById",
                table: "FIUParticipantDemographics",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FIUParticipantDemographics_FIUProgramDetailsID",
                table: "FIUParticipantDemographics",
                column: "FIUProgramDetailsID");

            migrationBuilder.CreateIndex(
                name: "IX_FIUParticipantDemographics_UpdatedById",
                table: "FIUParticipantDemographics",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FIUProgramContentAndResources_CreatedById",
                table: "FIUProgramContentAndResources",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FIUProgramContentAndResources_FIUProgramDetailsID",
                table: "FIUProgramContentAndResources",
                column: "FIUProgramDetailsID");

            migrationBuilder.CreateIndex(
                name: "IX_FIUProgramContentAndResources_UpdatedById",
                table: "FIUProgramContentAndResources",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FIUProgramDetails_CreatedById",
                table: "FIUProgramDetails",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FIUProgramDetails_UpdatedById",
                table: "FIUProgramDetails",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FIURecommendations_CreatedById",
                table: "FIURecommendations",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FIURecommendations_FIUProgramDetailsID",
                table: "FIURecommendations",
                column: "FIUProgramDetailsID");

            migrationBuilder.CreateIndex(
                name: "IX_FIURecommendations_UpdatedById",
                table: "FIURecommendations",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FIUReports_CreatedById",
                table: "FIUReports",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FIUReports_FIUProgramDetailsID",
                table: "FIUReports",
                column: "FIUProgramDetailsID");

            migrationBuilder.CreateIndex(
                name: "IX_FIUReports_UpdatedById",
                table: "FIUReports",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FIUResourcePersons_CreatedById",
                table: "FIUResourcePersons",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FIUResourcePersons_FIUProgramContentAndResourcesID",
                table: "FIUResourcePersons",
                column: "FIUProgramContentAndResourcesID");

            migrationBuilder.CreateIndex(
                name: "IX_FIUResourcePersons_UpdatedById",
                table: "FIUResourcePersons",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FIUTeachingAidsDeveloped_CreatedById",
                table: "FIUTeachingAidsDeveloped",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FIUTeachingAidsDeveloped_FIUProgramContentAndResourcesID",
                table: "FIUTeachingAidsDeveloped",
                column: "FIUProgramContentAndResourcesID");

            migrationBuilder.CreateIndex(
                name: "IX_FIUTeachingAidsDeveloped_UpdatedById",
                table: "FIUTeachingAidsDeveloped",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FIUTopicsCoveredInClass_CreatedById",
                table: "FIUTopicsCoveredInClass",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FIUTopicsCoveredInClass_FIUProgramContentAndResourcesID",
                table: "FIUTopicsCoveredInClass",
                column: "FIUProgramContentAndResourcesID");

            migrationBuilder.CreateIndex(
                name: "IX_FIUTopicsCoveredInClass_UpdatedById",
                table: "FIUTopicsCoveredInClass",
                column: "UpdatedById");
        }
    }
}

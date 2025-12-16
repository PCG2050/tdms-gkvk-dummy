using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class New_Unit_Sameti_added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UploadVideoUrl",
                table: "KvkRecommendations",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "SametiProgramDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProgramTypeId = table.Column<int>(type: "int", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    CategoryOther = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    TypeId = table.Column<int>(type: "int", maxLength: 100, nullable: true),
                    TypeOther = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ThemeId = table.Column<int>(type: "int", maxLength: 150, nullable: true),
                    ThemeOther = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ThematicAreaId = table.Column<int>(type: "int", maxLength: 150, nullable: true),
                    ThematicAreaOther = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SponsoredOrganization = table.Column<int>(type: "int", maxLength: 200, nullable: true),
                    SponsoredOrganizationName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ModeId = table.Column<int>(type: "int", maxLength: 100, nullable: true),
                    Duration = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RegionId = table.Column<int>(type: "int", maxLength: 150, nullable: true),
                    RegionOther = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    TPNo = table.Column<int>(type: "int", maxLength: 100, nullable: true),
                    Location = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    SourceOfFundId = table.Column<int>(type: "int", nullable: true),
                    Funds = table.Column<int>(type: "int", nullable: true),
                    StatusId = table.Column<int>(type: "int", maxLength: 100, nullable: true),
                    TotalOutlayRs = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Copi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PiAddress = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    BatchNo = table.Column<int>(type: "int", maxLength: 100, nullable: true),
                    Area = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    OrganizerBroucherFile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrganizerInstitutionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrganizerInstitutionAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SourceId = table.Column<int>(type: "int", nullable: true),
                    OtherSourceOfInformation = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    SourceOfTitle = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ProposalDate = table.Column<DateOnly>(type: "date", nullable: true),
                    ProposalUploadFile = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    UniversitySanctionLetterDate = table.Column<DateOnly>(type: "date", nullable: true),
                    UniversitySanctionLetterUploadFile = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ProjectSanctionDate = table.Column<DateOnly>(type: "date", nullable: true),
                    ProjectSanctionFile = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    UniImplDate = table.Column<DateOnly>(type: "date", nullable: true),
                    UniImplLetterFile = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    FundReleaseYear = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FundAmount = table.Column<double>(type: "float", nullable: true),
                    FundReleaseDate = table.Column<DateOnly>(type: "date", nullable: true),
                    FundReleaseFile = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    FundsSanctionLetterDate = table.Column<DateOnly>(type: "date", nullable: true),
                    FundsSanctionLetterUploadFile = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ReportingVideo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Attachements = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnitLocationId = table.Column<int>(type: "int", nullable: false),
                    OrganizationId = table.Column<int>(type: "int", nullable: false),
                    FormStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FormStatusRemarks = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ApprovedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ApprovedById = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SametiProgramDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SametiProgramDetails_InfoTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "InfoTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SametiProgramDetails_Modes_ModeId",
                        column: x => x.ModeId,
                        principalTable: "Modes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SametiProgramDetails_OrganizationUnitLocations_UnitLocationId",
                        column: x => x.UnitLocationId,
                        principalTable: "OrganizationUnitLocations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SametiProgramDetails_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SametiProgramDetails_ParticipatedSources_SourceId",
                        column: x => x.SourceId,
                        principalTable: "ParticipatedSources",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SametiProgramDetails_ProgramCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ProgramCategories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SametiProgramDetails_ProgramTypes_ProgramTypeId",
                        column: x => x.ProgramTypeId,
                        principalTable: "ProgramTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SametiProgramDetails_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SametiProgramDetails_SourcesOfFunds_SourceOfFundId",
                        column: x => x.SourceOfFundId,
                        principalTable: "SourcesOfFunds",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SametiProgramDetails_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SametiProgramDetails_ThematicAreas_ThematicAreaId",
                        column: x => x.ThematicAreaId,
                        principalTable: "ThematicAreas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SametiProgramDetails_Themes_ThemeId",
                        column: x => x.ThemeId,
                        principalTable: "Themes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SametiProgramDetails_Users_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SametiProgramDetails_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SametiProgramDetails_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SametiAdvisoryServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SametiProgramDetailsId = table.Column<int>(type: "int", nullable: false),
                    NoOfFacebookSMS = table.Column<int>(type: "int", nullable: false),
                    NoOfSMSSentToRegisteredFarmers = table.Column<int>(type: "int", nullable: false),
                    NoOfWhatsappGroups = table.Column<int>(type: "int", nullable: false),
                    NoOfWhatsappSMS = table.Column<int>(type: "int", nullable: false),
                    NoOfAnsweredWhatsappQueries = table.Column<int>(type: "int", nullable: false),
                    NoOfPhoneCalls = table.Column<int>(type: "int", nullable: false),
                    NoOfFaceToFaceDiscussions = table.Column<int>(type: "int", nullable: false),
                    NoOfGroupDiscussions = table.Column<int>(type: "int", nullable: false),
                    NoOfEmailsSent = table.Column<int>(type: "int", nullable: false),
                    NoOfNewspaperCoverage = table.Column<int>(type: "int", nullable: false),
                    NoOfBeneficiaries = table.Column<int>(type: "int", nullable: false),
                    UnitLocationId = table.Column<int>(type: "int", nullable: true),
                    OrganizationId = table.Column<int>(type: "int", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SametiAdvisoryServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SametiAdvisoryServices_SametiProgramDetails_SametiProgramDetailsId",
                        column: x => x.SametiProgramDetailsId,
                        principalTable: "SametiProgramDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SametiAdvisoryServices_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SametiAdvisoryServices_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SametiParticipantDemographics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SametiProgramDetailsId = table.Column<int>(type: "int", nullable: false),
                    ParticipantId = table.Column<int>(type: "int", maxLength: 200, nullable: true),
                    Male_SC = table.Column<int>(type: "int", nullable: true),
                    Male_ST = table.Column<int>(type: "int", nullable: true),
                    Male_OBC = table.Column<int>(type: "int", nullable: true),
                    Male_GEN = table.Column<int>(type: "int", nullable: true),
                    SC_Male_StayedInHostel = table.Column<int>(type: "int", nullable: true),
                    ST_Male_StayedInHostel = table.Column<int>(type: "int", nullable: true),
                    OBC_Male_StayedInHostel = table.Column<int>(type: "int", nullable: true),
                    GEN_Male_StayedInHostel = table.Column<int>(type: "int", nullable: true),
                    Female_SC = table.Column<int>(type: "int", nullable: true),
                    Female_ST = table.Column<int>(type: "int", nullable: true),
                    Female_OBC = table.Column<int>(type: "int", nullable: true),
                    Female_GEN = table.Column<int>(type: "int", nullable: true),
                    SC_Female_StayedInHostel = table.Column<int>(type: "int", nullable: true),
                    ST_Female_StayedInHostel = table.Column<int>(type: "int", nullable: true),
                    OBC_Female_StayedInHostel = table.Column<int>(type: "int", nullable: true),
                    GEN_Female_StayedInHostel = table.Column<int>(type: "int", nullable: true),
                    Total = table.Column<int>(type: "int", nullable: true),
                    UnitLocationId = table.Column<int>(type: "int", nullable: true),
                    OrganizationId = table.Column<int>(type: "int", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SametiParticipantDemographics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SametiParticipantDemographics_ParticipantDealer_ParticipantId",
                        column: x => x.ParticipantId,
                        principalTable: "ParticipantDealer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SametiParticipantDemographics_SametiProgramDetails_SametiProgramDetailsId",
                        column: x => x.SametiProgramDetailsId,
                        principalTable: "SametiProgramDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SametiParticipantDemographics_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SametiParticipantDemographics_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SametiProgramContentAndResources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SametiProgramDetailsId = table.Column<int>(type: "int", nullable: false),
                    UnitLocationId = table.Column<int>(type: "int", nullable: true),
                    OrganizationId = table.Column<int>(type: "int", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SametiProgramContentAndResources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SametiProgramContentAndResources_SametiProgramDetails_SametiProgramDetailsId",
                        column: x => x.SametiProgramDetailsId,
                        principalTable: "SametiProgramDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SametiProgramContentAndResources_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SametiProgramContentAndResources_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SametiRecommendations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SametiProgramDetailsId = table.Column<int>(type: "int", nullable: false),
                    ProblemsIdentified = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Recommendation = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ActionTaken = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    SignificantAchievement = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    SuccessStories = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ImpactOutcome = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    UnitLocationId = table.Column<int>(type: "int", nullable: true),
                    OrganizationId = table.Column<int>(type: "int", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SametiRecommendations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SametiRecommendations_SametiProgramDetails_SametiProgramDetailsId",
                        column: x => x.SametiProgramDetailsId,
                        principalTable: "SametiProgramDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SametiRecommendations_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SametiRecommendations_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SametiReports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SametiProgramDetailsId = table.Column<int>(type: "int", nullable: false),
                    ReportingYear = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: true),
                    ReportDate = table.Column<DateOnly>(type: "date", nullable: true),
                    ProgressReport = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    GeoTaggedPhoto = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ReportingVideo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Outcome = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    TestingCompletionDate = table.Column<DateOnly>(type: "date", nullable: true),
                    TestingCompletionLetter = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ProjectCompletionDate = table.Column<DateOnly>(type: "date", nullable: true),
                    ProjectCompletionLetter = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TypeOfReport = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    SpclReport = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    UnitLocationId = table.Column<int>(type: "int", nullable: true),
                    OrganizationId = table.Column<int>(type: "int", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SametiReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SametiReports_SametiProgramDetails_SametiProgramDetailsId",
                        column: x => x.SametiProgramDetailsId,
                        principalTable: "SametiProgramDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SametiReports_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SametiReports_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SametiResourcePersons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SametiProgramContentAndResourcesId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Designation = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    ResourceType = table.Column<int>(type: "int", maxLength: 150, nullable: true),
                    Responsibility = table.Column<int>(type: "int", maxLength: 250, nullable: true),
                    InstitutionOrDepartment = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    UnitLocationId = table.Column<int>(type: "int", nullable: true),
                    OrganizationId = table.Column<int>(type: "int", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SametiResourcePersons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SametiResourcePersons_SametiProgramContentAndResources_SametiProgramContentAndResourcesId",
                        column: x => x.SametiProgramContentAndResourcesId,
                        principalTable: "SametiProgramContentAndResources",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SametiResourcePersons_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SametiResourcePersons_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SametiTeachingAidsDeveloped",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SametiProgramContentAndResourcesId = table.Column<int>(type: "int", nullable: false),
                    TypeOfAidId = table.Column<int>(type: "int", maxLength: 200, nullable: true),
                    OtherTypeOfAid = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Purpose = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Number = table.Column<int>(type: "int", nullable: true),
                    UnitLocationId = table.Column<int>(type: "int", nullable: true),
                    OrganizationId = table.Column<int>(type: "int", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SametiTeachingAidsDeveloped", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SametiTeachingAidsDeveloped_SametiProgramContentAndResources_SametiProgramContentAndResourcesId",
                        column: x => x.SametiProgramContentAndResourcesId,
                        principalTable: "SametiProgramContentAndResources",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SametiTeachingAidsDeveloped_TypeOfAids_TypeOfAidId",
                        column: x => x.TypeOfAidId,
                        principalTable: "TypeOfAids",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SametiTeachingAidsDeveloped_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SametiTeachingAidsDeveloped_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SametiTopicsCoveredInClass",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SametiProgramContentAndResourcesId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    PhotoUpload = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    UnitLocationId = table.Column<int>(type: "int", nullable: true),
                    OrganizationId = table.Column<int>(type: "int", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SametiTopicsCoveredInClass", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SametiTopicsCoveredInClass_SametiProgramContentAndResources_SametiProgramContentAndResourcesId",
                        column: x => x.SametiProgramContentAndResourcesId,
                        principalTable: "SametiProgramContentAndResources",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SametiTopicsCoveredInClass_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SametiTopicsCoveredInClass_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SametiAdvisoryServices_CreatedById",
                table: "SametiAdvisoryServices",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_SametiAdvisoryServices_SametiProgramDetailsId",
                table: "SametiAdvisoryServices",
                column: "SametiProgramDetailsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SametiAdvisoryServices_UpdatedById",
                table: "SametiAdvisoryServices",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_SametiParticipantDemographics_CreatedById",
                table: "SametiParticipantDemographics",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_SametiParticipantDemographics_ParticipantId",
                table: "SametiParticipantDemographics",
                column: "ParticipantId");

            migrationBuilder.CreateIndex(
                name: "IX_SametiParticipantDemographics_SametiProgramDetailsId",
                table: "SametiParticipantDemographics",
                column: "SametiProgramDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_SametiParticipantDemographics_UpdatedById",
                table: "SametiParticipantDemographics",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_SametiProgramContentAndResources_CreatedById",
                table: "SametiProgramContentAndResources",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_SametiProgramContentAndResources_SametiProgramDetailsId",
                table: "SametiProgramContentAndResources",
                column: "SametiProgramDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_SametiProgramContentAndResources_UpdatedById",
                table: "SametiProgramContentAndResources",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_SametiProgramDetails_ApprovedById",
                table: "SametiProgramDetails",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_SametiProgramDetails_CategoryId",
                table: "SametiProgramDetails",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SametiProgramDetails_CreatedById",
                table: "SametiProgramDetails",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_SametiProgramDetails_ModeId",
                table: "SametiProgramDetails",
                column: "ModeId");

            migrationBuilder.CreateIndex(
                name: "IX_SametiProgramDetails_OrganizationId",
                table: "SametiProgramDetails",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_SametiProgramDetails_ProgramTypeId",
                table: "SametiProgramDetails",
                column: "ProgramTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SametiProgramDetails_RegionId",
                table: "SametiProgramDetails",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_SametiProgramDetails_SourceId",
                table: "SametiProgramDetails",
                column: "SourceId");

            migrationBuilder.CreateIndex(
                name: "IX_SametiProgramDetails_SourceOfFundId",
                table: "SametiProgramDetails",
                column: "SourceOfFundId");

            migrationBuilder.CreateIndex(
                name: "IX_SametiProgramDetails_StatusId",
                table: "SametiProgramDetails",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_SametiProgramDetails_ThematicAreaId",
                table: "SametiProgramDetails",
                column: "ThematicAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_SametiProgramDetails_ThemeId",
                table: "SametiProgramDetails",
                column: "ThemeId");

            migrationBuilder.CreateIndex(
                name: "IX_SametiProgramDetails_TypeId",
                table: "SametiProgramDetails",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SametiProgramDetails_UnitLocationId",
                table: "SametiProgramDetails",
                column: "UnitLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_SametiProgramDetails_UpdatedById",
                table: "SametiProgramDetails",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_SametiRecommendations_CreatedById",
                table: "SametiRecommendations",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_SametiRecommendations_SametiProgramDetailsId",
                table: "SametiRecommendations",
                column: "SametiProgramDetailsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SametiRecommendations_UpdatedById",
                table: "SametiRecommendations",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_SametiReports_CreatedById",
                table: "SametiReports",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_SametiReports_SametiProgramDetailsId",
                table: "SametiReports",
                column: "SametiProgramDetailsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SametiReports_UpdatedById",
                table: "SametiReports",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_SametiResourcePersons_CreatedById",
                table: "SametiResourcePersons",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_SametiResourcePersons_SametiProgramContentAndResourcesId",
                table: "SametiResourcePersons",
                column: "SametiProgramContentAndResourcesId");

            migrationBuilder.CreateIndex(
                name: "IX_SametiResourcePersons_UpdatedById",
                table: "SametiResourcePersons",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_SametiTeachingAidsDeveloped_CreatedById",
                table: "SametiTeachingAidsDeveloped",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_SametiTeachingAidsDeveloped_SametiProgramContentAndResourcesId",
                table: "SametiTeachingAidsDeveloped",
                column: "SametiProgramContentAndResourcesId");

            migrationBuilder.CreateIndex(
                name: "IX_SametiTeachingAidsDeveloped_TypeOfAidId",
                table: "SametiTeachingAidsDeveloped",
                column: "TypeOfAidId");

            migrationBuilder.CreateIndex(
                name: "IX_SametiTeachingAidsDeveloped_UpdatedById",
                table: "SametiTeachingAidsDeveloped",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_SametiTopicsCoveredInClass_CreatedById",
                table: "SametiTopicsCoveredInClass",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_SametiTopicsCoveredInClass_SametiProgramContentAndResourcesId",
                table: "SametiTopicsCoveredInClass",
                column: "SametiProgramContentAndResourcesId");

            migrationBuilder.CreateIndex(
                name: "IX_SametiTopicsCoveredInClass_UpdatedById",
                table: "SametiTopicsCoveredInClass",
                column: "UpdatedById");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SametiAdvisoryServices");

            migrationBuilder.DropTable(
                name: "SametiParticipantDemographics");

            migrationBuilder.DropTable(
                name: "SametiRecommendations");

            migrationBuilder.DropTable(
                name: "SametiReports");

            migrationBuilder.DropTable(
                name: "SametiResourcePersons");

            migrationBuilder.DropTable(
                name: "SametiTeachingAidsDeveloped");

            migrationBuilder.DropTable(
                name: "SametiTopicsCoveredInClass");

            migrationBuilder.DropTable(
                name: "SametiProgramContentAndResources");

            migrationBuilder.DropTable(
                name: "SametiProgramDetails");

            migrationBuilder.AlterColumn<string>(
                name: "UploadVideoUrl",
                table: "KvkRecommendations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);
        }
    }
}

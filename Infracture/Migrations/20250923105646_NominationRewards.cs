using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NominationRewards : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Visitors_Name",
                table: "Visitors");

            migrationBuilder.DropIndex(
                name: "IX_VillageAdoptivePrograms_Name",
                table: "VillageAdoptivePrograms");

            migrationBuilder.DropIndex(
                name: "IX_TypeOfAids_Name",
                table: "TypeOfAids");

            migrationBuilder.DropIndex(
                name: "IX_Themes_Name",
                table: "Themes");

            migrationBuilder.DropIndex(
                name: "IX_ThematicAreas_Name",
                table: "ThematicAreas");

            migrationBuilder.DropIndex(
                name: "IX_TargetFarmers_Name",
                table: "TargetFarmers");

            migrationBuilder.DropIndex(
                name: "IX_Statuses_Name",
                table: "Statuses");

            migrationBuilder.DropIndex(
                name: "IX_SponsoredOrganizations_Name",
                table: "SponsoredOrganizations");

            migrationBuilder.DropIndex(
                name: "IX_SourcesOfFunds_Name",
                table: "SourcesOfFunds");

            migrationBuilder.DropIndex(
                name: "IX_ServiceThemes_Name",
                table: "ServiceThemes");

            migrationBuilder.DropIndex(
                name: "IX_ServicesCategories_Name",
                table: "ServicesCategories");

            migrationBuilder.DropIndex(
                name: "IX_ServiceCategories_Name",
                table: "ServiceCategories");

            migrationBuilder.DropIndex(
                name: "IX_Responsibilities_Name",
                table: "Responsibilities");

            migrationBuilder.DropIndex(
                name: "IX_ResourceTypes_Name",
                table: "ResourceTypes");

            migrationBuilder.DropIndex(
                name: "IX_RelatedTos_Name",
                table: "RelatedTos");

            migrationBuilder.DropIndex(
                name: "IX_Regions_Name",
                table: "Regions");

            migrationBuilder.DropIndex(
                name: "IX_QuantityUnits_Name",
                table: "QuantityUnits");

            migrationBuilder.DropIndex(
                name: "IX_PublicationCategories_Name",
                table: "PublicationCategories");

            migrationBuilder.DropIndex(
                name: "IX_ProjectCategories_Name",
                table: "ProjectCategories");

            migrationBuilder.DropIndex(
                name: "IX_ProgramTypes_Name",
                table: "ProgramTypes");

            migrationBuilder.DropIndex(
                name: "IX_ProgramCategories_Name",
                table: "ProgramCategories");

            migrationBuilder.DropIndex(
                name: "IX_Positions_Name",
                table: "Positions");

            migrationBuilder.DropIndex(
                name: "IX_Particulars_Name",
                table: "Particulars");

            migrationBuilder.DropIndex(
                name: "IX_ParticipatedSources_Name",
                table: "ParticipatedSources");

            migrationBuilder.DropIndex(
                name: "IX_Participants_Name",
                table: "Participants");

            migrationBuilder.DropIndex(
                name: "IX_ParticipantDealer_Name",
                table: "ParticipantDealer");

            migrationBuilder.DropIndex(
                name: "IX_OFTResults_Name",
                table: "OFTResults");

            migrationBuilder.DropIndex(
                name: "IX_NominationTypes_Name",
                table: "NominationTypes");

            migrationBuilder.DropIndex(
                name: "IX_NominationCategories_Name",
                table: "NominationCategories");

            migrationBuilder.DropIndex(
                name: "IX_Modes_Name",
                table: "Modes");

            migrationBuilder.DropIndex(
                name: "IX_ModeOutreaches_Name",
                table: "ModeOutreaches");

            migrationBuilder.DropIndex(
                name: "IX_KannadaNewsPapers_Name",
                table: "KannadaNewsPapers");

            migrationBuilder.DropIndex(
                name: "IX_KannadaMagazines_Name",
                table: "KannadaMagazines");

            migrationBuilder.DropIndex(
                name: "IX_InfoTypes_Name",
                table: "InfoTypes");

            migrationBuilder.DropIndex(
                name: "IX_FLDResults_Name",
                table: "FLDResults");

            migrationBuilder.DropIndex(
                name: "IX_FIUActivities_Name",
                table: "FIUActivities");

            migrationBuilder.DropIndex(
                name: "IX_ExtensionWorks_Name",
                table: "ExtensionWorks");

            migrationBuilder.DropIndex(
                name: "IX_EventNames_Name",
                table: "EventNames");

            migrationBuilder.DropIndex(
                name: "IX_EnglishNewsPapers_Name",
                table: "EnglishNewsPapers");

            migrationBuilder.DropIndex(
                name: "IX_EnglishMagazines_Name",
                table: "EnglishMagazines");

            migrationBuilder.DropIndex(
                name: "IX_Contributions_Name",
                table: "Contributions");

            migrationBuilder.DropIndex(
                name: "IX_Collaborators_Name",
                table: "Collaborators");

            migrationBuilder.DropIndex(
                name: "IX_CollaborativeProgramOptions_Name",
                table: "CollaborativeProgramOptions");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Visitors",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "VillageAdoptivePrograms",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "TypeOfAids",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Themes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ThematicAreas",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "TargetFarmers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Statuses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "SponsoredOrganizations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "SourcesOfFunds",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ServiceThemes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ServicesCategories",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ServiceCategories",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Responsibilities",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ResourceTypes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "RelatedTos",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Regions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "QuantityUnits",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "PublicationCategories",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ProjectCategories",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ProgramTypes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ProgramCategories",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Positions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Particulars",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ParticipatedSources",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Participants",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ParticipantDealer",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "OFTResults",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "NominationTypes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "NominationCategories",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Modes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ModeOutreaches",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "KannadaNewsPapers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "KannadaMagazines",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "InfoTypes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "FLDResults",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "FIUActivities",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ExtensionWorks",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "EventNames",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "EnglishNewsPapers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "EnglishMagazines",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Contributions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Collaborators",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "CollaborativeProgramOptions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.CreateTable(
                name: "NominationRewards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeId = table.Column<int>(type: "int", nullable: true),
                    RegionId = table.Column<int>(type: "int", nullable: true),
                    ContributionId = table.Column<int>(type: "int", nullable: true),
                    ModeId = table.Column<int>(type: "int", nullable: true),
                    NominationCategoryId = table.Column<int>(type: "int", nullable: true),
                    InstitutionPositionId = table.Column<int>(type: "int", nullable: true),
                    OtherRegion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AwardName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OtherContribution = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AwardingAgency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpecificContributionTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrganizerInstitutionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrganizerInstituteAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AwardApplicationDate = table.Column<DateOnly>(type: "date", nullable: true),
                    AwardFilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AwardEventTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AwardEventDate = table.Column<DateOnly>(type: "date", nullable: true),
                    SanctionLetterDate = table.Column<DateOnly>(type: "date", nullable: true),
                    SanctionLetterFilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaperDate = table.Column<DateOnly>(type: "date", nullable: true),
                    PaperFilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AwardReceivingPhoto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AwardReceivingCertificate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstitutionBoardName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstitutionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstitutionDesignation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstitutionAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PositionFrom = table.Column<DateOnly>(type: "date", nullable: true),
                    PositionTo = table.Column<DateOnly>(type: "date", nullable: true),
                    DurationDays = table.Column<int>(type: "int", nullable: true),
                    NominationDate = table.Column<DateOnly>(type: "date", nullable: true),
                    NominationLetterPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Attachements = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnitLocationId = table.Column<int>(type: "int", nullable: false),
                    OrganizationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NominationRewards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NominationRewards_Contributions_ContributionId",
                        column: x => x.ContributionId,
                        principalTable: "Contributions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NominationRewards_Modes_ModeId",
                        column: x => x.ModeId,
                        principalTable: "Modes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NominationRewards_NominationCategories_NominationCategoryId",
                        column: x => x.NominationCategoryId,
                        principalTable: "NominationCategories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NominationRewards_NominationTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "NominationTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NominationRewards_OrganizationUnitLocations_UnitLocationId",
                        column: x => x.UnitLocationId,
                        principalTable: "OrganizationUnitLocations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NominationRewards_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NominationRewards_Positions_InstitutionPositionId",
                        column: x => x.InstitutionPositionId,
                        principalTable: "Positions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NominationRewards_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NominationRewards_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NominationRewards_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "NominationRewardEntrepreneurInnovations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<int>(type: "int", nullable: true),
                    DetailsOfInnovation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NominationRewardId = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NominationRewardEntrepreneurInnovations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NominationRewardEntrepreneurInnovations_NominationRewards_NominationRewardId",
                        column: x => x.NominationRewardId,
                        principalTable: "NominationRewards",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NominationRewardEntrepreneurInnovations_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NominationRewardEntrepreneurInnovations_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "NominationRewardFarmerInnovations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<int>(type: "int", nullable: true),
                    DetailsOfInnovation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NominationRewardId = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NominationRewardFarmerInnovations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NominationRewardFarmerInnovations_NominationRewards_NominationRewardId",
                        column: x => x.NominationRewardId,
                        principalTable: "NominationRewards",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NominationRewardFarmerInnovations_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NominationRewardFarmerInnovations_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "NominationRewardIFSEntrepreneurs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ComponentOfIFS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NominationRewardId = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NominationRewardIFSEntrepreneurs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NominationRewardIFSEntrepreneurs_NominationRewards_NominationRewardId",
                        column: x => x.NominationRewardId,
                        principalTable: "NominationRewards",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NominationRewardIFSEntrepreneurs_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NominationRewardIFSEntrepreneurs_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "NominationRewardIFSFarmers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ComponentOfIFS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NominationRewardId = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NominationRewardIFSFarmers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NominationRewardIFSFarmers_NominationRewards_NominationRewardId",
                        column: x => x.NominationRewardId,
                        principalTable: "NominationRewards",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NominationRewardIFSFarmers_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NominationRewardIFSFarmers_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "NominationRewardOrganicEntrepreneurs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<int>(type: "int", nullable: true),
                    CropsGrown = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NominationRewardId = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NominationRewardOrganicEntrepreneurs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NominationRewardOrganicEntrepreneurs_NominationRewards_NominationRewardId",
                        column: x => x.NominationRewardId,
                        principalTable: "NominationRewards",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NominationRewardOrganicEntrepreneurs_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NominationRewardOrganicEntrepreneurs_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "NominationRewardOrganicFarmers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<int>(type: "int", nullable: true),
                    CropsGrown = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NominationRewardId = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NominationRewardOrganicFarmers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NominationRewardOrganicFarmers_NominationRewards_NominationRewardId",
                        column: x => x.NominationRewardId,
                        principalTable: "NominationRewards",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NominationRewardOrganicFarmers_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NominationRewardOrganicFarmers_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_NominationRewardEntrepreneurInnovations_CreatedById",
                table: "NominationRewardEntrepreneurInnovations",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_NominationRewardEntrepreneurInnovations_NominationRewardId",
                table: "NominationRewardEntrepreneurInnovations",
                column: "NominationRewardId");

            migrationBuilder.CreateIndex(
                name: "IX_NominationRewardEntrepreneurInnovations_UpdatedById",
                table: "NominationRewardEntrepreneurInnovations",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_NominationRewardFarmerInnovations_CreatedById",
                table: "NominationRewardFarmerInnovations",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_NominationRewardFarmerInnovations_NominationRewardId",
                table: "NominationRewardFarmerInnovations",
                column: "NominationRewardId");

            migrationBuilder.CreateIndex(
                name: "IX_NominationRewardFarmerInnovations_UpdatedById",
                table: "NominationRewardFarmerInnovations",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_NominationRewardIFSEntrepreneurs_CreatedById",
                table: "NominationRewardIFSEntrepreneurs",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_NominationRewardIFSEntrepreneurs_NominationRewardId",
                table: "NominationRewardIFSEntrepreneurs",
                column: "NominationRewardId");

            migrationBuilder.CreateIndex(
                name: "IX_NominationRewardIFSEntrepreneurs_UpdatedById",
                table: "NominationRewardIFSEntrepreneurs",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_NominationRewardIFSFarmers_CreatedById",
                table: "NominationRewardIFSFarmers",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_NominationRewardIFSFarmers_NominationRewardId",
                table: "NominationRewardIFSFarmers",
                column: "NominationRewardId");

            migrationBuilder.CreateIndex(
                name: "IX_NominationRewardIFSFarmers_UpdatedById",
                table: "NominationRewardIFSFarmers",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_NominationRewardOrganicEntrepreneurs_CreatedById",
                table: "NominationRewardOrganicEntrepreneurs",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_NominationRewardOrganicEntrepreneurs_NominationRewardId",
                table: "NominationRewardOrganicEntrepreneurs",
                column: "NominationRewardId");

            migrationBuilder.CreateIndex(
                name: "IX_NominationRewardOrganicEntrepreneurs_UpdatedById",
                table: "NominationRewardOrganicEntrepreneurs",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_NominationRewardOrganicFarmers_CreatedById",
                table: "NominationRewardOrganicFarmers",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_NominationRewardOrganicFarmers_NominationRewardId",
                table: "NominationRewardOrganicFarmers",
                column: "NominationRewardId");

            migrationBuilder.CreateIndex(
                name: "IX_NominationRewardOrganicFarmers_UpdatedById",
                table: "NominationRewardOrganicFarmers",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_NominationRewards_ContributionId",
                table: "NominationRewards",
                column: "ContributionId");

            migrationBuilder.CreateIndex(
                name: "IX_NominationRewards_CreatedById",
                table: "NominationRewards",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_NominationRewards_InstitutionPositionId",
                table: "NominationRewards",
                column: "InstitutionPositionId");

            migrationBuilder.CreateIndex(
                name: "IX_NominationRewards_ModeId",
                table: "NominationRewards",
                column: "ModeId");

            migrationBuilder.CreateIndex(
                name: "IX_NominationRewards_NominationCategoryId",
                table: "NominationRewards",
                column: "NominationCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_NominationRewards_OrganizationId",
                table: "NominationRewards",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_NominationRewards_RegionId",
                table: "NominationRewards",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_NominationRewards_TypeId",
                table: "NominationRewards",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_NominationRewards_UnitLocationId",
                table: "NominationRewards",
                column: "UnitLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_NominationRewards_UpdatedById",
                table: "NominationRewards",
                column: "UpdatedById");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NominationRewardEntrepreneurInnovations");

            migrationBuilder.DropTable(
                name: "NominationRewardFarmerInnovations");

            migrationBuilder.DropTable(
                name: "NominationRewardIFSEntrepreneurs");

            migrationBuilder.DropTable(
                name: "NominationRewardIFSFarmers");

            migrationBuilder.DropTable(
                name: "NominationRewardOrganicEntrepreneurs");

            migrationBuilder.DropTable(
                name: "NominationRewardOrganicFarmers");

            migrationBuilder.DropTable(
                name: "NominationRewards");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Visitors",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "VillageAdoptivePrograms",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "TypeOfAids",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Themes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ThematicAreas",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "TargetFarmers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Statuses",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "SponsoredOrganizations",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "SourcesOfFunds",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ServiceThemes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ServicesCategories",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ServiceCategories",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Responsibilities",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ResourceTypes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "RelatedTos",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Regions",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "QuantityUnits",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "PublicationCategories",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ProjectCategories",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ProgramTypes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ProgramCategories",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Positions",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Particulars",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ParticipatedSources",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Participants",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ParticipantDealer",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "OFTResults",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "NominationTypes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "NominationCategories",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Modes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ModeOutreaches",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "KannadaNewsPapers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "KannadaMagazines",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "InfoTypes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "FLDResults",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "FIUActivities",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ExtensionWorks",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "EventNames",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "EnglishNewsPapers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "EnglishMagazines",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Contributions",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Collaborators",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "CollaborativeProgramOptions",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Visitors_Name",
                table: "Visitors",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VillageAdoptivePrograms_Name",
                table: "VillageAdoptivePrograms",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TypeOfAids_Name",
                table: "TypeOfAids",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Themes_Name",
                table: "Themes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ThematicAreas_Name",
                table: "ThematicAreas",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TargetFarmers_Name",
                table: "TargetFarmers",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Statuses_Name",
                table: "Statuses",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SponsoredOrganizations_Name",
                table: "SponsoredOrganizations",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SourcesOfFunds_Name",
                table: "SourcesOfFunds",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceThemes_Name",
                table: "ServiceThemes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServicesCategories_Name",
                table: "ServicesCategories",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceCategories_Name",
                table: "ServiceCategories",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Responsibilities_Name",
                table: "Responsibilities",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ResourceTypes_Name",
                table: "ResourceTypes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RelatedTos_Name",
                table: "RelatedTos",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Regions_Name",
                table: "Regions",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_QuantityUnits_Name",
                table: "QuantityUnits",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PublicationCategories_Name",
                table: "PublicationCategories",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectCategories_Name",
                table: "ProjectCategories",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProgramTypes_Name",
                table: "ProgramTypes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProgramCategories_Name",
                table: "ProgramCategories",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Positions_Name",
                table: "Positions",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Particulars_Name",
                table: "Particulars",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ParticipatedSources_Name",
                table: "ParticipatedSources",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Participants_Name",
                table: "Participants",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantDealer_Name",
                table: "ParticipantDealer",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OFTResults_Name",
                table: "OFTResults",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NominationTypes_Name",
                table: "NominationTypes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NominationCategories_Name",
                table: "NominationCategories",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Modes_Name",
                table: "Modes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ModeOutreaches_Name",
                table: "ModeOutreaches",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_KannadaNewsPapers_Name",
                table: "KannadaNewsPapers",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_KannadaMagazines_Name",
                table: "KannadaMagazines",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InfoTypes_Name",
                table: "InfoTypes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FLDResults_Name",
                table: "FLDResults",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FIUActivities_Name",
                table: "FIUActivities",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExtensionWorks_Name",
                table: "ExtensionWorks",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EventNames_Name",
                table: "EventNames",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EnglishNewsPapers_Name",
                table: "EnglishNewsPapers",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EnglishMagazines_Name",
                table: "EnglishMagazines",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contributions_Name",
                table: "Contributions",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Collaborators_Name",
                table: "Collaborators",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CollaborativeProgramOptions_Name",
                table: "CollaborativeProgramOptions",
                column: "Name",
                unique: true);
        }
    }
}

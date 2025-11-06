using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Deu_Eeu_Ibtva_Tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FIURecommendation_FIUProgramDetails_FIUProgramDetailsID",
                table: "FIURecommendation");

            migrationBuilder.DropForeignKey(
                name: "FK_FIURecommendation_Users_CreatedById",
                table: "FIURecommendation");

            migrationBuilder.DropForeignKey(
                name: "FK_FIURecommendation_Users_UpdatedById",
                table: "FIURecommendation");

            migrationBuilder.DropForeignKey(
                name: "FK_FIUResourcePerson_FIUProgramContentAndResources_FIUProgramContentAndResourcesID",
                table: "FIUResourcePerson");

            migrationBuilder.DropForeignKey(
                name: "FK_FIUResourcePerson_Users_CreatedById",
                table: "FIUResourcePerson");

            migrationBuilder.DropForeignKey(
                name: "FK_FIUResourcePerson_Users_UpdatedById",
                table: "FIUResourcePerson");

            migrationBuilder.DropForeignKey(
                name: "FK_FTIRecommendation_FTIProgramDetails_FTIProgramDetailsID",
                table: "FTIRecommendation");

            migrationBuilder.DropForeignKey(
                name: "FK_FTIRecommendation_Users_CreatedById",
                table: "FTIRecommendation");

            migrationBuilder.DropForeignKey(
                name: "FK_FTIRecommendation_Users_UpdatedById",
                table: "FTIRecommendation");

            migrationBuilder.DropForeignKey(
                name: "FK_FTIResourcePerson_FTIProgramContentAndResources_FTIProgramContentAndResourcesID",
                table: "FTIResourcePerson");

            migrationBuilder.DropForeignKey(
                name: "FK_FTIResourcePerson_Users_CreatedById",
                table: "FTIResourcePerson");

            migrationBuilder.DropForeignKey(
                name: "FK_FTIResourcePerson_Users_UpdatedById",
                table: "FTIResourcePerson");

            migrationBuilder.DropForeignKey(
                name: "FK_KvkAdvisoryServices_TableKVKProgramDetails_KVKProgramDetailsId",
                table: "KvkAdvisoryServices");

            migrationBuilder.DropForeignKey(
                name: "FK_KvkParticipantDemographics_TableKVKProgramDetails_KVKProgramDetailsId",
                table: "KvkParticipantDemographics");

            migrationBuilder.DropForeignKey(
                name: "FK_KvkProgramContentAndResources_TableKVKProgramDetails_KVKProgramDetailsId",
                table: "KvkProgramContentAndResources");

            migrationBuilder.DropForeignKey(
                name: "FK_KvkRecommendation_TableKVKProgramDetails_KVKProgramDetailsId",
                table: "KvkRecommendation");

            migrationBuilder.DropForeignKey(
                name: "FK_KvkRecommendation_Users_CreatedById",
                table: "KvkRecommendation");

            migrationBuilder.DropForeignKey(
                name: "FK_KvkRecommendation_Users_UpdatedById",
                table: "KvkRecommendation");

            migrationBuilder.DropForeignKey(
                name: "FK_KvkReports_TableKVKProgramDetails_KVKProgramDetailsId",
                table: "KvkReports");

            migrationBuilder.DropForeignKey(
                name: "FK_KvkResourcePerson_KvkProgramContentAndResources_ProgramContentAndResourcesId",
                table: "KvkResourcePerson");

            migrationBuilder.DropForeignKey(
                name: "FK_KvkResourcePerson_Users_CreatedById",
                table: "KvkResourcePerson");

            migrationBuilder.DropForeignKey(
                name: "FK_KvkResourcePerson_Users_UpdatedById",
                table: "KvkResourcePerson");

            migrationBuilder.DropForeignKey(
                name: "FK_STURecommendation_STUProgramDetails_STUProgramDetailsID",
                table: "STURecommendation");

            migrationBuilder.DropForeignKey(
                name: "FK_STURecommendation_Users_CreatedById",
                table: "STURecommendation");

            migrationBuilder.DropForeignKey(
                name: "FK_STURecommendation_Users_UpdatedById",
                table: "STURecommendation");

            migrationBuilder.DropForeignKey(
                name: "FK_STUResourcePerson_STUProgramContentAndResources_STUProgramContentAndResourcesID",
                table: "STUResourcePerson");

            migrationBuilder.DropForeignKey(
                name: "FK_STUResourcePerson_Users_CreatedById",
                table: "STUResourcePerson");

            migrationBuilder.DropForeignKey(
                name: "FK_STUResourcePerson_Users_UpdatedById",
                table: "STUResourcePerson");

            migrationBuilder.DropForeignKey(
                name: "FK_TableConsultingAndSocialMediaServices_TableModeAndOutages_Fk_ModeOrOutageId",
                table: "TableConsultingAndSocialMediaServices");

            migrationBuilder.DropForeignKey(
                name: "FK_TableKVKProgramDetails_Users_CreatedById",
                table: "TableKVKProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_TableKVKProgramDetails_Users_UpdatedById",
                table: "TableKVKProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_TableOtherActivity_OrganizationUnitLocations_UnitLocationId",
                table: "TableOtherActivity");

            migrationBuilder.DropForeignKey(
                name: "FK_TableOtherActivity_Organizations_OrganizationId",
                table: "TableOtherActivity");

            migrationBuilder.DropForeignKey(
                name: "FK_TableOtherActivity_Users_CreatedById",
                table: "TableOtherActivity");

            migrationBuilder.DropForeignKey(
                name: "FK_TableOtherActivity_Users_UpdatedById",
                table: "TableOtherActivity");

            migrationBuilder.DropIndex(
                name: "IX_TableConsultingAndSocialMediaServices_Fk_ModeOrOutageId",
                table: "TableConsultingAndSocialMediaServices");

            migrationBuilder.DropIndex(
                name: "IX_PublisherDetails_PublicationId",
                table: "PublisherDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TableOtherActivity",
                table: "TableOtherActivity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TableKVKProgramDetails",
                table: "TableKVKProgramDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_STUResourcePerson",
                table: "STUResourcePerson");

            migrationBuilder.DropPrimaryKey(
                name: "PK_STURecommendation",
                table: "STURecommendation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_KvkResourcePerson",
                table: "KvkResourcePerson");

            migrationBuilder.DropPrimaryKey(
                name: "PK_KvkRecommendation",
                table: "KvkRecommendation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FTIResourcePerson",
                table: "FTIResourcePerson");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FTIRecommendation",
                table: "FTIRecommendation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FIUResourcePerson",
                table: "FIUResourcePerson");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FIURecommendation",
                table: "FIURecommendation");

            migrationBuilder.RenameTable(
                name: "TableOtherActivity",
                newName: "OtherActivities");

            migrationBuilder.RenameTable(
                name: "TableKVKProgramDetails",
                newName: "KVKProgramDetails");

            migrationBuilder.RenameTable(
                name: "STUResourcePerson",
                newName: "STUResourcePersons");

            migrationBuilder.RenameTable(
                name: "STURecommendation",
                newName: "STURecommendations");

            migrationBuilder.RenameTable(
                name: "KvkResourcePerson",
                newName: "KvkResourcePersons");

            migrationBuilder.RenameTable(
                name: "KvkRecommendation",
                newName: "KvkRecommendations");

            migrationBuilder.RenameTable(
                name: "FTIResourcePerson",
                newName: "FTIResourcePersons");

            migrationBuilder.RenameTable(
                name: "FTIRecommendation",
                newName: "FTIRecommendations");

            migrationBuilder.RenameTable(
                name: "FIUResourcePerson",
                newName: "FIUResourcePersons");

            migrationBuilder.RenameTable(
                name: "FIURecommendation",
                newName: "FIURecommendations");

            migrationBuilder.RenameIndex(
                name: "IX_TableOtherActivity_UpdatedById",
                table: "OtherActivities",
                newName: "IX_OtherActivities_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_TableOtherActivity_UnitLocationId",
                table: "OtherActivities",
                newName: "IX_OtherActivities_UnitLocationId");

            migrationBuilder.RenameIndex(
                name: "IX_TableOtherActivity_OrganizationId",
                table: "OtherActivities",
                newName: "IX_OtherActivities_OrganizationId");

            migrationBuilder.RenameIndex(
                name: "IX_TableOtherActivity_CreatedById",
                table: "OtherActivities",
                newName: "IX_OtherActivities_CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_TableKVKProgramDetails_UpdatedById",
                table: "KVKProgramDetails",
                newName: "IX_KVKProgramDetails_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_TableKVKProgramDetails_CreatedById",
                table: "KVKProgramDetails",
                newName: "IX_KVKProgramDetails_CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_STUResourcePerson_UpdatedById",
                table: "STUResourcePersons",
                newName: "IX_STUResourcePersons_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_STUResourcePerson_STUProgramContentAndResourcesID",
                table: "STUResourcePersons",
                newName: "IX_STUResourcePersons_STUProgramContentAndResourcesID");

            migrationBuilder.RenameIndex(
                name: "IX_STUResourcePerson_CreatedById",
                table: "STUResourcePersons",
                newName: "IX_STUResourcePersons_CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_STURecommendation_UpdatedById",
                table: "STURecommendations",
                newName: "IX_STURecommendations_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_STURecommendation_STUProgramDetailsID",
                table: "STURecommendations",
                newName: "IX_STURecommendations_STUProgramDetailsID");

            migrationBuilder.RenameIndex(
                name: "IX_STURecommendation_CreatedById",
                table: "STURecommendations",
                newName: "IX_STURecommendations_CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_KvkResourcePerson_UpdatedById",
                table: "KvkResourcePersons",
                newName: "IX_KvkResourcePersons_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_KvkResourcePerson_ProgramContentAndResourcesId",
                table: "KvkResourcePersons",
                newName: "IX_KvkResourcePersons_ProgramContentAndResourcesId");

            migrationBuilder.RenameIndex(
                name: "IX_KvkResourcePerson_CreatedById",
                table: "KvkResourcePersons",
                newName: "IX_KvkResourcePersons_CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_KvkRecommendation_UpdatedById",
                table: "KvkRecommendations",
                newName: "IX_KvkRecommendations_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_KvkRecommendation_KVKProgramDetailsId",
                table: "KvkRecommendations",
                newName: "IX_KvkRecommendations_KVKProgramDetailsId");

            migrationBuilder.RenameIndex(
                name: "IX_KvkRecommendation_CreatedById",
                table: "KvkRecommendations",
                newName: "IX_KvkRecommendations_CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_FTIResourcePerson_UpdatedById",
                table: "FTIResourcePersons",
                newName: "IX_FTIResourcePersons_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_FTIResourcePerson_FTIProgramContentAndResourcesID",
                table: "FTIResourcePersons",
                newName: "IX_FTIResourcePersons_FTIProgramContentAndResourcesID");

            migrationBuilder.RenameIndex(
                name: "IX_FTIResourcePerson_CreatedById",
                table: "FTIResourcePersons",
                newName: "IX_FTIResourcePersons_CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_FTIRecommendation_UpdatedById",
                table: "FTIRecommendations",
                newName: "IX_FTIRecommendations_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_FTIRecommendation_FTIProgramDetailsID",
                table: "FTIRecommendations",
                newName: "IX_FTIRecommendations_FTIProgramDetailsID");

            migrationBuilder.RenameIndex(
                name: "IX_FTIRecommendation_CreatedById",
                table: "FTIRecommendations",
                newName: "IX_FTIRecommendations_CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_FIUResourcePerson_UpdatedById",
                table: "FIUResourcePersons",
                newName: "IX_FIUResourcePersons_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_FIUResourcePerson_FIUProgramContentAndResourcesID",
                table: "FIUResourcePersons",
                newName: "IX_FIUResourcePersons_FIUProgramContentAndResourcesID");

            migrationBuilder.RenameIndex(
                name: "IX_FIUResourcePerson_CreatedById",
                table: "FIUResourcePersons",
                newName: "IX_FIUResourcePersons_CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_FIURecommendation_UpdatedById",
                table: "FIURecommendations",
                newName: "IX_FIURecommendations_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_FIURecommendation_FIUProgramDetailsID",
                table: "FIURecommendations",
                newName: "IX_FIURecommendations_FIUProgramDetailsID");

            migrationBuilder.RenameIndex(
                name: "IX_FIURecommendation_CreatedById",
                table: "FIURecommendations",
                newName: "IX_FIURecommendations_CreatedById");

            migrationBuilder.AddColumn<int>(
                name: "ModeOrOutageId",
                table: "TableConsultingAndSocialMediaServices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OtherActivities",
                table: "OtherActivities",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_KVKProgramDetails",
                table: "KVKProgramDetails",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_STUResourcePersons",
                table: "STUResourcePersons",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_STURecommendations",
                table: "STURecommendations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_KvkResourcePersons",
                table: "KvkResourcePersons",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_KvkRecommendations",
                table: "KvkRecommendations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FTIResourcePersons",
                table: "FTIResourcePersons",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FTIRecommendations",
                table: "FTIRecommendations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FIUResourcePersons",
                table: "FIUResourcePersons",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FIURecommendations",
                table: "FIURecommendations",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "DeuProgramDetails",
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
                    SponsoredOrganization = table.Column<int>(type: "int", maxLength: 200, nullable: false),
                    SponsoredOrganizationName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Mode = table.Column<int>(type: "int", maxLength: 100, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Duration = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RegionId = table.Column<int>(type: "int", maxLength: 150, nullable: true),
                    RegionOther = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    TPNo = table.Column<int>(type: "int", maxLength: 100, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    SourceOfFundId = table.Column<int>(type: "int", nullable: true),
                    Funds = table.Column<int>(type: "int", nullable: true),
                    StatusId = table.Column<int>(type: "int", maxLength: 100, nullable: true),
                    Copi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalOutlayRs = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BatchNo = table.Column<int>(type: "int", maxLength: 100, nullable: true),
                    OrganizerBroucherFile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrganizerInstitutionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrganizerInstitutionAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SourceId = table.Column<int>(type: "int", nullable: true),
                    ProposalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProposalUploadFile = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    UniversitySanctionLetterDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UniversitySanctionLetterUploadFile = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    FundsSanctionLetterDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FundsSanctionLetterUploadFile = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Attachements = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnitLocationId = table.Column<int>(type: "int", nullable: false),
                    OrganizationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeuProgramDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeuProgramDetails_InfoTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "InfoTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DeuProgramDetails_OrganizationUnitLocations_UnitLocationId",
                        column: x => x.UnitLocationId,
                        principalTable: "OrganizationUnitLocations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DeuProgramDetails_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DeuProgramDetails_ParticipatedSources_SourceId",
                        column: x => x.SourceId,
                        principalTable: "ParticipatedSources",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DeuProgramDetails_ProgramCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ProgramCategories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DeuProgramDetails_ProgramTypes_ProgramTypeId",
                        column: x => x.ProgramTypeId,
                        principalTable: "ProgramTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DeuProgramDetails_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DeuProgramDetails_SourcesOfFunds_SourceOfFundId",
                        column: x => x.SourceOfFundId,
                        principalTable: "SourcesOfFunds",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DeuProgramDetails_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DeuProgramDetails_ThematicAreas_ThematicAreaId",
                        column: x => x.ThematicAreaId,
                        principalTable: "ThematicAreas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DeuProgramDetails_Themes_ThemeId",
                        column: x => x.ThemeId,
                        principalTable: "Themes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DeuProgramDetails_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DeuProgramDetails_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EeuProgramDetails",
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
                    SponsoredOrganization = table.Column<int>(type: "int", maxLength: 200, nullable: false),
                    SponsoredOrganizationName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Mode = table.Column<int>(type: "int", maxLength: 100, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Duration = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RegionId = table.Column<int>(type: "int", maxLength: 150, nullable: true),
                    RegionOther = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    TPNo = table.Column<int>(type: "int", maxLength: 100, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    TotalOutlayRs = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SourceOfFundId = table.Column<int>(type: "int", nullable: true),
                    Funds = table.Column<int>(type: "int", nullable: true),
                    StatusId = table.Column<int>(type: "int", maxLength: 100, nullable: true),
                    Copi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BatchNo = table.Column<int>(type: "int", maxLength: 100, nullable: true),
                    OrganizerBroucherFile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrganizerInstitutionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrganizerInstitutionAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SourceId = table.Column<int>(type: "int", nullable: true),
                    ProposalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProposalUploadFile = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    UniversitySanctionLetterDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UniversitySanctionLetterUploadFile = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    FundsSanctionLetterDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FundsSanctionLetterUploadFile = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Attachements = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnitLocationId = table.Column<int>(type: "int", nullable: false),
                    OrganizationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EeuProgramDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EeuProgramDetails_InfoTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "InfoTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EeuProgramDetails_OrganizationUnitLocations_UnitLocationId",
                        column: x => x.UnitLocationId,
                        principalTable: "OrganizationUnitLocations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EeuProgramDetails_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EeuProgramDetails_ParticipatedSources_SourceId",
                        column: x => x.SourceId,
                        principalTable: "ParticipatedSources",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EeuProgramDetails_ProgramCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ProgramCategories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EeuProgramDetails_ProgramTypes_ProgramTypeId",
                        column: x => x.ProgramTypeId,
                        principalTable: "ProgramTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EeuProgramDetails_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EeuProgramDetails_SourcesOfFunds_SourceOfFundId",
                        column: x => x.SourceOfFundId,
                        principalTable: "SourcesOfFunds",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EeuProgramDetails_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EeuProgramDetails_ThematicAreas_ThematicAreaId",
                        column: x => x.ThematicAreaId,
                        principalTable: "ThematicAreas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EeuProgramDetails_Themes_ThemeId",
                        column: x => x.ThemeId,
                        principalTable: "Themes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EeuProgramDetails_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EeuProgramDetails_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "IbtvaProgramDetails",
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
                    SponsoredOrganization = table.Column<int>(type: "int", maxLength: 200, nullable: false),
                    SponsoredOrganizationName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Mode = table.Column<int>(type: "int", maxLength: 100, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Duration = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RegionId = table.Column<int>(type: "int", maxLength: 150, nullable: true),
                    RegionOther = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    TPNo = table.Column<int>(type: "int", maxLength: 100, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    SourceOfFundId = table.Column<int>(type: "int", nullable: true),
                    Funds = table.Column<int>(type: "int", nullable: true),
                    StatusId = table.Column<int>(type: "int", maxLength: 100, nullable: true),
                    TotalOutlayRs = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Copi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BatchNo = table.Column<int>(type: "int", maxLength: 100, nullable: true),
                    OrganizerBroucherFile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrganizerInstitutionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrganizerInstitutionAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SourceId = table.Column<int>(type: "int", nullable: true),
                    ProposalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProposalUploadFile = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    UniversitySanctionLetterDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UniversitySanctionLetterUploadFile = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    FundsSanctionLetterDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FundsSanctionLetterUploadFile = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Attachements = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnitLocationId = table.Column<int>(type: "int", nullable: false),
                    OrganizationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IbtvaProgramDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IbtvaProgramDetails_InfoTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "InfoTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_IbtvaProgramDetails_OrganizationUnitLocations_UnitLocationId",
                        column: x => x.UnitLocationId,
                        principalTable: "OrganizationUnitLocations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_IbtvaProgramDetails_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_IbtvaProgramDetails_ParticipatedSources_SourceId",
                        column: x => x.SourceId,
                        principalTable: "ParticipatedSources",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_IbtvaProgramDetails_ProgramCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ProgramCategories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_IbtvaProgramDetails_ProgramTypes_ProgramTypeId",
                        column: x => x.ProgramTypeId,
                        principalTable: "ProgramTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_IbtvaProgramDetails_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_IbtvaProgramDetails_SourcesOfFunds_SourceOfFundId",
                        column: x => x.SourceOfFundId,
                        principalTable: "SourcesOfFunds",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_IbtvaProgramDetails_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_IbtvaProgramDetails_ThematicAreas_ThematicAreaId",
                        column: x => x.ThematicAreaId,
                        principalTable: "ThematicAreas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_IbtvaProgramDetails_Themes_ThemeId",
                        column: x => x.ThemeId,
                        principalTable: "Themes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_IbtvaProgramDetails_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_IbtvaProgramDetails_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DeuAdvisoryServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeuProgramDetailsId = table.Column<int>(type: "int", nullable: false),
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
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeuAdvisoryServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeuAdvisoryServices_DeuProgramDetails_DeuProgramDetailsId",
                        column: x => x.DeuProgramDetailsId,
                        principalTable: "DeuProgramDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DeuAdvisoryServices_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DeuAdvisoryServices_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DeuParticipantDemographics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeuProgramDetailsId = table.Column<int>(type: "int", nullable: false),
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
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeuParticipantDemographics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeuParticipantDemographics_DeuProgramDetails_DeuProgramDetailsId",
                        column: x => x.DeuProgramDetailsId,
                        principalTable: "DeuProgramDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DeuParticipantDemographics_ParticipantDealer_ParticipantId",
                        column: x => x.ParticipantId,
                        principalTable: "ParticipantDealer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DeuParticipantDemographics_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DeuParticipantDemographics_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DeuProgramContentAndResources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeuProgramDetailsId = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeuProgramContentAndResources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeuProgramContentAndResources_DeuProgramDetails_DeuProgramDetailsId",
                        column: x => x.DeuProgramDetailsId,
                        principalTable: "DeuProgramDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DeuProgramContentAndResources_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DeuProgramContentAndResources_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DeuRecommendation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeuProgramDetailsId = table.Column<int>(type: "int", nullable: false),
                    ProblemsIdentified = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Recommendation = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ActionTaken = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    SignificantAchievement = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    SuccessStories = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ImpactOutcome = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeuRecommendation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeuRecommendation_DeuProgramDetails_DeuProgramDetailsId",
                        column: x => x.DeuProgramDetailsId,
                        principalTable: "DeuProgramDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DeuRecommendation_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DeuRecommendation_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DeuReports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeuProgramDetailsId = table.Column<int>(type: "int", nullable: false),
                    ProgressReportReportingYear = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UploadPhoto = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    PhotosGeotaggedPhotoOrUploadPhoto = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    UploadVideo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    SignificantOutcome = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeuReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeuReports_DeuProgramDetails_DeuProgramDetailsId",
                        column: x => x.DeuProgramDetailsId,
                        principalTable: "DeuProgramDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DeuReports_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DeuReports_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EeuProgramContentAndResources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EeuProgramDetailsId = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EeuProgramContentAndResources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EeuProgramContentAndResources_DeuProgramDetails_EeuProgramDetailsId",
                        column: x => x.EeuProgramDetailsId,
                        principalTable: "DeuProgramDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EeuProgramContentAndResources_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EeuProgramContentAndResources_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EeuAdvisoryServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EeuProgramDetailsId = table.Column<int>(type: "int", nullable: false),
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
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EeuAdvisoryServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EeuAdvisoryServices_EeuProgramDetails_EeuProgramDetailsId",
                        column: x => x.EeuProgramDetailsId,
                        principalTable: "EeuProgramDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EeuAdvisoryServices_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EeuAdvisoryServices_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EeuParticipantDemographics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EeuProgramDetailsId = table.Column<int>(type: "int", nullable: false),
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
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EeuParticipantDemographics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EeuParticipantDemographics_EeuProgramDetails_EeuProgramDetailsId",
                        column: x => x.EeuProgramDetailsId,
                        principalTable: "EeuProgramDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EeuParticipantDemographics_ParticipantDealer_ParticipantId",
                        column: x => x.ParticipantId,
                        principalTable: "ParticipantDealer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EeuParticipantDemographics_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EeuParticipantDemographics_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EeuRecommendation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EeuProgramDetailsId = table.Column<int>(type: "int", nullable: false),
                    ProblemsIdentified = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Recommendation = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ActionTaken = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    SignificantAchievement = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    SuccessStories = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ImpactOutcome = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EeuRecommendation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EeuRecommendation_EeuProgramDetails_EeuProgramDetailsId",
                        column: x => x.EeuProgramDetailsId,
                        principalTable: "EeuProgramDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EeuRecommendation_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EeuRecommendation_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EeuReports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EeuProgramDetailsId = table.Column<int>(type: "int", nullable: false),
                    ProgressReportReportingYear = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UploadPhoto = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    PhotosGeotaggedPhotoOrUploadPhoto = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    UploadVideo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    SignificantOutcome = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EeuReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EeuReports_EeuProgramDetails_EeuProgramDetailsId",
                        column: x => x.EeuProgramDetailsId,
                        principalTable: "EeuProgramDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EeuReports_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EeuReports_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "IbtvaAdvisoryServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IbtvaProgramDetailsId = table.Column<int>(type: "int", nullable: false),
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
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IbtvaAdvisoryServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IbtvaAdvisoryServices_IbtvaProgramDetails_IbtvaProgramDetailsId",
                        column: x => x.IbtvaProgramDetailsId,
                        principalTable: "IbtvaProgramDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_IbtvaAdvisoryServices_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_IbtvaAdvisoryServices_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "IbtvaParticipantDemographics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IbtvaProgramDetailsId = table.Column<int>(type: "int", nullable: false),
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
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IbtvaParticipantDemographics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IbtvaParticipantDemographics_IbtvaProgramDetails_IbtvaProgramDetailsId",
                        column: x => x.IbtvaProgramDetailsId,
                        principalTable: "IbtvaProgramDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_IbtvaParticipantDemographics_ParticipantDealer_ParticipantId",
                        column: x => x.ParticipantId,
                        principalTable: "ParticipantDealer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_IbtvaParticipantDemographics_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_IbtvaParticipantDemographics_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "IbtvaProgramContentAndResources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IbtvaProgramDetailsId = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IbtvaProgramContentAndResources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IbtvaProgramContentAndResources_IbtvaProgramDetails_IbtvaProgramDetailsId",
                        column: x => x.IbtvaProgramDetailsId,
                        principalTable: "IbtvaProgramDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_IbtvaProgramContentAndResources_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_IbtvaProgramContentAndResources_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "IbtvaRecommendation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IbtvaProgramDetailsId = table.Column<int>(type: "int", nullable: false),
                    ProblemsIdentified = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Recommendation = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ActionTaken = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    SignificantAchievement = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    SuccessStories = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ImpactOutcome = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IbtvaRecommendation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IbtvaRecommendation_IbtvaProgramDetails_IbtvaProgramDetailsId",
                        column: x => x.IbtvaProgramDetailsId,
                        principalTable: "IbtvaProgramDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_IbtvaRecommendation_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_IbtvaRecommendation_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "IbtvaReports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IbtvaProgramDetailsId = table.Column<int>(type: "int", nullable: false),
                    ProgressReportReportingYear = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UploadPhoto = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    PhotosGeotaggedPhotoOrUploadPhoto = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    UploadVideo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    SignificantOutcome = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IbtvaReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IbtvaReports_IbtvaProgramDetails_IbtvaProgramDetailsId",
                        column: x => x.IbtvaProgramDetailsId,
                        principalTable: "IbtvaProgramDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_IbtvaReports_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_IbtvaReports_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DeuResourcePerson",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeuProgramContentAndResourcesId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Designation = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ResourceType = table.Column<int>(type: "int", maxLength: 150, nullable: false),
                    Responsibility = table.Column<int>(type: "int", maxLength: 250, nullable: false),
                    InstitutionOrDepartment = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeuResourcePerson", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeuResourcePerson_DeuProgramContentAndResources_DeuProgramContentAndResourcesId",
                        column: x => x.DeuProgramContentAndResourcesId,
                        principalTable: "DeuProgramContentAndResources",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DeuResourcePerson_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DeuResourcePerson_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DeuTeachingAIdsDeveloped",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeuProgramContentAndResourcesId = table.Column<int>(type: "int", nullable: false),
                    TypeOfAidId = table.Column<int>(type: "int", maxLength: 200, nullable: true),
                    Other = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Purpose = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeuTeachingAIdsDeveloped", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeuTeachingAIdsDeveloped_DeuProgramContentAndResources_DeuProgramContentAndResourcesId",
                        column: x => x.DeuProgramContentAndResourcesId,
                        principalTable: "DeuProgramContentAndResources",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DeuTeachingAIdsDeveloped_TypeOfAids_TypeOfAidId",
                        column: x => x.TypeOfAidId,
                        principalTable: "TypeOfAids",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DeuTeachingAIdsDeveloped_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DeuTeachingAIdsDeveloped_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DeuTopicsCoveredInClass",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeuProgramContentAndResourcesId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    PhotoUpload = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeuTopicsCoveredInClass", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeuTopicsCoveredInClass_DeuProgramContentAndResources_DeuProgramContentAndResourcesId",
                        column: x => x.DeuProgramContentAndResourcesId,
                        principalTable: "DeuProgramContentAndResources",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DeuTopicsCoveredInClass_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DeuTopicsCoveredInClass_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EeuResourcePerson",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EeuProgramContentAndResourcesId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Designation = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ResourceType = table.Column<int>(type: "int", maxLength: 150, nullable: false),
                    Responsibility = table.Column<int>(type: "int", maxLength: 250, nullable: false),
                    InstitutionOrDepartment = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EeuResourcePerson", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EeuResourcePerson_EeuProgramContentAndResources_EeuProgramContentAndResourcesId",
                        column: x => x.EeuProgramContentAndResourcesId,
                        principalTable: "EeuProgramContentAndResources",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EeuResourcePerson_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EeuResourcePerson_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EeuTeachingAidsDeveloped",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EeuProgramContentAndResourcesId = table.Column<int>(type: "int", nullable: false),
                    TypeOfAidId = table.Column<int>(type: "int", maxLength: 200, nullable: true),
                    Other = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Purpose = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EeuTeachingAidsDeveloped", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EeuTeachingAidsDeveloped_EeuProgramContentAndResources_EeuProgramContentAndResourcesId",
                        column: x => x.EeuProgramContentAndResourcesId,
                        principalTable: "EeuProgramContentAndResources",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EeuTeachingAidsDeveloped_TypeOfAids_TypeOfAidId",
                        column: x => x.TypeOfAidId,
                        principalTable: "TypeOfAids",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EeuTeachingAidsDeveloped_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EeuTeachingAidsDeveloped_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EeuTopicsCoveredInClass",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EeuProgramContentAndResourcesId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    PhotoUpload = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EeuTopicsCoveredInClass", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EeuTopicsCoveredInClass_EeuProgramContentAndResources_EeuProgramContentAndResourcesId",
                        column: x => x.EeuProgramContentAndResourcesId,
                        principalTable: "EeuProgramContentAndResources",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EeuTopicsCoveredInClass_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EeuTopicsCoveredInClass_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "IbtvaResourcePerson",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IbtvaProgramContentAndResourcesId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Designation = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ResourceType = table.Column<int>(type: "int", maxLength: 150, nullable: false),
                    Responsibility = table.Column<int>(type: "int", maxLength: 250, nullable: false),
                    InstitutionOrDepartment = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IbtvaResourcePerson", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IbtvaResourcePerson_IbtvaProgramContentAndResources_IbtvaProgramContentAndResourcesId",
                        column: x => x.IbtvaProgramContentAndResourcesId,
                        principalTable: "IbtvaProgramContentAndResources",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_IbtvaResourcePerson_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_IbtvaResourcePerson_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "IbtvaTeachingAIdsDeveloped",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IbtvaProgramContentAndResourcesId = table.Column<int>(type: "int", nullable: false),
                    TypeOfAidId = table.Column<int>(type: "int", maxLength: 200, nullable: true),
                    Other = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Purpose = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IbtvaTeachingAIdsDeveloped", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IbtvaTeachingAIdsDeveloped_IbtvaProgramContentAndResources_IbtvaProgramContentAndResourcesId",
                        column: x => x.IbtvaProgramContentAndResourcesId,
                        principalTable: "IbtvaProgramContentAndResources",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_IbtvaTeachingAIdsDeveloped_TypeOfAids_TypeOfAidId",
                        column: x => x.TypeOfAidId,
                        principalTable: "TypeOfAids",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_IbtvaTeachingAIdsDeveloped_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_IbtvaTeachingAIdsDeveloped_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "IbtvaTopicsCoveredInClass",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IbtvaProgramContentAndResourcesId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    PhotoUpload = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IbtvaTopicsCoveredInClass", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IbtvaTopicsCoveredInClass_IbtvaProgramContentAndResources_IbtvaProgramContentAndResourcesId",
                        column: x => x.IbtvaProgramContentAndResourcesId,
                        principalTable: "IbtvaProgramContentAndResources",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_IbtvaTopicsCoveredInClass_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_IbtvaTopicsCoveredInClass_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TableConsultingAndSocialMediaServices_ModeOrOutageId",
                table: "TableConsultingAndSocialMediaServices",
                column: "ModeOrOutageId");

            migrationBuilder.CreateIndex(
                name: "IX_PublisherDetails_PublicationId",
                table: "PublisherDetails",
                column: "PublicationId");

            migrationBuilder.CreateIndex(
                name: "IX_DeuAdvisoryServices_CreatedById",
                table: "DeuAdvisoryServices",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_DeuAdvisoryServices_DeuProgramDetailsId",
                table: "DeuAdvisoryServices",
                column: "DeuProgramDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_DeuAdvisoryServices_UpdatedById",
                table: "DeuAdvisoryServices",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_DeuParticipantDemographics_CreatedById",
                table: "DeuParticipantDemographics",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_DeuParticipantDemographics_DeuProgramDetailsId",
                table: "DeuParticipantDemographics",
                column: "DeuProgramDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_DeuParticipantDemographics_ParticipantId",
                table: "DeuParticipantDemographics",
                column: "ParticipantId");

            migrationBuilder.CreateIndex(
                name: "IX_DeuParticipantDemographics_UpdatedById",
                table: "DeuParticipantDemographics",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_DeuProgramContentAndResources_CreatedById",
                table: "DeuProgramContentAndResources",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_DeuProgramContentAndResources_DeuProgramDetailsId",
                table: "DeuProgramContentAndResources",
                column: "DeuProgramDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_DeuProgramContentAndResources_UpdatedById",
                table: "DeuProgramContentAndResources",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_DeuProgramDetails_CategoryId",
                table: "DeuProgramDetails",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_DeuProgramDetails_CreatedById",
                table: "DeuProgramDetails",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_DeuProgramDetails_OrganizationId",
                table: "DeuProgramDetails",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_DeuProgramDetails_ProgramTypeId",
                table: "DeuProgramDetails",
                column: "ProgramTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DeuProgramDetails_RegionId",
                table: "DeuProgramDetails",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_DeuProgramDetails_SourceId",
                table: "DeuProgramDetails",
                column: "SourceId");

            migrationBuilder.CreateIndex(
                name: "IX_DeuProgramDetails_SourceOfFundId",
                table: "DeuProgramDetails",
                column: "SourceOfFundId");

            migrationBuilder.CreateIndex(
                name: "IX_DeuProgramDetails_StatusId",
                table: "DeuProgramDetails",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_DeuProgramDetails_ThematicAreaId",
                table: "DeuProgramDetails",
                column: "ThematicAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_DeuProgramDetails_ThemeId",
                table: "DeuProgramDetails",
                column: "ThemeId");

            migrationBuilder.CreateIndex(
                name: "IX_DeuProgramDetails_TypeId",
                table: "DeuProgramDetails",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DeuProgramDetails_UnitLocationId",
                table: "DeuProgramDetails",
                column: "UnitLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_DeuProgramDetails_UpdatedById",
                table: "DeuProgramDetails",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_DeuRecommendation_CreatedById",
                table: "DeuRecommendation",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_DeuRecommendation_DeuProgramDetailsId",
                table: "DeuRecommendation",
                column: "DeuProgramDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_DeuRecommendation_UpdatedById",
                table: "DeuRecommendation",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_DeuReports_CreatedById",
                table: "DeuReports",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_DeuReports_DeuProgramDetailsId",
                table: "DeuReports",
                column: "DeuProgramDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_DeuReports_UpdatedById",
                table: "DeuReports",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_DeuResourcePerson_CreatedById",
                table: "DeuResourcePerson",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_DeuResourcePerson_DeuProgramContentAndResourcesId",
                table: "DeuResourcePerson",
                column: "DeuProgramContentAndResourcesId");

            migrationBuilder.CreateIndex(
                name: "IX_DeuResourcePerson_UpdatedById",
                table: "DeuResourcePerson",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_DeuTeachingAIdsDeveloped_CreatedById",
                table: "DeuTeachingAIdsDeveloped",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_DeuTeachingAIdsDeveloped_DeuProgramContentAndResourcesId",
                table: "DeuTeachingAIdsDeveloped",
                column: "DeuProgramContentAndResourcesId");

            migrationBuilder.CreateIndex(
                name: "IX_DeuTeachingAIdsDeveloped_TypeOfAidId",
                table: "DeuTeachingAIdsDeveloped",
                column: "TypeOfAidId");

            migrationBuilder.CreateIndex(
                name: "IX_DeuTeachingAIdsDeveloped_UpdatedById",
                table: "DeuTeachingAIdsDeveloped",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_DeuTopicsCoveredInClass_CreatedById",
                table: "DeuTopicsCoveredInClass",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_DeuTopicsCoveredInClass_DeuProgramContentAndResourcesId",
                table: "DeuTopicsCoveredInClass",
                column: "DeuProgramContentAndResourcesId");

            migrationBuilder.CreateIndex(
                name: "IX_DeuTopicsCoveredInClass_UpdatedById",
                table: "DeuTopicsCoveredInClass",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EeuAdvisoryServices_CreatedById",
                table: "EeuAdvisoryServices",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EeuAdvisoryServices_EeuProgramDetailsId",
                table: "EeuAdvisoryServices",
                column: "EeuProgramDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_EeuAdvisoryServices_UpdatedById",
                table: "EeuAdvisoryServices",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EeuParticipantDemographics_CreatedById",
                table: "EeuParticipantDemographics",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EeuParticipantDemographics_EeuProgramDetailsId",
                table: "EeuParticipantDemographics",
                column: "EeuProgramDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_EeuParticipantDemographics_ParticipantId",
                table: "EeuParticipantDemographics",
                column: "ParticipantId");

            migrationBuilder.CreateIndex(
                name: "IX_EeuParticipantDemographics_UpdatedById",
                table: "EeuParticipantDemographics",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EeuProgramContentAndResources_CreatedById",
                table: "EeuProgramContentAndResources",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EeuProgramContentAndResources_EeuProgramDetailsId",
                table: "EeuProgramContentAndResources",
                column: "EeuProgramDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_EeuProgramContentAndResources_UpdatedById",
                table: "EeuProgramContentAndResources",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EeuProgramDetails_CategoryId",
                table: "EeuProgramDetails",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_EeuProgramDetails_CreatedById",
                table: "EeuProgramDetails",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EeuProgramDetails_OrganizationId",
                table: "EeuProgramDetails",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_EeuProgramDetails_ProgramTypeId",
                table: "EeuProgramDetails",
                column: "ProgramTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EeuProgramDetails_RegionId",
                table: "EeuProgramDetails",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_EeuProgramDetails_SourceId",
                table: "EeuProgramDetails",
                column: "SourceId");

            migrationBuilder.CreateIndex(
                name: "IX_EeuProgramDetails_SourceOfFundId",
                table: "EeuProgramDetails",
                column: "SourceOfFundId");

            migrationBuilder.CreateIndex(
                name: "IX_EeuProgramDetails_StatusId",
                table: "EeuProgramDetails",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_EeuProgramDetails_ThematicAreaId",
                table: "EeuProgramDetails",
                column: "ThematicAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_EeuProgramDetails_ThemeId",
                table: "EeuProgramDetails",
                column: "ThemeId");

            migrationBuilder.CreateIndex(
                name: "IX_EeuProgramDetails_TypeId",
                table: "EeuProgramDetails",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EeuProgramDetails_UnitLocationId",
                table: "EeuProgramDetails",
                column: "UnitLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_EeuProgramDetails_UpdatedById",
                table: "EeuProgramDetails",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EeuRecommendation_CreatedById",
                table: "EeuRecommendation",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EeuRecommendation_EeuProgramDetailsId",
                table: "EeuRecommendation",
                column: "EeuProgramDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_EeuRecommendation_UpdatedById",
                table: "EeuRecommendation",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EeuReports_CreatedById",
                table: "EeuReports",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EeuReports_EeuProgramDetailsId",
                table: "EeuReports",
                column: "EeuProgramDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_EeuReports_UpdatedById",
                table: "EeuReports",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EeuResourcePerson_CreatedById",
                table: "EeuResourcePerson",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EeuResourcePerson_EeuProgramContentAndResourcesId",
                table: "EeuResourcePerson",
                column: "EeuProgramContentAndResourcesId");

            migrationBuilder.CreateIndex(
                name: "IX_EeuResourcePerson_UpdatedById",
                table: "EeuResourcePerson",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EeuTeachingAidsDeveloped_CreatedById",
                table: "EeuTeachingAidsDeveloped",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EeuTeachingAidsDeveloped_EeuProgramContentAndResourcesId",
                table: "EeuTeachingAidsDeveloped",
                column: "EeuProgramContentAndResourcesId");

            migrationBuilder.CreateIndex(
                name: "IX_EeuTeachingAidsDeveloped_TypeOfAidId",
                table: "EeuTeachingAidsDeveloped",
                column: "TypeOfAidId");

            migrationBuilder.CreateIndex(
                name: "IX_EeuTeachingAidsDeveloped_UpdatedById",
                table: "EeuTeachingAidsDeveloped",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EeuTopicsCoveredInClass_CreatedById",
                table: "EeuTopicsCoveredInClass",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EeuTopicsCoveredInClass_EeuProgramContentAndResourcesId",
                table: "EeuTopicsCoveredInClass",
                column: "EeuProgramContentAndResourcesId");

            migrationBuilder.CreateIndex(
                name: "IX_EeuTopicsCoveredInClass_UpdatedById",
                table: "EeuTopicsCoveredInClass",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_IbtvaAdvisoryServices_CreatedById",
                table: "IbtvaAdvisoryServices",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_IbtvaAdvisoryServices_IbtvaProgramDetailsId",
                table: "IbtvaAdvisoryServices",
                column: "IbtvaProgramDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_IbtvaAdvisoryServices_UpdatedById",
                table: "IbtvaAdvisoryServices",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_IbtvaParticipantDemographics_CreatedById",
                table: "IbtvaParticipantDemographics",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_IbtvaParticipantDemographics_IbtvaProgramDetailsId",
                table: "IbtvaParticipantDemographics",
                column: "IbtvaProgramDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_IbtvaParticipantDemographics_ParticipantId",
                table: "IbtvaParticipantDemographics",
                column: "ParticipantId");

            migrationBuilder.CreateIndex(
                name: "IX_IbtvaParticipantDemographics_UpdatedById",
                table: "IbtvaParticipantDemographics",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_IbtvaProgramContentAndResources_CreatedById",
                table: "IbtvaProgramContentAndResources",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_IbtvaProgramContentAndResources_IbtvaProgramDetailsId",
                table: "IbtvaProgramContentAndResources",
                column: "IbtvaProgramDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_IbtvaProgramContentAndResources_UpdatedById",
                table: "IbtvaProgramContentAndResources",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_IbtvaProgramDetails_CategoryId",
                table: "IbtvaProgramDetails",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_IbtvaProgramDetails_CreatedById",
                table: "IbtvaProgramDetails",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_IbtvaProgramDetails_OrganizationId",
                table: "IbtvaProgramDetails",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_IbtvaProgramDetails_ProgramTypeId",
                table: "IbtvaProgramDetails",
                column: "ProgramTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_IbtvaProgramDetails_RegionId",
                table: "IbtvaProgramDetails",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_IbtvaProgramDetails_SourceId",
                table: "IbtvaProgramDetails",
                column: "SourceId");

            migrationBuilder.CreateIndex(
                name: "IX_IbtvaProgramDetails_SourceOfFundId",
                table: "IbtvaProgramDetails",
                column: "SourceOfFundId");

            migrationBuilder.CreateIndex(
                name: "IX_IbtvaProgramDetails_StatusId",
                table: "IbtvaProgramDetails",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_IbtvaProgramDetails_ThematicAreaId",
                table: "IbtvaProgramDetails",
                column: "ThematicAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_IbtvaProgramDetails_ThemeId",
                table: "IbtvaProgramDetails",
                column: "ThemeId");

            migrationBuilder.CreateIndex(
                name: "IX_IbtvaProgramDetails_TypeId",
                table: "IbtvaProgramDetails",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_IbtvaProgramDetails_UnitLocationId",
                table: "IbtvaProgramDetails",
                column: "UnitLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_IbtvaProgramDetails_UpdatedById",
                table: "IbtvaProgramDetails",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_IbtvaRecommendation_CreatedById",
                table: "IbtvaRecommendation",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_IbtvaRecommendation_IbtvaProgramDetailsId",
                table: "IbtvaRecommendation",
                column: "IbtvaProgramDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_IbtvaRecommendation_UpdatedById",
                table: "IbtvaRecommendation",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_IbtvaReports_CreatedById",
                table: "IbtvaReports",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_IbtvaReports_IbtvaProgramDetailsId",
                table: "IbtvaReports",
                column: "IbtvaProgramDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_IbtvaReports_UpdatedById",
                table: "IbtvaReports",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_IbtvaResourcePerson_CreatedById",
                table: "IbtvaResourcePerson",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_IbtvaResourcePerson_IbtvaProgramContentAndResourcesId",
                table: "IbtvaResourcePerson",
                column: "IbtvaProgramContentAndResourcesId");

            migrationBuilder.CreateIndex(
                name: "IX_IbtvaResourcePerson_UpdatedById",
                table: "IbtvaResourcePerson",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_IbtvaTeachingAIdsDeveloped_CreatedById",
                table: "IbtvaTeachingAIdsDeveloped",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_IbtvaTeachingAIdsDeveloped_IbtvaProgramContentAndResourcesId",
                table: "IbtvaTeachingAIdsDeveloped",
                column: "IbtvaProgramContentAndResourcesId");

            migrationBuilder.CreateIndex(
                name: "IX_IbtvaTeachingAIdsDeveloped_TypeOfAidId",
                table: "IbtvaTeachingAIdsDeveloped",
                column: "TypeOfAidId");

            migrationBuilder.CreateIndex(
                name: "IX_IbtvaTeachingAIdsDeveloped_UpdatedById",
                table: "IbtvaTeachingAIdsDeveloped",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_IbtvaTopicsCoveredInClass_CreatedById",
                table: "IbtvaTopicsCoveredInClass",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_IbtvaTopicsCoveredInClass_IbtvaProgramContentAndResourcesId",
                table: "IbtvaTopicsCoveredInClass",
                column: "IbtvaProgramContentAndResourcesId");

            migrationBuilder.CreateIndex(
                name: "IX_IbtvaTopicsCoveredInClass_UpdatedById",
                table: "IbtvaTopicsCoveredInClass",
                column: "UpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_FIURecommendations_FIUProgramDetails_FIUProgramDetailsID",
                table: "FIURecommendations",
                column: "FIUProgramDetailsID",
                principalTable: "FIUProgramDetails",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FIURecommendations_Users_CreatedById",
                table: "FIURecommendations",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FIURecommendations_Users_UpdatedById",
                table: "FIURecommendations",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FIUResourcePersons_FIUProgramContentAndResources_FIUProgramContentAndResourcesID",
                table: "FIUResourcePersons",
                column: "FIUProgramContentAndResourcesID",
                principalTable: "FIUProgramContentAndResources",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FIUResourcePersons_Users_CreatedById",
                table: "FIUResourcePersons",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FIUResourcePersons_Users_UpdatedById",
                table: "FIUResourcePersons",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FTIRecommendations_FTIProgramDetails_FTIProgramDetailsID",
                table: "FTIRecommendations",
                column: "FTIProgramDetailsID",
                principalTable: "FTIProgramDetails",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FTIRecommendations_Users_CreatedById",
                table: "FTIRecommendations",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FTIRecommendations_Users_UpdatedById",
                table: "FTIRecommendations",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FTIResourcePersons_FTIProgramContentAndResources_FTIProgramContentAndResourcesID",
                table: "FTIResourcePersons",
                column: "FTIProgramContentAndResourcesID",
                principalTable: "FTIProgramContentAndResources",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FTIResourcePersons_Users_CreatedById",
                table: "FTIResourcePersons",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FTIResourcePersons_Users_UpdatedById",
                table: "FTIResourcePersons",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KvkAdvisoryServices_KVKProgramDetails_KVKProgramDetailsId",
                table: "KvkAdvisoryServices",
                column: "KVKProgramDetailsId",
                principalTable: "KVKProgramDetails",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KvkParticipantDemographics_KVKProgramDetails_KVKProgramDetailsId",
                table: "KvkParticipantDemographics",
                column: "KVKProgramDetailsId",
                principalTable: "KVKProgramDetails",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KvkProgramContentAndResources_KVKProgramDetails_KVKProgramDetailsId",
                table: "KvkProgramContentAndResources",
                column: "KVKProgramDetailsId",
                principalTable: "KVKProgramDetails",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KVKProgramDetails_Users_CreatedById",
                table: "KVKProgramDetails",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KVKProgramDetails_Users_UpdatedById",
                table: "KVKProgramDetails",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KvkRecommendations_KVKProgramDetails_KVKProgramDetailsId",
                table: "KvkRecommendations",
                column: "KVKProgramDetailsId",
                principalTable: "KVKProgramDetails",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KvkRecommendations_Users_CreatedById",
                table: "KvkRecommendations",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KvkRecommendations_Users_UpdatedById",
                table: "KvkRecommendations",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KvkReports_KVKProgramDetails_KVKProgramDetailsId",
                table: "KvkReports",
                column: "KVKProgramDetailsId",
                principalTable: "KVKProgramDetails",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KvkResourcePersons_KvkProgramContentAndResources_ProgramContentAndResourcesId",
                table: "KvkResourcePersons",
                column: "ProgramContentAndResourcesId",
                principalTable: "KvkProgramContentAndResources",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KvkResourcePersons_Users_CreatedById",
                table: "KvkResourcePersons",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KvkResourcePersons_Users_UpdatedById",
                table: "KvkResourcePersons",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OtherActivities_OrganizationUnitLocations_UnitLocationId",
                table: "OtherActivities",
                column: "UnitLocationId",
                principalTable: "OrganizationUnitLocations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OtherActivities_Organizations_OrganizationId",
                table: "OtherActivities",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OtherActivities_Users_CreatedById",
                table: "OtherActivities",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OtherActivities_Users_UpdatedById",
                table: "OtherActivities",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_STURecommendations_STUProgramDetails_STUProgramDetailsID",
                table: "STURecommendations",
                column: "STUProgramDetailsID",
                principalTable: "STUProgramDetails",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_STURecommendations_Users_CreatedById",
                table: "STURecommendations",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_STURecommendations_Users_UpdatedById",
                table: "STURecommendations",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_STUResourcePersons_STUProgramContentAndResources_STUProgramContentAndResourcesID",
                table: "STUResourcePersons",
                column: "STUProgramContentAndResourcesID",
                principalTable: "STUProgramContentAndResources",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_STUResourcePersons_Users_CreatedById",
                table: "STUResourcePersons",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_STUResourcePersons_Users_UpdatedById",
                table: "STUResourcePersons",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TableConsultingAndSocialMediaServices_TableModeAndOutages_ModeOrOutageId",
                table: "TableConsultingAndSocialMediaServices",
                column: "ModeOrOutageId",
                principalTable: "TableModeAndOutages",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FIURecommendations_FIUProgramDetails_FIUProgramDetailsID",
                table: "FIURecommendations");

            migrationBuilder.DropForeignKey(
                name: "FK_FIURecommendations_Users_CreatedById",
                table: "FIURecommendations");

            migrationBuilder.DropForeignKey(
                name: "FK_FIURecommendations_Users_UpdatedById",
                table: "FIURecommendations");

            migrationBuilder.DropForeignKey(
                name: "FK_FIUResourcePersons_FIUProgramContentAndResources_FIUProgramContentAndResourcesID",
                table: "FIUResourcePersons");

            migrationBuilder.DropForeignKey(
                name: "FK_FIUResourcePersons_Users_CreatedById",
                table: "FIUResourcePersons");

            migrationBuilder.DropForeignKey(
                name: "FK_FIUResourcePersons_Users_UpdatedById",
                table: "FIUResourcePersons");

            migrationBuilder.DropForeignKey(
                name: "FK_FTIRecommendations_FTIProgramDetails_FTIProgramDetailsID",
                table: "FTIRecommendations");

            migrationBuilder.DropForeignKey(
                name: "FK_FTIRecommendations_Users_CreatedById",
                table: "FTIRecommendations");

            migrationBuilder.DropForeignKey(
                name: "FK_FTIRecommendations_Users_UpdatedById",
                table: "FTIRecommendations");

            migrationBuilder.DropForeignKey(
                name: "FK_FTIResourcePersons_FTIProgramContentAndResources_FTIProgramContentAndResourcesID",
                table: "FTIResourcePersons");

            migrationBuilder.DropForeignKey(
                name: "FK_FTIResourcePersons_Users_CreatedById",
                table: "FTIResourcePersons");

            migrationBuilder.DropForeignKey(
                name: "FK_FTIResourcePersons_Users_UpdatedById",
                table: "FTIResourcePersons");

            migrationBuilder.DropForeignKey(
                name: "FK_KvkAdvisoryServices_KVKProgramDetails_KVKProgramDetailsId",
                table: "KvkAdvisoryServices");

            migrationBuilder.DropForeignKey(
                name: "FK_KvkParticipantDemographics_KVKProgramDetails_KVKProgramDetailsId",
                table: "KvkParticipantDemographics");

            migrationBuilder.DropForeignKey(
                name: "FK_KvkProgramContentAndResources_KVKProgramDetails_KVKProgramDetailsId",
                table: "KvkProgramContentAndResources");

            migrationBuilder.DropForeignKey(
                name: "FK_KVKProgramDetails_Users_CreatedById",
                table: "KVKProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_KVKProgramDetails_Users_UpdatedById",
                table: "KVKProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_KvkRecommendations_KVKProgramDetails_KVKProgramDetailsId",
                table: "KvkRecommendations");

            migrationBuilder.DropForeignKey(
                name: "FK_KvkRecommendations_Users_CreatedById",
                table: "KvkRecommendations");

            migrationBuilder.DropForeignKey(
                name: "FK_KvkRecommendations_Users_UpdatedById",
                table: "KvkRecommendations");

            migrationBuilder.DropForeignKey(
                name: "FK_KvkReports_KVKProgramDetails_KVKProgramDetailsId",
                table: "KvkReports");

            migrationBuilder.DropForeignKey(
                name: "FK_KvkResourcePersons_KvkProgramContentAndResources_ProgramContentAndResourcesId",
                table: "KvkResourcePersons");

            migrationBuilder.DropForeignKey(
                name: "FK_KvkResourcePersons_Users_CreatedById",
                table: "KvkResourcePersons");

            migrationBuilder.DropForeignKey(
                name: "FK_KvkResourcePersons_Users_UpdatedById",
                table: "KvkResourcePersons");

            migrationBuilder.DropForeignKey(
                name: "FK_OtherActivities_OrganizationUnitLocations_UnitLocationId",
                table: "OtherActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_OtherActivities_Organizations_OrganizationId",
                table: "OtherActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_OtherActivities_Users_CreatedById",
                table: "OtherActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_OtherActivities_Users_UpdatedById",
                table: "OtherActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_STURecommendations_STUProgramDetails_STUProgramDetailsID",
                table: "STURecommendations");

            migrationBuilder.DropForeignKey(
                name: "FK_STURecommendations_Users_CreatedById",
                table: "STURecommendations");

            migrationBuilder.DropForeignKey(
                name: "FK_STURecommendations_Users_UpdatedById",
                table: "STURecommendations");

            migrationBuilder.DropForeignKey(
                name: "FK_STUResourcePersons_STUProgramContentAndResources_STUProgramContentAndResourcesID",
                table: "STUResourcePersons");

            migrationBuilder.DropForeignKey(
                name: "FK_STUResourcePersons_Users_CreatedById",
                table: "STUResourcePersons");

            migrationBuilder.DropForeignKey(
                name: "FK_STUResourcePersons_Users_UpdatedById",
                table: "STUResourcePersons");

            migrationBuilder.DropForeignKey(
                name: "FK_TableConsultingAndSocialMediaServices_TableModeAndOutages_ModeOrOutageId",
                table: "TableConsultingAndSocialMediaServices");

            migrationBuilder.DropTable(
                name: "DeuAdvisoryServices");

            migrationBuilder.DropTable(
                name: "DeuParticipantDemographics");

            migrationBuilder.DropTable(
                name: "DeuRecommendation");

            migrationBuilder.DropTable(
                name: "DeuReports");

            migrationBuilder.DropTable(
                name: "DeuResourcePerson");

            migrationBuilder.DropTable(
                name: "DeuTeachingAIdsDeveloped");

            migrationBuilder.DropTable(
                name: "DeuTopicsCoveredInClass");

            migrationBuilder.DropTable(
                name: "EeuAdvisoryServices");

            migrationBuilder.DropTable(
                name: "EeuParticipantDemographics");

            migrationBuilder.DropTable(
                name: "EeuRecommendation");

            migrationBuilder.DropTable(
                name: "EeuReports");

            migrationBuilder.DropTable(
                name: "EeuResourcePerson");

            migrationBuilder.DropTable(
                name: "EeuTeachingAidsDeveloped");

            migrationBuilder.DropTable(
                name: "EeuTopicsCoveredInClass");

            migrationBuilder.DropTable(
                name: "IbtvaAdvisoryServices");

            migrationBuilder.DropTable(
                name: "IbtvaParticipantDemographics");

            migrationBuilder.DropTable(
                name: "IbtvaRecommendation");

            migrationBuilder.DropTable(
                name: "IbtvaReports");

            migrationBuilder.DropTable(
                name: "IbtvaResourcePerson");

            migrationBuilder.DropTable(
                name: "IbtvaTeachingAIdsDeveloped");

            migrationBuilder.DropTable(
                name: "IbtvaTopicsCoveredInClass");

            migrationBuilder.DropTable(
                name: "DeuProgramContentAndResources");

            migrationBuilder.DropTable(
                name: "EeuProgramDetails");

            migrationBuilder.DropTable(
                name: "EeuProgramContentAndResources");

            migrationBuilder.DropTable(
                name: "IbtvaProgramContentAndResources");

            migrationBuilder.DropTable(
                name: "DeuProgramDetails");

            migrationBuilder.DropTable(
                name: "IbtvaProgramDetails");

            migrationBuilder.DropIndex(
                name: "IX_TableConsultingAndSocialMediaServices_ModeOrOutageId",
                table: "TableConsultingAndSocialMediaServices");

            migrationBuilder.DropIndex(
                name: "IX_PublisherDetails_PublicationId",
                table: "PublisherDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_STUResourcePersons",
                table: "STUResourcePersons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_STURecommendations",
                table: "STURecommendations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OtherActivities",
                table: "OtherActivities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_KvkResourcePersons",
                table: "KvkResourcePersons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_KvkRecommendations",
                table: "KvkRecommendations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_KVKProgramDetails",
                table: "KVKProgramDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FTIResourcePersons",
                table: "FTIResourcePersons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FTIRecommendations",
                table: "FTIRecommendations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FIUResourcePersons",
                table: "FIUResourcePersons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FIURecommendations",
                table: "FIURecommendations");

            migrationBuilder.DropColumn(
                name: "ModeOrOutageId",
                table: "TableConsultingAndSocialMediaServices");

            migrationBuilder.RenameTable(
                name: "STUResourcePersons",
                newName: "STUResourcePerson");

            migrationBuilder.RenameTable(
                name: "STURecommendations",
                newName: "STURecommendation");

            migrationBuilder.RenameTable(
                name: "OtherActivities",
                newName: "TableOtherActivity");

            migrationBuilder.RenameTable(
                name: "KvkResourcePersons",
                newName: "KvkResourcePerson");

            migrationBuilder.RenameTable(
                name: "KvkRecommendations",
                newName: "KvkRecommendation");

            migrationBuilder.RenameTable(
                name: "KVKProgramDetails",
                newName: "TableKVKProgramDetails");

            migrationBuilder.RenameTable(
                name: "FTIResourcePersons",
                newName: "FTIResourcePerson");

            migrationBuilder.RenameTable(
                name: "FTIRecommendations",
                newName: "FTIRecommendation");

            migrationBuilder.RenameTable(
                name: "FIUResourcePersons",
                newName: "FIUResourcePerson");

            migrationBuilder.RenameTable(
                name: "FIURecommendations",
                newName: "FIURecommendation");

            migrationBuilder.RenameIndex(
                name: "IX_STUResourcePersons_UpdatedById",
                table: "STUResourcePerson",
                newName: "IX_STUResourcePerson_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_STUResourcePersons_STUProgramContentAndResourcesID",
                table: "STUResourcePerson",
                newName: "IX_STUResourcePerson_STUProgramContentAndResourcesID");

            migrationBuilder.RenameIndex(
                name: "IX_STUResourcePersons_CreatedById",
                table: "STUResourcePerson",
                newName: "IX_STUResourcePerson_CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_STURecommendations_UpdatedById",
                table: "STURecommendation",
                newName: "IX_STURecommendation_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_STURecommendations_STUProgramDetailsID",
                table: "STURecommendation",
                newName: "IX_STURecommendation_STUProgramDetailsID");

            migrationBuilder.RenameIndex(
                name: "IX_STURecommendations_CreatedById",
                table: "STURecommendation",
                newName: "IX_STURecommendation_CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_OtherActivities_UpdatedById",
                table: "TableOtherActivity",
                newName: "IX_TableOtherActivity_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_OtherActivities_UnitLocationId",
                table: "TableOtherActivity",
                newName: "IX_TableOtherActivity_UnitLocationId");

            migrationBuilder.RenameIndex(
                name: "IX_OtherActivities_OrganizationId",
                table: "TableOtherActivity",
                newName: "IX_TableOtherActivity_OrganizationId");

            migrationBuilder.RenameIndex(
                name: "IX_OtherActivities_CreatedById",
                table: "TableOtherActivity",
                newName: "IX_TableOtherActivity_CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_KvkResourcePersons_UpdatedById",
                table: "KvkResourcePerson",
                newName: "IX_KvkResourcePerson_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_KvkResourcePersons_ProgramContentAndResourcesId",
                table: "KvkResourcePerson",
                newName: "IX_KvkResourcePerson_ProgramContentAndResourcesId");

            migrationBuilder.RenameIndex(
                name: "IX_KvkResourcePersons_CreatedById",
                table: "KvkResourcePerson",
                newName: "IX_KvkResourcePerson_CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_KvkRecommendations_UpdatedById",
                table: "KvkRecommendation",
                newName: "IX_KvkRecommendation_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_KvkRecommendations_KVKProgramDetailsId",
                table: "KvkRecommendation",
                newName: "IX_KvkRecommendation_KVKProgramDetailsId");

            migrationBuilder.RenameIndex(
                name: "IX_KvkRecommendations_CreatedById",
                table: "KvkRecommendation",
                newName: "IX_KvkRecommendation_CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_KVKProgramDetails_UpdatedById",
                table: "TableKVKProgramDetails",
                newName: "IX_TableKVKProgramDetails_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_KVKProgramDetails_CreatedById",
                table: "TableKVKProgramDetails",
                newName: "IX_TableKVKProgramDetails_CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_FTIResourcePersons_UpdatedById",
                table: "FTIResourcePerson",
                newName: "IX_FTIResourcePerson_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_FTIResourcePersons_FTIProgramContentAndResourcesID",
                table: "FTIResourcePerson",
                newName: "IX_FTIResourcePerson_FTIProgramContentAndResourcesID");

            migrationBuilder.RenameIndex(
                name: "IX_FTIResourcePersons_CreatedById",
                table: "FTIResourcePerson",
                newName: "IX_FTIResourcePerson_CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_FTIRecommendations_UpdatedById",
                table: "FTIRecommendation",
                newName: "IX_FTIRecommendation_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_FTIRecommendations_FTIProgramDetailsID",
                table: "FTIRecommendation",
                newName: "IX_FTIRecommendation_FTIProgramDetailsID");

            migrationBuilder.RenameIndex(
                name: "IX_FTIRecommendations_CreatedById",
                table: "FTIRecommendation",
                newName: "IX_FTIRecommendation_CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_FIUResourcePersons_UpdatedById",
                table: "FIUResourcePerson",
                newName: "IX_FIUResourcePerson_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_FIUResourcePersons_FIUProgramContentAndResourcesID",
                table: "FIUResourcePerson",
                newName: "IX_FIUResourcePerson_FIUProgramContentAndResourcesID");

            migrationBuilder.RenameIndex(
                name: "IX_FIUResourcePersons_CreatedById",
                table: "FIUResourcePerson",
                newName: "IX_FIUResourcePerson_CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_FIURecommendations_UpdatedById",
                table: "FIURecommendation",
                newName: "IX_FIURecommendation_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_FIURecommendations_FIUProgramDetailsID",
                table: "FIURecommendation",
                newName: "IX_FIURecommendation_FIUProgramDetailsID");

            migrationBuilder.RenameIndex(
                name: "IX_FIURecommendations_CreatedById",
                table: "FIURecommendation",
                newName: "IX_FIURecommendation_CreatedById");

            migrationBuilder.AddPrimaryKey(
                name: "PK_STUResourcePerson",
                table: "STUResourcePerson",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_STURecommendation",
                table: "STURecommendation",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TableOtherActivity",
                table: "TableOtherActivity",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_KvkResourcePerson",
                table: "KvkResourcePerson",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_KvkRecommendation",
                table: "KvkRecommendation",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TableKVKProgramDetails",
                table: "TableKVKProgramDetails",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FTIResourcePerson",
                table: "FTIResourcePerson",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FTIRecommendation",
                table: "FTIRecommendation",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FIUResourcePerson",
                table: "FIUResourcePerson",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FIURecommendation",
                table: "FIURecommendation",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_TableConsultingAndSocialMediaServices_Fk_ModeOrOutageId",
                table: "TableConsultingAndSocialMediaServices",
                column: "Fk_ModeOrOutageId");

            migrationBuilder.CreateIndex(
                name: "IX_PublisherDetails_PublicationId",
                table: "PublisherDetails",
                column: "PublicationId",
                unique: true,
                filter: "[PublicationId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_FIURecommendation_FIUProgramDetails_FIUProgramDetailsID",
                table: "FIURecommendation",
                column: "FIUProgramDetailsID",
                principalTable: "FIUProgramDetails",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FIURecommendation_Users_CreatedById",
                table: "FIURecommendation",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FIURecommendation_Users_UpdatedById",
                table: "FIURecommendation",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FIUResourcePerson_FIUProgramContentAndResources_FIUProgramContentAndResourcesID",
                table: "FIUResourcePerson",
                column: "FIUProgramContentAndResourcesID",
                principalTable: "FIUProgramContentAndResources",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FIUResourcePerson_Users_CreatedById",
                table: "FIUResourcePerson",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FIUResourcePerson_Users_UpdatedById",
                table: "FIUResourcePerson",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FTIRecommendation_FTIProgramDetails_FTIProgramDetailsID",
                table: "FTIRecommendation",
                column: "FTIProgramDetailsID",
                principalTable: "FTIProgramDetails",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FTIRecommendation_Users_CreatedById",
                table: "FTIRecommendation",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FTIRecommendation_Users_UpdatedById",
                table: "FTIRecommendation",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FTIResourcePerson_FTIProgramContentAndResources_FTIProgramContentAndResourcesID",
                table: "FTIResourcePerson",
                column: "FTIProgramContentAndResourcesID",
                principalTable: "FTIProgramContentAndResources",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FTIResourcePerson_Users_CreatedById",
                table: "FTIResourcePerson",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FTIResourcePerson_Users_UpdatedById",
                table: "FTIResourcePerson",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KvkAdvisoryServices_TableKVKProgramDetails_KVKProgramDetailsId",
                table: "KvkAdvisoryServices",
                column: "KVKProgramDetailsId",
                principalTable: "TableKVKProgramDetails",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KvkParticipantDemographics_TableKVKProgramDetails_KVKProgramDetailsId",
                table: "KvkParticipantDemographics",
                column: "KVKProgramDetailsId",
                principalTable: "TableKVKProgramDetails",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KvkProgramContentAndResources_TableKVKProgramDetails_KVKProgramDetailsId",
                table: "KvkProgramContentAndResources",
                column: "KVKProgramDetailsId",
                principalTable: "TableKVKProgramDetails",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KvkRecommendation_TableKVKProgramDetails_KVKProgramDetailsId",
                table: "KvkRecommendation",
                column: "KVKProgramDetailsId",
                principalTable: "TableKVKProgramDetails",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KvkRecommendation_Users_CreatedById",
                table: "KvkRecommendation",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KvkRecommendation_Users_UpdatedById",
                table: "KvkRecommendation",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KvkReports_TableKVKProgramDetails_KVKProgramDetailsId",
                table: "KvkReports",
                column: "KVKProgramDetailsId",
                principalTable: "TableKVKProgramDetails",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KvkResourcePerson_KvkProgramContentAndResources_ProgramContentAndResourcesId",
                table: "KvkResourcePerson",
                column: "ProgramContentAndResourcesId",
                principalTable: "KvkProgramContentAndResources",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KvkResourcePerson_Users_CreatedById",
                table: "KvkResourcePerson",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KvkResourcePerson_Users_UpdatedById",
                table: "KvkResourcePerson",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_STURecommendation_STUProgramDetails_STUProgramDetailsID",
                table: "STURecommendation",
                column: "STUProgramDetailsID",
                principalTable: "STUProgramDetails",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_STURecommendation_Users_CreatedById",
                table: "STURecommendation",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_STURecommendation_Users_UpdatedById",
                table: "STURecommendation",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_STUResourcePerson_STUProgramContentAndResources_STUProgramContentAndResourcesID",
                table: "STUResourcePerson",
                column: "STUProgramContentAndResourcesID",
                principalTable: "STUProgramContentAndResources",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_STUResourcePerson_Users_CreatedById",
                table: "STUResourcePerson",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_STUResourcePerson_Users_UpdatedById",
                table: "STUResourcePerson",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TableConsultingAndSocialMediaServices_TableModeAndOutages_Fk_ModeOrOutageId",
                table: "TableConsultingAndSocialMediaServices",
                column: "Fk_ModeOrOutageId",
                principalTable: "TableModeAndOutages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TableKVKProgramDetails_Users_CreatedById",
                table: "TableKVKProgramDetails",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TableKVKProgramDetails_Users_UpdatedById",
                table: "TableKVKProgramDetails",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TableOtherActivity_OrganizationUnitLocations_UnitLocationId",
                table: "TableOtherActivity",
                column: "UnitLocationId",
                principalTable: "OrganizationUnitLocations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TableOtherActivity_Organizations_OrganizationId",
                table: "TableOtherActivity",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TableOtherActivity_Users_CreatedById",
                table: "TableOtherActivity",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TableOtherActivity_Users_UpdatedById",
                table: "TableOtherActivity",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}

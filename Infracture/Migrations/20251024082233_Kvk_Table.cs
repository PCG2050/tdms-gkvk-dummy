using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Kvk_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "FK_KvkReports_KVKProgramDetails_KVKProgramDetailsId",
                table: "KvkReports");

            migrationBuilder.DropForeignKey(
                name: "FK_KvkResourcePersons_KvkProgramContentAndResources_ProgramContentAndResourcesId",
                table: "KvkResourcePersons");

            migrationBuilder.DropForeignKey(
                name: "FK_KvkTeachingAidsDeveloped_KvkProgramContentAndResources_ProgramContentAndResourcesId",
                table: "KvkTeachingAidsDeveloped");

            migrationBuilder.DropForeignKey(
                name: "FK_KvkTopicsCoveredInClass_KvkProgramContentAndResources_ProgramContentAndResourcesId",
                table: "KvkTopicsCoveredInClass");

            migrationBuilder.DropIndex(
                name: "IX_KvkReports_KVKProgramDetailsId",
                table: "KvkReports");

            migrationBuilder.DropIndex(
                name: "IX_KvkRecommendations_KVKProgramDetailsId",
                table: "KvkRecommendations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_KVKProgramDetails",
                table: "KVKProgramDetails");

            migrationBuilder.DropIndex(
                name: "IX_KvkAdvisoryServices_KVKProgramDetailsId",
                table: "KvkAdvisoryServices");

            migrationBuilder.DropColumn(
                name: "Other",
                table: "KvkTeachingAidsDeveloped");

            migrationBuilder.DropColumn(
                name: "AreaHa",
                table: "KVKProgramDetails");

            migrationBuilder.DropColumn(
                name: "Mode",
                table: "KVKProgramDetails");

            migrationBuilder.DropColumn(
                name: "OrganizerAddress",
                table: "KVKProgramDetails");

            migrationBuilder.DropColumn(
                name: "OrganizerFileUpload",
                table: "KVKProgramDetails");

            migrationBuilder.DropColumn(
                name: "PaperPosterAbstract",
                table: "KVKProgramDetails");

            migrationBuilder.DropColumn(
                name: "PaperPosterAbstractDate",
                table: "KVKProgramDetails");

            migrationBuilder.DropColumn(
                name: "PaperPosterAbstractLink",
                table: "KVKProgramDetails");

            migrationBuilder.DropColumn(
                name: "ParticipatedAs",
                table: "KVKProgramDetails");

            migrationBuilder.DropColumn(
                name: "Participation",
                table: "KVKProgramDetails");

            migrationBuilder.DropColumn(
                name: "ParticipationFileLink",
                table: "KVKProgramDetails");

            migrationBuilder.DropColumn(
                name: "SourceOfInformation",
                table: "KVKProgramDetails");

            migrationBuilder.DropColumn(
                name: "TitleOfThesisOrProjectOrPaperOrOthers",
                table: "KVKProgramDetails");

            migrationBuilder.DropColumn(
                name: "UploadVideo",
                table: "KVKProgramDetails");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "KvkProgramContentAndResources");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "KvkProgramContentAndResources");

            migrationBuilder.RenameTable(
                name: "KVKProgramDetails",
                newName: "KvkProgramDetails");

            migrationBuilder.RenameColumn(
                name: "ProgramContentAndResourcesId",
                table: "KvkTopicsCoveredInClass",
                newName: "KvkProgramContentAndResourcesId");

            migrationBuilder.RenameIndex(
                name: "IX_KvkTopicsCoveredInClass_ProgramContentAndResourcesId",
                table: "KvkTopicsCoveredInClass",
                newName: "IX_KvkTopicsCoveredInClass_KvkProgramContentAndResourcesId");

            migrationBuilder.RenameColumn(
                name: "TypeOfAidDeveloped",
                table: "KvkTeachingAidsDeveloped",
                newName: "OtherTypeOfAid");

            migrationBuilder.RenameColumn(
                name: "ProgramContentAndResourcesId",
                table: "KvkTeachingAidsDeveloped",
                newName: "KvkProgramContentAndResourcesId");

            migrationBuilder.RenameIndex(
                name: "IX_KvkTeachingAidsDeveloped_ProgramContentAndResourcesId",
                table: "KvkTeachingAidsDeveloped",
                newName: "IX_KvkTeachingAidsDeveloped_KvkProgramContentAndResourcesId");

            migrationBuilder.RenameColumn(
                name: "ProgramContentAndResourcesId",
                table: "KvkResourcePersons",
                newName: "KvkProgramContentAndResourcesId");

            migrationBuilder.RenameIndex(
                name: "IX_KvkResourcePersons_ProgramContentAndResourcesId",
                table: "KvkResourcePersons",
                newName: "IX_KvkResourcePersons_KvkProgramContentAndResourcesId");

            migrationBuilder.RenameColumn(
                name: "KVKProgramDetailsId",
                table: "KvkReports",
                newName: "KvkProgramDetailsId");

            migrationBuilder.RenameColumn(
                name: "KVKProgramDetailsId",
                table: "KvkRecommendations",
                newName: "KvkProgramDetailsId");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "KvkProgramDetails",
                newName: "TypeId");

            migrationBuilder.RenameColumn(
                name: "Theme",
                table: "KvkProgramDetails",
                newName: "ThemeId");

            migrationBuilder.RenameColumn(
                name: "ThematicArea",
                table: "KvkProgramDetails",
                newName: "ThematicAreaId");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "KvkProgramDetails",
                newName: "StatusId");

            migrationBuilder.RenameColumn(
                name: "SD",
                table: "KvkProgramDetails",
                newName: "ModeId");

            migrationBuilder.RenameColumn(
                name: "Region",
                table: "KvkProgramDetails",
                newName: "RegionId");

            migrationBuilder.RenameIndex(
                name: "IX_KVKProgramDetails_UpdatedById",
                table: "KvkProgramDetails",
                newName: "IX_KvkProgramDetails_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_KVKProgramDetails_CreatedById",
                table: "KvkProgramDetails",
                newName: "IX_KvkProgramDetails_CreatedById");

            migrationBuilder.RenameColumn(
                name: "KVKProgramDetailsId",
                table: "KvkProgramContentAndResources",
                newName: "KvkProgramDetailsId");

            migrationBuilder.RenameIndex(
                name: "IX_KvkProgramContentAndResources_KVKProgramDetailsId",
                table: "KvkProgramContentAndResources",
                newName: "IX_KvkProgramContentAndResources_KvkProgramDetailsId");

            migrationBuilder.RenameColumn(
                name: "KVKProgramDetailsId",
                table: "KvkParticipantDemographics",
                newName: "KvkProgramDetailsId");

            migrationBuilder.RenameColumn(
                name: "Participant",
                table: "KvkParticipantDemographics",
                newName: "ParticipantId");

            migrationBuilder.RenameIndex(
                name: "IX_KvkParticipantDemographics_KVKProgramDetailsId",
                table: "KvkParticipantDemographics",
                newName: "IX_KvkParticipantDemographics_KvkProgramDetailsId");

            migrationBuilder.RenameColumn(
                name: "KVKProgramDetailsId",
                table: "KvkAdvisoryServices",
                newName: "KvkProgramDetailsId");

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "KvkTopicsCoveredInClass",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnitLocationId",
                table: "KvkTopicsCoveredInClass",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "KvkTeachingAidsDeveloped",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TypeOfAidId",
                table: "KvkTeachingAidsDeveloped",
                type: "int",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnitLocationId",
                table: "KvkTeachingAidsDeveloped",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "KvkResourcePersons",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnitLocationId",
                table: "KvkResourcePersons",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "KvkReports",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnitLocationId",
                table: "KvkReports",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "KvkRecommendations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnitLocationId",
                table: "KvkRecommendations",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "StartDate",
                table: "KvkProgramDetails",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProgramTypeId",
                table: "KvkProgramDetails",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "OrganizerInstitutionName",
                table: "KvkProgramDetails",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "EndDate",
                table: "KvkProgramDetails",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ApprovedAt",
                table: "KvkProgramDetails",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApprovedById",
                table: "KvkProgramDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Area",
                table: "KvkProgramDetails",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Attachements",
                table: "KvkProgramDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Copi",
                table: "KvkProgramDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FormStatus",
                table: "KvkProgramDetails",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FormStatusRemarks",
                table: "KvkProgramDetails",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Funds",
                table: "KvkProgramDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "KvkProgramDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "OrganizerBroucherFile",
                table: "KvkProgramDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrganizerInstitutionAddress",
                table: "KvkProgramDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SourceId",
                table: "KvkProgramDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SourceOfFundId",
                table: "KvkProgramDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnitLocationId",
                table: "KvkProgramDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "KvkProgramDetailsId",
                table: "KvkProgramContentAndResources",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "KvkProgramContentAndResources",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnitLocationId",
                table: "KvkProgramContentAndResources",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "KvkParticipantDemographics",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnitLocationId",
                table: "KvkParticipantDemographics",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NoOfWhatsappSMS",
                table: "KvkAdvisoryServices",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NoOfWhatsappGroups",
                table: "KvkAdvisoryServices",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NoOfSMSSentToRegisteredFarmers",
                table: "KvkAdvisoryServices",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NoOfPhoneCalls",
                table: "KvkAdvisoryServices",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NoOfNewspaperCoverage",
                table: "KvkAdvisoryServices",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NoOfGroupDiscussions",
                table: "KvkAdvisoryServices",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NoOfFacebookSMS",
                table: "KvkAdvisoryServices",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NoOfFaceToFaceDiscussions",
                table: "KvkAdvisoryServices",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NoOfEmailsSent",
                table: "KvkAdvisoryServices",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NoOfBeneficiaries",
                table: "KvkAdvisoryServices",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NoOfAnsweredWhatsappQueries",
                table: "KvkAdvisoryServices",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "KvkAdvisoryServices",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnitLocationId",
                table: "KvkAdvisoryServices",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_KvkProgramDetails",
                table: "KvkProgramDetails",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "FLDDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FLDDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KvkResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KvkProgramDetailsId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_KvkResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KvkResults_KvkProgramDetails_KvkProgramDetailsId",
                        column: x => x.KvkProgramDetailsId,
                        principalTable: "KvkProgramDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_KvkResults_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_KvkResults_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OFTDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OFTDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KvkFLDResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KvkResultId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_KvkFLDResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KvkFLDResults_FLDDetails_DetailsOfDemoId",
                        column: x => x.DetailsOfDemoId,
                        principalTable: "FLDDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_KvkFLDResults_KvkResults_KvkResultId",
                        column: x => x.KvkResultId,
                        principalTable: "KvkResults",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_KvkFLDResults_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_KvkFLDResults_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "KvkOFTResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KvkResultId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_KvkOFTResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KvkOFTResults_KvkResults_KvkResultId",
                        column: x => x.KvkResultId,
                        principalTable: "KvkResults",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_KvkOFTResults_OFTDetails_DetailsOfDemoId",
                        column: x => x.DetailsOfDemoId,
                        principalTable: "OFTDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_KvkOFTResults_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_KvkOFTResults_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_KvkTeachingAidsDeveloped_TypeOfAidId",
                table: "KvkTeachingAidsDeveloped",
                column: "TypeOfAidId");

            migrationBuilder.CreateIndex(
                name: "IX_KvkReports_KvkProgramDetailsId",
                table: "KvkReports",
                column: "KvkProgramDetailsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_KvkRecommendations_KvkProgramDetailsId",
                table: "KvkRecommendations",
                column: "KvkProgramDetailsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_KvkProgramDetails_ApprovedById",
                table: "KvkProgramDetails",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_KvkProgramDetails_CategoryId",
                table: "KvkProgramDetails",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_KvkProgramDetails_ModeId",
                table: "KvkProgramDetails",
                column: "ModeId");

            migrationBuilder.CreateIndex(
                name: "IX_KvkProgramDetails_OrganizationId",
                table: "KvkProgramDetails",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_KvkProgramDetails_ProgramTypeId",
                table: "KvkProgramDetails",
                column: "ProgramTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_KvkProgramDetails_RegionId",
                table: "KvkProgramDetails",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_KvkProgramDetails_SourceId",
                table: "KvkProgramDetails",
                column: "SourceId");

            migrationBuilder.CreateIndex(
                name: "IX_KvkProgramDetails_SourceOfFundId",
                table: "KvkProgramDetails",
                column: "SourceOfFundId");

            migrationBuilder.CreateIndex(
                name: "IX_KvkProgramDetails_StatusId",
                table: "KvkProgramDetails",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_KvkProgramDetails_ThematicAreaId",
                table: "KvkProgramDetails",
                column: "ThematicAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_KvkProgramDetails_ThemeId",
                table: "KvkProgramDetails",
                column: "ThemeId");

            migrationBuilder.CreateIndex(
                name: "IX_KvkProgramDetails_TypeId",
                table: "KvkProgramDetails",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_KvkProgramDetails_UnitLocationId",
                table: "KvkProgramDetails",
                column: "UnitLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_KvkParticipantDemographics_ParticipantId",
                table: "KvkParticipantDemographics",
                column: "ParticipantId");

            migrationBuilder.CreateIndex(
                name: "IX_KvkAdvisoryServices_KvkProgramDetailsId",
                table: "KvkAdvisoryServices",
                column: "KvkProgramDetailsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_KvkFLDResults_CreatedById",
                table: "KvkFLDResults",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_KvkFLDResults_DetailsOfDemoId",
                table: "KvkFLDResults",
                column: "DetailsOfDemoId");

            migrationBuilder.CreateIndex(
                name: "IX_KvkFLDResults_KvkResultId",
                table: "KvkFLDResults",
                column: "KvkResultId");

            migrationBuilder.CreateIndex(
                name: "IX_KvkFLDResults_UpdatedById",
                table: "KvkFLDResults",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_KvkOFTResults_CreatedById",
                table: "KvkOFTResults",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_KvkOFTResults_DetailsOfDemoId",
                table: "KvkOFTResults",
                column: "DetailsOfDemoId");

            migrationBuilder.CreateIndex(
                name: "IX_KvkOFTResults_KvkResultId",
                table: "KvkOFTResults",
                column: "KvkResultId");

            migrationBuilder.CreateIndex(
                name: "IX_KvkOFTResults_UpdatedById",
                table: "KvkOFTResults",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_KvkResults_CreatedById",
                table: "KvkResults",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_KvkResults_KvkProgramDetailsId",
                table: "KvkResults",
                column: "KvkProgramDetailsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_KvkResults_UpdatedById",
                table: "KvkResults",
                column: "UpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_KvkAdvisoryServices_KvkProgramDetails_KvkProgramDetailsId",
                table: "KvkAdvisoryServices",
                column: "KvkProgramDetailsId",
                principalTable: "KvkProgramDetails",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KvkParticipantDemographics_KvkProgramDetails_KvkProgramDetailsId",
                table: "KvkParticipantDemographics",
                column: "KvkProgramDetailsId",
                principalTable: "KvkProgramDetails",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KvkParticipantDemographics_ParticipantDealer_ParticipantId",
                table: "KvkParticipantDemographics",
                column: "ParticipantId",
                principalTable: "ParticipantDealer",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KvkProgramContentAndResources_KvkProgramDetails_KvkProgramDetailsId",
                table: "KvkProgramContentAndResources",
                column: "KvkProgramDetailsId",
                principalTable: "KvkProgramDetails",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KvkProgramDetails_InfoTypes_TypeId",
                table: "KvkProgramDetails",
                column: "TypeId",
                principalTable: "InfoTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KvkProgramDetails_Modes_ModeId",
                table: "KvkProgramDetails",
                column: "ModeId",
                principalTable: "Modes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KvkProgramDetails_OrganizationUnitLocations_UnitLocationId",
                table: "KvkProgramDetails",
                column: "UnitLocationId",
                principalTable: "OrganizationUnitLocations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KvkProgramDetails_Organizations_OrganizationId",
                table: "KvkProgramDetails",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KvkProgramDetails_ParticipatedSources_SourceId",
                table: "KvkProgramDetails",
                column: "SourceId",
                principalTable: "ParticipatedSources",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KvkProgramDetails_ProgramCategories_CategoryId",
                table: "KvkProgramDetails",
                column: "CategoryId",
                principalTable: "ProgramCategories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KvkProgramDetails_ProgramTypes_ProgramTypeId",
                table: "KvkProgramDetails",
                column: "ProgramTypeId",
                principalTable: "ProgramTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KvkProgramDetails_Regions_RegionId",
                table: "KvkProgramDetails",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KvkProgramDetails_SourcesOfFunds_SourceOfFundId",
                table: "KvkProgramDetails",
                column: "SourceOfFundId",
                principalTable: "SourcesOfFunds",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KvkProgramDetails_Statuses_StatusId",
                table: "KvkProgramDetails",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KvkProgramDetails_ThematicAreas_ThematicAreaId",
                table: "KvkProgramDetails",
                column: "ThematicAreaId",
                principalTable: "ThematicAreas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KvkProgramDetails_Themes_ThemeId",
                table: "KvkProgramDetails",
                column: "ThemeId",
                principalTable: "Themes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KvkProgramDetails_Users_ApprovedById",
                table: "KvkProgramDetails",
                column: "ApprovedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KvkProgramDetails_Users_CreatedById",
                table: "KvkProgramDetails",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KvkProgramDetails_Users_UpdatedById",
                table: "KvkProgramDetails",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KvkRecommendations_KvkProgramDetails_KvkProgramDetailsId",
                table: "KvkRecommendations",
                column: "KvkProgramDetailsId",
                principalTable: "KvkProgramDetails",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KvkReports_KvkProgramDetails_KvkProgramDetailsId",
                table: "KvkReports",
                column: "KvkProgramDetailsId",
                principalTable: "KvkProgramDetails",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KvkResourcePersons_KvkProgramContentAndResources_KvkProgramContentAndResourcesId",
                table: "KvkResourcePersons",
                column: "KvkProgramContentAndResourcesId",
                principalTable: "KvkProgramContentAndResources",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KvkTeachingAidsDeveloped_KvkProgramContentAndResources_KvkProgramContentAndResourcesId",
                table: "KvkTeachingAidsDeveloped",
                column: "KvkProgramContentAndResourcesId",
                principalTable: "KvkProgramContentAndResources",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KvkTeachingAidsDeveloped_TypeOfAids_TypeOfAidId",
                table: "KvkTeachingAidsDeveloped",
                column: "TypeOfAidId",
                principalTable: "TypeOfAids",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KvkTopicsCoveredInClass_KvkProgramContentAndResources_KvkProgramContentAndResourcesId",
                table: "KvkTopicsCoveredInClass",
                column: "KvkProgramContentAndResourcesId",
                principalTable: "KvkProgramContentAndResources",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KvkAdvisoryServices_KvkProgramDetails_KvkProgramDetailsId",
                table: "KvkAdvisoryServices");

            migrationBuilder.DropForeignKey(
                name: "FK_KvkParticipantDemographics_KvkProgramDetails_KvkProgramDetailsId",
                table: "KvkParticipantDemographics");

            migrationBuilder.DropForeignKey(
                name: "FK_KvkParticipantDemographics_ParticipantDealer_ParticipantId",
                table: "KvkParticipantDemographics");

            migrationBuilder.DropForeignKey(
                name: "FK_KvkProgramContentAndResources_KvkProgramDetails_KvkProgramDetailsId",
                table: "KvkProgramContentAndResources");

            migrationBuilder.DropForeignKey(
                name: "FK_KvkProgramDetails_InfoTypes_TypeId",
                table: "KvkProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_KvkProgramDetails_Modes_ModeId",
                table: "KvkProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_KvkProgramDetails_OrganizationUnitLocations_UnitLocationId",
                table: "KvkProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_KvkProgramDetails_Organizations_OrganizationId",
                table: "KvkProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_KvkProgramDetails_ParticipatedSources_SourceId",
                table: "KvkProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_KvkProgramDetails_ProgramCategories_CategoryId",
                table: "KvkProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_KvkProgramDetails_ProgramTypes_ProgramTypeId",
                table: "KvkProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_KvkProgramDetails_Regions_RegionId",
                table: "KvkProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_KvkProgramDetails_SourcesOfFunds_SourceOfFundId",
                table: "KvkProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_KvkProgramDetails_Statuses_StatusId",
                table: "KvkProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_KvkProgramDetails_ThematicAreas_ThematicAreaId",
                table: "KvkProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_KvkProgramDetails_Themes_ThemeId",
                table: "KvkProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_KvkProgramDetails_Users_ApprovedById",
                table: "KvkProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_KvkProgramDetails_Users_CreatedById",
                table: "KvkProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_KvkProgramDetails_Users_UpdatedById",
                table: "KvkProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_KvkRecommendations_KvkProgramDetails_KvkProgramDetailsId",
                table: "KvkRecommendations");

            migrationBuilder.DropForeignKey(
                name: "FK_KvkReports_KvkProgramDetails_KvkProgramDetailsId",
                table: "KvkReports");

            migrationBuilder.DropForeignKey(
                name: "FK_KvkResourcePersons_KvkProgramContentAndResources_KvkProgramContentAndResourcesId",
                table: "KvkResourcePersons");

            migrationBuilder.DropForeignKey(
                name: "FK_KvkTeachingAidsDeveloped_KvkProgramContentAndResources_KvkProgramContentAndResourcesId",
                table: "KvkTeachingAidsDeveloped");

            migrationBuilder.DropForeignKey(
                name: "FK_KvkTeachingAidsDeveloped_TypeOfAids_TypeOfAidId",
                table: "KvkTeachingAidsDeveloped");

            migrationBuilder.DropForeignKey(
                name: "FK_KvkTopicsCoveredInClass_KvkProgramContentAndResources_KvkProgramContentAndResourcesId",
                table: "KvkTopicsCoveredInClass");

            migrationBuilder.DropTable(
                name: "KvkFLDResults");

            migrationBuilder.DropTable(
                name: "KvkOFTResults");

            migrationBuilder.DropTable(
                name: "FLDDetails");

            migrationBuilder.DropTable(
                name: "KvkResults");

            migrationBuilder.DropTable(
                name: "OFTDetails");

            migrationBuilder.DropIndex(
                name: "IX_KvkTeachingAidsDeveloped_TypeOfAidId",
                table: "KvkTeachingAidsDeveloped");

            migrationBuilder.DropIndex(
                name: "IX_KvkReports_KvkProgramDetailsId",
                table: "KvkReports");

            migrationBuilder.DropIndex(
                name: "IX_KvkRecommendations_KvkProgramDetailsId",
                table: "KvkRecommendations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_KvkProgramDetails",
                table: "KvkProgramDetails");

            migrationBuilder.DropIndex(
                name: "IX_KvkProgramDetails_ApprovedById",
                table: "KvkProgramDetails");

            migrationBuilder.DropIndex(
                name: "IX_KvkProgramDetails_CategoryId",
                table: "KvkProgramDetails");

            migrationBuilder.DropIndex(
                name: "IX_KvkProgramDetails_ModeId",
                table: "KvkProgramDetails");

            migrationBuilder.DropIndex(
                name: "IX_KvkProgramDetails_OrganizationId",
                table: "KvkProgramDetails");

            migrationBuilder.DropIndex(
                name: "IX_KvkProgramDetails_ProgramTypeId",
                table: "KvkProgramDetails");

            migrationBuilder.DropIndex(
                name: "IX_KvkProgramDetails_RegionId",
                table: "KvkProgramDetails");

            migrationBuilder.DropIndex(
                name: "IX_KvkProgramDetails_SourceId",
                table: "KvkProgramDetails");

            migrationBuilder.DropIndex(
                name: "IX_KvkProgramDetails_SourceOfFundId",
                table: "KvkProgramDetails");

            migrationBuilder.DropIndex(
                name: "IX_KvkProgramDetails_StatusId",
                table: "KvkProgramDetails");

            migrationBuilder.DropIndex(
                name: "IX_KvkProgramDetails_ThematicAreaId",
                table: "KvkProgramDetails");

            migrationBuilder.DropIndex(
                name: "IX_KvkProgramDetails_ThemeId",
                table: "KvkProgramDetails");

            migrationBuilder.DropIndex(
                name: "IX_KvkProgramDetails_TypeId",
                table: "KvkProgramDetails");

            migrationBuilder.DropIndex(
                name: "IX_KvkProgramDetails_UnitLocationId",
                table: "KvkProgramDetails");

            migrationBuilder.DropIndex(
                name: "IX_KvkParticipantDemographics_ParticipantId",
                table: "KvkParticipantDemographics");

            migrationBuilder.DropIndex(
                name: "IX_KvkAdvisoryServices_KvkProgramDetailsId",
                table: "KvkAdvisoryServices");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "KvkTopicsCoveredInClass");

            migrationBuilder.DropColumn(
                name: "UnitLocationId",
                table: "KvkTopicsCoveredInClass");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "KvkTeachingAidsDeveloped");

            migrationBuilder.DropColumn(
                name: "TypeOfAidId",
                table: "KvkTeachingAidsDeveloped");

            migrationBuilder.DropColumn(
                name: "UnitLocationId",
                table: "KvkTeachingAidsDeveloped");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "KvkResourcePersons");

            migrationBuilder.DropColumn(
                name: "UnitLocationId",
                table: "KvkResourcePersons");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "KvkReports");

            migrationBuilder.DropColumn(
                name: "UnitLocationId",
                table: "KvkReports");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "KvkRecommendations");

            migrationBuilder.DropColumn(
                name: "UnitLocationId",
                table: "KvkRecommendations");

            migrationBuilder.DropColumn(
                name: "ApprovedAt",
                table: "KvkProgramDetails");

            migrationBuilder.DropColumn(
                name: "ApprovedById",
                table: "KvkProgramDetails");

            migrationBuilder.DropColumn(
                name: "Area",
                table: "KvkProgramDetails");

            migrationBuilder.DropColumn(
                name: "Attachements",
                table: "KvkProgramDetails");

            migrationBuilder.DropColumn(
                name: "Copi",
                table: "KvkProgramDetails");

            migrationBuilder.DropColumn(
                name: "FormStatus",
                table: "KvkProgramDetails");

            migrationBuilder.DropColumn(
                name: "FormStatusRemarks",
                table: "KvkProgramDetails");

            migrationBuilder.DropColumn(
                name: "Funds",
                table: "KvkProgramDetails");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "KvkProgramDetails");

            migrationBuilder.DropColumn(
                name: "OrganizerBroucherFile",
                table: "KvkProgramDetails");

            migrationBuilder.DropColumn(
                name: "OrganizerInstitutionAddress",
                table: "KvkProgramDetails");

            migrationBuilder.DropColumn(
                name: "SourceId",
                table: "KvkProgramDetails");

            migrationBuilder.DropColumn(
                name: "SourceOfFundId",
                table: "KvkProgramDetails");

            migrationBuilder.DropColumn(
                name: "UnitLocationId",
                table: "KvkProgramDetails");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "KvkProgramContentAndResources");

            migrationBuilder.DropColumn(
                name: "UnitLocationId",
                table: "KvkProgramContentAndResources");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "KvkParticipantDemographics");

            migrationBuilder.DropColumn(
                name: "UnitLocationId",
                table: "KvkParticipantDemographics");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "KvkAdvisoryServices");

            migrationBuilder.DropColumn(
                name: "UnitLocationId",
                table: "KvkAdvisoryServices");

            migrationBuilder.RenameTable(
                name: "KvkProgramDetails",
                newName: "KVKProgramDetails");

            migrationBuilder.RenameColumn(
                name: "KvkProgramContentAndResourcesId",
                table: "KvkTopicsCoveredInClass",
                newName: "ProgramContentAndResourcesId");

            migrationBuilder.RenameIndex(
                name: "IX_KvkTopicsCoveredInClass_KvkProgramContentAndResourcesId",
                table: "KvkTopicsCoveredInClass",
                newName: "IX_KvkTopicsCoveredInClass_ProgramContentAndResourcesId");

            migrationBuilder.RenameColumn(
                name: "OtherTypeOfAid",
                table: "KvkTeachingAidsDeveloped",
                newName: "TypeOfAidDeveloped");

            migrationBuilder.RenameColumn(
                name: "KvkProgramContentAndResourcesId",
                table: "KvkTeachingAidsDeveloped",
                newName: "ProgramContentAndResourcesId");

            migrationBuilder.RenameIndex(
                name: "IX_KvkTeachingAidsDeveloped_KvkProgramContentAndResourcesId",
                table: "KvkTeachingAidsDeveloped",
                newName: "IX_KvkTeachingAidsDeveloped_ProgramContentAndResourcesId");

            migrationBuilder.RenameColumn(
                name: "KvkProgramContentAndResourcesId",
                table: "KvkResourcePersons",
                newName: "ProgramContentAndResourcesId");

            migrationBuilder.RenameIndex(
                name: "IX_KvkResourcePersons_KvkProgramContentAndResourcesId",
                table: "KvkResourcePersons",
                newName: "IX_KvkResourcePersons_ProgramContentAndResourcesId");

            migrationBuilder.RenameColumn(
                name: "KvkProgramDetailsId",
                table: "KvkReports",
                newName: "KVKProgramDetailsId");

            migrationBuilder.RenameColumn(
                name: "KvkProgramDetailsId",
                table: "KvkRecommendations",
                newName: "KVKProgramDetailsId");

            migrationBuilder.RenameColumn(
                name: "TypeId",
                table: "KVKProgramDetails",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "ThemeId",
                table: "KVKProgramDetails",
                newName: "Theme");

            migrationBuilder.RenameColumn(
                name: "ThematicAreaId",
                table: "KVKProgramDetails",
                newName: "ThematicArea");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "KVKProgramDetails",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "RegionId",
                table: "KVKProgramDetails",
                newName: "Region");

            migrationBuilder.RenameColumn(
                name: "ModeId",
                table: "KVKProgramDetails",
                newName: "SD");

            migrationBuilder.RenameIndex(
                name: "IX_KvkProgramDetails_UpdatedById",
                table: "KVKProgramDetails",
                newName: "IX_KVKProgramDetails_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_KvkProgramDetails_CreatedById",
                table: "KVKProgramDetails",
                newName: "IX_KVKProgramDetails_CreatedById");

            migrationBuilder.RenameColumn(
                name: "KvkProgramDetailsId",
                table: "KvkProgramContentAndResources",
                newName: "KVKProgramDetailsId");

            migrationBuilder.RenameIndex(
                name: "IX_KvkProgramContentAndResources_KvkProgramDetailsId",
                table: "KvkProgramContentAndResources",
                newName: "IX_KvkProgramContentAndResources_KVKProgramDetailsId");

            migrationBuilder.RenameColumn(
                name: "KvkProgramDetailsId",
                table: "KvkParticipantDemographics",
                newName: "KVKProgramDetailsId");

            migrationBuilder.RenameColumn(
                name: "ParticipantId",
                table: "KvkParticipantDemographics",
                newName: "Participant");

            migrationBuilder.RenameIndex(
                name: "IX_KvkParticipantDemographics_KvkProgramDetailsId",
                table: "KvkParticipantDemographics",
                newName: "IX_KvkParticipantDemographics_KVKProgramDetailsId");

            migrationBuilder.RenameColumn(
                name: "KvkProgramDetailsId",
                table: "KvkAdvisoryServices",
                newName: "KVKProgramDetailsId");

            migrationBuilder.AddColumn<string>(
                name: "Other",
                table: "KvkTeachingAidsDeveloped",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "KVKProgramDetails",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<int>(
                name: "ProgramTypeId",
                table: "KVKProgramDetails",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OrganizerInstitutionName",
                table: "KVKProgramDetails",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "KVKProgramDetails",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddColumn<decimal>(
                name: "AreaHa",
                table: "KVKProgramDetails",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Mode",
                table: "KVKProgramDetails",
                type: "int",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrganizerAddress",
                table: "KVKProgramDetails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrganizerFileUpload",
                table: "KVKProgramDetails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaperPosterAbstract",
                table: "KVKProgramDetails",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PaperPosterAbstractDate",
                table: "KVKProgramDetails",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaperPosterAbstractLink",
                table: "KVKProgramDetails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ParticipatedAs",
                table: "KVKProgramDetails",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Participation",
                table: "KVKProgramDetails",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ParticipationFileLink",
                table: "KVKProgramDetails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SourceOfInformation",
                table: "KVKProgramDetails",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TitleOfThesisOrProjectOrPaperOrOthers",
                table: "KVKProgramDetails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UploadVideo",
                table: "KVKProgramDetails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "KVKProgramDetailsId",
                table: "KvkProgramContentAndResources",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "KvkProgramContentAndResources",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "KvkProgramContentAndResources",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NoOfWhatsappSMS",
                table: "KvkAdvisoryServices",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "NoOfWhatsappGroups",
                table: "KvkAdvisoryServices",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "NoOfSMSSentToRegisteredFarmers",
                table: "KvkAdvisoryServices",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "NoOfPhoneCalls",
                table: "KvkAdvisoryServices",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "NoOfNewspaperCoverage",
                table: "KvkAdvisoryServices",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "NoOfGroupDiscussions",
                table: "KvkAdvisoryServices",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "NoOfFacebookSMS",
                table: "KvkAdvisoryServices",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "NoOfFaceToFaceDiscussions",
                table: "KvkAdvisoryServices",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "NoOfEmailsSent",
                table: "KvkAdvisoryServices",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "NoOfBeneficiaries",
                table: "KvkAdvisoryServices",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "NoOfAnsweredWhatsappQueries",
                table: "KvkAdvisoryServices",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_KVKProgramDetails",
                table: "KVKProgramDetails",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_KvkReports_KVKProgramDetailsId",
                table: "KvkReports",
                column: "KVKProgramDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_KvkRecommendations_KVKProgramDetailsId",
                table: "KvkRecommendations",
                column: "KVKProgramDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_KvkAdvisoryServices_KVKProgramDetailsId",
                table: "KvkAdvisoryServices",
                column: "KVKProgramDetailsId");

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
                name: "FK_KvkTeachingAidsDeveloped_KvkProgramContentAndResources_ProgramContentAndResourcesId",
                table: "KvkTeachingAidsDeveloped",
                column: "ProgramContentAndResourcesId",
                principalTable: "KvkProgramContentAndResources",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KvkTopicsCoveredInClass_KvkProgramContentAndResources_ProgramContentAndResourcesId",
                table: "KvkTopicsCoveredInClass",
                column: "ProgramContentAndResourcesId",
                principalTable: "KvkProgramContentAndResources",
                principalColumn: "Id");
        }
    }
}

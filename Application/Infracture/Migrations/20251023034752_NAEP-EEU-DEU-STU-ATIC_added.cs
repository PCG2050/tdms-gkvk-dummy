using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NAEPEEUDEUSTUATIC_added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AticAdvisoryServices_OrganizationUnitLocations_UnitLocationId",
                table: "AticAdvisoryServices");

            migrationBuilder.DropForeignKey(
                name: "FK_AticAdvisoryServices_Organizations_OrganizationId",
                table: "AticAdvisoryServices");

            migrationBuilder.DropForeignKey(
                name: "FK_AticAdvisoryServices_Users_ApprovedById",
                table: "AticAdvisoryServices");

            migrationBuilder.DropForeignKey(
                name: "FK_AticSales_OrganizationUnitLocations_UnitLocationId",
                table: "AticSales");

            migrationBuilder.DropForeignKey(
                name: "FK_AticSales_Organizations_OrganizationId",
                table: "AticSales");

            migrationBuilder.DropForeignKey(
                name: "FK_EeuProgramContentAndResources_DeuProgramDetails_EeuProgramDetailsId",
                table: "EeuProgramContentAndResources");

            migrationBuilder.DropForeignKey(
                name: "FK_STUAdvisoryServices_STUProgramDetails_STUProgramDetailsID",
                table: "STUAdvisoryServices");

            migrationBuilder.DropForeignKey(
                name: "FK_STUAdvisoryServices_Users_CreatedById",
                table: "STUAdvisoryServices");

            migrationBuilder.DropForeignKey(
                name: "FK_STUAdvisoryServices_Users_UpdatedById",
                table: "STUAdvisoryServices");

            migrationBuilder.DropForeignKey(
                name: "FK_STUParticipantDemographics_STUProgramDetails_STUProgramDetailsID",
                table: "STUParticipantDemographics");

            migrationBuilder.DropForeignKey(
                name: "FK_STUParticipantDemographics_Users_CreatedById",
                table: "STUParticipantDemographics");

            migrationBuilder.DropForeignKey(
                name: "FK_STUParticipantDemographics_Users_UpdatedById",
                table: "STUParticipantDemographics");

            migrationBuilder.DropForeignKey(
                name: "FK_STUProgramContentAndResources_STUProgramDetails_STUProgramDetailsID",
                table: "STUProgramContentAndResources");

            migrationBuilder.DropForeignKey(
                name: "FK_STUProgramContentAndResources_Users_CreatedById",
                table: "STUProgramContentAndResources");

            migrationBuilder.DropForeignKey(
                name: "FK_STUProgramContentAndResources_Users_UpdatedById",
                table: "STUProgramContentAndResources");

            migrationBuilder.DropForeignKey(
                name: "FK_STUProgramDetails_Users_CreatedById",
                table: "STUProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_STUProgramDetails_Users_UpdatedById",
                table: "STUProgramDetails");

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
                name: "FK_STUReports_STUProgramDetails_STUProgramDetailsID",
                table: "STUReports");

            migrationBuilder.DropForeignKey(
                name: "FK_STUReports_Users_CreatedById",
                table: "STUReports");

            migrationBuilder.DropForeignKey(
                name: "FK_STUReports_Users_UpdatedById",
                table: "STUReports");

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
                name: "FK_STUTeachingAidsDeveloped_STUProgramContentAndResources_STUProgramContentAndResourcesID",
                table: "STUTeachingAidsDeveloped");

            migrationBuilder.DropForeignKey(
                name: "FK_STUTeachingAidsDeveloped_Users_CreatedById",
                table: "STUTeachingAidsDeveloped");

            migrationBuilder.DropForeignKey(
                name: "FK_STUTeachingAidsDeveloped_Users_UpdatedById",
                table: "STUTeachingAidsDeveloped");

            migrationBuilder.DropForeignKey(
                name: "FK_STUTopicsCoveredInClass_STUProgramContentAndResources_STUProgramContentAndResourcesID",
                table: "STUTopicsCoveredInClass");

            migrationBuilder.DropForeignKey(
                name: "FK_STUTopicsCoveredInClass_Users_CreatedById",
                table: "STUTopicsCoveredInClass");

            migrationBuilder.DropForeignKey(
                name: "FK_STUTopicsCoveredInClass_Users_UpdatedById",
                table: "STUTopicsCoveredInClass");

            migrationBuilder.DropTable(
                name: "AticOtherActivities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_STUTopicsCoveredInClass",
                table: "STUTopicsCoveredInClass");

            migrationBuilder.DropPrimaryKey(
                name: "PK_STUTeachingAidsDeveloped",
                table: "STUTeachingAidsDeveloped");

            migrationBuilder.DropPrimaryKey(
                name: "PK_STUResourcePersons",
                table: "STUResourcePersons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_STUReports",
                table: "STUReports");

            migrationBuilder.DropIndex(
                name: "IX_STUReports_STUProgramDetailsID",
                table: "STUReports");

            migrationBuilder.DropPrimaryKey(
                name: "PK_STURecommendations",
                table: "STURecommendations");

            migrationBuilder.DropIndex(
                name: "IX_STURecommendations_STUProgramDetailsID",
                table: "STURecommendations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_STUProgramDetails",
                table: "STUProgramDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_STUProgramContentAndResources",
                table: "STUProgramContentAndResources");

            migrationBuilder.DropPrimaryKey(
                name: "PK_STUParticipantDemographics",
                table: "STUParticipantDemographics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_STUAdvisoryServices",
                table: "STUAdvisoryServices");

            migrationBuilder.DropIndex(
                name: "IX_STUAdvisoryServices_STUProgramDetailsID",
                table: "STUAdvisoryServices");

            migrationBuilder.DropIndex(
                name: "IX_EeuReports_EeuProgramDetailsId",
                table: "EeuReports");

            migrationBuilder.DropIndex(
                name: "IX_EeuRecommendations_EeuProgramDetailsId",
                table: "EeuRecommendations");

            migrationBuilder.DropIndex(
                name: "IX_EeuAdvisoryServices_EeuProgramDetailsId",
                table: "EeuAdvisoryServices");

            migrationBuilder.DropIndex(
                name: "IX_DeuReports_DeuProgramDetailsId",
                table: "DeuReports");

            migrationBuilder.DropIndex(
                name: "IX_DeuRecommendations_DeuProgramDetailsId",
                table: "DeuRecommendations");

            migrationBuilder.DropIndex(
                name: "IX_DeuAdvisoryServices_DeuProgramDetailsId",
                table: "DeuAdvisoryServices");

            migrationBuilder.DropIndex(
                name: "IX_AticAdvisoryServices_ApprovedById",
                table: "AticAdvisoryServices");

            migrationBuilder.DropIndex(
                name: "IX_AticAdvisoryServices_OrganizationId",
                table: "AticAdvisoryServices");

            migrationBuilder.DropIndex(
                name: "IX_AticAdvisoryServices_UnitLocationId",
                table: "AticAdvisoryServices");

            migrationBuilder.DropColumn(
                name: "Other",
                table: "STUTeachingAidsDeveloped");

            migrationBuilder.DropColumn(
                name: "AreaHa",
                table: "STUProgramDetails");

            migrationBuilder.DropColumn(
                name: "Mode",
                table: "STUProgramDetails");

            migrationBuilder.DropColumn(
                name: "OrganizerAddress",
                table: "STUProgramDetails");

            migrationBuilder.DropColumn(
                name: "OrganizerFileUpload",
                table: "STUProgramDetails");

            migrationBuilder.DropColumn(
                name: "PaperPosterAbstract",
                table: "STUProgramDetails");

            migrationBuilder.DropColumn(
                name: "PaperPosterAbstractDate",
                table: "STUProgramDetails");

            migrationBuilder.DropColumn(
                name: "PaperPosterAbstractLink",
                table: "STUProgramDetails");

            migrationBuilder.DropColumn(
                name: "ParticipatedAs",
                table: "STUProgramDetails");

            migrationBuilder.DropColumn(
                name: "Participation",
                table: "STUProgramDetails");

            migrationBuilder.DropColumn(
                name: "ParticipationFileLink",
                table: "STUProgramDetails");

            migrationBuilder.DropColumn(
                name: "SourceOfInformation",
                table: "STUProgramDetails");

            migrationBuilder.DropColumn(
                name: "TitleOfThesisOrProjectOrPaperOrOthers",
                table: "STUProgramDetails");

            migrationBuilder.DropColumn(
                name: "UploadVideo",
                table: "STUProgramDetails");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "STUProgramContentAndResources");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "STUProgramContentAndResources");

            migrationBuilder.DropColumn(
                name: "Other",
                table: "EeuTeachingAidsDeveloped");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "EeuProgramContentAndResources");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "EeuProgramContentAndResources");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "DeuProgramContentAndResources");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "DeuProgramContentAndResources");

            migrationBuilder.DropColumn(
                name: "ApprovedAt",
                table: "AticAdvisoryServices");

            migrationBuilder.DropColumn(
                name: "ApprovedById",
                table: "AticAdvisoryServices");

            migrationBuilder.DropColumn(
                name: "Attachements",
                table: "AticAdvisoryServices");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "AticAdvisoryServices");

            migrationBuilder.DropColumn(
                name: "FormStatus",
                table: "AticAdvisoryServices");

            migrationBuilder.DropColumn(
                name: "FormStatusRemarks",
                table: "AticAdvisoryServices");

            migrationBuilder.DropColumn(
                name: "ServiceType",
                table: "AticAdvisoryServices");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "AticAdvisoryServices");

            migrationBuilder.RenameTable(
                name: "STUTopicsCoveredInClass",
                newName: "StuTopicsCoveredInClass");

            migrationBuilder.RenameTable(
                name: "STUTeachingAidsDeveloped",
                newName: "StuTeachingAidsDeveloped");

            migrationBuilder.RenameTable(
                name: "STUResourcePersons",
                newName: "StuResourcePersons");

            migrationBuilder.RenameTable(
                name: "STUReports",
                newName: "StuReports");

            migrationBuilder.RenameTable(
                name: "STURecommendations",
                newName: "StuRecommendations");

            migrationBuilder.RenameTable(
                name: "STUProgramDetails",
                newName: "StuProgramDetails");

            migrationBuilder.RenameTable(
                name: "STUProgramContentAndResources",
                newName: "StuProgramContentAndResources");

            migrationBuilder.RenameTable(
                name: "STUParticipantDemographics",
                newName: "StuParticipantDemographics");

            migrationBuilder.RenameTable(
                name: "STUAdvisoryServices",
                newName: "StuAdvisoryServices");

            migrationBuilder.RenameColumn(
                name: "STUProgramContentAndResourcesID",
                table: "StuTopicsCoveredInClass",
                newName: "StuProgramContentAndResourcesId");

            migrationBuilder.RenameIndex(
                name: "IX_STUTopicsCoveredInClass_UpdatedById",
                table: "StuTopicsCoveredInClass",
                newName: "IX_StuTopicsCoveredInClass_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_STUTopicsCoveredInClass_STUProgramContentAndResourcesID",
                table: "StuTopicsCoveredInClass",
                newName: "IX_StuTopicsCoveredInClass_StuProgramContentAndResourcesId");

            migrationBuilder.RenameIndex(
                name: "IX_STUTopicsCoveredInClass_CreatedById",
                table: "StuTopicsCoveredInClass",
                newName: "IX_StuTopicsCoveredInClass_CreatedById");

            migrationBuilder.RenameColumn(
                name: "STUProgramContentAndResourcesID",
                table: "StuTeachingAidsDeveloped",
                newName: "StuProgramContentAndResourcesId");

            migrationBuilder.RenameColumn(
                name: "TypeOfAidDeveloped",
                table: "StuTeachingAidsDeveloped",
                newName: "OtherTypeOfAid");

            migrationBuilder.RenameIndex(
                name: "IX_STUTeachingAidsDeveloped_UpdatedById",
                table: "StuTeachingAidsDeveloped",
                newName: "IX_StuTeachingAidsDeveloped_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_STUTeachingAidsDeveloped_STUProgramContentAndResourcesID",
                table: "StuTeachingAidsDeveloped",
                newName: "IX_StuTeachingAidsDeveloped_StuProgramContentAndResourcesId");

            migrationBuilder.RenameIndex(
                name: "IX_STUTeachingAidsDeveloped_CreatedById",
                table: "StuTeachingAidsDeveloped",
                newName: "IX_StuTeachingAidsDeveloped_CreatedById");

            migrationBuilder.RenameColumn(
                name: "STUProgramContentAndResourcesID",
                table: "StuResourcePersons",
                newName: "StuProgramContentAndResourcesId");

            migrationBuilder.RenameIndex(
                name: "IX_STUResourcePersons_UpdatedById",
                table: "StuResourcePersons",
                newName: "IX_StuResourcePersons_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_STUResourcePersons_STUProgramContentAndResourcesID",
                table: "StuResourcePersons",
                newName: "IX_StuResourcePersons_StuProgramContentAndResourcesId");

            migrationBuilder.RenameIndex(
                name: "IX_STUResourcePersons_CreatedById",
                table: "StuResourcePersons",
                newName: "IX_StuResourcePersons_CreatedById");

            migrationBuilder.RenameColumn(
                name: "STUProgramDetailsID",
                table: "StuReports",
                newName: "StuProgramDetailsId");

            migrationBuilder.RenameIndex(
                name: "IX_STUReports_UpdatedById",
                table: "StuReports",
                newName: "IX_StuReports_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_STUReports_CreatedById",
                table: "StuReports",
                newName: "IX_StuReports_CreatedById");

            migrationBuilder.RenameColumn(
                name: "STUProgramDetailsID",
                table: "StuRecommendations",
                newName: "StuProgramDetailsId");

            migrationBuilder.RenameIndex(
                name: "IX_STURecommendations_UpdatedById",
                table: "StuRecommendations",
                newName: "IX_StuRecommendations_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_STURecommendations_CreatedById",
                table: "StuRecommendations",
                newName: "IX_StuRecommendations_CreatedById");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "StuProgramDetails",
                newName: "TypeId");

            migrationBuilder.RenameColumn(
                name: "Theme",
                table: "StuProgramDetails",
                newName: "ThemeId");

            migrationBuilder.RenameColumn(
                name: "ThematicArea",
                table: "StuProgramDetails",
                newName: "ThematicAreaId");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "StuProgramDetails",
                newName: "StatusId");

            migrationBuilder.RenameColumn(
                name: "SD",
                table: "StuProgramDetails",
                newName: "ModeId");

            migrationBuilder.RenameColumn(
                name: "Region",
                table: "StuProgramDetails",
                newName: "RegionId");

            migrationBuilder.RenameIndex(
                name: "IX_STUProgramDetails_UpdatedById",
                table: "StuProgramDetails",
                newName: "IX_StuProgramDetails_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_STUProgramDetails_CreatedById",
                table: "StuProgramDetails",
                newName: "IX_StuProgramDetails_CreatedById");

            migrationBuilder.RenameColumn(
                name: "STUProgramDetailsID",
                table: "StuProgramContentAndResources",
                newName: "StuProgramDetailsId");

            migrationBuilder.RenameIndex(
                name: "IX_STUProgramContentAndResources_UpdatedById",
                table: "StuProgramContentAndResources",
                newName: "IX_StuProgramContentAndResources_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_STUProgramContentAndResources_STUProgramDetailsID",
                table: "StuProgramContentAndResources",
                newName: "IX_StuProgramContentAndResources_StuProgramDetailsId");

            migrationBuilder.RenameIndex(
                name: "IX_STUProgramContentAndResources_CreatedById",
                table: "StuProgramContentAndResources",
                newName: "IX_StuProgramContentAndResources_CreatedById");

            migrationBuilder.RenameColumn(
                name: "STUProgramDetailsID",
                table: "StuParticipantDemographics",
                newName: "StuProgramDetailsId");

            migrationBuilder.RenameColumn(
                name: "Participant",
                table: "StuParticipantDemographics",
                newName: "ParticipantId");

            migrationBuilder.RenameIndex(
                name: "IX_STUParticipantDemographics_UpdatedById",
                table: "StuParticipantDemographics",
                newName: "IX_StuParticipantDemographics_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_STUParticipantDemographics_STUProgramDetailsID",
                table: "StuParticipantDemographics",
                newName: "IX_StuParticipantDemographics_StuProgramDetailsId");

            migrationBuilder.RenameIndex(
                name: "IX_STUParticipantDemographics_CreatedById",
                table: "StuParticipantDemographics",
                newName: "IX_StuParticipantDemographics_CreatedById");

            migrationBuilder.RenameColumn(
                name: "STUProgramDetailsID",
                table: "StuAdvisoryServices",
                newName: "StuProgramDetailsId");

            migrationBuilder.RenameIndex(
                name: "IX_STUAdvisoryServices_UpdatedById",
                table: "StuAdvisoryServices",
                newName: "IX_StuAdvisoryServices_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_STUAdvisoryServices_CreatedById",
                table: "StuAdvisoryServices",
                newName: "IX_StuAdvisoryServices_CreatedById");

            migrationBuilder.RenameColumn(
                name: "Mode",
                table: "EeuProgramDetails",
                newName: "ModeId");

            migrationBuilder.RenameColumn(
                name: "Other",
                table: "DeuTeachingAidsDeveloped",
                newName: "OtherTypeOfAid");

            migrationBuilder.RenameColumn(
                name: "Mode",
                table: "DeuProgramDetails",
                newName: "ModeId");

            migrationBuilder.RenameColumn(
                name: "ServiceCount",
                table: "AticAdvisoryServices",
                newName: "NoOfWhatsappSMS");

            migrationBuilder.RenameColumn(
                name: "BeneficiaryCount",
                table: "AticAdvisoryServices",
                newName: "NoOfWhatsappGroups");

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "StuTopicsCoveredInClass",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnitLocationId",
                table: "StuTopicsCoveredInClass",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "StuTeachingAidsDeveloped",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TypeOfAidId",
                table: "StuTeachingAidsDeveloped",
                type: "int",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnitLocationId",
                table: "StuTeachingAidsDeveloped",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "StuResourcePersons",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnitLocationId",
                table: "StuResourcePersons",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "StuReports",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnitLocationId",
                table: "StuReports",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "StuRecommendations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnitLocationId",
                table: "StuRecommendations",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "StuProgramDetails",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "StartDate",
                table: "StuProgramDetails",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProgramTypeId",
                table: "StuProgramDetails",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "OrganizerInstitutionName",
                table: "StuProgramDetails",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "EndDate",
                table: "StuProgramDetails",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "StuProgramDetails",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ApprovedAt",
                table: "StuProgramDetails",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApprovedById",
                table: "StuProgramDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Attachements",
                table: "StuProgramDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Copi",
                table: "StuProgramDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FormStatus",
                table: "StuProgramDetails",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FormStatusRemarks",
                table: "StuProgramDetails",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Funds",
                table: "StuProgramDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "StuProgramDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "OrganizerBroucherFile",
                table: "StuProgramDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrganizerInstitutionAddress",
                table: "StuProgramDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SourceId",
                table: "StuProgramDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SourceOfFundId",
                table: "StuProgramDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnitLocationId",
                table: "StuProgramDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "StuProgramContentAndResources",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnitLocationId",
                table: "StuProgramContentAndResources",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "StuParticipantDemographics",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnitLocationId",
                table: "StuParticipantDemographics",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "StuAdvisoryServices",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnitLocationId",
                table: "StuAdvisoryServices",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Number",
                table: "IbtvaTeachingAidsDeveloped",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "IbtvaResourcePersons",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "IbtvaProgramDetails",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "EeuTopicsCoveredInClass",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnitLocationId",
                table: "EeuTopicsCoveredInClass",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Purpose",
                table: "EeuTeachingAidsDeveloped",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300);

            migrationBuilder.AlterColumn<int>(
                name: "Number",
                table: "EeuTeachingAidsDeveloped",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "EeuTeachingAidsDeveloped",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherTypeOfAid",
                table: "EeuTeachingAidsDeveloped",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnitLocationId",
                table: "EeuTeachingAidsDeveloped",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Responsibility",
                table: "EeuResourcePersons",
                type: "int",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<int>(
                name: "ResourceType",
                table: "EeuResourcePersons",
                type: "int",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "EeuResourcePersons",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "InstitutionOrDepartment",
                table: "EeuResourcePersons",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<string>(
                name: "Designation",
                table: "EeuResourcePersons",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "EeuResourcePersons",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnitLocationId",
                table: "EeuResourcePersons",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UploadVideo",
                table: "EeuReports",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "UploadPhoto",
                table: "EeuReports",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "SignificantOutcome",
                table: "EeuReports",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<string>(
                name: "ProgressReportReportingYear",
                table: "EeuReports",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "PhotosGeotaggedPhotoOrUploadPhoto",
                table: "EeuReports",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "EeuReports",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnitLocationId",
                table: "EeuReports",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SuccessStories",
                table: "EeuRecommendations",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<string>(
                name: "SignificantAchievement",
                table: "EeuRecommendations",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<string>(
                name: "Recommendation",
                table: "EeuRecommendations",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<string>(
                name: "ProblemsIdentified",
                table: "EeuRecommendations",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<string>(
                name: "ImpactOutcome",
                table: "EeuRecommendations",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<string>(
                name: "ActionTaken",
                table: "EeuRecommendations",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "EeuRecommendations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnitLocationId",
                table: "EeuRecommendations",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalOutlayRs",
                table: "EeuProgramDetails",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "EeuProgramDetails",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "StartDate",
                table: "EeuProgramDetails",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "EndDate",
                table: "EeuProgramDetails",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "EeuProgramContentAndResources",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnitLocationId",
                table: "EeuProgramContentAndResources",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "EeuParticipantDemographics",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnitLocationId",
                table: "EeuParticipantDemographics",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "EeuAdvisoryServices",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnitLocationId",
                table: "EeuAdvisoryServices",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "DeuTopicsCoveredInClass",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnitLocationId",
                table: "DeuTopicsCoveredInClass",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "DeuTeachingAidsDeveloped",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnitLocationId",
                table: "DeuTeachingAidsDeveloped",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "DeuResourcePersons",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnitLocationId",
                table: "DeuResourcePersons",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "DeuReports",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnitLocationId",
                table: "DeuReports",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "DeuRecommendations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnitLocationId",
                table: "DeuRecommendations",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalOutlayRs",
                table: "DeuProgramDetails",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "StartDate",
                table: "DeuProgramDetails",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "EndDate",
                table: "DeuProgramDetails",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "DeuProgramContentAndResources",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnitLocationId",
                table: "DeuProgramContentAndResources",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "DeuParticipantDemographics",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnitLocationId",
                table: "DeuParticipantDemographics",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NoOfWhatsappSMS",
                table: "DeuAdvisoryServices",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NoOfWhatsappGroups",
                table: "DeuAdvisoryServices",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NoOfSMSSentToRegisteredFarmers",
                table: "DeuAdvisoryServices",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NoOfPhoneCalls",
                table: "DeuAdvisoryServices",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NoOfNewspaperCoverage",
                table: "DeuAdvisoryServices",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NoOfGroupDiscussions",
                table: "DeuAdvisoryServices",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NoOfFacebookSMS",
                table: "DeuAdvisoryServices",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NoOfFaceToFaceDiscussions",
                table: "DeuAdvisoryServices",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NoOfEmailsSent",
                table: "DeuAdvisoryServices",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NoOfBeneficiaries",
                table: "DeuAdvisoryServices",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NoOfAnsweredWhatsappQueries",
                table: "DeuAdvisoryServices",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "DeuAdvisoryServices",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnitLocationId",
                table: "DeuAdvisoryServices",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UnitLocationId",
                table: "AticAdvisoryServices",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "OrganizationId",
                table: "AticAdvisoryServices",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "AticProgramDetailsId",
                table: "AticAdvisoryServices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NoOfAnsweredWhatsappQueries",
                table: "AticAdvisoryServices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NoOfBeneficiaries",
                table: "AticAdvisoryServices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NoOfEmailsSent",
                table: "AticAdvisoryServices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NoOfFaceToFaceDiscussions",
                table: "AticAdvisoryServices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NoOfFacebookSMS",
                table: "AticAdvisoryServices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NoOfGroupDiscussions",
                table: "AticAdvisoryServices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NoOfNewspaperCoverage",
                table: "AticAdvisoryServices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NoOfPhoneCalls",
                table: "AticAdvisoryServices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NoOfSMSSentToRegisteredFarmers",
                table: "AticAdvisoryServices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_StuTopicsCoveredInClass",
                table: "StuTopicsCoveredInClass",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StuTeachingAidsDeveloped",
                table: "StuTeachingAidsDeveloped",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StuResourcePersons",
                table: "StuResourcePersons",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StuReports",
                table: "StuReports",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StuRecommendations",
                table: "StuRecommendations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StuProgramDetails",
                table: "StuProgramDetails",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StuProgramContentAndResources",
                table: "StuProgramContentAndResources",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StuParticipantDemographics",
                table: "StuParticipantDemographics",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StuAdvisoryServices",
                table: "StuAdvisoryServices",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "AticProgramDetails",
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
                    table.PrimaryKey("PK_AticProgramDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AticProgramDetails_InfoTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "InfoTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AticProgramDetails_Modes_ModeId",
                        column: x => x.ModeId,
                        principalTable: "Modes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AticProgramDetails_OrganizationUnitLocations_UnitLocationId",
                        column: x => x.UnitLocationId,
                        principalTable: "OrganizationUnitLocations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AticProgramDetails_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AticProgramDetails_ParticipatedSources_SourceId",
                        column: x => x.SourceId,
                        principalTable: "ParticipatedSources",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AticProgramDetails_ProgramCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ProgramCategories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AticProgramDetails_ProgramTypes_ProgramTypeId",
                        column: x => x.ProgramTypeId,
                        principalTable: "ProgramTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AticProgramDetails_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AticProgramDetails_SourcesOfFunds_SourceOfFundId",
                        column: x => x.SourceOfFundId,
                        principalTable: "SourcesOfFunds",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AticProgramDetails_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AticProgramDetails_ThematicAreas_ThematicAreaId",
                        column: x => x.ThematicAreaId,
                        principalTable: "ThematicAreas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AticProgramDetails_Themes_ThemeId",
                        column: x => x.ThemeId,
                        principalTable: "Themes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AticProgramDetails_Users_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AticProgramDetails_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AticProgramDetails_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "NaepProgramDetails",
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
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
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
                    table.PrimaryKey("PK_NaepProgramDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NaepProgramDetails_InfoTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "InfoTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NaepProgramDetails_Modes_ModeId",
                        column: x => x.ModeId,
                        principalTable: "Modes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NaepProgramDetails_OrganizationUnitLocations_UnitLocationId",
                        column: x => x.UnitLocationId,
                        principalTable: "OrganizationUnitLocations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NaepProgramDetails_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NaepProgramDetails_ParticipatedSources_SourceId",
                        column: x => x.SourceId,
                        principalTable: "ParticipatedSources",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NaepProgramDetails_ProgramCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ProgramCategories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NaepProgramDetails_ProgramTypes_ProgramTypeId",
                        column: x => x.ProgramTypeId,
                        principalTable: "ProgramTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NaepProgramDetails_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NaepProgramDetails_SourcesOfFunds_SourceOfFundId",
                        column: x => x.SourceOfFundId,
                        principalTable: "SourcesOfFunds",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NaepProgramDetails_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NaepProgramDetails_ThematicAreas_ThematicAreaId",
                        column: x => x.ThematicAreaId,
                        principalTable: "ThematicAreas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NaepProgramDetails_Themes_ThemeId",
                        column: x => x.ThemeId,
                        principalTable: "Themes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NaepProgramDetails_Users_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NaepProgramDetails_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NaepProgramDetails_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AticParticipantDemographics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AticProgramDetailsId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_AticParticipantDemographics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AticParticipantDemographics_AticProgramDetails_AticProgramDetailsId",
                        column: x => x.AticProgramDetailsId,
                        principalTable: "AticProgramDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AticParticipantDemographics_ParticipantDealer_ParticipantId",
                        column: x => x.ParticipantId,
                        principalTable: "ParticipantDealer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AticParticipantDemographics_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AticParticipantDemographics_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AticProgramContentAndResources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AticProgramDetailsId = table.Column<int>(type: "int", nullable: true),
                    UnitLocationId = table.Column<int>(type: "int", nullable: true),
                    OrganizationId = table.Column<int>(type: "int", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AticProgramContentAndResources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AticProgramContentAndResources_AticProgramDetails_AticProgramDetailsId",
                        column: x => x.AticProgramDetailsId,
                        principalTable: "AticProgramDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AticProgramContentAndResources_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AticProgramContentAndResources_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AticRecommendations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AticProgramDetailsId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_AticRecommendations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AticRecommendations_AticProgramDetails_AticProgramDetailsId",
                        column: x => x.AticProgramDetailsId,
                        principalTable: "AticProgramDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AticRecommendations_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AticRecommendations_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AticReports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AticProgramDetailsId = table.Column<int>(type: "int", nullable: false),
                    ProgressReportReportingYear = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UploadPhoto = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PhotosGeotaggedPhotoOrUploadPhoto = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    UploadVideo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    SignificantOutcome = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    UnitLocationId = table.Column<int>(type: "int", nullable: true),
                    OrganizationId = table.Column<int>(type: "int", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AticReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AticReports_AticProgramDetails_AticProgramDetailsId",
                        column: x => x.AticProgramDetailsId,
                        principalTable: "AticProgramDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AticReports_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AticReports_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "NaepAdvisoryServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NaepProgramDetailsId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_NaepAdvisoryServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NaepAdvisoryServices_NaepProgramDetails_NaepProgramDetailsId",
                        column: x => x.NaepProgramDetailsId,
                        principalTable: "NaepProgramDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NaepAdvisoryServices_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NaepAdvisoryServices_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "NaepParticipantDemographics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NaepProgramDetailsId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_NaepParticipantDemographics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NaepParticipantDemographics_NaepProgramDetails_NaepProgramDetailsId",
                        column: x => x.NaepProgramDetailsId,
                        principalTable: "NaepProgramDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NaepParticipantDemographics_ParticipantDealer_ParticipantId",
                        column: x => x.ParticipantId,
                        principalTable: "ParticipantDealer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NaepParticipantDemographics_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NaepParticipantDemographics_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "NaepProgramContentAndResources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NaepProgramDetailsId = table.Column<int>(type: "int", nullable: true),
                    UnitLocationId = table.Column<int>(type: "int", nullable: true),
                    OrganizationId = table.Column<int>(type: "int", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NaepProgramContentAndResources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NaepProgramContentAndResources_NaepProgramDetails_NaepProgramDetailsId",
                        column: x => x.NaepProgramDetailsId,
                        principalTable: "NaepProgramDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NaepProgramContentAndResources_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NaepProgramContentAndResources_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "NaepRecommendations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NaepProgramDetailsId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_NaepRecommendations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NaepRecommendations_NaepProgramDetails_NaepProgramDetailsId",
                        column: x => x.NaepProgramDetailsId,
                        principalTable: "NaepProgramDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NaepRecommendations_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NaepRecommendations_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "NaepReports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NaepProgramDetailsId = table.Column<int>(type: "int", nullable: false),
                    ProgressReportReportingYear = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UploadPhoto = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PhotosGeotaggedPhotoOrUploadPhoto = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    UploadVideo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    SignificantOutcome = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    UnitLocationId = table.Column<int>(type: "int", nullable: true),
                    OrganizationId = table.Column<int>(type: "int", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NaepReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NaepReports_NaepProgramDetails_NaepProgramDetailsId",
                        column: x => x.NaepProgramDetailsId,
                        principalTable: "NaepProgramDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NaepReports_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NaepReports_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AticResourcePersons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AticProgramContentAndResourcesId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_AticResourcePersons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AticResourcePersons_AticProgramContentAndResources_AticProgramContentAndResourcesId",
                        column: x => x.AticProgramContentAndResourcesId,
                        principalTable: "AticProgramContentAndResources",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AticResourcePersons_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AticResourcePersons_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AticTeachingAidsDeveloped",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AticProgramContentAndResourcesId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_AticTeachingAidsDeveloped", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AticTeachingAidsDeveloped_AticProgramContentAndResources_AticProgramContentAndResourcesId",
                        column: x => x.AticProgramContentAndResourcesId,
                        principalTable: "AticProgramContentAndResources",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AticTeachingAidsDeveloped_TypeOfAids_TypeOfAidId",
                        column: x => x.TypeOfAidId,
                        principalTable: "TypeOfAids",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AticTeachingAidsDeveloped_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AticTeachingAidsDeveloped_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AticTopicsCoveredInClass",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AticProgramContentAndResourcesId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_AticTopicsCoveredInClass", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AticTopicsCoveredInClass_AticProgramContentAndResources_AticProgramContentAndResourcesId",
                        column: x => x.AticProgramContentAndResourcesId,
                        principalTable: "AticProgramContentAndResources",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AticTopicsCoveredInClass_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AticTopicsCoveredInClass_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "NaepResourcePersons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NaepProgramContentAndResourcesId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_NaepResourcePersons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NaepResourcePersons_NaepProgramContentAndResources_NaepProgramContentAndResourcesId",
                        column: x => x.NaepProgramContentAndResourcesId,
                        principalTable: "NaepProgramContentAndResources",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NaepResourcePersons_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NaepResourcePersons_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "NaepTeachingAidsDeveloped",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NaepProgramContentAndResourcesId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_NaepTeachingAidsDeveloped", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NaepTeachingAidsDeveloped_NaepProgramContentAndResources_NaepProgramContentAndResourcesId",
                        column: x => x.NaepProgramContentAndResourcesId,
                        principalTable: "NaepProgramContentAndResources",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NaepTeachingAidsDeveloped_TypeOfAids_TypeOfAidId",
                        column: x => x.TypeOfAidId,
                        principalTable: "TypeOfAids",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NaepTeachingAidsDeveloped_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NaepTeachingAidsDeveloped_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "NaepTopicsCoveredInClass",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NaepProgramContentAndResourcesId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_NaepTopicsCoveredInClass", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NaepTopicsCoveredInClass_NaepProgramContentAndResources_NaepProgramContentAndResourcesId",
                        column: x => x.NaepProgramContentAndResourcesId,
                        principalTable: "NaepProgramContentAndResources",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NaepTopicsCoveredInClass_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NaepTopicsCoveredInClass_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_StuTeachingAidsDeveloped_TypeOfAidId",
                table: "StuTeachingAidsDeveloped",
                column: "TypeOfAidId");

            migrationBuilder.CreateIndex(
                name: "IX_StuReports_StuProgramDetailsId",
                table: "StuReports",
                column: "StuProgramDetailsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StuRecommendations_StuProgramDetailsId",
                table: "StuRecommendations",
                column: "StuProgramDetailsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StuProgramDetails_ApprovedById",
                table: "StuProgramDetails",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_StuProgramDetails_CategoryId",
                table: "StuProgramDetails",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_StuProgramDetails_ModeId",
                table: "StuProgramDetails",
                column: "ModeId");

            migrationBuilder.CreateIndex(
                name: "IX_StuProgramDetails_OrganizationId",
                table: "StuProgramDetails",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_StuProgramDetails_ProgramTypeId",
                table: "StuProgramDetails",
                column: "ProgramTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_StuProgramDetails_RegionId",
                table: "StuProgramDetails",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_StuProgramDetails_SourceId",
                table: "StuProgramDetails",
                column: "SourceId");

            migrationBuilder.CreateIndex(
                name: "IX_StuProgramDetails_SourceOfFundId",
                table: "StuProgramDetails",
                column: "SourceOfFundId");

            migrationBuilder.CreateIndex(
                name: "IX_StuProgramDetails_StatusId",
                table: "StuProgramDetails",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_StuProgramDetails_ThematicAreaId",
                table: "StuProgramDetails",
                column: "ThematicAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_StuProgramDetails_ThemeId",
                table: "StuProgramDetails",
                column: "ThemeId");

            migrationBuilder.CreateIndex(
                name: "IX_StuProgramDetails_TypeId",
                table: "StuProgramDetails",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_StuProgramDetails_UnitLocationId",
                table: "StuProgramDetails",
                column: "UnitLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_StuParticipantDemographics_ParticipantId",
                table: "StuParticipantDemographics",
                column: "ParticipantId");

            migrationBuilder.CreateIndex(
                name: "IX_StuAdvisoryServices_StuProgramDetailsId",
                table: "StuAdvisoryServices",
                column: "StuProgramDetailsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EeuReports_EeuProgramDetailsId",
                table: "EeuReports",
                column: "EeuProgramDetailsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EeuRecommendations_EeuProgramDetailsId",
                table: "EeuRecommendations",
                column: "EeuProgramDetailsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EeuProgramDetails_ModeId",
                table: "EeuProgramDetails",
                column: "ModeId");

            migrationBuilder.CreateIndex(
                name: "IX_EeuAdvisoryServices_EeuProgramDetailsId",
                table: "EeuAdvisoryServices",
                column: "EeuProgramDetailsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeuReports_DeuProgramDetailsId",
                table: "DeuReports",
                column: "DeuProgramDetailsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeuRecommendations_DeuProgramDetailsId",
                table: "DeuRecommendations",
                column: "DeuProgramDetailsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeuProgramDetails_ModeId",
                table: "DeuProgramDetails",
                column: "ModeId");

            migrationBuilder.CreateIndex(
                name: "IX_DeuAdvisoryServices_DeuProgramDetailsId",
                table: "DeuAdvisoryServices",
                column: "DeuProgramDetailsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AticAdvisoryServices_AticProgramDetailsId",
                table: "AticAdvisoryServices",
                column: "AticProgramDetailsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AticParticipantDemographics_AticProgramDetailsId",
                table: "AticParticipantDemographics",
                column: "AticProgramDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_AticParticipantDemographics_CreatedById",
                table: "AticParticipantDemographics",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AticParticipantDemographics_ParticipantId",
                table: "AticParticipantDemographics",
                column: "ParticipantId");

            migrationBuilder.CreateIndex(
                name: "IX_AticParticipantDemographics_UpdatedById",
                table: "AticParticipantDemographics",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AticProgramContentAndResources_AticProgramDetailsId",
                table: "AticProgramContentAndResources",
                column: "AticProgramDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_AticProgramContentAndResources_CreatedById",
                table: "AticProgramContentAndResources",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AticProgramContentAndResources_UpdatedById",
                table: "AticProgramContentAndResources",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AticProgramDetails_ApprovedById",
                table: "AticProgramDetails",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_AticProgramDetails_CategoryId",
                table: "AticProgramDetails",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_AticProgramDetails_CreatedById",
                table: "AticProgramDetails",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AticProgramDetails_ModeId",
                table: "AticProgramDetails",
                column: "ModeId");

            migrationBuilder.CreateIndex(
                name: "IX_AticProgramDetails_OrganizationId",
                table: "AticProgramDetails",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_AticProgramDetails_ProgramTypeId",
                table: "AticProgramDetails",
                column: "ProgramTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AticProgramDetails_RegionId",
                table: "AticProgramDetails",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_AticProgramDetails_SourceId",
                table: "AticProgramDetails",
                column: "SourceId");

            migrationBuilder.CreateIndex(
                name: "IX_AticProgramDetails_SourceOfFundId",
                table: "AticProgramDetails",
                column: "SourceOfFundId");

            migrationBuilder.CreateIndex(
                name: "IX_AticProgramDetails_StatusId",
                table: "AticProgramDetails",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_AticProgramDetails_ThematicAreaId",
                table: "AticProgramDetails",
                column: "ThematicAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_AticProgramDetails_ThemeId",
                table: "AticProgramDetails",
                column: "ThemeId");

            migrationBuilder.CreateIndex(
                name: "IX_AticProgramDetails_TypeId",
                table: "AticProgramDetails",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AticProgramDetails_UnitLocationId",
                table: "AticProgramDetails",
                column: "UnitLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_AticProgramDetails_UpdatedById",
                table: "AticProgramDetails",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AticRecommendations_AticProgramDetailsId",
                table: "AticRecommendations",
                column: "AticProgramDetailsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AticRecommendations_CreatedById",
                table: "AticRecommendations",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AticRecommendations_UpdatedById",
                table: "AticRecommendations",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AticReports_AticProgramDetailsId",
                table: "AticReports",
                column: "AticProgramDetailsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AticReports_CreatedById",
                table: "AticReports",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AticReports_UpdatedById",
                table: "AticReports",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AticResourcePersons_AticProgramContentAndResourcesId",
                table: "AticResourcePersons",
                column: "AticProgramContentAndResourcesId");

            migrationBuilder.CreateIndex(
                name: "IX_AticResourcePersons_CreatedById",
                table: "AticResourcePersons",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AticResourcePersons_UpdatedById",
                table: "AticResourcePersons",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AticTeachingAidsDeveloped_AticProgramContentAndResourcesId",
                table: "AticTeachingAidsDeveloped",
                column: "AticProgramContentAndResourcesId");

            migrationBuilder.CreateIndex(
                name: "IX_AticTeachingAidsDeveloped_CreatedById",
                table: "AticTeachingAidsDeveloped",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AticTeachingAidsDeveloped_TypeOfAidId",
                table: "AticTeachingAidsDeveloped",
                column: "TypeOfAidId");

            migrationBuilder.CreateIndex(
                name: "IX_AticTeachingAidsDeveloped_UpdatedById",
                table: "AticTeachingAidsDeveloped",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AticTopicsCoveredInClass_AticProgramContentAndResourcesId",
                table: "AticTopicsCoveredInClass",
                column: "AticProgramContentAndResourcesId");

            migrationBuilder.CreateIndex(
                name: "IX_AticTopicsCoveredInClass_CreatedById",
                table: "AticTopicsCoveredInClass",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AticTopicsCoveredInClass_UpdatedById",
                table: "AticTopicsCoveredInClass",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_NaepAdvisoryServices_CreatedById",
                table: "NaepAdvisoryServices",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_NaepAdvisoryServices_NaepProgramDetailsId",
                table: "NaepAdvisoryServices",
                column: "NaepProgramDetailsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NaepAdvisoryServices_UpdatedById",
                table: "NaepAdvisoryServices",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_NaepParticipantDemographics_CreatedById",
                table: "NaepParticipantDemographics",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_NaepParticipantDemographics_NaepProgramDetailsId",
                table: "NaepParticipantDemographics",
                column: "NaepProgramDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_NaepParticipantDemographics_ParticipantId",
                table: "NaepParticipantDemographics",
                column: "ParticipantId");

            migrationBuilder.CreateIndex(
                name: "IX_NaepParticipantDemographics_UpdatedById",
                table: "NaepParticipantDemographics",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_NaepProgramContentAndResources_CreatedById",
                table: "NaepProgramContentAndResources",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_NaepProgramContentAndResources_NaepProgramDetailsId",
                table: "NaepProgramContentAndResources",
                column: "NaepProgramDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_NaepProgramContentAndResources_UpdatedById",
                table: "NaepProgramContentAndResources",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_NaepProgramDetails_ApprovedById",
                table: "NaepProgramDetails",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_NaepProgramDetails_CategoryId",
                table: "NaepProgramDetails",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_NaepProgramDetails_CreatedById",
                table: "NaepProgramDetails",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_NaepProgramDetails_ModeId",
                table: "NaepProgramDetails",
                column: "ModeId");

            migrationBuilder.CreateIndex(
                name: "IX_NaepProgramDetails_OrganizationId",
                table: "NaepProgramDetails",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_NaepProgramDetails_ProgramTypeId",
                table: "NaepProgramDetails",
                column: "ProgramTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_NaepProgramDetails_RegionId",
                table: "NaepProgramDetails",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_NaepProgramDetails_SourceId",
                table: "NaepProgramDetails",
                column: "SourceId");

            migrationBuilder.CreateIndex(
                name: "IX_NaepProgramDetails_SourceOfFundId",
                table: "NaepProgramDetails",
                column: "SourceOfFundId");

            migrationBuilder.CreateIndex(
                name: "IX_NaepProgramDetails_StatusId",
                table: "NaepProgramDetails",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_NaepProgramDetails_ThematicAreaId",
                table: "NaepProgramDetails",
                column: "ThematicAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_NaepProgramDetails_ThemeId",
                table: "NaepProgramDetails",
                column: "ThemeId");

            migrationBuilder.CreateIndex(
                name: "IX_NaepProgramDetails_TypeId",
                table: "NaepProgramDetails",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_NaepProgramDetails_UnitLocationId",
                table: "NaepProgramDetails",
                column: "UnitLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_NaepProgramDetails_UpdatedById",
                table: "NaepProgramDetails",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_NaepRecommendations_CreatedById",
                table: "NaepRecommendations",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_NaepRecommendations_NaepProgramDetailsId",
                table: "NaepRecommendations",
                column: "NaepProgramDetailsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NaepRecommendations_UpdatedById",
                table: "NaepRecommendations",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_NaepReports_CreatedById",
                table: "NaepReports",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_NaepReports_NaepProgramDetailsId",
                table: "NaepReports",
                column: "NaepProgramDetailsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NaepReports_UpdatedById",
                table: "NaepReports",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_NaepResourcePersons_CreatedById",
                table: "NaepResourcePersons",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_NaepResourcePersons_NaepProgramContentAndResourcesId",
                table: "NaepResourcePersons",
                column: "NaepProgramContentAndResourcesId");

            migrationBuilder.CreateIndex(
                name: "IX_NaepResourcePersons_UpdatedById",
                table: "NaepResourcePersons",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_NaepTeachingAidsDeveloped_CreatedById",
                table: "NaepTeachingAidsDeveloped",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_NaepTeachingAidsDeveloped_NaepProgramContentAndResourcesId",
                table: "NaepTeachingAidsDeveloped",
                column: "NaepProgramContentAndResourcesId");

            migrationBuilder.CreateIndex(
                name: "IX_NaepTeachingAidsDeveloped_TypeOfAidId",
                table: "NaepTeachingAidsDeveloped",
                column: "TypeOfAidId");

            migrationBuilder.CreateIndex(
                name: "IX_NaepTeachingAidsDeveloped_UpdatedById",
                table: "NaepTeachingAidsDeveloped",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_NaepTopicsCoveredInClass_CreatedById",
                table: "NaepTopicsCoveredInClass",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_NaepTopicsCoveredInClass_NaepProgramContentAndResourcesId",
                table: "NaepTopicsCoveredInClass",
                column: "NaepProgramContentAndResourcesId");

            migrationBuilder.CreateIndex(
                name: "IX_NaepTopicsCoveredInClass_UpdatedById",
                table: "NaepTopicsCoveredInClass",
                column: "UpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_AticAdvisoryServices_AticProgramDetails_AticProgramDetailsId",
                table: "AticAdvisoryServices",
                column: "AticProgramDetailsId",
                principalTable: "AticProgramDetails",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AticSales_OrganizationUnitLocations_UnitLocationId",
                table: "AticSales",
                column: "UnitLocationId",
                principalTable: "OrganizationUnitLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AticSales_Organizations_OrganizationId",
                table: "AticSales",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DeuProgramDetails_Modes_ModeId",
                table: "DeuProgramDetails",
                column: "ModeId",
                principalTable: "Modes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EeuProgramContentAndResources_EeuProgramDetails_EeuProgramDetailsId",
                table: "EeuProgramContentAndResources",
                column: "EeuProgramDetailsId",
                principalTable: "EeuProgramDetails",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EeuProgramDetails_Modes_ModeId",
                table: "EeuProgramDetails",
                column: "ModeId",
                principalTable: "Modes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StuAdvisoryServices_StuProgramDetails_StuProgramDetailsId",
                table: "StuAdvisoryServices",
                column: "StuProgramDetailsId",
                principalTable: "StuProgramDetails",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StuAdvisoryServices_Users_CreatedById",
                table: "StuAdvisoryServices",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StuAdvisoryServices_Users_UpdatedById",
                table: "StuAdvisoryServices",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StuParticipantDemographics_ParticipantDealer_ParticipantId",
                table: "StuParticipantDemographics",
                column: "ParticipantId",
                principalTable: "ParticipantDealer",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StuParticipantDemographics_StuProgramDetails_StuProgramDetailsId",
                table: "StuParticipantDemographics",
                column: "StuProgramDetailsId",
                principalTable: "StuProgramDetails",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StuParticipantDemographics_Users_CreatedById",
                table: "StuParticipantDemographics",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StuParticipantDemographics_Users_UpdatedById",
                table: "StuParticipantDemographics",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StuProgramContentAndResources_StuProgramDetails_StuProgramDetailsId",
                table: "StuProgramContentAndResources",
                column: "StuProgramDetailsId",
                principalTable: "StuProgramDetails",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StuProgramContentAndResources_Users_CreatedById",
                table: "StuProgramContentAndResources",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StuProgramContentAndResources_Users_UpdatedById",
                table: "StuProgramContentAndResources",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StuProgramDetails_InfoTypes_TypeId",
                table: "StuProgramDetails",
                column: "TypeId",
                principalTable: "InfoTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StuProgramDetails_Modes_ModeId",
                table: "StuProgramDetails",
                column: "ModeId",
                principalTable: "Modes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StuProgramDetails_OrganizationUnitLocations_UnitLocationId",
                table: "StuProgramDetails",
                column: "UnitLocationId",
                principalTable: "OrganizationUnitLocations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StuProgramDetails_Organizations_OrganizationId",
                table: "StuProgramDetails",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StuProgramDetails_ParticipatedSources_SourceId",
                table: "StuProgramDetails",
                column: "SourceId",
                principalTable: "ParticipatedSources",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StuProgramDetails_ProgramCategories_CategoryId",
                table: "StuProgramDetails",
                column: "CategoryId",
                principalTable: "ProgramCategories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StuProgramDetails_ProgramTypes_ProgramTypeId",
                table: "StuProgramDetails",
                column: "ProgramTypeId",
                principalTable: "ProgramTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StuProgramDetails_Regions_RegionId",
                table: "StuProgramDetails",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StuProgramDetails_SourcesOfFunds_SourceOfFundId",
                table: "StuProgramDetails",
                column: "SourceOfFundId",
                principalTable: "SourcesOfFunds",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StuProgramDetails_Statuses_StatusId",
                table: "StuProgramDetails",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StuProgramDetails_ThematicAreas_ThematicAreaId",
                table: "StuProgramDetails",
                column: "ThematicAreaId",
                principalTable: "ThematicAreas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StuProgramDetails_Themes_ThemeId",
                table: "StuProgramDetails",
                column: "ThemeId",
                principalTable: "Themes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StuProgramDetails_Users_ApprovedById",
                table: "StuProgramDetails",
                column: "ApprovedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StuProgramDetails_Users_CreatedById",
                table: "StuProgramDetails",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StuProgramDetails_Users_UpdatedById",
                table: "StuProgramDetails",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StuRecommendations_StuProgramDetails_StuProgramDetailsId",
                table: "StuRecommendations",
                column: "StuProgramDetailsId",
                principalTable: "StuProgramDetails",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StuRecommendations_Users_CreatedById",
                table: "StuRecommendations",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StuRecommendations_Users_UpdatedById",
                table: "StuRecommendations",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StuReports_StuProgramDetails_StuProgramDetailsId",
                table: "StuReports",
                column: "StuProgramDetailsId",
                principalTable: "StuProgramDetails",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StuReports_Users_CreatedById",
                table: "StuReports",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StuReports_Users_UpdatedById",
                table: "StuReports",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StuResourcePersons_StuProgramContentAndResources_StuProgramContentAndResourcesId",
                table: "StuResourcePersons",
                column: "StuProgramContentAndResourcesId",
                principalTable: "StuProgramContentAndResources",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StuResourcePersons_Users_CreatedById",
                table: "StuResourcePersons",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StuResourcePersons_Users_UpdatedById",
                table: "StuResourcePersons",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StuTeachingAidsDeveloped_StuProgramContentAndResources_StuProgramContentAndResourcesId",
                table: "StuTeachingAidsDeveloped",
                column: "StuProgramContentAndResourcesId",
                principalTable: "StuProgramContentAndResources",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StuTeachingAidsDeveloped_TypeOfAids_TypeOfAidId",
                table: "StuTeachingAidsDeveloped",
                column: "TypeOfAidId",
                principalTable: "TypeOfAids",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StuTeachingAidsDeveloped_Users_CreatedById",
                table: "StuTeachingAidsDeveloped",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StuTeachingAidsDeveloped_Users_UpdatedById",
                table: "StuTeachingAidsDeveloped",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StuTopicsCoveredInClass_StuProgramContentAndResources_StuProgramContentAndResourcesId",
                table: "StuTopicsCoveredInClass",
                column: "StuProgramContentAndResourcesId",
                principalTable: "StuProgramContentAndResources",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StuTopicsCoveredInClass_Users_CreatedById",
                table: "StuTopicsCoveredInClass",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StuTopicsCoveredInClass_Users_UpdatedById",
                table: "StuTopicsCoveredInClass",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AticAdvisoryServices_AticProgramDetails_AticProgramDetailsId",
                table: "AticAdvisoryServices");

            migrationBuilder.DropForeignKey(
                name: "FK_AticSales_OrganizationUnitLocations_UnitLocationId",
                table: "AticSales");

            migrationBuilder.DropForeignKey(
                name: "FK_AticSales_Organizations_OrganizationId",
                table: "AticSales");

            migrationBuilder.DropForeignKey(
                name: "FK_DeuProgramDetails_Modes_ModeId",
                table: "DeuProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_EeuProgramContentAndResources_EeuProgramDetails_EeuProgramDetailsId",
                table: "EeuProgramContentAndResources");

            migrationBuilder.DropForeignKey(
                name: "FK_EeuProgramDetails_Modes_ModeId",
                table: "EeuProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_StuAdvisoryServices_StuProgramDetails_StuProgramDetailsId",
                table: "StuAdvisoryServices");

            migrationBuilder.DropForeignKey(
                name: "FK_StuAdvisoryServices_Users_CreatedById",
                table: "StuAdvisoryServices");

            migrationBuilder.DropForeignKey(
                name: "FK_StuAdvisoryServices_Users_UpdatedById",
                table: "StuAdvisoryServices");

            migrationBuilder.DropForeignKey(
                name: "FK_StuParticipantDemographics_ParticipantDealer_ParticipantId",
                table: "StuParticipantDemographics");

            migrationBuilder.DropForeignKey(
                name: "FK_StuParticipantDemographics_StuProgramDetails_StuProgramDetailsId",
                table: "StuParticipantDemographics");

            migrationBuilder.DropForeignKey(
                name: "FK_StuParticipantDemographics_Users_CreatedById",
                table: "StuParticipantDemographics");

            migrationBuilder.DropForeignKey(
                name: "FK_StuParticipantDemographics_Users_UpdatedById",
                table: "StuParticipantDemographics");

            migrationBuilder.DropForeignKey(
                name: "FK_StuProgramContentAndResources_StuProgramDetails_StuProgramDetailsId",
                table: "StuProgramContentAndResources");

            migrationBuilder.DropForeignKey(
                name: "FK_StuProgramContentAndResources_Users_CreatedById",
                table: "StuProgramContentAndResources");

            migrationBuilder.DropForeignKey(
                name: "FK_StuProgramContentAndResources_Users_UpdatedById",
                table: "StuProgramContentAndResources");

            migrationBuilder.DropForeignKey(
                name: "FK_StuProgramDetails_InfoTypes_TypeId",
                table: "StuProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_StuProgramDetails_Modes_ModeId",
                table: "StuProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_StuProgramDetails_OrganizationUnitLocations_UnitLocationId",
                table: "StuProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_StuProgramDetails_Organizations_OrganizationId",
                table: "StuProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_StuProgramDetails_ParticipatedSources_SourceId",
                table: "StuProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_StuProgramDetails_ProgramCategories_CategoryId",
                table: "StuProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_StuProgramDetails_ProgramTypes_ProgramTypeId",
                table: "StuProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_StuProgramDetails_Regions_RegionId",
                table: "StuProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_StuProgramDetails_SourcesOfFunds_SourceOfFundId",
                table: "StuProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_StuProgramDetails_Statuses_StatusId",
                table: "StuProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_StuProgramDetails_ThematicAreas_ThematicAreaId",
                table: "StuProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_StuProgramDetails_Themes_ThemeId",
                table: "StuProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_StuProgramDetails_Users_ApprovedById",
                table: "StuProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_StuProgramDetails_Users_CreatedById",
                table: "StuProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_StuProgramDetails_Users_UpdatedById",
                table: "StuProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_StuRecommendations_StuProgramDetails_StuProgramDetailsId",
                table: "StuRecommendations");

            migrationBuilder.DropForeignKey(
                name: "FK_StuRecommendations_Users_CreatedById",
                table: "StuRecommendations");

            migrationBuilder.DropForeignKey(
                name: "FK_StuRecommendations_Users_UpdatedById",
                table: "StuRecommendations");

            migrationBuilder.DropForeignKey(
                name: "FK_StuReports_StuProgramDetails_StuProgramDetailsId",
                table: "StuReports");

            migrationBuilder.DropForeignKey(
                name: "FK_StuReports_Users_CreatedById",
                table: "StuReports");

            migrationBuilder.DropForeignKey(
                name: "FK_StuReports_Users_UpdatedById",
                table: "StuReports");

            migrationBuilder.DropForeignKey(
                name: "FK_StuResourcePersons_StuProgramContentAndResources_StuProgramContentAndResourcesId",
                table: "StuResourcePersons");

            migrationBuilder.DropForeignKey(
                name: "FK_StuResourcePersons_Users_CreatedById",
                table: "StuResourcePersons");

            migrationBuilder.DropForeignKey(
                name: "FK_StuResourcePersons_Users_UpdatedById",
                table: "StuResourcePersons");

            migrationBuilder.DropForeignKey(
                name: "FK_StuTeachingAidsDeveloped_StuProgramContentAndResources_StuProgramContentAndResourcesId",
                table: "StuTeachingAidsDeveloped");

            migrationBuilder.DropForeignKey(
                name: "FK_StuTeachingAidsDeveloped_TypeOfAids_TypeOfAidId",
                table: "StuTeachingAidsDeveloped");

            migrationBuilder.DropForeignKey(
                name: "FK_StuTeachingAidsDeveloped_Users_CreatedById",
                table: "StuTeachingAidsDeveloped");

            migrationBuilder.DropForeignKey(
                name: "FK_StuTeachingAidsDeveloped_Users_UpdatedById",
                table: "StuTeachingAidsDeveloped");

            migrationBuilder.DropForeignKey(
                name: "FK_StuTopicsCoveredInClass_StuProgramContentAndResources_StuProgramContentAndResourcesId",
                table: "StuTopicsCoveredInClass");

            migrationBuilder.DropForeignKey(
                name: "FK_StuTopicsCoveredInClass_Users_CreatedById",
                table: "StuTopicsCoveredInClass");

            migrationBuilder.DropForeignKey(
                name: "FK_StuTopicsCoveredInClass_Users_UpdatedById",
                table: "StuTopicsCoveredInClass");

            migrationBuilder.DropTable(
                name: "AticParticipantDemographics");

            migrationBuilder.DropTable(
                name: "AticRecommendations");

            migrationBuilder.DropTable(
                name: "AticReports");

            migrationBuilder.DropTable(
                name: "AticResourcePersons");

            migrationBuilder.DropTable(
                name: "AticTeachingAidsDeveloped");

            migrationBuilder.DropTable(
                name: "AticTopicsCoveredInClass");

            migrationBuilder.DropTable(
                name: "NaepAdvisoryServices");

            migrationBuilder.DropTable(
                name: "NaepParticipantDemographics");

            migrationBuilder.DropTable(
                name: "NaepRecommendations");

            migrationBuilder.DropTable(
                name: "NaepReports");

            migrationBuilder.DropTable(
                name: "NaepResourcePersons");

            migrationBuilder.DropTable(
                name: "NaepTeachingAidsDeveloped");

            migrationBuilder.DropTable(
                name: "NaepTopicsCoveredInClass");

            migrationBuilder.DropTable(
                name: "AticProgramContentAndResources");

            migrationBuilder.DropTable(
                name: "NaepProgramContentAndResources");

            migrationBuilder.DropTable(
                name: "AticProgramDetails");

            migrationBuilder.DropTable(
                name: "NaepProgramDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StuTopicsCoveredInClass",
                table: "StuTopicsCoveredInClass");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StuTeachingAidsDeveloped",
                table: "StuTeachingAidsDeveloped");

            migrationBuilder.DropIndex(
                name: "IX_StuTeachingAidsDeveloped_TypeOfAidId",
                table: "StuTeachingAidsDeveloped");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StuResourcePersons",
                table: "StuResourcePersons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StuReports",
                table: "StuReports");

            migrationBuilder.DropIndex(
                name: "IX_StuReports_StuProgramDetailsId",
                table: "StuReports");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StuRecommendations",
                table: "StuRecommendations");

            migrationBuilder.DropIndex(
                name: "IX_StuRecommendations_StuProgramDetailsId",
                table: "StuRecommendations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StuProgramDetails",
                table: "StuProgramDetails");

            migrationBuilder.DropIndex(
                name: "IX_StuProgramDetails_ApprovedById",
                table: "StuProgramDetails");

            migrationBuilder.DropIndex(
                name: "IX_StuProgramDetails_CategoryId",
                table: "StuProgramDetails");

            migrationBuilder.DropIndex(
                name: "IX_StuProgramDetails_ModeId",
                table: "StuProgramDetails");

            migrationBuilder.DropIndex(
                name: "IX_StuProgramDetails_OrganizationId",
                table: "StuProgramDetails");

            migrationBuilder.DropIndex(
                name: "IX_StuProgramDetails_ProgramTypeId",
                table: "StuProgramDetails");

            migrationBuilder.DropIndex(
                name: "IX_StuProgramDetails_RegionId",
                table: "StuProgramDetails");

            migrationBuilder.DropIndex(
                name: "IX_StuProgramDetails_SourceId",
                table: "StuProgramDetails");

            migrationBuilder.DropIndex(
                name: "IX_StuProgramDetails_SourceOfFundId",
                table: "StuProgramDetails");

            migrationBuilder.DropIndex(
                name: "IX_StuProgramDetails_StatusId",
                table: "StuProgramDetails");

            migrationBuilder.DropIndex(
                name: "IX_StuProgramDetails_ThematicAreaId",
                table: "StuProgramDetails");

            migrationBuilder.DropIndex(
                name: "IX_StuProgramDetails_ThemeId",
                table: "StuProgramDetails");

            migrationBuilder.DropIndex(
                name: "IX_StuProgramDetails_TypeId",
                table: "StuProgramDetails");

            migrationBuilder.DropIndex(
                name: "IX_StuProgramDetails_UnitLocationId",
                table: "StuProgramDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StuProgramContentAndResources",
                table: "StuProgramContentAndResources");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StuParticipantDemographics",
                table: "StuParticipantDemographics");

            migrationBuilder.DropIndex(
                name: "IX_StuParticipantDemographics_ParticipantId",
                table: "StuParticipantDemographics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StuAdvisoryServices",
                table: "StuAdvisoryServices");

            migrationBuilder.DropIndex(
                name: "IX_StuAdvisoryServices_StuProgramDetailsId",
                table: "StuAdvisoryServices");

            migrationBuilder.DropIndex(
                name: "IX_EeuReports_EeuProgramDetailsId",
                table: "EeuReports");

            migrationBuilder.DropIndex(
                name: "IX_EeuRecommendations_EeuProgramDetailsId",
                table: "EeuRecommendations");

            migrationBuilder.DropIndex(
                name: "IX_EeuProgramDetails_ModeId",
                table: "EeuProgramDetails");

            migrationBuilder.DropIndex(
                name: "IX_EeuAdvisoryServices_EeuProgramDetailsId",
                table: "EeuAdvisoryServices");

            migrationBuilder.DropIndex(
                name: "IX_DeuReports_DeuProgramDetailsId",
                table: "DeuReports");

            migrationBuilder.DropIndex(
                name: "IX_DeuRecommendations_DeuProgramDetailsId",
                table: "DeuRecommendations");

            migrationBuilder.DropIndex(
                name: "IX_DeuProgramDetails_ModeId",
                table: "DeuProgramDetails");

            migrationBuilder.DropIndex(
                name: "IX_DeuAdvisoryServices_DeuProgramDetailsId",
                table: "DeuAdvisoryServices");

            migrationBuilder.DropIndex(
                name: "IX_AticAdvisoryServices_AticProgramDetailsId",
                table: "AticAdvisoryServices");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "StuTopicsCoveredInClass");

            migrationBuilder.DropColumn(
                name: "UnitLocationId",
                table: "StuTopicsCoveredInClass");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "StuTeachingAidsDeveloped");

            migrationBuilder.DropColumn(
                name: "TypeOfAidId",
                table: "StuTeachingAidsDeveloped");

            migrationBuilder.DropColumn(
                name: "UnitLocationId",
                table: "StuTeachingAidsDeveloped");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "StuResourcePersons");

            migrationBuilder.DropColumn(
                name: "UnitLocationId",
                table: "StuResourcePersons");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "StuReports");

            migrationBuilder.DropColumn(
                name: "UnitLocationId",
                table: "StuReports");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "StuRecommendations");

            migrationBuilder.DropColumn(
                name: "UnitLocationId",
                table: "StuRecommendations");

            migrationBuilder.DropColumn(
                name: "ApprovedAt",
                table: "StuProgramDetails");

            migrationBuilder.DropColumn(
                name: "ApprovedById",
                table: "StuProgramDetails");

            migrationBuilder.DropColumn(
                name: "Attachements",
                table: "StuProgramDetails");

            migrationBuilder.DropColumn(
                name: "Copi",
                table: "StuProgramDetails");

            migrationBuilder.DropColumn(
                name: "FormStatus",
                table: "StuProgramDetails");

            migrationBuilder.DropColumn(
                name: "FormStatusRemarks",
                table: "StuProgramDetails");

            migrationBuilder.DropColumn(
                name: "Funds",
                table: "StuProgramDetails");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "StuProgramDetails");

            migrationBuilder.DropColumn(
                name: "OrganizerBroucherFile",
                table: "StuProgramDetails");

            migrationBuilder.DropColumn(
                name: "OrganizerInstitutionAddress",
                table: "StuProgramDetails");

            migrationBuilder.DropColumn(
                name: "SourceId",
                table: "StuProgramDetails");

            migrationBuilder.DropColumn(
                name: "SourceOfFundId",
                table: "StuProgramDetails");

            migrationBuilder.DropColumn(
                name: "UnitLocationId",
                table: "StuProgramDetails");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "StuProgramContentAndResources");

            migrationBuilder.DropColumn(
                name: "UnitLocationId",
                table: "StuProgramContentAndResources");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "StuParticipantDemographics");

            migrationBuilder.DropColumn(
                name: "UnitLocationId",
                table: "StuParticipantDemographics");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "StuAdvisoryServices");

            migrationBuilder.DropColumn(
                name: "UnitLocationId",
                table: "StuAdvisoryServices");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "EeuTopicsCoveredInClass");

            migrationBuilder.DropColumn(
                name: "UnitLocationId",
                table: "EeuTopicsCoveredInClass");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "EeuTeachingAidsDeveloped");

            migrationBuilder.DropColumn(
                name: "OtherTypeOfAid",
                table: "EeuTeachingAidsDeveloped");

            migrationBuilder.DropColumn(
                name: "UnitLocationId",
                table: "EeuTeachingAidsDeveloped");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "EeuResourcePersons");

            migrationBuilder.DropColumn(
                name: "UnitLocationId",
                table: "EeuResourcePersons");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "EeuReports");

            migrationBuilder.DropColumn(
                name: "UnitLocationId",
                table: "EeuReports");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "EeuRecommendations");

            migrationBuilder.DropColumn(
                name: "UnitLocationId",
                table: "EeuRecommendations");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "EeuProgramContentAndResources");

            migrationBuilder.DropColumn(
                name: "UnitLocationId",
                table: "EeuProgramContentAndResources");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "EeuParticipantDemographics");

            migrationBuilder.DropColumn(
                name: "UnitLocationId",
                table: "EeuParticipantDemographics");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "EeuAdvisoryServices");

            migrationBuilder.DropColumn(
                name: "UnitLocationId",
                table: "EeuAdvisoryServices");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "DeuTopicsCoveredInClass");

            migrationBuilder.DropColumn(
                name: "UnitLocationId",
                table: "DeuTopicsCoveredInClass");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "DeuTeachingAidsDeveloped");

            migrationBuilder.DropColumn(
                name: "UnitLocationId",
                table: "DeuTeachingAidsDeveloped");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "DeuResourcePersons");

            migrationBuilder.DropColumn(
                name: "UnitLocationId",
                table: "DeuResourcePersons");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "DeuReports");

            migrationBuilder.DropColumn(
                name: "UnitLocationId",
                table: "DeuReports");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "DeuRecommendations");

            migrationBuilder.DropColumn(
                name: "UnitLocationId",
                table: "DeuRecommendations");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "DeuProgramContentAndResources");

            migrationBuilder.DropColumn(
                name: "UnitLocationId",
                table: "DeuProgramContentAndResources");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "DeuParticipantDemographics");

            migrationBuilder.DropColumn(
                name: "UnitLocationId",
                table: "DeuParticipantDemographics");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "DeuAdvisoryServices");

            migrationBuilder.DropColumn(
                name: "UnitLocationId",
                table: "DeuAdvisoryServices");

            migrationBuilder.DropColumn(
                name: "AticProgramDetailsId",
                table: "AticAdvisoryServices");

            migrationBuilder.DropColumn(
                name: "NoOfAnsweredWhatsappQueries",
                table: "AticAdvisoryServices");

            migrationBuilder.DropColumn(
                name: "NoOfBeneficiaries",
                table: "AticAdvisoryServices");

            migrationBuilder.DropColumn(
                name: "NoOfEmailsSent",
                table: "AticAdvisoryServices");

            migrationBuilder.DropColumn(
                name: "NoOfFaceToFaceDiscussions",
                table: "AticAdvisoryServices");

            migrationBuilder.DropColumn(
                name: "NoOfFacebookSMS",
                table: "AticAdvisoryServices");

            migrationBuilder.DropColumn(
                name: "NoOfGroupDiscussions",
                table: "AticAdvisoryServices");

            migrationBuilder.DropColumn(
                name: "NoOfNewspaperCoverage",
                table: "AticAdvisoryServices");

            migrationBuilder.DropColumn(
                name: "NoOfPhoneCalls",
                table: "AticAdvisoryServices");

            migrationBuilder.DropColumn(
                name: "NoOfSMSSentToRegisteredFarmers",
                table: "AticAdvisoryServices");

            migrationBuilder.RenameTable(
                name: "StuTopicsCoveredInClass",
                newName: "STUTopicsCoveredInClass");

            migrationBuilder.RenameTable(
                name: "StuTeachingAidsDeveloped",
                newName: "STUTeachingAidsDeveloped");

            migrationBuilder.RenameTable(
                name: "StuResourcePersons",
                newName: "STUResourcePersons");

            migrationBuilder.RenameTable(
                name: "StuReports",
                newName: "STUReports");

            migrationBuilder.RenameTable(
                name: "StuRecommendations",
                newName: "STURecommendations");

            migrationBuilder.RenameTable(
                name: "StuProgramDetails",
                newName: "STUProgramDetails");

            migrationBuilder.RenameTable(
                name: "StuProgramContentAndResources",
                newName: "STUProgramContentAndResources");

            migrationBuilder.RenameTable(
                name: "StuParticipantDemographics",
                newName: "STUParticipantDemographics");

            migrationBuilder.RenameTable(
                name: "StuAdvisoryServices",
                newName: "STUAdvisoryServices");

            migrationBuilder.RenameColumn(
                name: "StuProgramContentAndResourcesId",
                table: "STUTopicsCoveredInClass",
                newName: "STUProgramContentAndResourcesID");

            migrationBuilder.RenameIndex(
                name: "IX_StuTopicsCoveredInClass_UpdatedById",
                table: "STUTopicsCoveredInClass",
                newName: "IX_STUTopicsCoveredInClass_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_StuTopicsCoveredInClass_StuProgramContentAndResourcesId",
                table: "STUTopicsCoveredInClass",
                newName: "IX_STUTopicsCoveredInClass_STUProgramContentAndResourcesID");

            migrationBuilder.RenameIndex(
                name: "IX_StuTopicsCoveredInClass_CreatedById",
                table: "STUTopicsCoveredInClass",
                newName: "IX_STUTopicsCoveredInClass_CreatedById");

            migrationBuilder.RenameColumn(
                name: "StuProgramContentAndResourcesId",
                table: "STUTeachingAidsDeveloped",
                newName: "STUProgramContentAndResourcesID");

            migrationBuilder.RenameColumn(
                name: "OtherTypeOfAid",
                table: "STUTeachingAidsDeveloped",
                newName: "TypeOfAidDeveloped");

            migrationBuilder.RenameIndex(
                name: "IX_StuTeachingAidsDeveloped_UpdatedById",
                table: "STUTeachingAidsDeveloped",
                newName: "IX_STUTeachingAidsDeveloped_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_StuTeachingAidsDeveloped_StuProgramContentAndResourcesId",
                table: "STUTeachingAidsDeveloped",
                newName: "IX_STUTeachingAidsDeveloped_STUProgramContentAndResourcesID");

            migrationBuilder.RenameIndex(
                name: "IX_StuTeachingAidsDeveloped_CreatedById",
                table: "STUTeachingAidsDeveloped",
                newName: "IX_STUTeachingAidsDeveloped_CreatedById");

            migrationBuilder.RenameColumn(
                name: "StuProgramContentAndResourcesId",
                table: "STUResourcePersons",
                newName: "STUProgramContentAndResourcesID");

            migrationBuilder.RenameIndex(
                name: "IX_StuResourcePersons_UpdatedById",
                table: "STUResourcePersons",
                newName: "IX_STUResourcePersons_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_StuResourcePersons_StuProgramContentAndResourcesId",
                table: "STUResourcePersons",
                newName: "IX_STUResourcePersons_STUProgramContentAndResourcesID");

            migrationBuilder.RenameIndex(
                name: "IX_StuResourcePersons_CreatedById",
                table: "STUResourcePersons",
                newName: "IX_STUResourcePersons_CreatedById");

            migrationBuilder.RenameColumn(
                name: "StuProgramDetailsId",
                table: "STUReports",
                newName: "STUProgramDetailsID");

            migrationBuilder.RenameIndex(
                name: "IX_StuReports_UpdatedById",
                table: "STUReports",
                newName: "IX_STUReports_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_StuReports_CreatedById",
                table: "STUReports",
                newName: "IX_STUReports_CreatedById");

            migrationBuilder.RenameColumn(
                name: "StuProgramDetailsId",
                table: "STURecommendations",
                newName: "STUProgramDetailsID");

            migrationBuilder.RenameIndex(
                name: "IX_StuRecommendations_UpdatedById",
                table: "STURecommendations",
                newName: "IX_STURecommendations_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_StuRecommendations_CreatedById",
                table: "STURecommendations",
                newName: "IX_STURecommendations_CreatedById");

            migrationBuilder.RenameColumn(
                name: "TypeId",
                table: "STUProgramDetails",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "ThemeId",
                table: "STUProgramDetails",
                newName: "Theme");

            migrationBuilder.RenameColumn(
                name: "ThematicAreaId",
                table: "STUProgramDetails",
                newName: "ThematicArea");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "STUProgramDetails",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "RegionId",
                table: "STUProgramDetails",
                newName: "Region");

            migrationBuilder.RenameColumn(
                name: "ModeId",
                table: "STUProgramDetails",
                newName: "SD");

            migrationBuilder.RenameIndex(
                name: "IX_StuProgramDetails_UpdatedById",
                table: "STUProgramDetails",
                newName: "IX_STUProgramDetails_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_StuProgramDetails_CreatedById",
                table: "STUProgramDetails",
                newName: "IX_STUProgramDetails_CreatedById");

            migrationBuilder.RenameColumn(
                name: "StuProgramDetailsId",
                table: "STUProgramContentAndResources",
                newName: "STUProgramDetailsID");

            migrationBuilder.RenameIndex(
                name: "IX_StuProgramContentAndResources_UpdatedById",
                table: "STUProgramContentAndResources",
                newName: "IX_STUProgramContentAndResources_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_StuProgramContentAndResources_StuProgramDetailsId",
                table: "STUProgramContentAndResources",
                newName: "IX_STUProgramContentAndResources_STUProgramDetailsID");

            migrationBuilder.RenameIndex(
                name: "IX_StuProgramContentAndResources_CreatedById",
                table: "STUProgramContentAndResources",
                newName: "IX_STUProgramContentAndResources_CreatedById");

            migrationBuilder.RenameColumn(
                name: "StuProgramDetailsId",
                table: "STUParticipantDemographics",
                newName: "STUProgramDetailsID");

            migrationBuilder.RenameColumn(
                name: "ParticipantId",
                table: "STUParticipantDemographics",
                newName: "Participant");

            migrationBuilder.RenameIndex(
                name: "IX_StuParticipantDemographics_UpdatedById",
                table: "STUParticipantDemographics",
                newName: "IX_STUParticipantDemographics_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_StuParticipantDemographics_StuProgramDetailsId",
                table: "STUParticipantDemographics",
                newName: "IX_STUParticipantDemographics_STUProgramDetailsID");

            migrationBuilder.RenameIndex(
                name: "IX_StuParticipantDemographics_CreatedById",
                table: "STUParticipantDemographics",
                newName: "IX_STUParticipantDemographics_CreatedById");

            migrationBuilder.RenameColumn(
                name: "StuProgramDetailsId",
                table: "STUAdvisoryServices",
                newName: "STUProgramDetailsID");

            migrationBuilder.RenameIndex(
                name: "IX_StuAdvisoryServices_UpdatedById",
                table: "STUAdvisoryServices",
                newName: "IX_STUAdvisoryServices_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_StuAdvisoryServices_CreatedById",
                table: "STUAdvisoryServices",
                newName: "IX_STUAdvisoryServices_CreatedById");

            migrationBuilder.RenameColumn(
                name: "ModeId",
                table: "EeuProgramDetails",
                newName: "Mode");

            migrationBuilder.RenameColumn(
                name: "OtherTypeOfAid",
                table: "DeuTeachingAidsDeveloped",
                newName: "Other");

            migrationBuilder.RenameColumn(
                name: "ModeId",
                table: "DeuProgramDetails",
                newName: "Mode");

            migrationBuilder.RenameColumn(
                name: "NoOfWhatsappSMS",
                table: "AticAdvisoryServices",
                newName: "ServiceCount");

            migrationBuilder.RenameColumn(
                name: "NoOfWhatsappGroups",
                table: "AticAdvisoryServices",
                newName: "BeneficiaryCount");

            migrationBuilder.AddColumn<string>(
                name: "Other",
                table: "STUTeachingAidsDeveloped",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "STUProgramDetails",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "STUProgramDetails",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<int>(
                name: "ProgramTypeId",
                table: "STUProgramDetails",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OrganizerInstitutionName",
                table: "STUProgramDetails",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "STUProgramDetails",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "STUProgramDetails",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "AreaHa",
                table: "STUProgramDetails",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Mode",
                table: "STUProgramDetails",
                type: "int",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrganizerAddress",
                table: "STUProgramDetails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrganizerFileUpload",
                table: "STUProgramDetails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaperPosterAbstract",
                table: "STUProgramDetails",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PaperPosterAbstractDate",
                table: "STUProgramDetails",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaperPosterAbstractLink",
                table: "STUProgramDetails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ParticipatedAs",
                table: "STUProgramDetails",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Participation",
                table: "STUProgramDetails",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ParticipationFileLink",
                table: "STUProgramDetails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SourceOfInformation",
                table: "STUProgramDetails",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TitleOfThesisOrProjectOrPaperOrOthers",
                table: "STUProgramDetails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UploadVideo",
                table: "STUProgramDetails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "STUProgramContentAndResources",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "STUProgramContentAndResources",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Number",
                table: "IbtvaTeachingAidsDeveloped",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "IbtvaResourcePersons",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "IbtvaProgramDetails",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Purpose",
                table: "EeuTeachingAidsDeveloped",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Number",
                table: "EeuTeachingAidsDeveloped",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Other",
                table: "EeuTeachingAidsDeveloped",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "Responsibility",
                table: "EeuResourcePersons",
                type: "int",
                maxLength: 250,
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ResourceType",
                table: "EeuResourcePersons",
                type: "int",
                maxLength: 150,
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "EeuResourcePersons",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "InstitutionOrDepartment",
                table: "EeuResourcePersons",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Designation",
                table: "EeuResourcePersons",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UploadVideo",
                table: "EeuReports",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UploadPhoto",
                table: "EeuReports",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SignificantOutcome",
                table: "EeuReports",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProgressReportReportingYear",
                table: "EeuReports",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PhotosGeotaggedPhotoOrUploadPhoto",
                table: "EeuReports",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SuccessStories",
                table: "EeuRecommendations",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SignificantAchievement",
                table: "EeuRecommendations",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Recommendation",
                table: "EeuRecommendations",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProblemsIdentified",
                table: "EeuRecommendations",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ImpactOutcome",
                table: "EeuRecommendations",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ActionTaken",
                table: "EeuRecommendations",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalOutlayRs",
                table: "EeuProgramDetails",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "EeuProgramDetails",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "EeuProgramDetails",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "EeuProgramDetails",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "EeuProgramContentAndResources",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "EeuProgramContentAndResources",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalOutlayRs",
                table: "DeuProgramDetails",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "DeuProgramDetails",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "DeuProgramDetails",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "DeuProgramContentAndResources",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "DeuProgramContentAndResources",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NoOfWhatsappSMS",
                table: "DeuAdvisoryServices",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "NoOfWhatsappGroups",
                table: "DeuAdvisoryServices",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "NoOfSMSSentToRegisteredFarmers",
                table: "DeuAdvisoryServices",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "NoOfPhoneCalls",
                table: "DeuAdvisoryServices",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "NoOfNewspaperCoverage",
                table: "DeuAdvisoryServices",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "NoOfGroupDiscussions",
                table: "DeuAdvisoryServices",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "NoOfFacebookSMS",
                table: "DeuAdvisoryServices",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "NoOfFaceToFaceDiscussions",
                table: "DeuAdvisoryServices",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "NoOfEmailsSent",
                table: "DeuAdvisoryServices",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "NoOfBeneficiaries",
                table: "DeuAdvisoryServices",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "NoOfAnsweredWhatsappQueries",
                table: "DeuAdvisoryServices",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "UnitLocationId",
                table: "AticAdvisoryServices",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OrganizationId",
                table: "AticAdvisoryServices",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ApprovedAt",
                table: "AticAdvisoryServices",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApprovedById",
                table: "AticAdvisoryServices",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Attachements",
                table: "AticAdvisoryServices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "EndDate",
                table: "AticAdvisoryServices",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "FormStatus",
                table: "AticAdvisoryServices",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FormStatusRemarks",
                table: "AticAdvisoryServices",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ServiceType",
                table: "AticAdvisoryServices",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateOnly>(
                name: "StartDate",
                table: "AticAdvisoryServices",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddPrimaryKey(
                name: "PK_STUTopicsCoveredInClass",
                table: "STUTopicsCoveredInClass",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_STUTeachingAidsDeveloped",
                table: "STUTeachingAidsDeveloped",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_STUResourcePersons",
                table: "STUResourcePersons",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_STUReports",
                table: "STUReports",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_STURecommendations",
                table: "STURecommendations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_STUProgramDetails",
                table: "STUProgramDetails",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_STUProgramContentAndResources",
                table: "STUProgramContentAndResources",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_STUParticipantDemographics",
                table: "STUParticipantDemographics",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_STUAdvisoryServices",
                table: "STUAdvisoryServices",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "AticOtherActivities",
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
                    table.PrimaryKey("PK_AticOtherActivities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AticOtherActivities_OrganizationUnitLocations_UnitLocationId",
                        column: x => x.UnitLocationId,
                        principalTable: "OrganizationUnitLocations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AticOtherActivities_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AticOtherActivities_Users_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AticOtherActivities_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AticOtherActivities_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_STUReports_STUProgramDetailsID",
                table: "STUReports",
                column: "STUProgramDetailsID");

            migrationBuilder.CreateIndex(
                name: "IX_STURecommendations_STUProgramDetailsID",
                table: "STURecommendations",
                column: "STUProgramDetailsID");

            migrationBuilder.CreateIndex(
                name: "IX_STUAdvisoryServices_STUProgramDetailsID",
                table: "STUAdvisoryServices",
                column: "STUProgramDetailsID");

            migrationBuilder.CreateIndex(
                name: "IX_EeuReports_EeuProgramDetailsId",
                table: "EeuReports",
                column: "EeuProgramDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_EeuRecommendations_EeuProgramDetailsId",
                table: "EeuRecommendations",
                column: "EeuProgramDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_EeuAdvisoryServices_EeuProgramDetailsId",
                table: "EeuAdvisoryServices",
                column: "EeuProgramDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_DeuReports_DeuProgramDetailsId",
                table: "DeuReports",
                column: "DeuProgramDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_DeuRecommendations_DeuProgramDetailsId",
                table: "DeuRecommendations",
                column: "DeuProgramDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_DeuAdvisoryServices_DeuProgramDetailsId",
                table: "DeuAdvisoryServices",
                column: "DeuProgramDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_AticAdvisoryServices_ApprovedById",
                table: "AticAdvisoryServices",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_AticAdvisoryServices_OrganizationId",
                table: "AticAdvisoryServices",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_AticAdvisoryServices_UnitLocationId",
                table: "AticAdvisoryServices",
                column: "UnitLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_AticOtherActivities_ApprovedById",
                table: "AticOtherActivities",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_AticOtherActivities_CreatedById",
                table: "AticOtherActivities",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AticOtherActivities_OrganizationId",
                table: "AticOtherActivities",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_AticOtherActivities_UnitLocationId",
                table: "AticOtherActivities",
                column: "UnitLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_AticOtherActivities_UpdatedById",
                table: "AticOtherActivities",
                column: "UpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_AticAdvisoryServices_OrganizationUnitLocations_UnitLocationId",
                table: "AticAdvisoryServices",
                column: "UnitLocationId",
                principalTable: "OrganizationUnitLocations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AticAdvisoryServices_Organizations_OrganizationId",
                table: "AticAdvisoryServices",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AticAdvisoryServices_Users_ApprovedById",
                table: "AticAdvisoryServices",
                column: "ApprovedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AticSales_OrganizationUnitLocations_UnitLocationId",
                table: "AticSales",
                column: "UnitLocationId",
                principalTable: "OrganizationUnitLocations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AticSales_Organizations_OrganizationId",
                table: "AticSales",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EeuProgramContentAndResources_DeuProgramDetails_EeuProgramDetailsId",
                table: "EeuProgramContentAndResources",
                column: "EeuProgramDetailsId",
                principalTable: "DeuProgramDetails",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_STUAdvisoryServices_STUProgramDetails_STUProgramDetailsID",
                table: "STUAdvisoryServices",
                column: "STUProgramDetailsID",
                principalTable: "STUProgramDetails",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_STUAdvisoryServices_Users_CreatedById",
                table: "STUAdvisoryServices",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_STUAdvisoryServices_Users_UpdatedById",
                table: "STUAdvisoryServices",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_STUParticipantDemographics_STUProgramDetails_STUProgramDetailsID",
                table: "STUParticipantDemographics",
                column: "STUProgramDetailsID",
                principalTable: "STUProgramDetails",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_STUParticipantDemographics_Users_CreatedById",
                table: "STUParticipantDemographics",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_STUParticipantDemographics_Users_UpdatedById",
                table: "STUParticipantDemographics",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_STUProgramContentAndResources_STUProgramDetails_STUProgramDetailsID",
                table: "STUProgramContentAndResources",
                column: "STUProgramDetailsID",
                principalTable: "STUProgramDetails",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_STUProgramContentAndResources_Users_CreatedById",
                table: "STUProgramContentAndResources",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_STUProgramContentAndResources_Users_UpdatedById",
                table: "STUProgramContentAndResources",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_STUProgramDetails_Users_CreatedById",
                table: "STUProgramDetails",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_STUProgramDetails_Users_UpdatedById",
                table: "STUProgramDetails",
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
                name: "FK_STUReports_STUProgramDetails_STUProgramDetailsID",
                table: "STUReports",
                column: "STUProgramDetailsID",
                principalTable: "STUProgramDetails",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_STUReports_Users_CreatedById",
                table: "STUReports",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_STUReports_Users_UpdatedById",
                table: "STUReports",
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
                name: "FK_STUTeachingAidsDeveloped_STUProgramContentAndResources_STUProgramContentAndResourcesID",
                table: "STUTeachingAidsDeveloped",
                column: "STUProgramContentAndResourcesID",
                principalTable: "STUProgramContentAndResources",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_STUTeachingAidsDeveloped_Users_CreatedById",
                table: "STUTeachingAidsDeveloped",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_STUTeachingAidsDeveloped_Users_UpdatedById",
                table: "STUTeachingAidsDeveloped",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_STUTopicsCoveredInClass_STUProgramContentAndResources_STUProgramContentAndResourcesID",
                table: "STUTopicsCoveredInClass",
                column: "STUProgramContentAndResourcesID",
                principalTable: "STUProgramContentAndResources",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_STUTopicsCoveredInClass_Users_CreatedById",
                table: "STUTopicsCoveredInClass",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_STUTopicsCoveredInClass_Users_UpdatedById",
                table: "STUTopicsCoveredInClass",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}

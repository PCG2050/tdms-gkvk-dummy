using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Fti_Changes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FTIAdvisoryServices_FTIProgramDetails_FTIProgramDetailsID",
                table: "FTIAdvisoryServices");

            migrationBuilder.DropForeignKey(
                name: "FK_FTIAdvisoryServices_Users_CreatedById",
                table: "FTIAdvisoryServices");

            migrationBuilder.DropForeignKey(
                name: "FK_FTIAdvisoryServices_Users_UpdatedById",
                table: "FTIAdvisoryServices");

            migrationBuilder.DropForeignKey(
                name: "FK_FTIParticipantDemographics_FTIProgramDetails_FTIProgramDetailsID",
                table: "FTIParticipantDemographics");

            migrationBuilder.DropForeignKey(
                name: "FK_FTIParticipantDemographics_Users_CreatedById",
                table: "FTIParticipantDemographics");

            migrationBuilder.DropForeignKey(
                name: "FK_FTIParticipantDemographics_Users_UpdatedById",
                table: "FTIParticipantDemographics");

            migrationBuilder.DropForeignKey(
                name: "FK_FTIProgramContentAndResources_FTIProgramDetails_FTIProgramDetailsID",
                table: "FTIProgramContentAndResources");

            migrationBuilder.DropForeignKey(
                name: "FK_FTIProgramContentAndResources_Users_CreatedById",
                table: "FTIProgramContentAndResources");

            migrationBuilder.DropForeignKey(
                name: "FK_FTIProgramContentAndResources_Users_UpdatedById",
                table: "FTIProgramContentAndResources");

            migrationBuilder.DropForeignKey(
                name: "FK_FTIProgramDetails_Collaborators_CollaboratorId",
                table: "FTIProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_FTIProgramDetails_InfoTypes_TypeId",
                table: "FTIProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_FTIProgramDetails_ProgramCategories_CategoryId",
                table: "FTIProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_FTIProgramDetails_ThematicAreas_ThematicAreaId",
                table: "FTIProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_FTIProgramDetails_Users_CreatedById",
                table: "FTIProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_FTIProgramDetails_Users_UpdatedById",
                table: "FTIProgramDetails");

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
                name: "FK_FTIReports_FTIProgramDetails_FTIProgramDetailsID",
                table: "FTIReports");

            migrationBuilder.DropForeignKey(
                name: "FK_FTIReports_Users_CreatedById",
                table: "FTIReports");

            migrationBuilder.DropForeignKey(
                name: "FK_FTIReports_Users_UpdatedById",
                table: "FTIReports");

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
                name: "FK_FTITeachingAidsDeveloped_FTIProgramContentAndResources_FTIProgramContentAndResourcesID",
                table: "FTITeachingAidsDeveloped");

            migrationBuilder.DropForeignKey(
                name: "FK_FTITeachingAidsDeveloped_Users_CreatedById",
                table: "FTITeachingAidsDeveloped");

            migrationBuilder.DropForeignKey(
                name: "FK_FTITeachingAidsDeveloped_Users_UpdatedById",
                table: "FTITeachingAidsDeveloped");

            migrationBuilder.DropForeignKey(
                name: "FK_FTITopicsCoveredInClass_FTIProgramContentAndResources_FTIProgramContentAndResourcesID",
                table: "FTITopicsCoveredInClass");

            migrationBuilder.DropForeignKey(
                name: "FK_FTITopicsCoveredInClass_Users_CreatedById",
                table: "FTITopicsCoveredInClass");

            migrationBuilder.DropForeignKey(
                name: "FK_FTITopicsCoveredInClass_Users_UpdatedById",
                table: "FTITopicsCoveredInClass");

            migrationBuilder.DropTable(
                name: "FtiOtherActivities");

            migrationBuilder.DropTable(
                name: "FtiTrainingPrograms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FTITopicsCoveredInClass",
                table: "FTITopicsCoveredInClass");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FTITeachingAidsDeveloped",
                table: "FTITeachingAidsDeveloped");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FTIResourcePersons",
                table: "FTIResourcePersons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FTIReports",
                table: "FTIReports");

            migrationBuilder.DropIndex(
                name: "IX_FTIReports_FTIProgramDetailsID",
                table: "FTIReports");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FTIRecommendations",
                table: "FTIRecommendations");

            migrationBuilder.DropIndex(
                name: "IX_FTIRecommendations_FTIProgramDetailsID",
                table: "FTIRecommendations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FTIProgramDetails",
                table: "FTIProgramDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FTIProgramContentAndResources",
                table: "FTIProgramContentAndResources");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FTIParticipantDemographics",
                table: "FTIParticipantDemographics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FTIAdvisoryServices",
                table: "FTIAdvisoryServices");

            migrationBuilder.DropIndex(
                name: "IX_FTIAdvisoryServices_FTIProgramDetailsID",
                table: "FTIAdvisoryServices");

            migrationBuilder.DropIndex(
                name: "IX_FIUProgramActivities_CreatedAt",
                table: "FIUProgramActivities");

            migrationBuilder.DropIndex(
                name: "IX_FIUProgramActivities_FormStatus",
                table: "FIUProgramActivities");

            migrationBuilder.DropIndex(
                name: "IX_FIUActivities_DisplayOrder",
                table: "FIUActivities");

            migrationBuilder.DropColumn(
                name: "Other",
                table: "FTITeachingAidsDeveloped");

            migrationBuilder.DropColumn(
                name: "AreaHa",
                table: "FTIProgramDetails");

            migrationBuilder.DropColumn(
                name: "Mode",
                table: "FTIProgramDetails");

            migrationBuilder.DropColumn(
                name: "OrganizerAddress",
                table: "FTIProgramDetails");

            migrationBuilder.DropColumn(
                name: "OrganizerFileUpload",
                table: "FTIProgramDetails");

            migrationBuilder.DropColumn(
                name: "PaperPosterAbstract",
                table: "FTIProgramDetails");

            migrationBuilder.DropColumn(
                name: "PaperPosterAbstractDate",
                table: "FTIProgramDetails");

            migrationBuilder.DropColumn(
                name: "PaperPosterAbstractLink",
                table: "FTIProgramDetails");

            migrationBuilder.DropColumn(
                name: "ParticipatedAs",
                table: "FTIProgramDetails");

            migrationBuilder.DropColumn(
                name: "Participation",
                table: "FTIProgramDetails");

            migrationBuilder.DropColumn(
                name: "ParticipationFileLink",
                table: "FTIProgramDetails");

            migrationBuilder.DropColumn(
                name: "SourceOfInformation",
                table: "FTIProgramDetails");

            migrationBuilder.DropColumn(
                name: "TitleOfThesisOrProjectOrPaperOrOthers",
                table: "FTIProgramDetails");

            migrationBuilder.DropColumn(
                name: "UploadVideo",
                table: "FTIProgramDetails");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "FTIProgramContentAndResources");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "FTIProgramContentAndResources");

            migrationBuilder.DropColumn(
                name: "FIUActivitiesId",
                table: "ASMVisitorDetails");

            migrationBuilder.RenameTable(
                name: "FTITopicsCoveredInClass",
                newName: "FtiTopicsCoveredInClass");

            migrationBuilder.RenameTable(
                name: "FTITeachingAidsDeveloped",
                newName: "FtiTeachingAidsDeveloped");

            migrationBuilder.RenameTable(
                name: "FTIResourcePersons",
                newName: "FtiResourcePersons");

            migrationBuilder.RenameTable(
                name: "FTIReports",
                newName: "FtiReports");

            migrationBuilder.RenameTable(
                name: "FTIRecommendations",
                newName: "FtiRecommendations");

            migrationBuilder.RenameTable(
                name: "FTIProgramDetails",
                newName: "FtiProgramDetails");

            migrationBuilder.RenameTable(
                name: "FTIProgramContentAndResources",
                newName: "FtiProgramContentAndResources");

            migrationBuilder.RenameTable(
                name: "FTIParticipantDemographics",
                newName: "FtiParticipantDemographics");

            migrationBuilder.RenameTable(
                name: "FTIAdvisoryServices",
                newName: "FtiAdvisoryServices");

            migrationBuilder.RenameColumn(
                name: "FTIProgramContentAndResourcesID",
                table: "FtiTopicsCoveredInClass",
                newName: "FtiProgramContentAndResourcesId");

            migrationBuilder.RenameIndex(
                name: "IX_FTITopicsCoveredInClass_UpdatedById",
                table: "FtiTopicsCoveredInClass",
                newName: "IX_FtiTopicsCoveredInClass_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_FTITopicsCoveredInClass_FTIProgramContentAndResourcesID",
                table: "FtiTopicsCoveredInClass",
                newName: "IX_FtiTopicsCoveredInClass_FtiProgramContentAndResourcesId");

            migrationBuilder.RenameIndex(
                name: "IX_FTITopicsCoveredInClass_CreatedById",
                table: "FtiTopicsCoveredInClass",
                newName: "IX_FtiTopicsCoveredInClass_CreatedById");

            migrationBuilder.RenameColumn(
                name: "FTIProgramContentAndResourcesID",
                table: "FtiTeachingAidsDeveloped",
                newName: "FtiProgramContentAndResourcesId");

            migrationBuilder.RenameColumn(
                name: "TypeOfAidDeveloped",
                table: "FtiTeachingAidsDeveloped",
                newName: "OtherTypeOfAid");

            migrationBuilder.RenameIndex(
                name: "IX_FTITeachingAidsDeveloped_UpdatedById",
                table: "FtiTeachingAidsDeveloped",
                newName: "IX_FtiTeachingAidsDeveloped_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_FTITeachingAidsDeveloped_FTIProgramContentAndResourcesID",
                table: "FtiTeachingAidsDeveloped",
                newName: "IX_FtiTeachingAidsDeveloped_FtiProgramContentAndResourcesId");

            migrationBuilder.RenameIndex(
                name: "IX_FTITeachingAidsDeveloped_CreatedById",
                table: "FtiTeachingAidsDeveloped",
                newName: "IX_FtiTeachingAidsDeveloped_CreatedById");

            migrationBuilder.RenameColumn(
                name: "FTIProgramContentAndResourcesID",
                table: "FtiResourcePersons",
                newName: "FtiProgramContentAndResourcesId");

            migrationBuilder.RenameIndex(
                name: "IX_FTIResourcePersons_UpdatedById",
                table: "FtiResourcePersons",
                newName: "IX_FtiResourcePersons_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_FTIResourcePersons_FTIProgramContentAndResourcesID",
                table: "FtiResourcePersons",
                newName: "IX_FtiResourcePersons_FtiProgramContentAndResourcesId");

            migrationBuilder.RenameIndex(
                name: "IX_FTIResourcePersons_CreatedById",
                table: "FtiResourcePersons",
                newName: "IX_FtiResourcePersons_CreatedById");

            migrationBuilder.RenameColumn(
                name: "FTIProgramDetailsID",
                table: "FtiReports",
                newName: "FtiProgramDetailsId");

            migrationBuilder.RenameIndex(
                name: "IX_FTIReports_UpdatedById",
                table: "FtiReports",
                newName: "IX_FtiReports_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_FTIReports_CreatedById",
                table: "FtiReports",
                newName: "IX_FtiReports_CreatedById");

            migrationBuilder.RenameColumn(
                name: "FTIProgramDetailsID",
                table: "FtiRecommendations",
                newName: "FtiProgramDetailsId");

            migrationBuilder.RenameIndex(
                name: "IX_FTIRecommendations_UpdatedById",
                table: "FtiRecommendations",
                newName: "IX_FtiRecommendations_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_FTIRecommendations_CreatedById",
                table: "FtiRecommendations",
                newName: "IX_FtiRecommendations_CreatedById");

            migrationBuilder.RenameColumn(
                name: "Theme",
                table: "FtiProgramDetails",
                newName: "ThemeId");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "FtiProgramDetails",
                newName: "StatusId");

            migrationBuilder.RenameColumn(
                name: "SD",
                table: "FtiProgramDetails",
                newName: "ModeId");

            migrationBuilder.RenameColumn(
                name: "Region",
                table: "FtiProgramDetails",
                newName: "RegionId");

            migrationBuilder.RenameColumn(
                name: "CollaboratorOther",
                table: "FtiProgramDetails",
                newName: "OrganizerInstitutionAddress");

            migrationBuilder.RenameColumn(
                name: "CollaboratorId",
                table: "FtiProgramDetails",
                newName: "SourceOfFundId");

            migrationBuilder.RenameIndex(
                name: "IX_FTIProgramDetails_UpdatedById",
                table: "FtiProgramDetails",
                newName: "IX_FtiProgramDetails_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_FTIProgramDetails_TypeId",
                table: "FtiProgramDetails",
                newName: "IX_FtiProgramDetails_TypeId");

            migrationBuilder.RenameIndex(
                name: "IX_FTIProgramDetails_ThematicAreaId",
                table: "FtiProgramDetails",
                newName: "IX_FtiProgramDetails_ThematicAreaId");

            migrationBuilder.RenameIndex(
                name: "IX_FTIProgramDetails_CreatedById",
                table: "FtiProgramDetails",
                newName: "IX_FtiProgramDetails_CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_FTIProgramDetails_CategoryId",
                table: "FtiProgramDetails",
                newName: "IX_FtiProgramDetails_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_FTIProgramDetails_CollaboratorId",
                table: "FtiProgramDetails",
                newName: "IX_FtiProgramDetails_SourceOfFundId");

            migrationBuilder.RenameColumn(
                name: "FTIProgramDetailsID",
                table: "FtiProgramContentAndResources",
                newName: "FtiProgramDetailsId");

            migrationBuilder.RenameIndex(
                name: "IX_FTIProgramContentAndResources_UpdatedById",
                table: "FtiProgramContentAndResources",
                newName: "IX_FtiProgramContentAndResources_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_FTIProgramContentAndResources_FTIProgramDetailsID",
                table: "FtiProgramContentAndResources",
                newName: "IX_FtiProgramContentAndResources_FtiProgramDetailsId");

            migrationBuilder.RenameIndex(
                name: "IX_FTIProgramContentAndResources_CreatedById",
                table: "FtiProgramContentAndResources",
                newName: "IX_FtiProgramContentAndResources_CreatedById");

            migrationBuilder.RenameColumn(
                name: "FTIProgramDetailsID",
                table: "FtiParticipantDemographics",
                newName: "FtiProgramDetailsId");

            migrationBuilder.RenameColumn(
                name: "Participant",
                table: "FtiParticipantDemographics",
                newName: "ParticipantId");

            migrationBuilder.RenameIndex(
                name: "IX_FTIParticipantDemographics_UpdatedById",
                table: "FtiParticipantDemographics",
                newName: "IX_FtiParticipantDemographics_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_FTIParticipantDemographics_FTIProgramDetailsID",
                table: "FtiParticipantDemographics",
                newName: "IX_FtiParticipantDemographics_FtiProgramDetailsId");

            migrationBuilder.RenameIndex(
                name: "IX_FTIParticipantDemographics_CreatedById",
                table: "FtiParticipantDemographics",
                newName: "IX_FtiParticipantDemographics_CreatedById");

            migrationBuilder.RenameColumn(
                name: "FTIProgramDetailsID",
                table: "FtiAdvisoryServices",
                newName: "FtiProgramDetailsId");

            migrationBuilder.RenameIndex(
                name: "IX_FTIAdvisoryServices_UpdatedById",
                table: "FtiAdvisoryServices",
                newName: "IX_FtiAdvisoryServices_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_FTIAdvisoryServices_CreatedById",
                table: "FtiAdvisoryServices",
                newName: "IX_FtiAdvisoryServices_CreatedById");

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "FtiTopicsCoveredInClass",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnitLocationId",
                table: "FtiTopicsCoveredInClass",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "FtiTeachingAidsDeveloped",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TypeOfAidId",
                table: "FtiTeachingAidsDeveloped",
                type: "int",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnitLocationId",
                table: "FtiTeachingAidsDeveloped",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "FtiResourcePersons",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnitLocationId",
                table: "FtiResourcePersons",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "FtiReports",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnitLocationId",
                table: "FtiReports",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "FtiRecommendations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnitLocationId",
                table: "FtiRecommendations",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "FtiProgramDetails",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "StartDate",
                table: "FtiProgramDetails",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SponsoredOrganization",
                table: "FtiProgramDetails",
                type: "int",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "OrganizerInstitutionName",
                table: "FtiProgramDetails",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ApprovedAt",
                table: "FtiProgramDetails",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApprovedById",
                table: "FtiProgramDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Attachements",
                table: "FtiProgramDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Copi",
                table: "FtiProgramDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FormStatus",
                table: "FtiProgramDetails",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FormStatusRemarks",
                table: "FtiProgramDetails",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Funds",
                table: "FtiProgramDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "FtiProgramDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "OrganizerBroucherFile",
                table: "FtiProgramDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SourceId",
                table: "FtiProgramDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnitLocationId",
                table: "FtiProgramDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "FtiProgramDetailsId",
                table: "FtiProgramContentAndResources",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "FtiProgramContentAndResources",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnitLocationId",
                table: "FtiProgramContentAndResources",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "FtiParticipantDemographics",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnitLocationId",
                table: "FtiParticipantDemographics",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NoOfWhatsappSMS",
                table: "FtiAdvisoryServices",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NoOfWhatsappGroups",
                table: "FtiAdvisoryServices",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NoOfSMSSentToRegisteredFarmers",
                table: "FtiAdvisoryServices",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NoOfPhoneCalls",
                table: "FtiAdvisoryServices",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NoOfNewspaperCoverage",
                table: "FtiAdvisoryServices",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NoOfGroupDiscussions",
                table: "FtiAdvisoryServices",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NoOfFacebookSMS",
                table: "FtiAdvisoryServices",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NoOfFaceToFaceDiscussions",
                table: "FtiAdvisoryServices",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NoOfEmailsSent",
                table: "FtiAdvisoryServices",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NoOfBeneficiaries",
                table: "FtiAdvisoryServices",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NoOfAnsweredWhatsappQueries",
                table: "FtiAdvisoryServices",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "FtiAdvisoryServices",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnitLocationId",
                table: "FtiAdvisoryServices",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "SubmittedDate",
                table: "ASMVisitorDetails",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FtiTopicsCoveredInClass",
                table: "FtiTopicsCoveredInClass",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FtiTeachingAidsDeveloped",
                table: "FtiTeachingAidsDeveloped",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FtiResourcePersons",
                table: "FtiResourcePersons",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FtiReports",
                table: "FtiReports",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FtiRecommendations",
                table: "FtiRecommendations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FtiProgramDetails",
                table: "FtiProgramDetails",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FtiProgramContentAndResources",
                table: "FtiProgramContentAndResources",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FtiParticipantDemographics",
                table: "FtiParticipantDemographics",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FtiAdvisoryServices",
                table: "FtiAdvisoryServices",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_FtiTeachingAidsDeveloped_TypeOfAidId",
                table: "FtiTeachingAidsDeveloped",
                column: "TypeOfAidId");

            migrationBuilder.CreateIndex(
                name: "IX_FtiReports_FtiProgramDetailsId",
                table: "FtiReports",
                column: "FtiProgramDetailsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FtiRecommendations_FtiProgramDetailsId",
                table: "FtiRecommendations",
                column: "FtiProgramDetailsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FtiProgramDetails_ApprovedById",
                table: "FtiProgramDetails",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_FtiProgramDetails_ModeId",
                table: "FtiProgramDetails",
                column: "ModeId");

            migrationBuilder.CreateIndex(
                name: "IX_FtiProgramDetails_OrganizationId",
                table: "FtiProgramDetails",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_FtiProgramDetails_ProgramTypeId",
                table: "FtiProgramDetails",
                column: "ProgramTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_FtiProgramDetails_RegionId",
                table: "FtiProgramDetails",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_FtiProgramDetails_SourceId",
                table: "FtiProgramDetails",
                column: "SourceId");

            migrationBuilder.CreateIndex(
                name: "IX_FtiProgramDetails_StatusId",
                table: "FtiProgramDetails",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_FtiProgramDetails_ThemeId",
                table: "FtiProgramDetails",
                column: "ThemeId");

            migrationBuilder.CreateIndex(
                name: "IX_FtiProgramDetails_UnitLocationId",
                table: "FtiProgramDetails",
                column: "UnitLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_FtiParticipantDemographics_ParticipantId",
                table: "FtiParticipantDemographics",
                column: "ParticipantId");

            migrationBuilder.CreateIndex(
                name: "IX_FtiAdvisoryServices_FtiProgramDetailsId",
                table: "FtiAdvisoryServices",
                column: "FtiProgramDetailsId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FtiAdvisoryServices_FtiProgramDetails_FtiProgramDetailsId",
                table: "FtiAdvisoryServices",
                column: "FtiProgramDetailsId",
                principalTable: "FtiProgramDetails",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FtiAdvisoryServices_Users_CreatedById",
                table: "FtiAdvisoryServices",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FtiAdvisoryServices_Users_UpdatedById",
                table: "FtiAdvisoryServices",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FtiParticipantDemographics_FtiProgramDetails_FtiProgramDetailsId",
                table: "FtiParticipantDemographics",
                column: "FtiProgramDetailsId",
                principalTable: "FtiProgramDetails",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FtiParticipantDemographics_ParticipantDealer_ParticipantId",
                table: "FtiParticipantDemographics",
                column: "ParticipantId",
                principalTable: "ParticipantDealer",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FtiParticipantDemographics_Users_CreatedById",
                table: "FtiParticipantDemographics",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FtiParticipantDemographics_Users_UpdatedById",
                table: "FtiParticipantDemographics",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FtiProgramContentAndResources_FtiProgramDetails_FtiProgramDetailsId",
                table: "FtiProgramContentAndResources",
                column: "FtiProgramDetailsId",
                principalTable: "FtiProgramDetails",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FtiProgramContentAndResources_Users_CreatedById",
                table: "FtiProgramContentAndResources",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FtiProgramContentAndResources_Users_UpdatedById",
                table: "FtiProgramContentAndResources",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FtiProgramDetails_InfoTypes_TypeId",
                table: "FtiProgramDetails",
                column: "TypeId",
                principalTable: "InfoTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FtiProgramDetails_Modes_ModeId",
                table: "FtiProgramDetails",
                column: "ModeId",
                principalTable: "Modes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FtiProgramDetails_OrganizationUnitLocations_UnitLocationId",
                table: "FtiProgramDetails",
                column: "UnitLocationId",
                principalTable: "OrganizationUnitLocations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FtiProgramDetails_Organizations_OrganizationId",
                table: "FtiProgramDetails",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FtiProgramDetails_ParticipatedSources_SourceId",
                table: "FtiProgramDetails",
                column: "SourceId",
                principalTable: "ParticipatedSources",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FtiProgramDetails_ProgramCategories_CategoryId",
                table: "FtiProgramDetails",
                column: "CategoryId",
                principalTable: "ProgramCategories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FtiProgramDetails_ProgramTypes_ProgramTypeId",
                table: "FtiProgramDetails",
                column: "ProgramTypeId",
                principalTable: "ProgramTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FtiProgramDetails_Regions_RegionId",
                table: "FtiProgramDetails",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FtiProgramDetails_SourcesOfFunds_SourceOfFundId",
                table: "FtiProgramDetails",
                column: "SourceOfFundId",
                principalTable: "SourcesOfFunds",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FtiProgramDetails_Statuses_StatusId",
                table: "FtiProgramDetails",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FtiProgramDetails_ThematicAreas_ThematicAreaId",
                table: "FtiProgramDetails",
                column: "ThematicAreaId",
                principalTable: "ThematicAreas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FtiProgramDetails_Themes_ThemeId",
                table: "FtiProgramDetails",
                column: "ThemeId",
                principalTable: "Themes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FtiProgramDetails_Users_ApprovedById",
                table: "FtiProgramDetails",
                column: "ApprovedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FtiProgramDetails_Users_CreatedById",
                table: "FtiProgramDetails",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FtiProgramDetails_Users_UpdatedById",
                table: "FtiProgramDetails",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FtiRecommendations_FtiProgramDetails_FtiProgramDetailsId",
                table: "FtiRecommendations",
                column: "FtiProgramDetailsId",
                principalTable: "FtiProgramDetails",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FtiRecommendations_Users_CreatedById",
                table: "FtiRecommendations",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FtiRecommendations_Users_UpdatedById",
                table: "FtiRecommendations",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FtiReports_FtiProgramDetails_FtiProgramDetailsId",
                table: "FtiReports",
                column: "FtiProgramDetailsId",
                principalTable: "FtiProgramDetails",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FtiReports_Users_CreatedById",
                table: "FtiReports",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FtiReports_Users_UpdatedById",
                table: "FtiReports",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FtiResourcePersons_FtiProgramContentAndResources_FtiProgramContentAndResourcesId",
                table: "FtiResourcePersons",
                column: "FtiProgramContentAndResourcesId",
                principalTable: "FtiProgramContentAndResources",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FtiResourcePersons_Users_CreatedById",
                table: "FtiResourcePersons",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FtiResourcePersons_Users_UpdatedById",
                table: "FtiResourcePersons",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FtiTeachingAidsDeveloped_FtiProgramContentAndResources_FtiProgramContentAndResourcesId",
                table: "FtiTeachingAidsDeveloped",
                column: "FtiProgramContentAndResourcesId",
                principalTable: "FtiProgramContentAndResources",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FtiTeachingAidsDeveloped_TypeOfAids_TypeOfAidId",
                table: "FtiTeachingAidsDeveloped",
                column: "TypeOfAidId",
                principalTable: "TypeOfAids",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FtiTeachingAidsDeveloped_Users_CreatedById",
                table: "FtiTeachingAidsDeveloped",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FtiTeachingAidsDeveloped_Users_UpdatedById",
                table: "FtiTeachingAidsDeveloped",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FtiTopicsCoveredInClass_FtiProgramContentAndResources_FtiProgramContentAndResourcesId",
                table: "FtiTopicsCoveredInClass",
                column: "FtiProgramContentAndResourcesId",
                principalTable: "FtiProgramContentAndResources",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FtiTopicsCoveredInClass_Users_CreatedById",
                table: "FtiTopicsCoveredInClass",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FtiTopicsCoveredInClass_Users_UpdatedById",
                table: "FtiTopicsCoveredInClass",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FtiAdvisoryServices_FtiProgramDetails_FtiProgramDetailsId",
                table: "FtiAdvisoryServices");

            migrationBuilder.DropForeignKey(
                name: "FK_FtiAdvisoryServices_Users_CreatedById",
                table: "FtiAdvisoryServices");

            migrationBuilder.DropForeignKey(
                name: "FK_FtiAdvisoryServices_Users_UpdatedById",
                table: "FtiAdvisoryServices");

            migrationBuilder.DropForeignKey(
                name: "FK_FtiParticipantDemographics_FtiProgramDetails_FtiProgramDetailsId",
                table: "FtiParticipantDemographics");

            migrationBuilder.DropForeignKey(
                name: "FK_FtiParticipantDemographics_ParticipantDealer_ParticipantId",
                table: "FtiParticipantDemographics");

            migrationBuilder.DropForeignKey(
                name: "FK_FtiParticipantDemographics_Users_CreatedById",
                table: "FtiParticipantDemographics");

            migrationBuilder.DropForeignKey(
                name: "FK_FtiParticipantDemographics_Users_UpdatedById",
                table: "FtiParticipantDemographics");

            migrationBuilder.DropForeignKey(
                name: "FK_FtiProgramContentAndResources_FtiProgramDetails_FtiProgramDetailsId",
                table: "FtiProgramContentAndResources");

            migrationBuilder.DropForeignKey(
                name: "FK_FtiProgramContentAndResources_Users_CreatedById",
                table: "FtiProgramContentAndResources");

            migrationBuilder.DropForeignKey(
                name: "FK_FtiProgramContentAndResources_Users_UpdatedById",
                table: "FtiProgramContentAndResources");

            migrationBuilder.DropForeignKey(
                name: "FK_FtiProgramDetails_InfoTypes_TypeId",
                table: "FtiProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_FtiProgramDetails_Modes_ModeId",
                table: "FtiProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_FtiProgramDetails_OrganizationUnitLocations_UnitLocationId",
                table: "FtiProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_FtiProgramDetails_Organizations_OrganizationId",
                table: "FtiProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_FtiProgramDetails_ParticipatedSources_SourceId",
                table: "FtiProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_FtiProgramDetails_ProgramCategories_CategoryId",
                table: "FtiProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_FtiProgramDetails_ProgramTypes_ProgramTypeId",
                table: "FtiProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_FtiProgramDetails_Regions_RegionId",
                table: "FtiProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_FtiProgramDetails_SourcesOfFunds_SourceOfFundId",
                table: "FtiProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_FtiProgramDetails_Statuses_StatusId",
                table: "FtiProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_FtiProgramDetails_ThematicAreas_ThematicAreaId",
                table: "FtiProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_FtiProgramDetails_Themes_ThemeId",
                table: "FtiProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_FtiProgramDetails_Users_ApprovedById",
                table: "FtiProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_FtiProgramDetails_Users_CreatedById",
                table: "FtiProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_FtiProgramDetails_Users_UpdatedById",
                table: "FtiProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_FtiRecommendations_FtiProgramDetails_FtiProgramDetailsId",
                table: "FtiRecommendations");

            migrationBuilder.DropForeignKey(
                name: "FK_FtiRecommendations_Users_CreatedById",
                table: "FtiRecommendations");

            migrationBuilder.DropForeignKey(
                name: "FK_FtiRecommendations_Users_UpdatedById",
                table: "FtiRecommendations");

            migrationBuilder.DropForeignKey(
                name: "FK_FtiReports_FtiProgramDetails_FtiProgramDetailsId",
                table: "FtiReports");

            migrationBuilder.DropForeignKey(
                name: "FK_FtiReports_Users_CreatedById",
                table: "FtiReports");

            migrationBuilder.DropForeignKey(
                name: "FK_FtiReports_Users_UpdatedById",
                table: "FtiReports");

            migrationBuilder.DropForeignKey(
                name: "FK_FtiResourcePersons_FtiProgramContentAndResources_FtiProgramContentAndResourcesId",
                table: "FtiResourcePersons");

            migrationBuilder.DropForeignKey(
                name: "FK_FtiResourcePersons_Users_CreatedById",
                table: "FtiResourcePersons");

            migrationBuilder.DropForeignKey(
                name: "FK_FtiResourcePersons_Users_UpdatedById",
                table: "FtiResourcePersons");

            migrationBuilder.DropForeignKey(
                name: "FK_FtiTeachingAidsDeveloped_FtiProgramContentAndResources_FtiProgramContentAndResourcesId",
                table: "FtiTeachingAidsDeveloped");

            migrationBuilder.DropForeignKey(
                name: "FK_FtiTeachingAidsDeveloped_TypeOfAids_TypeOfAidId",
                table: "FtiTeachingAidsDeveloped");

            migrationBuilder.DropForeignKey(
                name: "FK_FtiTeachingAidsDeveloped_Users_CreatedById",
                table: "FtiTeachingAidsDeveloped");

            migrationBuilder.DropForeignKey(
                name: "FK_FtiTeachingAidsDeveloped_Users_UpdatedById",
                table: "FtiTeachingAidsDeveloped");

            migrationBuilder.DropForeignKey(
                name: "FK_FtiTopicsCoveredInClass_FtiProgramContentAndResources_FtiProgramContentAndResourcesId",
                table: "FtiTopicsCoveredInClass");

            migrationBuilder.DropForeignKey(
                name: "FK_FtiTopicsCoveredInClass_Users_CreatedById",
                table: "FtiTopicsCoveredInClass");

            migrationBuilder.DropForeignKey(
                name: "FK_FtiTopicsCoveredInClass_Users_UpdatedById",
                table: "FtiTopicsCoveredInClass");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FtiTopicsCoveredInClass",
                table: "FtiTopicsCoveredInClass");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FtiTeachingAidsDeveloped",
                table: "FtiTeachingAidsDeveloped");

            migrationBuilder.DropIndex(
                name: "IX_FtiTeachingAidsDeveloped_TypeOfAidId",
                table: "FtiTeachingAidsDeveloped");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FtiResourcePersons",
                table: "FtiResourcePersons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FtiReports",
                table: "FtiReports");

            migrationBuilder.DropIndex(
                name: "IX_FtiReports_FtiProgramDetailsId",
                table: "FtiReports");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FtiRecommendations",
                table: "FtiRecommendations");

            migrationBuilder.DropIndex(
                name: "IX_FtiRecommendations_FtiProgramDetailsId",
                table: "FtiRecommendations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FtiProgramDetails",
                table: "FtiProgramDetails");

            migrationBuilder.DropIndex(
                name: "IX_FtiProgramDetails_ApprovedById",
                table: "FtiProgramDetails");

            migrationBuilder.DropIndex(
                name: "IX_FtiProgramDetails_ModeId",
                table: "FtiProgramDetails");

            migrationBuilder.DropIndex(
                name: "IX_FtiProgramDetails_OrganizationId",
                table: "FtiProgramDetails");

            migrationBuilder.DropIndex(
                name: "IX_FtiProgramDetails_ProgramTypeId",
                table: "FtiProgramDetails");

            migrationBuilder.DropIndex(
                name: "IX_FtiProgramDetails_RegionId",
                table: "FtiProgramDetails");

            migrationBuilder.DropIndex(
                name: "IX_FtiProgramDetails_SourceId",
                table: "FtiProgramDetails");

            migrationBuilder.DropIndex(
                name: "IX_FtiProgramDetails_StatusId",
                table: "FtiProgramDetails");

            migrationBuilder.DropIndex(
                name: "IX_FtiProgramDetails_ThemeId",
                table: "FtiProgramDetails");

            migrationBuilder.DropIndex(
                name: "IX_FtiProgramDetails_UnitLocationId",
                table: "FtiProgramDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FtiProgramContentAndResources",
                table: "FtiProgramContentAndResources");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FtiParticipantDemographics",
                table: "FtiParticipantDemographics");

            migrationBuilder.DropIndex(
                name: "IX_FtiParticipantDemographics_ParticipantId",
                table: "FtiParticipantDemographics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FtiAdvisoryServices",
                table: "FtiAdvisoryServices");

            migrationBuilder.DropIndex(
                name: "IX_FtiAdvisoryServices_FtiProgramDetailsId",
                table: "FtiAdvisoryServices");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "FtiTopicsCoveredInClass");

            migrationBuilder.DropColumn(
                name: "UnitLocationId",
                table: "FtiTopicsCoveredInClass");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "FtiTeachingAidsDeveloped");

            migrationBuilder.DropColumn(
                name: "TypeOfAidId",
                table: "FtiTeachingAidsDeveloped");

            migrationBuilder.DropColumn(
                name: "UnitLocationId",
                table: "FtiTeachingAidsDeveloped");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "FtiResourcePersons");

            migrationBuilder.DropColumn(
                name: "UnitLocationId",
                table: "FtiResourcePersons");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "FtiReports");

            migrationBuilder.DropColumn(
                name: "UnitLocationId",
                table: "FtiReports");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "FtiRecommendations");

            migrationBuilder.DropColumn(
                name: "UnitLocationId",
                table: "FtiRecommendations");

            migrationBuilder.DropColumn(
                name: "ApprovedAt",
                table: "FtiProgramDetails");

            migrationBuilder.DropColumn(
                name: "ApprovedById",
                table: "FtiProgramDetails");

            migrationBuilder.DropColumn(
                name: "Attachements",
                table: "FtiProgramDetails");

            migrationBuilder.DropColumn(
                name: "Copi",
                table: "FtiProgramDetails");

            migrationBuilder.DropColumn(
                name: "FormStatus",
                table: "FtiProgramDetails");

            migrationBuilder.DropColumn(
                name: "FormStatusRemarks",
                table: "FtiProgramDetails");

            migrationBuilder.DropColumn(
                name: "Funds",
                table: "FtiProgramDetails");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "FtiProgramDetails");

            migrationBuilder.DropColumn(
                name: "OrganizerBroucherFile",
                table: "FtiProgramDetails");

            migrationBuilder.DropColumn(
                name: "SourceId",
                table: "FtiProgramDetails");

            migrationBuilder.DropColumn(
                name: "UnitLocationId",
                table: "FtiProgramDetails");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "FtiProgramContentAndResources");

            migrationBuilder.DropColumn(
                name: "UnitLocationId",
                table: "FtiProgramContentAndResources");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "FtiParticipantDemographics");

            migrationBuilder.DropColumn(
                name: "UnitLocationId",
                table: "FtiParticipantDemographics");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "FtiAdvisoryServices");

            migrationBuilder.DropColumn(
                name: "UnitLocationId",
                table: "FtiAdvisoryServices");

            migrationBuilder.RenameTable(
                name: "FtiTopicsCoveredInClass",
                newName: "FTITopicsCoveredInClass");

            migrationBuilder.RenameTable(
                name: "FtiTeachingAidsDeveloped",
                newName: "FTITeachingAidsDeveloped");

            migrationBuilder.RenameTable(
                name: "FtiResourcePersons",
                newName: "FTIResourcePersons");

            migrationBuilder.RenameTable(
                name: "FtiReports",
                newName: "FTIReports");

            migrationBuilder.RenameTable(
                name: "FtiRecommendations",
                newName: "FTIRecommendations");

            migrationBuilder.RenameTable(
                name: "FtiProgramDetails",
                newName: "FTIProgramDetails");

            migrationBuilder.RenameTable(
                name: "FtiProgramContentAndResources",
                newName: "FTIProgramContentAndResources");

            migrationBuilder.RenameTable(
                name: "FtiParticipantDemographics",
                newName: "FTIParticipantDemographics");

            migrationBuilder.RenameTable(
                name: "FtiAdvisoryServices",
                newName: "FTIAdvisoryServices");

            migrationBuilder.RenameColumn(
                name: "FtiProgramContentAndResourcesId",
                table: "FTITopicsCoveredInClass",
                newName: "FTIProgramContentAndResourcesID");

            migrationBuilder.RenameIndex(
                name: "IX_FtiTopicsCoveredInClass_UpdatedById",
                table: "FTITopicsCoveredInClass",
                newName: "IX_FTITopicsCoveredInClass_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_FtiTopicsCoveredInClass_FtiProgramContentAndResourcesId",
                table: "FTITopicsCoveredInClass",
                newName: "IX_FTITopicsCoveredInClass_FTIProgramContentAndResourcesID");

            migrationBuilder.RenameIndex(
                name: "IX_FtiTopicsCoveredInClass_CreatedById",
                table: "FTITopicsCoveredInClass",
                newName: "IX_FTITopicsCoveredInClass_CreatedById");

            migrationBuilder.RenameColumn(
                name: "FtiProgramContentAndResourcesId",
                table: "FTITeachingAidsDeveloped",
                newName: "FTIProgramContentAndResourcesID");

            migrationBuilder.RenameColumn(
                name: "OtherTypeOfAid",
                table: "FTITeachingAidsDeveloped",
                newName: "TypeOfAidDeveloped");

            migrationBuilder.RenameIndex(
                name: "IX_FtiTeachingAidsDeveloped_UpdatedById",
                table: "FTITeachingAidsDeveloped",
                newName: "IX_FTITeachingAidsDeveloped_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_FtiTeachingAidsDeveloped_FtiProgramContentAndResourcesId",
                table: "FTITeachingAidsDeveloped",
                newName: "IX_FTITeachingAidsDeveloped_FTIProgramContentAndResourcesID");

            migrationBuilder.RenameIndex(
                name: "IX_FtiTeachingAidsDeveloped_CreatedById",
                table: "FTITeachingAidsDeveloped",
                newName: "IX_FTITeachingAidsDeveloped_CreatedById");

            migrationBuilder.RenameColumn(
                name: "FtiProgramContentAndResourcesId",
                table: "FTIResourcePersons",
                newName: "FTIProgramContentAndResourcesID");

            migrationBuilder.RenameIndex(
                name: "IX_FtiResourcePersons_UpdatedById",
                table: "FTIResourcePersons",
                newName: "IX_FTIResourcePersons_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_FtiResourcePersons_FtiProgramContentAndResourcesId",
                table: "FTIResourcePersons",
                newName: "IX_FTIResourcePersons_FTIProgramContentAndResourcesID");

            migrationBuilder.RenameIndex(
                name: "IX_FtiResourcePersons_CreatedById",
                table: "FTIResourcePersons",
                newName: "IX_FTIResourcePersons_CreatedById");

            migrationBuilder.RenameColumn(
                name: "FtiProgramDetailsId",
                table: "FTIReports",
                newName: "FTIProgramDetailsID");

            migrationBuilder.RenameIndex(
                name: "IX_FtiReports_UpdatedById",
                table: "FTIReports",
                newName: "IX_FTIReports_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_FtiReports_CreatedById",
                table: "FTIReports",
                newName: "IX_FTIReports_CreatedById");

            migrationBuilder.RenameColumn(
                name: "FtiProgramDetailsId",
                table: "FTIRecommendations",
                newName: "FTIProgramDetailsID");

            migrationBuilder.RenameIndex(
                name: "IX_FtiRecommendations_UpdatedById",
                table: "FTIRecommendations",
                newName: "IX_FTIRecommendations_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_FtiRecommendations_CreatedById",
                table: "FTIRecommendations",
                newName: "IX_FTIRecommendations_CreatedById");

            migrationBuilder.RenameColumn(
                name: "ThemeId",
                table: "FTIProgramDetails",
                newName: "Theme");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "FTIProgramDetails",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "SourceOfFundId",
                table: "FTIProgramDetails",
                newName: "CollaboratorId");

            migrationBuilder.RenameColumn(
                name: "RegionId",
                table: "FTIProgramDetails",
                newName: "Region");

            migrationBuilder.RenameColumn(
                name: "OrganizerInstitutionAddress",
                table: "FTIProgramDetails",
                newName: "CollaboratorOther");

            migrationBuilder.RenameColumn(
                name: "ModeId",
                table: "FTIProgramDetails",
                newName: "SD");

            migrationBuilder.RenameIndex(
                name: "IX_FtiProgramDetails_UpdatedById",
                table: "FTIProgramDetails",
                newName: "IX_FTIProgramDetails_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_FtiProgramDetails_TypeId",
                table: "FTIProgramDetails",
                newName: "IX_FTIProgramDetails_TypeId");

            migrationBuilder.RenameIndex(
                name: "IX_FtiProgramDetails_ThematicAreaId",
                table: "FTIProgramDetails",
                newName: "IX_FTIProgramDetails_ThematicAreaId");

            migrationBuilder.RenameIndex(
                name: "IX_FtiProgramDetails_CreatedById",
                table: "FTIProgramDetails",
                newName: "IX_FTIProgramDetails_CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_FtiProgramDetails_CategoryId",
                table: "FTIProgramDetails",
                newName: "IX_FTIProgramDetails_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_FtiProgramDetails_SourceOfFundId",
                table: "FTIProgramDetails",
                newName: "IX_FTIProgramDetails_CollaboratorId");

            migrationBuilder.RenameColumn(
                name: "FtiProgramDetailsId",
                table: "FTIProgramContentAndResources",
                newName: "FTIProgramDetailsID");

            migrationBuilder.RenameIndex(
                name: "IX_FtiProgramContentAndResources_UpdatedById",
                table: "FTIProgramContentAndResources",
                newName: "IX_FTIProgramContentAndResources_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_FtiProgramContentAndResources_FtiProgramDetailsId",
                table: "FTIProgramContentAndResources",
                newName: "IX_FTIProgramContentAndResources_FTIProgramDetailsID");

            migrationBuilder.RenameIndex(
                name: "IX_FtiProgramContentAndResources_CreatedById",
                table: "FTIProgramContentAndResources",
                newName: "IX_FTIProgramContentAndResources_CreatedById");

            migrationBuilder.RenameColumn(
                name: "FtiProgramDetailsId",
                table: "FTIParticipantDemographics",
                newName: "FTIProgramDetailsID");

            migrationBuilder.RenameColumn(
                name: "ParticipantId",
                table: "FTIParticipantDemographics",
                newName: "Participant");

            migrationBuilder.RenameIndex(
                name: "IX_FtiParticipantDemographics_UpdatedById",
                table: "FTIParticipantDemographics",
                newName: "IX_FTIParticipantDemographics_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_FtiParticipantDemographics_FtiProgramDetailsId",
                table: "FTIParticipantDemographics",
                newName: "IX_FTIParticipantDemographics_FTIProgramDetailsID");

            migrationBuilder.RenameIndex(
                name: "IX_FtiParticipantDemographics_CreatedById",
                table: "FTIParticipantDemographics",
                newName: "IX_FTIParticipantDemographics_CreatedById");

            migrationBuilder.RenameColumn(
                name: "FtiProgramDetailsId",
                table: "FTIAdvisoryServices",
                newName: "FTIProgramDetailsID");

            migrationBuilder.RenameIndex(
                name: "IX_FtiAdvisoryServices_UpdatedById",
                table: "FTIAdvisoryServices",
                newName: "IX_FTIAdvisoryServices_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_FtiAdvisoryServices_CreatedById",
                table: "FTIAdvisoryServices",
                newName: "IX_FTIAdvisoryServices_CreatedById");

            migrationBuilder.AddColumn<string>(
                name: "Other",
                table: "FTITeachingAidsDeveloped",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "FTIProgramDetails",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "StartDate",
                table: "FTIProgramDetails",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<int>(
                name: "SponsoredOrganization",
                table: "FTIProgramDetails",
                type: "int",
                maxLength: 200,
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OrganizerInstitutionName",
                table: "FTIProgramDetails",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "AreaHa",
                table: "FTIProgramDetails",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Mode",
                table: "FTIProgramDetails",
                type: "int",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrganizerAddress",
                table: "FTIProgramDetails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrganizerFileUpload",
                table: "FTIProgramDetails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaperPosterAbstract",
                table: "FTIProgramDetails",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PaperPosterAbstractDate",
                table: "FTIProgramDetails",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaperPosterAbstractLink",
                table: "FTIProgramDetails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ParticipatedAs",
                table: "FTIProgramDetails",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Participation",
                table: "FTIProgramDetails",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ParticipationFileLink",
                table: "FTIProgramDetails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SourceOfInformation",
                table: "FTIProgramDetails",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TitleOfThesisOrProjectOrPaperOrOthers",
                table: "FTIProgramDetails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UploadVideo",
                table: "FTIProgramDetails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FTIProgramDetailsID",
                table: "FTIProgramContentAndResources",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "FTIProgramContentAndResources",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "FTIProgramContentAndResources",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NoOfWhatsappSMS",
                table: "FTIAdvisoryServices",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "NoOfWhatsappGroups",
                table: "FTIAdvisoryServices",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "NoOfSMSSentToRegisteredFarmers",
                table: "FTIAdvisoryServices",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "NoOfPhoneCalls",
                table: "FTIAdvisoryServices",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "NoOfNewspaperCoverage",
                table: "FTIAdvisoryServices",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "NoOfGroupDiscussions",
                table: "FTIAdvisoryServices",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "NoOfFacebookSMS",
                table: "FTIAdvisoryServices",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "NoOfFaceToFaceDiscussions",
                table: "FTIAdvisoryServices",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "NoOfEmailsSent",
                table: "FTIAdvisoryServices",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "NoOfBeneficiaries",
                table: "FTIAdvisoryServices",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "NoOfAnsweredWhatsappQueries",
                table: "FTIAdvisoryServices",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SubmittedDate",
                table: "ASMVisitorDetails",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddColumn<int>(
                name: "FIUActivitiesId",
                table: "ASMVisitorDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FTITopicsCoveredInClass",
                table: "FTITopicsCoveredInClass",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FTITeachingAidsDeveloped",
                table: "FTITeachingAidsDeveloped",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FTIResourcePersons",
                table: "FTIResourcePersons",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FTIReports",
                table: "FTIReports",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FTIRecommendations",
                table: "FTIRecommendations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FTIProgramDetails",
                table: "FTIProgramDetails",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FTIProgramContentAndResources",
                table: "FTIProgramContentAndResources",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FTIParticipantDemographics",
                table: "FTIParticipantDemographics",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FTIAdvisoryServices",
                table: "FTIAdvisoryServices",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "FtiOtherActivities",
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
                    table.PrimaryKey("PK_FtiOtherActivities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FtiOtherActivities_OrganizationUnitLocations_UnitLocationId",
                        column: x => x.UnitLocationId,
                        principalTable: "OrganizationUnitLocations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FtiOtherActivities_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FtiOtherActivities_Users_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FtiOtherActivities_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FtiOtherActivities_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FtiTrainingPrograms",
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
                    OrganisationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParticipantCount = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    TrainingCount = table.Column<int>(type: "int", nullable: false),
                    TrainingTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FtiTrainingPrograms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FtiTrainingPrograms_OrganizationUnitLocations_UnitLocationId",
                        column: x => x.UnitLocationId,
                        principalTable: "OrganizationUnitLocations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FtiTrainingPrograms_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FtiTrainingPrograms_Users_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FtiTrainingPrograms_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FtiTrainingPrograms_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FTIReports_FTIProgramDetailsID",
                table: "FTIReports",
                column: "FTIProgramDetailsID");

            migrationBuilder.CreateIndex(
                name: "IX_FTIRecommendations_FTIProgramDetailsID",
                table: "FTIRecommendations",
                column: "FTIProgramDetailsID");

            migrationBuilder.CreateIndex(
                name: "IX_FTIAdvisoryServices_FTIProgramDetailsID",
                table: "FTIAdvisoryServices",
                column: "FTIProgramDetailsID");

            migrationBuilder.CreateIndex(
                name: "IX_FIUProgramActivities_CreatedAt",
                table: "FIUProgramActivities",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_FIUProgramActivities_FormStatus",
                table: "FIUProgramActivities",
                column: "FormStatus");

            migrationBuilder.CreateIndex(
                name: "IX_FIUActivities_DisplayOrder",
                table: "FIUActivities",
                column: "DisplayOrder");

            migrationBuilder.CreateIndex(
                name: "IX_FtiOtherActivities_ApprovedById",
                table: "FtiOtherActivities",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_FtiOtherActivities_CreatedById",
                table: "FtiOtherActivities",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FtiOtherActivities_OrganizationId",
                table: "FtiOtherActivities",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_FtiOtherActivities_UnitLocationId",
                table: "FtiOtherActivities",
                column: "UnitLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_FtiOtherActivities_UpdatedById",
                table: "FtiOtherActivities",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FtiTrainingPrograms_ApprovedById",
                table: "FtiTrainingPrograms",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_FtiTrainingPrograms_CreatedById",
                table: "FtiTrainingPrograms",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FtiTrainingPrograms_OrganizationId",
                table: "FtiTrainingPrograms",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_FtiTrainingPrograms_UnitLocationId",
                table: "FtiTrainingPrograms",
                column: "UnitLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_FtiTrainingPrograms_UpdatedById",
                table: "FtiTrainingPrograms",
                column: "UpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_FTIAdvisoryServices_FTIProgramDetails_FTIProgramDetailsID",
                table: "FTIAdvisoryServices",
                column: "FTIProgramDetailsID",
                principalTable: "FTIProgramDetails",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FTIAdvisoryServices_Users_CreatedById",
                table: "FTIAdvisoryServices",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FTIAdvisoryServices_Users_UpdatedById",
                table: "FTIAdvisoryServices",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FTIParticipantDemographics_FTIProgramDetails_FTIProgramDetailsID",
                table: "FTIParticipantDemographics",
                column: "FTIProgramDetailsID",
                principalTable: "FTIProgramDetails",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FTIParticipantDemographics_Users_CreatedById",
                table: "FTIParticipantDemographics",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FTIParticipantDemographics_Users_UpdatedById",
                table: "FTIParticipantDemographics",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FTIProgramContentAndResources_FTIProgramDetails_FTIProgramDetailsID",
                table: "FTIProgramContentAndResources",
                column: "FTIProgramDetailsID",
                principalTable: "FTIProgramDetails",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FTIProgramContentAndResources_Users_CreatedById",
                table: "FTIProgramContentAndResources",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FTIProgramContentAndResources_Users_UpdatedById",
                table: "FTIProgramContentAndResources",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FTIProgramDetails_Collaborators_CollaboratorId",
                table: "FTIProgramDetails",
                column: "CollaboratorId",
                principalTable: "Collaborators",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FTIProgramDetails_InfoTypes_TypeId",
                table: "FTIProgramDetails",
                column: "TypeId",
                principalTable: "InfoTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FTIProgramDetails_ProgramCategories_CategoryId",
                table: "FTIProgramDetails",
                column: "CategoryId",
                principalTable: "ProgramCategories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FTIProgramDetails_ThematicAreas_ThematicAreaId",
                table: "FTIProgramDetails",
                column: "ThematicAreaId",
                principalTable: "ThematicAreas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FTIProgramDetails_Users_CreatedById",
                table: "FTIProgramDetails",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FTIProgramDetails_Users_UpdatedById",
                table: "FTIProgramDetails",
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
                name: "FK_FTIReports_FTIProgramDetails_FTIProgramDetailsID",
                table: "FTIReports",
                column: "FTIProgramDetailsID",
                principalTable: "FTIProgramDetails",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FTIReports_Users_CreatedById",
                table: "FTIReports",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FTIReports_Users_UpdatedById",
                table: "FTIReports",
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
                name: "FK_FTITeachingAidsDeveloped_FTIProgramContentAndResources_FTIProgramContentAndResourcesID",
                table: "FTITeachingAidsDeveloped",
                column: "FTIProgramContentAndResourcesID",
                principalTable: "FTIProgramContentAndResources",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FTITeachingAidsDeveloped_Users_CreatedById",
                table: "FTITeachingAidsDeveloped",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FTITeachingAidsDeveloped_Users_UpdatedById",
                table: "FTITeachingAidsDeveloped",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FTITopicsCoveredInClass_FTIProgramContentAndResources_FTIProgramContentAndResourcesID",
                table: "FTITopicsCoveredInClass",
                column: "FTIProgramContentAndResourcesID",
                principalTable: "FTIProgramContentAndResources",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FTITopicsCoveredInClass_Users_CreatedById",
                table: "FTITopicsCoveredInClass",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FTITopicsCoveredInClass_Users_UpdatedById",
                table: "FTITopicsCoveredInClass",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}

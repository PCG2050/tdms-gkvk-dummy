using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class IbtvaChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_IbtvaReports_IbtvaProgramDetailsId",
                table: "IbtvaReports");

            migrationBuilder.DropIndex(
                name: "IX_IbtvaRecommendations_IbtvaProgramDetailsId",
                table: "IbtvaRecommendations");

            migrationBuilder.DropIndex(
                name: "IX_IbtvaAdvisoryServices_IbtvaProgramDetailsId",
                table: "IbtvaAdvisoryServices");

            migrationBuilder.DropColumn(
                name: "CurrentPhase",
                table: "IbtvaProgramDetails");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "IbtvaProgramContentAndResources");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "IbtvaProgramContentAndResources");

            migrationBuilder.RenameColumn(
                name: "Mode",
                table: "IbtvaProgramDetails",
                newName: "ModeId");

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "IbtvaTopicsCoveredInClass",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnitLocationId",
                table: "IbtvaTopicsCoveredInClass",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "IbtvaTeachingAidsDeveloped",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnitLocationId",
                table: "IbtvaTeachingAidsDeveloped",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "IbtvaResourcePersons",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnitLocationId",
                table: "IbtvaResourcePersons",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "IbtvaReports",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnitLocationId",
                table: "IbtvaReports",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "IbtvaRecommendations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnitLocationId",
                table: "IbtvaRecommendations",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "StartDate",
                table: "IbtvaProgramDetails",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "EndDate",
                table: "IbtvaProgramDetails",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ApprovedAt",
                table: "IbtvaProgramDetails",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApprovedById",
                table: "IbtvaProgramDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FormStatusRemarks",
                table: "IbtvaProgramDetails",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "IbtvaProgramContentAndResources",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnitLocationId",
                table: "IbtvaProgramContentAndResources",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "IbtvaParticipantDemographics",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnitLocationId",
                table: "IbtvaParticipantDemographics",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "IbtvaAdvisoryServices",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnitLocationId",
                table: "IbtvaAdvisoryServices",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ApprovedAt",
                table: "FtiTrainingPrograms",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApprovedById",
                table: "FtiTrainingPrograms",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FormStatus",
                table: "FtiTrainingPrograms",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FormStatusRemarks",
                table: "FtiTrainingPrograms",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ApprovedAt",
                table: "FtiOtherActivities",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApprovedById",
                table: "FtiOtherActivities",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FormStatus",
                table: "FtiOtherActivities",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FormStatusRemarks",
                table: "FtiOtherActivities",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ApprovedAt",
                table: "FiuProgrammes",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApprovedById",
                table: "FiuProgrammes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FormStatus",
                table: "FiuProgrammes",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FormStatusRemarks",
                table: "FiuProgrammes",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ApprovedAt",
                table: "EeuTrainingProgrammes",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApprovedById",
                table: "EeuTrainingProgrammes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FormStatus",
                table: "EeuTrainingProgrammes",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FormStatusRemarks",
                table: "EeuTrainingProgrammes",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ApprovedAt",
                table: "EeuProgramDetails",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApprovedById",
                table: "EeuProgramDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FormStatus",
                table: "EeuProgramDetails",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FormStatusRemarks",
                table: "EeuProgramDetails",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ApprovedAt",
                table: "EeuOtherActivities",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApprovedById",
                table: "EeuOtherActivities",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FormStatus",
                table: "EeuOtherActivities",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FormStatusRemarks",
                table: "EeuOtherActivities",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ApprovedAt",
                table: "EeuOFTs",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApprovedById",
                table: "EeuOFTs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FormStatus",
                table: "EeuOFTs",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FormStatusRemarks",
                table: "EeuOFTs",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ApprovedAt",
                table: "EeuFLDs",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApprovedById",
                table: "EeuFLDs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FormStatus",
                table: "EeuFLDs",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FormStatusRemarks",
                table: "EeuFLDs",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ApprovedAt",
                table: "DeuProgramDetails",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApprovedById",
                table: "DeuProgramDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FormStatus",
                table: "DeuProgramDetails",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FormStatusRemarks",
                table: "DeuProgramDetails",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ApprovedAt",
                table: "DeuOtherActivities",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApprovedById",
                table: "DeuOtherActivities",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FormStatus",
                table: "DeuOtherActivities",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FormStatusRemarks",
                table: "DeuOtherActivities",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ApprovedAt",
                table: "DeuCourses",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApprovedById",
                table: "DeuCourses",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FormStatus",
                table: "DeuCourses",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FormStatusRemarks",
                table: "DeuCourses",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ApprovedAt",
                table: "DaesiProgrammes",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApprovedById",
                table: "DaesiProgrammes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FormStatus",
                table: "DaesiProgrammes",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FormStatusRemarks",
                table: "DaesiProgrammes",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ApprovedAt",
                table: "DaesiOtherActivities",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApprovedById",
                table: "DaesiOtherActivities",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FormStatus",
                table: "DaesiOtherActivities",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FormStatusRemarks",
                table: "DaesiOtherActivities",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ApprovedAt",
                table: "AticSales",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApprovedById",
                table: "AticSales",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FormStatus",
                table: "AticSales",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FormStatusRemarks",
                table: "AticSales",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ApprovedAt",
                table: "AticOtherActivities",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApprovedById",
                table: "AticOtherActivities",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FormStatus",
                table: "AticOtherActivities",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FormStatusRemarks",
                table: "AticOtherActivities",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_IbtvaReports_IbtvaProgramDetailsId",
                table: "IbtvaReports",
                column: "IbtvaProgramDetailsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IbtvaRecommendations_IbtvaProgramDetailsId",
                table: "IbtvaRecommendations",
                column: "IbtvaProgramDetailsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IbtvaProgramDetails_ApprovedById",
                table: "IbtvaProgramDetails",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_IbtvaProgramDetails_ModeId",
                table: "IbtvaProgramDetails",
                column: "ModeId");

            migrationBuilder.CreateIndex(
                name: "IX_IbtvaAdvisoryServices_IbtvaProgramDetailsId",
                table: "IbtvaAdvisoryServices",
                column: "IbtvaProgramDetailsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FtiTrainingPrograms_ApprovedById",
                table: "FtiTrainingPrograms",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_FtiOtherActivities_ApprovedById",
                table: "FtiOtherActivities",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_FiuProgrammes_ApprovedById",
                table: "FiuProgrammes",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_EeuTrainingProgrammes_ApprovedById",
                table: "EeuTrainingProgrammes",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_EeuProgramDetails_ApprovedById",
                table: "EeuProgramDetails",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_EeuOtherActivities_ApprovedById",
                table: "EeuOtherActivities",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_EeuOFTs_ApprovedById",
                table: "EeuOFTs",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_EeuFLDs_ApprovedById",
                table: "EeuFLDs",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_DeuProgramDetails_ApprovedById",
                table: "DeuProgramDetails",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_DeuOtherActivities_ApprovedById",
                table: "DeuOtherActivities",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_DeuCourses_ApprovedById",
                table: "DeuCourses",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_DaesiProgrammes_ApprovedById",
                table: "DaesiProgrammes",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_DaesiOtherActivities_ApprovedById",
                table: "DaesiOtherActivities",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_AticSales_ApprovedById",
                table: "AticSales",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_AticOtherActivities_ApprovedById",
                table: "AticOtherActivities",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_AticAdvisoryServices_ApprovedById",
                table: "AticAdvisoryServices",
                column: "ApprovedById");

            migrationBuilder.AddForeignKey(
                name: "FK_AticAdvisoryServices_Users_ApprovedById",
                table: "AticAdvisoryServices",
                column: "ApprovedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AticOtherActivities_Users_ApprovedById",
                table: "AticOtherActivities",
                column: "ApprovedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AticSales_Users_ApprovedById",
                table: "AticSales",
                column: "ApprovedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DaesiOtherActivities_Users_ApprovedById",
                table: "DaesiOtherActivities",
                column: "ApprovedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DaesiProgrammes_Users_ApprovedById",
                table: "DaesiProgrammes",
                column: "ApprovedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DeuCourses_Users_ApprovedById",
                table: "DeuCourses",
                column: "ApprovedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DeuOtherActivities_Users_ApprovedById",
                table: "DeuOtherActivities",
                column: "ApprovedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DeuProgramDetails_Users_ApprovedById",
                table: "DeuProgramDetails",
                column: "ApprovedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EeuFLDs_Users_ApprovedById",
                table: "EeuFLDs",
                column: "ApprovedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EeuOFTs_Users_ApprovedById",
                table: "EeuOFTs",
                column: "ApprovedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EeuOtherActivities_Users_ApprovedById",
                table: "EeuOtherActivities",
                column: "ApprovedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EeuProgramDetails_Users_ApprovedById",
                table: "EeuProgramDetails",
                column: "ApprovedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EeuTrainingProgrammes_Users_ApprovedById",
                table: "EeuTrainingProgrammes",
                column: "ApprovedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FiuProgrammes_Users_ApprovedById",
                table: "FiuProgrammes",
                column: "ApprovedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FtiOtherActivities_Users_ApprovedById",
                table: "FtiOtherActivities",
                column: "ApprovedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FtiTrainingPrograms_Users_ApprovedById",
                table: "FtiTrainingPrograms",
                column: "ApprovedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IbtvaProgramDetails_Modes_ModeId",
                table: "IbtvaProgramDetails",
                column: "ModeId",
                principalTable: "Modes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IbtvaProgramDetails_Users_ApprovedById",
                table: "IbtvaProgramDetails",
                column: "ApprovedById",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AticAdvisoryServices_Users_ApprovedById",
                table: "AticAdvisoryServices");

            migrationBuilder.DropForeignKey(
                name: "FK_AticOtherActivities_Users_ApprovedById",
                table: "AticOtherActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_AticSales_Users_ApprovedById",
                table: "AticSales");

            migrationBuilder.DropForeignKey(
                name: "FK_DaesiOtherActivities_Users_ApprovedById",
                table: "DaesiOtherActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_DaesiProgrammes_Users_ApprovedById",
                table: "DaesiProgrammes");

            migrationBuilder.DropForeignKey(
                name: "FK_DeuCourses_Users_ApprovedById",
                table: "DeuCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_DeuOtherActivities_Users_ApprovedById",
                table: "DeuOtherActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_DeuProgramDetails_Users_ApprovedById",
                table: "DeuProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_EeuFLDs_Users_ApprovedById",
                table: "EeuFLDs");

            migrationBuilder.DropForeignKey(
                name: "FK_EeuOFTs_Users_ApprovedById",
                table: "EeuOFTs");

            migrationBuilder.DropForeignKey(
                name: "FK_EeuOtherActivities_Users_ApprovedById",
                table: "EeuOtherActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_EeuProgramDetails_Users_ApprovedById",
                table: "EeuProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_EeuTrainingProgrammes_Users_ApprovedById",
                table: "EeuTrainingProgrammes");

            migrationBuilder.DropForeignKey(
                name: "FK_FiuProgrammes_Users_ApprovedById",
                table: "FiuProgrammes");

            migrationBuilder.DropForeignKey(
                name: "FK_FtiOtherActivities_Users_ApprovedById",
                table: "FtiOtherActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_FtiTrainingPrograms_Users_ApprovedById",
                table: "FtiTrainingPrograms");

            migrationBuilder.DropForeignKey(
                name: "FK_IbtvaProgramDetails_Modes_ModeId",
                table: "IbtvaProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_IbtvaProgramDetails_Users_ApprovedById",
                table: "IbtvaProgramDetails");

            migrationBuilder.DropIndex(
                name: "IX_IbtvaReports_IbtvaProgramDetailsId",
                table: "IbtvaReports");

            migrationBuilder.DropIndex(
                name: "IX_IbtvaRecommendations_IbtvaProgramDetailsId",
                table: "IbtvaRecommendations");

            migrationBuilder.DropIndex(
                name: "IX_IbtvaProgramDetails_ApprovedById",
                table: "IbtvaProgramDetails");

            migrationBuilder.DropIndex(
                name: "IX_IbtvaProgramDetails_ModeId",
                table: "IbtvaProgramDetails");

            migrationBuilder.DropIndex(
                name: "IX_IbtvaAdvisoryServices_IbtvaProgramDetailsId",
                table: "IbtvaAdvisoryServices");

            migrationBuilder.DropIndex(
                name: "IX_FtiTrainingPrograms_ApprovedById",
                table: "FtiTrainingPrograms");

            migrationBuilder.DropIndex(
                name: "IX_FtiOtherActivities_ApprovedById",
                table: "FtiOtherActivities");

            migrationBuilder.DropIndex(
                name: "IX_FiuProgrammes_ApprovedById",
                table: "FiuProgrammes");

            migrationBuilder.DropIndex(
                name: "IX_EeuTrainingProgrammes_ApprovedById",
                table: "EeuTrainingProgrammes");

            migrationBuilder.DropIndex(
                name: "IX_EeuProgramDetails_ApprovedById",
                table: "EeuProgramDetails");

            migrationBuilder.DropIndex(
                name: "IX_EeuOtherActivities_ApprovedById",
                table: "EeuOtherActivities");

            migrationBuilder.DropIndex(
                name: "IX_EeuOFTs_ApprovedById",
                table: "EeuOFTs");

            migrationBuilder.DropIndex(
                name: "IX_EeuFLDs_ApprovedById",
                table: "EeuFLDs");

            migrationBuilder.DropIndex(
                name: "IX_DeuProgramDetails_ApprovedById",
                table: "DeuProgramDetails");

            migrationBuilder.DropIndex(
                name: "IX_DeuOtherActivities_ApprovedById",
                table: "DeuOtherActivities");

            migrationBuilder.DropIndex(
                name: "IX_DeuCourses_ApprovedById",
                table: "DeuCourses");

            migrationBuilder.DropIndex(
                name: "IX_DaesiProgrammes_ApprovedById",
                table: "DaesiProgrammes");

            migrationBuilder.DropIndex(
                name: "IX_DaesiOtherActivities_ApprovedById",
                table: "DaesiOtherActivities");

            migrationBuilder.DropIndex(
                name: "IX_AticSales_ApprovedById",
                table: "AticSales");

            migrationBuilder.DropIndex(
                name: "IX_AticOtherActivities_ApprovedById",
                table: "AticOtherActivities");

            migrationBuilder.DropIndex(
                name: "IX_AticAdvisoryServices_ApprovedById",
                table: "AticAdvisoryServices");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "IbtvaTopicsCoveredInClass");

            migrationBuilder.DropColumn(
                name: "UnitLocationId",
                table: "IbtvaTopicsCoveredInClass");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "IbtvaTeachingAidsDeveloped");

            migrationBuilder.DropColumn(
                name: "UnitLocationId",
                table: "IbtvaTeachingAidsDeveloped");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "IbtvaResourcePersons");

            migrationBuilder.DropColumn(
                name: "UnitLocationId",
                table: "IbtvaResourcePersons");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "IbtvaReports");

            migrationBuilder.DropColumn(
                name: "UnitLocationId",
                table: "IbtvaReports");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "IbtvaRecommendations");

            migrationBuilder.DropColumn(
                name: "UnitLocationId",
                table: "IbtvaRecommendations");

            migrationBuilder.DropColumn(
                name: "ApprovedAt",
                table: "IbtvaProgramDetails");

            migrationBuilder.DropColumn(
                name: "ApprovedById",
                table: "IbtvaProgramDetails");

            migrationBuilder.DropColumn(
                name: "FormStatusRemarks",
                table: "IbtvaProgramDetails");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "IbtvaProgramContentAndResources");

            migrationBuilder.DropColumn(
                name: "UnitLocationId",
                table: "IbtvaProgramContentAndResources");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "IbtvaParticipantDemographics");

            migrationBuilder.DropColumn(
                name: "UnitLocationId",
                table: "IbtvaParticipantDemographics");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "IbtvaAdvisoryServices");

            migrationBuilder.DropColumn(
                name: "UnitLocationId",
                table: "IbtvaAdvisoryServices");

            migrationBuilder.DropColumn(
                name: "ApprovedAt",
                table: "FtiTrainingPrograms");

            migrationBuilder.DropColumn(
                name: "ApprovedById",
                table: "FtiTrainingPrograms");

            migrationBuilder.DropColumn(
                name: "FormStatus",
                table: "FtiTrainingPrograms");

            migrationBuilder.DropColumn(
                name: "FormStatusRemarks",
                table: "FtiTrainingPrograms");

            migrationBuilder.DropColumn(
                name: "ApprovedAt",
                table: "FtiOtherActivities");

            migrationBuilder.DropColumn(
                name: "ApprovedById",
                table: "FtiOtherActivities");

            migrationBuilder.DropColumn(
                name: "FormStatus",
                table: "FtiOtherActivities");

            migrationBuilder.DropColumn(
                name: "FormStatusRemarks",
                table: "FtiOtherActivities");

            migrationBuilder.DropColumn(
                name: "ApprovedAt",
                table: "FiuProgrammes");

            migrationBuilder.DropColumn(
                name: "ApprovedById",
                table: "FiuProgrammes");

            migrationBuilder.DropColumn(
                name: "FormStatus",
                table: "FiuProgrammes");

            migrationBuilder.DropColumn(
                name: "FormStatusRemarks",
                table: "FiuProgrammes");

            migrationBuilder.DropColumn(
                name: "ApprovedAt",
                table: "EeuTrainingProgrammes");

            migrationBuilder.DropColumn(
                name: "ApprovedById",
                table: "EeuTrainingProgrammes");

            migrationBuilder.DropColumn(
                name: "FormStatus",
                table: "EeuTrainingProgrammes");

            migrationBuilder.DropColumn(
                name: "FormStatusRemarks",
                table: "EeuTrainingProgrammes");

            migrationBuilder.DropColumn(
                name: "ApprovedAt",
                table: "EeuProgramDetails");

            migrationBuilder.DropColumn(
                name: "ApprovedById",
                table: "EeuProgramDetails");

            migrationBuilder.DropColumn(
                name: "FormStatus",
                table: "EeuProgramDetails");

            migrationBuilder.DropColumn(
                name: "FormStatusRemarks",
                table: "EeuProgramDetails");

            migrationBuilder.DropColumn(
                name: "ApprovedAt",
                table: "EeuOtherActivities");

            migrationBuilder.DropColumn(
                name: "ApprovedById",
                table: "EeuOtherActivities");

            migrationBuilder.DropColumn(
                name: "FormStatus",
                table: "EeuOtherActivities");

            migrationBuilder.DropColumn(
                name: "FormStatusRemarks",
                table: "EeuOtherActivities");

            migrationBuilder.DropColumn(
                name: "ApprovedAt",
                table: "EeuOFTs");

            migrationBuilder.DropColumn(
                name: "ApprovedById",
                table: "EeuOFTs");

            migrationBuilder.DropColumn(
                name: "FormStatus",
                table: "EeuOFTs");

            migrationBuilder.DropColumn(
                name: "FormStatusRemarks",
                table: "EeuOFTs");

            migrationBuilder.DropColumn(
                name: "ApprovedAt",
                table: "EeuFLDs");

            migrationBuilder.DropColumn(
                name: "ApprovedById",
                table: "EeuFLDs");

            migrationBuilder.DropColumn(
                name: "FormStatus",
                table: "EeuFLDs");

            migrationBuilder.DropColumn(
                name: "FormStatusRemarks",
                table: "EeuFLDs");

            migrationBuilder.DropColumn(
                name: "ApprovedAt",
                table: "DeuProgramDetails");

            migrationBuilder.DropColumn(
                name: "ApprovedById",
                table: "DeuProgramDetails");

            migrationBuilder.DropColumn(
                name: "FormStatus",
                table: "DeuProgramDetails");

            migrationBuilder.DropColumn(
                name: "FormStatusRemarks",
                table: "DeuProgramDetails");

            migrationBuilder.DropColumn(
                name: "ApprovedAt",
                table: "DeuOtherActivities");

            migrationBuilder.DropColumn(
                name: "ApprovedById",
                table: "DeuOtherActivities");

            migrationBuilder.DropColumn(
                name: "FormStatus",
                table: "DeuOtherActivities");

            migrationBuilder.DropColumn(
                name: "FormStatusRemarks",
                table: "DeuOtherActivities");

            migrationBuilder.DropColumn(
                name: "ApprovedAt",
                table: "DeuCourses");

            migrationBuilder.DropColumn(
                name: "ApprovedById",
                table: "DeuCourses");

            migrationBuilder.DropColumn(
                name: "FormStatus",
                table: "DeuCourses");

            migrationBuilder.DropColumn(
                name: "FormStatusRemarks",
                table: "DeuCourses");

            migrationBuilder.DropColumn(
                name: "ApprovedAt",
                table: "DaesiProgrammes");

            migrationBuilder.DropColumn(
                name: "ApprovedById",
                table: "DaesiProgrammes");

            migrationBuilder.DropColumn(
                name: "FormStatus",
                table: "DaesiProgrammes");

            migrationBuilder.DropColumn(
                name: "FormStatusRemarks",
                table: "DaesiProgrammes");

            migrationBuilder.DropColumn(
                name: "ApprovedAt",
                table: "DaesiOtherActivities");

            migrationBuilder.DropColumn(
                name: "ApprovedById",
                table: "DaesiOtherActivities");

            migrationBuilder.DropColumn(
                name: "FormStatus",
                table: "DaesiOtherActivities");

            migrationBuilder.DropColumn(
                name: "FormStatusRemarks",
                table: "DaesiOtherActivities");

            migrationBuilder.DropColumn(
                name: "ApprovedAt",
                table: "AticSales");

            migrationBuilder.DropColumn(
                name: "ApprovedById",
                table: "AticSales");

            migrationBuilder.DropColumn(
                name: "FormStatus",
                table: "AticSales");

            migrationBuilder.DropColumn(
                name: "FormStatusRemarks",
                table: "AticSales");

            migrationBuilder.DropColumn(
                name: "ApprovedAt",
                table: "AticOtherActivities");

            migrationBuilder.DropColumn(
                name: "ApprovedById",
                table: "AticOtherActivities");

            migrationBuilder.DropColumn(
                name: "FormStatus",
                table: "AticOtherActivities");

            migrationBuilder.DropColumn(
                name: "FormStatusRemarks",
                table: "AticOtherActivities");

            migrationBuilder.DropColumn(
                name: "ApprovedAt",
                table: "AticAdvisoryServices");

            migrationBuilder.DropColumn(
                name: "ApprovedById",
                table: "AticAdvisoryServices");

            migrationBuilder.DropColumn(
                name: "FormStatus",
                table: "AticAdvisoryServices");

            migrationBuilder.DropColumn(
                name: "FormStatusRemarks",
                table: "AticAdvisoryServices");

            migrationBuilder.RenameColumn(
                name: "ModeId",
                table: "IbtvaProgramDetails",
                newName: "Mode");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "IbtvaProgramDetails",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "IbtvaProgramDetails",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddColumn<int>(
                name: "CurrentPhase",
                table: "IbtvaProgramDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "IbtvaProgramContentAndResources",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "IbtvaProgramContentAndResources",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_IbtvaReports_IbtvaProgramDetailsId",
                table: "IbtvaReports",
                column: "IbtvaProgramDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_IbtvaRecommendations_IbtvaProgramDetailsId",
                table: "IbtvaRecommendations",
                column: "IbtvaProgramDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_IbtvaAdvisoryServices_IbtvaProgramDetailsId",
                table: "IbtvaAdvisoryServices",
                column: "IbtvaProgramDetailsId");
        }
    }
}

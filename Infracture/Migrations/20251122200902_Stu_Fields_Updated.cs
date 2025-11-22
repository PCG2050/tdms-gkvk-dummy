using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Stu_Fields_Updated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "StuReports");

            migrationBuilder.DropColumn(
                name: "SignificantOutcome",
                table: "StuReports");

            migrationBuilder.RenameColumn(
                name: "UploadVideo",
                table: "StuReports",
                newName: "TestingCompletionLetter");

            migrationBuilder.RenameColumn(
                name: "UploadPhoto",
                table: "StuReports",
                newName: "SpclReport");

            migrationBuilder.RenameColumn(
                name: "ProgressReportReportingYear",
                table: "StuReports",
                newName: "TypeOfReport");

            migrationBuilder.RenameColumn(
                name: "PhotosGeotaggedPhotoOrUploadPhoto",
                table: "StuReports",
                newName: "ReportingVideo");

            migrationBuilder.AddColumn<string>(
                name: "GeoTaggedPhoto",
                table: "StuReports",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Outcome",
                table: "StuReports",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProgressReport",
                table: "StuReports",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "ProjectCompletionDate",
                table: "StuReports",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProjectCompletionLetter",
                table: "StuReports",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "ReportDate",
                table: "StuReports",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReportingYear",
                table: "StuReports",
                type: "nvarchar(4)",
                maxLength: 4,
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "TestingCompletionDate",
                table: "StuReports",
                type: "date",
                nullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "UniversitySanctionLetterDate",
                table: "StuProgramDetails",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "ProposalDate",
                table: "StuProgramDetails",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "FundsSanctionLetterDate",
                table: "StuProgramDetails",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Area",
                table: "StuProgramDetails",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FundAmount",
                table: "StuProgramDetails",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "FundReleaseDate",
                table: "StuProgramDetails",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FundReleaseFile",
                table: "StuProgramDetails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FundReleaseYear",
                table: "StuProgramDetails",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherSourceOfInformation",
                table: "StuProgramDetails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PiAddress",
                table: "StuProgramDetails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "ProjectSanctionDate",
                table: "StuProgramDetails",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProjectSanctionFile",
                table: "StuProgramDetails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReportingVideo",
                table: "StuProgramDetails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SourceOfTitle",
                table: "StuProgramDetails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "UniImplDate",
                table: "StuProgramDetails",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UniImplLetterFile",
                table: "StuProgramDetails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NoOfWhatsappSMS",
                table: "StuAdvisoryServices",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NoOfWhatsappGroups",
                table: "StuAdvisoryServices",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NoOfSMSSentToRegisteredFarmers",
                table: "StuAdvisoryServices",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NoOfPhoneCalls",
                table: "StuAdvisoryServices",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NoOfNewspaperCoverage",
                table: "StuAdvisoryServices",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NoOfGroupDiscussions",
                table: "StuAdvisoryServices",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NoOfFacebookSMS",
                table: "StuAdvisoryServices",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NoOfFaceToFaceDiscussions",
                table: "StuAdvisoryServices",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NoOfEmailsSent",
                table: "StuAdvisoryServices",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NoOfBeneficiaries",
                table: "StuAdvisoryServices",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NoOfAnsweredWhatsappQueries",
                table: "StuAdvisoryServices",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GeoTaggedPhoto",
                table: "StuReports");

            migrationBuilder.DropColumn(
                name: "Outcome",
                table: "StuReports");

            migrationBuilder.DropColumn(
                name: "ProgressReport",
                table: "StuReports");

            migrationBuilder.DropColumn(
                name: "ProjectCompletionDate",
                table: "StuReports");

            migrationBuilder.DropColumn(
                name: "ProjectCompletionLetter",
                table: "StuReports");

            migrationBuilder.DropColumn(
                name: "ReportDate",
                table: "StuReports");

            migrationBuilder.DropColumn(
                name: "ReportingYear",
                table: "StuReports");

            migrationBuilder.DropColumn(
                name: "TestingCompletionDate",
                table: "StuReports");

            migrationBuilder.DropColumn(
                name: "Area",
                table: "StuProgramDetails");

            migrationBuilder.DropColumn(
                name: "FundAmount",
                table: "StuProgramDetails");

            migrationBuilder.DropColumn(
                name: "FundReleaseDate",
                table: "StuProgramDetails");

            migrationBuilder.DropColumn(
                name: "FundReleaseFile",
                table: "StuProgramDetails");

            migrationBuilder.DropColumn(
                name: "FundReleaseYear",
                table: "StuProgramDetails");

            migrationBuilder.DropColumn(
                name: "OtherSourceOfInformation",
                table: "StuProgramDetails");

            migrationBuilder.DropColumn(
                name: "PiAddress",
                table: "StuProgramDetails");

            migrationBuilder.DropColumn(
                name: "ProjectSanctionDate",
                table: "StuProgramDetails");

            migrationBuilder.DropColumn(
                name: "ProjectSanctionFile",
                table: "StuProgramDetails");

            migrationBuilder.DropColumn(
                name: "ReportingVideo",
                table: "StuProgramDetails");

            migrationBuilder.DropColumn(
                name: "SourceOfTitle",
                table: "StuProgramDetails");

            migrationBuilder.DropColumn(
                name: "UniImplDate",
                table: "StuProgramDetails");

            migrationBuilder.DropColumn(
                name: "UniImplLetterFile",
                table: "StuProgramDetails");

            migrationBuilder.RenameColumn(
                name: "TypeOfReport",
                table: "StuReports",
                newName: "ProgressReportReportingYear");

            migrationBuilder.RenameColumn(
                name: "TestingCompletionLetter",
                table: "StuReports",
                newName: "UploadVideo");

            migrationBuilder.RenameColumn(
                name: "SpclReport",
                table: "StuReports",
                newName: "UploadPhoto");

            migrationBuilder.RenameColumn(
                name: "ReportingVideo",
                table: "StuReports",
                newName: "PhotosGeotaggedPhotoOrUploadPhoto");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "StuReports",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SignificantOutcome",
                table: "StuReports",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UniversitySanctionLetterDate",
                table: "StuProgramDetails",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ProposalDate",
                table: "StuProgramDetails",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FundsSanctionLetterDate",
                table: "StuProgramDetails",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NoOfWhatsappSMS",
                table: "StuAdvisoryServices",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "NoOfWhatsappGroups",
                table: "StuAdvisoryServices",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "NoOfSMSSentToRegisteredFarmers",
                table: "StuAdvisoryServices",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "NoOfPhoneCalls",
                table: "StuAdvisoryServices",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "NoOfNewspaperCoverage",
                table: "StuAdvisoryServices",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "NoOfGroupDiscussions",
                table: "StuAdvisoryServices",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "NoOfFacebookSMS",
                table: "StuAdvisoryServices",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "NoOfFaceToFaceDiscussions",
                table: "StuAdvisoryServices",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "NoOfEmailsSent",
                table: "StuAdvisoryServices",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "NoOfBeneficiaries",
                table: "StuAdvisoryServices",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "NoOfAnsweredWhatsappQueries",
                table: "StuAdvisoryServices",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}

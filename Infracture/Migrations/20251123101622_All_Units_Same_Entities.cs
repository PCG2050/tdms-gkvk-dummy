using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class All_Units_Same_Entities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "NaepReports");

            migrationBuilder.DropColumn(
                name: "SignificantOutcome",
                table: "NaepReports");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "IbtvaReports");

            migrationBuilder.DropColumn(
                name: "SignificantOutcome",
                table: "IbtvaReports");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "FtiReports");

            migrationBuilder.DropColumn(
                name: "SignificantOutcome",
                table: "FtiReports");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "EeuReports");

            migrationBuilder.DropColumn(
                name: "SignificantOutcome",
                table: "EeuReports");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "DeuReports");

            migrationBuilder.DropColumn(
                name: "SignificantOutcome",
                table: "DeuReports");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "AticReports");

            migrationBuilder.DropColumn(
                name: "SignificantOutcome",
                table: "AticReports");

            migrationBuilder.RenameColumn(
                name: "UploadVideo",
                table: "NaepReports",
                newName: "TestingCompletionLetter");

            migrationBuilder.RenameColumn(
                name: "UploadPhoto",
                table: "NaepReports",
                newName: "SpclReport");

            migrationBuilder.RenameColumn(
                name: "ProgressReportReportingYear",
                table: "NaepReports",
                newName: "TypeOfReport");

            migrationBuilder.RenameColumn(
                name: "PhotosGeotaggedPhotoOrUploadPhoto",
                table: "NaepReports",
                newName: "ReportingVideo");

            migrationBuilder.RenameColumn(
                name: "UploadVideo",
                table: "IbtvaReports",
                newName: "TestingCompletionLetter");

            migrationBuilder.RenameColumn(
                name: "UploadPhoto",
                table: "IbtvaReports",
                newName: "SpclReport");

            migrationBuilder.RenameColumn(
                name: "ProgressReportReportingYear",
                table: "IbtvaReports",
                newName: "TypeOfReport");

            migrationBuilder.RenameColumn(
                name: "PhotosGeotaggedPhotoOrUploadPhoto",
                table: "IbtvaReports",
                newName: "ReportingVideo");

            migrationBuilder.RenameColumn(
                name: "UploadVideo",
                table: "FtiReports",
                newName: "TestingCompletionLetter");

            migrationBuilder.RenameColumn(
                name: "UploadPhoto",
                table: "FtiReports",
                newName: "SpclReport");

            migrationBuilder.RenameColumn(
                name: "ProgressReportReportingYear",
                table: "FtiReports",
                newName: "TypeOfReport");

            migrationBuilder.RenameColumn(
                name: "PhotosGeotaggedPhotoOrUploadPhoto",
                table: "FtiReports",
                newName: "ReportingVideo");

            migrationBuilder.RenameColumn(
                name: "UploadVideo",
                table: "EeuReports",
                newName: "TestingCompletionLetter");

            migrationBuilder.RenameColumn(
                name: "UploadPhoto",
                table: "EeuReports",
                newName: "SpclReport");

            migrationBuilder.RenameColumn(
                name: "ProgressReportReportingYear",
                table: "EeuReports",
                newName: "TypeOfReport");

            migrationBuilder.RenameColumn(
                name: "PhotosGeotaggedPhotoOrUploadPhoto",
                table: "EeuReports",
                newName: "ReportingVideo");

            migrationBuilder.RenameColumn(
                name: "UploadVideo",
                table: "DeuReports",
                newName: "TestingCompletionLetter");

            migrationBuilder.RenameColumn(
                name: "UploadPhoto",
                table: "DeuReports",
                newName: "SpclReport");

            migrationBuilder.RenameColumn(
                name: "ProgressReportReportingYear",
                table: "DeuReports",
                newName: "TypeOfReport");

            migrationBuilder.RenameColumn(
                name: "PhotosGeotaggedPhotoOrUploadPhoto",
                table: "DeuReports",
                newName: "ReportingVideo");

            migrationBuilder.RenameColumn(
                name: "UploadVideo",
                table: "AticReports",
                newName: "TestingCompletionLetter");

            migrationBuilder.RenameColumn(
                name: "UploadPhoto",
                table: "AticReports",
                newName: "SpclReport");

            migrationBuilder.RenameColumn(
                name: "ProgressReportReportingYear",
                table: "AticReports",
                newName: "TypeOfReport");

            migrationBuilder.RenameColumn(
                name: "PhotosGeotaggedPhotoOrUploadPhoto",
                table: "AticReports",
                newName: "ReportingVideo");

            migrationBuilder.AddColumn<string>(
                name: "GeoTaggedPhoto",
                table: "NaepReports",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Outcome",
                table: "NaepReports",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProgressReport",
                table: "NaepReports",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "ProjectCompletionDate",
                table: "NaepReports",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProjectCompletionLetter",
                table: "NaepReports",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "ReportDate",
                table: "NaepReports",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReportingYear",
                table: "NaepReports",
                type: "nvarchar(4)",
                maxLength: 4,
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "TestingCompletionDate",
                table: "NaepReports",
                type: "date",
                nullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "UniversitySanctionLetterDate",
                table: "NaepProgramDetails",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "NaepProgramDetails",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "ProposalDate",
                table: "NaepProgramDetails",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "FundsSanctionLetterDate",
                table: "NaepProgramDetails",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Area",
                table: "NaepProgramDetails",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FundAmount",
                table: "NaepProgramDetails",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "FundReleaseDate",
                table: "NaepProgramDetails",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FundReleaseFile",
                table: "NaepProgramDetails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FundReleaseYear",
                table: "NaepProgramDetails",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherSourceOfInformation",
                table: "NaepProgramDetails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PiAddress",
                table: "NaepProgramDetails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "ProjectSanctionDate",
                table: "NaepProgramDetails",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProjectSanctionFile",
                table: "NaepProgramDetails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReportingVideo",
                table: "NaepProgramDetails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SourceOfTitle",
                table: "NaepProgramDetails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "UniImplDate",
                table: "NaepProgramDetails",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UniImplLetterFile",
                table: "NaepProgramDetails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GeoTaggedPhoto",
                table: "IbtvaReports",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Outcome",
                table: "IbtvaReports",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProgressReport",
                table: "IbtvaReports",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "ProjectCompletionDate",
                table: "IbtvaReports",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProjectCompletionLetter",
                table: "IbtvaReports",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "ReportDate",
                table: "IbtvaReports",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReportingYear",
                table: "IbtvaReports",
                type: "nvarchar(4)",
                maxLength: 4,
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "TestingCompletionDate",
                table: "IbtvaReports",
                type: "date",
                nullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "UniversitySanctionLetterDate",
                table: "IbtvaProgramDetails",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "ProposalDate",
                table: "IbtvaProgramDetails",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "FundsSanctionLetterDate",
                table: "IbtvaProgramDetails",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Area",
                table: "IbtvaProgramDetails",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FundAmount",
                table: "IbtvaProgramDetails",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "FundReleaseDate",
                table: "IbtvaProgramDetails",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FundReleaseFile",
                table: "IbtvaProgramDetails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FundReleaseYear",
                table: "IbtvaProgramDetails",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherSourceOfInformation",
                table: "IbtvaProgramDetails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PiAddress",
                table: "IbtvaProgramDetails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "ProjectSanctionDate",
                table: "IbtvaProgramDetails",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProjectSanctionFile",
                table: "IbtvaProgramDetails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReportingVideo",
                table: "IbtvaProgramDetails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SourceOfTitle",
                table: "IbtvaProgramDetails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "UniImplDate",
                table: "IbtvaProgramDetails",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UniImplLetterFile",
                table: "IbtvaProgramDetails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GeoTaggedPhoto",
                table: "FtiReports",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Outcome",
                table: "FtiReports",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProgressReport",
                table: "FtiReports",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "ProjectCompletionDate",
                table: "FtiReports",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProjectCompletionLetter",
                table: "FtiReports",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "ReportDate",
                table: "FtiReports",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReportingYear",
                table: "FtiReports",
                type: "nvarchar(4)",
                maxLength: 4,
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "TestingCompletionDate",
                table: "FtiReports",
                type: "date",
                nullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "UniversitySanctionLetterDate",
                table: "FtiProgramDetails",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "ProposalDate",
                table: "FtiProgramDetails",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "FundsSanctionLetterDate",
                table: "FtiProgramDetails",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Area",
                table: "FtiProgramDetails",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FundAmount",
                table: "FtiProgramDetails",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "FundReleaseDate",
                table: "FtiProgramDetails",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FundReleaseFile",
                table: "FtiProgramDetails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FundReleaseYear",
                table: "FtiProgramDetails",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherSourceOfInformation",
                table: "FtiProgramDetails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PiAddress",
                table: "FtiProgramDetails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "ProjectSanctionDate",
                table: "FtiProgramDetails",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProjectSanctionFile",
                table: "FtiProgramDetails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReportingVideo",
                table: "FtiProgramDetails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SourceOfTitle",
                table: "FtiProgramDetails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "UniImplDate",
                table: "FtiProgramDetails",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UniImplLetterFile",
                table: "FtiProgramDetails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GeoTaggedPhoto",
                table: "EeuReports",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Outcome",
                table: "EeuReports",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProgressReport",
                table: "EeuReports",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "ProjectCompletionDate",
                table: "EeuReports",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProjectCompletionLetter",
                table: "EeuReports",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "ReportDate",
                table: "EeuReports",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReportingYear",
                table: "EeuReports",
                type: "nvarchar(4)",
                maxLength: 4,
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "TestingCompletionDate",
                table: "EeuReports",
                type: "date",
                nullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "UniversitySanctionLetterDate",
                table: "EeuProgramDetails",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "ProposalDate",
                table: "EeuProgramDetails",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "FundsSanctionLetterDate",
                table: "EeuProgramDetails",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Area",
                table: "EeuProgramDetails",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FundAmount",
                table: "EeuProgramDetails",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "FundReleaseDate",
                table: "EeuProgramDetails",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FundReleaseFile",
                table: "EeuProgramDetails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FundReleaseYear",
                table: "EeuProgramDetails",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherSourceOfInformation",
                table: "EeuProgramDetails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PiAddress",
                table: "EeuProgramDetails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "ProjectSanctionDate",
                table: "EeuProgramDetails",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProjectSanctionFile",
                table: "EeuProgramDetails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReportingVideo",
                table: "EeuProgramDetails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SourceOfTitle",
                table: "EeuProgramDetails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "UniImplDate",
                table: "EeuProgramDetails",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UniImplLetterFile",
                table: "EeuProgramDetails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GeoTaggedPhoto",
                table: "DeuReports",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Outcome",
                table: "DeuReports",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProgressReport",
                table: "DeuReports",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "ProjectCompletionDate",
                table: "DeuReports",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProjectCompletionLetter",
                table: "DeuReports",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "ReportDate",
                table: "DeuReports",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReportingYear",
                table: "DeuReports",
                type: "nvarchar(4)",
                maxLength: 4,
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "TestingCompletionDate",
                table: "DeuReports",
                type: "date",
                nullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "UniversitySanctionLetterDate",
                table: "DeuProgramDetails",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "DeuProgramDetails",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "ProposalDate",
                table: "DeuProgramDetails",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "FundsSanctionLetterDate",
                table: "DeuProgramDetails",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Area",
                table: "DeuProgramDetails",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FundAmount",
                table: "DeuProgramDetails",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "FundReleaseDate",
                table: "DeuProgramDetails",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FundReleaseFile",
                table: "DeuProgramDetails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FundReleaseYear",
                table: "DeuProgramDetails",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherSourceOfInformation",
                table: "DeuProgramDetails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PiAddress",
                table: "DeuProgramDetails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "ProjectSanctionDate",
                table: "DeuProgramDetails",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProjectSanctionFile",
                table: "DeuProgramDetails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReportingVideo",
                table: "DeuProgramDetails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SourceOfTitle",
                table: "DeuProgramDetails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "UniImplDate",
                table: "DeuProgramDetails",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UniImplLetterFile",
                table: "DeuProgramDetails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GeoTaggedPhoto",
                table: "AticReports",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Outcome",
                table: "AticReports",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProgressReport",
                table: "AticReports",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "ProjectCompletionDate",
                table: "AticReports",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProjectCompletionLetter",
                table: "AticReports",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "ReportDate",
                table: "AticReports",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReportingYear",
                table: "AticReports",
                type: "nvarchar(4)",
                maxLength: 4,
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "TestingCompletionDate",
                table: "AticReports",
                type: "date",
                nullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "UniversitySanctionLetterDate",
                table: "AticProgramDetails",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "ProposalDate",
                table: "AticProgramDetails",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "FundsSanctionLetterDate",
                table: "AticProgramDetails",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Area",
                table: "AticProgramDetails",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FundAmount",
                table: "AticProgramDetails",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "FundReleaseDate",
                table: "AticProgramDetails",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FundReleaseFile",
                table: "AticProgramDetails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FundReleaseYear",
                table: "AticProgramDetails",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherSourceOfInformation",
                table: "AticProgramDetails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PiAddress",
                table: "AticProgramDetails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "ProjectSanctionDate",
                table: "AticProgramDetails",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProjectSanctionFile",
                table: "AticProgramDetails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReportingVideo",
                table: "AticProgramDetails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SourceOfTitle",
                table: "AticProgramDetails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "UniImplDate",
                table: "AticProgramDetails",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UniImplLetterFile",
                table: "AticProgramDetails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GeoTaggedPhoto",
                table: "NaepReports");

            migrationBuilder.DropColumn(
                name: "Outcome",
                table: "NaepReports");

            migrationBuilder.DropColumn(
                name: "ProgressReport",
                table: "NaepReports");

            migrationBuilder.DropColumn(
                name: "ProjectCompletionDate",
                table: "NaepReports");

            migrationBuilder.DropColumn(
                name: "ProjectCompletionLetter",
                table: "NaepReports");

            migrationBuilder.DropColumn(
                name: "ReportDate",
                table: "NaepReports");

            migrationBuilder.DropColumn(
                name: "ReportingYear",
                table: "NaepReports");

            migrationBuilder.DropColumn(
                name: "TestingCompletionDate",
                table: "NaepReports");

            migrationBuilder.DropColumn(
                name: "Area",
                table: "NaepProgramDetails");

            migrationBuilder.DropColumn(
                name: "FundAmount",
                table: "NaepProgramDetails");

            migrationBuilder.DropColumn(
                name: "FundReleaseDate",
                table: "NaepProgramDetails");

            migrationBuilder.DropColumn(
                name: "FundReleaseFile",
                table: "NaepProgramDetails");

            migrationBuilder.DropColumn(
                name: "FundReleaseYear",
                table: "NaepProgramDetails");

            migrationBuilder.DropColumn(
                name: "OtherSourceOfInformation",
                table: "NaepProgramDetails");

            migrationBuilder.DropColumn(
                name: "PiAddress",
                table: "NaepProgramDetails");

            migrationBuilder.DropColumn(
                name: "ProjectSanctionDate",
                table: "NaepProgramDetails");

            migrationBuilder.DropColumn(
                name: "ProjectSanctionFile",
                table: "NaepProgramDetails");

            migrationBuilder.DropColumn(
                name: "ReportingVideo",
                table: "NaepProgramDetails");

            migrationBuilder.DropColumn(
                name: "SourceOfTitle",
                table: "NaepProgramDetails");

            migrationBuilder.DropColumn(
                name: "UniImplDate",
                table: "NaepProgramDetails");

            migrationBuilder.DropColumn(
                name: "UniImplLetterFile",
                table: "NaepProgramDetails");

            migrationBuilder.DropColumn(
                name: "GeoTaggedPhoto",
                table: "IbtvaReports");

            migrationBuilder.DropColumn(
                name: "Outcome",
                table: "IbtvaReports");

            migrationBuilder.DropColumn(
                name: "ProgressReport",
                table: "IbtvaReports");

            migrationBuilder.DropColumn(
                name: "ProjectCompletionDate",
                table: "IbtvaReports");

            migrationBuilder.DropColumn(
                name: "ProjectCompletionLetter",
                table: "IbtvaReports");

            migrationBuilder.DropColumn(
                name: "ReportDate",
                table: "IbtvaReports");

            migrationBuilder.DropColumn(
                name: "ReportingYear",
                table: "IbtvaReports");

            migrationBuilder.DropColumn(
                name: "TestingCompletionDate",
                table: "IbtvaReports");

            migrationBuilder.DropColumn(
                name: "Area",
                table: "IbtvaProgramDetails");

            migrationBuilder.DropColumn(
                name: "FundAmount",
                table: "IbtvaProgramDetails");

            migrationBuilder.DropColumn(
                name: "FundReleaseDate",
                table: "IbtvaProgramDetails");

            migrationBuilder.DropColumn(
                name: "FundReleaseFile",
                table: "IbtvaProgramDetails");

            migrationBuilder.DropColumn(
                name: "FundReleaseYear",
                table: "IbtvaProgramDetails");

            migrationBuilder.DropColumn(
                name: "OtherSourceOfInformation",
                table: "IbtvaProgramDetails");

            migrationBuilder.DropColumn(
                name: "PiAddress",
                table: "IbtvaProgramDetails");

            migrationBuilder.DropColumn(
                name: "ProjectSanctionDate",
                table: "IbtvaProgramDetails");

            migrationBuilder.DropColumn(
                name: "ProjectSanctionFile",
                table: "IbtvaProgramDetails");

            migrationBuilder.DropColumn(
                name: "ReportingVideo",
                table: "IbtvaProgramDetails");

            migrationBuilder.DropColumn(
                name: "SourceOfTitle",
                table: "IbtvaProgramDetails");

            migrationBuilder.DropColumn(
                name: "UniImplDate",
                table: "IbtvaProgramDetails");

            migrationBuilder.DropColumn(
                name: "UniImplLetterFile",
                table: "IbtvaProgramDetails");

            migrationBuilder.DropColumn(
                name: "GeoTaggedPhoto",
                table: "FtiReports");

            migrationBuilder.DropColumn(
                name: "Outcome",
                table: "FtiReports");

            migrationBuilder.DropColumn(
                name: "ProgressReport",
                table: "FtiReports");

            migrationBuilder.DropColumn(
                name: "ProjectCompletionDate",
                table: "FtiReports");

            migrationBuilder.DropColumn(
                name: "ProjectCompletionLetter",
                table: "FtiReports");

            migrationBuilder.DropColumn(
                name: "ReportDate",
                table: "FtiReports");

            migrationBuilder.DropColumn(
                name: "ReportingYear",
                table: "FtiReports");

            migrationBuilder.DropColumn(
                name: "TestingCompletionDate",
                table: "FtiReports");

            migrationBuilder.DropColumn(
                name: "Area",
                table: "FtiProgramDetails");

            migrationBuilder.DropColumn(
                name: "FundAmount",
                table: "FtiProgramDetails");

            migrationBuilder.DropColumn(
                name: "FundReleaseDate",
                table: "FtiProgramDetails");

            migrationBuilder.DropColumn(
                name: "FundReleaseFile",
                table: "FtiProgramDetails");

            migrationBuilder.DropColumn(
                name: "FundReleaseYear",
                table: "FtiProgramDetails");

            migrationBuilder.DropColumn(
                name: "OtherSourceOfInformation",
                table: "FtiProgramDetails");

            migrationBuilder.DropColumn(
                name: "PiAddress",
                table: "FtiProgramDetails");

            migrationBuilder.DropColumn(
                name: "ProjectSanctionDate",
                table: "FtiProgramDetails");

            migrationBuilder.DropColumn(
                name: "ProjectSanctionFile",
                table: "FtiProgramDetails");

            migrationBuilder.DropColumn(
                name: "ReportingVideo",
                table: "FtiProgramDetails");

            migrationBuilder.DropColumn(
                name: "SourceOfTitle",
                table: "FtiProgramDetails");

            migrationBuilder.DropColumn(
                name: "UniImplDate",
                table: "FtiProgramDetails");

            migrationBuilder.DropColumn(
                name: "UniImplLetterFile",
                table: "FtiProgramDetails");

            migrationBuilder.DropColumn(
                name: "GeoTaggedPhoto",
                table: "EeuReports");

            migrationBuilder.DropColumn(
                name: "Outcome",
                table: "EeuReports");

            migrationBuilder.DropColumn(
                name: "ProgressReport",
                table: "EeuReports");

            migrationBuilder.DropColumn(
                name: "ProjectCompletionDate",
                table: "EeuReports");

            migrationBuilder.DropColumn(
                name: "ProjectCompletionLetter",
                table: "EeuReports");

            migrationBuilder.DropColumn(
                name: "ReportDate",
                table: "EeuReports");

            migrationBuilder.DropColumn(
                name: "ReportingYear",
                table: "EeuReports");

            migrationBuilder.DropColumn(
                name: "TestingCompletionDate",
                table: "EeuReports");

            migrationBuilder.DropColumn(
                name: "Area",
                table: "EeuProgramDetails");

            migrationBuilder.DropColumn(
                name: "FundAmount",
                table: "EeuProgramDetails");

            migrationBuilder.DropColumn(
                name: "FundReleaseDate",
                table: "EeuProgramDetails");

            migrationBuilder.DropColumn(
                name: "FundReleaseFile",
                table: "EeuProgramDetails");

            migrationBuilder.DropColumn(
                name: "FundReleaseYear",
                table: "EeuProgramDetails");

            migrationBuilder.DropColumn(
                name: "OtherSourceOfInformation",
                table: "EeuProgramDetails");

            migrationBuilder.DropColumn(
                name: "PiAddress",
                table: "EeuProgramDetails");

            migrationBuilder.DropColumn(
                name: "ProjectSanctionDate",
                table: "EeuProgramDetails");

            migrationBuilder.DropColumn(
                name: "ProjectSanctionFile",
                table: "EeuProgramDetails");

            migrationBuilder.DropColumn(
                name: "ReportingVideo",
                table: "EeuProgramDetails");

            migrationBuilder.DropColumn(
                name: "SourceOfTitle",
                table: "EeuProgramDetails");

            migrationBuilder.DropColumn(
                name: "UniImplDate",
                table: "EeuProgramDetails");

            migrationBuilder.DropColumn(
                name: "UniImplLetterFile",
                table: "EeuProgramDetails");

            migrationBuilder.DropColumn(
                name: "GeoTaggedPhoto",
                table: "DeuReports");

            migrationBuilder.DropColumn(
                name: "Outcome",
                table: "DeuReports");

            migrationBuilder.DropColumn(
                name: "ProgressReport",
                table: "DeuReports");

            migrationBuilder.DropColumn(
                name: "ProjectCompletionDate",
                table: "DeuReports");

            migrationBuilder.DropColumn(
                name: "ProjectCompletionLetter",
                table: "DeuReports");

            migrationBuilder.DropColumn(
                name: "ReportDate",
                table: "DeuReports");

            migrationBuilder.DropColumn(
                name: "ReportingYear",
                table: "DeuReports");

            migrationBuilder.DropColumn(
                name: "TestingCompletionDate",
                table: "DeuReports");

            migrationBuilder.DropColumn(
                name: "Area",
                table: "DeuProgramDetails");

            migrationBuilder.DropColumn(
                name: "FundAmount",
                table: "DeuProgramDetails");

            migrationBuilder.DropColumn(
                name: "FundReleaseDate",
                table: "DeuProgramDetails");

            migrationBuilder.DropColumn(
                name: "FundReleaseFile",
                table: "DeuProgramDetails");

            migrationBuilder.DropColumn(
                name: "FundReleaseYear",
                table: "DeuProgramDetails");

            migrationBuilder.DropColumn(
                name: "OtherSourceOfInformation",
                table: "DeuProgramDetails");

            migrationBuilder.DropColumn(
                name: "PiAddress",
                table: "DeuProgramDetails");

            migrationBuilder.DropColumn(
                name: "ProjectSanctionDate",
                table: "DeuProgramDetails");

            migrationBuilder.DropColumn(
                name: "ProjectSanctionFile",
                table: "DeuProgramDetails");

            migrationBuilder.DropColumn(
                name: "ReportingVideo",
                table: "DeuProgramDetails");

            migrationBuilder.DropColumn(
                name: "SourceOfTitle",
                table: "DeuProgramDetails");

            migrationBuilder.DropColumn(
                name: "UniImplDate",
                table: "DeuProgramDetails");

            migrationBuilder.DropColumn(
                name: "UniImplLetterFile",
                table: "DeuProgramDetails");

            migrationBuilder.DropColumn(
                name: "GeoTaggedPhoto",
                table: "AticReports");

            migrationBuilder.DropColumn(
                name: "Outcome",
                table: "AticReports");

            migrationBuilder.DropColumn(
                name: "ProgressReport",
                table: "AticReports");

            migrationBuilder.DropColumn(
                name: "ProjectCompletionDate",
                table: "AticReports");

            migrationBuilder.DropColumn(
                name: "ProjectCompletionLetter",
                table: "AticReports");

            migrationBuilder.DropColumn(
                name: "ReportDate",
                table: "AticReports");

            migrationBuilder.DropColumn(
                name: "ReportingYear",
                table: "AticReports");

            migrationBuilder.DropColumn(
                name: "TestingCompletionDate",
                table: "AticReports");

            migrationBuilder.DropColumn(
                name: "Area",
                table: "AticProgramDetails");

            migrationBuilder.DropColumn(
                name: "FundAmount",
                table: "AticProgramDetails");

            migrationBuilder.DropColumn(
                name: "FundReleaseDate",
                table: "AticProgramDetails");

            migrationBuilder.DropColumn(
                name: "FundReleaseFile",
                table: "AticProgramDetails");

            migrationBuilder.DropColumn(
                name: "FundReleaseYear",
                table: "AticProgramDetails");

            migrationBuilder.DropColumn(
                name: "OtherSourceOfInformation",
                table: "AticProgramDetails");

            migrationBuilder.DropColumn(
                name: "PiAddress",
                table: "AticProgramDetails");

            migrationBuilder.DropColumn(
                name: "ProjectSanctionDate",
                table: "AticProgramDetails");

            migrationBuilder.DropColumn(
                name: "ProjectSanctionFile",
                table: "AticProgramDetails");

            migrationBuilder.DropColumn(
                name: "ReportingVideo",
                table: "AticProgramDetails");

            migrationBuilder.DropColumn(
                name: "SourceOfTitle",
                table: "AticProgramDetails");

            migrationBuilder.DropColumn(
                name: "UniImplDate",
                table: "AticProgramDetails");

            migrationBuilder.DropColumn(
                name: "UniImplLetterFile",
                table: "AticProgramDetails");

            migrationBuilder.RenameColumn(
                name: "TypeOfReport",
                table: "NaepReports",
                newName: "ProgressReportReportingYear");

            migrationBuilder.RenameColumn(
                name: "TestingCompletionLetter",
                table: "NaepReports",
                newName: "UploadVideo");

            migrationBuilder.RenameColumn(
                name: "SpclReport",
                table: "NaepReports",
                newName: "UploadPhoto");

            migrationBuilder.RenameColumn(
                name: "ReportingVideo",
                table: "NaepReports",
                newName: "PhotosGeotaggedPhotoOrUploadPhoto");

            migrationBuilder.RenameColumn(
                name: "TypeOfReport",
                table: "IbtvaReports",
                newName: "ProgressReportReportingYear");

            migrationBuilder.RenameColumn(
                name: "TestingCompletionLetter",
                table: "IbtvaReports",
                newName: "UploadVideo");

            migrationBuilder.RenameColumn(
                name: "SpclReport",
                table: "IbtvaReports",
                newName: "UploadPhoto");

            migrationBuilder.RenameColumn(
                name: "ReportingVideo",
                table: "IbtvaReports",
                newName: "PhotosGeotaggedPhotoOrUploadPhoto");

            migrationBuilder.RenameColumn(
                name: "TypeOfReport",
                table: "FtiReports",
                newName: "ProgressReportReportingYear");

            migrationBuilder.RenameColumn(
                name: "TestingCompletionLetter",
                table: "FtiReports",
                newName: "UploadVideo");

            migrationBuilder.RenameColumn(
                name: "SpclReport",
                table: "FtiReports",
                newName: "UploadPhoto");

            migrationBuilder.RenameColumn(
                name: "ReportingVideo",
                table: "FtiReports",
                newName: "PhotosGeotaggedPhotoOrUploadPhoto");

            migrationBuilder.RenameColumn(
                name: "TypeOfReport",
                table: "EeuReports",
                newName: "ProgressReportReportingYear");

            migrationBuilder.RenameColumn(
                name: "TestingCompletionLetter",
                table: "EeuReports",
                newName: "UploadVideo");

            migrationBuilder.RenameColumn(
                name: "SpclReport",
                table: "EeuReports",
                newName: "UploadPhoto");

            migrationBuilder.RenameColumn(
                name: "ReportingVideo",
                table: "EeuReports",
                newName: "PhotosGeotaggedPhotoOrUploadPhoto");

            migrationBuilder.RenameColumn(
                name: "TypeOfReport",
                table: "DeuReports",
                newName: "ProgressReportReportingYear");

            migrationBuilder.RenameColumn(
                name: "TestingCompletionLetter",
                table: "DeuReports",
                newName: "UploadVideo");

            migrationBuilder.RenameColumn(
                name: "SpclReport",
                table: "DeuReports",
                newName: "UploadPhoto");

            migrationBuilder.RenameColumn(
                name: "ReportingVideo",
                table: "DeuReports",
                newName: "PhotosGeotaggedPhotoOrUploadPhoto");

            migrationBuilder.RenameColumn(
                name: "TypeOfReport",
                table: "AticReports",
                newName: "ProgressReportReportingYear");

            migrationBuilder.RenameColumn(
                name: "TestingCompletionLetter",
                table: "AticReports",
                newName: "UploadVideo");

            migrationBuilder.RenameColumn(
                name: "SpclReport",
                table: "AticReports",
                newName: "UploadPhoto");

            migrationBuilder.RenameColumn(
                name: "ReportingVideo",
                table: "AticReports",
                newName: "PhotosGeotaggedPhotoOrUploadPhoto");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "NaepReports",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SignificantOutcome",
                table: "NaepReports",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UniversitySanctionLetterDate",
                table: "NaepProgramDetails",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "NaepProgramDetails",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ProposalDate",
                table: "NaepProgramDetails",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FundsSanctionLetterDate",
                table: "NaepProgramDetails",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "IbtvaReports",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SignificantOutcome",
                table: "IbtvaReports",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UniversitySanctionLetterDate",
                table: "IbtvaProgramDetails",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ProposalDate",
                table: "IbtvaProgramDetails",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FundsSanctionLetterDate",
                table: "IbtvaProgramDetails",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "FtiReports",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SignificantOutcome",
                table: "FtiReports",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UniversitySanctionLetterDate",
                table: "FtiProgramDetails",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ProposalDate",
                table: "FtiProgramDetails",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FundsSanctionLetterDate",
                table: "FtiProgramDetails",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "EeuReports",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SignificantOutcome",
                table: "EeuReports",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UniversitySanctionLetterDate",
                table: "EeuProgramDetails",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ProposalDate",
                table: "EeuProgramDetails",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FundsSanctionLetterDate",
                table: "EeuProgramDetails",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "DeuReports",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SignificantOutcome",
                table: "DeuReports",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UniversitySanctionLetterDate",
                table: "DeuProgramDetails",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "DeuProgramDetails",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ProposalDate",
                table: "DeuProgramDetails",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FundsSanctionLetterDate",
                table: "DeuProgramDetails",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "AticReports",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SignificantOutcome",
                table: "AticReports",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UniversitySanctionLetterDate",
                table: "AticProgramDetails",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ProposalDate",
                table: "AticProgramDetails",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FundsSanctionLetterDate",
                table: "AticProgramDetails",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);
        }
    }
}

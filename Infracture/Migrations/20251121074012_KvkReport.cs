using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class KvkReport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "KvkReports");

            migrationBuilder.DropColumn(
                name: "SignificantOutcome",
                table: "KvkReports");

            migrationBuilder.RenameColumn(
                name: "UploadVideo",
                table: "KvkReports",
                newName: "TestingCompletionLetter");

            migrationBuilder.RenameColumn(
                name: "UploadPhoto",
                table: "KvkReports",
                newName: "SpclReport");

            migrationBuilder.RenameColumn(
                name: "ProgressReportReportingYear",
                table: "KvkReports",
                newName: "TypeOfReport");

            migrationBuilder.RenameColumn(
                name: "PhotosGeotaggedPhotoOrUploadPhoto",
                table: "KvkReports",
                newName: "ReportingVideo");

            migrationBuilder.AddColumn<string>(
                name: "GeoTaggedPhoto",
                table: "KvkReports",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Outcome",
                table: "KvkReports",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProgressReport",
                table: "KvkReports",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "ProjectCompletionDate",
                table: "KvkReports",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProjectCompletionLetter",
                table: "KvkReports",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "ReportDate",
                table: "KvkReports",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReportingYear",
                table: "KvkReports",
                type: "nvarchar(4)",
                maxLength: 4,
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "TestingCompletionDate",
                table: "KvkReports",
                type: "date",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GeoTaggedPhoto",
                table: "KvkReports");

            migrationBuilder.DropColumn(
                name: "Outcome",
                table: "KvkReports");

            migrationBuilder.DropColumn(
                name: "ProgressReport",
                table: "KvkReports");

            migrationBuilder.DropColumn(
                name: "ProjectCompletionDate",
                table: "KvkReports");

            migrationBuilder.DropColumn(
                name: "ProjectCompletionLetter",
                table: "KvkReports");

            migrationBuilder.DropColumn(
                name: "ReportDate",
                table: "KvkReports");

            migrationBuilder.DropColumn(
                name: "ReportingYear",
                table: "KvkReports");

            migrationBuilder.DropColumn(
                name: "TestingCompletionDate",
                table: "KvkReports");

            migrationBuilder.RenameColumn(
                name: "TypeOfReport",
                table: "KvkReports",
                newName: "ProgressReportReportingYear");

            migrationBuilder.RenameColumn(
                name: "TestingCompletionLetter",
                table: "KvkReports",
                newName: "UploadVideo");

            migrationBuilder.RenameColumn(
                name: "SpclReport",
                table: "KvkReports",
                newName: "UploadPhoto");

            migrationBuilder.RenameColumn(
                name: "ReportingVideo",
                table: "KvkReports",
                newName: "PhotosGeotaggedPhotoOrUploadPhoto");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "KvkReports",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SignificantOutcome",
                table: "KvkReports",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);
        }
    }
}

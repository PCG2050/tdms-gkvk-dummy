using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMissingFieldsToKvkProgramDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Remove Required constraint from Title
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "KvkProgramDetails",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            // Add new fields
            migrationBuilder.AddColumn<string>(
                name: "PiAddress",
                table: "KvkProgramDetails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherSourceOfInformation",
                table: "KvkProgramDetails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SourceOfTitle",
                table: "KvkProgramDetails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ProjectSanctionDate",
                table: "KvkProgramDetails",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProjectSanctionFile",
                table: "KvkProgramDetails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UniImplDate",
                table: "KvkProgramDetails",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UniImplLetterFile",
                table: "KvkProgramDetails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FundReleaseYear",
                table: "KvkProgramDetails",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "FundAmount",
                table: "KvkProgramDetails",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FundReleaseDate",
                table: "KvkProgramDetails",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FundReleaseFile",
                table: "KvkProgramDetails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReportingVideo",
                table: "KvkProgramDetails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Remove added fields
            migrationBuilder.DropColumn(
                name: "PiAddress",
                table: "KvkProgramDetails");

            migrationBuilder.DropColumn(
                name: "OtherSourceOfInformation",
                table: "KvkProgramDetails");

            migrationBuilder.DropColumn(
                name: "SourceOfTitle",
                table: "KvkProgramDetails");

            migrationBuilder.DropColumn(
                name: "ProjectSanctionDate",
                table: "KvkProgramDetails");

            migrationBuilder.DropColumn(
                name: "ProjectSanctionFile",
                table: "KvkProgramDetails");

            migrationBuilder.DropColumn(
                name: "UniImplDate",
                table: "KvkProgramDetails");

            migrationBuilder.DropColumn(
                name: "UniImplLetterFile",
                table: "KvkProgramDetails");

            migrationBuilder.DropColumn(
                name: "FundReleaseYear",
                table: "KvkProgramDetails");

            migrationBuilder.DropColumn(
                name: "FundAmount",
                table: "KvkProgramDetails");

            migrationBuilder.DropColumn(
                name: "FundReleaseDate",
                table: "KvkProgramDetails");

            migrationBuilder.DropColumn(
                name: "FundReleaseFile",
                table: "KvkProgramDetails");

            migrationBuilder.DropColumn(
                name: "ReportingVideo",
                table: "KvkProgramDetails");

            // Restore Required constraint on Title
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "KvkProgramDetails",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldNullable: true);
        }
    }
}

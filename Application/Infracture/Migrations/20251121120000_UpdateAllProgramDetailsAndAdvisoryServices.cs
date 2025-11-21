using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class UpdateAllProgramDetailsAndAdvisoryServices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // ============================================================================
            // PROGRAM DETAILS TABLES - Apply changes to all 7 units
            // ============================================================================

            var programDetailsTables = new[]
            {
                "StuProgramDetails",
                "DeuProgramDetails",
                "EeuProgramDetails",
                "NaepProgramDetails",
                "IbtvaProgramDetails",
                "AticProgramDetails",
                "FtiProgramDetails"
            };

            foreach (var table in programDetailsTables)
            {
                // A. Add Area column
                migrationBuilder.AddColumn<int>(
                    name: "Area",
                    table: table,
                    type: "int",
                    nullable: true);

                // B. Alter existing date columns from DateTime to DateOnly
                migrationBuilder.AlterColumn<DateOnly>(
                    name: "ProposalDate",
                    table: table,
                    type: "date",
                    nullable: true,
                    oldClrType: typeof(DateTime),
                    oldType: "datetime2",
                    oldNullable: true);

                migrationBuilder.AlterColumn<DateOnly>(
                    name: "UniversitySanctionLetterDate",
                    table: table,
                    type: "date",
                    nullable: true,
                    oldClrType: typeof(DateTime),
                    oldType: "datetime2",
                    oldNullable: true);

                migrationBuilder.AlterColumn<DateOnly>(
                    name: "FundsSanctionLetterDate",
                    table: table,
                    type: "date",
                    nullable: true,
                    oldClrType: typeof(DateTime),
                    oldType: "datetime2",
                    oldNullable: true);

                // C. Add new columns
                migrationBuilder.AddColumn<string>(
                    name: "PiAddress",
                    table: table,
                    type: "nvarchar(500)",
                    maxLength: 500,
                    nullable: true);

                migrationBuilder.AddColumn<string>(
                    name: "OtherSourceOfInformation",
                    table: table,
                    type: "nvarchar(500)",
                    maxLength: 500,
                    nullable: true);

                migrationBuilder.AddColumn<string>(
                    name: "SourceOfTitle",
                    table: table,
                    type: "nvarchar(500)",
                    maxLength: 500,
                    nullable: true);

                migrationBuilder.AddColumn<DateOnly>(
                    name: "ProjectSanctionDate",
                    table: table,
                    type: "date",
                    nullable: true);

                migrationBuilder.AddColumn<string>(
                    name: "ProjectSanctionFile",
                    table: table,
                    type: "nvarchar(500)",
                    maxLength: 500,
                    nullable: true);

                migrationBuilder.AddColumn<DateOnly>(
                    name: "UniImplDate",
                    table: table,
                    type: "date",
                    nullable: true);

                migrationBuilder.AddColumn<string>(
                    name: "UniImplLetterFile",
                    table: table,
                    type: "nvarchar(500)",
                    maxLength: 500,
                    nullable: true);

                migrationBuilder.AddColumn<string>(
                    name: "FundReleaseYear",
                    table: table,
                    type: "nvarchar(100)",
                    maxLength: 100,
                    nullable: true);

                migrationBuilder.AddColumn<double>(
                    name: "FundAmount",
                    table: table,
                    type: "float",
                    nullable: true);

                migrationBuilder.AddColumn<DateOnly>(
                    name: "FundReleaseDate",
                    table: table,
                    type: "date",
                    nullable: true);

                migrationBuilder.AddColumn<string>(
                    name: "FundReleaseFile",
                    table: table,
                    type: "nvarchar(500)",
                    maxLength: 500,
                    nullable: true);

                migrationBuilder.AddColumn<string>(
                    name: "ReportingVideo",
                    table: table,
                    type: "nvarchar(500)",
                    maxLength: 500,
                    nullable: true);
            }

            // ============================================================================
            // ADVISORY SERVICES TABLES - Make int columns nullable for all 8 units
            // ============================================================================

            var advisoryServicesTables = new[]
            {
                "KvkAdvisoryServices",
                "StuAdvisoryServices",
                "DeuAdvisoryServices",
                "EeuAdvisoryServices",
                "NaepAdvisoryServices",
                "IbtvaAdvisoryServices",
                "AticAdvisoryServices",
                "FtiAdvisoryServices"
            };

            foreach (var table in advisoryServicesTables)
            {
                migrationBuilder.AlterColumn<int>(
                    name: "NoOfFacebookSMS",
                    table: table,
                    type: "int",
                    nullable: true,
                    oldClrType: typeof(int),
                    oldType: "int");

                migrationBuilder.AlterColumn<int>(
                    name: "NoOfSMSSentToRegisteredFarmers",
                    table: table,
                    type: "int",
                    nullable: true,
                    oldClrType: typeof(int),
                    oldType: "int");

                migrationBuilder.AlterColumn<int>(
                    name: "NoOfWhatsappGroups",
                    table: table,
                    type: "int",
                    nullable: true,
                    oldClrType: typeof(int),
                    oldType: "int");

                migrationBuilder.AlterColumn<int>(
                    name: "NoOfWhatsappSMS",
                    table: table,
                    type: "int",
                    nullable: true,
                    oldClrType: typeof(int),
                    oldType: "int");

                migrationBuilder.AlterColumn<int>(
                    name: "NoOfAnsweredWhatsappQueries",
                    table: table,
                    type: "int",
                    nullable: true,
                    oldClrType: typeof(int),
                    oldType: "int");

                migrationBuilder.AlterColumn<int>(
                    name: "NoOfPhoneCalls",
                    table: table,
                    type: "int",
                    nullable: true,
                    oldClrType: typeof(int),
                    oldType: "int");

                migrationBuilder.AlterColumn<int>(
                    name: "NoOfFaceToFaceDiscussions",
                    table: table,
                    type: "int",
                    nullable: true,
                    oldClrType: typeof(int),
                    oldType: "int");

                migrationBuilder.AlterColumn<int>(
                    name: "NoOfGroupDiscussions",
                    table: table,
                    type: "int",
                    nullable: true,
                    oldClrType: typeof(int),
                    oldType: "int");

                migrationBuilder.AlterColumn<int>(
                    name: "NoOfEmailsSent",
                    table: table,
                    type: "int",
                    nullable: true,
                    oldClrType: typeof(int),
                    oldType: "int");

                migrationBuilder.AlterColumn<int>(
                    name: "NoOfNewspaperCoverage",
                    table: table,
                    type: "int",
                    nullable: true,
                    oldClrType: typeof(int),
                    oldType: "int");

                migrationBuilder.AlterColumn<int>(
                    name: "NoOfBeneficiaries",
                    table: table,
                    type: "int",
                    nullable: true,
                    oldClrType: typeof(int),
                    oldType: "int");
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // ============================================================================
            // REVERSE PROGRAM DETAILS TABLES CHANGES
            // ============================================================================

            var programDetailsTables = new[]
            {
                "StuProgramDetails",
                "DeuProgramDetails",
                "EeuProgramDetails",
                "NaepProgramDetails",
                "IbtvaProgramDetails",
                "AticProgramDetails",
                "FtiProgramDetails"
            };

            foreach (var table in programDetailsTables)
            {
                // Drop new columns (in reverse order)
                migrationBuilder.DropColumn(
                    name: "ReportingVideo",
                    table: table);

                migrationBuilder.DropColumn(
                    name: "FundReleaseFile",
                    table: table);

                migrationBuilder.DropColumn(
                    name: "FundReleaseDate",
                    table: table);

                migrationBuilder.DropColumn(
                    name: "FundAmount",
                    table: table);

                migrationBuilder.DropColumn(
                    name: "FundReleaseYear",
                    table: table);

                migrationBuilder.DropColumn(
                    name: "UniImplLetterFile",
                    table: table);

                migrationBuilder.DropColumn(
                    name: "UniImplDate",
                    table: table);

                migrationBuilder.DropColumn(
                    name: "ProjectSanctionFile",
                    table: table);

                migrationBuilder.DropColumn(
                    name: "ProjectSanctionDate",
                    table: table);

                migrationBuilder.DropColumn(
                    name: "SourceOfTitle",
                    table: table);

                migrationBuilder.DropColumn(
                    name: "OtherSourceOfInformation",
                    table: table);

                migrationBuilder.DropColumn(
                    name: "PiAddress",
                    table: table);

                migrationBuilder.DropColumn(
                    name: "Area",
                    table: table);

                // Revert date columns from DateOnly back to DateTime
                migrationBuilder.AlterColumn<DateTime>(
                    name: "ProposalDate",
                    table: table,
                    type: "datetime2",
                    nullable: true,
                    oldClrType: typeof(DateOnly),
                    oldType: "date",
                    oldNullable: true);

                migrationBuilder.AlterColumn<DateTime>(
                    name: "UniversitySanctionLetterDate",
                    table: table,
                    type: "datetime2",
                    nullable: true,
                    oldClrType: typeof(DateOnly),
                    oldType: "date",
                    oldNullable: true);

                migrationBuilder.AlterColumn<DateTime>(
                    name: "FundsSanctionLetterDate",
                    table: table,
                    type: "datetime2",
                    nullable: true,
                    oldClrType: typeof(DateOnly),
                    oldType: "date",
                    oldNullable: true);
            }

            // ============================================================================
            // REVERSE ADVISORY SERVICES TABLES CHANGES
            // ============================================================================

            var advisoryServicesTables = new[]
            {
                "KvkAdvisoryServices",
                "StuAdvisoryServices",
                "DeuAdvisoryServices",
                "EeuAdvisoryServices",
                "NaepAdvisoryServices",
                "IbtvaAdvisoryServices",
                "AticAdvisoryServices",
                "FtiAdvisoryServices"
            };

            foreach (var table in advisoryServicesTables)
            {
                // Revert int columns from nullable back to non-nullable
                migrationBuilder.AlterColumn<int>(
                    name: "NoOfFacebookSMS",
                    table: table,
                    type: "int",
                    nullable: false,
                    oldClrType: typeof(int),
                    oldType: "int",
                    oldNullable: true);

                migrationBuilder.AlterColumn<int>(
                    name: "NoOfSMSSentToRegisteredFarmers",
                    table: table,
                    type: "int",
                    nullable: false,
                    oldClrType: typeof(int),
                    oldType: "int",
                    oldNullable: true);

                migrationBuilder.AlterColumn<int>(
                    name: "NoOfWhatsappGroups",
                    table: table,
                    type: "int",
                    nullable: false,
                    oldClrType: typeof(int),
                    oldType: "int",
                    oldNullable: true);

                migrationBuilder.AlterColumn<int>(
                    name: "NoOfWhatsappSMS",
                    table: table,
                    type: "int",
                    nullable: false,
                    oldClrType: typeof(int),
                    oldType: "int",
                    oldNullable: true);

                migrationBuilder.AlterColumn<int>(
                    name: "NoOfAnsweredWhatsappQueries",
                    table: table,
                    type: "int",
                    nullable: false,
                    oldClrType: typeof(int),
                    oldType: "int",
                    oldNullable: true);

                migrationBuilder.AlterColumn<int>(
                    name: "NoOfPhoneCalls",
                    table: table,
                    type: "int",
                    nullable: false,
                    oldClrType: typeof(int),
                    oldType: "int",
                    oldNullable: true);

                migrationBuilder.AlterColumn<int>(
                    name: "NoOfFaceToFaceDiscussions",
                    table: table,
                    type: "int",
                    nullable: false,
                    oldClrType: typeof(int),
                    oldType: "int",
                    oldNullable: true);

                migrationBuilder.AlterColumn<int>(
                    name: "NoOfGroupDiscussions",
                    table: table,
                    type: "int",
                    nullable: false,
                    oldClrType: typeof(int),
                    oldType: "int",
                    oldNullable: true);

                migrationBuilder.AlterColumn<int>(
                    name: "NoOfEmailsSent",
                    table: table,
                    type: "int",
                    nullable: false,
                    oldClrType: typeof(int),
                    oldType: "int",
                    oldNullable: true);

                migrationBuilder.AlterColumn<int>(
                    name: "NoOfNewspaperCoverage",
                    table: table,
                    type: "int",
                    nullable: false,
                    oldClrType: typeof(int),
                    oldType: "int",
                    oldNullable: true);

                migrationBuilder.AlterColumn<int>(
                    name: "NoOfBeneficiaries",
                    table: table,
                    type: "int",
                    nullable: false,
                    oldClrType: typeof(int),
                    oldType: "int",
                    oldNullable: true);
            }
        }
    }
}

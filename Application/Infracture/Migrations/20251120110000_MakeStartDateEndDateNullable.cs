using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MakeStartDateEndDateNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Make StartDate and EndDate nullable in all tables that inherit from ReportEntryBaseEntity

            // KVK
            migrationBuilder.AlterColumn<DateOnly>(
                name: "StartDate",
                table: "KvkProgramDetails",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "EndDate",
                table: "KvkProgramDetails",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            // STU
            migrationBuilder.AlterColumn<DateOnly>(
                name: "StartDate",
                table: "StuProgramDetails",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "EndDate",
                table: "StuProgramDetails",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            // DEU
            migrationBuilder.AlterColumn<DateOnly>(
                name: "StartDate",
                table: "DeuProgramDetails",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "EndDate",
                table: "DeuProgramDetails",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            // EEU
            migrationBuilder.AlterColumn<DateOnly>(
                name: "StartDate",
                table: "EeuProgramDetails",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "EndDate",
                table: "EeuProgramDetails",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            // NAEP
            migrationBuilder.AlterColumn<DateOnly>(
                name: "StartDate",
                table: "NaepProgramDetails",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "EndDate",
                table: "NaepProgramDetails",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            // IBTVA
            migrationBuilder.AlterColumn<DateOnly>(
                name: "StartDate",
                table: "IbtvaProgramDetails",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "EndDate",
                table: "IbtvaProgramDetails",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            // ATIC
            migrationBuilder.AlterColumn<DateOnly>(
                name: "StartDate",
                table: "AticProgramDetails",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "EndDate",
                table: "AticProgramDetails",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            // Add other tables as needed (ASM, FTI, FIU, etc.)
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Revert changes

            // KVK
            migrationBuilder.AlterColumn<DateOnly>(
                name: "StartDate",
                table: "KvkProgramDetails",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "EndDate",
                table: "KvkProgramDetails",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            // STU
            migrationBuilder.AlterColumn<DateOnly>(
                name: "StartDate",
                table: "StuProgramDetails",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "EndDate",
                table: "StuProgramDetails",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            // DEU
            migrationBuilder.AlterColumn<DateOnly>(
                name: "StartDate",
                table: "DeuProgramDetails",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "EndDate",
                table: "DeuProgramDetails",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            // EEU
            migrationBuilder.AlterColumn<DateOnly>(
                name: "StartDate",
                table: "EeuProgramDetails",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "EndDate",
                table: "EeuProgramDetails",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            // NAEP
            migrationBuilder.AlterColumn<DateOnly>(
                name: "StartDate",
                table: "NaepProgramDetails",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "EndDate",
                table: "NaepProgramDetails",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            // IBTVA
            migrationBuilder.AlterColumn<DateOnly>(
                name: "StartDate",
                table: "IbtvaProgramDetails",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "EndDate",
                table: "IbtvaProgramDetails",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            // ATIC
            migrationBuilder.AlterColumn<DateOnly>(
                name: "StartDate",
                table: "AticProgramDetails",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "EndDate",
                table: "AticProgramDetails",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);
        }
    }
}

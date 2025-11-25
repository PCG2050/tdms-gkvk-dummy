using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class IsDeactivated_Fixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeactivatedAt",
                table: "UnitTrainers",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeactivatedById",
                table: "UnitTrainers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeactivated",
                table: "UnitTrainers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeactivatedAt",
                table: "UnitHeadAssignments",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeactivatedById",
                table: "UnitHeadAssignments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeactivated",
                table: "UnitHeadAssignments",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeactivatedAt",
                table: "UnitTrainers");

            migrationBuilder.DropColumn(
                name: "DeactivatedById",
                table: "UnitTrainers");

            migrationBuilder.DropColumn(
                name: "IsDeactivated",
                table: "UnitTrainers");

            migrationBuilder.DropColumn(
                name: "DeactivatedAt",
                table: "UnitHeadAssignments");

            migrationBuilder.DropColumn(
                name: "DeactivatedById",
                table: "UnitHeadAssignments");

            migrationBuilder.DropColumn(
                name: "IsDeactivated",
                table: "UnitHeadAssignments");
        }
    }
}

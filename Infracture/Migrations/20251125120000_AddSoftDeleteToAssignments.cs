using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSoftDeleteToAssignments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Add soft delete columns to UnitHeadAssignments table
            migrationBuilder.AddColumn<bool>(
                name: "IsDeactivated",
                table: "UnitHeadAssignments",
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

            // Add soft delete columns to UnitTrainers table (TrainerAssignments)
            migrationBuilder.AddColumn<bool>(
                name: "IsDeactivated",
                table: "UnitTrainers",
                type: "bit",
                nullable: false,
                defaultValue: false);

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Remove soft delete columns from UnitHeadAssignments table
            migrationBuilder.DropColumn(
                name: "IsDeactivated",
                table: "UnitHeadAssignments");

            migrationBuilder.DropColumn(
                name: "DeactivatedAt",
                table: "UnitHeadAssignments");

            migrationBuilder.DropColumn(
                name: "DeactivatedById",
                table: "UnitHeadAssignments");

            // Remove soft delete columns from UnitTrainers table
            migrationBuilder.DropColumn(
                name: "IsDeactivated",
                table: "UnitTrainers");

            migrationBuilder.DropColumn(
                name: "DeactivatedAt",
                table: "UnitTrainers");

            migrationBuilder.DropColumn(
                name: "DeactivatedById",
                table: "UnitTrainers");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PublicationUnitCodeAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Publications_OrganizationUnitLocations_UnitLocationId",
                table: "Publications");

            migrationBuilder.DropForeignKey(
                name: "FK_Publications_Organizations_OrganizationId",
                table: "Publications");

            migrationBuilder.DropIndex(
                name: "IX_Publications_OrganizationId",
                table: "Publications");

            migrationBuilder.DropIndex(
                name: "IX_Publications_UnitLocationId",
                table: "Publications");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Publications");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "Publications");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Publications");

            migrationBuilder.DropColumn(
                name: "UnitLocationId",
                table: "Publications");

            migrationBuilder.RenameColumn(
                name: "Attachements",
                table: "Publications",
                newName: "UnitCode");

            migrationBuilder.AddColumn<int>(
                name: "CurrentPhase",
                table: "IbtvaProgramDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "FormStatus",
                table: "IbtvaProgramDetails",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentPhase",
                table: "IbtvaProgramDetails");

            migrationBuilder.DropColumn(
                name: "FormStatus",
                table: "IbtvaProgramDetails");

            migrationBuilder.RenameColumn(
                name: "UnitCode",
                table: "Publications",
                newName: "Attachements");

            migrationBuilder.AddColumn<DateOnly>(
                name: "EndDate",
                table: "Publications",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "Publications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateOnly>(
                name: "StartDate",
                table: "Publications",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<int>(
                name: "UnitLocationId",
                table: "Publications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Publications_OrganizationId",
                table: "Publications",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Publications_UnitLocationId",
                table: "Publications",
                column: "UnitLocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Publications_OrganizationUnitLocations_UnitLocationId",
                table: "Publications",
                column: "UnitLocationId",
                principalTable: "OrganizationUnitLocations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Publications_Organizations_OrganizationId",
                table: "Publications",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id");
        }
    }
}

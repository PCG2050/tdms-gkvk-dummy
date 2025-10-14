using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NominationReward : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Attachements",
                table: "NominationRewards");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ApprovedAt",
                table: "OtherActivities",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApprovedById",
                table: "OtherActivities",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "EndDate",
                table: "OtherActivities",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FormStatus",
                table: "OtherActivities",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FormStatusRemarks",
                table: "OtherActivities",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "OtherActivities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateOnly>(
                name: "StartDate",
                table: "OtherActivities",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnitLocationId",
                table: "OtherActivities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "StartDate",
                table: "NominationRewards",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "EndDate",
                table: "NominationRewards",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ApprovedAt",
                table: "NominationRewards",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApprovedById",
                table: "NominationRewards",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FormStatus",
                table: "NominationRewards",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FormStatusRemarks",
                table: "NominationRewards",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OtherActivities_ApprovedById",
                table: "OtherActivities",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_OtherActivities_OrganizationId",
                table: "OtherActivities",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_OtherActivities_UnitLocationId",
                table: "OtherActivities",
                column: "UnitLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_NominationRewards_ApprovedById",
                table: "NominationRewards",
                column: "ApprovedById");

            migrationBuilder.AddForeignKey(
                name: "FK_NominationRewards_Users_ApprovedById",
                table: "NominationRewards",
                column: "ApprovedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OtherActivities_OrganizationUnitLocations_UnitLocationId",
                table: "OtherActivities",
                column: "UnitLocationId",
                principalTable: "OrganizationUnitLocations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OtherActivities_Organizations_OrganizationId",
                table: "OtherActivities",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OtherActivities_Users_ApprovedById",
                table: "OtherActivities",
                column: "ApprovedById",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NominationRewards_Users_ApprovedById",
                table: "NominationRewards");

            migrationBuilder.DropForeignKey(
                name: "FK_OtherActivities_OrganizationUnitLocations_UnitLocationId",
                table: "OtherActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_OtherActivities_Organizations_OrganizationId",
                table: "OtherActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_OtherActivities_Users_ApprovedById",
                table: "OtherActivities");

            migrationBuilder.DropIndex(
                name: "IX_OtherActivities_ApprovedById",
                table: "OtherActivities");

            migrationBuilder.DropIndex(
                name: "IX_OtherActivities_OrganizationId",
                table: "OtherActivities");

            migrationBuilder.DropIndex(
                name: "IX_OtherActivities_UnitLocationId",
                table: "OtherActivities");

            migrationBuilder.DropIndex(
                name: "IX_NominationRewards_ApprovedById",
                table: "NominationRewards");

            migrationBuilder.DropColumn(
                name: "ApprovedAt",
                table: "OtherActivities");

            migrationBuilder.DropColumn(
                name: "ApprovedById",
                table: "OtherActivities");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "OtherActivities");

            migrationBuilder.DropColumn(
                name: "FormStatus",
                table: "OtherActivities");

            migrationBuilder.DropColumn(
                name: "FormStatusRemarks",
                table: "OtherActivities");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "OtherActivities");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "OtherActivities");

            migrationBuilder.DropColumn(
                name: "UnitLocationId",
                table: "OtherActivities");

            migrationBuilder.DropColumn(
                name: "ApprovedAt",
                table: "NominationRewards");

            migrationBuilder.DropColumn(
                name: "ApprovedById",
                table: "NominationRewards");

            migrationBuilder.DropColumn(
                name: "FormStatus",
                table: "NominationRewards");

            migrationBuilder.DropColumn(
                name: "FormStatusRemarks",
                table: "NominationRewards");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "StartDate",
                table: "NominationRewards",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "EndDate",
                table: "NominationRewards",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Attachements",
                table: "NominationRewards",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

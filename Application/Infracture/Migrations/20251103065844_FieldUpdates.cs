using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FieldUpdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ASMVisitorDetails_Statuses_StatusId",
                table: "ASMVisitorDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_NominationRewards_Positions_InstitutionPositionId",
                table: "NominationRewards");

            migrationBuilder.DropIndex(
                name: "IX_NominationRewards_InstitutionPositionId",
                table: "NominationRewards");

            migrationBuilder.DropIndex(
                name: "IX_ASMVisitorDetails_StatusId",
                table: "ASMVisitorDetails");

            migrationBuilder.DropColumn(
                name: "InstitutionPositionId",
                table: "NominationRewards");

            migrationBuilder.DropColumn(
                name: "Actions",
                table: "ASMVisitorDetails");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "ASMVisitorDetails");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ApprovedAt",
                table: "FIUProgramActivities",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApprovedById",
                table: "FIUProgramActivities",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "EndDate",
                table: "FIUProgramActivities",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FormStatus",
                table: "FIUProgramActivities",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FormStatusRemarks",
                table: "FIUProgramActivities",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "StartDate",
                table: "FIUProgramActivities",
                type: "date",
                nullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "StartDate",
                table: "ASMVisitorDetails",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "InstituteName",
                table: "ASMVisitorDetails",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "EndDate",
                table: "ASMVisitorDetails",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ApprovedAt",
                table: "ASMVisitorDetails",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApprovedById",
                table: "ASMVisitorDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FIUActivitiesId",
                table: "ASMVisitorDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FormStatus",
                table: "ASMVisitorDetails",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FormStatusRemarks",
                table: "ASMVisitorDetails",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "ASMVisitorDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UnitLocationId",
                table: "ASMVisitorDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_FIUProgramActivities_ApprovedById",
                table: "FIUProgramActivities",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_ASMVisitorDetails_ApprovedById",
                table: "ASMVisitorDetails",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_ASMVisitorDetails_OrganizationId",
                table: "ASMVisitorDetails",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_ASMVisitorDetails_UnitLocationId",
                table: "ASMVisitorDetails",
                column: "UnitLocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_ASMVisitorDetails_OrganizationUnitLocations_UnitLocationId",
                table: "ASMVisitorDetails",
                column: "UnitLocationId",
                principalTable: "OrganizationUnitLocations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ASMVisitorDetails_Organizations_OrganizationId",
                table: "ASMVisitorDetails",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ASMVisitorDetails_Users_ApprovedById",
                table: "ASMVisitorDetails",
                column: "ApprovedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FIUProgramActivities_Users_ApprovedById",
                table: "FIUProgramActivities",
                column: "ApprovedById",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ASMVisitorDetails_OrganizationUnitLocations_UnitLocationId",
                table: "ASMVisitorDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ASMVisitorDetails_Organizations_OrganizationId",
                table: "ASMVisitorDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ASMVisitorDetails_Users_ApprovedById",
                table: "ASMVisitorDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_FIUProgramActivities_Users_ApprovedById",
                table: "FIUProgramActivities");

            migrationBuilder.DropIndex(
                name: "IX_FIUProgramActivities_ApprovedById",
                table: "FIUProgramActivities");

            migrationBuilder.DropIndex(
                name: "IX_ASMVisitorDetails_ApprovedById",
                table: "ASMVisitorDetails");

            migrationBuilder.DropIndex(
                name: "IX_ASMVisitorDetails_OrganizationId",
                table: "ASMVisitorDetails");

            migrationBuilder.DropIndex(
                name: "IX_ASMVisitorDetails_UnitLocationId",
                table: "ASMVisitorDetails");

            migrationBuilder.DropColumn(
                name: "ApprovedAt",
                table: "FIUProgramActivities");

            migrationBuilder.DropColumn(
                name: "ApprovedById",
                table: "FIUProgramActivities");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "FIUProgramActivities");

            migrationBuilder.DropColumn(
                name: "FormStatus",
                table: "FIUProgramActivities");

            migrationBuilder.DropColumn(
                name: "FormStatusRemarks",
                table: "FIUProgramActivities");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "FIUProgramActivities");

            migrationBuilder.DropColumn(
                name: "ApprovedAt",
                table: "ASMVisitorDetails");

            migrationBuilder.DropColumn(
                name: "ApprovedById",
                table: "ASMVisitorDetails");

            migrationBuilder.DropColumn(
                name: "FIUActivitiesId",
                table: "ASMVisitorDetails");

            migrationBuilder.DropColumn(
                name: "FormStatus",
                table: "ASMVisitorDetails");

            migrationBuilder.DropColumn(
                name: "FormStatusRemarks",
                table: "ASMVisitorDetails");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "ASMVisitorDetails");

            migrationBuilder.DropColumn(
                name: "UnitLocationId",
                table: "ASMVisitorDetails");

            migrationBuilder.AddColumn<int>(
                name: "InstitutionPositionId",
                table: "NominationRewards",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "ASMVisitorDetails",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "InstituteName",
                table: "ASMVisitorDetails",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "ASMVisitorDetails",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Actions",
                table: "ASMVisitorDetails",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "ASMVisitorDetails",
                type: "int",
                maxLength: 100,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_NominationRewards_InstitutionPositionId",
                table: "NominationRewards",
                column: "InstitutionPositionId");

            migrationBuilder.CreateIndex(
                name: "IX_ASMVisitorDetails_StatusId",
                table: "ASMVisitorDetails",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_ASMVisitorDetails_Statuses_StatusId",
                table: "ASMVisitorDetails",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NominationRewards_Positions_InstitutionPositionId",
                table: "NominationRewards",
                column: "InstitutionPositionId",
                principalTable: "Positions",
                principalColumn: "Id");
        }
    }
}

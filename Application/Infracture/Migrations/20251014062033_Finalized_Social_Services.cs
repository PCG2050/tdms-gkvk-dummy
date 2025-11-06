using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Finalized_Social_Services : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RevolvingFundStatuses_Services_TblServiceId",
                table: "RevolvingFundStatuses");

            migrationBuilder.DropForeignKey(
                name: "FK_TableModeAndOutreaches_TableConsultingAndSocialMediaServices_TableConsultingAndSocialMediaService",
                table: "TableModeAndOutreaches");

            migrationBuilder.DropForeignKey(
                name: "FK_VisitorDetail_Services_TblServiceId",
                table: "VisitorDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_VisitorDetail_Users_CreatedById",
                table: "VisitorDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_VisitorDetail_Users_UpdatedById",
                table: "VisitorDetail");

            migrationBuilder.DropIndex(
                name: "IX_TableModeAndOutreaches_TableConsultingAndSocialMediaService",
                table: "TableModeAndOutreaches");

            migrationBuilder.DropIndex(
                name: "IX_RevolvingFundStatuses_TblServiceId",
                table: "RevolvingFundStatuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VisitorDetail",
                table: "VisitorDetail");

            migrationBuilder.DropColumn(
                name: "TableConsultingAndSocialMediaService",
                table: "TableModeAndOutreaches");

            migrationBuilder.DropColumn(
                name: "TblServiceId",
                table: "RevolvingFundStatuses");

            migrationBuilder.RenameTable(
                name: "VisitorDetail",
                newName: "VisitorDetails");

            migrationBuilder.RenameIndex(
                name: "IX_VisitorDetail_UpdatedById",
                table: "VisitorDetails",
                newName: "IX_VisitorDetails_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_VisitorDetail_TblServiceId",
                table: "VisitorDetails",
                newName: "IX_VisitorDetails_TblServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_VisitorDetail_CreatedById",
                table: "VisitorDetails",
                newName: "IX_VisitorDetails_CreatedById");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "Date",
                table: "TableHostels",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<decimal>(
                name: "AmountGenerated",
                table: "TableHostels",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<DateOnly>(
                name: "SubmittedDate",
                table: "TableHostels",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AlterColumn<int>(
                name: "RelatedToId",
                table: "TableConsultingAndSocialMediaServices",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "TableConsultingAndSocialMediaServices",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ApprovedAt",
                table: "TableConsultingAndSocialMediaServices",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApprovedById",
                table: "TableConsultingAndSocialMediaServices",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "EndDate",
                table: "TableConsultingAndSocialMediaServices",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FormStatus",
                table: "TableConsultingAndSocialMediaServices",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FormStatusRemarks",
                table: "TableConsultingAndSocialMediaServices",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "TableConsultingAndSocialMediaServices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateOnly>(
                name: "StartDate",
                table: "TableConsultingAndSocialMediaServices",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnitLocationId",
                table: "TableConsultingAndSocialMediaServices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "Date",
                table: "Services",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<decimal>(
                name: "AmountGenerated",
                table: "Services",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ApprovedAt",
                table: "Services",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApprovedById",
                table: "Services",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "EndDate",
                table: "Services",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FormStatus",
                table: "Services",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FormStatusRemarks",
                table: "Services",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "Services",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateOnly>(
                name: "StartDate",
                table: "Services",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnitLocationId",
                table: "Services",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Receipt",
                table: "RevolvingFundStatuses",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<decimal>(
                name: "OpeningBalance",
                table: "RevolvingFundStatuses",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<decimal>(
                name: "Expenditure",
                table: "RevolvingFundStatuses",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<decimal>(
                name: "ClosingBalance",
                table: "RevolvingFundStatuses",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<int>(
                name: "VisitorId",
                table: "VisitorDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_VisitorDetails",
                table: "VisitorDetails",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_TableModeAndOutreaches_ConsultingServiceId",
                table: "TableModeAndOutreaches",
                column: "ConsultingServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_TableConsultingAndSocialMediaServices_ApprovedById",
                table: "TableConsultingAndSocialMediaServices",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_TableConsultingAndSocialMediaServices_OrganizationId",
                table: "TableConsultingAndSocialMediaServices",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_TableConsultingAndSocialMediaServices_UnitLocationId",
                table: "TableConsultingAndSocialMediaServices",
                column: "UnitLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_ApprovedById",
                table: "Services",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_Services_OrganizationId",
                table: "Services",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_UnitLocationId",
                table: "Services",
                column: "UnitLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_RevolvingFundStatuses_ServiceId",
                table: "RevolvingFundStatuses",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_VisitorDetails_VisitorId",
                table: "VisitorDetails",
                column: "VisitorId");

            migrationBuilder.AddForeignKey(
                name: "FK_RevolvingFundStatuses_Services_ServiceId",
                table: "RevolvingFundStatuses",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_OrganizationUnitLocations_UnitLocationId",
                table: "Services",
                column: "UnitLocationId",
                principalTable: "OrganizationUnitLocations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Organizations_OrganizationId",
                table: "Services",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Users_ApprovedById",
                table: "Services",
                column: "ApprovedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TableConsultingAndSocialMediaServices_OrganizationUnitLocations_UnitLocationId",
                table: "TableConsultingAndSocialMediaServices",
                column: "UnitLocationId",
                principalTable: "OrganizationUnitLocations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TableConsultingAndSocialMediaServices_Organizations_OrganizationId",
                table: "TableConsultingAndSocialMediaServices",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TableConsultingAndSocialMediaServices_Users_ApprovedById",
                table: "TableConsultingAndSocialMediaServices",
                column: "ApprovedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TableModeAndOutreaches_TableConsultingAndSocialMediaServices_ConsultingServiceId",
                table: "TableModeAndOutreaches",
                column: "ConsultingServiceId",
                principalTable: "TableConsultingAndSocialMediaServices",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VisitorDetails_Services_TblServiceId",
                table: "VisitorDetails",
                column: "TblServiceId",
                principalTable: "Services",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VisitorDetails_Users_CreatedById",
                table: "VisitorDetails",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VisitorDetails_Users_UpdatedById",
                table: "VisitorDetails",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VisitorDetails_Visitors_VisitorId",
                table: "VisitorDetails",
                column: "VisitorId",
                principalTable: "Visitors",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RevolvingFundStatuses_Services_ServiceId",
                table: "RevolvingFundStatuses");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_OrganizationUnitLocations_UnitLocationId",
                table: "Services");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_Organizations_OrganizationId",
                table: "Services");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_Users_ApprovedById",
                table: "Services");

            migrationBuilder.DropForeignKey(
                name: "FK_TableConsultingAndSocialMediaServices_OrganizationUnitLocations_UnitLocationId",
                table: "TableConsultingAndSocialMediaServices");

            migrationBuilder.DropForeignKey(
                name: "FK_TableConsultingAndSocialMediaServices_Organizations_OrganizationId",
                table: "TableConsultingAndSocialMediaServices");

            migrationBuilder.DropForeignKey(
                name: "FK_TableConsultingAndSocialMediaServices_Users_ApprovedById",
                table: "TableConsultingAndSocialMediaServices");

            migrationBuilder.DropForeignKey(
                name: "FK_TableModeAndOutreaches_TableConsultingAndSocialMediaServices_ConsultingServiceId",
                table: "TableModeAndOutreaches");

            migrationBuilder.DropForeignKey(
                name: "FK_VisitorDetails_Services_TblServiceId",
                table: "VisitorDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_VisitorDetails_Users_CreatedById",
                table: "VisitorDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_VisitorDetails_Users_UpdatedById",
                table: "VisitorDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_VisitorDetails_Visitors_VisitorId",
                table: "VisitorDetails");

            migrationBuilder.DropIndex(
                name: "IX_TableModeAndOutreaches_ConsultingServiceId",
                table: "TableModeAndOutreaches");

            migrationBuilder.DropIndex(
                name: "IX_TableConsultingAndSocialMediaServices_ApprovedById",
                table: "TableConsultingAndSocialMediaServices");

            migrationBuilder.DropIndex(
                name: "IX_TableConsultingAndSocialMediaServices_OrganizationId",
                table: "TableConsultingAndSocialMediaServices");

            migrationBuilder.DropIndex(
                name: "IX_TableConsultingAndSocialMediaServices_UnitLocationId",
                table: "TableConsultingAndSocialMediaServices");

            migrationBuilder.DropIndex(
                name: "IX_Services_ApprovedById",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Services_OrganizationId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Services_UnitLocationId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_RevolvingFundStatuses_ServiceId",
                table: "RevolvingFundStatuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VisitorDetails",
                table: "VisitorDetails");

            migrationBuilder.DropIndex(
                name: "IX_VisitorDetails_VisitorId",
                table: "VisitorDetails");

            migrationBuilder.DropColumn(
                name: "SubmittedDate",
                table: "TableHostels");

            migrationBuilder.DropColumn(
                name: "ApprovedAt",
                table: "TableConsultingAndSocialMediaServices");

            migrationBuilder.DropColumn(
                name: "ApprovedById",
                table: "TableConsultingAndSocialMediaServices");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "TableConsultingAndSocialMediaServices");

            migrationBuilder.DropColumn(
                name: "FormStatus",
                table: "TableConsultingAndSocialMediaServices");

            migrationBuilder.DropColumn(
                name: "FormStatusRemarks",
                table: "TableConsultingAndSocialMediaServices");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "TableConsultingAndSocialMediaServices");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "TableConsultingAndSocialMediaServices");

            migrationBuilder.DropColumn(
                name: "UnitLocationId",
                table: "TableConsultingAndSocialMediaServices");

            migrationBuilder.DropColumn(
                name: "ApprovedAt",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "ApprovedById",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "FormStatus",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "FormStatusRemarks",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "UnitLocationId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "VisitorId",
                table: "VisitorDetails");

            migrationBuilder.RenameTable(
                name: "VisitorDetails",
                newName: "VisitorDetail");

            migrationBuilder.RenameIndex(
                name: "IX_VisitorDetails_UpdatedById",
                table: "VisitorDetail",
                newName: "IX_VisitorDetail_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_VisitorDetails_TblServiceId",
                table: "VisitorDetail",
                newName: "IX_VisitorDetail_TblServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_VisitorDetails_CreatedById",
                table: "VisitorDetail",
                newName: "IX_VisitorDetail_CreatedById");

            migrationBuilder.AddColumn<int>(
                name: "TableConsultingAndSocialMediaService",
                table: "TableModeAndOutreaches",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "Date",
                table: "TableHostels",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "AmountGenerated",
                table: "TableHostels",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<int>(
                name: "RelatedToId",
                table: "TableConsultingAndSocialMediaServices",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "TableConsultingAndSocialMediaServices",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Services",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "AmountGenerated",
                table: "Services",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<double>(
                name: "Receipt",
                table: "RevolvingFundStatuses",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<double>(
                name: "OpeningBalance",
                table: "RevolvingFundStatuses",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<double>(
                name: "Expenditure",
                table: "RevolvingFundStatuses",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<double>(
                name: "ClosingBalance",
                table: "RevolvingFundStatuses",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<int>(
                name: "TblServiceId",
                table: "RevolvingFundStatuses",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_VisitorDetail",
                table: "VisitorDetail",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_TableModeAndOutreaches_TableConsultingAndSocialMediaService",
                table: "TableModeAndOutreaches",
                column: "TableConsultingAndSocialMediaService");

            migrationBuilder.CreateIndex(
                name: "IX_RevolvingFundStatuses_TblServiceId",
                table: "RevolvingFundStatuses",
                column: "TblServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_RevolvingFundStatuses_Services_TblServiceId",
                table: "RevolvingFundStatuses",
                column: "TblServiceId",
                principalTable: "Services",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TableModeAndOutreaches_TableConsultingAndSocialMediaServices_TableConsultingAndSocialMediaService",
                table: "TableModeAndOutreaches",
                column: "TableConsultingAndSocialMediaService",
                principalTable: "TableConsultingAndSocialMediaServices",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VisitorDetail_Services_TblServiceId",
                table: "VisitorDetail",
                column: "TblServiceId",
                principalTable: "Services",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VisitorDetail_Users_CreatedById",
                table: "VisitorDetail",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VisitorDetail_Users_UpdatedById",
                table: "VisitorDetail",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFtiMultiTenancyAndApprovalFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Since FTI tables are empty (user confirmed), we can safely add columns

            // ========== ADD MULTI-TENANCY AND APPROVAL FIELDS TO FTIProgramDetails ==========

            // These fields are inherited from ReportEntryBaseEntity
            migrationBuilder.AddColumn<int>(
                name: "UnitLocationId",
                table: "FTIProgramDetails",
                type: "int",
                nullable: false);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "FTIProgramDetails",
                type: "int",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "Attachements",
                table: "FTIProgramDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FormStatus",
                table: "FTIProgramDetails",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "Draft");

            migrationBuilder.AddColumn<string>(
                name: "FormStatusRemarks",
                table: "FTIProgramDetails",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ApprovedAt",
                table: "FTIProgramDetails",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApprovedById",
                table: "FTIProgramDetails",
                type: "int",
                nullable: true);

            // Create indexes for FTIProgramDetails
            migrationBuilder.CreateIndex(
                name: "IX_FTIProgramDetails_UnitLocationId",
                table: "FTIProgramDetails",
                column: "UnitLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_FTIProgramDetails_OrganizationId",
                table: "FTIProgramDetails",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_FTIProgramDetails_ApprovedById",
                table: "FTIProgramDetails",
                column: "ApprovedById");

            // Create foreign keys for FTIProgramDetails
            migrationBuilder.AddForeignKey(
                name: "FK_FTIProgramDetails_OrganizationUnitLocations_UnitLocationId",
                table: "FTIProgramDetails",
                column: "UnitLocationId",
                principalTable: "OrganizationUnitLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FTIProgramDetails_Organizations_OrganizationId",
                table: "FTIProgramDetails",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_FTIProgramDetails_Users_ApprovedById",
                table: "FTIProgramDetails",
                column: "ApprovedById",
                principalTable: "Users",
                principalColumn: "Id");


            // ========== ADD MULTI-TENANCY FIELDS TO ALL FTI CHILD ENTITIES ==========

            // FTIProgramContentAndResources
            migrationBuilder.AddColumn<int>(
                name: "UnitLocationId",
                table: "FTIProgramContentAndResources",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "FTIProgramContentAndResources",
                type: "int",
                nullable: true);

            // FTIAdvisoryServices
            migrationBuilder.AddColumn<int>(
                name: "UnitLocationId",
                table: "FTIAdvisoryServices",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "FTIAdvisoryServices",
                type: "int",
                nullable: true);

            // FTIParticipantDemographics
            migrationBuilder.AddColumn<int>(
                name: "UnitLocationId",
                table: "FTIParticipantDemographics",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "FTIParticipantDemographics",
                type: "int",
                nullable: true);

            // FTIRecommendation (table might be singular or plural - migration will handle it)
            migrationBuilder.Sql(@"
                IF EXISTS (SELECT * FROM sys.tables WHERE name = 'FTIRecommendation')
                BEGIN
                    ALTER TABLE FTIRecommendation ADD UnitLocationId int NULL;
                    ALTER TABLE FTIRecommendation ADD OrganizationId int NULL;
                END
                ELSE IF EXISTS (SELECT * FROM sys.tables WHERE name = 'FTIRecommendations')
                BEGIN
                    ALTER TABLE FTIRecommendations ADD UnitLocationId int NULL;
                    ALTER TABLE FTIRecommendations ADD OrganizationId int NULL;
                END
            ");

            // FTIReport (table might be singular or plural - migration will handle it)
            migrationBuilder.Sql(@"
                IF EXISTS (SELECT * FROM sys.tables WHERE name = 'FTIReport')
                BEGIN
                    ALTER TABLE FTIReport ADD UnitLocationId int NULL;
                    ALTER TABLE FTIReport ADD OrganizationId int NULL;
                END
                ELSE IF EXISTS (SELECT * FROM sys.tables WHERE name = 'FTIReports')
                BEGIN
                    ALTER TABLE FTIReports ADD UnitLocationId int NULL;
                    ALTER TABLE FTIReports ADD OrganizationId int NULL;
                END
            ");

            // FTIResourcePerson (table might be singular or plural - migration will handle it)
            migrationBuilder.Sql(@"
                IF EXISTS (SELECT * FROM sys.tables WHERE name = 'FTIResourcePerson')
                BEGIN
                    ALTER TABLE FTIResourcePerson ADD UnitLocationId int NULL;
                    ALTER TABLE FTIResourcePerson ADD OrganizationId int NULL;
                END
                ELSE IF EXISTS (SELECT * FROM sys.tables WHERE name = 'FTIResourcePersons')
                BEGIN
                    ALTER TABLE FTIResourcePersons ADD UnitLocationId int NULL;
                    ALTER TABLE FTIResourcePersons ADD OrganizationId int NULL;
                END
            ");

            // FTITeachingAIdsDeveloped or FTITeachingAidsDeveloped (handle both naming conventions)
            migrationBuilder.Sql(@"
                IF EXISTS (SELECT * FROM sys.tables WHERE name = 'FTITeachingAIdsDeveloped')
                BEGIN
                    ALTER TABLE FTITeachingAIdsDeveloped ADD UnitLocationId int NULL;
                    ALTER TABLE FTITeachingAIdsDeveloped ADD OrganizationId int NULL;
                END
                ELSE IF EXISTS (SELECT * FROM sys.tables WHERE name = 'FTITeachingAidsDeveloped')
                BEGIN
                    ALTER TABLE FTITeachingAidsDeveloped ADD UnitLocationId int NULL;
                    ALTER TABLE FTITeachingAidsDeveloped ADD OrganizationId int NULL;
                END
            ");

            // FTITopicsCoveredInClass
            migrationBuilder.AddColumn<int>(
                name: "UnitLocationId",
                table: "FTITopicsCoveredInClass",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "FTITopicsCoveredInClass",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop foreign keys
            migrationBuilder.DropForeignKey(
                name: "FK_FTIProgramDetails_OrganizationUnitLocations_UnitLocationId",
                table: "FTIProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_FTIProgramDetails_Organizations_OrganizationId",
                table: "FTIProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_FTIProgramDetails_Users_ApprovedById",
                table: "FTIProgramDetails");

            // Drop indexes
            migrationBuilder.DropIndex(
                name: "IX_FTIProgramDetails_UnitLocationId",
                table: "FTIProgramDetails");

            migrationBuilder.DropIndex(
                name: "IX_FTIProgramDetails_OrganizationId",
                table: "FTIProgramDetails");

            migrationBuilder.DropIndex(
                name: "IX_FTIProgramDetails_ApprovedById",
                table: "FTIProgramDetails");

            // Drop columns from FTIProgramDetails
            migrationBuilder.DropColumn(
                name: "UnitLocationId",
                table: "FTIProgramDetails");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "FTIProgramDetails");

            migrationBuilder.DropColumn(
                name: "Attachements",
                table: "FTIProgramDetails");

            migrationBuilder.DropColumn(
                name: "FormStatus",
                table: "FTIProgramDetails");

            migrationBuilder.DropColumn(
                name: "FormStatusRemarks",
                table: "FTIProgramDetails");

            migrationBuilder.DropColumn(
                name: "ApprovedAt",
                table: "FTIProgramDetails");

            migrationBuilder.DropColumn(
                name: "ApprovedById",
                table: "FTIProgramDetails");

            // Drop columns from child entities
            migrationBuilder.DropColumn(
                name: "UnitLocationId",
                table: "FTIProgramContentAndResources");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "FTIProgramContentAndResources");

            migrationBuilder.DropColumn(
                name: "UnitLocationId",
                table: "FTIAdvisoryServices");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "FTIAdvisoryServices");

            migrationBuilder.DropColumn(
                name: "UnitLocationId",
                table: "FTIParticipantDemographics");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "FTIParticipantDemographics");

            migrationBuilder.DropColumn(
                name: "UnitLocationId",
                table: "FTITopicsCoveredInClass");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "FTITopicsCoveredInClass");

            // Drop columns from tables that might have different names
            migrationBuilder.Sql(@"
                IF EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('FTIRecommendation') AND name = 'UnitLocationId')
                    ALTER TABLE FTIRecommendation DROP COLUMN UnitLocationId;
                IF EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('FTIRecommendation') AND name = 'OrganizationId')
                    ALTER TABLE FTIRecommendation DROP COLUMN OrganizationId;

                IF EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('FTIRecommendations') AND name = 'UnitLocationId')
                    ALTER TABLE FTIRecommendations DROP COLUMN UnitLocationId;
                IF EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('FTIRecommendations') AND name = 'OrganizationId')
                    ALTER TABLE FTIRecommendations DROP COLUMN OrganizationId;

                IF EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('FTIReport') AND name = 'UnitLocationId')
                    ALTER TABLE FTIReport DROP COLUMN UnitLocationId;
                IF EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('FTIReport') AND name = 'OrganizationId')
                    ALTER TABLE FTIReport DROP COLUMN OrganizationId;

                IF EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('FTIReports') AND name = 'UnitLocationId')
                    ALTER TABLE FTIReports DROP COLUMN UnitLocationId;
                IF EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('FTIReports') AND name = 'OrganizationId')
                    ALTER TABLE FTIReports DROP COLUMN OrganizationId;

                IF EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('FTIResourcePerson') AND name = 'UnitLocationId')
                    ALTER TABLE FTIResourcePerson DROP COLUMN UnitLocationId;
                IF EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('FTIResourcePerson') AND name = 'OrganizationId')
                    ALTER TABLE FTIResourcePerson DROP COLUMN OrganizationId;

                IF EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('FTIResourcePersons') AND name = 'UnitLocationId')
                    ALTER TABLE FTIResourcePersons DROP COLUMN UnitLocationId;
                IF EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('FTIResourcePersons') AND name = 'OrganizationId')
                    ALTER TABLE FTIResourcePersons DROP COLUMN OrganizationId;

                IF EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('FTITeachingAIdsDeveloped') AND name = 'UnitLocationId')
                    ALTER TABLE FTITeachingAIdsDeveloped DROP COLUMN UnitLocationId;
                IF EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('FTITeachingAIdsDeveloped') AND name = 'OrganizationId')
                    ALTER TABLE FTITeachingAIdsDeveloped DROP COLUMN OrganizationId;

                IF EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('FTITeachingAidsDeveloped') AND name = 'UnitLocationId')
                    ALTER TABLE FTITeachingAidsDeveloped DROP COLUMN UnitLocationId;
                IF EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('FTITeachingAidsDeveloped') AND name = 'OrganizationId')
                    ALTER TABLE FTITeachingAidsDeveloped DROP COLUMN OrganizationId;
            ");
        }
    }
}

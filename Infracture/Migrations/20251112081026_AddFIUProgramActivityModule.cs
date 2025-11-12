using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFIUProgramActivityModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FiuProgrammes");

            migrationBuilder.DropTable(
                name: "FiuProgrammeTypes");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "FIUProgramActivities");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "FIUProgramActivities");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "FIUActivities");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "FIUActivities");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "FIUActivities");

            migrationBuilder.AlterColumn<string>(
                name: "UploadMediaUrl",
                table: "FIUProgramActivities",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Number",
                table: "FIUProgramActivities",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FIUActivitiesId",
                table: "FIUProgramActivities",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "FIUProgramActivities",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "SubmittedAt",
                table: "FIUProgramActivities",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ActivityCategory",
                table: "FIUActivities",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ActivityDescription",
                table: "FIUActivities",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ActivityName",
                table: "FIUActivities",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "DisplayOrder",
                table: "FIUActivities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "FIUActivities",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "RequiresMediaUpload",
                table: "FIUActivities",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UnitOfMeasurement",
                table: "FIUActivities",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FIUProgramActivities_CreatedAt",
                table: "FIUProgramActivities",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_FIUProgramActivities_FormStatus",
                table: "FIUProgramActivities",
                column: "FormStatus");

            migrationBuilder.CreateIndex(
                name: "IX_FIUActivities_DisplayOrder",
                table: "FIUActivities",
                column: "DisplayOrder");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FIUProgramActivities_CreatedAt",
                table: "FIUProgramActivities");

            migrationBuilder.DropIndex(
                name: "IX_FIUProgramActivities_FormStatus",
                table: "FIUProgramActivities");

            migrationBuilder.DropIndex(
                name: "IX_FIUActivities_DisplayOrder",
                table: "FIUActivities");

            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "FIUProgramActivities");

            migrationBuilder.DropColumn(
                name: "SubmittedAt",
                table: "FIUProgramActivities");

            migrationBuilder.DropColumn(
                name: "ActivityCategory",
                table: "FIUActivities");

            migrationBuilder.DropColumn(
                name: "ActivityDescription",
                table: "FIUActivities");

            migrationBuilder.DropColumn(
                name: "ActivityName",
                table: "FIUActivities");

            migrationBuilder.DropColumn(
                name: "DisplayOrder",
                table: "FIUActivities");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "FIUActivities");

            migrationBuilder.DropColumn(
                name: "RequiresMediaUpload",
                table: "FIUActivities");

            migrationBuilder.DropColumn(
                name: "UnitOfMeasurement",
                table: "FIUActivities");

            migrationBuilder.AlterColumn<string>(
                name: "UploadMediaUrl",
                table: "FIUProgramActivities",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Number",
                table: "FIUProgramActivities",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "FIUActivitiesId",
                table: "FIUProgramActivities",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<DateOnly>(
                name: "EndDate",
                table: "FIUProgramActivities",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "StartDate",
                table: "FIUProgramActivities",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "FIUActivities",
                type: "datetimeoffset",
                nullable: false,
                defaultValueSql: "SYSUTCDATETIME()");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "FIUActivities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "FIUActivities",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FiuProgrammeTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FiuProgrammeTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FiuProgrammeTypes_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FiuProgrammeTypes_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FiuProgrammes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApprovedById = table.Column<int>(type: "int", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    OrganizationId = table.Column<int>(type: "int", nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    UnitLocationId = table.Column<int>(type: "int", nullable: false),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    ApprovedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Attachements = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Count = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    FormStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FormStatusRemarks = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsOther = table.Column<bool>(type: "bit", nullable: false),
                    OtherName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FiuProgrammes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FiuProgrammes_FiuProgrammeTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "FiuProgrammeTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FiuProgrammes_OrganizationUnitLocations_UnitLocationId",
                        column: x => x.UnitLocationId,
                        principalTable: "OrganizationUnitLocations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FiuProgrammes_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FiuProgrammes_Users_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FiuProgrammes_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FiuProgrammes_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FiuProgrammes_ApprovedById",
                table: "FiuProgrammes",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_FiuProgrammes_CreatedById",
                table: "FiuProgrammes",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FiuProgrammes_OrganizationId",
                table: "FiuProgrammes",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_FiuProgrammes_TypeId",
                table: "FiuProgrammes",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_FiuProgrammes_UnitLocationId",
                table: "FiuProgrammes",
                column: "UnitLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_FiuProgrammes_UpdatedById",
                table: "FiuProgrammes",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FiuProgrammeTypes_CreatedById",
                table: "FiuProgrammeTypes",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FiuProgrammeTypes_UpdatedById",
                table: "FiuProgrammeTypes",
                column: "UpdatedById");
        }
    }
}

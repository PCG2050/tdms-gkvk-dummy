using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FTI_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReportRecord",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReportStartTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ReportEndTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportRecord", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportRecord_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReportRecord_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Unit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentUnitId = table.Column<int>(type: "int", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Unit_Unit_ParentUnitId",
                        column: x => x.ParentUnitId,
                        principalTable: "Unit",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Unit_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Unit_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FtiOtherActivities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActivityDetails = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ReportRecordId = table.Column<int>(type: "int", nullable: false),
                    UnitId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FtiOtherActivities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FtiOtherActivities_ReportRecord_ReportRecordId",
                        column: x => x.ReportRecordId,
                        principalTable: "ReportRecord",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FtiOtherActivities_Unit_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Unit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FtiOtherActivities_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FtiOtherActivities_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FtiTrainingPrograms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrganisationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrainingTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: false),
                    TrainingCount = table.Column<int>(type: "int", nullable: false),
                    ParticipantCount = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ReportRecordId = table.Column<int>(type: "int", nullable: false),
                    UnitId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FtiTrainingPrograms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FtiTrainingPrograms_ReportRecord_ReportRecordId",
                        column: x => x.ReportRecordId,
                        principalTable: "ReportRecord",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FtiTrainingPrograms_Unit_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Unit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FtiTrainingPrograms_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FtiTrainingPrograms_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FtiOtherActivities_CreatedById",
                table: "FtiOtherActivities",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FtiOtherActivities_ReportRecordId",
                table: "FtiOtherActivities",
                column: "ReportRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_FtiOtherActivities_UnitId",
                table: "FtiOtherActivities",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_FtiOtherActivities_UpdatedById",
                table: "FtiOtherActivities",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FtiTrainingPrograms_CreatedById",
                table: "FtiTrainingPrograms",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FtiTrainingPrograms_ReportRecordId",
                table: "FtiTrainingPrograms",
                column: "ReportRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_FtiTrainingPrograms_UnitId",
                table: "FtiTrainingPrograms",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_FtiTrainingPrograms_UpdatedById",
                table: "FtiTrainingPrograms",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ReportRecord_CreatedById",
                table: "ReportRecord",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ReportRecord_UpdatedById",
                table: "ReportRecord",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Unit_CreatedById",
                table: "Unit",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Unit_ParentUnitId",
                table: "Unit",
                column: "ParentUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Unit_UpdatedById",
                table: "Unit",
                column: "UpdatedById");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FtiOtherActivities");

            migrationBuilder.DropTable(
                name: "FtiTrainingPrograms");

            migrationBuilder.DropTable(
                name: "ReportRecord");

            migrationBuilder.DropTable(
                name: "Unit");
        }
    }
}

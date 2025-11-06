using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class STU_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StuOtherActivities",
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
                    table.PrimaryKey("PK_StuOtherActivities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StuOtherActivities_ReportRecord_ReportRecordId",
                        column: x => x.ReportRecordId,
                        principalTable: "ReportRecord",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StuOtherActivities_Unit_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Unit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StuOtherActivities_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StuOtherActivities_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StuSponseredTrainingProgrammes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Organization = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrainingTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Place = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_StuSponseredTrainingProgrammes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StuSponseredTrainingProgrammes_ReportRecord_ReportRecordId",
                        column: x => x.ReportRecordId,
                        principalTable: "ReportRecord",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StuSponseredTrainingProgrammes_Unit_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Unit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StuSponseredTrainingProgrammes_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StuSponseredTrainingProgrammes_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StuTrainingProgrammes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainingTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Place = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_StuTrainingProgrammes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StuTrainingProgrammes_ReportRecord_ReportRecordId",
                        column: x => x.ReportRecordId,
                        principalTable: "ReportRecord",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StuTrainingProgrammes_Unit_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Unit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StuTrainingProgrammes_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StuTrainingProgrammes_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_StuOtherActivities_CreatedById",
                table: "StuOtherActivities",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_StuOtherActivities_ReportRecordId",
                table: "StuOtherActivities",
                column: "ReportRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_StuOtherActivities_UnitId",
                table: "StuOtherActivities",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_StuOtherActivities_UpdatedById",
                table: "StuOtherActivities",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_StuSponseredTrainingProgrammes_CreatedById",
                table: "StuSponseredTrainingProgrammes",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_StuSponseredTrainingProgrammes_ReportRecordId",
                table: "StuSponseredTrainingProgrammes",
                column: "ReportRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_StuSponseredTrainingProgrammes_UnitId",
                table: "StuSponseredTrainingProgrammes",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_StuSponseredTrainingProgrammes_UpdatedById",
                table: "StuSponseredTrainingProgrammes",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_StuTrainingProgrammes_CreatedById",
                table: "StuTrainingProgrammes",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_StuTrainingProgrammes_ReportRecordId",
                table: "StuTrainingProgrammes",
                column: "ReportRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_StuTrainingProgrammes_UnitId",
                table: "StuTrainingProgrammes",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_StuTrainingProgrammes_UpdatedById",
                table: "StuTrainingProgrammes",
                column: "UpdatedById");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StuOtherActivities");

            migrationBuilder.DropTable(
                name: "StuSponseredTrainingProgrammes");

            migrationBuilder.DropTable(
                name: "StuTrainingProgrammes");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class IBTVA_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IbtavOtherActivities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActicityDetails = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ReportRecordId = table.Column<int>(type: "int", nullable: false),
                    UnitId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IbtavOtherActivities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IbtavOtherActivities_ReportRecord_ReportRecordId",
                        column: x => x.ReportRecordId,
                        principalTable: "ReportRecord",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IbtavOtherActivities_Unit_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Unit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IbtavOtherActivities_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_IbtavOtherActivities_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "IbtvaProgrammes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainingTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: false),
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
                    table.PrimaryKey("PK_IbtvaProgrammes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IbtvaProgrammes_ReportRecord_ReportRecordId",
                        column: x => x.ReportRecordId,
                        principalTable: "ReportRecord",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IbtvaProgrammes_Unit_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Unit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IbtvaProgrammes_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_IbtvaProgrammes_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_IbtavOtherActivities_CreatedById",
                table: "IbtavOtherActivities",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_IbtavOtherActivities_ReportRecordId",
                table: "IbtavOtherActivities",
                column: "ReportRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_IbtavOtherActivities_UnitId",
                table: "IbtavOtherActivities",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_IbtavOtherActivities_UpdatedById",
                table: "IbtavOtherActivities",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_IbtvaProgrammes_CreatedById",
                table: "IbtvaProgrammes",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_IbtvaProgrammes_ReportRecordId",
                table: "IbtvaProgrammes",
                column: "ReportRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_IbtvaProgrammes_UnitId",
                table: "IbtvaProgrammes",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_IbtvaProgrammes_UpdatedById",
                table: "IbtvaProgrammes",
                column: "UpdatedById");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IbtavOtherActivities");

            migrationBuilder.DropTable(
                name: "IbtvaProgrammes");
        }
    }
}

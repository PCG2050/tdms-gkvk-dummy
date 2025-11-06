using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class STU_DAESI_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DaesiOtherActivities",
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
                    table.PrimaryKey("PK_DaesiOtherActivities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DaesiOtherActivities_ReportRecord_ReportRecordId",
                        column: x => x.ReportRecordId,
                        principalTable: "ReportRecord",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DaesiOtherActivities_Unit_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Unit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DaesiOtherActivities_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DaesiOtherActivities_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DaesiProgrammes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Place = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NodalTrainingInstitute = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BatchCount = table.Column<int>(type: "int", nullable: false),
                    DealerCount = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ReportRecordId = table.Column<int>(type: "int", nullable: false),
                    UnitId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DaesiProgrammes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DaesiProgrammes_ReportRecord_ReportRecordId",
                        column: x => x.ReportRecordId,
                        principalTable: "ReportRecord",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DaesiProgrammes_Unit_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Unit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DaesiProgrammes_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DaesiProgrammes_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DaesiOtherActivities_CreatedById",
                table: "DaesiOtherActivities",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_DaesiOtherActivities_ReportRecordId",
                table: "DaesiOtherActivities",
                column: "ReportRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_DaesiOtherActivities_UnitId",
                table: "DaesiOtherActivities",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_DaesiOtherActivities_UpdatedById",
                table: "DaesiOtherActivities",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_DaesiProgrammes_CreatedById",
                table: "DaesiProgrammes",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_DaesiProgrammes_ReportRecordId",
                table: "DaesiProgrammes",
                column: "ReportRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_DaesiProgrammes_UnitId",
                table: "DaesiProgrammes",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_DaesiProgrammes_UpdatedById",
                table: "DaesiProgrammes",
                column: "UpdatedById");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DaesiOtherActivities");

            migrationBuilder.DropTable(
                name: "DaesiProgrammes");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EEU_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EeuFLDs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Crop = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Area = table.Column<double>(type: "float", nullable: false),
                    TrialMaleScStCount = table.Column<int>(type: "int", nullable: false),
                    TrialMaleGenCount = table.Column<int>(type: "int", nullable: false),
                    TrialFemaleScStCount = table.Column<int>(type: "int", nullable: false),
                    TrailFemalGenCount = table.Column<int>(type: "int", nullable: false),
                    YieldDemo = table.Column<double>(type: "float", nullable: false),
                    YieldCheck = table.Column<double>(type: "float", nullable: false),
                    PercentIncreaseInYield = table.Column<double>(type: "float", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ReportRecordId = table.Column<int>(type: "int", nullable: false),
                    UnitId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EeuFLDs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EeuFLDs_ReportRecord_ReportRecordId",
                        column: x => x.ReportRecordId,
                        principalTable: "ReportRecord",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EeuFLDs_Unit_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Unit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EeuFLDs_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EeuFLDs_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EeuOFTs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Crop = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Area = table.Column<double>(type: "float", nullable: false),
                    TrialMaleScStCount = table.Column<int>(type: "int", nullable: false),
                    TrialMaleGenCount = table.Column<int>(type: "int", nullable: false),
                    TrialFemaleScStCount = table.Column<int>(type: "int", nullable: false),
                    TrailFemalGenCount = table.Column<int>(type: "int", nullable: false),
                    YieldT1 = table.Column<double>(type: "float", nullable: false),
                    YieldT2 = table.Column<double>(type: "float", nullable: false),
                    PercentIncreaseInYield = table.Column<double>(type: "float", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ReportRecordId = table.Column<int>(type: "int", nullable: false),
                    UnitId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EeuOFTs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EeuOFTs_ReportRecord_ReportRecordId",
                        column: x => x.ReportRecordId,
                        principalTable: "ReportRecord",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EeuOFTs_Unit_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Unit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EeuOFTs_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EeuOFTs_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EeuOtherActivities",
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
                    table.PrimaryKey("PK_EeuOtherActivities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EeuOtherActivities_ReportRecord_ReportRecordId",
                        column: x => x.ReportRecordId,
                        principalTable: "ReportRecord",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EeuOtherActivities_Unit_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Unit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EeuOtherActivities_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EeuOtherActivities_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EeuTrainingProgrammes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
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
                    table.PrimaryKey("PK_EeuTrainingProgrammes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EeuTrainingProgrammes_ReportRecord_ReportRecordId",
                        column: x => x.ReportRecordId,
                        principalTable: "ReportRecord",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EeuTrainingProgrammes_Unit_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Unit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EeuTrainingProgrammes_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EeuTrainingProgrammes_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EeuFLDs_CreatedById",
                table: "EeuFLDs",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EeuFLDs_ReportRecordId",
                table: "EeuFLDs",
                column: "ReportRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_EeuFLDs_UnitId",
                table: "EeuFLDs",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_EeuFLDs_UpdatedById",
                table: "EeuFLDs",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EeuOFTs_CreatedById",
                table: "EeuOFTs",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EeuOFTs_ReportRecordId",
                table: "EeuOFTs",
                column: "ReportRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_EeuOFTs_UnitId",
                table: "EeuOFTs",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_EeuOFTs_UpdatedById",
                table: "EeuOFTs",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EeuOtherActivities_CreatedById",
                table: "EeuOtherActivities",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EeuOtherActivities_ReportRecordId",
                table: "EeuOtherActivities",
                column: "ReportRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_EeuOtherActivities_UnitId",
                table: "EeuOtherActivities",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_EeuOtherActivities_UpdatedById",
                table: "EeuOtherActivities",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EeuTrainingProgrammes_CreatedById",
                table: "EeuTrainingProgrammes",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EeuTrainingProgrammes_ReportRecordId",
                table: "EeuTrainingProgrammes",
                column: "ReportRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_EeuTrainingProgrammes_UnitId",
                table: "EeuTrainingProgrammes",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_EeuTrainingProgrammes_UpdatedById",
                table: "EeuTrainingProgrammes",
                column: "UpdatedById");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EeuFLDs");

            migrationBuilder.DropTable(
                name: "EeuOFTs");

            migrationBuilder.DropTable(
                name: "EeuOtherActivities");

            migrationBuilder.DropTable(
                name: "EeuTrainingProgrammes");
        }
    }
}

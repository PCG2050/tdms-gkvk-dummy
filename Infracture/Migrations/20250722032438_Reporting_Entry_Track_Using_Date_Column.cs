using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Reporting_Entry_Track_Using_Date_Column : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AsmVisits_ReportRecord_ReportRecordId",
                table: "AsmVisits");

            migrationBuilder.DropForeignKey(
                name: "FK_AticAdvisoryServices_ReportRecord_ReportRecordId",
                table: "AticAdvisoryServices");

            migrationBuilder.DropForeignKey(
                name: "FK_AticOtherActivities_ReportRecord_ReportRecordId",
                table: "AticOtherActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_AticSales_ReportRecord_ReportRecordId",
                table: "AticSales");

            migrationBuilder.DropForeignKey(
                name: "FK_DaesiOtherActivities_ReportRecord_ReportRecordId",
                table: "DaesiOtherActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_DaesiProgrammes_ReportRecord_ReportRecordId",
                table: "DaesiProgrammes");

            migrationBuilder.DropForeignKey(
                name: "FK_DeuCourses_ReportRecord_ReportRecordId",
                table: "DeuCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_DeuOtherActivities_ReportRecord_ReportRecordId",
                table: "DeuOtherActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_EeuFLDs_ReportRecord_ReportRecordId",
                table: "EeuFLDs");

            migrationBuilder.DropForeignKey(
                name: "FK_EeuOFTs_ReportRecord_ReportRecordId",
                table: "EeuOFTs");

            migrationBuilder.DropForeignKey(
                name: "FK_EeuOtherActivities_ReportRecord_ReportRecordId",
                table: "EeuOtherActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_EeuTrainingProgrammes_ReportRecord_ReportRecordId",
                table: "EeuTrainingProgrammes");

            migrationBuilder.DropForeignKey(
                name: "FK_FiuProgrammes_ReportRecord_ReportRecordId",
                table: "FiuProgrammes");

            migrationBuilder.DropForeignKey(
                name: "FK_FtiOtherActivities_ReportRecord_ReportRecordId",
                table: "FtiOtherActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_FtiTrainingPrograms_ReportRecord_ReportRecordId",
                table: "FtiTrainingPrograms");

            migrationBuilder.DropForeignKey(
                name: "FK_IbtavOtherActivities_ReportRecord_ReportRecordId",
                table: "IbtavOtherActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_IbtvaProgrammes_ReportRecord_ReportRecordId",
                table: "IbtvaProgrammes");

            migrationBuilder.DropForeignKey(
                name: "FK_StuOtherActivities_ReportRecord_ReportRecordId",
                table: "StuOtherActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_StuSponseredTrainingProgrammes_ReportRecord_ReportRecordId",
                table: "StuSponseredTrainingProgrammes");

            migrationBuilder.DropForeignKey(
                name: "FK_StuTrainingProgrammes_ReportRecord_ReportRecordId",
                table: "StuTrainingProgrammes");

            migrationBuilder.DropTable(
                name: "ReportRecord");

            migrationBuilder.DropIndex(
                name: "IX_StuTrainingProgrammes_ReportRecordId",
                table: "StuTrainingProgrammes");

            migrationBuilder.DropIndex(
                name: "IX_StuSponseredTrainingProgrammes_ReportRecordId",
                table: "StuSponseredTrainingProgrammes");

            migrationBuilder.DropIndex(
                name: "IX_StuOtherActivities_ReportRecordId",
                table: "StuOtherActivities");

            migrationBuilder.DropIndex(
                name: "IX_IbtvaProgrammes_ReportRecordId",
                table: "IbtvaProgrammes");

            migrationBuilder.DropIndex(
                name: "IX_IbtavOtherActivities_ReportRecordId",
                table: "IbtavOtherActivities");

            migrationBuilder.DropIndex(
                name: "IX_FtiTrainingPrograms_ReportRecordId",
                table: "FtiTrainingPrograms");

            migrationBuilder.DropIndex(
                name: "IX_FtiOtherActivities_ReportRecordId",
                table: "FtiOtherActivities");

            migrationBuilder.DropIndex(
                name: "IX_FiuProgrammes_ReportRecordId",
                table: "FiuProgrammes");

            migrationBuilder.DropIndex(
                name: "IX_EeuTrainingProgrammes_ReportRecordId",
                table: "EeuTrainingProgrammes");

            migrationBuilder.DropIndex(
                name: "IX_EeuOtherActivities_ReportRecordId",
                table: "EeuOtherActivities");

            migrationBuilder.DropIndex(
                name: "IX_EeuOFTs_ReportRecordId",
                table: "EeuOFTs");

            migrationBuilder.DropIndex(
                name: "IX_EeuFLDs_ReportRecordId",
                table: "EeuFLDs");

            migrationBuilder.DropIndex(
                name: "IX_DeuOtherActivities_ReportRecordId",
                table: "DeuOtherActivities");

            migrationBuilder.DropIndex(
                name: "IX_DeuCourses_ReportRecordId",
                table: "DeuCourses");

            migrationBuilder.DropIndex(
                name: "IX_DaesiProgrammes_ReportRecordId",
                table: "DaesiProgrammes");

            migrationBuilder.DropIndex(
                name: "IX_DaesiOtherActivities_ReportRecordId",
                table: "DaesiOtherActivities");

            migrationBuilder.DropIndex(
                name: "IX_AticSales_ReportRecordId",
                table: "AticSales");

            migrationBuilder.DropIndex(
                name: "IX_AticOtherActivities_ReportRecordId",
                table: "AticOtherActivities");

            migrationBuilder.DropIndex(
                name: "IX_AticAdvisoryServices_ReportRecordId",
                table: "AticAdvisoryServices");

            migrationBuilder.DropIndex(
                name: "IX_AsmVisits_ReportRecordId",
                table: "AsmVisits");

            migrationBuilder.DropColumn(
                name: "ReportRecordId",
                table: "StuTrainingProgrammes");

            migrationBuilder.DropColumn(
                name: "ReportRecordId",
                table: "StuSponseredTrainingProgrammes");

            migrationBuilder.DropColumn(
                name: "ReportRecordId",
                table: "StuOtherActivities");

            migrationBuilder.DropColumn(
                name: "ReportRecordId",
                table: "IbtvaProgrammes");

            migrationBuilder.DropColumn(
                name: "ReportRecordId",
                table: "IbtavOtherActivities");

            migrationBuilder.DropColumn(
                name: "ReportRecordId",
                table: "FtiTrainingPrograms");

            migrationBuilder.DropColumn(
                name: "ReportRecordId",
                table: "FtiOtherActivities");

            migrationBuilder.DropColumn(
                name: "ReportRecordId",
                table: "FiuProgrammes");

            migrationBuilder.DropColumn(
                name: "ReportRecordId",
                table: "EeuTrainingProgrammes");

            migrationBuilder.DropColumn(
                name: "ReportRecordId",
                table: "EeuOtherActivities");

            migrationBuilder.DropColumn(
                name: "ReportRecordId",
                table: "EeuOFTs");

            migrationBuilder.DropColumn(
                name: "ReportRecordId",
                table: "EeuFLDs");

            migrationBuilder.DropColumn(
                name: "ReportRecordId",
                table: "DeuOtherActivities");

            migrationBuilder.DropColumn(
                name: "ReportRecordId",
                table: "DeuCourses");

            migrationBuilder.DropColumn(
                name: "ReportRecordId",
                table: "DaesiProgrammes");

            migrationBuilder.DropColumn(
                name: "ReportRecordId",
                table: "DaesiOtherActivities");

            migrationBuilder.DropColumn(
                name: "ReportRecordId",
                table: "AticSales");

            migrationBuilder.DropColumn(
                name: "ReportRecordId",
                table: "AticOtherActivities");

            migrationBuilder.DropColumn(
                name: "ReportRecordId",
                table: "AticAdvisoryServices");

            migrationBuilder.DropColumn(
                name: "ReportRecordId",
                table: "AsmVisits");

            migrationBuilder.AddColumn<DateOnly>(
                name: "Date",
                table: "StuOtherActivities",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<DateOnly>(
                name: "Date",
                table: "IbtavOtherActivities",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<DateOnly>(
                name: "Date",
                table: "FtiOtherActivities",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<DateOnly>(
                name: "Date",
                table: "FiuProgrammes",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<DateOnly>(
                name: "Date",
                table: "EeuOtherActivities",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<DateOnly>(
                name: "Date",
                table: "EeuOFTs",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<DateOnly>(
                name: "Date",
                table: "EeuFLDs",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<DateOnly>(
                name: "Date",
                table: "DeuOtherActivities",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<DateOnly>(
                name: "Date",
                table: "DeuCourses",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<DateOnly>(
                name: "Date",
                table: "DaesiProgrammes",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<DateOnly>(
                name: "Date",
                table: "DaesiOtherActivities",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<DateOnly>(
                name: "Date",
                table: "AticSales",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<DateOnly>(
                name: "Date",
                table: "AticOtherActivities",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<DateOnly>(
                name: "Date",
                table: "AticAdvisoryServices",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "StuOtherActivities");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "IbtavOtherActivities");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "FtiOtherActivities");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "FiuProgrammes");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "EeuOtherActivities");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "EeuOFTs");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "EeuFLDs");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "DeuOtherActivities");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "DeuCourses");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "DaesiProgrammes");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "DaesiOtherActivities");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "AticSales");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "AticOtherActivities");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "AticAdvisoryServices");

            migrationBuilder.AddColumn<int>(
                name: "ReportRecordId",
                table: "StuTrainingProgrammes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReportRecordId",
                table: "StuSponseredTrainingProgrammes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReportRecordId",
                table: "StuOtherActivities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReportRecordId",
                table: "IbtvaProgrammes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReportRecordId",
                table: "IbtavOtherActivities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReportRecordId",
                table: "FtiTrainingPrograms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReportRecordId",
                table: "FtiOtherActivities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReportRecordId",
                table: "FiuProgrammes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReportRecordId",
                table: "EeuTrainingProgrammes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReportRecordId",
                table: "EeuOtherActivities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReportRecordId",
                table: "EeuOFTs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReportRecordId",
                table: "EeuFLDs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReportRecordId",
                table: "DeuOtherActivities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReportRecordId",
                table: "DeuCourses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReportRecordId",
                table: "DaesiProgrammes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReportRecordId",
                table: "DaesiOtherActivities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReportRecordId",
                table: "AticSales",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReportRecordId",
                table: "AticOtherActivities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReportRecordId",
                table: "AticAdvisoryServices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReportRecordId",
                table: "AsmVisits",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ReportRecord",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    ReportEndTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ReportStartTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
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

            migrationBuilder.CreateIndex(
                name: "IX_StuTrainingProgrammes_ReportRecordId",
                table: "StuTrainingProgrammes",
                column: "ReportRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_StuSponseredTrainingProgrammes_ReportRecordId",
                table: "StuSponseredTrainingProgrammes",
                column: "ReportRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_StuOtherActivities_ReportRecordId",
                table: "StuOtherActivities",
                column: "ReportRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_IbtvaProgrammes_ReportRecordId",
                table: "IbtvaProgrammes",
                column: "ReportRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_IbtavOtherActivities_ReportRecordId",
                table: "IbtavOtherActivities",
                column: "ReportRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_FtiTrainingPrograms_ReportRecordId",
                table: "FtiTrainingPrograms",
                column: "ReportRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_FtiOtherActivities_ReportRecordId",
                table: "FtiOtherActivities",
                column: "ReportRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_FiuProgrammes_ReportRecordId",
                table: "FiuProgrammes",
                column: "ReportRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_EeuTrainingProgrammes_ReportRecordId",
                table: "EeuTrainingProgrammes",
                column: "ReportRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_EeuOtherActivities_ReportRecordId",
                table: "EeuOtherActivities",
                column: "ReportRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_EeuOFTs_ReportRecordId",
                table: "EeuOFTs",
                column: "ReportRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_EeuFLDs_ReportRecordId",
                table: "EeuFLDs",
                column: "ReportRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_DeuOtherActivities_ReportRecordId",
                table: "DeuOtherActivities",
                column: "ReportRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_DeuCourses_ReportRecordId",
                table: "DeuCourses",
                column: "ReportRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_DaesiProgrammes_ReportRecordId",
                table: "DaesiProgrammes",
                column: "ReportRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_DaesiOtherActivities_ReportRecordId",
                table: "DaesiOtherActivities",
                column: "ReportRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_AticSales_ReportRecordId",
                table: "AticSales",
                column: "ReportRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_AticOtherActivities_ReportRecordId",
                table: "AticOtherActivities",
                column: "ReportRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_AticAdvisoryServices_ReportRecordId",
                table: "AticAdvisoryServices",
                column: "ReportRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_AsmVisits_ReportRecordId",
                table: "AsmVisits",
                column: "ReportRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportRecord_CreatedById",
                table: "ReportRecord",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ReportRecord_UpdatedById",
                table: "ReportRecord",
                column: "UpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_AsmVisits_ReportRecord_ReportRecordId",
                table: "AsmVisits",
                column: "ReportRecordId",
                principalTable: "ReportRecord",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AticAdvisoryServices_ReportRecord_ReportRecordId",
                table: "AticAdvisoryServices",
                column: "ReportRecordId",
                principalTable: "ReportRecord",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AticOtherActivities_ReportRecord_ReportRecordId",
                table: "AticOtherActivities",
                column: "ReportRecordId",
                principalTable: "ReportRecord",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AticSales_ReportRecord_ReportRecordId",
                table: "AticSales",
                column: "ReportRecordId",
                principalTable: "ReportRecord",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DaesiOtherActivities_ReportRecord_ReportRecordId",
                table: "DaesiOtherActivities",
                column: "ReportRecordId",
                principalTable: "ReportRecord",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DaesiProgrammes_ReportRecord_ReportRecordId",
                table: "DaesiProgrammes",
                column: "ReportRecordId",
                principalTable: "ReportRecord",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DeuCourses_ReportRecord_ReportRecordId",
                table: "DeuCourses",
                column: "ReportRecordId",
                principalTable: "ReportRecord",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DeuOtherActivities_ReportRecord_ReportRecordId",
                table: "DeuOtherActivities",
                column: "ReportRecordId",
                principalTable: "ReportRecord",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EeuFLDs_ReportRecord_ReportRecordId",
                table: "EeuFLDs",
                column: "ReportRecordId",
                principalTable: "ReportRecord",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EeuOFTs_ReportRecord_ReportRecordId",
                table: "EeuOFTs",
                column: "ReportRecordId",
                principalTable: "ReportRecord",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EeuOtherActivities_ReportRecord_ReportRecordId",
                table: "EeuOtherActivities",
                column: "ReportRecordId",
                principalTable: "ReportRecord",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EeuTrainingProgrammes_ReportRecord_ReportRecordId",
                table: "EeuTrainingProgrammes",
                column: "ReportRecordId",
                principalTable: "ReportRecord",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FiuProgrammes_ReportRecord_ReportRecordId",
                table: "FiuProgrammes",
                column: "ReportRecordId",
                principalTable: "ReportRecord",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FtiOtherActivities_ReportRecord_ReportRecordId",
                table: "FtiOtherActivities",
                column: "ReportRecordId",
                principalTable: "ReportRecord",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FtiTrainingPrograms_ReportRecord_ReportRecordId",
                table: "FtiTrainingPrograms",
                column: "ReportRecordId",
                principalTable: "ReportRecord",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IbtavOtherActivities_ReportRecord_ReportRecordId",
                table: "IbtavOtherActivities",
                column: "ReportRecordId",
                principalTable: "ReportRecord",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IbtvaProgrammes_ReportRecord_ReportRecordId",
                table: "IbtvaProgrammes",
                column: "ReportRecordId",
                principalTable: "ReportRecord",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StuOtherActivities_ReportRecord_ReportRecordId",
                table: "StuOtherActivities",
                column: "ReportRecordId",
                principalTable: "ReportRecord",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StuSponseredTrainingProgrammes_ReportRecord_ReportRecordId",
                table: "StuSponseredTrainingProgrammes",
                column: "ReportRecordId",
                principalTable: "ReportRecord",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StuTrainingProgrammes_ReportRecord_ReportRecordId",
                table: "StuTrainingProgrammes",
                column: "ReportRecordId",
                principalTable: "ReportRecord",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

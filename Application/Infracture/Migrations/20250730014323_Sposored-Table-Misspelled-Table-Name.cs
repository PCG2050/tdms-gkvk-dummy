using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SposoredTableMisspelledTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StuSponseredTrainingProgrammes");

            migrationBuilder.CreateTable(
                name: "StuSponsoredTrainingProgrammes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Organization = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrainingTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Place = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: false),
                    TrainingCount = table.Column<int>(type: "int", nullable: false),
                    ParticipantCount = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Attachements = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnitLocationId = table.Column<int>(type: "int", nullable: false),
                    OrganizationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StuSponsoredTrainingProgrammes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StuSponsoredTrainingProgrammes_OrganizationUnitLocations_UnitLocationId",
                        column: x => x.UnitLocationId,
                        principalTable: "OrganizationUnitLocations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StuSponsoredTrainingProgrammes_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StuSponsoredTrainingProgrammes_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_StuSponsoredTrainingProgrammes_CreatedById",
                table: "StuSponsoredTrainingProgrammes",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_StuSponsoredTrainingProgrammes_UnitLocationId",
                table: "StuSponsoredTrainingProgrammes",
                column: "UnitLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_StuSponsoredTrainingProgrammes_UpdatedById",
                table: "StuSponsoredTrainingProgrammes",
                column: "UpdatedById");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StuSponsoredTrainingProgrammes");

            migrationBuilder.CreateTable(
                name: "StuSponseredTrainingProgrammes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    UnitLocationId = table.Column<int>(type: "int", nullable: false),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    Attachements = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Organization = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrganizationId = table.Column<int>(type: "int", nullable: false),
                    ParticipantCount = table.Column<int>(type: "int", nullable: false),
                    Place = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    TrainingCount = table.Column<int>(type: "int", nullable: false),
                    TrainingTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StuSponseredTrainingProgrammes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StuSponseredTrainingProgrammes_OrganizationUnitLocations_UnitLocationId",
                        column: x => x.UnitLocationId,
                        principalTable: "OrganizationUnitLocations",
                        principalColumn: "Id");
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

            migrationBuilder.CreateIndex(
                name: "IX_StuSponseredTrainingProgrammes_CreatedById",
                table: "StuSponseredTrainingProgrammes",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_StuSponseredTrainingProgrammes_UnitLocationId",
                table: "StuSponseredTrainingProgrammes",
                column: "UnitLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_StuSponseredTrainingProgrammes_UpdatedById",
                table: "StuSponseredTrainingProgrammes",
                column: "UpdatedById");
        }
    }
}

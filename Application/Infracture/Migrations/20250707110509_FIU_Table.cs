using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FIU_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FiuProgrammeTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
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
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    IsOther = table.Column<bool>(type: "bit", nullable: false),
                    OtherName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Count = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ReportRecordId = table.Column<int>(type: "int", nullable: false),
                    UnitId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FiuProgrammes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FiuProgrammes_FiuProgrammeTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "FiuProgrammeTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FiuProgrammes_ReportRecord_ReportRecordId",
                        column: x => x.ReportRecordId,
                        principalTable: "ReportRecord",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FiuProgrammes_Unit_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Unit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_FiuProgrammes_CreatedById",
                table: "FiuProgrammes",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FiuProgrammes_ReportRecordId",
                table: "FiuProgrammes",
                column: "ReportRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_FiuProgrammes_TypeId",
                table: "FiuProgrammes",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_FiuProgrammes_UnitId",
                table: "FiuProgrammes",
                column: "UnitId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FiuProgrammes");

            migrationBuilder.DropTable(
                name: "FiuProgrammeTypes");
        }
    }
}

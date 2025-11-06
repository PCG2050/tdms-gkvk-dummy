using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Unit_UnitHead : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UnitTrainers_Users_TrainerId",
                table: "UnitTrainers");

            migrationBuilder.CreateTable(
                name: "UnitHeadAssignments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UnitLocationId = table.Column<int>(type: "int", nullable: false),
                    UnitHeadId = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitHeadAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnitHeadAssignments_OrganizationUnitLocations_UnitLocationId",
                        column: x => x.UnitLocationId,
                        principalTable: "OrganizationUnitLocations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UnitHeadAssignments_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UnitHeadAssignments_Users_UnitHeadId",
                        column: x => x.UnitHeadId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UnitHeadAssignments_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_UnitHeadAssignments_CreatedById",
                table: "UnitHeadAssignments",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_UnitHeadAssignments_UnitHeadId",
                table: "UnitHeadAssignments",
                column: "UnitHeadId");

            migrationBuilder.CreateIndex(
                name: "IX_UnitHeadAssignments_UnitLocationId",
                table: "UnitHeadAssignments",
                column: "UnitLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_UnitHeadAssignments_UpdatedById",
                table: "UnitHeadAssignments",
                column: "UpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_UnitTrainers_Users_TrainerId",
                table: "UnitTrainers",
                column: "TrainerId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UnitTrainers_Users_TrainerId",
                table: "UnitTrainers");

            migrationBuilder.DropTable(
                name: "UnitHeadAssignments");

            migrationBuilder.AddForeignKey(
                name: "FK_UnitTrainers_Users_TrainerId",
                table: "UnitTrainers",
                column: "TrainerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

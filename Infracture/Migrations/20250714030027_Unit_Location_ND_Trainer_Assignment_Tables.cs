using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Unit_Location_ND_Trainer_Assignment_Tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrganizationUnitLocations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrganizationId = table.Column<int>(type: "int", nullable: false),
                    UnitId = table.Column<int>(type: "int", nullable: false),
                    DistrictId = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationUnitLocations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrganizationUnitLocations_Districts_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "Districts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrganizationUnitLocations_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrganizationUnitLocations_Unit_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Unit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrganizationUnitLocations_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrganizationUnitLocations_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UnitTrainers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UnitLocationId = table.Column<int>(type: "int", nullable: false),
                    TrainerId = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitTrainers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnitTrainers_OrganizationUnitLocations_UnitLocationId",
                        column: x => x.UnitLocationId,
                        principalTable: "OrganizationUnitLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UnitTrainers_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UnitTrainers_Users_TrainerId",
                        column: x => x.TrainerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UnitTrainers_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationUnitLocations_CreatedById",
                table: "OrganizationUnitLocations",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationUnitLocations_DistrictId",
                table: "OrganizationUnitLocations",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationUnitLocations_OrganizationId",
                table: "OrganizationUnitLocations",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationUnitLocations_UnitId",
                table: "OrganizationUnitLocations",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationUnitLocations_UpdatedById",
                table: "OrganizationUnitLocations",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_UnitTrainers_CreatedById",
                table: "UnitTrainers",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_UnitTrainers_TrainerId",
                table: "UnitTrainers",
                column: "TrainerId");

            migrationBuilder.CreateIndex(
                name: "IX_UnitTrainers_UnitLocationId",
                table: "UnitTrainers",
                column: "UnitLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_UnitTrainers_UpdatedById",
                table: "UnitTrainers",
                column: "UpdatedById");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UnitTrainers");

            migrationBuilder.DropTable(
                name: "OrganizationUnitLocations");
        }
    }
}

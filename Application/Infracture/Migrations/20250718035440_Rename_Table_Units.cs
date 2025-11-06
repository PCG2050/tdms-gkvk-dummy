using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Rename_Table_Units : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrganizationUnitLocations_Unit_UnitId",
                table: "OrganizationUnitLocations");

            migrationBuilder.DropForeignKey(
                name: "FK_Unit_Unit_ParentUnitId",
                table: "Unit");

            migrationBuilder.DropForeignKey(
                name: "FK_Unit_Users_CreatedById",
                table: "Unit");

            migrationBuilder.DropForeignKey(
                name: "FK_Unit_Users_UpdatedById",
                table: "Unit");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Unit",
                table: "Unit");

            migrationBuilder.RenameTable(
                name: "Unit",
                newName: "Units");

            migrationBuilder.RenameIndex(
                name: "IX_Unit_UpdatedById",
                table: "Units",
                newName: "IX_Units_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_Unit_ParentUnitId",
                table: "Units",
                newName: "IX_Units_ParentUnitId");

            migrationBuilder.RenameIndex(
                name: "IX_Unit_CreatedById",
                table: "Units",
                newName: "IX_Units_CreatedById");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Units",
                table: "Units",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationUnitLocations_Units_UnitId",
                table: "OrganizationUnitLocations",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Units_Units_ParentUnitId",
                table: "Units",
                column: "ParentUnitId",
                principalTable: "Units",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Units_Users_CreatedById",
                table: "Units",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Units_Users_UpdatedById",
                table: "Units",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrganizationUnitLocations_Units_UnitId",
                table: "OrganizationUnitLocations");

            migrationBuilder.DropForeignKey(
                name: "FK_Units_Units_ParentUnitId",
                table: "Units");

            migrationBuilder.DropForeignKey(
                name: "FK_Units_Users_CreatedById",
                table: "Units");

            migrationBuilder.DropForeignKey(
                name: "FK_Units_Users_UpdatedById",
                table: "Units");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Units",
                table: "Units");

            migrationBuilder.RenameTable(
                name: "Units",
                newName: "Unit");

            migrationBuilder.RenameIndex(
                name: "IX_Units_UpdatedById",
                table: "Unit",
                newName: "IX_Unit_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_Units_ParentUnitId",
                table: "Unit",
                newName: "IX_Unit_ParentUnitId");

            migrationBuilder.RenameIndex(
                name: "IX_Units_CreatedById",
                table: "Unit",
                newName: "IX_Unit_CreatedById");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Unit",
                table: "Unit",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationUnitLocations_Unit_UnitId",
                table: "OrganizationUnitLocations",
                column: "UnitId",
                principalTable: "Unit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Unit_Unit_ParentUnitId",
                table: "Unit",
                column: "ParentUnitId",
                principalTable: "Unit",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Unit_Users_CreatedById",
                table: "Unit",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Unit_Users_UpdatedById",
                table: "Unit",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}

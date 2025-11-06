using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FIULocationTracking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Step 1: Add columns as NULLABLE first (remove defaultValue: 0)
            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "FIUProgramActivities",
                type: "int",
                nullable: true);  // Changed to nullable

            migrationBuilder.AddColumn<int>(
                name: "UnitLocationId",
                table: "FIUProgramActivities",
                type: "int",
                nullable: true);  // Changed to nullable

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "FIUOtherActivities",
                type: "int",
                nullable: true);  // Changed to nullable

            migrationBuilder.AddColumn<int>(
                name: "UnitLocationId",
                table: "FIUOtherActivities",
                type: "int",
                nullable: true);  // Changed to nullable

            // Step 2: Backfill existing data
            migrationBuilder.Sql(@"
                -- Backfill FIUProgramActivities
                UPDATE fpa
                SET 
                    fpa.UnitLocationId = ta.UnitLocationId,
                    fpa.OrganizationId = ul.OrganizationId
                FROM FIUProgramActivities fpa
                INNER JOIN UnitTrainers ta ON ta.TrainerId = fpa.CreatedById
                INNER JOIN OrganizationUnitLocations ul ON ul.Id = ta.UnitLocationId
                WHERE ul.UnitId = 3;

                -- Handle orphaned FIUProgramActivities
                DECLARE @DefaultFIULocation INT;
                SELECT TOP 1 @DefaultFIULocation = Id FROM OrganizationUnitLocations WHERE UnitId = 3;
                
                IF @DefaultFIULocation IS NOT NULL
                BEGIN
                    UPDATE fpa
                    SET 
                        fpa.UnitLocationId = @DefaultFIULocation,
                        fpa.OrganizationId = ul.OrganizationId
                    FROM FIUProgramActivities fpa
                    CROSS JOIN OrganizationUnitLocations ul
                    WHERE fpa.UnitLocationId IS NULL AND ul.Id = @DefaultFIULocation;
                END

                -- Backfill FIUOtherActivities
                UPDATE foa
                SET 
                    foa.UnitLocationId = ta.UnitLocationId,
                    foa.OrganizationId = ul.OrganizationId
                FROM FIUOtherActivities foa
                INNER JOIN UnitTrainers ta ON ta.TrainerId = foa.CreatedById
                INNER JOIN OrganizationUnitLocations ul ON ul.Id = ta.UnitLocationId
                WHERE ul.UnitId = 3;

                -- Handle orphaned FIUOtherActivities
                IF @DefaultFIULocation IS NOT NULL
                BEGIN
                    UPDATE foa
                    SET 
                        foa.UnitLocationId = @DefaultFIULocation,
                        foa.OrganizationId = ul.OrganizationId
                    FROM FIUOtherActivities foa
                    CROSS JOIN OrganizationUnitLocations ul
                    WHERE foa.UnitLocationId IS NULL AND ul.Id = @DefaultFIULocation;
                END
            ");

            // Step 3: Make columns NOT NULL after data is populated
            migrationBuilder.AlterColumn<int>(
                name: "UnitLocationId",
                table: "FIUProgramActivities",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OrganizationId",
                table: "FIUProgramActivities",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UnitLocationId",
                table: "FIUOtherActivities",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OrganizationId",
                table: "FIUOtherActivities",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            // Step 4: Create indexes
            migrationBuilder.CreateIndex(
                name: "IX_FIUProgramActivities_OrganizationId",
                table: "FIUProgramActivities",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_FIUProgramActivities_UnitLocationId",
                table: "FIUProgramActivities",
                column: "UnitLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_FIUOtherActivities_OrganizationId",
                table: "FIUOtherActivities",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_FIUOtherActivities_UnitLocationId",
                table: "FIUOtherActivities",
                column: "UnitLocationId");

            // Step 5: Add foreign keys (after data is valid)
            migrationBuilder.AddForeignKey(
                name: "FK_FIUOtherActivities_OrganizationUnitLocations_UnitLocationId",
                table: "FIUOtherActivities",
                column: "UnitLocationId",
                principalTable: "OrganizationUnitLocations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FIUOtherActivities_Organizations_OrganizationId",
                table: "FIUOtherActivities",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FIUProgramActivities_OrganizationUnitLocations_UnitLocationId",
                table: "FIUProgramActivities",
                column: "UnitLocationId",
                principalTable: "OrganizationUnitLocations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FIUProgramActivities_Organizations_OrganizationId",
                table: "FIUProgramActivities",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FIUOtherActivities_OrganizationUnitLocations_UnitLocationId",
                table: "FIUOtherActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_FIUOtherActivities_Organizations_OrganizationId",
                table: "FIUOtherActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_FIUProgramActivities_OrganizationUnitLocations_UnitLocationId",
                table: "FIUProgramActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_FIUProgramActivities_Organizations_OrganizationId",
                table: "FIUProgramActivities");

            migrationBuilder.DropIndex(
                name: "IX_FIUProgramActivities_OrganizationId",
                table: "FIUProgramActivities");

            migrationBuilder.DropIndex(
                name: "IX_FIUProgramActivities_UnitLocationId",
                table: "FIUProgramActivities");

            migrationBuilder.DropIndex(
                name: "IX_FIUOtherActivities_OrganizationId",
                table: "FIUOtherActivities");

            migrationBuilder.DropIndex(
                name: "IX_FIUOtherActivities_UnitLocationId",
                table: "FIUOtherActivities");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "FIUProgramActivities");

            migrationBuilder.DropColumn(
                name: "UnitLocationId",
                table: "FIUProgramActivities");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "FIUOtherActivities");

            migrationBuilder.DropColumn(
                name: "UnitLocationId",
                table: "FIUOtherActivities");
        }
    }
}
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class OrganizationColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DistrictId",
                table: "Organizations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PinCode",
                table: "Organizations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StorageContainerName",
                table: "Organizations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_DistrictId",
                table: "Organizations",
                column: "DistrictId");

            migrationBuilder.AddForeignKey(
                name: "FK_Organizations_Districts_DistrictId",
                table: "Organizations",
                column: "DistrictId",
                principalTable: "Districts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Organizations_Districts_DistrictId",
                table: "Organizations");

            migrationBuilder.DropIndex(
                name: "IX_Organizations_DistrictId",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "DistrictId",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "PinCode",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "StorageContainerName",
                table: "Organizations");
        }
    }
}

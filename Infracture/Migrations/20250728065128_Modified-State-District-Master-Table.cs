using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedStateDistrictMasterTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Districts_Users_CreatedById",
                table: "Districts");

            migrationBuilder.DropForeignKey(
                name: "FK_Districts_Users_UpdatedById",
                table: "Districts");

            migrationBuilder.DropForeignKey(
                name: "FK_States_Users_CreatedById",
                table: "States");

            migrationBuilder.DropForeignKey(
                name: "FK_States_Users_UpdatedById",
                table: "States");

            migrationBuilder.DropIndex(
                name: "IX_States_CreatedById",
                table: "States");

            migrationBuilder.DropIndex(
                name: "IX_States_UpdatedById",
                table: "States");

            migrationBuilder.DropIndex(
                name: "IX_Districts_CreatedById",
                table: "Districts");

            migrationBuilder.DropIndex(
                name: "IX_Districts_UpdatedById",
                table: "Districts");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "States");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                table: "States");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Districts");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                table: "Districts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "States",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedById",
                table: "States",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Districts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedById",
                table: "Districts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_States_CreatedById",
                table: "States",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_States_UpdatedById",
                table: "States",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Districts_CreatedById",
                table: "Districts",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Districts_UpdatedById",
                table: "Districts",
                column: "UpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Districts_Users_CreatedById",
                table: "Districts",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Districts_Users_UpdatedById",
                table: "Districts",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_States_Users_CreatedById",
                table: "States",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_States_Users_UpdatedById",
                table: "States",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Stitable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FTIProgramDetails_ProgramTypes_TypeId",
                table: "FTIProgramDetails");

            migrationBuilder.AddForeignKey(
                name: "FK_FTIProgramDetails_InfoTypes_TypeId",
                table: "FTIProgramDetails",
                column: "TypeId",
                principalTable: "InfoTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FTIProgramDetails_InfoTypes_TypeId",
                table: "FTIProgramDetails");

            migrationBuilder.AddForeignKey(
                name: "FK_FTIProgramDetails_ProgramTypes_TypeId",
                table: "FTIProgramDetails",
                column: "TypeId",
                principalTable: "ProgramTypes",
                principalColumn: "Id");
        }
    }
}

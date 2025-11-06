using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Visitor_Detail_Changes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Female_GEN",
                table: "VisitorDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Female_OBC",
                table: "VisitorDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Female_SC",
                table: "VisitorDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Female_ST",
                table: "VisitorDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Female_Total",
                table: "VisitorDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Male_GEN",
                table: "VisitorDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Male_OBC",
                table: "VisitorDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Male_SC",
                table: "VisitorDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Male_ST",
                table: "VisitorDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Male_Total",
                table: "VisitorDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Total",
                table: "VisitorDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PositionId",
                table: "NominationRewards",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_NominationRewards_PositionId",
                table: "NominationRewards",
                column: "PositionId");

            migrationBuilder.AddForeignKey(
                name: "FK_NominationRewards_Positions_PositionId",
                table: "NominationRewards",
                column: "PositionId",
                principalTable: "Positions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NominationRewards_Positions_PositionId",
                table: "NominationRewards");

            migrationBuilder.DropIndex(
                name: "IX_NominationRewards_PositionId",
                table: "NominationRewards");

            migrationBuilder.DropColumn(
                name: "Female_GEN",
                table: "VisitorDetails");

            migrationBuilder.DropColumn(
                name: "Female_OBC",
                table: "VisitorDetails");

            migrationBuilder.DropColumn(
                name: "Female_SC",
                table: "VisitorDetails");

            migrationBuilder.DropColumn(
                name: "Female_ST",
                table: "VisitorDetails");

            migrationBuilder.DropColumn(
                name: "Female_Total",
                table: "VisitorDetails");

            migrationBuilder.DropColumn(
                name: "Male_GEN",
                table: "VisitorDetails");

            migrationBuilder.DropColumn(
                name: "Male_OBC",
                table: "VisitorDetails");

            migrationBuilder.DropColumn(
                name: "Male_SC",
                table: "VisitorDetails");

            migrationBuilder.DropColumn(
                name: "Male_ST",
                table: "VisitorDetails");

            migrationBuilder.DropColumn(
                name: "Male_Total",
                table: "VisitorDetails");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "VisitorDetails");

            migrationBuilder.DropColumn(
                name: "PositionId",
                table: "NominationRewards");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class User_TrainerAssignment_Table_Relation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UnitTrainers_Users_TrainerId",
                table: "UnitTrainers");

            migrationBuilder.AddForeignKey(
                name: "FK_UnitTrainers_Users_TrainerId",
                table: "UnitTrainers",
                column: "TrainerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UnitTrainers_Users_TrainerId",
                table: "UnitTrainers");

            migrationBuilder.AddForeignKey(
                name: "FK_UnitTrainers_Users_TrainerId",
                table: "UnitTrainers",
                column: "TrainerId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}

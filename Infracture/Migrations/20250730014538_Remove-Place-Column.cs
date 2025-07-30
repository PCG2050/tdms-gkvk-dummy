using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemovePlaceColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Place",
                table: "StuTrainingProgrammes");

            migrationBuilder.DropColumn(
                name: "Place",
                table: "StuSponsoredTrainingProgrammes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Place",
                table: "StuTrainingProgrammes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Place",
                table: "StuSponsoredTrainingProgrammes",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

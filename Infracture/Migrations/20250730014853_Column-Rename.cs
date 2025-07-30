using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ColumnRename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Organization",
                table: "StuSponsoredTrainingProgrammes",
                newName: "SponsorOrganization");

            migrationBuilder.CreateIndex(
                name: "IX_StuSponsoredTrainingProgrammes_OrganizationId",
                table: "StuSponsoredTrainingProgrammes",
                column: "OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_StuSponsoredTrainingProgrammes_Organizations_OrganizationId",
                table: "StuSponsoredTrainingProgrammes",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StuSponsoredTrainingProgrammes_Organizations_OrganizationId",
                table: "StuSponsoredTrainingProgrammes");

            migrationBuilder.DropIndex(
                name: "IX_StuSponsoredTrainingProgrammes_OrganizationId",
                table: "StuSponsoredTrainingProgrammes");

            migrationBuilder.RenameColumn(
                name: "SponsorOrganization",
                table: "StuSponsoredTrainingProgrammes",
                newName: "Organization");
        }
    }
}

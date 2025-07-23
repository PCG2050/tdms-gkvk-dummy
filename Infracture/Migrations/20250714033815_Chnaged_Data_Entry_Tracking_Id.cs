using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Chnaged_Data_Entry_Tracking_Id : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AsmVisits_Unit_UnitId",
                table: "AsmVisits");

            migrationBuilder.DropForeignKey(
                name: "FK_AticAdvisoryServices_Unit_UnitId",
                table: "AticAdvisoryServices");

            migrationBuilder.DropForeignKey(
                name: "FK_AticOtherActivities_Unit_UnitId",
                table: "AticOtherActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_AticSales_Unit_UnitId",
                table: "AticSales");

            migrationBuilder.DropForeignKey(
                name: "FK_DaesiOtherActivities_Unit_UnitId",
                table: "DaesiOtherActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_DaesiProgrammes_Unit_UnitId",
                table: "DaesiProgrammes");

            migrationBuilder.DropForeignKey(
                name: "FK_DeuCourses_Unit_UnitId",
                table: "DeuCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_DeuOtherActivities_Unit_UnitId",
                table: "DeuOtherActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_EeuFLDs_Unit_UnitId",
                table: "EeuFLDs");

            migrationBuilder.DropForeignKey(
                name: "FK_EeuOFTs_Unit_UnitId",
                table: "EeuOFTs");

            migrationBuilder.DropForeignKey(
                name: "FK_EeuOtherActivities_Unit_UnitId",
                table: "EeuOtherActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_EeuTrainingProgrammes_Unit_UnitId",
                table: "EeuTrainingProgrammes");

            migrationBuilder.DropForeignKey(
                name: "FK_FiuProgrammes_Unit_UnitId",
                table: "FiuProgrammes");

            migrationBuilder.DropForeignKey(
                name: "FK_FtiOtherActivities_Unit_UnitId",
                table: "FtiOtherActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_FtiTrainingPrograms_Unit_UnitId",
                table: "FtiTrainingPrograms");

            migrationBuilder.DropForeignKey(
                name: "FK_IbtavOtherActivities_Unit_UnitId",
                table: "IbtavOtherActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_IbtvaProgrammes_Unit_UnitId",
                table: "IbtvaProgrammes");

            migrationBuilder.DropForeignKey(
                name: "FK_StuOtherActivities_Unit_UnitId",
                table: "StuOtherActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_StuSponseredTrainingProgrammes_Unit_UnitId",
                table: "StuSponseredTrainingProgrammes");

            migrationBuilder.DropForeignKey(
                name: "FK_StuTrainingProgrammes_Unit_UnitId",
                table: "StuTrainingProgrammes");

            migrationBuilder.RenameColumn(
                name: "UnitId",
                table: "StuTrainingProgrammes",
                newName: "UnitLocationId");

            migrationBuilder.RenameIndex(
                name: "IX_StuTrainingProgrammes_UnitId",
                table: "StuTrainingProgrammes",
                newName: "IX_StuTrainingProgrammes_UnitLocationId");

            migrationBuilder.RenameColumn(
                name: "UnitId",
                table: "StuSponseredTrainingProgrammes",
                newName: "UnitLocationId");

            migrationBuilder.RenameIndex(
                name: "IX_StuSponseredTrainingProgrammes_UnitId",
                table: "StuSponseredTrainingProgrammes",
                newName: "IX_StuSponseredTrainingProgrammes_UnitLocationId");

            migrationBuilder.RenameColumn(
                name: "UnitId",
                table: "StuOtherActivities",
                newName: "UnitLocationId");

            migrationBuilder.RenameIndex(
                name: "IX_StuOtherActivities_UnitId",
                table: "StuOtherActivities",
                newName: "IX_StuOtherActivities_UnitLocationId");

            migrationBuilder.RenameColumn(
                name: "UnitId",
                table: "IbtvaProgrammes",
                newName: "UnitLocationId");

            migrationBuilder.RenameIndex(
                name: "IX_IbtvaProgrammes_UnitId",
                table: "IbtvaProgrammes",
                newName: "IX_IbtvaProgrammes_UnitLocationId");

            migrationBuilder.RenameColumn(
                name: "UnitId",
                table: "IbtavOtherActivities",
                newName: "UnitLocationId");

            migrationBuilder.RenameIndex(
                name: "IX_IbtavOtherActivities_UnitId",
                table: "IbtavOtherActivities",
                newName: "IX_IbtavOtherActivities_UnitLocationId");

            migrationBuilder.RenameColumn(
                name: "UnitId",
                table: "FtiTrainingPrograms",
                newName: "UnitLocationId");

            migrationBuilder.RenameIndex(
                name: "IX_FtiTrainingPrograms_UnitId",
                table: "FtiTrainingPrograms",
                newName: "IX_FtiTrainingPrograms_UnitLocationId");

            migrationBuilder.RenameColumn(
                name: "UnitId",
                table: "FtiOtherActivities",
                newName: "UnitLocationId");

            migrationBuilder.RenameIndex(
                name: "IX_FtiOtherActivities_UnitId",
                table: "FtiOtherActivities",
                newName: "IX_FtiOtherActivities_UnitLocationId");

            migrationBuilder.RenameColumn(
                name: "UnitId",
                table: "FiuProgrammes",
                newName: "UnitLocationId");

            migrationBuilder.RenameIndex(
                name: "IX_FiuProgrammes_UnitId",
                table: "FiuProgrammes",
                newName: "IX_FiuProgrammes_UnitLocationId");

            migrationBuilder.RenameColumn(
                name: "UnitId",
                table: "EeuTrainingProgrammes",
                newName: "UnitLocationId");

            migrationBuilder.RenameIndex(
                name: "IX_EeuTrainingProgrammes_UnitId",
                table: "EeuTrainingProgrammes",
                newName: "IX_EeuTrainingProgrammes_UnitLocationId");

            migrationBuilder.RenameColumn(
                name: "UnitId",
                table: "EeuOtherActivities",
                newName: "UnitLocationId");

            migrationBuilder.RenameIndex(
                name: "IX_EeuOtherActivities_UnitId",
                table: "EeuOtherActivities",
                newName: "IX_EeuOtherActivities_UnitLocationId");

            migrationBuilder.RenameColumn(
                name: "UnitId",
                table: "EeuOFTs",
                newName: "UnitLocationId");

            migrationBuilder.RenameIndex(
                name: "IX_EeuOFTs_UnitId",
                table: "EeuOFTs",
                newName: "IX_EeuOFTs_UnitLocationId");

            migrationBuilder.RenameColumn(
                name: "UnitId",
                table: "EeuFLDs",
                newName: "UnitLocationId");

            migrationBuilder.RenameIndex(
                name: "IX_EeuFLDs_UnitId",
                table: "EeuFLDs",
                newName: "IX_EeuFLDs_UnitLocationId");

            migrationBuilder.RenameColumn(
                name: "UnitId",
                table: "DeuOtherActivities",
                newName: "UnitLocationId");

            migrationBuilder.RenameIndex(
                name: "IX_DeuOtherActivities_UnitId",
                table: "DeuOtherActivities",
                newName: "IX_DeuOtherActivities_UnitLocationId");

            migrationBuilder.RenameColumn(
                name: "UnitId",
                table: "DeuCourses",
                newName: "UnitLocationId");

            migrationBuilder.RenameIndex(
                name: "IX_DeuCourses_UnitId",
                table: "DeuCourses",
                newName: "IX_DeuCourses_UnitLocationId");

            migrationBuilder.RenameColumn(
                name: "UnitId",
                table: "DaesiProgrammes",
                newName: "UnitLocationId");

            migrationBuilder.RenameIndex(
                name: "IX_DaesiProgrammes_UnitId",
                table: "DaesiProgrammes",
                newName: "IX_DaesiProgrammes_UnitLocationId");

            migrationBuilder.RenameColumn(
                name: "UnitId",
                table: "DaesiOtherActivities",
                newName: "UnitLocationId");

            migrationBuilder.RenameIndex(
                name: "IX_DaesiOtherActivities_UnitId",
                table: "DaesiOtherActivities",
                newName: "IX_DaesiOtherActivities_UnitLocationId");

            migrationBuilder.RenameColumn(
                name: "UnitId",
                table: "AticSales",
                newName: "UnitLocationId");

            migrationBuilder.RenameIndex(
                name: "IX_AticSales_UnitId",
                table: "AticSales",
                newName: "IX_AticSales_UnitLocationId");

            migrationBuilder.RenameColumn(
                name: "UnitId",
                table: "AticOtherActivities",
                newName: "UnitLocationId");

            migrationBuilder.RenameIndex(
                name: "IX_AticOtherActivities_UnitId",
                table: "AticOtherActivities",
                newName: "IX_AticOtherActivities_UnitLocationId");

            migrationBuilder.RenameColumn(
                name: "UnitId",
                table: "AticAdvisoryServices",
                newName: "UnitLocationId");

            migrationBuilder.RenameIndex(
                name: "IX_AticAdvisoryServices_UnitId",
                table: "AticAdvisoryServices",
                newName: "IX_AticAdvisoryServices_UnitLocationId");

            migrationBuilder.RenameColumn(
                name: "UnitId",
                table: "AsmVisits",
                newName: "UnitLocationId");

            migrationBuilder.RenameIndex(
                name: "IX_AsmVisits_UnitId",
                table: "AsmVisits",
                newName: "IX_AsmVisits_UnitLocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_AsmVisits_OrganizationUnitLocations_UnitLocationId",
                table: "AsmVisits",
                column: "UnitLocationId",
                principalTable: "OrganizationUnitLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AticAdvisoryServices_OrganizationUnitLocations_UnitLocationId",
                table: "AticAdvisoryServices",
                column: "UnitLocationId",
                principalTable: "OrganizationUnitLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AticOtherActivities_OrganizationUnitLocations_UnitLocationId",
                table: "AticOtherActivities",
                column: "UnitLocationId",
                principalTable: "OrganizationUnitLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AticSales_OrganizationUnitLocations_UnitLocationId",
                table: "AticSales",
                column: "UnitLocationId",
                principalTable: "OrganizationUnitLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DaesiOtherActivities_OrganizationUnitLocations_UnitLocationId",
                table: "DaesiOtherActivities",
                column: "UnitLocationId",
                principalTable: "OrganizationUnitLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DaesiProgrammes_OrganizationUnitLocations_UnitLocationId",
                table: "DaesiProgrammes",
                column: "UnitLocationId",
                principalTable: "OrganizationUnitLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DeuCourses_OrganizationUnitLocations_UnitLocationId",
                table: "DeuCourses",
                column: "UnitLocationId",
                principalTable: "OrganizationUnitLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DeuOtherActivities_OrganizationUnitLocations_UnitLocationId",
                table: "DeuOtherActivities",
                column: "UnitLocationId",
                principalTable: "OrganizationUnitLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EeuFLDs_OrganizationUnitLocations_UnitLocationId",
                table: "EeuFLDs",
                column: "UnitLocationId",
                principalTable: "OrganizationUnitLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EeuOFTs_OrganizationUnitLocations_UnitLocationId",
                table: "EeuOFTs",
                column: "UnitLocationId",
                principalTable: "OrganizationUnitLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EeuOtherActivities_OrganizationUnitLocations_UnitLocationId",
                table: "EeuOtherActivities",
                column: "UnitLocationId",
                principalTable: "OrganizationUnitLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EeuTrainingProgrammes_OrganizationUnitLocations_UnitLocationId",
                table: "EeuTrainingProgrammes",
                column: "UnitLocationId",
                principalTable: "OrganizationUnitLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FiuProgrammes_OrganizationUnitLocations_UnitLocationId",
                table: "FiuProgrammes",
                column: "UnitLocationId",
                principalTable: "OrganizationUnitLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FtiOtherActivities_OrganizationUnitLocations_UnitLocationId",
                table: "FtiOtherActivities",
                column: "UnitLocationId",
                principalTable: "OrganizationUnitLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FtiTrainingPrograms_OrganizationUnitLocations_UnitLocationId",
                table: "FtiTrainingPrograms",
                column: "UnitLocationId",
                principalTable: "OrganizationUnitLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IbtavOtherActivities_OrganizationUnitLocations_UnitLocationId",
                table: "IbtavOtherActivities",
                column: "UnitLocationId",
                principalTable: "OrganizationUnitLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IbtvaProgrammes_OrganizationUnitLocations_UnitLocationId",
                table: "IbtvaProgrammes",
                column: "UnitLocationId",
                principalTable: "OrganizationUnitLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StuOtherActivities_OrganizationUnitLocations_UnitLocationId",
                table: "StuOtherActivities",
                column: "UnitLocationId",
                principalTable: "OrganizationUnitLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StuSponseredTrainingProgrammes_OrganizationUnitLocations_UnitLocationId",
                table: "StuSponseredTrainingProgrammes",
                column: "UnitLocationId",
                principalTable: "OrganizationUnitLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StuTrainingProgrammes_OrganizationUnitLocations_UnitLocationId",
                table: "StuTrainingProgrammes",
                column: "UnitLocationId",
                principalTable: "OrganizationUnitLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AsmVisits_OrganizationUnitLocations_UnitLocationId",
                table: "AsmVisits");

            migrationBuilder.DropForeignKey(
                name: "FK_AticAdvisoryServices_OrganizationUnitLocations_UnitLocationId",
                table: "AticAdvisoryServices");

            migrationBuilder.DropForeignKey(
                name: "FK_AticOtherActivities_OrganizationUnitLocations_UnitLocationId",
                table: "AticOtherActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_AticSales_OrganizationUnitLocations_UnitLocationId",
                table: "AticSales");

            migrationBuilder.DropForeignKey(
                name: "FK_DaesiOtherActivities_OrganizationUnitLocations_UnitLocationId",
                table: "DaesiOtherActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_DaesiProgrammes_OrganizationUnitLocations_UnitLocationId",
                table: "DaesiProgrammes");

            migrationBuilder.DropForeignKey(
                name: "FK_DeuCourses_OrganizationUnitLocations_UnitLocationId",
                table: "DeuCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_DeuOtherActivities_OrganizationUnitLocations_UnitLocationId",
                table: "DeuOtherActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_EeuFLDs_OrganizationUnitLocations_UnitLocationId",
                table: "EeuFLDs");

            migrationBuilder.DropForeignKey(
                name: "FK_EeuOFTs_OrganizationUnitLocations_UnitLocationId",
                table: "EeuOFTs");

            migrationBuilder.DropForeignKey(
                name: "FK_EeuOtherActivities_OrganizationUnitLocations_UnitLocationId",
                table: "EeuOtherActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_EeuTrainingProgrammes_OrganizationUnitLocations_UnitLocationId",
                table: "EeuTrainingProgrammes");

            migrationBuilder.DropForeignKey(
                name: "FK_FiuProgrammes_OrganizationUnitLocations_UnitLocationId",
                table: "FiuProgrammes");

            migrationBuilder.DropForeignKey(
                name: "FK_FtiOtherActivities_OrganizationUnitLocations_UnitLocationId",
                table: "FtiOtherActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_FtiTrainingPrograms_OrganizationUnitLocations_UnitLocationId",
                table: "FtiTrainingPrograms");

            migrationBuilder.DropForeignKey(
                name: "FK_IbtavOtherActivities_OrganizationUnitLocations_UnitLocationId",
                table: "IbtavOtherActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_IbtvaProgrammes_OrganizationUnitLocations_UnitLocationId",
                table: "IbtvaProgrammes");

            migrationBuilder.DropForeignKey(
                name: "FK_StuOtherActivities_OrganizationUnitLocations_UnitLocationId",
                table: "StuOtherActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_StuSponseredTrainingProgrammes_OrganizationUnitLocations_UnitLocationId",
                table: "StuSponseredTrainingProgrammes");

            migrationBuilder.DropForeignKey(
                name: "FK_StuTrainingProgrammes_OrganizationUnitLocations_UnitLocationId",
                table: "StuTrainingProgrammes");

            migrationBuilder.RenameColumn(
                name: "UnitLocationId",
                table: "StuTrainingProgrammes",
                newName: "UnitId");

            migrationBuilder.RenameIndex(
                name: "IX_StuTrainingProgrammes_UnitLocationId",
                table: "StuTrainingProgrammes",
                newName: "IX_StuTrainingProgrammes_UnitId");

            migrationBuilder.RenameColumn(
                name: "UnitLocationId",
                table: "StuSponseredTrainingProgrammes",
                newName: "UnitId");

            migrationBuilder.RenameIndex(
                name: "IX_StuSponseredTrainingProgrammes_UnitLocationId",
                table: "StuSponseredTrainingProgrammes",
                newName: "IX_StuSponseredTrainingProgrammes_UnitId");

            migrationBuilder.RenameColumn(
                name: "UnitLocationId",
                table: "StuOtherActivities",
                newName: "UnitId");

            migrationBuilder.RenameIndex(
                name: "IX_StuOtherActivities_UnitLocationId",
                table: "StuOtherActivities",
                newName: "IX_StuOtherActivities_UnitId");

            migrationBuilder.RenameColumn(
                name: "UnitLocationId",
                table: "IbtvaProgrammes",
                newName: "UnitId");

            migrationBuilder.RenameIndex(
                name: "IX_IbtvaProgrammes_UnitLocationId",
                table: "IbtvaProgrammes",
                newName: "IX_IbtvaProgrammes_UnitId");

            migrationBuilder.RenameColumn(
                name: "UnitLocationId",
                table: "IbtavOtherActivities",
                newName: "UnitId");

            migrationBuilder.RenameIndex(
                name: "IX_IbtavOtherActivities_UnitLocationId",
                table: "IbtavOtherActivities",
                newName: "IX_IbtavOtherActivities_UnitId");

            migrationBuilder.RenameColumn(
                name: "UnitLocationId",
                table: "FtiTrainingPrograms",
                newName: "UnitId");

            migrationBuilder.RenameIndex(
                name: "IX_FtiTrainingPrograms_UnitLocationId",
                table: "FtiTrainingPrograms",
                newName: "IX_FtiTrainingPrograms_UnitId");

            migrationBuilder.RenameColumn(
                name: "UnitLocationId",
                table: "FtiOtherActivities",
                newName: "UnitId");

            migrationBuilder.RenameIndex(
                name: "IX_FtiOtherActivities_UnitLocationId",
                table: "FtiOtherActivities",
                newName: "IX_FtiOtherActivities_UnitId");

            migrationBuilder.RenameColumn(
                name: "UnitLocationId",
                table: "FiuProgrammes",
                newName: "UnitId");

            migrationBuilder.RenameIndex(
                name: "IX_FiuProgrammes_UnitLocationId",
                table: "FiuProgrammes",
                newName: "IX_FiuProgrammes_UnitId");

            migrationBuilder.RenameColumn(
                name: "UnitLocationId",
                table: "EeuTrainingProgrammes",
                newName: "UnitId");

            migrationBuilder.RenameIndex(
                name: "IX_EeuTrainingProgrammes_UnitLocationId",
                table: "EeuTrainingProgrammes",
                newName: "IX_EeuTrainingProgrammes_UnitId");

            migrationBuilder.RenameColumn(
                name: "UnitLocationId",
                table: "EeuOtherActivities",
                newName: "UnitId");

            migrationBuilder.RenameIndex(
                name: "IX_EeuOtherActivities_UnitLocationId",
                table: "EeuOtherActivities",
                newName: "IX_EeuOtherActivities_UnitId");

            migrationBuilder.RenameColumn(
                name: "UnitLocationId",
                table: "EeuOFTs",
                newName: "UnitId");

            migrationBuilder.RenameIndex(
                name: "IX_EeuOFTs_UnitLocationId",
                table: "EeuOFTs",
                newName: "IX_EeuOFTs_UnitId");

            migrationBuilder.RenameColumn(
                name: "UnitLocationId",
                table: "EeuFLDs",
                newName: "UnitId");

            migrationBuilder.RenameIndex(
                name: "IX_EeuFLDs_UnitLocationId",
                table: "EeuFLDs",
                newName: "IX_EeuFLDs_UnitId");

            migrationBuilder.RenameColumn(
                name: "UnitLocationId",
                table: "DeuOtherActivities",
                newName: "UnitId");

            migrationBuilder.RenameIndex(
                name: "IX_DeuOtherActivities_UnitLocationId",
                table: "DeuOtherActivities",
                newName: "IX_DeuOtherActivities_UnitId");

            migrationBuilder.RenameColumn(
                name: "UnitLocationId",
                table: "DeuCourses",
                newName: "UnitId");

            migrationBuilder.RenameIndex(
                name: "IX_DeuCourses_UnitLocationId",
                table: "DeuCourses",
                newName: "IX_DeuCourses_UnitId");

            migrationBuilder.RenameColumn(
                name: "UnitLocationId",
                table: "DaesiProgrammes",
                newName: "UnitId");

            migrationBuilder.RenameIndex(
                name: "IX_DaesiProgrammes_UnitLocationId",
                table: "DaesiProgrammes",
                newName: "IX_DaesiProgrammes_UnitId");

            migrationBuilder.RenameColumn(
                name: "UnitLocationId",
                table: "DaesiOtherActivities",
                newName: "UnitId");

            migrationBuilder.RenameIndex(
                name: "IX_DaesiOtherActivities_UnitLocationId",
                table: "DaesiOtherActivities",
                newName: "IX_DaesiOtherActivities_UnitId");

            migrationBuilder.RenameColumn(
                name: "UnitLocationId",
                table: "AticSales",
                newName: "UnitId");

            migrationBuilder.RenameIndex(
                name: "IX_AticSales_UnitLocationId",
                table: "AticSales",
                newName: "IX_AticSales_UnitId");

            migrationBuilder.RenameColumn(
                name: "UnitLocationId",
                table: "AticOtherActivities",
                newName: "UnitId");

            migrationBuilder.RenameIndex(
                name: "IX_AticOtherActivities_UnitLocationId",
                table: "AticOtherActivities",
                newName: "IX_AticOtherActivities_UnitId");

            migrationBuilder.RenameColumn(
                name: "UnitLocationId",
                table: "AticAdvisoryServices",
                newName: "UnitId");

            migrationBuilder.RenameIndex(
                name: "IX_AticAdvisoryServices_UnitLocationId",
                table: "AticAdvisoryServices",
                newName: "IX_AticAdvisoryServices_UnitId");

            migrationBuilder.RenameColumn(
                name: "UnitLocationId",
                table: "AsmVisits",
                newName: "UnitId");

            migrationBuilder.RenameIndex(
                name: "IX_AsmVisits_UnitLocationId",
                table: "AsmVisits",
                newName: "IX_AsmVisits_UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_AsmVisits_Unit_UnitId",
                table: "AsmVisits",
                column: "UnitId",
                principalTable: "Unit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AticAdvisoryServices_Unit_UnitId",
                table: "AticAdvisoryServices",
                column: "UnitId",
                principalTable: "Unit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AticOtherActivities_Unit_UnitId",
                table: "AticOtherActivities",
                column: "UnitId",
                principalTable: "Unit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AticSales_Unit_UnitId",
                table: "AticSales",
                column: "UnitId",
                principalTable: "Unit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DaesiOtherActivities_Unit_UnitId",
                table: "DaesiOtherActivities",
                column: "UnitId",
                principalTable: "Unit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DaesiProgrammes_Unit_UnitId",
                table: "DaesiProgrammes",
                column: "UnitId",
                principalTable: "Unit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DeuCourses_Unit_UnitId",
                table: "DeuCourses",
                column: "UnitId",
                principalTable: "Unit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DeuOtherActivities_Unit_UnitId",
                table: "DeuOtherActivities",
                column: "UnitId",
                principalTable: "Unit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EeuFLDs_Unit_UnitId",
                table: "EeuFLDs",
                column: "UnitId",
                principalTable: "Unit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EeuOFTs_Unit_UnitId",
                table: "EeuOFTs",
                column: "UnitId",
                principalTable: "Unit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EeuOtherActivities_Unit_UnitId",
                table: "EeuOtherActivities",
                column: "UnitId",
                principalTable: "Unit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EeuTrainingProgrammes_Unit_UnitId",
                table: "EeuTrainingProgrammes",
                column: "UnitId",
                principalTable: "Unit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FiuProgrammes_Unit_UnitId",
                table: "FiuProgrammes",
                column: "UnitId",
                principalTable: "Unit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FtiOtherActivities_Unit_UnitId",
                table: "FtiOtherActivities",
                column: "UnitId",
                principalTable: "Unit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FtiTrainingPrograms_Unit_UnitId",
                table: "FtiTrainingPrograms",
                column: "UnitId",
                principalTable: "Unit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IbtavOtherActivities_Unit_UnitId",
                table: "IbtavOtherActivities",
                column: "UnitId",
                principalTable: "Unit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IbtvaProgrammes_Unit_UnitId",
                table: "IbtvaProgrammes",
                column: "UnitId",
                principalTable: "Unit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StuOtherActivities_Unit_UnitId",
                table: "StuOtherActivities",
                column: "UnitId",
                principalTable: "Unit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StuSponseredTrainingProgrammes_Unit_UnitId",
                table: "StuSponseredTrainingProgrammes",
                column: "UnitId",
                principalTable: "Unit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StuTrainingProgrammes_Unit_UnitId",
                table: "StuTrainingProgrammes",
                column: "UnitId",
                principalTable: "Unit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

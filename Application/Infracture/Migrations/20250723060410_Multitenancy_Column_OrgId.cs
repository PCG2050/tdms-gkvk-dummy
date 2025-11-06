using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Multitenancy_Column_OrgId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "FK_Districts_States_StateId",
                table: "Districts");

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
                name: "FK_FiuProgrammes_FiuProgrammeTypes_TypeId",
                table: "FiuProgrammes");

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
                name: "FK_OrganizationUnitLocations_Districts_DistrictId",
                table: "OrganizationUnitLocations");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganizationUnitLocations_Organizations_OrganizationId",
                table: "OrganizationUnitLocations");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganizationUnitLocations_Units_UnitId",
                table: "OrganizationUnitLocations");

            migrationBuilder.DropForeignKey(
                name: "FK_StuOtherActivities_OrganizationUnitLocations_UnitLocationId",
                table: "StuOtherActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_StuSponseredTrainingProgrammes_OrganizationUnitLocations_UnitLocationId",
                table: "StuSponseredTrainingProgrammes");

            migrationBuilder.DropForeignKey(
                name: "FK_StuTrainingProgrammes_OrganizationUnitLocations_UnitLocationId",
                table: "StuTrainingProgrammes");

            migrationBuilder.DropForeignKey(
                name: "FK_UnitTrainers_OrganizationUnitLocations_UnitLocationId",
                table: "UnitTrainers");

            migrationBuilder.DropForeignKey(
                name: "FK_UnitTrainers_Users_TrainerId",
                table: "UnitTrainers");

            migrationBuilder.AddColumn<string>(
                name: "ProfileImageUrl",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "StuTrainingProgrammes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "StuSponseredTrainingProgrammes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "StuOtherActivities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "IbtvaProgrammes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "IbtavOtherActivities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "FtiTrainingPrograms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "FtiOtherActivities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "FiuProgrammes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "EeuTrainingProgrammes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "EeuOtherActivities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "EeuOFTs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "EeuFLDs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "DeuOtherActivities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "DeuCourses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "DaesiProgrammes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "DaesiOtherActivities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "AticSales",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "AticOtherActivities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "AticAdvisoryServices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "AsmVisits",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_StuTrainingProgrammes_OrganizationId",
                table: "StuTrainingProgrammes",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_StuOtherActivities_OrganizationId",
                table: "StuOtherActivities",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_IbtvaProgrammes_OrganizationId",
                table: "IbtvaProgrammes",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_IbtavOtherActivities_OrganizationId",
                table: "IbtavOtherActivities",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_FtiTrainingPrograms_OrganizationId",
                table: "FtiTrainingPrograms",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_FtiOtherActivities_OrganizationId",
                table: "FtiOtherActivities",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_FiuProgrammes_OrganizationId",
                table: "FiuProgrammes",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_EeuTrainingProgrammes_OrganizationId",
                table: "EeuTrainingProgrammes",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_EeuOtherActivities_OrganizationId",
                table: "EeuOtherActivities",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_EeuOFTs_OrganizationId",
                table: "EeuOFTs",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_EeuFLDs_OrganizationId",
                table: "EeuFLDs",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_DeuOtherActivities_OrganizationId",
                table: "DeuOtherActivities",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_DeuCourses_OrganizationId",
                table: "DeuCourses",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_DaesiProgrammes_OrganizationId",
                table: "DaesiProgrammes",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_DaesiOtherActivities_OrganizationId",
                table: "DaesiOtherActivities",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_AticSales_OrganizationId",
                table: "AticSales",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_AticOtherActivities_OrganizationId",
                table: "AticOtherActivities",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_AticAdvisoryServices_OrganizationId",
                table: "AticAdvisoryServices",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_AsmVisits_OrganizationId",
                table: "AsmVisits",
                column: "OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_AsmVisits_OrganizationUnitLocations_UnitLocationId",
                table: "AsmVisits",
                column: "UnitLocationId",
                principalTable: "OrganizationUnitLocations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AsmVisits_Organizations_OrganizationId",
                table: "AsmVisits",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AticAdvisoryServices_OrganizationUnitLocations_UnitLocationId",
                table: "AticAdvisoryServices",
                column: "UnitLocationId",
                principalTable: "OrganizationUnitLocations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AticAdvisoryServices_Organizations_OrganizationId",
                table: "AticAdvisoryServices",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AticOtherActivities_OrganizationUnitLocations_UnitLocationId",
                table: "AticOtherActivities",
                column: "UnitLocationId",
                principalTable: "OrganizationUnitLocations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AticOtherActivities_Organizations_OrganizationId",
                table: "AticOtherActivities",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AticSales_OrganizationUnitLocations_UnitLocationId",
                table: "AticSales",
                column: "UnitLocationId",
                principalTable: "OrganizationUnitLocations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AticSales_Organizations_OrganizationId",
                table: "AticSales",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DaesiOtherActivities_OrganizationUnitLocations_UnitLocationId",
                table: "DaesiOtherActivities",
                column: "UnitLocationId",
                principalTable: "OrganizationUnitLocations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DaesiOtherActivities_Organizations_OrganizationId",
                table: "DaesiOtherActivities",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DaesiProgrammes_OrganizationUnitLocations_UnitLocationId",
                table: "DaesiProgrammes",
                column: "UnitLocationId",
                principalTable: "OrganizationUnitLocations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DaesiProgrammes_Organizations_OrganizationId",
                table: "DaesiProgrammes",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DeuCourses_OrganizationUnitLocations_UnitLocationId",
                table: "DeuCourses",
                column: "UnitLocationId",
                principalTable: "OrganizationUnitLocations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DeuCourses_Organizations_OrganizationId",
                table: "DeuCourses",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DeuOtherActivities_OrganizationUnitLocations_UnitLocationId",
                table: "DeuOtherActivities",
                column: "UnitLocationId",
                principalTable: "OrganizationUnitLocations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DeuOtherActivities_Organizations_OrganizationId",
                table: "DeuOtherActivities",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Districts_States_StateId",
                table: "Districts",
                column: "StateId",
                principalTable: "States",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EeuFLDs_OrganizationUnitLocations_UnitLocationId",
                table: "EeuFLDs",
                column: "UnitLocationId",
                principalTable: "OrganizationUnitLocations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EeuFLDs_Organizations_OrganizationId",
                table: "EeuFLDs",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EeuOFTs_OrganizationUnitLocations_UnitLocationId",
                table: "EeuOFTs",
                column: "UnitLocationId",
                principalTable: "OrganizationUnitLocations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EeuOFTs_Organizations_OrganizationId",
                table: "EeuOFTs",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EeuOtherActivities_OrganizationUnitLocations_UnitLocationId",
                table: "EeuOtherActivities",
                column: "UnitLocationId",
                principalTable: "OrganizationUnitLocations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EeuOtherActivities_Organizations_OrganizationId",
                table: "EeuOtherActivities",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EeuTrainingProgrammes_OrganizationUnitLocations_UnitLocationId",
                table: "EeuTrainingProgrammes",
                column: "UnitLocationId",
                principalTable: "OrganizationUnitLocations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EeuTrainingProgrammes_Organizations_OrganizationId",
                table: "EeuTrainingProgrammes",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FiuProgrammes_FiuProgrammeTypes_TypeId",
                table: "FiuProgrammes",
                column: "TypeId",
                principalTable: "FiuProgrammeTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FiuProgrammes_OrganizationUnitLocations_UnitLocationId",
                table: "FiuProgrammes",
                column: "UnitLocationId",
                principalTable: "OrganizationUnitLocations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FiuProgrammes_Organizations_OrganizationId",
                table: "FiuProgrammes",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FtiOtherActivities_OrganizationUnitLocations_UnitLocationId",
                table: "FtiOtherActivities",
                column: "UnitLocationId",
                principalTable: "OrganizationUnitLocations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FtiOtherActivities_Organizations_OrganizationId",
                table: "FtiOtherActivities",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FtiTrainingPrograms_OrganizationUnitLocations_UnitLocationId",
                table: "FtiTrainingPrograms",
                column: "UnitLocationId",
                principalTable: "OrganizationUnitLocations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FtiTrainingPrograms_Organizations_OrganizationId",
                table: "FtiTrainingPrograms",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IbtavOtherActivities_OrganizationUnitLocations_UnitLocationId",
                table: "IbtavOtherActivities",
                column: "UnitLocationId",
                principalTable: "OrganizationUnitLocations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IbtavOtherActivities_Organizations_OrganizationId",
                table: "IbtavOtherActivities",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IbtvaProgrammes_OrganizationUnitLocations_UnitLocationId",
                table: "IbtvaProgrammes",
                column: "UnitLocationId",
                principalTable: "OrganizationUnitLocations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IbtvaProgrammes_Organizations_OrganizationId",
                table: "IbtvaProgrammes",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationUnitLocations_Districts_DistrictId",
                table: "OrganizationUnitLocations",
                column: "DistrictId",
                principalTable: "Districts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationUnitLocations_Organizations_OrganizationId",
                table: "OrganizationUnitLocations",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationUnitLocations_Units_UnitId",
                table: "OrganizationUnitLocations",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StuOtherActivities_OrganizationUnitLocations_UnitLocationId",
                table: "StuOtherActivities",
                column: "UnitLocationId",
                principalTable: "OrganizationUnitLocations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StuOtherActivities_Organizations_OrganizationId",
                table: "StuOtherActivities",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StuSponseredTrainingProgrammes_OrganizationUnitLocations_UnitLocationId",
                table: "StuSponseredTrainingProgrammes",
                column: "UnitLocationId",
                principalTable: "OrganizationUnitLocations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StuTrainingProgrammes_OrganizationUnitLocations_UnitLocationId",
                table: "StuTrainingProgrammes",
                column: "UnitLocationId",
                principalTable: "OrganizationUnitLocations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StuTrainingProgrammes_Organizations_OrganizationId",
                table: "StuTrainingProgrammes",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UnitTrainers_OrganizationUnitLocations_UnitLocationId",
                table: "UnitTrainers",
                column: "UnitLocationId",
                principalTable: "OrganizationUnitLocations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UnitTrainers_Users_TrainerId",
                table: "UnitTrainers",
                column: "TrainerId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AsmVisits_OrganizationUnitLocations_UnitLocationId",
                table: "AsmVisits");

            migrationBuilder.DropForeignKey(
                name: "FK_AsmVisits_Organizations_OrganizationId",
                table: "AsmVisits");

            migrationBuilder.DropForeignKey(
                name: "FK_AticAdvisoryServices_OrganizationUnitLocations_UnitLocationId",
                table: "AticAdvisoryServices");

            migrationBuilder.DropForeignKey(
                name: "FK_AticAdvisoryServices_Organizations_OrganizationId",
                table: "AticAdvisoryServices");

            migrationBuilder.DropForeignKey(
                name: "FK_AticOtherActivities_OrganizationUnitLocations_UnitLocationId",
                table: "AticOtherActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_AticOtherActivities_Organizations_OrganizationId",
                table: "AticOtherActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_AticSales_OrganizationUnitLocations_UnitLocationId",
                table: "AticSales");

            migrationBuilder.DropForeignKey(
                name: "FK_AticSales_Organizations_OrganizationId",
                table: "AticSales");

            migrationBuilder.DropForeignKey(
                name: "FK_DaesiOtherActivities_OrganizationUnitLocations_UnitLocationId",
                table: "DaesiOtherActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_DaesiOtherActivities_Organizations_OrganizationId",
                table: "DaesiOtherActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_DaesiProgrammes_OrganizationUnitLocations_UnitLocationId",
                table: "DaesiProgrammes");

            migrationBuilder.DropForeignKey(
                name: "FK_DaesiProgrammes_Organizations_OrganizationId",
                table: "DaesiProgrammes");

            migrationBuilder.DropForeignKey(
                name: "FK_DeuCourses_OrganizationUnitLocations_UnitLocationId",
                table: "DeuCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_DeuCourses_Organizations_OrganizationId",
                table: "DeuCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_DeuOtherActivities_OrganizationUnitLocations_UnitLocationId",
                table: "DeuOtherActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_DeuOtherActivities_Organizations_OrganizationId",
                table: "DeuOtherActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_Districts_States_StateId",
                table: "Districts");

            migrationBuilder.DropForeignKey(
                name: "FK_EeuFLDs_OrganizationUnitLocations_UnitLocationId",
                table: "EeuFLDs");

            migrationBuilder.DropForeignKey(
                name: "FK_EeuFLDs_Organizations_OrganizationId",
                table: "EeuFLDs");

            migrationBuilder.DropForeignKey(
                name: "FK_EeuOFTs_OrganizationUnitLocations_UnitLocationId",
                table: "EeuOFTs");

            migrationBuilder.DropForeignKey(
                name: "FK_EeuOFTs_Organizations_OrganizationId",
                table: "EeuOFTs");

            migrationBuilder.DropForeignKey(
                name: "FK_EeuOtherActivities_OrganizationUnitLocations_UnitLocationId",
                table: "EeuOtherActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_EeuOtherActivities_Organizations_OrganizationId",
                table: "EeuOtherActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_EeuTrainingProgrammes_OrganizationUnitLocations_UnitLocationId",
                table: "EeuTrainingProgrammes");

            migrationBuilder.DropForeignKey(
                name: "FK_EeuTrainingProgrammes_Organizations_OrganizationId",
                table: "EeuTrainingProgrammes");

            migrationBuilder.DropForeignKey(
                name: "FK_FiuProgrammes_FiuProgrammeTypes_TypeId",
                table: "FiuProgrammes");

            migrationBuilder.DropForeignKey(
                name: "FK_FiuProgrammes_OrganizationUnitLocations_UnitLocationId",
                table: "FiuProgrammes");

            migrationBuilder.DropForeignKey(
                name: "FK_FiuProgrammes_Organizations_OrganizationId",
                table: "FiuProgrammes");

            migrationBuilder.DropForeignKey(
                name: "FK_FtiOtherActivities_OrganizationUnitLocations_UnitLocationId",
                table: "FtiOtherActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_FtiOtherActivities_Organizations_OrganizationId",
                table: "FtiOtherActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_FtiTrainingPrograms_OrganizationUnitLocations_UnitLocationId",
                table: "FtiTrainingPrograms");

            migrationBuilder.DropForeignKey(
                name: "FK_FtiTrainingPrograms_Organizations_OrganizationId",
                table: "FtiTrainingPrograms");

            migrationBuilder.DropForeignKey(
                name: "FK_IbtavOtherActivities_OrganizationUnitLocations_UnitLocationId",
                table: "IbtavOtherActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_IbtavOtherActivities_Organizations_OrganizationId",
                table: "IbtavOtherActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_IbtvaProgrammes_OrganizationUnitLocations_UnitLocationId",
                table: "IbtvaProgrammes");

            migrationBuilder.DropForeignKey(
                name: "FK_IbtvaProgrammes_Organizations_OrganizationId",
                table: "IbtvaProgrammes");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganizationUnitLocations_Districts_DistrictId",
                table: "OrganizationUnitLocations");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganizationUnitLocations_Organizations_OrganizationId",
                table: "OrganizationUnitLocations");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganizationUnitLocations_Units_UnitId",
                table: "OrganizationUnitLocations");

            migrationBuilder.DropForeignKey(
                name: "FK_StuOtherActivities_OrganizationUnitLocations_UnitLocationId",
                table: "StuOtherActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_StuOtherActivities_Organizations_OrganizationId",
                table: "StuOtherActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_StuSponseredTrainingProgrammes_OrganizationUnitLocations_UnitLocationId",
                table: "StuSponseredTrainingProgrammes");

            migrationBuilder.DropForeignKey(
                name: "FK_StuTrainingProgrammes_OrganizationUnitLocations_UnitLocationId",
                table: "StuTrainingProgrammes");

            migrationBuilder.DropForeignKey(
                name: "FK_StuTrainingProgrammes_Organizations_OrganizationId",
                table: "StuTrainingProgrammes");

            migrationBuilder.DropForeignKey(
                name: "FK_UnitTrainers_OrganizationUnitLocations_UnitLocationId",
                table: "UnitTrainers");

            migrationBuilder.DropForeignKey(
                name: "FK_UnitTrainers_Users_TrainerId",
                table: "UnitTrainers");

            migrationBuilder.DropIndex(
                name: "IX_StuTrainingProgrammes_OrganizationId",
                table: "StuTrainingProgrammes");

            migrationBuilder.DropIndex(
                name: "IX_StuOtherActivities_OrganizationId",
                table: "StuOtherActivities");

            migrationBuilder.DropIndex(
                name: "IX_IbtvaProgrammes_OrganizationId",
                table: "IbtvaProgrammes");

            migrationBuilder.DropIndex(
                name: "IX_IbtavOtherActivities_OrganizationId",
                table: "IbtavOtherActivities");

            migrationBuilder.DropIndex(
                name: "IX_FtiTrainingPrograms_OrganizationId",
                table: "FtiTrainingPrograms");

            migrationBuilder.DropIndex(
                name: "IX_FtiOtherActivities_OrganizationId",
                table: "FtiOtherActivities");

            migrationBuilder.DropIndex(
                name: "IX_FiuProgrammes_OrganizationId",
                table: "FiuProgrammes");

            migrationBuilder.DropIndex(
                name: "IX_EeuTrainingProgrammes_OrganizationId",
                table: "EeuTrainingProgrammes");

            migrationBuilder.DropIndex(
                name: "IX_EeuOtherActivities_OrganizationId",
                table: "EeuOtherActivities");

            migrationBuilder.DropIndex(
                name: "IX_EeuOFTs_OrganizationId",
                table: "EeuOFTs");

            migrationBuilder.DropIndex(
                name: "IX_EeuFLDs_OrganizationId",
                table: "EeuFLDs");

            migrationBuilder.DropIndex(
                name: "IX_DeuOtherActivities_OrganizationId",
                table: "DeuOtherActivities");

            migrationBuilder.DropIndex(
                name: "IX_DeuCourses_OrganizationId",
                table: "DeuCourses");

            migrationBuilder.DropIndex(
                name: "IX_DaesiProgrammes_OrganizationId",
                table: "DaesiProgrammes");

            migrationBuilder.DropIndex(
                name: "IX_DaesiOtherActivities_OrganizationId",
                table: "DaesiOtherActivities");

            migrationBuilder.DropIndex(
                name: "IX_AticSales_OrganizationId",
                table: "AticSales");

            migrationBuilder.DropIndex(
                name: "IX_AticOtherActivities_OrganizationId",
                table: "AticOtherActivities");

            migrationBuilder.DropIndex(
                name: "IX_AticAdvisoryServices_OrganizationId",
                table: "AticAdvisoryServices");

            migrationBuilder.DropIndex(
                name: "IX_AsmVisits_OrganizationId",
                table: "AsmVisits");

            migrationBuilder.DropColumn(
                name: "ProfileImageUrl",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "StuTrainingProgrammes");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "StuSponseredTrainingProgrammes");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "StuOtherActivities");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "IbtvaProgrammes");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "IbtavOtherActivities");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "FtiTrainingPrograms");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "FtiOtherActivities");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "FiuProgrammes");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "EeuTrainingProgrammes");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "EeuOtherActivities");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "EeuOFTs");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "EeuFLDs");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "DeuOtherActivities");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "DeuCourses");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "DaesiProgrammes");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "DaesiOtherActivities");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "AticSales");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "AticOtherActivities");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "AticAdvisoryServices");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "AsmVisits");

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
                name: "FK_Districts_States_StateId",
                table: "Districts",
                column: "StateId",
                principalTable: "States",
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
                name: "FK_FiuProgrammes_FiuProgrammeTypes_TypeId",
                table: "FiuProgrammes",
                column: "TypeId",
                principalTable: "FiuProgrammeTypes",
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
                name: "FK_OrganizationUnitLocations_Districts_DistrictId",
                table: "OrganizationUnitLocations",
                column: "DistrictId",
                principalTable: "Districts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationUnitLocations_Organizations_OrganizationId",
                table: "OrganizationUnitLocations",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationUnitLocations_Units_UnitId",
                table: "OrganizationUnitLocations",
                column: "UnitId",
                principalTable: "Units",
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

            migrationBuilder.AddForeignKey(
                name: "FK_UnitTrainers_OrganizationUnitLocations_UnitLocationId",
                table: "UnitTrainers",
                column: "UnitLocationId",
                principalTable: "OrganizationUnitLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UnitTrainers_Users_TrainerId",
                table: "UnitTrainers",
                column: "TrainerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

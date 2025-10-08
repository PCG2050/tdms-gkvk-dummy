using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedFTItables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeuRecommendation_DeuProgramDetails_DeuProgramDetailsId",
                table: "DeuRecommendation");

            migrationBuilder.DropForeignKey(
                name: "FK_DeuRecommendation_Users_CreatedById",
                table: "DeuRecommendation");

            migrationBuilder.DropForeignKey(
                name: "FK_DeuRecommendation_Users_UpdatedById",
                table: "DeuRecommendation");

            migrationBuilder.DropForeignKey(
                name: "FK_DeuResourcePerson_DeuProgramContentAndResources_DeuProgramContentAndResourcesId",
                table: "DeuResourcePerson");

            migrationBuilder.DropForeignKey(
                name: "FK_DeuResourcePerson_Users_CreatedById",
                table: "DeuResourcePerson");

            migrationBuilder.DropForeignKey(
                name: "FK_DeuResourcePerson_Users_UpdatedById",
                table: "DeuResourcePerson");

            migrationBuilder.DropForeignKey(
                name: "FK_DeuTeachingAIdsDeveloped_DeuProgramContentAndResources_DeuProgramContentAndResourcesId",
                table: "DeuTeachingAIdsDeveloped");

            migrationBuilder.DropForeignKey(
                name: "FK_DeuTeachingAIdsDeveloped_TypeOfAids_TypeOfAidId",
                table: "DeuTeachingAIdsDeveloped");

            migrationBuilder.DropForeignKey(
                name: "FK_DeuTeachingAIdsDeveloped_Users_CreatedById",
                table: "DeuTeachingAIdsDeveloped");

            migrationBuilder.DropForeignKey(
                name: "FK_DeuTeachingAIdsDeveloped_Users_UpdatedById",
                table: "DeuTeachingAIdsDeveloped");

            migrationBuilder.DropForeignKey(
                name: "FK_EeuRecommendation_EeuProgramDetails_EeuProgramDetailsId",
                table: "EeuRecommendation");

            migrationBuilder.DropForeignKey(
                name: "FK_EeuRecommendation_Users_CreatedById",
                table: "EeuRecommendation");

            migrationBuilder.DropForeignKey(
                name: "FK_EeuRecommendation_Users_UpdatedById",
                table: "EeuRecommendation");

            migrationBuilder.DropForeignKey(
                name: "FK_EeuResourcePerson_EeuProgramContentAndResources_EeuProgramContentAndResourcesId",
                table: "EeuResourcePerson");

            migrationBuilder.DropForeignKey(
                name: "FK_EeuResourcePerson_Users_CreatedById",
                table: "EeuResourcePerson");

            migrationBuilder.DropForeignKey(
                name: "FK_EeuResourcePerson_Users_UpdatedById",
                table: "EeuResourcePerson");

            migrationBuilder.DropForeignKey(
                name: "FK_IbtvaRecommendation_IbtvaProgramDetails_IbtvaProgramDetailsId",
                table: "IbtvaRecommendation");

            migrationBuilder.DropForeignKey(
                name: "FK_IbtvaRecommendation_Users_CreatedById",
                table: "IbtvaRecommendation");

            migrationBuilder.DropForeignKey(
                name: "FK_IbtvaRecommendation_Users_UpdatedById",
                table: "IbtvaRecommendation");

            migrationBuilder.DropForeignKey(
                name: "FK_IbtvaResourcePerson_IbtvaProgramContentAndResources_IbtvaProgramContentAndResourcesId",
                table: "IbtvaResourcePerson");

            migrationBuilder.DropForeignKey(
                name: "FK_IbtvaResourcePerson_Users_CreatedById",
                table: "IbtvaResourcePerson");

            migrationBuilder.DropForeignKey(
                name: "FK_IbtvaResourcePerson_Users_UpdatedById",
                table: "IbtvaResourcePerson");

            migrationBuilder.DropForeignKey(
                name: "FK_IbtvaTeachingAIdsDeveloped_IbtvaProgramContentAndResources_IbtvaProgramContentAndResourcesId",
                table: "IbtvaTeachingAIdsDeveloped");

            migrationBuilder.DropForeignKey(
                name: "FK_IbtvaTeachingAIdsDeveloped_TypeOfAids_TypeOfAidId",
                table: "IbtvaTeachingAIdsDeveloped");

            migrationBuilder.DropForeignKey(
                name: "FK_IbtvaTeachingAIdsDeveloped_Users_CreatedById",
                table: "IbtvaTeachingAIdsDeveloped");

            migrationBuilder.DropForeignKey(
                name: "FK_IbtvaTeachingAIdsDeveloped_Users_UpdatedById",
                table: "IbtvaTeachingAIdsDeveloped");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IbtvaTeachingAIdsDeveloped",
                table: "IbtvaTeachingAIdsDeveloped");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DeuTeachingAIdsDeveloped",
                table: "DeuTeachingAIdsDeveloped");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IbtvaResourcePerson",
                table: "IbtvaResourcePerson");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IbtvaRecommendation",
                table: "IbtvaRecommendation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EeuResourcePerson",
                table: "EeuResourcePerson");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EeuRecommendation",
                table: "EeuRecommendation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DeuResourcePerson",
                table: "DeuResourcePerson");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DeuRecommendation",
                table: "DeuRecommendation");

            migrationBuilder.RenameTable(
                name: "IbtvaTeachingAIdsDeveloped",
                newName: "IbtvaTeachingAidsDeveloped");

            migrationBuilder.RenameTable(
                name: "DeuTeachingAIdsDeveloped",
                newName: "DeuTeachingAidsDeveloped");

            migrationBuilder.RenameTable(
                name: "IbtvaResourcePerson",
                newName: "IbtvaResourcePersons");

            migrationBuilder.RenameTable(
                name: "IbtvaRecommendation",
                newName: "IbtvaRecommendations");

            migrationBuilder.RenameTable(
                name: "EeuResourcePerson",
                newName: "EeuResourcePersons");

            migrationBuilder.RenameTable(
                name: "EeuRecommendation",
                newName: "EeuRecommendations");

            migrationBuilder.RenameTable(
                name: "DeuResourcePerson",
                newName: "DeuResourcePersons");

            migrationBuilder.RenameTable(
                name: "DeuRecommendation",
                newName: "DeuRecommendations");

            migrationBuilder.RenameIndex(
                name: "IX_IbtvaTeachingAIdsDeveloped_UpdatedById",
                table: "IbtvaTeachingAidsDeveloped",
                newName: "IX_IbtvaTeachingAidsDeveloped_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_IbtvaTeachingAIdsDeveloped_TypeOfAidId",
                table: "IbtvaTeachingAidsDeveloped",
                newName: "IX_IbtvaTeachingAidsDeveloped_TypeOfAidId");

            migrationBuilder.RenameIndex(
                name: "IX_IbtvaTeachingAIdsDeveloped_IbtvaProgramContentAndResourcesId",
                table: "IbtvaTeachingAidsDeveloped",
                newName: "IX_IbtvaTeachingAidsDeveloped_IbtvaProgramContentAndResourcesId");

            migrationBuilder.RenameIndex(
                name: "IX_IbtvaTeachingAIdsDeveloped_CreatedById",
                table: "IbtvaTeachingAidsDeveloped",
                newName: "IX_IbtvaTeachingAidsDeveloped_CreatedById");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "FTIProgramDetails",
                newName: "TypeId");

            migrationBuilder.RenameColumn(
                name: "ThematicArea",
                table: "FTIProgramDetails",
                newName: "ThematicAreaId");

            migrationBuilder.RenameIndex(
                name: "IX_DeuTeachingAIdsDeveloped_UpdatedById",
                table: "DeuTeachingAidsDeveloped",
                newName: "IX_DeuTeachingAidsDeveloped_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_DeuTeachingAIdsDeveloped_TypeOfAidId",
                table: "DeuTeachingAidsDeveloped",
                newName: "IX_DeuTeachingAidsDeveloped_TypeOfAidId");

            migrationBuilder.RenameIndex(
                name: "IX_DeuTeachingAIdsDeveloped_DeuProgramContentAndResourcesId",
                table: "DeuTeachingAidsDeveloped",
                newName: "IX_DeuTeachingAidsDeveloped_DeuProgramContentAndResourcesId");

            migrationBuilder.RenameIndex(
                name: "IX_DeuTeachingAIdsDeveloped_CreatedById",
                table: "DeuTeachingAidsDeveloped",
                newName: "IX_DeuTeachingAidsDeveloped_CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_IbtvaResourcePerson_UpdatedById",
                table: "IbtvaResourcePersons",
                newName: "IX_IbtvaResourcePersons_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_IbtvaResourcePerson_IbtvaProgramContentAndResourcesId",
                table: "IbtvaResourcePersons",
                newName: "IX_IbtvaResourcePersons_IbtvaProgramContentAndResourcesId");

            migrationBuilder.RenameIndex(
                name: "IX_IbtvaResourcePerson_CreatedById",
                table: "IbtvaResourcePersons",
                newName: "IX_IbtvaResourcePersons_CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_IbtvaRecommendation_UpdatedById",
                table: "IbtvaRecommendations",
                newName: "IX_IbtvaRecommendations_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_IbtvaRecommendation_IbtvaProgramDetailsId",
                table: "IbtvaRecommendations",
                newName: "IX_IbtvaRecommendations_IbtvaProgramDetailsId");

            migrationBuilder.RenameIndex(
                name: "IX_IbtvaRecommendation_CreatedById",
                table: "IbtvaRecommendations",
                newName: "IX_IbtvaRecommendations_CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_EeuResourcePerson_UpdatedById",
                table: "EeuResourcePersons",
                newName: "IX_EeuResourcePersons_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_EeuResourcePerson_EeuProgramContentAndResourcesId",
                table: "EeuResourcePersons",
                newName: "IX_EeuResourcePersons_EeuProgramContentAndResourcesId");

            migrationBuilder.RenameIndex(
                name: "IX_EeuResourcePerson_CreatedById",
                table: "EeuResourcePersons",
                newName: "IX_EeuResourcePersons_CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_EeuRecommendation_UpdatedById",
                table: "EeuRecommendations",
                newName: "IX_EeuRecommendations_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_EeuRecommendation_EeuProgramDetailsId",
                table: "EeuRecommendations",
                newName: "IX_EeuRecommendations_EeuProgramDetailsId");

            migrationBuilder.RenameIndex(
                name: "IX_EeuRecommendation_CreatedById",
                table: "EeuRecommendations",
                newName: "IX_EeuRecommendations_CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_DeuResourcePerson_UpdatedById",
                table: "DeuResourcePersons",
                newName: "IX_DeuResourcePersons_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_DeuResourcePerson_DeuProgramContentAndResourcesId",
                table: "DeuResourcePersons",
                newName: "IX_DeuResourcePersons_DeuProgramContentAndResourcesId");

            migrationBuilder.RenameIndex(
                name: "IX_DeuResourcePerson_CreatedById",
                table: "DeuResourcePersons",
                newName: "IX_DeuResourcePersons_CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_DeuRecommendation_UpdatedById",
                table: "DeuRecommendations",
                newName: "IX_DeuRecommendations_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_DeuRecommendation_DeuProgramDetailsId",
                table: "DeuRecommendations",
                newName: "IX_DeuRecommendations_DeuProgramDetailsId");

            migrationBuilder.RenameIndex(
                name: "IX_DeuRecommendation_CreatedById",
                table: "DeuRecommendations",
                newName: "IX_DeuRecommendations_CreatedById");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "StartDate",
                table: "FTIProgramDetails",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "EndDate",
                table: "FTIProgramDetails",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "CollaboratorId",
                table: "FTIProgramDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CollaboratorOther",
                table: "FTIProgramDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_IbtvaTeachingAidsDeveloped",
                table: "IbtvaTeachingAidsDeveloped",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DeuTeachingAidsDeveloped",
                table: "DeuTeachingAidsDeveloped",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IbtvaResourcePersons",
                table: "IbtvaResourcePersons",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IbtvaRecommendations",
                table: "IbtvaRecommendations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EeuResourcePersons",
                table: "EeuResourcePersons",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EeuRecommendations",
                table: "EeuRecommendations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DeuResourcePersons",
                table: "DeuResourcePersons",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DeuRecommendations",
                table: "DeuRecommendations",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_FTIProgramDetails_CategoryId",
                table: "FTIProgramDetails",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_FTIProgramDetails_CollaboratorId",
                table: "FTIProgramDetails",
                column: "CollaboratorId");

            migrationBuilder.CreateIndex(
                name: "IX_FTIProgramDetails_ThematicAreaId",
                table: "FTIProgramDetails",
                column: "ThematicAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_FTIProgramDetails_TypeId",
                table: "FTIProgramDetails",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeuRecommendations_DeuProgramDetails_DeuProgramDetailsId",
                table: "DeuRecommendations",
                column: "DeuProgramDetailsId",
                principalTable: "DeuProgramDetails",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DeuRecommendations_Users_CreatedById",
                table: "DeuRecommendations",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DeuRecommendations_Users_UpdatedById",
                table: "DeuRecommendations",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DeuResourcePersons_DeuProgramContentAndResources_DeuProgramContentAndResourcesId",
                table: "DeuResourcePersons",
                column: "DeuProgramContentAndResourcesId",
                principalTable: "DeuProgramContentAndResources",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DeuResourcePersons_Users_CreatedById",
                table: "DeuResourcePersons",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DeuResourcePersons_Users_UpdatedById",
                table: "DeuResourcePersons",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DeuTeachingAidsDeveloped_DeuProgramContentAndResources_DeuProgramContentAndResourcesId",
                table: "DeuTeachingAidsDeveloped",
                column: "DeuProgramContentAndResourcesId",
                principalTable: "DeuProgramContentAndResources",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DeuTeachingAidsDeveloped_TypeOfAids_TypeOfAidId",
                table: "DeuTeachingAidsDeveloped",
                column: "TypeOfAidId",
                principalTable: "TypeOfAids",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DeuTeachingAidsDeveloped_Users_CreatedById",
                table: "DeuTeachingAidsDeveloped",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DeuTeachingAidsDeveloped_Users_UpdatedById",
                table: "DeuTeachingAidsDeveloped",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EeuRecommendations_EeuProgramDetails_EeuProgramDetailsId",
                table: "EeuRecommendations",
                column: "EeuProgramDetailsId",
                principalTable: "EeuProgramDetails",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EeuRecommendations_Users_CreatedById",
                table: "EeuRecommendations",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EeuRecommendations_Users_UpdatedById",
                table: "EeuRecommendations",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EeuResourcePersons_EeuProgramContentAndResources_EeuProgramContentAndResourcesId",
                table: "EeuResourcePersons",
                column: "EeuProgramContentAndResourcesId",
                principalTable: "EeuProgramContentAndResources",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EeuResourcePersons_Users_CreatedById",
                table: "EeuResourcePersons",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EeuResourcePersons_Users_UpdatedById",
                table: "EeuResourcePersons",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FTIProgramDetails_Collaborators_CollaboratorId",
                table: "FTIProgramDetails",
                column: "CollaboratorId",
                principalTable: "Collaborators",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FTIProgramDetails_ProgramCategories_CategoryId",
                table: "FTIProgramDetails",
                column: "CategoryId",
                principalTable: "ProgramCategories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FTIProgramDetails_ProgramTypes_TypeId",
                table: "FTIProgramDetails",
                column: "TypeId",
                principalTable: "ProgramTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FTIProgramDetails_ThematicAreas_ThematicAreaId",
                table: "FTIProgramDetails",
                column: "ThematicAreaId",
                principalTable: "ThematicAreas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IbtvaRecommendations_IbtvaProgramDetails_IbtvaProgramDetailsId",
                table: "IbtvaRecommendations",
                column: "IbtvaProgramDetailsId",
                principalTable: "IbtvaProgramDetails",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IbtvaRecommendations_Users_CreatedById",
                table: "IbtvaRecommendations",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IbtvaRecommendations_Users_UpdatedById",
                table: "IbtvaRecommendations",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IbtvaResourcePersons_IbtvaProgramContentAndResources_IbtvaProgramContentAndResourcesId",
                table: "IbtvaResourcePersons",
                column: "IbtvaProgramContentAndResourcesId",
                principalTable: "IbtvaProgramContentAndResources",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IbtvaResourcePersons_Users_CreatedById",
                table: "IbtvaResourcePersons",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IbtvaResourcePersons_Users_UpdatedById",
                table: "IbtvaResourcePersons",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IbtvaTeachingAidsDeveloped_IbtvaProgramContentAndResources_IbtvaProgramContentAndResourcesId",
                table: "IbtvaTeachingAidsDeveloped",
                column: "IbtvaProgramContentAndResourcesId",
                principalTable: "IbtvaProgramContentAndResources",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IbtvaTeachingAidsDeveloped_TypeOfAids_TypeOfAidId",
                table: "IbtvaTeachingAidsDeveloped",
                column: "TypeOfAidId",
                principalTable: "TypeOfAids",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IbtvaTeachingAidsDeveloped_Users_CreatedById",
                table: "IbtvaTeachingAidsDeveloped",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IbtvaTeachingAidsDeveloped_Users_UpdatedById",
                table: "IbtvaTeachingAidsDeveloped",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeuRecommendations_DeuProgramDetails_DeuProgramDetailsId",
                table: "DeuRecommendations");

            migrationBuilder.DropForeignKey(
                name: "FK_DeuRecommendations_Users_CreatedById",
                table: "DeuRecommendations");

            migrationBuilder.DropForeignKey(
                name: "FK_DeuRecommendations_Users_UpdatedById",
                table: "DeuRecommendations");

            migrationBuilder.DropForeignKey(
                name: "FK_DeuResourcePersons_DeuProgramContentAndResources_DeuProgramContentAndResourcesId",
                table: "DeuResourcePersons");

            migrationBuilder.DropForeignKey(
                name: "FK_DeuResourcePersons_Users_CreatedById",
                table: "DeuResourcePersons");

            migrationBuilder.DropForeignKey(
                name: "FK_DeuResourcePersons_Users_UpdatedById",
                table: "DeuResourcePersons");

            migrationBuilder.DropForeignKey(
                name: "FK_DeuTeachingAidsDeveloped_DeuProgramContentAndResources_DeuProgramContentAndResourcesId",
                table: "DeuTeachingAidsDeveloped");

            migrationBuilder.DropForeignKey(
                name: "FK_DeuTeachingAidsDeveloped_TypeOfAids_TypeOfAidId",
                table: "DeuTeachingAidsDeveloped");

            migrationBuilder.DropForeignKey(
                name: "FK_DeuTeachingAidsDeveloped_Users_CreatedById",
                table: "DeuTeachingAidsDeveloped");

            migrationBuilder.DropForeignKey(
                name: "FK_DeuTeachingAidsDeveloped_Users_UpdatedById",
                table: "DeuTeachingAidsDeveloped");

            migrationBuilder.DropForeignKey(
                name: "FK_EeuRecommendations_EeuProgramDetails_EeuProgramDetailsId",
                table: "EeuRecommendations");

            migrationBuilder.DropForeignKey(
                name: "FK_EeuRecommendations_Users_CreatedById",
                table: "EeuRecommendations");

            migrationBuilder.DropForeignKey(
                name: "FK_EeuRecommendations_Users_UpdatedById",
                table: "EeuRecommendations");

            migrationBuilder.DropForeignKey(
                name: "FK_EeuResourcePersons_EeuProgramContentAndResources_EeuProgramContentAndResourcesId",
                table: "EeuResourcePersons");

            migrationBuilder.DropForeignKey(
                name: "FK_EeuResourcePersons_Users_CreatedById",
                table: "EeuResourcePersons");

            migrationBuilder.DropForeignKey(
                name: "FK_EeuResourcePersons_Users_UpdatedById",
                table: "EeuResourcePersons");

            migrationBuilder.DropForeignKey(
                name: "FK_FTIProgramDetails_Collaborators_CollaboratorId",
                table: "FTIProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_FTIProgramDetails_ProgramCategories_CategoryId",
                table: "FTIProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_FTIProgramDetails_ProgramTypes_TypeId",
                table: "FTIProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_FTIProgramDetails_ThematicAreas_ThematicAreaId",
                table: "FTIProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_IbtvaRecommendations_IbtvaProgramDetails_IbtvaProgramDetailsId",
                table: "IbtvaRecommendations");

            migrationBuilder.DropForeignKey(
                name: "FK_IbtvaRecommendations_Users_CreatedById",
                table: "IbtvaRecommendations");

            migrationBuilder.DropForeignKey(
                name: "FK_IbtvaRecommendations_Users_UpdatedById",
                table: "IbtvaRecommendations");

            migrationBuilder.DropForeignKey(
                name: "FK_IbtvaResourcePersons_IbtvaProgramContentAndResources_IbtvaProgramContentAndResourcesId",
                table: "IbtvaResourcePersons");

            migrationBuilder.DropForeignKey(
                name: "FK_IbtvaResourcePersons_Users_CreatedById",
                table: "IbtvaResourcePersons");

            migrationBuilder.DropForeignKey(
                name: "FK_IbtvaResourcePersons_Users_UpdatedById",
                table: "IbtvaResourcePersons");

            migrationBuilder.DropForeignKey(
                name: "FK_IbtvaTeachingAidsDeveloped_IbtvaProgramContentAndResources_IbtvaProgramContentAndResourcesId",
                table: "IbtvaTeachingAidsDeveloped");

            migrationBuilder.DropForeignKey(
                name: "FK_IbtvaTeachingAidsDeveloped_TypeOfAids_TypeOfAidId",
                table: "IbtvaTeachingAidsDeveloped");

            migrationBuilder.DropForeignKey(
                name: "FK_IbtvaTeachingAidsDeveloped_Users_CreatedById",
                table: "IbtvaTeachingAidsDeveloped");

            migrationBuilder.DropForeignKey(
                name: "FK_IbtvaTeachingAidsDeveloped_Users_UpdatedById",
                table: "IbtvaTeachingAidsDeveloped");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IbtvaTeachingAidsDeveloped",
                table: "IbtvaTeachingAidsDeveloped");

            migrationBuilder.DropIndex(
                name: "IX_FTIProgramDetails_CategoryId",
                table: "FTIProgramDetails");

            migrationBuilder.DropIndex(
                name: "IX_FTIProgramDetails_CollaboratorId",
                table: "FTIProgramDetails");

            migrationBuilder.DropIndex(
                name: "IX_FTIProgramDetails_ThematicAreaId",
                table: "FTIProgramDetails");

            migrationBuilder.DropIndex(
                name: "IX_FTIProgramDetails_TypeId",
                table: "FTIProgramDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DeuTeachingAidsDeveloped",
                table: "DeuTeachingAidsDeveloped");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IbtvaResourcePersons",
                table: "IbtvaResourcePersons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IbtvaRecommendations",
                table: "IbtvaRecommendations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EeuResourcePersons",
                table: "EeuResourcePersons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EeuRecommendations",
                table: "EeuRecommendations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DeuResourcePersons",
                table: "DeuResourcePersons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DeuRecommendations",
                table: "DeuRecommendations");

            migrationBuilder.DropColumn(
                name: "CollaboratorId",
                table: "FTIProgramDetails");

            migrationBuilder.DropColumn(
                name: "CollaboratorOther",
                table: "FTIProgramDetails");

            migrationBuilder.RenameTable(
                name: "IbtvaTeachingAidsDeveloped",
                newName: "IbtvaTeachingAIdsDeveloped");

            migrationBuilder.RenameTable(
                name: "DeuTeachingAidsDeveloped",
                newName: "DeuTeachingAIdsDeveloped");

            migrationBuilder.RenameTable(
                name: "IbtvaResourcePersons",
                newName: "IbtvaResourcePerson");

            migrationBuilder.RenameTable(
                name: "IbtvaRecommendations",
                newName: "IbtvaRecommendation");

            migrationBuilder.RenameTable(
                name: "EeuResourcePersons",
                newName: "EeuResourcePerson");

            migrationBuilder.RenameTable(
                name: "EeuRecommendations",
                newName: "EeuRecommendation");

            migrationBuilder.RenameTable(
                name: "DeuResourcePersons",
                newName: "DeuResourcePerson");

            migrationBuilder.RenameTable(
                name: "DeuRecommendations",
                newName: "DeuRecommendation");

            migrationBuilder.RenameIndex(
                name: "IX_IbtvaTeachingAidsDeveloped_UpdatedById",
                table: "IbtvaTeachingAIdsDeveloped",
                newName: "IX_IbtvaTeachingAIdsDeveloped_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_IbtvaTeachingAidsDeveloped_TypeOfAidId",
                table: "IbtvaTeachingAIdsDeveloped",
                newName: "IX_IbtvaTeachingAIdsDeveloped_TypeOfAidId");

            migrationBuilder.RenameIndex(
                name: "IX_IbtvaTeachingAidsDeveloped_IbtvaProgramContentAndResourcesId",
                table: "IbtvaTeachingAIdsDeveloped",
                newName: "IX_IbtvaTeachingAIdsDeveloped_IbtvaProgramContentAndResourcesId");

            migrationBuilder.RenameIndex(
                name: "IX_IbtvaTeachingAidsDeveloped_CreatedById",
                table: "IbtvaTeachingAIdsDeveloped",
                newName: "IX_IbtvaTeachingAIdsDeveloped_CreatedById");

            migrationBuilder.RenameColumn(
                name: "TypeId",
                table: "FTIProgramDetails",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "ThematicAreaId",
                table: "FTIProgramDetails",
                newName: "ThematicArea");

            migrationBuilder.RenameIndex(
                name: "IX_DeuTeachingAidsDeveloped_UpdatedById",
                table: "DeuTeachingAIdsDeveloped",
                newName: "IX_DeuTeachingAIdsDeveloped_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_DeuTeachingAidsDeveloped_TypeOfAidId",
                table: "DeuTeachingAIdsDeveloped",
                newName: "IX_DeuTeachingAIdsDeveloped_TypeOfAidId");

            migrationBuilder.RenameIndex(
                name: "IX_DeuTeachingAidsDeveloped_DeuProgramContentAndResourcesId",
                table: "DeuTeachingAIdsDeveloped",
                newName: "IX_DeuTeachingAIdsDeveloped_DeuProgramContentAndResourcesId");

            migrationBuilder.RenameIndex(
                name: "IX_DeuTeachingAidsDeveloped_CreatedById",
                table: "DeuTeachingAIdsDeveloped",
                newName: "IX_DeuTeachingAIdsDeveloped_CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_IbtvaResourcePersons_UpdatedById",
                table: "IbtvaResourcePerson",
                newName: "IX_IbtvaResourcePerson_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_IbtvaResourcePersons_IbtvaProgramContentAndResourcesId",
                table: "IbtvaResourcePerson",
                newName: "IX_IbtvaResourcePerson_IbtvaProgramContentAndResourcesId");

            migrationBuilder.RenameIndex(
                name: "IX_IbtvaResourcePersons_CreatedById",
                table: "IbtvaResourcePerson",
                newName: "IX_IbtvaResourcePerson_CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_IbtvaRecommendations_UpdatedById",
                table: "IbtvaRecommendation",
                newName: "IX_IbtvaRecommendation_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_IbtvaRecommendations_IbtvaProgramDetailsId",
                table: "IbtvaRecommendation",
                newName: "IX_IbtvaRecommendation_IbtvaProgramDetailsId");

            migrationBuilder.RenameIndex(
                name: "IX_IbtvaRecommendations_CreatedById",
                table: "IbtvaRecommendation",
                newName: "IX_IbtvaRecommendation_CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_EeuResourcePersons_UpdatedById",
                table: "EeuResourcePerson",
                newName: "IX_EeuResourcePerson_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_EeuResourcePersons_EeuProgramContentAndResourcesId",
                table: "EeuResourcePerson",
                newName: "IX_EeuResourcePerson_EeuProgramContentAndResourcesId");

            migrationBuilder.RenameIndex(
                name: "IX_EeuResourcePersons_CreatedById",
                table: "EeuResourcePerson",
                newName: "IX_EeuResourcePerson_CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_EeuRecommendations_UpdatedById",
                table: "EeuRecommendation",
                newName: "IX_EeuRecommendation_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_EeuRecommendations_EeuProgramDetailsId",
                table: "EeuRecommendation",
                newName: "IX_EeuRecommendation_EeuProgramDetailsId");

            migrationBuilder.RenameIndex(
                name: "IX_EeuRecommendations_CreatedById",
                table: "EeuRecommendation",
                newName: "IX_EeuRecommendation_CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_DeuResourcePersons_UpdatedById",
                table: "DeuResourcePerson",
                newName: "IX_DeuResourcePerson_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_DeuResourcePersons_DeuProgramContentAndResourcesId",
                table: "DeuResourcePerson",
                newName: "IX_DeuResourcePerson_DeuProgramContentAndResourcesId");

            migrationBuilder.RenameIndex(
                name: "IX_DeuResourcePersons_CreatedById",
                table: "DeuResourcePerson",
                newName: "IX_DeuResourcePerson_CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_DeuRecommendations_UpdatedById",
                table: "DeuRecommendation",
                newName: "IX_DeuRecommendation_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_DeuRecommendations_DeuProgramDetailsId",
                table: "DeuRecommendation",
                newName: "IX_DeuRecommendation_DeuProgramDetailsId");

            migrationBuilder.RenameIndex(
                name: "IX_DeuRecommendations_CreatedById",
                table: "DeuRecommendation",
                newName: "IX_DeuRecommendation_CreatedById");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "FTIProgramDetails",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "FTIProgramDetails",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IbtvaTeachingAIdsDeveloped",
                table: "IbtvaTeachingAIdsDeveloped",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DeuTeachingAIdsDeveloped",
                table: "DeuTeachingAIdsDeveloped",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IbtvaResourcePerson",
                table: "IbtvaResourcePerson",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IbtvaRecommendation",
                table: "IbtvaRecommendation",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EeuResourcePerson",
                table: "EeuResourcePerson",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EeuRecommendation",
                table: "EeuRecommendation",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DeuResourcePerson",
                table: "DeuResourcePerson",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DeuRecommendation",
                table: "DeuRecommendation",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DeuRecommendation_DeuProgramDetails_DeuProgramDetailsId",
                table: "DeuRecommendation",
                column: "DeuProgramDetailsId",
                principalTable: "DeuProgramDetails",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DeuRecommendation_Users_CreatedById",
                table: "DeuRecommendation",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DeuRecommendation_Users_UpdatedById",
                table: "DeuRecommendation",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DeuResourcePerson_DeuProgramContentAndResources_DeuProgramContentAndResourcesId",
                table: "DeuResourcePerson",
                column: "DeuProgramContentAndResourcesId",
                principalTable: "DeuProgramContentAndResources",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DeuResourcePerson_Users_CreatedById",
                table: "DeuResourcePerson",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DeuResourcePerson_Users_UpdatedById",
                table: "DeuResourcePerson",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DeuTeachingAIdsDeveloped_DeuProgramContentAndResources_DeuProgramContentAndResourcesId",
                table: "DeuTeachingAIdsDeveloped",
                column: "DeuProgramContentAndResourcesId",
                principalTable: "DeuProgramContentAndResources",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DeuTeachingAIdsDeveloped_TypeOfAids_TypeOfAidId",
                table: "DeuTeachingAIdsDeveloped",
                column: "TypeOfAidId",
                principalTable: "TypeOfAids",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DeuTeachingAIdsDeveloped_Users_CreatedById",
                table: "DeuTeachingAIdsDeveloped",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DeuTeachingAIdsDeveloped_Users_UpdatedById",
                table: "DeuTeachingAIdsDeveloped",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EeuRecommendation_EeuProgramDetails_EeuProgramDetailsId",
                table: "EeuRecommendation",
                column: "EeuProgramDetailsId",
                principalTable: "EeuProgramDetails",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EeuRecommendation_Users_CreatedById",
                table: "EeuRecommendation",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EeuRecommendation_Users_UpdatedById",
                table: "EeuRecommendation",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EeuResourcePerson_EeuProgramContentAndResources_EeuProgramContentAndResourcesId",
                table: "EeuResourcePerson",
                column: "EeuProgramContentAndResourcesId",
                principalTable: "EeuProgramContentAndResources",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EeuResourcePerson_Users_CreatedById",
                table: "EeuResourcePerson",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EeuResourcePerson_Users_UpdatedById",
                table: "EeuResourcePerson",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IbtvaRecommendation_IbtvaProgramDetails_IbtvaProgramDetailsId",
                table: "IbtvaRecommendation",
                column: "IbtvaProgramDetailsId",
                principalTable: "IbtvaProgramDetails",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IbtvaRecommendation_Users_CreatedById",
                table: "IbtvaRecommendation",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IbtvaRecommendation_Users_UpdatedById",
                table: "IbtvaRecommendation",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IbtvaResourcePerson_IbtvaProgramContentAndResources_IbtvaProgramContentAndResourcesId",
                table: "IbtvaResourcePerson",
                column: "IbtvaProgramContentAndResourcesId",
                principalTable: "IbtvaProgramContentAndResources",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IbtvaResourcePerson_Users_CreatedById",
                table: "IbtvaResourcePerson",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IbtvaResourcePerson_Users_UpdatedById",
                table: "IbtvaResourcePerson",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IbtvaTeachingAIdsDeveloped_IbtvaProgramContentAndResources_IbtvaProgramContentAndResourcesId",
                table: "IbtvaTeachingAIdsDeveloped",
                column: "IbtvaProgramContentAndResourcesId",
                principalTable: "IbtvaProgramContentAndResources",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IbtvaTeachingAIdsDeveloped_TypeOfAids_TypeOfAidId",
                table: "IbtvaTeachingAIdsDeveloped",
                column: "TypeOfAidId",
                principalTable: "TypeOfAids",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IbtvaTeachingAIdsDeveloped_Users_CreatedById",
                table: "IbtvaTeachingAIdsDeveloped",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IbtvaTeachingAIdsDeveloped_Users_UpdatedById",
                table: "IbtvaTeachingAIdsDeveloped",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}

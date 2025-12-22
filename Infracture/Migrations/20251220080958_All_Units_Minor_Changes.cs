using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class All_Units_Minor_Changes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AticSales");

            migrationBuilder.AddColumn<int>(
                name: "CollaborativeProgramOptionId",
                table: "StuProgramDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CollaborativeProgramOptionOther",
                table: "StuProgramDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CollaboratorId",
                table: "StuProgramDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CollaboratorOther",
                table: "StuProgramDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ParticipantFileUpload",
                table: "StuProgramDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ParticipatedAsId",
                table: "StuProgramDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CollaborativeProgramOptionId",
                table: "SametiProgramDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CollaborativeProgramOptionOther",
                table: "SametiProgramDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CollaboratorId",
                table: "SametiProgramDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CollaboratorOther",
                table: "SametiProgramDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ParticipantFileUpload",
                table: "SametiProgramDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ParticipatedAsId",
                table: "SametiProgramDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CollaborativeProgramOptionId",
                table: "NaepProgramDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CollaborativeProgramOptionOther",
                table: "NaepProgramDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CollaboratorId",
                table: "NaepProgramDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CollaboratorOther",
                table: "NaepProgramDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ParticipantFileUpload",
                table: "NaepProgramDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ParticipatedAsId",
                table: "NaepProgramDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CollaborativeProgramOptionId",
                table: "KvkProgramDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CollaborativeProgramOptionOther",
                table: "KvkProgramDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CollaboratorId",
                table: "KvkProgramDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CollaboratorOther",
                table: "KvkProgramDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CollaborativeProgramOptionId",
                table: "IbtvaProgramDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CollaborativeProgramOptionOther",
                table: "IbtvaProgramDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CollaboratorId",
                table: "IbtvaProgramDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CollaboratorOther",
                table: "IbtvaProgramDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ParticipantFileUpload",
                table: "IbtvaProgramDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ParticipatedAsId",
                table: "IbtvaProgramDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CollaborativeProgramOptionId",
                table: "EeuProgramDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CollaborativeProgramOptionOther",
                table: "EeuProgramDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CollaboratorId",
                table: "EeuProgramDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CollaboratorOther",
                table: "EeuProgramDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CollaborativeProgramOptionId",
                table: "DeuProgramDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CollaborativeProgramOptionOther",
                table: "DeuProgramDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CollaboratorId",
                table: "DeuProgramDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CollaboratorOther",
                table: "DeuProgramDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ParticipantFileUpload",
                table: "DeuProgramDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ParticipatedAsId",
                table: "DeuProgramDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PercentageOfExpenditure",
                table: "Budgets",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CollaborativeProgramOptionId",
                table: "AticProgramDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CollaborativeProgramOptionOther",
                table: "AticProgramDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CollaboratorId",
                table: "AticProgramDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CollaboratorOther",
                table: "AticProgramDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ParticipantFileUpload",
                table: "AticProgramDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ParticipatedAsId",
                table: "AticProgramDetails",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StuProgramDetails_CollaborativeProgramOptionId",
                table: "StuProgramDetails",
                column: "CollaborativeProgramOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_StuProgramDetails_CollaboratorId",
                table: "StuProgramDetails",
                column: "CollaboratorId");

            migrationBuilder.CreateIndex(
                name: "IX_StuProgramDetails_ParticipatedAsId",
                table: "StuProgramDetails",
                column: "ParticipatedAsId");

            migrationBuilder.CreateIndex(
                name: "IX_SametiProgramDetails_CollaborativeProgramOptionId",
                table: "SametiProgramDetails",
                column: "CollaborativeProgramOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_SametiProgramDetails_CollaboratorId",
                table: "SametiProgramDetails",
                column: "CollaboratorId");

            migrationBuilder.CreateIndex(
                name: "IX_SametiProgramDetails_ParticipatedAsId",
                table: "SametiProgramDetails",
                column: "ParticipatedAsId");

            migrationBuilder.CreateIndex(
                name: "IX_NaepProgramDetails_CollaborativeProgramOptionId",
                table: "NaepProgramDetails",
                column: "CollaborativeProgramOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_NaepProgramDetails_CollaboratorId",
                table: "NaepProgramDetails",
                column: "CollaboratorId");

            migrationBuilder.CreateIndex(
                name: "IX_NaepProgramDetails_ParticipatedAsId",
                table: "NaepProgramDetails",
                column: "ParticipatedAsId");

            migrationBuilder.CreateIndex(
                name: "IX_KvkProgramDetails_CollaborativeProgramOptionId",
                table: "KvkProgramDetails",
                column: "CollaborativeProgramOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_KvkProgramDetails_CollaboratorId",
                table: "KvkProgramDetails",
                column: "CollaboratorId");

            migrationBuilder.CreateIndex(
                name: "IX_IbtvaProgramDetails_CollaborativeProgramOptionId",
                table: "IbtvaProgramDetails",
                column: "CollaborativeProgramOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_IbtvaProgramDetails_CollaboratorId",
                table: "IbtvaProgramDetails",
                column: "CollaboratorId");

            migrationBuilder.CreateIndex(
                name: "IX_IbtvaProgramDetails_ParticipatedAsId",
                table: "IbtvaProgramDetails",
                column: "ParticipatedAsId");

            migrationBuilder.CreateIndex(
                name: "IX_EeuProgramDetails_CollaborativeProgramOptionId",
                table: "EeuProgramDetails",
                column: "CollaborativeProgramOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_EeuProgramDetails_CollaboratorId",
                table: "EeuProgramDetails",
                column: "CollaboratorId");

            migrationBuilder.CreateIndex(
                name: "IX_DeuProgramDetails_CollaborativeProgramOptionId",
                table: "DeuProgramDetails",
                column: "CollaborativeProgramOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_DeuProgramDetails_CollaboratorId",
                table: "DeuProgramDetails",
                column: "CollaboratorId");

            migrationBuilder.CreateIndex(
                name: "IX_DeuProgramDetails_ParticipatedAsId",
                table: "DeuProgramDetails",
                column: "ParticipatedAsId");

            migrationBuilder.CreateIndex(
                name: "IX_AticProgramDetails_CollaborativeProgramOptionId",
                table: "AticProgramDetails",
                column: "CollaborativeProgramOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_AticProgramDetails_CollaboratorId",
                table: "AticProgramDetails",
                column: "CollaboratorId");

            migrationBuilder.CreateIndex(
                name: "IX_AticProgramDetails_ParticipatedAsId",
                table: "AticProgramDetails",
                column: "ParticipatedAsId");

            migrationBuilder.AddForeignKey(
                name: "FK_AticProgramDetails_CollaborativeProgramOptions_CollaborativeProgramOptionId",
                table: "AticProgramDetails",
                column: "CollaborativeProgramOptionId",
                principalTable: "CollaborativeProgramOptions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AticProgramDetails_Collaborators_CollaboratorId",
                table: "AticProgramDetails",
                column: "CollaboratorId",
                principalTable: "Collaborators",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AticProgramDetails_Participants_ParticipatedAsId",
                table: "AticProgramDetails",
                column: "ParticipatedAsId",
                principalTable: "Participants",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DeuProgramDetails_CollaborativeProgramOptions_CollaborativeProgramOptionId",
                table: "DeuProgramDetails",
                column: "CollaborativeProgramOptionId",
                principalTable: "CollaborativeProgramOptions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DeuProgramDetails_Collaborators_CollaboratorId",
                table: "DeuProgramDetails",
                column: "CollaboratorId",
                principalTable: "Collaborators",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DeuProgramDetails_Participants_ParticipatedAsId",
                table: "DeuProgramDetails",
                column: "ParticipatedAsId",
                principalTable: "Participants",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EeuProgramDetails_CollaborativeProgramOptions_CollaborativeProgramOptionId",
                table: "EeuProgramDetails",
                column: "CollaborativeProgramOptionId",
                principalTable: "CollaborativeProgramOptions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EeuProgramDetails_Collaborators_CollaboratorId",
                table: "EeuProgramDetails",
                column: "CollaboratorId",
                principalTable: "Collaborators",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IbtvaProgramDetails_CollaborativeProgramOptions_CollaborativeProgramOptionId",
                table: "IbtvaProgramDetails",
                column: "CollaborativeProgramOptionId",
                principalTable: "CollaborativeProgramOptions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IbtvaProgramDetails_Collaborators_CollaboratorId",
                table: "IbtvaProgramDetails",
                column: "CollaboratorId",
                principalTable: "Collaborators",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IbtvaProgramDetails_Participants_ParticipatedAsId",
                table: "IbtvaProgramDetails",
                column: "ParticipatedAsId",
                principalTable: "Participants",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KvkProgramDetails_CollaborativeProgramOptions_CollaborativeProgramOptionId",
                table: "KvkProgramDetails",
                column: "CollaborativeProgramOptionId",
                principalTable: "CollaborativeProgramOptions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KvkProgramDetails_Collaborators_CollaboratorId",
                table: "KvkProgramDetails",
                column: "CollaboratorId",
                principalTable: "Collaborators",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NaepProgramDetails_CollaborativeProgramOptions_CollaborativeProgramOptionId",
                table: "NaepProgramDetails",
                column: "CollaborativeProgramOptionId",
                principalTable: "CollaborativeProgramOptions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NaepProgramDetails_Collaborators_CollaboratorId",
                table: "NaepProgramDetails",
                column: "CollaboratorId",
                principalTable: "Collaborators",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NaepProgramDetails_Participants_ParticipatedAsId",
                table: "NaepProgramDetails",
                column: "ParticipatedAsId",
                principalTable: "Participants",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SametiProgramDetails_CollaborativeProgramOptions_CollaborativeProgramOptionId",
                table: "SametiProgramDetails",
                column: "CollaborativeProgramOptionId",
                principalTable: "CollaborativeProgramOptions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SametiProgramDetails_Collaborators_CollaboratorId",
                table: "SametiProgramDetails",
                column: "CollaboratorId",
                principalTable: "Collaborators",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SametiProgramDetails_Participants_ParticipatedAsId",
                table: "SametiProgramDetails",
                column: "ParticipatedAsId",
                principalTable: "Participants",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StuProgramDetails_CollaborativeProgramOptions_CollaborativeProgramOptionId",
                table: "StuProgramDetails",
                column: "CollaborativeProgramOptionId",
                principalTable: "CollaborativeProgramOptions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StuProgramDetails_Collaborators_CollaboratorId",
                table: "StuProgramDetails",
                column: "CollaboratorId",
                principalTable: "Collaborators",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StuProgramDetails_Participants_ParticipatedAsId",
                table: "StuProgramDetails",
                column: "ParticipatedAsId",
                principalTable: "Participants",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AticProgramDetails_CollaborativeProgramOptions_CollaborativeProgramOptionId",
                table: "AticProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_AticProgramDetails_Collaborators_CollaboratorId",
                table: "AticProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_AticProgramDetails_Participants_ParticipatedAsId",
                table: "AticProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_DeuProgramDetails_CollaborativeProgramOptions_CollaborativeProgramOptionId",
                table: "DeuProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_DeuProgramDetails_Collaborators_CollaboratorId",
                table: "DeuProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_DeuProgramDetails_Participants_ParticipatedAsId",
                table: "DeuProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_EeuProgramDetails_CollaborativeProgramOptions_CollaborativeProgramOptionId",
                table: "EeuProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_EeuProgramDetails_Collaborators_CollaboratorId",
                table: "EeuProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_IbtvaProgramDetails_CollaborativeProgramOptions_CollaborativeProgramOptionId",
                table: "IbtvaProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_IbtvaProgramDetails_Collaborators_CollaboratorId",
                table: "IbtvaProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_IbtvaProgramDetails_Participants_ParticipatedAsId",
                table: "IbtvaProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_KvkProgramDetails_CollaborativeProgramOptions_CollaborativeProgramOptionId",
                table: "KvkProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_KvkProgramDetails_Collaborators_CollaboratorId",
                table: "KvkProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_NaepProgramDetails_CollaborativeProgramOptions_CollaborativeProgramOptionId",
                table: "NaepProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_NaepProgramDetails_Collaborators_CollaboratorId",
                table: "NaepProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_NaepProgramDetails_Participants_ParticipatedAsId",
                table: "NaepProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_SametiProgramDetails_CollaborativeProgramOptions_CollaborativeProgramOptionId",
                table: "SametiProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_SametiProgramDetails_Collaborators_CollaboratorId",
                table: "SametiProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_SametiProgramDetails_Participants_ParticipatedAsId",
                table: "SametiProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_StuProgramDetails_CollaborativeProgramOptions_CollaborativeProgramOptionId",
                table: "StuProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_StuProgramDetails_Collaborators_CollaboratorId",
                table: "StuProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_StuProgramDetails_Participants_ParticipatedAsId",
                table: "StuProgramDetails");

            migrationBuilder.DropIndex(
                name: "IX_StuProgramDetails_CollaborativeProgramOptionId",
                table: "StuProgramDetails");

            migrationBuilder.DropIndex(
                name: "IX_StuProgramDetails_CollaboratorId",
                table: "StuProgramDetails");

            migrationBuilder.DropIndex(
                name: "IX_StuProgramDetails_ParticipatedAsId",
                table: "StuProgramDetails");

            migrationBuilder.DropIndex(
                name: "IX_SametiProgramDetails_CollaborativeProgramOptionId",
                table: "SametiProgramDetails");

            migrationBuilder.DropIndex(
                name: "IX_SametiProgramDetails_CollaboratorId",
                table: "SametiProgramDetails");

            migrationBuilder.DropIndex(
                name: "IX_SametiProgramDetails_ParticipatedAsId",
                table: "SametiProgramDetails");

            migrationBuilder.DropIndex(
                name: "IX_NaepProgramDetails_CollaborativeProgramOptionId",
                table: "NaepProgramDetails");

            migrationBuilder.DropIndex(
                name: "IX_NaepProgramDetails_CollaboratorId",
                table: "NaepProgramDetails");

            migrationBuilder.DropIndex(
                name: "IX_NaepProgramDetails_ParticipatedAsId",
                table: "NaepProgramDetails");

            migrationBuilder.DropIndex(
                name: "IX_KvkProgramDetails_CollaborativeProgramOptionId",
                table: "KvkProgramDetails");

            migrationBuilder.DropIndex(
                name: "IX_KvkProgramDetails_CollaboratorId",
                table: "KvkProgramDetails");

            migrationBuilder.DropIndex(
                name: "IX_IbtvaProgramDetails_CollaborativeProgramOptionId",
                table: "IbtvaProgramDetails");

            migrationBuilder.DropIndex(
                name: "IX_IbtvaProgramDetails_CollaboratorId",
                table: "IbtvaProgramDetails");

            migrationBuilder.DropIndex(
                name: "IX_IbtvaProgramDetails_ParticipatedAsId",
                table: "IbtvaProgramDetails");

            migrationBuilder.DropIndex(
                name: "IX_EeuProgramDetails_CollaborativeProgramOptionId",
                table: "EeuProgramDetails");

            migrationBuilder.DropIndex(
                name: "IX_EeuProgramDetails_CollaboratorId",
                table: "EeuProgramDetails");

            migrationBuilder.DropIndex(
                name: "IX_DeuProgramDetails_CollaborativeProgramOptionId",
                table: "DeuProgramDetails");

            migrationBuilder.DropIndex(
                name: "IX_DeuProgramDetails_CollaboratorId",
                table: "DeuProgramDetails");

            migrationBuilder.DropIndex(
                name: "IX_DeuProgramDetails_ParticipatedAsId",
                table: "DeuProgramDetails");

            migrationBuilder.DropIndex(
                name: "IX_AticProgramDetails_CollaborativeProgramOptionId",
                table: "AticProgramDetails");

            migrationBuilder.DropIndex(
                name: "IX_AticProgramDetails_CollaboratorId",
                table: "AticProgramDetails");

            migrationBuilder.DropIndex(
                name: "IX_AticProgramDetails_ParticipatedAsId",
                table: "AticProgramDetails");

            migrationBuilder.DropColumn(
                name: "CollaborativeProgramOptionId",
                table: "StuProgramDetails");

            migrationBuilder.DropColumn(
                name: "CollaborativeProgramOptionOther",
                table: "StuProgramDetails");

            migrationBuilder.DropColumn(
                name: "CollaboratorId",
                table: "StuProgramDetails");

            migrationBuilder.DropColumn(
                name: "CollaboratorOther",
                table: "StuProgramDetails");

            migrationBuilder.DropColumn(
                name: "ParticipantFileUpload",
                table: "StuProgramDetails");

            migrationBuilder.DropColumn(
                name: "ParticipatedAsId",
                table: "StuProgramDetails");

            migrationBuilder.DropColumn(
                name: "CollaborativeProgramOptionId",
                table: "SametiProgramDetails");

            migrationBuilder.DropColumn(
                name: "CollaborativeProgramOptionOther",
                table: "SametiProgramDetails");

            migrationBuilder.DropColumn(
                name: "CollaboratorId",
                table: "SametiProgramDetails");

            migrationBuilder.DropColumn(
                name: "CollaboratorOther",
                table: "SametiProgramDetails");

            migrationBuilder.DropColumn(
                name: "ParticipantFileUpload",
                table: "SametiProgramDetails");

            migrationBuilder.DropColumn(
                name: "ParticipatedAsId",
                table: "SametiProgramDetails");

            migrationBuilder.DropColumn(
                name: "CollaborativeProgramOptionId",
                table: "NaepProgramDetails");

            migrationBuilder.DropColumn(
                name: "CollaborativeProgramOptionOther",
                table: "NaepProgramDetails");

            migrationBuilder.DropColumn(
                name: "CollaboratorId",
                table: "NaepProgramDetails");

            migrationBuilder.DropColumn(
                name: "CollaboratorOther",
                table: "NaepProgramDetails");

            migrationBuilder.DropColumn(
                name: "ParticipantFileUpload",
                table: "NaepProgramDetails");

            migrationBuilder.DropColumn(
                name: "ParticipatedAsId",
                table: "NaepProgramDetails");

            migrationBuilder.DropColumn(
                name: "CollaborativeProgramOptionId",
                table: "KvkProgramDetails");

            migrationBuilder.DropColumn(
                name: "CollaborativeProgramOptionOther",
                table: "KvkProgramDetails");

            migrationBuilder.DropColumn(
                name: "CollaboratorId",
                table: "KvkProgramDetails");

            migrationBuilder.DropColumn(
                name: "CollaboratorOther",
                table: "KvkProgramDetails");

            migrationBuilder.DropColumn(
                name: "CollaborativeProgramOptionId",
                table: "IbtvaProgramDetails");

            migrationBuilder.DropColumn(
                name: "CollaborativeProgramOptionOther",
                table: "IbtvaProgramDetails");

            migrationBuilder.DropColumn(
                name: "CollaboratorId",
                table: "IbtvaProgramDetails");

            migrationBuilder.DropColumn(
                name: "CollaboratorOther",
                table: "IbtvaProgramDetails");

            migrationBuilder.DropColumn(
                name: "ParticipantFileUpload",
                table: "IbtvaProgramDetails");

            migrationBuilder.DropColumn(
                name: "ParticipatedAsId",
                table: "IbtvaProgramDetails");

            migrationBuilder.DropColumn(
                name: "CollaborativeProgramOptionId",
                table: "EeuProgramDetails");

            migrationBuilder.DropColumn(
                name: "CollaborativeProgramOptionOther",
                table: "EeuProgramDetails");

            migrationBuilder.DropColumn(
                name: "CollaboratorId",
                table: "EeuProgramDetails");

            migrationBuilder.DropColumn(
                name: "CollaboratorOther",
                table: "EeuProgramDetails");

            migrationBuilder.DropColumn(
                name: "CollaborativeProgramOptionId",
                table: "DeuProgramDetails");

            migrationBuilder.DropColumn(
                name: "CollaborativeProgramOptionOther",
                table: "DeuProgramDetails");

            migrationBuilder.DropColumn(
                name: "CollaboratorId",
                table: "DeuProgramDetails");

            migrationBuilder.DropColumn(
                name: "CollaboratorOther",
                table: "DeuProgramDetails");

            migrationBuilder.DropColumn(
                name: "ParticipantFileUpload",
                table: "DeuProgramDetails");

            migrationBuilder.DropColumn(
                name: "ParticipatedAsId",
                table: "DeuProgramDetails");

            migrationBuilder.DropColumn(
                name: "PercentageOfExpenditure",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "CollaborativeProgramOptionId",
                table: "AticProgramDetails");

            migrationBuilder.DropColumn(
                name: "CollaborativeProgramOptionOther",
                table: "AticProgramDetails");

            migrationBuilder.DropColumn(
                name: "CollaboratorId",
                table: "AticProgramDetails");

            migrationBuilder.DropColumn(
                name: "CollaboratorOther",
                table: "AticProgramDetails");

            migrationBuilder.DropColumn(
                name: "ParticipantFileUpload",
                table: "AticProgramDetails");

            migrationBuilder.DropColumn(
                name: "ParticipatedAsId",
                table: "AticProgramDetails");

            migrationBuilder.CreateTable(
                name: "AticSales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApprovedById = table.Column<int>(type: "int", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    OrganizationId = table.Column<int>(type: "int", nullable: false),
                    UnitLocationId = table.Column<int>(type: "int", nullable: false),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    ApprovedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Attachements = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    FormStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FormStatusRemarks = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    QuantityType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AticSales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AticSales_OrganizationUnitLocations_UnitLocationId",
                        column: x => x.UnitLocationId,
                        principalTable: "OrganizationUnitLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AticSales_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AticSales_Users_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AticSales_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AticSales_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AticSales_ApprovedById",
                table: "AticSales",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_AticSales_CreatedById",
                table: "AticSales",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AticSales_OrganizationId",
                table: "AticSales",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_AticSales_UnitLocationId",
                table: "AticSales",
                column: "UnitLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_AticSales_UpdatedById",
                table: "AticSales",
                column: "UpdatedById");
        }
    }
}

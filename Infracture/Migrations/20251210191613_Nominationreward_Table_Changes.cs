using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Nominationreward_Table_Changes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NominationRewards_Contributions_ContributionId",
                table: "NominationRewards");

            migrationBuilder.DropForeignKey(
                name: "FK_NominationRewards_Modes_ModeId",
                table: "NominationRewards");

            migrationBuilder.DropForeignKey(
                name: "FK_NominationRewards_NominationCategories_NominationCategoryId",
                table: "NominationRewards");

            migrationBuilder.DropForeignKey(
                name: "FK_NominationRewards_Positions_PositionId",
                table: "NominationRewards");

            migrationBuilder.DropIndex(
                name: "IX_NominationRewards_ContributionId",
                table: "NominationRewards");

            migrationBuilder.DropIndex(
                name: "IX_NominationRewards_ModeId",
                table: "NominationRewards");

            migrationBuilder.DropIndex(
                name: "IX_NominationRewards_NominationCategoryId",
                table: "NominationRewards");

            migrationBuilder.DropIndex(
                name: "IX_NominationRewards_PositionId",
                table: "NominationRewards");

            migrationBuilder.DropColumn(
                name: "AwardApplicationDate",
                table: "NominationRewards");

            migrationBuilder.DropColumn(
                name: "AwardEventDate",
                table: "NominationRewards");

            migrationBuilder.DropColumn(
                name: "AwardEventTitle",
                table: "NominationRewards");

            migrationBuilder.DropColumn(
                name: "AwardFilePath",
                table: "NominationRewards");

            migrationBuilder.DropColumn(
                name: "AwardName",
                table: "NominationRewards");

            migrationBuilder.DropColumn(
                name: "AwardReceivingCertificate",
                table: "NominationRewards");

            migrationBuilder.DropColumn(
                name: "AwardReceivingPhoto",
                table: "NominationRewards");

            migrationBuilder.DropColumn(
                name: "AwardingAgency",
                table: "NominationRewards");

            migrationBuilder.DropColumn(
                name: "ContributionId",
                table: "NominationRewards");

            migrationBuilder.DropColumn(
                name: "DurationDays",
                table: "NominationRewards");

            migrationBuilder.DropColumn(
                name: "InstitutionAddress",
                table: "NominationRewards");

            migrationBuilder.DropColumn(
                name: "InstitutionBoardName",
                table: "NominationRewards");

            migrationBuilder.DropColumn(
                name: "InstitutionDesignation",
                table: "NominationRewards");

            migrationBuilder.DropColumn(
                name: "InstitutionName",
                table: "NominationRewards");

            migrationBuilder.DropColumn(
                name: "ModeId",
                table: "NominationRewards");

            migrationBuilder.DropColumn(
                name: "NominationCategoryId",
                table: "NominationRewards");

            migrationBuilder.DropColumn(
                name: "NominationDate",
                table: "NominationRewards");

            migrationBuilder.DropColumn(
                name: "NominationLetterPath",
                table: "NominationRewards");

            migrationBuilder.DropColumn(
                name: "OrganizerInstituteAddress",
                table: "NominationRewards");

            migrationBuilder.DropColumn(
                name: "OrganizerInstitutionName",
                table: "NominationRewards");

            migrationBuilder.DropColumn(
                name: "OtherContribution",
                table: "NominationRewards");

            migrationBuilder.DropColumn(
                name: "PaperDate",
                table: "NominationRewards");

            migrationBuilder.DropColumn(
                name: "PaperFilePath",
                table: "NominationRewards");

            migrationBuilder.DropColumn(
                name: "PositionFrom",
                table: "NominationRewards");

            migrationBuilder.DropColumn(
                name: "PositionId",
                table: "NominationRewards");

            migrationBuilder.DropColumn(
                name: "PositionTo",
                table: "NominationRewards");

            migrationBuilder.DropColumn(
                name: "SanctionLetterDate",
                table: "NominationRewards");

            migrationBuilder.DropColumn(
                name: "SanctionLetterFilePath",
                table: "NominationRewards");

            migrationBuilder.RenameColumn(
                name: "SpecificContributionTitle",
                table: "NominationRewards",
                newName: "OtherType");

            migrationBuilder.CreateTable(
                name: "Achievements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AchievementDetail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NominationRewardId = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Achievements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Achievements_NominationRewards_NominationRewardId",
                        column: x => x.NominationRewardId,
                        principalTable: "NominationRewards",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Achievements_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Achievements_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AwardPhotos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AwardReceivingPhoto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AwardReceivingCertificate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NominationRewardId = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AwardPhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AwardPhotos_NominationRewards_NominationRewardId",
                        column: x => x.NominationRewardId,
                        principalTable: "NominationRewards",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AwardPhotos_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AwardPhotos_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AwardRecognitions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AwardName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContributionId = table.Column<int>(type: "int", nullable: true),
                    OtherContribution = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AwardingAgency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstitutionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstitutionAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NominationRewardId = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AwardRecognitions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AwardRecognitions_Contributions_ContributionId",
                        column: x => x.ContributionId,
                        principalTable: "Contributions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AwardRecognitions_NominationRewards_NominationRewardId",
                        column: x => x.NominationRewardId,
                        principalTable: "NominationRewards",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AwardRecognitions_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AwardRecognitions_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UniversitySanctionLetterPaperPosters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SanctionLetterDate = table.Column<DateOnly>(type: "date", nullable: true),
                    SanctionLetterFilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaperDate = table.Column<DateOnly>(type: "date", nullable: true),
                    PaperFilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NominationRewardId = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UniversitySanctionLetterPaperPosters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UniversitySanctionLetterPaperPosters_NominationRewards_NominationRewardId",
                        column: x => x.NominationRewardId,
                        principalTable: "NominationRewards",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UniversitySanctionLetterPaperPosters_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UniversitySanctionLetterPaperPosters_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Achievements_CreatedById",
                table: "Achievements",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Achievements_NominationRewardId",
                table: "Achievements",
                column: "NominationRewardId");

            migrationBuilder.CreateIndex(
                name: "IX_Achievements_UpdatedById",
                table: "Achievements",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AwardPhotos_CreatedById",
                table: "AwardPhotos",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AwardPhotos_NominationRewardId",
                table: "AwardPhotos",
                column: "NominationRewardId");

            migrationBuilder.CreateIndex(
                name: "IX_AwardPhotos_UpdatedById",
                table: "AwardPhotos",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AwardRecognitions_ContributionId",
                table: "AwardRecognitions",
                column: "ContributionId");

            migrationBuilder.CreateIndex(
                name: "IX_AwardRecognitions_CreatedById",
                table: "AwardRecognitions",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AwardRecognitions_NominationRewardId",
                table: "AwardRecognitions",
                column: "NominationRewardId");

            migrationBuilder.CreateIndex(
                name: "IX_AwardRecognitions_UpdatedById",
                table: "AwardRecognitions",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_UniversitySanctionLetterPaperPosters_CreatedById",
                table: "UniversitySanctionLetterPaperPosters",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_UniversitySanctionLetterPaperPosters_NominationRewardId",
                table: "UniversitySanctionLetterPaperPosters",
                column: "NominationRewardId");

            migrationBuilder.CreateIndex(
                name: "IX_UniversitySanctionLetterPaperPosters_UpdatedById",
                table: "UniversitySanctionLetterPaperPosters",
                column: "UpdatedById");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Achievements");

            migrationBuilder.DropTable(
                name: "AwardPhotos");

            migrationBuilder.DropTable(
                name: "AwardRecognitions");

            migrationBuilder.DropTable(
                name: "UniversitySanctionLetterPaperPosters");

            migrationBuilder.RenameColumn(
                name: "OtherType",
                table: "NominationRewards",
                newName: "SpecificContributionTitle");

            migrationBuilder.AddColumn<DateOnly>(
                name: "AwardApplicationDate",
                table: "NominationRewards",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "AwardEventDate",
                table: "NominationRewards",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AwardEventTitle",
                table: "NominationRewards",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AwardFilePath",
                table: "NominationRewards",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AwardName",
                table: "NominationRewards",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AwardReceivingCertificate",
                table: "NominationRewards",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AwardReceivingPhoto",
                table: "NominationRewards",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AwardingAgency",
                table: "NominationRewards",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ContributionId",
                table: "NominationRewards",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DurationDays",
                table: "NominationRewards",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InstitutionAddress",
                table: "NominationRewards",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InstitutionBoardName",
                table: "NominationRewards",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InstitutionDesignation",
                table: "NominationRewards",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InstitutionName",
                table: "NominationRewards",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ModeId",
                table: "NominationRewards",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NominationCategoryId",
                table: "NominationRewards",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "NominationDate",
                table: "NominationRewards",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NominationLetterPath",
                table: "NominationRewards",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrganizerInstituteAddress",
                table: "NominationRewards",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrganizerInstitutionName",
                table: "NominationRewards",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherContribution",
                table: "NominationRewards",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "PaperDate",
                table: "NominationRewards",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaperFilePath",
                table: "NominationRewards",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "PositionFrom",
                table: "NominationRewards",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PositionId",
                table: "NominationRewards",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "PositionTo",
                table: "NominationRewards",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "SanctionLetterDate",
                table: "NominationRewards",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SanctionLetterFilePath",
                table: "NominationRewards",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_NominationRewards_ContributionId",
                table: "NominationRewards",
                column: "ContributionId");

            migrationBuilder.CreateIndex(
                name: "IX_NominationRewards_ModeId",
                table: "NominationRewards",
                column: "ModeId");

            migrationBuilder.CreateIndex(
                name: "IX_NominationRewards_NominationCategoryId",
                table: "NominationRewards",
                column: "NominationCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_NominationRewards_PositionId",
                table: "NominationRewards",
                column: "PositionId");

            migrationBuilder.AddForeignKey(
                name: "FK_NominationRewards_Contributions_ContributionId",
                table: "NominationRewards",
                column: "ContributionId",
                principalTable: "Contributions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NominationRewards_Modes_ModeId",
                table: "NominationRewards",
                column: "ModeId",
                principalTable: "Modes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NominationRewards_NominationCategories_NominationCategoryId",
                table: "NominationRewards",
                column: "NominationCategoryId",
                principalTable: "NominationCategories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NominationRewards_Positions_PositionId",
                table: "NominationRewards",
                column: "PositionId",
                principalTable: "Positions",
                principalColumn: "Id");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Kvk_Atic_NewFields_Added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResourceType",
                table: "StuResourcePersons");

            migrationBuilder.DropColumn(
                name: "Responsibility",
                table: "StuResourcePersons");

            migrationBuilder.DropColumn(
                name: "ResourceType",
                table: "SametiResourcePersons");

            migrationBuilder.DropColumn(
                name: "Responsibility",
                table: "SametiResourcePersons");

            migrationBuilder.DropColumn(
                name: "ResourceType",
                table: "NaepResourcePersons");

            migrationBuilder.DropColumn(
                name: "Responsibility",
                table: "NaepResourcePersons");

            migrationBuilder.DropColumn(
                name: "Responsibility",
                table: "KvkResourcePersons");

            migrationBuilder.DropColumn(
                name: "ResourceType",
                table: "IbtvaResourcePersons");

            migrationBuilder.DropColumn(
                name: "Responsibility",
                table: "IbtvaResourcePersons");

            migrationBuilder.DropColumn(
                name: "ResourceType",
                table: "FtiResourcePersons");

            migrationBuilder.DropColumn(
                name: "Responsibility",
                table: "FtiResourcePersons");

            migrationBuilder.DropColumn(
                name: "ResourceType",
                table: "EeuResourcePersons");

            migrationBuilder.DropColumn(
                name: "Responsibility",
                table: "EeuResourcePersons");

            migrationBuilder.DropColumn(
                name: "ResourceType",
                table: "DeuResourcePersons");

            migrationBuilder.DropColumn(
                name: "Responsibility",
                table: "DeuResourcePersons");

            migrationBuilder.DropColumn(
                name: "ResourceType",
                table: "AticResourcePersons");

            migrationBuilder.DropColumn(
                name: "Responsibility",
                table: "AticResourcePersons");

            migrationBuilder.AddColumn<int>(
                name: "ResourceTypeId",
                table: "StuResourcePersons",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ResponsibilityId",
                table: "StuResourcePersons",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ResourceTypeId",
                table: "SametiResourcePersons",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ResponsibilityId",
                table: "SametiResourcePersons",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ResourceTypeId",
                table: "NaepResourcePersons",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ResponsibilityId",
                table: "NaepResourcePersons",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ResponsibilityId",
                table: "KvkResourcePersons",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DistrictId",
                table: "KvkProgramDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EventNamesId",
                table: "KvkProgramDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Facilitator",
                table: "KvkProgramDetails",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherEventNames",
                table: "KvkProgramDetails",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherLocation",
                table: "KvkProgramDetails",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "PaperPosterAbstractDate",
                table: "KvkProgramDetails",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaperPosterAbstractFile",
                table: "KvkProgramDetails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PlaceId",
                table: "KvkProgramDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PurposeOfVisit",
                table: "KvkProgramDetails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SampleAnalyzed",
                table: "KvkProgramDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SampleCollected",
                table: "KvkProgramDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TargetFarmersId",
                table: "KvkProgramDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TypeTopic",
                table: "KvkProgramDetails",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "UniversityPermissionLetterDate",
                table: "KvkProgramDetails",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UniversityPermissionLetterFile",
                table: "KvkProgramDetails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VAPOptionsId",
                table: "KvkProgramDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ZoneOptionsId",
                table: "KvkProgramDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ZonesOptionsId",
                table: "KvkProgramDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ResourceTypeId",
                table: "IbtvaResourcePersons",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ResponsibilityId",
                table: "IbtvaResourcePersons",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ResourceTypeId",
                table: "FtiResourcePersons",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ResponsibilityId",
                table: "FtiResourcePersons",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ResourceTypeId",
                table: "EeuResourcePersons",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ResponsibilityId",
                table: "EeuResourcePersons",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ResourceTypeId",
                table: "DeuResourcePersons",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ResponsibilityId",
                table: "DeuResourcePersons",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ResourceTypeId",
                table: "AticResourcePersons",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ResponsibilityId",
                table: "AticResourcePersons",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DistrictId",
                table: "AticProgramDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EventNamesId",
                table: "AticProgramDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Facilitator",
                table: "AticProgramDetails",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherEventNames",
                table: "AticProgramDetails",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherLocation",
                table: "AticProgramDetails",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "PaperPosterAbstractDate",
                table: "AticProgramDetails",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaperPosterAbstractFile",
                table: "AticProgramDetails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PlaceId",
                table: "AticProgramDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PurposeOfVisit",
                table: "AticProgramDetails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SampleAnalyzed",
                table: "AticProgramDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SampleCollected",
                table: "AticProgramDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TargetFarmersId",
                table: "AticProgramDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TypeTopic",
                table: "AticProgramDetails",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "UniversityPermissionLetterDate",
                table: "AticProgramDetails",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UniversityPermissionLetterFile",
                table: "AticProgramDetails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VAPOptionsId",
                table: "AticProgramDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ZoneOptionsId",
                table: "AticProgramDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ZonesOptionsId",
                table: "AticProgramDetails",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "VAPOptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VAPOptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ZoneOptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZoneOptions", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StuResourcePersons_ResourceTypeId",
                table: "StuResourcePersons",
                column: "ResourceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_StuResourcePersons_ResponsibilityId",
                table: "StuResourcePersons",
                column: "ResponsibilityId");

            migrationBuilder.CreateIndex(
                name: "IX_SametiResourcePersons_ResourceTypeId",
                table: "SametiResourcePersons",
                column: "ResourceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SametiResourcePersons_ResponsibilityId",
                table: "SametiResourcePersons",
                column: "ResponsibilityId");

            migrationBuilder.CreateIndex(
                name: "IX_NaepResourcePersons_ResourceTypeId",
                table: "NaepResourcePersons",
                column: "ResourceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_NaepResourcePersons_ResponsibilityId",
                table: "NaepResourcePersons",
                column: "ResponsibilityId");

            migrationBuilder.CreateIndex(
                name: "IX_KvkResourcePersons_ResponsibilityId",
                table: "KvkResourcePersons",
                column: "ResponsibilityId");

            migrationBuilder.CreateIndex(
                name: "IX_KvkProgramDetails_DistrictId",
                table: "KvkProgramDetails",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_KvkProgramDetails_EventNamesId",
                table: "KvkProgramDetails",
                column: "EventNamesId");

            migrationBuilder.CreateIndex(
                name: "IX_KvkProgramDetails_PlaceId",
                table: "KvkProgramDetails",
                column: "PlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_KvkProgramDetails_TargetFarmersId",
                table: "KvkProgramDetails",
                column: "TargetFarmersId");

            migrationBuilder.CreateIndex(
                name: "IX_KvkProgramDetails_VAPOptionsId",
                table: "KvkProgramDetails",
                column: "VAPOptionsId");

            migrationBuilder.CreateIndex(
                name: "IX_KvkProgramDetails_ZoneOptionsId",
                table: "KvkProgramDetails",
                column: "ZoneOptionsId");

            migrationBuilder.CreateIndex(
                name: "IX_IbtvaResourcePersons_ResourceTypeId",
                table: "IbtvaResourcePersons",
                column: "ResourceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_IbtvaResourcePersons_ResponsibilityId",
                table: "IbtvaResourcePersons",
                column: "ResponsibilityId");

            migrationBuilder.CreateIndex(
                name: "IX_FtiResourcePersons_ResourceTypeId",
                table: "FtiResourcePersons",
                column: "ResourceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_FtiResourcePersons_ResponsibilityId",
                table: "FtiResourcePersons",
                column: "ResponsibilityId");

            migrationBuilder.CreateIndex(
                name: "IX_EeuResourcePersons_ResourceTypeId",
                table: "EeuResourcePersons",
                column: "ResourceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EeuResourcePersons_ResponsibilityId",
                table: "EeuResourcePersons",
                column: "ResponsibilityId");

            migrationBuilder.CreateIndex(
                name: "IX_DeuResourcePersons_ResourceTypeId",
                table: "DeuResourcePersons",
                column: "ResourceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DeuResourcePersons_ResponsibilityId",
                table: "DeuResourcePersons",
                column: "ResponsibilityId");

            migrationBuilder.CreateIndex(
                name: "IX_AticResourcePersons_ResourceTypeId",
                table: "AticResourcePersons",
                column: "ResourceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AticResourcePersons_ResponsibilityId",
                table: "AticResourcePersons",
                column: "ResponsibilityId");

            migrationBuilder.CreateIndex(
                name: "IX_AticProgramDetails_DistrictId",
                table: "AticProgramDetails",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_AticProgramDetails_EventNamesId",
                table: "AticProgramDetails",
                column: "EventNamesId");

            migrationBuilder.CreateIndex(
                name: "IX_AticProgramDetails_PlaceId",
                table: "AticProgramDetails",
                column: "PlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_AticProgramDetails_TargetFarmersId",
                table: "AticProgramDetails",
                column: "TargetFarmersId");

            migrationBuilder.CreateIndex(
                name: "IX_AticProgramDetails_VAPOptionsId",
                table: "AticProgramDetails",
                column: "VAPOptionsId");

            migrationBuilder.CreateIndex(
                name: "IX_AticProgramDetails_ZoneOptionsId",
                table: "AticProgramDetails",
                column: "ZoneOptionsId");

            migrationBuilder.AddForeignKey(
                name: "FK_AticProgramDetails_Districts_DistrictId",
                table: "AticProgramDetails",
                column: "DistrictId",
                principalTable: "Districts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AticProgramDetails_Districts_PlaceId",
                table: "AticProgramDetails",
                column: "PlaceId",
                principalTable: "Districts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AticProgramDetails_EventNames_EventNamesId",
                table: "AticProgramDetails",
                column: "EventNamesId",
                principalTable: "EventNames",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AticProgramDetails_TargetFarmers_TargetFarmersId",
                table: "AticProgramDetails",
                column: "TargetFarmersId",
                principalTable: "TargetFarmers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AticProgramDetails_VAPOptions_VAPOptionsId",
                table: "AticProgramDetails",
                column: "VAPOptionsId",
                principalTable: "VAPOptions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AticProgramDetails_ZoneOptions_ZoneOptionsId",
                table: "AticProgramDetails",
                column: "ZoneOptionsId",
                principalTable: "ZoneOptions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AticResourcePersons_ResourceTypes_ResourceTypeId",
                table: "AticResourcePersons",
                column: "ResourceTypeId",
                principalTable: "ResourceTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AticResourcePersons_Responsibilities_ResponsibilityId",
                table: "AticResourcePersons",
                column: "ResponsibilityId",
                principalTable: "Responsibilities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DeuResourcePersons_ResourceTypes_ResourceTypeId",
                table: "DeuResourcePersons",
                column: "ResourceTypeId",
                principalTable: "ResourceTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DeuResourcePersons_Responsibilities_ResponsibilityId",
                table: "DeuResourcePersons",
                column: "ResponsibilityId",
                principalTable: "Responsibilities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EeuResourcePersons_ResourceTypes_ResourceTypeId",
                table: "EeuResourcePersons",
                column: "ResourceTypeId",
                principalTable: "ResourceTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EeuResourcePersons_Responsibilities_ResponsibilityId",
                table: "EeuResourcePersons",
                column: "ResponsibilityId",
                principalTable: "Responsibilities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FtiResourcePersons_ResourceTypes_ResourceTypeId",
                table: "FtiResourcePersons",
                column: "ResourceTypeId",
                principalTable: "ResourceTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FtiResourcePersons_Responsibilities_ResponsibilityId",
                table: "FtiResourcePersons",
                column: "ResponsibilityId",
                principalTable: "Responsibilities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IbtvaResourcePersons_ResourceTypes_ResourceTypeId",
                table: "IbtvaResourcePersons",
                column: "ResourceTypeId",
                principalTable: "ResourceTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IbtvaResourcePersons_Responsibilities_ResponsibilityId",
                table: "IbtvaResourcePersons",
                column: "ResponsibilityId",
                principalTable: "Responsibilities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KvkProgramDetails_Districts_DistrictId",
                table: "KvkProgramDetails",
                column: "DistrictId",
                principalTable: "Districts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KvkProgramDetails_Districts_PlaceId",
                table: "KvkProgramDetails",
                column: "PlaceId",
                principalTable: "Districts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KvkProgramDetails_EventNames_EventNamesId",
                table: "KvkProgramDetails",
                column: "EventNamesId",
                principalTable: "EventNames",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KvkProgramDetails_TargetFarmers_TargetFarmersId",
                table: "KvkProgramDetails",
                column: "TargetFarmersId",
                principalTable: "TargetFarmers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KvkProgramDetails_VAPOptions_VAPOptionsId",
                table: "KvkProgramDetails",
                column: "VAPOptionsId",
                principalTable: "VAPOptions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KvkProgramDetails_ZoneOptions_ZoneOptionsId",
                table: "KvkProgramDetails",
                column: "ZoneOptionsId",
                principalTable: "ZoneOptions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KvkResourcePersons_Responsibilities_ResponsibilityId",
                table: "KvkResourcePersons",
                column: "ResponsibilityId",
                principalTable: "Responsibilities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NaepResourcePersons_ResourceTypes_ResourceTypeId",
                table: "NaepResourcePersons",
                column: "ResourceTypeId",
                principalTable: "ResourceTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NaepResourcePersons_Responsibilities_ResponsibilityId",
                table: "NaepResourcePersons",
                column: "ResponsibilityId",
                principalTable: "Responsibilities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SametiResourcePersons_ResourceTypes_ResourceTypeId",
                table: "SametiResourcePersons",
                column: "ResourceTypeId",
                principalTable: "ResourceTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SametiResourcePersons_Responsibilities_ResponsibilityId",
                table: "SametiResourcePersons",
                column: "ResponsibilityId",
                principalTable: "Responsibilities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StuResourcePersons_ResourceTypes_ResourceTypeId",
                table: "StuResourcePersons",
                column: "ResourceTypeId",
                principalTable: "ResourceTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StuResourcePersons_Responsibilities_ResponsibilityId",
                table: "StuResourcePersons",
                column: "ResponsibilityId",
                principalTable: "Responsibilities",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AticProgramDetails_Districts_DistrictId",
                table: "AticProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_AticProgramDetails_Districts_PlaceId",
                table: "AticProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_AticProgramDetails_EventNames_EventNamesId",
                table: "AticProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_AticProgramDetails_TargetFarmers_TargetFarmersId",
                table: "AticProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_AticProgramDetails_VAPOptions_VAPOptionsId",
                table: "AticProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_AticProgramDetails_ZoneOptions_ZoneOptionsId",
                table: "AticProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_AticResourcePersons_ResourceTypes_ResourceTypeId",
                table: "AticResourcePersons");

            migrationBuilder.DropForeignKey(
                name: "FK_AticResourcePersons_Responsibilities_ResponsibilityId",
                table: "AticResourcePersons");

            migrationBuilder.DropForeignKey(
                name: "FK_DeuResourcePersons_ResourceTypes_ResourceTypeId",
                table: "DeuResourcePersons");

            migrationBuilder.DropForeignKey(
                name: "FK_DeuResourcePersons_Responsibilities_ResponsibilityId",
                table: "DeuResourcePersons");

            migrationBuilder.DropForeignKey(
                name: "FK_EeuResourcePersons_ResourceTypes_ResourceTypeId",
                table: "EeuResourcePersons");

            migrationBuilder.DropForeignKey(
                name: "FK_EeuResourcePersons_Responsibilities_ResponsibilityId",
                table: "EeuResourcePersons");

            migrationBuilder.DropForeignKey(
                name: "FK_FtiResourcePersons_ResourceTypes_ResourceTypeId",
                table: "FtiResourcePersons");

            migrationBuilder.DropForeignKey(
                name: "FK_FtiResourcePersons_Responsibilities_ResponsibilityId",
                table: "FtiResourcePersons");

            migrationBuilder.DropForeignKey(
                name: "FK_IbtvaResourcePersons_ResourceTypes_ResourceTypeId",
                table: "IbtvaResourcePersons");

            migrationBuilder.DropForeignKey(
                name: "FK_IbtvaResourcePersons_Responsibilities_ResponsibilityId",
                table: "IbtvaResourcePersons");

            migrationBuilder.DropForeignKey(
                name: "FK_KvkProgramDetails_Districts_DistrictId",
                table: "KvkProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_KvkProgramDetails_Districts_PlaceId",
                table: "KvkProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_KvkProgramDetails_EventNames_EventNamesId",
                table: "KvkProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_KvkProgramDetails_TargetFarmers_TargetFarmersId",
                table: "KvkProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_KvkProgramDetails_VAPOptions_VAPOptionsId",
                table: "KvkProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_KvkProgramDetails_ZoneOptions_ZoneOptionsId",
                table: "KvkProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_KvkResourcePersons_Responsibilities_ResponsibilityId",
                table: "KvkResourcePersons");

            migrationBuilder.DropForeignKey(
                name: "FK_NaepResourcePersons_ResourceTypes_ResourceTypeId",
                table: "NaepResourcePersons");

            migrationBuilder.DropForeignKey(
                name: "FK_NaepResourcePersons_Responsibilities_ResponsibilityId",
                table: "NaepResourcePersons");

            migrationBuilder.DropForeignKey(
                name: "FK_SametiResourcePersons_ResourceTypes_ResourceTypeId",
                table: "SametiResourcePersons");

            migrationBuilder.DropForeignKey(
                name: "FK_SametiResourcePersons_Responsibilities_ResponsibilityId",
                table: "SametiResourcePersons");

            migrationBuilder.DropForeignKey(
                name: "FK_StuResourcePersons_ResourceTypes_ResourceTypeId",
                table: "StuResourcePersons");

            migrationBuilder.DropForeignKey(
                name: "FK_StuResourcePersons_Responsibilities_ResponsibilityId",
                table: "StuResourcePersons");

            migrationBuilder.DropTable(
                name: "VAPOptions");

            migrationBuilder.DropTable(
                name: "ZoneOptions");

            migrationBuilder.DropIndex(
                name: "IX_StuResourcePersons_ResourceTypeId",
                table: "StuResourcePersons");

            migrationBuilder.DropIndex(
                name: "IX_StuResourcePersons_ResponsibilityId",
                table: "StuResourcePersons");

            migrationBuilder.DropIndex(
                name: "IX_SametiResourcePersons_ResourceTypeId",
                table: "SametiResourcePersons");

            migrationBuilder.DropIndex(
                name: "IX_SametiResourcePersons_ResponsibilityId",
                table: "SametiResourcePersons");

            migrationBuilder.DropIndex(
                name: "IX_NaepResourcePersons_ResourceTypeId",
                table: "NaepResourcePersons");

            migrationBuilder.DropIndex(
                name: "IX_NaepResourcePersons_ResponsibilityId",
                table: "NaepResourcePersons");

            migrationBuilder.DropIndex(
                name: "IX_KvkResourcePersons_ResponsibilityId",
                table: "KvkResourcePersons");

            migrationBuilder.DropIndex(
                name: "IX_KvkProgramDetails_DistrictId",
                table: "KvkProgramDetails");

            migrationBuilder.DropIndex(
                name: "IX_KvkProgramDetails_EventNamesId",
                table: "KvkProgramDetails");

            migrationBuilder.DropIndex(
                name: "IX_KvkProgramDetails_PlaceId",
                table: "KvkProgramDetails");

            migrationBuilder.DropIndex(
                name: "IX_KvkProgramDetails_TargetFarmersId",
                table: "KvkProgramDetails");

            migrationBuilder.DropIndex(
                name: "IX_KvkProgramDetails_VAPOptionsId",
                table: "KvkProgramDetails");

            migrationBuilder.DropIndex(
                name: "IX_KvkProgramDetails_ZoneOptionsId",
                table: "KvkProgramDetails");

            migrationBuilder.DropIndex(
                name: "IX_IbtvaResourcePersons_ResourceTypeId",
                table: "IbtvaResourcePersons");

            migrationBuilder.DropIndex(
                name: "IX_IbtvaResourcePersons_ResponsibilityId",
                table: "IbtvaResourcePersons");

            migrationBuilder.DropIndex(
                name: "IX_FtiResourcePersons_ResourceTypeId",
                table: "FtiResourcePersons");

            migrationBuilder.DropIndex(
                name: "IX_FtiResourcePersons_ResponsibilityId",
                table: "FtiResourcePersons");

            migrationBuilder.DropIndex(
                name: "IX_EeuResourcePersons_ResourceTypeId",
                table: "EeuResourcePersons");

            migrationBuilder.DropIndex(
                name: "IX_EeuResourcePersons_ResponsibilityId",
                table: "EeuResourcePersons");

            migrationBuilder.DropIndex(
                name: "IX_DeuResourcePersons_ResourceTypeId",
                table: "DeuResourcePersons");

            migrationBuilder.DropIndex(
                name: "IX_DeuResourcePersons_ResponsibilityId",
                table: "DeuResourcePersons");

            migrationBuilder.DropIndex(
                name: "IX_AticResourcePersons_ResourceTypeId",
                table: "AticResourcePersons");

            migrationBuilder.DropIndex(
                name: "IX_AticResourcePersons_ResponsibilityId",
                table: "AticResourcePersons");

            migrationBuilder.DropIndex(
                name: "IX_AticProgramDetails_DistrictId",
                table: "AticProgramDetails");

            migrationBuilder.DropIndex(
                name: "IX_AticProgramDetails_EventNamesId",
                table: "AticProgramDetails");

            migrationBuilder.DropIndex(
                name: "IX_AticProgramDetails_PlaceId",
                table: "AticProgramDetails");

            migrationBuilder.DropIndex(
                name: "IX_AticProgramDetails_TargetFarmersId",
                table: "AticProgramDetails");

            migrationBuilder.DropIndex(
                name: "IX_AticProgramDetails_VAPOptionsId",
                table: "AticProgramDetails");

            migrationBuilder.DropIndex(
                name: "IX_AticProgramDetails_ZoneOptionsId",
                table: "AticProgramDetails");

            migrationBuilder.DropColumn(
                name: "ResourceTypeId",
                table: "StuResourcePersons");

            migrationBuilder.DropColumn(
                name: "ResponsibilityId",
                table: "StuResourcePersons");

            migrationBuilder.DropColumn(
                name: "ResourceTypeId",
                table: "SametiResourcePersons");

            migrationBuilder.DropColumn(
                name: "ResponsibilityId",
                table: "SametiResourcePersons");

            migrationBuilder.DropColumn(
                name: "ResourceTypeId",
                table: "NaepResourcePersons");

            migrationBuilder.DropColumn(
                name: "ResponsibilityId",
                table: "NaepResourcePersons");

            migrationBuilder.DropColumn(
                name: "ResponsibilityId",
                table: "KvkResourcePersons");

            migrationBuilder.DropColumn(
                name: "DistrictId",
                table: "KvkProgramDetails");

            migrationBuilder.DropColumn(
                name: "EventNamesId",
                table: "KvkProgramDetails");

            migrationBuilder.DropColumn(
                name: "Facilitator",
                table: "KvkProgramDetails");

            migrationBuilder.DropColumn(
                name: "OtherEventNames",
                table: "KvkProgramDetails");

            migrationBuilder.DropColumn(
                name: "OtherLocation",
                table: "KvkProgramDetails");

            migrationBuilder.DropColumn(
                name: "PaperPosterAbstractDate",
                table: "KvkProgramDetails");

            migrationBuilder.DropColumn(
                name: "PaperPosterAbstractFile",
                table: "KvkProgramDetails");

            migrationBuilder.DropColumn(
                name: "PlaceId",
                table: "KvkProgramDetails");

            migrationBuilder.DropColumn(
                name: "PurposeOfVisit",
                table: "KvkProgramDetails");

            migrationBuilder.DropColumn(
                name: "SampleAnalyzed",
                table: "KvkProgramDetails");

            migrationBuilder.DropColumn(
                name: "SampleCollected",
                table: "KvkProgramDetails");

            migrationBuilder.DropColumn(
                name: "TargetFarmersId",
                table: "KvkProgramDetails");

            migrationBuilder.DropColumn(
                name: "TypeTopic",
                table: "KvkProgramDetails");

            migrationBuilder.DropColumn(
                name: "UniversityPermissionLetterDate",
                table: "KvkProgramDetails");

            migrationBuilder.DropColumn(
                name: "UniversityPermissionLetterFile",
                table: "KvkProgramDetails");

            migrationBuilder.DropColumn(
                name: "VAPOptionsId",
                table: "KvkProgramDetails");

            migrationBuilder.DropColumn(
                name: "ZoneOptionsId",
                table: "KvkProgramDetails");

            migrationBuilder.DropColumn(
                name: "ZonesOptionsId",
                table: "KvkProgramDetails");

            migrationBuilder.DropColumn(
                name: "ResourceTypeId",
                table: "IbtvaResourcePersons");

            migrationBuilder.DropColumn(
                name: "ResponsibilityId",
                table: "IbtvaResourcePersons");

            migrationBuilder.DropColumn(
                name: "ResourceTypeId",
                table: "FtiResourcePersons");

            migrationBuilder.DropColumn(
                name: "ResponsibilityId",
                table: "FtiResourcePersons");

            migrationBuilder.DropColumn(
                name: "ResourceTypeId",
                table: "EeuResourcePersons");

            migrationBuilder.DropColumn(
                name: "ResponsibilityId",
                table: "EeuResourcePersons");

            migrationBuilder.DropColumn(
                name: "ResourceTypeId",
                table: "DeuResourcePersons");

            migrationBuilder.DropColumn(
                name: "ResponsibilityId",
                table: "DeuResourcePersons");

            migrationBuilder.DropColumn(
                name: "ResourceTypeId",
                table: "AticResourcePersons");

            migrationBuilder.DropColumn(
                name: "ResponsibilityId",
                table: "AticResourcePersons");

            migrationBuilder.DropColumn(
                name: "DistrictId",
                table: "AticProgramDetails");

            migrationBuilder.DropColumn(
                name: "EventNamesId",
                table: "AticProgramDetails");

            migrationBuilder.DropColumn(
                name: "Facilitator",
                table: "AticProgramDetails");

            migrationBuilder.DropColumn(
                name: "OtherEventNames",
                table: "AticProgramDetails");

            migrationBuilder.DropColumn(
                name: "OtherLocation",
                table: "AticProgramDetails");

            migrationBuilder.DropColumn(
                name: "PaperPosterAbstractDate",
                table: "AticProgramDetails");

            migrationBuilder.DropColumn(
                name: "PaperPosterAbstractFile",
                table: "AticProgramDetails");

            migrationBuilder.DropColumn(
                name: "PlaceId",
                table: "AticProgramDetails");

            migrationBuilder.DropColumn(
                name: "PurposeOfVisit",
                table: "AticProgramDetails");

            migrationBuilder.DropColumn(
                name: "SampleAnalyzed",
                table: "AticProgramDetails");

            migrationBuilder.DropColumn(
                name: "SampleCollected",
                table: "AticProgramDetails");

            migrationBuilder.DropColumn(
                name: "TargetFarmersId",
                table: "AticProgramDetails");

            migrationBuilder.DropColumn(
                name: "TypeTopic",
                table: "AticProgramDetails");

            migrationBuilder.DropColumn(
                name: "UniversityPermissionLetterDate",
                table: "AticProgramDetails");

            migrationBuilder.DropColumn(
                name: "UniversityPermissionLetterFile",
                table: "AticProgramDetails");

            migrationBuilder.DropColumn(
                name: "VAPOptionsId",
                table: "AticProgramDetails");

            migrationBuilder.DropColumn(
                name: "ZoneOptionsId",
                table: "AticProgramDetails");

            migrationBuilder.DropColumn(
                name: "ZonesOptionsId",
                table: "AticProgramDetails");

            migrationBuilder.AddColumn<int>(
                name: "ResourceType",
                table: "StuResourcePersons",
                type: "int",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Responsibility",
                table: "StuResourcePersons",
                type: "int",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ResourceType",
                table: "SametiResourcePersons",
                type: "int",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Responsibility",
                table: "SametiResourcePersons",
                type: "int",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ResourceType",
                table: "NaepResourcePersons",
                type: "int",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Responsibility",
                table: "NaepResourcePersons",
                type: "int",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Responsibility",
                table: "KvkResourcePersons",
                type: "int",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ResourceType",
                table: "IbtvaResourcePersons",
                type: "int",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Responsibility",
                table: "IbtvaResourcePersons",
                type: "int",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ResourceType",
                table: "FtiResourcePersons",
                type: "int",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Responsibility",
                table: "FtiResourcePersons",
                type: "int",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ResourceType",
                table: "EeuResourcePersons",
                type: "int",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Responsibility",
                table: "EeuResourcePersons",
                type: "int",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ResourceType",
                table: "DeuResourcePersons",
                type: "int",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Responsibility",
                table: "DeuResourcePersons",
                type: "int",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ResourceType",
                table: "AticResourcePersons",
                type: "int",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Responsibility",
                table: "AticResourcePersons",
                type: "int",
                maxLength: 250,
                nullable: true);
        }
    }
}

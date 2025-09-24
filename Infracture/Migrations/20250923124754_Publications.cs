using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Publications : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Publications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    OtherPublication = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModeId = table.Column<int>(type: "int", nullable: true),
                    ModePublication = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegionId = table.Column<int>(type: "int", nullable: true),
                    MJASFormat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PublicationTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PublicationJournalTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PublicationYear = table.Column<int>(type: "int", nullable: true),
                    PublicationVolume = table.Column<int>(type: "int", nullable: true),
                    PublicationIssue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PublicationPagesFrom = table.Column<int>(type: "int", nullable: true),
                    PublicationPagesTo = table.Column<int>(type: "int", nullable: true),
                    PublicationISBN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PublicationUniNumber = table.Column<int>(type: "int", nullable: true),
                    PublicationNAAS = table.Column<int>(type: "int", nullable: true),
                    PublicationImpact = table.Column<int>(type: "int", nullable: true),
                    PublicationWebLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PublicationCover = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PublicationWhole = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SourceId = table.Column<int>(type: "int", nullable: true),
                    Funds = table.Column<int>(type: "int", nullable: true),
                    SponsorDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PermissionLetterDate = table.Column<DateOnly>(type: "date", nullable: true),
                    PermissionLetterUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PublicationDate = table.Column<DateOnly>(type: "date", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Attachements = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnitLocationId = table.Column<int>(type: "int", nullable: false),
                    OrganizationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Publications_Modes_ModeId",
                        column: x => x.ModeId,
                        principalTable: "Modes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Publications_OrganizationUnitLocations_UnitLocationId",
                        column: x => x.UnitLocationId,
                        principalTable: "OrganizationUnitLocations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Publications_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Publications_ParticipatedSources_SourceId",
                        column: x => x.SourceId,
                        principalTable: "ParticipatedSources",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Publications_PublicationCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "PublicationCategories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Publications_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Publications_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Publications_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ExtensionLiteratures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublicationId = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<DateOnly>(type: "date", nullable: true),
                    AmountPerCopy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberOfCopies = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalAmount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtensionLiteratures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExtensionLiteratures_Publications_PublicationId",
                        column: x => x.PublicationId,
                        principalTable: "Publications",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ExtensionLiteratures_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ExtensionLiteratures_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PublisherDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublicationId = table.Column<int>(type: "int", nullable: true),
                    PublisherBrochure = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PublisherName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PublisherInstitutionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PublisherAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublisherDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PublisherDetails_Publications_PublicationId",
                        column: x => x.PublicationId,
                        principalTable: "Publications",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PublisherDetails_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PublisherDetails_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExtensionLiteratures_CreatedById",
                table: "ExtensionLiteratures",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ExtensionLiteratures_PublicationId",
                table: "ExtensionLiteratures",
                column: "PublicationId");

            migrationBuilder.CreateIndex(
                name: "IX_ExtensionLiteratures_UpdatedById",
                table: "ExtensionLiteratures",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Publications_CategoryId",
                table: "Publications",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Publications_CreatedById",
                table: "Publications",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Publications_ModeId",
                table: "Publications",
                column: "ModeId");

            migrationBuilder.CreateIndex(
                name: "IX_Publications_OrganizationId",
                table: "Publications",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Publications_RegionId",
                table: "Publications",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Publications_SourceId",
                table: "Publications",
                column: "SourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Publications_UnitLocationId",
                table: "Publications",
                column: "UnitLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Publications_UpdatedById",
                table: "Publications",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PublisherDetails_CreatedById",
                table: "PublisherDetails",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PublisherDetails_PublicationId",
                table: "PublisherDetails",
                column: "PublicationId",
                unique: true,
                filter: "[PublicationId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PublisherDetails_UpdatedById",
                table: "PublisherDetails",
                column: "UpdatedById");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExtensionLiteratures");

            migrationBuilder.DropTable(
                name: "PublisherDetails");

            migrationBuilder.DropTable(
                name: "Publications");
        }
    }
}

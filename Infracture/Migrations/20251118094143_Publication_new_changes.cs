using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Publication_new_changes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExtensionWorkId",
                table: "Publications",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TitleOfThesis",
                table: "Publications",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PublicationEnglishMagazine",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublicationId = table.Column<int>(type: "int", nullable: false),
                    EnglishMagazineId = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicationEnglishMagazine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PublicationEnglishMagazine_EnglishMagazines_EnglishMagazineId",
                        column: x => x.EnglishMagazineId,
                        principalTable: "EnglishMagazines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PublicationEnglishMagazine_Publications_PublicationId",
                        column: x => x.PublicationId,
                        principalTable: "Publications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PublicationEnglishMagazine_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PublicationEnglishMagazine_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PublicationEnglishNewsPaper",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublicationId = table.Column<int>(type: "int", nullable: false),
                    EnglishNewsPaperId = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicationEnglishNewsPaper", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PublicationEnglishNewsPaper_EnglishNewsPapers_EnglishNewsPaperId",
                        column: x => x.EnglishNewsPaperId,
                        principalTable: "EnglishNewsPapers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PublicationEnglishNewsPaper_Publications_PublicationId",
                        column: x => x.PublicationId,
                        principalTable: "Publications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PublicationEnglishNewsPaper_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PublicationEnglishNewsPaper_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PublicationKannadaMagazine",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublicationId = table.Column<int>(type: "int", nullable: false),
                    KannadaMagazineId = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicationKannadaMagazine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PublicationKannadaMagazine_KannadaMagazines_KannadaMagazineId",
                        column: x => x.KannadaMagazineId,
                        principalTable: "KannadaMagazines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PublicationKannadaMagazine_Publications_PublicationId",
                        column: x => x.PublicationId,
                        principalTable: "Publications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PublicationKannadaMagazine_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PublicationKannadaMagazine_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PublicationKannadaNewsPaper",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublicationId = table.Column<int>(type: "int", nullable: false),
                    KannadaNewsPaperId = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicationKannadaNewsPaper", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PublicationKannadaNewsPaper_KannadaNewsPapers_KannadaNewsPaperId",
                        column: x => x.KannadaNewsPaperId,
                        principalTable: "KannadaNewsPapers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PublicationKannadaNewsPaper_Publications_PublicationId",
                        column: x => x.PublicationId,
                        principalTable: "Publications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PublicationKannadaNewsPaper_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PublicationKannadaNewsPaper_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Publications_ExtensionWorkId",
                table: "Publications",
                column: "ExtensionWorkId");

            migrationBuilder.CreateIndex(
                name: "IX_PublicationEnglishMagazine_CreatedById",
                table: "PublicationEnglishMagazine",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PublicationEnglishMagazine_EnglishMagazineId",
                table: "PublicationEnglishMagazine",
                column: "EnglishMagazineId");

            migrationBuilder.CreateIndex(
                name: "IX_PublicationEnglishMagazine_PublicationId",
                table: "PublicationEnglishMagazine",
                column: "PublicationId");

            migrationBuilder.CreateIndex(
                name: "IX_PublicationEnglishMagazine_UpdatedById",
                table: "PublicationEnglishMagazine",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PublicationEnglishNewsPaper_CreatedById",
                table: "PublicationEnglishNewsPaper",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PublicationEnglishNewsPaper_EnglishNewsPaperId",
                table: "PublicationEnglishNewsPaper",
                column: "EnglishNewsPaperId");

            migrationBuilder.CreateIndex(
                name: "IX_PublicationEnglishNewsPaper_PublicationId",
                table: "PublicationEnglishNewsPaper",
                column: "PublicationId");

            migrationBuilder.CreateIndex(
                name: "IX_PublicationEnglishNewsPaper_UpdatedById",
                table: "PublicationEnglishNewsPaper",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PublicationKannadaMagazine_CreatedById",
                table: "PublicationKannadaMagazine",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PublicationKannadaMagazine_KannadaMagazineId",
                table: "PublicationKannadaMagazine",
                column: "KannadaMagazineId");

            migrationBuilder.CreateIndex(
                name: "IX_PublicationKannadaMagazine_PublicationId",
                table: "PublicationKannadaMagazine",
                column: "PublicationId");

            migrationBuilder.CreateIndex(
                name: "IX_PublicationKannadaMagazine_UpdatedById",
                table: "PublicationKannadaMagazine",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PublicationKannadaNewsPaper_CreatedById",
                table: "PublicationKannadaNewsPaper",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PublicationKannadaNewsPaper_KannadaNewsPaperId",
                table: "PublicationKannadaNewsPaper",
                column: "KannadaNewsPaperId");

            migrationBuilder.CreateIndex(
                name: "IX_PublicationKannadaNewsPaper_PublicationId",
                table: "PublicationKannadaNewsPaper",
                column: "PublicationId");

            migrationBuilder.CreateIndex(
                name: "IX_PublicationKannadaNewsPaper_UpdatedById",
                table: "PublicationKannadaNewsPaper",
                column: "UpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Publications_ExtensionWorks_ExtensionWorkId",
                table: "Publications",
                column: "ExtensionWorkId",
                principalTable: "ExtensionWorks",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Publications_ExtensionWorks_ExtensionWorkId",
                table: "Publications");

            migrationBuilder.DropTable(
                name: "PublicationEnglishMagazine");

            migrationBuilder.DropTable(
                name: "PublicationEnglishNewsPaper");

            migrationBuilder.DropTable(
                name: "PublicationKannadaMagazine");

            migrationBuilder.DropTable(
                name: "PublicationKannadaNewsPaper");

            migrationBuilder.DropIndex(
                name: "IX_Publications_ExtensionWorkId",
                table: "Publications");

            migrationBuilder.DropColumn(
                name: "ExtensionWorkId",
                table: "Publications");

            migrationBuilder.DropColumn(
                name: "TitleOfThesis",
                table: "Publications");
        }
    }
}

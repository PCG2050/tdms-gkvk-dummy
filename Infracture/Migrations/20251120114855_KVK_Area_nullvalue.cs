using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class KVK_Area_nullvalue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PublicationEnglishMagazine_EnglishMagazines_EnglishMagazineId",
                table: "PublicationEnglishMagazine");

            migrationBuilder.DropForeignKey(
                name: "FK_PublicationEnglishNewsPaper_EnglishNewsPapers_EnglishNewsPaperId",
                table: "PublicationEnglishNewsPaper");

            migrationBuilder.DropForeignKey(
                name: "FK_PublicationKannadaMagazine_KannadaMagazines_KannadaMagazineId",
                table: "PublicationKannadaMagazine");

            migrationBuilder.DropForeignKey(
                name: "FK_PublicationKannadaNewsPaper_KannadaNewsPapers_KannadaNewsPaperId",
                table: "PublicationKannadaNewsPaper");

            migrationBuilder.AlterColumn<decimal>(
                name: "Area",
                table: "KvkProgramDetails",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddForeignKey(
                name: "FK_PublicationEnglishMagazine_EnglishMagazines_EnglishMagazineId",
                table: "PublicationEnglishMagazine",
                column: "EnglishMagazineId",
                principalTable: "EnglishMagazines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PublicationEnglishNewsPaper_EnglishNewsPapers_EnglishNewsPaperId",
                table: "PublicationEnglishNewsPaper",
                column: "EnglishNewsPaperId",
                principalTable: "EnglishNewsPapers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PublicationKannadaMagazine_KannadaMagazines_KannadaMagazineId",
                table: "PublicationKannadaMagazine",
                column: "KannadaMagazineId",
                principalTable: "KannadaMagazines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PublicationKannadaNewsPaper_KannadaNewsPapers_KannadaNewsPaperId",
                table: "PublicationKannadaNewsPaper",
                column: "KannadaNewsPaperId",
                principalTable: "KannadaNewsPapers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PublicationEnglishMagazine_EnglishMagazines_EnglishMagazineId",
                table: "PublicationEnglishMagazine");

            migrationBuilder.DropForeignKey(
                name: "FK_PublicationEnglishNewsPaper_EnglishNewsPapers_EnglishNewsPaperId",
                table: "PublicationEnglishNewsPaper");

            migrationBuilder.DropForeignKey(
                name: "FK_PublicationKannadaMagazine_KannadaMagazines_KannadaMagazineId",
                table: "PublicationKannadaMagazine");

            migrationBuilder.DropForeignKey(
                name: "FK_PublicationKannadaNewsPaper_KannadaNewsPapers_KannadaNewsPaperId",
                table: "PublicationKannadaNewsPaper");

            migrationBuilder.AlterColumn<decimal>(
                name: "Area",
                table: "KvkProgramDetails",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PublicationEnglishMagazine_EnglishMagazines_EnglishMagazineId",
                table: "PublicationEnglishMagazine",
                column: "EnglishMagazineId",
                principalTable: "EnglishMagazines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PublicationEnglishNewsPaper_EnglishNewsPapers_EnglishNewsPaperId",
                table: "PublicationEnglishNewsPaper",
                column: "EnglishNewsPaperId",
                principalTable: "EnglishNewsPapers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PublicationKannadaMagazine_KannadaMagazines_KannadaMagazineId",
                table: "PublicationKannadaMagazine",
                column: "KannadaMagazineId",
                principalTable: "KannadaMagazines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PublicationKannadaNewsPaper_KannadaNewsPapers_KannadaNewsPaperId",
                table: "PublicationKannadaNewsPaper",
                column: "KannadaNewsPaperId",
                principalTable: "KannadaNewsPapers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

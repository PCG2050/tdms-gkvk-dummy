using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Kvk_Updated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KvkFLDResults_FLDDetails_DetailsOfDemoId",
                table: "KvkFLDResults");

            migrationBuilder.DropForeignKey(
                name: "FK_KvkOFTResults_OFTDetails_DetailsOfDemoId",
                table: "KvkOFTResults");

            migrationBuilder.DropTable(
                name: "FLDDetails");

            migrationBuilder.DropTable(
                name: "OFTDetails");

            migrationBuilder.AddForeignKey(
                name: "FK_KvkFLDResults_FLDResults_DetailsOfDemoId",
                table: "KvkFLDResults",
                column: "DetailsOfDemoId",
                principalTable: "FLDResults",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KvkOFTResults_OFTResults_DetailsOfDemoId",
                table: "KvkOFTResults",
                column: "DetailsOfDemoId",
                principalTable: "OFTResults",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KvkFLDResults_FLDResults_DetailsOfDemoId",
                table: "KvkFLDResults");

            migrationBuilder.DropForeignKey(
                name: "FK_KvkOFTResults_OFTResults_DetailsOfDemoId",
                table: "KvkOFTResults");

            migrationBuilder.CreateTable(
                name: "FLDDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FLDDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OFTDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OFTDetails", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_KvkFLDResults_FLDDetails_DetailsOfDemoId",
                table: "KvkFLDResults",
                column: "DetailsOfDemoId",
                principalTable: "FLDDetails",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KvkOFTResults_OFTDetails_DetailsOfDemoId",
                table: "KvkOFTResults",
                column: "DetailsOfDemoId",
                principalTable: "OFTDetails",
                principalColumn: "Id");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Tblservice_chnages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VisitorDetails_Visitors_VisitorId",
                table: "VisitorDetails");

            migrationBuilder.DropIndex(
                name: "IX_VisitorDetails_VisitorId",
                table: "VisitorDetails");

            migrationBuilder.DropColumn(
                name: "VisitorId",
                table: "VisitorDetails");

            migrationBuilder.AddColumn<int>(
                name: "ParticipationTypeId",
                table: "Services",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VisitorId",
                table: "Services",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ParticipationTypes",
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
                    table.PrimaryKey("PK_ParticipationTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Services_ParticipationTypeId",
                table: "Services",
                column: "ParticipationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_VisitorId",
                table: "Services",
                column: "VisitorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_ParticipationTypes_ParticipationTypeId",
                table: "Services",
                column: "ParticipationTypeId",
                principalTable: "ParticipationTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Visitors_VisitorId",
                table: "Services",
                column: "VisitorId",
                principalTable: "Visitors",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_ParticipationTypes_ParticipationTypeId",
                table: "Services");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_Visitors_VisitorId",
                table: "Services");

            migrationBuilder.DropTable(
                name: "ParticipationTypes");

            migrationBuilder.DropIndex(
                name: "IX_Services_ParticipationTypeId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Services_VisitorId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "ParticipationTypeId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "VisitorId",
                table: "Services");

            migrationBuilder.AddColumn<int>(
                name: "VisitorId",
                table: "VisitorDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_VisitorDetails_VisitorId",
                table: "VisitorDetails",
                column: "VisitorId");

            migrationBuilder.AddForeignKey(
                name: "FK_VisitorDetails_Visitors_VisitorId",
                table: "VisitorDetails",
                column: "VisitorId",
                principalTable: "Visitors",
                principalColumn: "Id");
        }
    }
}

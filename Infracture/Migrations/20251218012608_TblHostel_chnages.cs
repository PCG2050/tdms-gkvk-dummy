using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TblHostel_chnages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TableHostels_Services_TblServiceId",
                table: "TableHostels");

            migrationBuilder.DropForeignKey(
                name: "FK_VisitorDetails_Services_TblServiceId",
                table: "VisitorDetails");

            migrationBuilder.DropIndex(
                name: "IX_VisitorDetails_TblServiceId",
                table: "VisitorDetails");

            migrationBuilder.DropIndex(
                name: "IX_TableHostels_TblServiceId",
                table: "TableHostels");

            migrationBuilder.DropColumn(
                name: "TblServiceId",
                table: "VisitorDetails");

            migrationBuilder.DropColumn(
                name: "TblServiceId",
                table: "TableHostels");

            migrationBuilder.CreateIndex(
                name: "IX_VisitorDetails_ServiceId",
                table: "VisitorDetails",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_TableHostels_ServiceId",
                table: "TableHostels",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_TableHostels_Services_ServiceId",
                table: "TableHostels",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VisitorDetails_Services_ServiceId",
                table: "VisitorDetails",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TableHostels_Services_ServiceId",
                table: "TableHostels");

            migrationBuilder.DropForeignKey(
                name: "FK_VisitorDetails_Services_ServiceId",
                table: "VisitorDetails");

            migrationBuilder.DropIndex(
                name: "IX_VisitorDetails_ServiceId",
                table: "VisitorDetails");

            migrationBuilder.DropIndex(
                name: "IX_TableHostels_ServiceId",
                table: "TableHostels");

            migrationBuilder.AddColumn<int>(
                name: "TblServiceId",
                table: "VisitorDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TblServiceId",
                table: "TableHostels",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VisitorDetails_TblServiceId",
                table: "VisitorDetails",
                column: "TblServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_TableHostels_TblServiceId",
                table: "TableHostels",
                column: "TblServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_TableHostels_Services_TblServiceId",
                table: "TableHostels",
                column: "TblServiceId",
                principalTable: "Services",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VisitorDetails_Services_TblServiceId",
                table: "VisitorDetails",
                column: "TblServiceId",
                principalTable: "Services",
                principalColumn: "Id");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ASMConsultancyMasterDataChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OtherActivities_OrganizationUnitLocations_UnitLocationId",
                table: "OtherActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_OtherActivities_Organizations_OrganizationId",
                table: "OtherActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_RevolvingFundStatuses_Services_ServiceId",
                table: "RevolvingFundStatuses");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_OrganizationUnitLocations_UnitLocationId",
                table: "Services");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_Organizations_OrganizationId",
                table: "Services");

            migrationBuilder.DropForeignKey(
                name: "FK_TableConsultingAndSocialMediaServices_OrganizationUnitLocations_UnitLocationId",
                table: "TableConsultingAndSocialMediaServices");

            migrationBuilder.DropForeignKey(
                name: "FK_TableConsultingAndSocialMediaServices_Organizations_OrganizationId",
                table: "TableConsultingAndSocialMediaServices");

            migrationBuilder.DropForeignKey(
                name: "FK_TableConsultingAndSocialMediaServices_TableModeAndOutages_ModeOrOutageId",
                table: "TableConsultingAndSocialMediaServices");

            migrationBuilder.DropForeignKey(
                name: "FK_TableHostels_Services_ServiceId",
                table: "TableHostels");

            migrationBuilder.DropTable(
                name: "ServicesCategories");

            migrationBuilder.DropTable(
                name: "TableModeAndOutages");

            migrationBuilder.DropIndex(
                name: "IX_TableHostels_ServiceId",
                table: "TableHostels");

            migrationBuilder.DropIndex(
                name: "IX_TableConsultingAndSocialMediaServices_ModeOrOutageId",
                table: "TableConsultingAndSocialMediaServices");

            migrationBuilder.DropIndex(
                name: "IX_TableConsultingAndSocialMediaServices_OrganizationId",
                table: "TableConsultingAndSocialMediaServices");

            migrationBuilder.DropIndex(
                name: "IX_Services_OrganizationId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Services_UnitLocationId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_RevolvingFundStatuses_ServiceId",
                table: "RevolvingFundStatuses");

            migrationBuilder.DropIndex(
                name: "IX_OtherActivities_OrganizationId",
                table: "OtherActivities");

            migrationBuilder.DropIndex(
                name: "IX_OtherActivities_UnitLocationId",
                table: "OtherActivities");

            migrationBuilder.DropColumn(
                name: "VillageOrTaluq",
                table: "TableHostels");

            migrationBuilder.DropColumn(
                name: "Attachements",
                table: "TableConsultingAndSocialMediaServices");

            migrationBuilder.DropColumn(
                name: "Document",
                table: "TableConsultingAndSocialMediaServices");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "TableConsultingAndSocialMediaServices");

            migrationBuilder.DropColumn(
                name: "RelatedToDeciplineOther",
                table: "TableConsultingAndSocialMediaServices");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "TableConsultingAndSocialMediaServices");

            migrationBuilder.DropColumn(
                name: "Attachements",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "Fk_Catogory",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "Fk_SourceOfFund",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "Fk_Theme",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "OtherCatogory",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "RelatedTo",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "UnitLocationId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "Attachements",
                table: "OtherActivities");

            migrationBuilder.DropColumn(
                name: "DocumentPath",
                table: "OtherActivities");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "OtherActivities");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "OtherActivities");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "OtherActivities");

            migrationBuilder.DropColumn(
                name: "UnitLocationId",
                table: "OtherActivities");

            migrationBuilder.RenameColumn(
                name: "UnitLocationId",
                table: "TableConsultingAndSocialMediaServices",
                newName: "RelatedToId");

            migrationBuilder.RenameColumn(
                name: "OrganizationId",
                table: "TableConsultingAndSocialMediaServices",
                newName: "PhoneCalls");

            migrationBuilder.RenameColumn(
                name: "ModeOrOutageId",
                table: "TableConsultingAndSocialMediaServices",
                newName: "NoOfWhatsappMessage");

            migrationBuilder.RenameColumn(
                name: "Fk_RelatedToDeciplineId",
                table: "TableConsultingAndSocialMediaServices",
                newName: "NoOfWhatsappGroup");

            migrationBuilder.RenameColumn(
                name: "Fk_Particulars",
                table: "TableConsultingAndSocialMediaServices",
                newName: "NoOfTweets");

            migrationBuilder.RenameColumn(
                name: "Fk_ModeOrOutageId",
                table: "TableConsultingAndSocialMediaServices",
                newName: "ParticularsId");

            migrationBuilder.RenameColumn(
                name: "Fk_CategoryId",
                table: "TableConsultingAndSocialMediaServices",
                newName: "NoOfSms");

            migrationBuilder.RenameIndex(
                name: "IX_TableConsultingAndSocialMediaServices_UnitLocationId",
                table: "TableConsultingAndSocialMediaServices",
                newName: "IX_TableConsultingAndSocialMediaServices_RelatedToId");

            migrationBuilder.AlterColumn<string>(
                name: "Purpose",
                table: "TableHostels",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "Date",
                table: "TableHostels",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<double>(
                name: "AmountGenerated",
                table: "TableHostels",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<int>(
                name: "TblServiceId",
                table: "TableHostels",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VillageOrTaluk",
                table: "TableHostels",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "WeblinkOrApplink",
                table: "TableConsultingAndSocialMediaServices",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "TableConsultingAndSocialMediaServices",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<string>(
                name: "Publisher",
                table: "TableConsultingAndSocialMediaServices",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "ParticularsOthers",
                table: "TableConsultingAndSocialMediaServices",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<string>(
                name: "Location",
                table: "TableConsultingAndSocialMediaServices",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "TableConsultingAndSocialMediaServices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CountOfAnsweredQueries",
                table: "TableConsultingAndSocialMediaServices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ExtensionActivityId",
                table: "TableConsultingAndSocialMediaServices",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FaceToFace",
                table: "TableConsultingAndSocialMediaServices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FacebookPostCount",
                table: "TableConsultingAndSocialMediaServices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Female_GEN",
                table: "TableConsultingAndSocialMediaServices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Female_OBC",
                table: "TableConsultingAndSocialMediaServices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Female_SC",
                table: "TableConsultingAndSocialMediaServices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Female_ST",
                table: "TableConsultingAndSocialMediaServices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GroupDiscussion",
                table: "TableConsultingAndSocialMediaServices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Male_GEN",
                table: "TableConsultingAndSocialMediaServices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Male_OBC",
                table: "TableConsultingAndSocialMediaServices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Male_SC",
                table: "TableConsultingAndSocialMediaServices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Male_ST",
                table: "TableConsultingAndSocialMediaServices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ModeOrOutreachId",
                table: "TableConsultingAndSocialMediaServices",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ModeOutreachId",
                table: "TableConsultingAndSocialMediaServices",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NoOfBeneficiaries",
                table: "TableConsultingAndSocialMediaServices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NoOfEmailSent",
                table: "TableConsultingAndSocialMediaServices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NoOfNewspaperCoverage",
                table: "TableConsultingAndSocialMediaServices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NoOfPressCoverage",
                table: "TableConsultingAndSocialMediaServices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NoOfPressMeet",
                table: "TableConsultingAndSocialMediaServices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NoOfPressVisit",
                table: "TableConsultingAndSocialMediaServices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PublishedDocUrl",
                table: "TableConsultingAndSocialMediaServices",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RelatedToDisciplineOther",
                table: "TableConsultingAndSocialMediaServices",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Variety",
                table: "Services",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "TitleOfActivityConducted",
                table: "Services",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<string>(
                name: "OtherTheme",
                table: "Services",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "OtherSourceOfFund",
                table: "Services",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "CropPlantProductName",
                table: "Services",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<string>(
                name: "Component",
                table: "Services",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<double>(
                name: "AmountGenerated",
                table: "Services",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Services",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherCategory",
                table: "Services",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "QuantityUnitId",
                table: "Services",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RentedTo",
                table: "Services",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SourceOfFundId",
                table: "Services",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ThemeId",
                table: "Services",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Receipt",
                table: "RevolvingFundStatuses",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<double>(
                name: "OpeningBalance",
                table: "RevolvingFundStatuses",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<double>(
                name: "Expenditure",
                table: "RevolvingFundStatuses",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<double>(
                name: "ClosingBalance",
                table: "RevolvingFundStatuses",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<int>(
                name: "TblServiceId",
                table: "RevolvingFundStatuses",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "OtherActivities",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AddColumn<string>(
                name: "UploadPath",
                table: "OtherActivities",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ConsultancyServicesCategories",
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
                    table.PrimaryKey("PK_ConsultancyServicesCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TableModeAndOutreaches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConsultingServiceId = table.Column<int>(type: "int", nullable: false),
                    TableConsultingAndSocialMediaService = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    MobileNo = table.Column<int>(type: "int", nullable: true),
                    Gender = table.Column<bool>(type: "bit", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TableModeAndOutreaches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TableModeAndOutreaches_TableConsultingAndSocialMediaServices_TableConsultingAndSocialMediaService",
                        column: x => x.TableConsultingAndSocialMediaService,
                        principalTable: "TableConsultingAndSocialMediaServices",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TableModeAndOutreaches_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TableModeAndOutreaches_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "VisitorDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    TblServiceId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobileNo = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<DateOnly>(type: "date", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Purpose = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PurposeOfVisit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitorDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VisitorDetail_Services_TblServiceId",
                        column: x => x.TblServiceId,
                        principalTable: "Services",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_VisitorDetail_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_VisitorDetail_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TableHostels_TblServiceId",
                table: "TableHostels",
                column: "TblServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_TableConsultingAndSocialMediaServices_CategoryId",
                table: "TableConsultingAndSocialMediaServices",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TableConsultingAndSocialMediaServices_ExtensionActivityId",
                table: "TableConsultingAndSocialMediaServices",
                column: "ExtensionActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_TableConsultingAndSocialMediaServices_ModeOutreachId",
                table: "TableConsultingAndSocialMediaServices",
                column: "ModeOutreachId");

            migrationBuilder.CreateIndex(
                name: "IX_TableConsultingAndSocialMediaServices_ParticularsId",
                table: "TableConsultingAndSocialMediaServices",
                column: "ParticularsId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_CategoryId",
                table: "Services",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_QuantityUnitId",
                table: "Services",
                column: "QuantityUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_SourceOfFundId",
                table: "Services",
                column: "SourceOfFundId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_ThemeId",
                table: "Services",
                column: "ThemeId");

            migrationBuilder.CreateIndex(
                name: "IX_RevolvingFundStatuses_TblServiceId",
                table: "RevolvingFundStatuses",
                column: "TblServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_TableModeAndOutreaches_CreatedById",
                table: "TableModeAndOutreaches",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TableModeAndOutreaches_TableConsultingAndSocialMediaService",
                table: "TableModeAndOutreaches",
                column: "TableConsultingAndSocialMediaService");

            migrationBuilder.CreateIndex(
                name: "IX_TableModeAndOutreaches_UpdatedById",
                table: "TableModeAndOutreaches",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_VisitorDetail_CreatedById",
                table: "VisitorDetail",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_VisitorDetail_TblServiceId",
                table: "VisitorDetail",
                column: "TblServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_VisitorDetail_UpdatedById",
                table: "VisitorDetail",
                column: "UpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_RevolvingFundStatuses_Services_TblServiceId",
                table: "RevolvingFundStatuses",
                column: "TblServiceId",
                principalTable: "Services",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_ConsultancyServicesCategories_CategoryId",
                table: "Services",
                column: "CategoryId",
                principalTable: "ConsultancyServicesCategories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_QuantityUnits_QuantityUnitId",
                table: "Services",
                column: "QuantityUnitId",
                principalTable: "QuantityUnits",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_ServiceThemes_ThemeId",
                table: "Services",
                column: "ThemeId",
                principalTable: "ServiceThemes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_SourcesOfFunds_SourceOfFundId",
                table: "Services",
                column: "SourceOfFundId",
                principalTable: "SourcesOfFunds",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TableConsultingAndSocialMediaServices_ConsultancyServicesCategories_CategoryId",
                table: "TableConsultingAndSocialMediaServices",
                column: "CategoryId",
                principalTable: "ConsultancyServicesCategories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TableConsultingAndSocialMediaServices_ExtensionWorks_ExtensionActivityId",
                table: "TableConsultingAndSocialMediaServices",
                column: "ExtensionActivityId",
                principalTable: "ExtensionWorks",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TableConsultingAndSocialMediaServices_ModeOutreaches_ModeOutreachId",
                table: "TableConsultingAndSocialMediaServices",
                column: "ModeOutreachId",
                principalTable: "ModeOutreaches",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TableConsultingAndSocialMediaServices_Particulars_ParticularsId",
                table: "TableConsultingAndSocialMediaServices",
                column: "ParticularsId",
                principalTable: "Particulars",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TableConsultingAndSocialMediaServices_RelatedTos_RelatedToId",
                table: "TableConsultingAndSocialMediaServices",
                column: "RelatedToId",
                principalTable: "RelatedTos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TableHostels_Services_TblServiceId",
                table: "TableHostels",
                column: "TblServiceId",
                principalTable: "Services",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RevolvingFundStatuses_Services_TblServiceId",
                table: "RevolvingFundStatuses");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_ConsultancyServicesCategories_CategoryId",
                table: "Services");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_QuantityUnits_QuantityUnitId",
                table: "Services");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_ServiceThemes_ThemeId",
                table: "Services");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_SourcesOfFunds_SourceOfFundId",
                table: "Services");

            migrationBuilder.DropForeignKey(
                name: "FK_TableConsultingAndSocialMediaServices_ConsultancyServicesCategories_CategoryId",
                table: "TableConsultingAndSocialMediaServices");

            migrationBuilder.DropForeignKey(
                name: "FK_TableConsultingAndSocialMediaServices_ExtensionWorks_ExtensionActivityId",
                table: "TableConsultingAndSocialMediaServices");

            migrationBuilder.DropForeignKey(
                name: "FK_TableConsultingAndSocialMediaServices_ModeOutreaches_ModeOutreachId",
                table: "TableConsultingAndSocialMediaServices");

            migrationBuilder.DropForeignKey(
                name: "FK_TableConsultingAndSocialMediaServices_Particulars_ParticularsId",
                table: "TableConsultingAndSocialMediaServices");

            migrationBuilder.DropForeignKey(
                name: "FK_TableConsultingAndSocialMediaServices_RelatedTos_RelatedToId",
                table: "TableConsultingAndSocialMediaServices");

            migrationBuilder.DropForeignKey(
                name: "FK_TableHostels_Services_TblServiceId",
                table: "TableHostels");

            migrationBuilder.DropTable(
                name: "ConsultancyServicesCategories");

            migrationBuilder.DropTable(
                name: "TableModeAndOutreaches");

            migrationBuilder.DropTable(
                name: "VisitorDetail");

            migrationBuilder.DropIndex(
                name: "IX_TableHostels_TblServiceId",
                table: "TableHostels");

            migrationBuilder.DropIndex(
                name: "IX_TableConsultingAndSocialMediaServices_CategoryId",
                table: "TableConsultingAndSocialMediaServices");

            migrationBuilder.DropIndex(
                name: "IX_TableConsultingAndSocialMediaServices_ExtensionActivityId",
                table: "TableConsultingAndSocialMediaServices");

            migrationBuilder.DropIndex(
                name: "IX_TableConsultingAndSocialMediaServices_ModeOutreachId",
                table: "TableConsultingAndSocialMediaServices");

            migrationBuilder.DropIndex(
                name: "IX_TableConsultingAndSocialMediaServices_ParticularsId",
                table: "TableConsultingAndSocialMediaServices");

            migrationBuilder.DropIndex(
                name: "IX_Services_CategoryId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Services_QuantityUnitId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Services_SourceOfFundId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Services_ThemeId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_RevolvingFundStatuses_TblServiceId",
                table: "RevolvingFundStatuses");

            migrationBuilder.DropColumn(
                name: "TblServiceId",
                table: "TableHostels");

            migrationBuilder.DropColumn(
                name: "VillageOrTaluk",
                table: "TableHostels");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "TableConsultingAndSocialMediaServices");

            migrationBuilder.DropColumn(
                name: "CountOfAnsweredQueries",
                table: "TableConsultingAndSocialMediaServices");

            migrationBuilder.DropColumn(
                name: "ExtensionActivityId",
                table: "TableConsultingAndSocialMediaServices");

            migrationBuilder.DropColumn(
                name: "FaceToFace",
                table: "TableConsultingAndSocialMediaServices");

            migrationBuilder.DropColumn(
                name: "FacebookPostCount",
                table: "TableConsultingAndSocialMediaServices");

            migrationBuilder.DropColumn(
                name: "Female_GEN",
                table: "TableConsultingAndSocialMediaServices");

            migrationBuilder.DropColumn(
                name: "Female_OBC",
                table: "TableConsultingAndSocialMediaServices");

            migrationBuilder.DropColumn(
                name: "Female_SC",
                table: "TableConsultingAndSocialMediaServices");

            migrationBuilder.DropColumn(
                name: "Female_ST",
                table: "TableConsultingAndSocialMediaServices");

            migrationBuilder.DropColumn(
                name: "GroupDiscussion",
                table: "TableConsultingAndSocialMediaServices");

            migrationBuilder.DropColumn(
                name: "Male_GEN",
                table: "TableConsultingAndSocialMediaServices");

            migrationBuilder.DropColumn(
                name: "Male_OBC",
                table: "TableConsultingAndSocialMediaServices");

            migrationBuilder.DropColumn(
                name: "Male_SC",
                table: "TableConsultingAndSocialMediaServices");

            migrationBuilder.DropColumn(
                name: "Male_ST",
                table: "TableConsultingAndSocialMediaServices");

            migrationBuilder.DropColumn(
                name: "ModeOrOutreachId",
                table: "TableConsultingAndSocialMediaServices");

            migrationBuilder.DropColumn(
                name: "ModeOutreachId",
                table: "TableConsultingAndSocialMediaServices");

            migrationBuilder.DropColumn(
                name: "NoOfBeneficiaries",
                table: "TableConsultingAndSocialMediaServices");

            migrationBuilder.DropColumn(
                name: "NoOfEmailSent",
                table: "TableConsultingAndSocialMediaServices");

            migrationBuilder.DropColumn(
                name: "NoOfNewspaperCoverage",
                table: "TableConsultingAndSocialMediaServices");

            migrationBuilder.DropColumn(
                name: "NoOfPressCoverage",
                table: "TableConsultingAndSocialMediaServices");

            migrationBuilder.DropColumn(
                name: "NoOfPressMeet",
                table: "TableConsultingAndSocialMediaServices");

            migrationBuilder.DropColumn(
                name: "NoOfPressVisit",
                table: "TableConsultingAndSocialMediaServices");

            migrationBuilder.DropColumn(
                name: "PublishedDocUrl",
                table: "TableConsultingAndSocialMediaServices");

            migrationBuilder.DropColumn(
                name: "RelatedToDisciplineOther",
                table: "TableConsultingAndSocialMediaServices");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "OtherCategory",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "QuantityUnitId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "RentedTo",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "SourceOfFundId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "ThemeId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "TblServiceId",
                table: "RevolvingFundStatuses");

            migrationBuilder.DropColumn(
                name: "UploadPath",
                table: "OtherActivities");

            migrationBuilder.RenameColumn(
                name: "RelatedToId",
                table: "TableConsultingAndSocialMediaServices",
                newName: "UnitLocationId");

            migrationBuilder.RenameColumn(
                name: "PhoneCalls",
                table: "TableConsultingAndSocialMediaServices",
                newName: "OrganizationId");

            migrationBuilder.RenameColumn(
                name: "ParticularsId",
                table: "TableConsultingAndSocialMediaServices",
                newName: "Fk_ModeOrOutageId");

            migrationBuilder.RenameColumn(
                name: "NoOfWhatsappMessage",
                table: "TableConsultingAndSocialMediaServices",
                newName: "ModeOrOutageId");

            migrationBuilder.RenameColumn(
                name: "NoOfWhatsappGroup",
                table: "TableConsultingAndSocialMediaServices",
                newName: "Fk_RelatedToDeciplineId");

            migrationBuilder.RenameColumn(
                name: "NoOfTweets",
                table: "TableConsultingAndSocialMediaServices",
                newName: "Fk_Particulars");

            migrationBuilder.RenameColumn(
                name: "NoOfSms",
                table: "TableConsultingAndSocialMediaServices",
                newName: "Fk_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_TableConsultingAndSocialMediaServices_RelatedToId",
                table: "TableConsultingAndSocialMediaServices",
                newName: "IX_TableConsultingAndSocialMediaServices_UnitLocationId");

            migrationBuilder.AlterColumn<string>(
                name: "Purpose",
                table: "TableHostels",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "TableHostels",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<decimal>(
                name: "AmountGenerated",
                table: "TableHostels",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<string>(
                name: "VillageOrTaluq",
                table: "TableHostels",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "WeblinkOrApplink",
                table: "TableConsultingAndSocialMediaServices",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "TableConsultingAndSocialMediaServices",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Publisher",
                table: "TableConsultingAndSocialMediaServices",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ParticularsOthers",
                table: "TableConsultingAndSocialMediaServices",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Location",
                table: "TableConsultingAndSocialMediaServices",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Attachements",
                table: "TableConsultingAndSocialMediaServices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Document",
                table: "TableConsultingAndSocialMediaServices",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateOnly>(
                name: "EndDate",
                table: "TableConsultingAndSocialMediaServices",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "RelatedToDeciplineOther",
                table: "TableConsultingAndSocialMediaServices",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateOnly>(
                name: "StartDate",
                table: "TableConsultingAndSocialMediaServices",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AlterColumn<string>(
                name: "Variety",
                table: "Services",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TitleOfActivityConducted",
                table: "Services",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OtherTheme",
                table: "Services",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OtherSourceOfFund",
                table: "Services",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CropPlantProductName",
                table: "Services",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Component",
                table: "Services",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "AmountGenerated",
                table: "Services",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<string>(
                name: "Attachements",
                table: "Services",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "EndDate",
                table: "Services",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<int>(
                name: "Fk_Catogory",
                table: "Services",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Fk_SourceOfFund",
                table: "Services",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Fk_Theme",
                table: "Services",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "Services",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "OtherCatogory",
                table: "Services",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Services",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "RelatedTo",
                table: "Services",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateOnly>(
                name: "StartDate",
                table: "Services",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<int>(
                name: "UnitLocationId",
                table: "Services",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "Receipt",
                table: "RevolvingFundStatuses",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<decimal>(
                name: "OpeningBalance",
                table: "RevolvingFundStatuses",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<decimal>(
                name: "Expenditure",
                table: "RevolvingFundStatuses",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<decimal>(
                name: "ClosingBalance",
                table: "RevolvingFundStatuses",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "OtherActivities",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Attachements",
                table: "OtherActivities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DocumentPath",
                table: "OtherActivities",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateOnly>(
                name: "EndDate",
                table: "OtherActivities",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "OtherActivities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateOnly>(
                name: "StartDate",
                table: "OtherActivities",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<int>(
                name: "UnitLocationId",
                table: "OtherActivities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ServicesCategories",
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
                    table.PrimaryKey("PK_ServicesCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TableModeAndOutages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    CountOfAnsweredQueries = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    FK_ConsultingServiceId = table.Column<int>(type: "int", nullable: false),
                    FaceToFace = table.Column<int>(type: "int", nullable: false),
                    FacebookPostCount = table.Column<int>(type: "int", nullable: false),
                    Female_GEN = table.Column<int>(type: "int", nullable: false),
                    Female_OBC = table.Column<int>(type: "int", nullable: false),
                    Female_SC = table.Column<int>(type: "int", nullable: false),
                    Female_ST = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<bool>(type: "bit", maxLength: 10, nullable: false),
                    GroupDiscussion = table.Column<int>(type: "int", nullable: false),
                    Male_GEN = table.Column<int>(type: "int", nullable: false),
                    Male_OBC = table.Column<int>(type: "int", nullable: false),
                    Male_SC = table.Column<int>(type: "int", nullable: false),
                    Male_ST = table.Column<int>(type: "int", nullable: false),
                    MobileNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    NoOfBeneficiaries = table.Column<int>(type: "int", nullable: false),
                    NoOfEmailSent = table.Column<int>(type: "int", nullable: false),
                    NoOfNewspaperCoverage = table.Column<int>(type: "int", nullable: false),
                    NoOfPressCoverage = table.Column<int>(type: "int", nullable: false),
                    NoOfPressMeet = table.Column<int>(type: "int", nullable: false),
                    NoOfPressVisit = table.Column<int>(type: "int", nullable: false),
                    NoOfSms = table.Column<int>(type: "int", nullable: false),
                    NoOfTweets = table.Column<int>(type: "int", nullable: false),
                    NoOfWhatsappGroup = table.Column<int>(type: "int", nullable: false),
                    NoOfWhatsappMessage = table.Column<int>(type: "int", nullable: false),
                    PhoneCalls = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TableModeAndOutages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TableModeAndOutages_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TableModeAndOutages_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TableHostels_ServiceId",
                table: "TableHostels",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_TableConsultingAndSocialMediaServices_ModeOrOutageId",
                table: "TableConsultingAndSocialMediaServices",
                column: "ModeOrOutageId");

            migrationBuilder.CreateIndex(
                name: "IX_TableConsultingAndSocialMediaServices_OrganizationId",
                table: "TableConsultingAndSocialMediaServices",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_OrganizationId",
                table: "Services",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_UnitLocationId",
                table: "Services",
                column: "UnitLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_RevolvingFundStatuses_ServiceId",
                table: "RevolvingFundStatuses",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_OtherActivities_OrganizationId",
                table: "OtherActivities",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_OtherActivities_UnitLocationId",
                table: "OtherActivities",
                column: "UnitLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_TableModeAndOutages_CreatedById",
                table: "TableModeAndOutages",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TableModeAndOutages_UpdatedById",
                table: "TableModeAndOutages",
                column: "UpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_OtherActivities_OrganizationUnitLocations_UnitLocationId",
                table: "OtherActivities",
                column: "UnitLocationId",
                principalTable: "OrganizationUnitLocations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OtherActivities_Organizations_OrganizationId",
                table: "OtherActivities",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RevolvingFundStatuses_Services_ServiceId",
                table: "RevolvingFundStatuses",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_OrganizationUnitLocations_UnitLocationId",
                table: "Services",
                column: "UnitLocationId",
                principalTable: "OrganizationUnitLocations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Organizations_OrganizationId",
                table: "Services",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TableConsultingAndSocialMediaServices_OrganizationUnitLocations_UnitLocationId",
                table: "TableConsultingAndSocialMediaServices",
                column: "UnitLocationId",
                principalTable: "OrganizationUnitLocations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TableConsultingAndSocialMediaServices_Organizations_OrganizationId",
                table: "TableConsultingAndSocialMediaServices",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TableConsultingAndSocialMediaServices_TableModeAndOutages_ModeOrOutageId",
                table: "TableConsultingAndSocialMediaServices",
                column: "ModeOrOutageId",
                principalTable: "TableModeAndOutages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TableHostels_Services_ServiceId",
                table: "TableHostels",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id");
        }
    }
}

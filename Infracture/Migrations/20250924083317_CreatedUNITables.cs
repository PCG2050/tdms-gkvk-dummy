using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreatedUNITables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FIUProgramDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProgramTypeId = table.Column<int>(type: "int", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    CategoryOther = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Type = table.Column<int>(type: "int", maxLength: 100, nullable: false),
                    TypeOther = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Theme = table.Column<int>(type: "int", maxLength: 150, nullable: false),
                    ThemeOther = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ThematicArea = table.Column<int>(type: "int", maxLength: 150, nullable: false),
                    ThematicAreaOther = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SponsoredOrganization = table.Column<int>(type: "int", maxLength: 200, nullable: false),
                    SponsoredOrganizationName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Mode = table.Column<int>(type: "int", maxLength: 100, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Duration = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Region = table.Column<int>(type: "int", maxLength: 150, nullable: false),
                    RegionOther = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TPNo = table.Column<int>(type: "int", maxLength: 100, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    TotalOutlayRs = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<int>(type: "int", maxLength: 100, nullable: false),
                    BatchNo = table.Column<int>(type: "int", maxLength: 100, nullable: false),
                    ProposalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProposalUploadFile = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    UniversitySanctionLetterDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UniversitySanctionLetterUploadFile = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    FundsSanctionLetterDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FundsSanctionLetterUploadFile = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    UploadVideo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    SD = table.Column<int>(type: "int", maxLength: 100, nullable: false),
                    AreaHa = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    OrganizerFileUpload = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    OrganizerInstitutionName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    OrganizerAddress = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Participation = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    ParticipatedAs = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    ParticipationFileLink = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    SourceOfInformation = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    TitleOfThesisOrProjectOrPaperOrOthers = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    PaperPosterAbstract = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PaperPosterAbstractDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PaperPosterAbstractLink = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FIUProgramDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FIUProgramDetails_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FIUProgramDetails_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FTIProgramDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProgramTypeId = table.Column<int>(type: "int", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    CategoryOther = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Type = table.Column<int>(type: "int", maxLength: 100, nullable: false),
                    TypeOther = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Theme = table.Column<int>(type: "int", maxLength: 150, nullable: false),
                    ThemeOther = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ThematicArea = table.Column<int>(type: "int", maxLength: 150, nullable: false),
                    ThematicAreaOther = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SponsoredOrganization = table.Column<int>(type: "int", maxLength: 200, nullable: false),
                    SponsoredOrganizationName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Mode = table.Column<int>(type: "int", maxLength: 100, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Duration = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Region = table.Column<int>(type: "int", maxLength: 150, nullable: false),
                    RegionOther = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TPNo = table.Column<int>(type: "int", maxLength: 100, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    TotalOutlayRs = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<int>(type: "int", maxLength: 100, nullable: false),
                    BatchNo = table.Column<int>(type: "int", maxLength: 100, nullable: false),
                    ProposalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProposalUploadFile = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    UniversitySanctionLetterDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UniversitySanctionLetterUploadFile = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    FundsSanctionLetterDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FundsSanctionLetterUploadFile = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    UploadVideo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    SD = table.Column<int>(type: "int", maxLength: 100, nullable: false),
                    AreaHa = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    OrganizerFileUpload = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    OrganizerInstitutionName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    OrganizerAddress = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Participation = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    ParticipatedAs = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    ParticipationFileLink = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    SourceOfInformation = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    TitleOfThesisOrProjectOrPaperOrOthers = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    PaperPosterAbstract = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PaperPosterAbstractDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PaperPosterAbstractLink = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FTIProgramDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FTIProgramDetails_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FTIProgramDetails_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fk_Catogory = table.Column<int>(type: "int", nullable: false),
                    OtherCatogory = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Fk_Theme = table.Column<int>(type: "int", nullable: false),
                    OtherTheme = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Fk_SourceOfFund = table.Column<int>(type: "int", nullable: false),
                    OtherSourceOfFund = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Component = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    AmountGenerated = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CropPlantProductName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Variety = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RelatedTo = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    TitleOfActivityConducted = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    AmountReleased = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
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
                    table.PrimaryKey("PK_Services", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Services_OrganizationUnitLocations_UnitLocationId",
                        column: x => x.UnitLocationId,
                        principalTable: "OrganizationUnitLocations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Services_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Services_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Services_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "STUProgramDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProgramTypeId = table.Column<int>(type: "int", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    CategoryOther = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Type = table.Column<int>(type: "int", maxLength: 100, nullable: false),
                    TypeOther = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Theme = table.Column<int>(type: "int", maxLength: 150, nullable: false),
                    ThemeOther = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ThematicArea = table.Column<int>(type: "int", maxLength: 150, nullable: false),
                    ThematicAreaOther = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SponsoredOrganization = table.Column<int>(type: "int", maxLength: 200, nullable: false),
                    SponsoredOrganizationName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Mode = table.Column<int>(type: "int", maxLength: 100, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Duration = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Region = table.Column<int>(type: "int", maxLength: 150, nullable: false),
                    RegionOther = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TPNo = table.Column<int>(type: "int", maxLength: 100, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    TotalOutlayRs = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<int>(type: "int", maxLength: 100, nullable: false),
                    BatchNo = table.Column<int>(type: "int", maxLength: 100, nullable: false),
                    ProposalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProposalUploadFile = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    UniversitySanctionLetterDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UniversitySanctionLetterUploadFile = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    FundsSanctionLetterDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FundsSanctionLetterUploadFile = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    UploadVideo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    SD = table.Column<int>(type: "int", maxLength: 100, nullable: false),
                    AreaHa = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    OrganizerFileUpload = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    OrganizerInstitutionName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    OrganizerAddress = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Participation = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    ParticipatedAs = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    ParticipationFileLink = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    SourceOfInformation = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    TitleOfThesisOrProjectOrPaperOrOthers = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    PaperPosterAbstract = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PaperPosterAbstractDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PaperPosterAbstractLink = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STUProgramDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_STUProgramDetails_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_STUProgramDetails_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TableKVKProgramDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProgramTypeId = table.Column<int>(type: "int", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    CategoryOther = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Type = table.Column<int>(type: "int", maxLength: 100, nullable: false),
                    TypeOther = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Theme = table.Column<int>(type: "int", maxLength: 150, nullable: false),
                    ThemeOther = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ThematicArea = table.Column<int>(type: "int", maxLength: 150, nullable: false),
                    ThematicAreaOther = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SponsoredOrganization = table.Column<int>(type: "int", maxLength: 200, nullable: false),
                    SponsoredOrganizationName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Mode = table.Column<int>(type: "int", maxLength: 100, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Duration = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Region = table.Column<int>(type: "int", maxLength: 150, nullable: false),
                    RegionOther = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TPNo = table.Column<int>(type: "int", maxLength: 100, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    TotalOutlayRs = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<int>(type: "int", maxLength: 100, nullable: false),
                    BatchNo = table.Column<int>(type: "int", maxLength: 100, nullable: false),
                    ProposalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProposalUploadFile = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    UniversitySanctionLetterDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UniversitySanctionLetterUploadFile = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    FundsSanctionLetterDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FundsSanctionLetterUploadFile = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    UploadVideo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    SD = table.Column<int>(type: "int", maxLength: 100, nullable: false),
                    AreaHa = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    OrganizerFileUpload = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    OrganizerInstitutionName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    OrganizerAddress = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Participation = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    ParticipatedAs = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    ParticipationFileLink = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    SourceOfInformation = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    TitleOfThesisOrProjectOrPaperOrOthers = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    PaperPosterAbstract = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PaperPosterAbstractDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PaperPosterAbstractLink = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TableKVKProgramDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TableKVKProgramDetails_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TableKVKProgramDetails_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TableModeAndOutages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FK_ConsultingServiceId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    MobileNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Gender = table.Column<bool>(type: "bit", maxLength: 10, nullable: false),
                    PhoneCalls = table.Column<int>(type: "int", nullable: false),
                    Male_SC = table.Column<int>(type: "int", nullable: false),
                    Male_ST = table.Column<int>(type: "int", nullable: false),
                    Male_OBC = table.Column<int>(type: "int", nullable: false),
                    Male_GEN = table.Column<int>(type: "int", nullable: false),
                    Female_SC = table.Column<int>(type: "int", nullable: false),
                    Female_ST = table.Column<int>(type: "int", nullable: false),
                    Female_OBC = table.Column<int>(type: "int", nullable: false),
                    Female_GEN = table.Column<int>(type: "int", nullable: false),
                    FacebookPostCount = table.Column<int>(type: "int", nullable: false),
                    NoOfSms = table.Column<int>(type: "int", nullable: false),
                    NoOfWhatsappGroup = table.Column<int>(type: "int", nullable: false),
                    NoOfWhatsappMessage = table.Column<int>(type: "int", nullable: false),
                    CountOfAnsweredQueries = table.Column<int>(type: "int", nullable: false),
                    FaceToFace = table.Column<int>(type: "int", nullable: false),
                    GroupDiscussion = table.Column<int>(type: "int", nullable: false),
                    NoOfEmailSent = table.Column<int>(type: "int", nullable: false),
                    NoOfNewspaperCoverage = table.Column<int>(type: "int", nullable: false),
                    NoOfBeneficiaries = table.Column<int>(type: "int", nullable: false),
                    NoOfPressVisit = table.Column<int>(type: "int", nullable: false),
                    NoOfPressMeet = table.Column<int>(type: "int", nullable: false),
                    NoOfPressCoverage = table.Column<int>(type: "int", nullable: false),
                    NoOfTweets = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
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

            migrationBuilder.CreateTable(
                name: "TableOtherActivity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    DocumentPath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
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
                    table.PrimaryKey("PK_TableOtherActivity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TableOtherActivity_OrganizationUnitLocations_UnitLocationId",
                        column: x => x.UnitLocationId,
                        principalTable: "OrganizationUnitLocations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TableOtherActivity_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TableOtherActivity_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TableOtherActivity_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FIUAdvisoryServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FIUProgramDetailsID = table.Column<int>(type: "int", nullable: false),
                    NoOfFacebookSMS = table.Column<int>(type: "int", nullable: false),
                    NoOfSMSSentToRegisteredFarmers = table.Column<int>(type: "int", nullable: false),
                    NoOfWhatsappGroups = table.Column<int>(type: "int", nullable: false),
                    NoOfWhatsappSMS = table.Column<int>(type: "int", nullable: false),
                    NoOfAnsweredWhatsappQueries = table.Column<int>(type: "int", nullable: false),
                    NoOfPhoneCalls = table.Column<int>(type: "int", nullable: false),
                    NoOfFaceToFaceDiscussions = table.Column<int>(type: "int", nullable: false),
                    NoOfGroupDiscussions = table.Column<int>(type: "int", nullable: false),
                    NoOfEmailsSent = table.Column<int>(type: "int", nullable: false),
                    NoOfNewspaperCoverage = table.Column<int>(type: "int", nullable: false),
                    NoOfBeneficiaries = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FIUAdvisoryServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FIUAdvisoryServices_FIUProgramDetails_FIUProgramDetailsID",
                        column: x => x.FIUProgramDetailsID,
                        principalTable: "FIUProgramDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FIUAdvisoryServices_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FIUAdvisoryServices_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FIUParticipantDemographics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FIUProgramDetailsID = table.Column<int>(type: "int", nullable: false),
                    Participant = table.Column<int>(type: "int", maxLength: 200, nullable: false),
                    Male_SC = table.Column<int>(type: "int", nullable: false),
                    Male_ST = table.Column<int>(type: "int", nullable: false),
                    Male_OBC = table.Column<int>(type: "int", nullable: false),
                    Male_GEN = table.Column<int>(type: "int", nullable: false),
                    SC_Male_StayedInHostel = table.Column<int>(type: "int", nullable: false),
                    ST_Male_StayedInHostel = table.Column<int>(type: "int", nullable: false),
                    OBC_Male_StayedInHostel = table.Column<int>(type: "int", nullable: false),
                    GEN_Male_StayedInHostel = table.Column<int>(type: "int", nullable: false),
                    Female_SC = table.Column<int>(type: "int", nullable: false),
                    Female_ST = table.Column<int>(type: "int", nullable: false),
                    Female_OBC = table.Column<int>(type: "int", nullable: false),
                    Female_GEN = table.Column<int>(type: "int", nullable: false),
                    SC_Female_StayedInHostel = table.Column<int>(type: "int", nullable: false),
                    ST_Female_StayedInHostel = table.Column<int>(type: "int", nullable: false),
                    OBC_Female_StayedInHostel = table.Column<int>(type: "int", nullable: false),
                    GEN_Female_StayedInHostel = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FIUParticipantDemographics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FIUParticipantDemographics_FIUProgramDetails_FIUProgramDetailsID",
                        column: x => x.FIUProgramDetailsID,
                        principalTable: "FIUProgramDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FIUParticipantDemographics_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FIUParticipantDemographics_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FIUProgramContentAndResources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FIUProgramDetailsID = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FIUProgramContentAndResources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FIUProgramContentAndResources_FIUProgramDetails_FIUProgramDetailsID",
                        column: x => x.FIUProgramDetailsID,
                        principalTable: "FIUProgramDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FIUProgramContentAndResources_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FIUProgramContentAndResources_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FIURecommendation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FIUProgramDetailsID = table.Column<int>(type: "int", nullable: false),
                    ProblemsIdentified = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Recommendation = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ActionTaken = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    SignificantAchievement = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    SuccessStories = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ImpactOutcome = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FIURecommendation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FIURecommendation_FIUProgramDetails_FIUProgramDetailsID",
                        column: x => x.FIUProgramDetailsID,
                        principalTable: "FIUProgramDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FIURecommendation_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FIURecommendation_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FIUReports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FIUProgramDetailsID = table.Column<int>(type: "int", nullable: false),
                    ProgressReportReportingYear = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UploadPhoto = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    PhotosGeotaggedPhotoOrUploadPhoto = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    UploadVideo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    SignificantOutcome = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FIUReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FIUReports_FIUProgramDetails_FIUProgramDetailsID",
                        column: x => x.FIUProgramDetailsID,
                        principalTable: "FIUProgramDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FIUReports_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FIUReports_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FTIAdvisoryServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FTIProgramDetailsID = table.Column<int>(type: "int", nullable: false),
                    NoOfFacebookSMS = table.Column<int>(type: "int", nullable: false),
                    NoOfSMSSentToRegisteredFarmers = table.Column<int>(type: "int", nullable: false),
                    NoOfWhatsappGroups = table.Column<int>(type: "int", nullable: false),
                    NoOfWhatsappSMS = table.Column<int>(type: "int", nullable: false),
                    NoOfAnsweredWhatsappQueries = table.Column<int>(type: "int", nullable: false),
                    NoOfPhoneCalls = table.Column<int>(type: "int", nullable: false),
                    NoOfFaceToFaceDiscussions = table.Column<int>(type: "int", nullable: false),
                    NoOfGroupDiscussions = table.Column<int>(type: "int", nullable: false),
                    NoOfEmailsSent = table.Column<int>(type: "int", nullable: false),
                    NoOfNewspaperCoverage = table.Column<int>(type: "int", nullable: false),
                    NoOfBeneficiaries = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FTIAdvisoryServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FTIAdvisoryServices_FTIProgramDetails_FTIProgramDetailsID",
                        column: x => x.FTIProgramDetailsID,
                        principalTable: "FTIProgramDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FTIAdvisoryServices_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FTIAdvisoryServices_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FTIParticipantDemographics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FTIProgramDetailsID = table.Column<int>(type: "int", nullable: false),
                    Participant = table.Column<int>(type: "int", maxLength: 200, nullable: false),
                    Male_SC = table.Column<int>(type: "int", nullable: false),
                    Male_ST = table.Column<int>(type: "int", nullable: false),
                    Male_OBC = table.Column<int>(type: "int", nullable: false),
                    Male_GEN = table.Column<int>(type: "int", nullable: false),
                    SC_Male_StayedInHostel = table.Column<int>(type: "int", nullable: false),
                    ST_Male_StayedInHostel = table.Column<int>(type: "int", nullable: false),
                    OBC_Male_StayedInHostel = table.Column<int>(type: "int", nullable: false),
                    GEN_Male_StayedInHostel = table.Column<int>(type: "int", nullable: false),
                    Female_SC = table.Column<int>(type: "int", nullable: false),
                    Female_ST = table.Column<int>(type: "int", nullable: false),
                    Female_OBC = table.Column<int>(type: "int", nullable: false),
                    Female_GEN = table.Column<int>(type: "int", nullable: false),
                    SC_Female_StayedInHostel = table.Column<int>(type: "int", nullable: false),
                    ST_Female_StayedInHostel = table.Column<int>(type: "int", nullable: false),
                    OBC_Female_StayedInHostel = table.Column<int>(type: "int", nullable: false),
                    GEN_Female_StayedInHostel = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FTIParticipantDemographics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FTIParticipantDemographics_FTIProgramDetails_FTIProgramDetailsID",
                        column: x => x.FTIProgramDetailsID,
                        principalTable: "FTIProgramDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FTIParticipantDemographics_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FTIParticipantDemographics_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FTIProgramContentAndResources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FTIProgramDetailsID = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FTIProgramContentAndResources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FTIProgramContentAndResources_FTIProgramDetails_FTIProgramDetailsID",
                        column: x => x.FTIProgramDetailsID,
                        principalTable: "FTIProgramDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FTIProgramContentAndResources_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FTIProgramContentAndResources_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FTIRecommendation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FTIProgramDetailsID = table.Column<int>(type: "int", nullable: false),
                    ProblemsIdentified = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Recommendation = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ActionTaken = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    SignificantAchievement = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    SuccessStories = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ImpactOutcome = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FTIRecommendation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FTIRecommendation_FTIProgramDetails_FTIProgramDetailsID",
                        column: x => x.FTIProgramDetailsID,
                        principalTable: "FTIProgramDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FTIRecommendation_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FTIRecommendation_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FTIReports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FTIProgramDetailsID = table.Column<int>(type: "int", nullable: false),
                    ProgressReportReportingYear = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UploadPhoto = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    PhotosGeotaggedPhotoOrUploadPhoto = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    UploadVideo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    SignificantOutcome = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FTIReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FTIReports_FTIProgramDetails_FTIProgramDetailsID",
                        column: x => x.FTIProgramDetailsID,
                        principalTable: "FTIProgramDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FTIReports_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FTIReports_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RevolvingFundStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    OpeningBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Receipt = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Expenditure = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ClosingBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RevolvingFundStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RevolvingFundStatuses_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RevolvingFundStatuses_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RevolvingFundStatuses_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TableHostels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Male_SC = table.Column<int>(type: "int", nullable: false),
                    Male_ST = table.Column<int>(type: "int", nullable: false),
                    Male_OBC = table.Column<int>(type: "int", nullable: false),
                    Male_GEN = table.Column<int>(type: "int", nullable: false),
                    Female_SC = table.Column<int>(type: "int", nullable: false),
                    Female_ST = table.Column<int>(type: "int", nullable: false),
                    Female_OBC = table.Column<int>(type: "int", nullable: false),
                    Female_GEN = table.Column<int>(type: "int", nullable: false),
                    NumberOfDaysStayed = table.Column<int>(type: "int", nullable: false),
                    VillageOrTaluq = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Purpose = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    AmountGenerated = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TableHostels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TableHostels_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TableHostels_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TableHostels_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "STUAdvisoryServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STUProgramDetailsID = table.Column<int>(type: "int", nullable: false),
                    NoOfFacebookSMS = table.Column<int>(type: "int", nullable: false),
                    NoOfSMSSentToRegisteredFarmers = table.Column<int>(type: "int", nullable: false),
                    NoOfWhatsappGroups = table.Column<int>(type: "int", nullable: false),
                    NoOfWhatsappSMS = table.Column<int>(type: "int", nullable: false),
                    NoOfAnsweredWhatsappQueries = table.Column<int>(type: "int", nullable: false),
                    NoOfPhoneCalls = table.Column<int>(type: "int", nullable: false),
                    NoOfFaceToFaceDiscussions = table.Column<int>(type: "int", nullable: false),
                    NoOfGroupDiscussions = table.Column<int>(type: "int", nullable: false),
                    NoOfEmailsSent = table.Column<int>(type: "int", nullable: false),
                    NoOfNewspaperCoverage = table.Column<int>(type: "int", nullable: false),
                    NoOfBeneficiaries = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STUAdvisoryServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_STUAdvisoryServices_STUProgramDetails_STUProgramDetailsID",
                        column: x => x.STUProgramDetailsID,
                        principalTable: "STUProgramDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_STUAdvisoryServices_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_STUAdvisoryServices_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "STUParticipantDemographics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STUProgramDetailsID = table.Column<int>(type: "int", nullable: false),
                    Participant = table.Column<int>(type: "int", maxLength: 200, nullable: false),
                    Male_SC = table.Column<int>(type: "int", nullable: false),
                    Male_ST = table.Column<int>(type: "int", nullable: false),
                    Male_OBC = table.Column<int>(type: "int", nullable: false),
                    Male_GEN = table.Column<int>(type: "int", nullable: false),
                    SC_Male_StayedInHostel = table.Column<int>(type: "int", nullable: false),
                    ST_Male_StayedInHostel = table.Column<int>(type: "int", nullable: false),
                    OBC_Male_StayedInHostel = table.Column<int>(type: "int", nullable: false),
                    GEN_Male_StayedInHostel = table.Column<int>(type: "int", nullable: false),
                    Female_SC = table.Column<int>(type: "int", nullable: false),
                    Female_ST = table.Column<int>(type: "int", nullable: false),
                    Female_OBC = table.Column<int>(type: "int", nullable: false),
                    Female_GEN = table.Column<int>(type: "int", nullable: false),
                    SC_Female_StayedInHostel = table.Column<int>(type: "int", nullable: false),
                    ST_Female_StayedInHostel = table.Column<int>(type: "int", nullable: false),
                    OBC_Female_StayedInHostel = table.Column<int>(type: "int", nullable: false),
                    GEN_Female_StayedInHostel = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STUParticipantDemographics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_STUParticipantDemographics_STUProgramDetails_STUProgramDetailsID",
                        column: x => x.STUProgramDetailsID,
                        principalTable: "STUProgramDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_STUParticipantDemographics_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_STUParticipantDemographics_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "STUProgramContentAndResources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STUProgramDetailsID = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STUProgramContentAndResources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_STUProgramContentAndResources_STUProgramDetails_STUProgramDetailsID",
                        column: x => x.STUProgramDetailsID,
                        principalTable: "STUProgramDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_STUProgramContentAndResources_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_STUProgramContentAndResources_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "STURecommendation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STUProgramDetailsID = table.Column<int>(type: "int", nullable: false),
                    ProblemsIdentified = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Recommendation = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ActionTaken = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    SignificantAchievement = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    SuccessStories = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ImpactOutcome = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STURecommendation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_STURecommendation_STUProgramDetails_STUProgramDetailsID",
                        column: x => x.STUProgramDetailsID,
                        principalTable: "STUProgramDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_STURecommendation_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_STURecommendation_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "STUReports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STUProgramDetailsID = table.Column<int>(type: "int", nullable: false),
                    ProgressReportReportingYear = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UploadPhoto = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    PhotosGeotaggedPhotoOrUploadPhoto = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    UploadVideo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    SignificantOutcome = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STUReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_STUReports_STUProgramDetails_STUProgramDetailsID",
                        column: x => x.STUProgramDetailsID,
                        principalTable: "STUProgramDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_STUReports_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_STUReports_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "KvkAdvisoryServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KVKProgramDetailsId = table.Column<int>(type: "int", nullable: false),
                    NoOfFacebookSMS = table.Column<int>(type: "int", nullable: false),
                    NoOfSMSSentToRegisteredFarmers = table.Column<int>(type: "int", nullable: false),
                    NoOfWhatsappGroups = table.Column<int>(type: "int", nullable: false),
                    NoOfWhatsappSMS = table.Column<int>(type: "int", nullable: false),
                    NoOfAnsweredWhatsappQueries = table.Column<int>(type: "int", nullable: false),
                    NoOfPhoneCalls = table.Column<int>(type: "int", nullable: false),
                    NoOfFaceToFaceDiscussions = table.Column<int>(type: "int", nullable: false),
                    NoOfGroupDiscussions = table.Column<int>(type: "int", nullable: false),
                    NoOfEmailsSent = table.Column<int>(type: "int", nullable: false),
                    NoOfNewspaperCoverage = table.Column<int>(type: "int", nullable: false),
                    NoOfBeneficiaries = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KvkAdvisoryServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KvkAdvisoryServices_TableKVKProgramDetails_KVKProgramDetailsId",
                        column: x => x.KVKProgramDetailsId,
                        principalTable: "TableKVKProgramDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_KvkAdvisoryServices_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_KvkAdvisoryServices_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "KvkParticipantDemographics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KVKProgramDetailsId = table.Column<int>(type: "int", nullable: false),
                    Participant = table.Column<int>(type: "int", maxLength: 200, nullable: false),
                    Male_SC = table.Column<int>(type: "int", nullable: false),
                    Male_ST = table.Column<int>(type: "int", nullable: false),
                    Male_OBC = table.Column<int>(type: "int", nullable: false),
                    Male_GEN = table.Column<int>(type: "int", nullable: false),
                    SC_Male_StayedInHostel = table.Column<int>(type: "int", nullable: false),
                    ST_Male_StayedInHostel = table.Column<int>(type: "int", nullable: false),
                    OBC_Male_StayedInHostel = table.Column<int>(type: "int", nullable: false),
                    GEN_Male_StayedInHostel = table.Column<int>(type: "int", nullable: false),
                    Female_SC = table.Column<int>(type: "int", nullable: false),
                    Female_ST = table.Column<int>(type: "int", nullable: false),
                    Female_OBC = table.Column<int>(type: "int", nullable: false),
                    Female_GEN = table.Column<int>(type: "int", nullable: false),
                    SC_Female_StayedInHostel = table.Column<int>(type: "int", nullable: false),
                    ST_Female_StayedInHostel = table.Column<int>(type: "int", nullable: false),
                    OBC_Female_StayedInHostel = table.Column<int>(type: "int", nullable: false),
                    GEN_Female_StayedInHostel = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KvkParticipantDemographics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KvkParticipantDemographics_TableKVKProgramDetails_KVKProgramDetailsId",
                        column: x => x.KVKProgramDetailsId,
                        principalTable: "TableKVKProgramDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_KvkParticipantDemographics_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_KvkParticipantDemographics_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "KvkProgramContentAndResources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KVKProgramDetailsId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KvkProgramContentAndResources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KvkProgramContentAndResources_TableKVKProgramDetails_KVKProgramDetailsId",
                        column: x => x.KVKProgramDetailsId,
                        principalTable: "TableKVKProgramDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_KvkProgramContentAndResources_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_KvkProgramContentAndResources_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "KvkRecommendation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KVKProgramDetailsId = table.Column<int>(type: "int", nullable: false),
                    ProblemsIdentified = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Recommendation = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ActionTaken = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    SignificantAchievement = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    SuccessStories = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ImpactOutcome = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KvkRecommendation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KvkRecommendation_TableKVKProgramDetails_KVKProgramDetailsId",
                        column: x => x.KVKProgramDetailsId,
                        principalTable: "TableKVKProgramDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_KvkRecommendation_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_KvkRecommendation_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "KvkReports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KVKProgramDetailsId = table.Column<int>(type: "int", nullable: false),
                    ProgressReportReportingYear = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UploadPhoto = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    PhotosGeotaggedPhotoOrUploadPhoto = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    UploadVideo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    SignificantOutcome = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KvkReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KvkReports_TableKVKProgramDetails_KVKProgramDetailsId",
                        column: x => x.KVKProgramDetailsId,
                        principalTable: "TableKVKProgramDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_KvkReports_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_KvkReports_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TableConsultingAndSocialMediaServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fk_CategoryId = table.Column<int>(type: "int", nullable: false),
                    Fk_RelatedToDeciplineId = table.Column<int>(type: "int", nullable: false),
                    Fk_Particulars = table.Column<int>(type: "int", nullable: false),
                    Fk_ModeOrOutageId = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    RelatedToDeciplineOther = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    ParticularsOthers = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Publisher = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    WeblinkOrApplink = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Document = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
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
                    table.PrimaryKey("PK_TableConsultingAndSocialMediaServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TableConsultingAndSocialMediaServices_OrganizationUnitLocations_UnitLocationId",
                        column: x => x.UnitLocationId,
                        principalTable: "OrganizationUnitLocations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TableConsultingAndSocialMediaServices_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TableConsultingAndSocialMediaServices_TableModeAndOutages_Fk_ModeOrOutageId",
                        column: x => x.Fk_ModeOrOutageId,
                        principalTable: "TableModeAndOutages",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TableConsultingAndSocialMediaServices_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TableConsultingAndSocialMediaServices_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FIUResourcePerson",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FIUProgramContentAndResourcesID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Designation = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ResourceType = table.Column<int>(type: "int", maxLength: 150, nullable: false),
                    Responsibility = table.Column<int>(type: "int", maxLength: 250, nullable: false),
                    InstitutionOrDepartment = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FIUResourcePerson", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FIUResourcePerson_FIUProgramContentAndResources_FIUProgramContentAndResourcesID",
                        column: x => x.FIUProgramContentAndResourcesID,
                        principalTable: "FIUProgramContentAndResources",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FIUResourcePerson_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FIUResourcePerson_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FIUTeachingAidsDeveloped",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FIUProgramContentAndResourcesID = table.Column<int>(type: "int", nullable: false),
                    TypeOfAidDeveloped = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Other = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Purpose = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FIUTeachingAidsDeveloped", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FIUTeachingAidsDeveloped_FIUProgramContentAndResources_FIUProgramContentAndResourcesID",
                        column: x => x.FIUProgramContentAndResourcesID,
                        principalTable: "FIUProgramContentAndResources",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FIUTeachingAidsDeveloped_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FIUTeachingAidsDeveloped_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FIUTopicsCoveredInClass",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FIUProgramContentAndResourcesID = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    PhotoUpload = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FIUTopicsCoveredInClass", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FIUTopicsCoveredInClass_FIUProgramContentAndResources_FIUProgramContentAndResourcesID",
                        column: x => x.FIUProgramContentAndResourcesID,
                        principalTable: "FIUProgramContentAndResources",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FIUTopicsCoveredInClass_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FIUTopicsCoveredInClass_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FTIResourcePerson",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FTIProgramContentAndResourcesID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Designation = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ResourceType = table.Column<int>(type: "int", maxLength: 150, nullable: false),
                    Responsibility = table.Column<int>(type: "int", maxLength: 250, nullable: false),
                    InstitutionOrDepartment = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FTIResourcePerson", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FTIResourcePerson_FTIProgramContentAndResources_FTIProgramContentAndResourcesID",
                        column: x => x.FTIProgramContentAndResourcesID,
                        principalTable: "FTIProgramContentAndResources",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FTIResourcePerson_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FTIResourcePerson_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FTITeachingAidsDeveloped",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FTIProgramContentAndResourcesID = table.Column<int>(type: "int", nullable: false),
                    TypeOfAidDeveloped = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Other = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Purpose = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FTITeachingAidsDeveloped", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FTITeachingAidsDeveloped_FTIProgramContentAndResources_FTIProgramContentAndResourcesID",
                        column: x => x.FTIProgramContentAndResourcesID,
                        principalTable: "FTIProgramContentAndResources",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FTITeachingAidsDeveloped_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FTITeachingAidsDeveloped_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FTITopicsCoveredInClass",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FTIProgramContentAndResourcesID = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    PhotoUpload = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FTITopicsCoveredInClass", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FTITopicsCoveredInClass_FTIProgramContentAndResources_FTIProgramContentAndResourcesID",
                        column: x => x.FTIProgramContentAndResourcesID,
                        principalTable: "FTIProgramContentAndResources",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FTITopicsCoveredInClass_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FTITopicsCoveredInClass_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "STUResourcePerson",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STUProgramContentAndResourcesID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Designation = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ResourceType = table.Column<int>(type: "int", maxLength: 150, nullable: false),
                    Responsibility = table.Column<int>(type: "int", maxLength: 250, nullable: false),
                    InstitutionOrDepartment = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STUResourcePerson", x => x.Id);
                    table.ForeignKey(
                        name: "FK_STUResourcePerson_STUProgramContentAndResources_STUProgramContentAndResourcesID",
                        column: x => x.STUProgramContentAndResourcesID,
                        principalTable: "STUProgramContentAndResources",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_STUResourcePerson_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_STUResourcePerson_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "STUTeachingAidsDeveloped",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STUProgramContentAndResourcesID = table.Column<int>(type: "int", nullable: false),
                    TypeOfAidDeveloped = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Other = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Purpose = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STUTeachingAidsDeveloped", x => x.Id);
                    table.ForeignKey(
                        name: "FK_STUTeachingAidsDeveloped_STUProgramContentAndResources_STUProgramContentAndResourcesID",
                        column: x => x.STUProgramContentAndResourcesID,
                        principalTable: "STUProgramContentAndResources",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_STUTeachingAidsDeveloped_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_STUTeachingAidsDeveloped_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "STUTopicsCoveredInClass",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STUProgramContentAndResourcesID = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    PhotoUpload = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STUTopicsCoveredInClass", x => x.Id);
                    table.ForeignKey(
                        name: "FK_STUTopicsCoveredInClass_STUProgramContentAndResources_STUProgramContentAndResourcesID",
                        column: x => x.STUProgramContentAndResourcesID,
                        principalTable: "STUProgramContentAndResources",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_STUTopicsCoveredInClass_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_STUTopicsCoveredInClass_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "KvkResourcePerson",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProgramContentAndResourcesId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Designation = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ResourceType = table.Column<int>(type: "int", maxLength: 150, nullable: false),
                    Responsibility = table.Column<int>(type: "int", maxLength: 250, nullable: false),
                    InstitutionOrDepartment = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KvkResourcePerson", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KvkResourcePerson_KvkProgramContentAndResources_ProgramContentAndResourcesId",
                        column: x => x.ProgramContentAndResourcesId,
                        principalTable: "KvkProgramContentAndResources",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_KvkResourcePerson_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_KvkResourcePerson_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "KvkTeachingAidsDeveloped",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProgramContentAndResourcesId = table.Column<int>(type: "int", nullable: false),
                    TypeOfAidDeveloped = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Other = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Purpose = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KvkTeachingAidsDeveloped", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KvkTeachingAidsDeveloped_KvkProgramContentAndResources_ProgramContentAndResourcesId",
                        column: x => x.ProgramContentAndResourcesId,
                        principalTable: "KvkProgramContentAndResources",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_KvkTeachingAidsDeveloped_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_KvkTeachingAidsDeveloped_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "KvkTopicsCoveredInClass",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProgramContentAndResourcesId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    PhotoUpload = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KvkTopicsCoveredInClass", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KvkTopicsCoveredInClass_KvkProgramContentAndResources_ProgramContentAndResourcesId",
                        column: x => x.ProgramContentAndResourcesId,
                        principalTable: "KvkProgramContentAndResources",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_KvkTopicsCoveredInClass_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_KvkTopicsCoveredInClass_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FIUAdvisoryServices_CreatedById",
                table: "FIUAdvisoryServices",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FIUAdvisoryServices_FIUProgramDetailsID",
                table: "FIUAdvisoryServices",
                column: "FIUProgramDetailsID");

            migrationBuilder.CreateIndex(
                name: "IX_FIUAdvisoryServices_UpdatedById",
                table: "FIUAdvisoryServices",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FIUParticipantDemographics_CreatedById",
                table: "FIUParticipantDemographics",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FIUParticipantDemographics_FIUProgramDetailsID",
                table: "FIUParticipantDemographics",
                column: "FIUProgramDetailsID");

            migrationBuilder.CreateIndex(
                name: "IX_FIUParticipantDemographics_UpdatedById",
                table: "FIUParticipantDemographics",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FIUProgramContentAndResources_CreatedById",
                table: "FIUProgramContentAndResources",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FIUProgramContentAndResources_FIUProgramDetailsID",
                table: "FIUProgramContentAndResources",
                column: "FIUProgramDetailsID");

            migrationBuilder.CreateIndex(
                name: "IX_FIUProgramContentAndResources_UpdatedById",
                table: "FIUProgramContentAndResources",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FIUProgramDetails_CreatedById",
                table: "FIUProgramDetails",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FIUProgramDetails_UpdatedById",
                table: "FIUProgramDetails",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FIURecommendation_CreatedById",
                table: "FIURecommendation",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FIURecommendation_FIUProgramDetailsID",
                table: "FIURecommendation",
                column: "FIUProgramDetailsID");

            migrationBuilder.CreateIndex(
                name: "IX_FIURecommendation_UpdatedById",
                table: "FIURecommendation",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FIUReports_CreatedById",
                table: "FIUReports",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FIUReports_FIUProgramDetailsID",
                table: "FIUReports",
                column: "FIUProgramDetailsID");

            migrationBuilder.CreateIndex(
                name: "IX_FIUReports_UpdatedById",
                table: "FIUReports",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FIUResourcePerson_CreatedById",
                table: "FIUResourcePerson",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FIUResourcePerson_FIUProgramContentAndResourcesID",
                table: "FIUResourcePerson",
                column: "FIUProgramContentAndResourcesID");

            migrationBuilder.CreateIndex(
                name: "IX_FIUResourcePerson_UpdatedById",
                table: "FIUResourcePerson",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FIUTeachingAidsDeveloped_CreatedById",
                table: "FIUTeachingAidsDeveloped",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FIUTeachingAidsDeveloped_FIUProgramContentAndResourcesID",
                table: "FIUTeachingAidsDeveloped",
                column: "FIUProgramContentAndResourcesID");

            migrationBuilder.CreateIndex(
                name: "IX_FIUTeachingAidsDeveloped_UpdatedById",
                table: "FIUTeachingAidsDeveloped",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FIUTopicsCoveredInClass_CreatedById",
                table: "FIUTopicsCoveredInClass",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FIUTopicsCoveredInClass_FIUProgramContentAndResourcesID",
                table: "FIUTopicsCoveredInClass",
                column: "FIUProgramContentAndResourcesID");

            migrationBuilder.CreateIndex(
                name: "IX_FIUTopicsCoveredInClass_UpdatedById",
                table: "FIUTopicsCoveredInClass",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FTIAdvisoryServices_CreatedById",
                table: "FTIAdvisoryServices",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FTIAdvisoryServices_FTIProgramDetailsID",
                table: "FTIAdvisoryServices",
                column: "FTIProgramDetailsID");

            migrationBuilder.CreateIndex(
                name: "IX_FTIAdvisoryServices_UpdatedById",
                table: "FTIAdvisoryServices",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FTIParticipantDemographics_CreatedById",
                table: "FTIParticipantDemographics",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FTIParticipantDemographics_FTIProgramDetailsID",
                table: "FTIParticipantDemographics",
                column: "FTIProgramDetailsID");

            migrationBuilder.CreateIndex(
                name: "IX_FTIParticipantDemographics_UpdatedById",
                table: "FTIParticipantDemographics",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FTIProgramContentAndResources_CreatedById",
                table: "FTIProgramContentAndResources",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FTIProgramContentAndResources_FTIProgramDetailsID",
                table: "FTIProgramContentAndResources",
                column: "FTIProgramDetailsID");

            migrationBuilder.CreateIndex(
                name: "IX_FTIProgramContentAndResources_UpdatedById",
                table: "FTIProgramContentAndResources",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FTIProgramDetails_CreatedById",
                table: "FTIProgramDetails",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FTIProgramDetails_UpdatedById",
                table: "FTIProgramDetails",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FTIRecommendation_CreatedById",
                table: "FTIRecommendation",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FTIRecommendation_FTIProgramDetailsID",
                table: "FTIRecommendation",
                column: "FTIProgramDetailsID");

            migrationBuilder.CreateIndex(
                name: "IX_FTIRecommendation_UpdatedById",
                table: "FTIRecommendation",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FTIReports_CreatedById",
                table: "FTIReports",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FTIReports_FTIProgramDetailsID",
                table: "FTIReports",
                column: "FTIProgramDetailsID");

            migrationBuilder.CreateIndex(
                name: "IX_FTIReports_UpdatedById",
                table: "FTIReports",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FTIResourcePerson_CreatedById",
                table: "FTIResourcePerson",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FTIResourcePerson_FTIProgramContentAndResourcesID",
                table: "FTIResourcePerson",
                column: "FTIProgramContentAndResourcesID");

            migrationBuilder.CreateIndex(
                name: "IX_FTIResourcePerson_UpdatedById",
                table: "FTIResourcePerson",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FTITeachingAidsDeveloped_CreatedById",
                table: "FTITeachingAidsDeveloped",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FTITeachingAidsDeveloped_FTIProgramContentAndResourcesID",
                table: "FTITeachingAidsDeveloped",
                column: "FTIProgramContentAndResourcesID");

            migrationBuilder.CreateIndex(
                name: "IX_FTITeachingAidsDeveloped_UpdatedById",
                table: "FTITeachingAidsDeveloped",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FTITopicsCoveredInClass_CreatedById",
                table: "FTITopicsCoveredInClass",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FTITopicsCoveredInClass_FTIProgramContentAndResourcesID",
                table: "FTITopicsCoveredInClass",
                column: "FTIProgramContentAndResourcesID");

            migrationBuilder.CreateIndex(
                name: "IX_FTITopicsCoveredInClass_UpdatedById",
                table: "FTITopicsCoveredInClass",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_KvkAdvisoryServices_CreatedById",
                table: "KvkAdvisoryServices",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_KvkAdvisoryServices_KVKProgramDetailsId",
                table: "KvkAdvisoryServices",
                column: "KVKProgramDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_KvkAdvisoryServices_UpdatedById",
                table: "KvkAdvisoryServices",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_KvkParticipantDemographics_CreatedById",
                table: "KvkParticipantDemographics",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_KvkParticipantDemographics_KVKProgramDetailsId",
                table: "KvkParticipantDemographics",
                column: "KVKProgramDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_KvkParticipantDemographics_UpdatedById",
                table: "KvkParticipantDemographics",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_KvkProgramContentAndResources_CreatedById",
                table: "KvkProgramContentAndResources",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_KvkProgramContentAndResources_KVKProgramDetailsId",
                table: "KvkProgramContentAndResources",
                column: "KVKProgramDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_KvkProgramContentAndResources_UpdatedById",
                table: "KvkProgramContentAndResources",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_KvkRecommendation_CreatedById",
                table: "KvkRecommendation",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_KvkRecommendation_KVKProgramDetailsId",
                table: "KvkRecommendation",
                column: "KVKProgramDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_KvkRecommendation_UpdatedById",
                table: "KvkRecommendation",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_KvkReports_CreatedById",
                table: "KvkReports",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_KvkReports_KVKProgramDetailsId",
                table: "KvkReports",
                column: "KVKProgramDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_KvkReports_UpdatedById",
                table: "KvkReports",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_KvkResourcePerson_CreatedById",
                table: "KvkResourcePerson",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_KvkResourcePerson_ProgramContentAndResourcesId",
                table: "KvkResourcePerson",
                column: "ProgramContentAndResourcesId");

            migrationBuilder.CreateIndex(
                name: "IX_KvkResourcePerson_UpdatedById",
                table: "KvkResourcePerson",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_KvkTeachingAidsDeveloped_CreatedById",
                table: "KvkTeachingAidsDeveloped",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_KvkTeachingAidsDeveloped_ProgramContentAndResourcesId",
                table: "KvkTeachingAidsDeveloped",
                column: "ProgramContentAndResourcesId");

            migrationBuilder.CreateIndex(
                name: "IX_KvkTeachingAidsDeveloped_UpdatedById",
                table: "KvkTeachingAidsDeveloped",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_KvkTopicsCoveredInClass_CreatedById",
                table: "KvkTopicsCoveredInClass",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_KvkTopicsCoveredInClass_ProgramContentAndResourcesId",
                table: "KvkTopicsCoveredInClass",
                column: "ProgramContentAndResourcesId");

            migrationBuilder.CreateIndex(
                name: "IX_KvkTopicsCoveredInClass_UpdatedById",
                table: "KvkTopicsCoveredInClass",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_RevolvingFundStatuses_CreatedById",
                table: "RevolvingFundStatuses",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_RevolvingFundStatuses_ServiceId",
                table: "RevolvingFundStatuses",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_RevolvingFundStatuses_UpdatedById",
                table: "RevolvingFundStatuses",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Services_CreatedById",
                table: "Services",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Services_OrganizationId",
                table: "Services",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_UnitLocationId",
                table: "Services",
                column: "UnitLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_UpdatedById",
                table: "Services",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_STUAdvisoryServices_CreatedById",
                table: "STUAdvisoryServices",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_STUAdvisoryServices_STUProgramDetailsID",
                table: "STUAdvisoryServices",
                column: "STUProgramDetailsID");

            migrationBuilder.CreateIndex(
                name: "IX_STUAdvisoryServices_UpdatedById",
                table: "STUAdvisoryServices",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_STUParticipantDemographics_CreatedById",
                table: "STUParticipantDemographics",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_STUParticipantDemographics_STUProgramDetailsID",
                table: "STUParticipantDemographics",
                column: "STUProgramDetailsID");

            migrationBuilder.CreateIndex(
                name: "IX_STUParticipantDemographics_UpdatedById",
                table: "STUParticipantDemographics",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_STUProgramContentAndResources_CreatedById",
                table: "STUProgramContentAndResources",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_STUProgramContentAndResources_STUProgramDetailsID",
                table: "STUProgramContentAndResources",
                column: "STUProgramDetailsID");

            migrationBuilder.CreateIndex(
                name: "IX_STUProgramContentAndResources_UpdatedById",
                table: "STUProgramContentAndResources",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_STUProgramDetails_CreatedById",
                table: "STUProgramDetails",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_STUProgramDetails_UpdatedById",
                table: "STUProgramDetails",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_STURecommendation_CreatedById",
                table: "STURecommendation",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_STURecommendation_STUProgramDetailsID",
                table: "STURecommendation",
                column: "STUProgramDetailsID");

            migrationBuilder.CreateIndex(
                name: "IX_STURecommendation_UpdatedById",
                table: "STURecommendation",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_STUReports_CreatedById",
                table: "STUReports",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_STUReports_STUProgramDetailsID",
                table: "STUReports",
                column: "STUProgramDetailsID");

            migrationBuilder.CreateIndex(
                name: "IX_STUReports_UpdatedById",
                table: "STUReports",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_STUResourcePerson_CreatedById",
                table: "STUResourcePerson",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_STUResourcePerson_STUProgramContentAndResourcesID",
                table: "STUResourcePerson",
                column: "STUProgramContentAndResourcesID");

            migrationBuilder.CreateIndex(
                name: "IX_STUResourcePerson_UpdatedById",
                table: "STUResourcePerson",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_STUTeachingAidsDeveloped_CreatedById",
                table: "STUTeachingAidsDeveloped",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_STUTeachingAidsDeveloped_STUProgramContentAndResourcesID",
                table: "STUTeachingAidsDeveloped",
                column: "STUProgramContentAndResourcesID");

            migrationBuilder.CreateIndex(
                name: "IX_STUTeachingAidsDeveloped_UpdatedById",
                table: "STUTeachingAidsDeveloped",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_STUTopicsCoveredInClass_CreatedById",
                table: "STUTopicsCoveredInClass",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_STUTopicsCoveredInClass_STUProgramContentAndResourcesID",
                table: "STUTopicsCoveredInClass",
                column: "STUProgramContentAndResourcesID");

            migrationBuilder.CreateIndex(
                name: "IX_STUTopicsCoveredInClass_UpdatedById",
                table: "STUTopicsCoveredInClass",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TableConsultingAndSocialMediaServices_CreatedById",
                table: "TableConsultingAndSocialMediaServices",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TableConsultingAndSocialMediaServices_Fk_ModeOrOutageId",
                table: "TableConsultingAndSocialMediaServices",
                column: "Fk_ModeOrOutageId");

            migrationBuilder.CreateIndex(
                name: "IX_TableConsultingAndSocialMediaServices_OrganizationId",
                table: "TableConsultingAndSocialMediaServices",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_TableConsultingAndSocialMediaServices_UnitLocationId",
                table: "TableConsultingAndSocialMediaServices",
                column: "UnitLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_TableConsultingAndSocialMediaServices_UpdatedById",
                table: "TableConsultingAndSocialMediaServices",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TableHostels_CreatedById",
                table: "TableHostels",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TableHostels_ServiceId",
                table: "TableHostels",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_TableHostels_UpdatedById",
                table: "TableHostels",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TableKVKProgramDetails_CreatedById",
                table: "TableKVKProgramDetails",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TableKVKProgramDetails_UpdatedById",
                table: "TableKVKProgramDetails",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TableModeAndOutages_CreatedById",
                table: "TableModeAndOutages",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TableModeAndOutages_UpdatedById",
                table: "TableModeAndOutages",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TableOtherActivity_CreatedById",
                table: "TableOtherActivity",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TableOtherActivity_OrganizationId",
                table: "TableOtherActivity",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_TableOtherActivity_UnitLocationId",
                table: "TableOtherActivity",
                column: "UnitLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_TableOtherActivity_UpdatedById",
                table: "TableOtherActivity",
                column: "UpdatedById");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FIUAdvisoryServices");

            migrationBuilder.DropTable(
                name: "FIUParticipantDemographics");

            migrationBuilder.DropTable(
                name: "FIURecommendation");

            migrationBuilder.DropTable(
                name: "FIUReports");

            migrationBuilder.DropTable(
                name: "FIUResourcePerson");

            migrationBuilder.DropTable(
                name: "FIUTeachingAidsDeveloped");

            migrationBuilder.DropTable(
                name: "FIUTopicsCoveredInClass");

            migrationBuilder.DropTable(
                name: "FTIAdvisoryServices");

            migrationBuilder.DropTable(
                name: "FTIParticipantDemographics");

            migrationBuilder.DropTable(
                name: "FTIRecommendation");

            migrationBuilder.DropTable(
                name: "FTIReports");

            migrationBuilder.DropTable(
                name: "FTIResourcePerson");

            migrationBuilder.DropTable(
                name: "FTITeachingAidsDeveloped");

            migrationBuilder.DropTable(
                name: "FTITopicsCoveredInClass");

            migrationBuilder.DropTable(
                name: "KvkAdvisoryServices");

            migrationBuilder.DropTable(
                name: "KvkParticipantDemographics");

            migrationBuilder.DropTable(
                name: "KvkRecommendation");

            migrationBuilder.DropTable(
                name: "KvkReports");

            migrationBuilder.DropTable(
                name: "KvkResourcePerson");

            migrationBuilder.DropTable(
                name: "KvkTeachingAidsDeveloped");

            migrationBuilder.DropTable(
                name: "KvkTopicsCoveredInClass");

            migrationBuilder.DropTable(
                name: "RevolvingFundStatuses");

            migrationBuilder.DropTable(
                name: "STUAdvisoryServices");

            migrationBuilder.DropTable(
                name: "STUParticipantDemographics");

            migrationBuilder.DropTable(
                name: "STURecommendation");

            migrationBuilder.DropTable(
                name: "STUReports");

            migrationBuilder.DropTable(
                name: "STUResourcePerson");

            migrationBuilder.DropTable(
                name: "STUTeachingAidsDeveloped");

            migrationBuilder.DropTable(
                name: "STUTopicsCoveredInClass");

            migrationBuilder.DropTable(
                name: "TableConsultingAndSocialMediaServices");

            migrationBuilder.DropTable(
                name: "TableHostels");

            migrationBuilder.DropTable(
                name: "TableOtherActivity");

            migrationBuilder.DropTable(
                name: "FIUProgramContentAndResources");

            migrationBuilder.DropTable(
                name: "FTIProgramContentAndResources");

            migrationBuilder.DropTable(
                name: "KvkProgramContentAndResources");

            migrationBuilder.DropTable(
                name: "STUProgramContentAndResources");

            migrationBuilder.DropTable(
                name: "TableModeAndOutages");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "FIUProgramDetails");

            migrationBuilder.DropTable(
                name: "FTIProgramDetails");

            migrationBuilder.DropTable(
                name: "TableKVKProgramDetails");

            migrationBuilder.DropTable(
                name: "STUProgramDetails");
        }
    }
}

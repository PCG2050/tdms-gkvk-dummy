using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Financial_Status_added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FinancialBudgets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UnitLocationId = table.Column<int>(type: "int", nullable: false),
                    OrganizationId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: true),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: true),
                    FormStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FormStatusRemarks = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ApprovedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ApprovedById = table.Column<int>(type: "int", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialBudgets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinancialBudgets_OrganizationUnitLocations_UnitLocationId",
                        column: x => x.UnitLocationId,
                        principalTable: "OrganizationUnitLocations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FinancialBudgets_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FinancialBudgets_Users_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FinancialBudgets_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FinancialBudgets_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Budgets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FinancialBudgetId = table.Column<int>(type: "int", nullable: false),
                    Particulars = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ABAC = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DAC = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Sanctioned = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Released = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Expenditure = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Budgets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Budgets_FinancialBudgets_FinancialBudgetId",
                        column: x => x.FinancialBudgetId,
                        principalTable: "FinancialBudgets",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Budgets_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Budgets_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DetailsOfBankAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FinancialBudgetId = table.Column<int>(type: "int", nullable: false),
                    NameOfBank = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    LocationBranch = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    BranchCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AccountName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    AccountNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MICRNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IFSCCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetailsOfBankAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetailsOfBankAccounts_FinancialBudgets_FinancialBudgetId",
                        column: x => x.FinancialBudgetId,
                        principalTable: "FinancialBudgets",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DetailsOfBankAccounts_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DetailsOfBankAccounts_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RevolvingFunds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FinancialBudgetId = table.Column<int>(type: "int", nullable: false),
                    YearMonth = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OpeningBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Expenditure = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Receipt = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ClosingBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RevolvingFunds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RevolvingFunds_FinancialBudgets_FinancialBudgetId",
                        column: x => x.FinancialBudgetId,
                        principalTable: "FinancialBudgets",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RevolvingFunds_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RevolvingFunds_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_CreatedById",
                table: "Budgets",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_FinancialBudgetId",
                table: "Budgets",
                column: "FinancialBudgetId");

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_UpdatedById",
                table: "Budgets",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_DetailsOfBankAccounts_CreatedById",
                table: "DetailsOfBankAccounts",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_DetailsOfBankAccounts_FinancialBudgetId",
                table: "DetailsOfBankAccounts",
                column: "FinancialBudgetId");

            migrationBuilder.CreateIndex(
                name: "IX_DetailsOfBankAccounts_UpdatedById",
                table: "DetailsOfBankAccounts",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialBudgets_ApprovedById",
                table: "FinancialBudgets",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialBudgets_CreatedById",
                table: "FinancialBudgets",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialBudgets_OrganizationId",
                table: "FinancialBudgets",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialBudgets_UnitLocationId",
                table: "FinancialBudgets",
                column: "UnitLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialBudgets_UpdatedById",
                table: "FinancialBudgets",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_RevolvingFunds_CreatedById",
                table: "RevolvingFunds",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_RevolvingFunds_FinancialBudgetId",
                table: "RevolvingFunds",
                column: "FinancialBudgetId");

            migrationBuilder.CreateIndex(
                name: "IX_RevolvingFunds_UpdatedById",
                table: "RevolvingFunds",
                column: "UpdatedById");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Budgets");

            migrationBuilder.DropTable(
                name: "DetailsOfBankAccounts");

            migrationBuilder.DropTable(
                name: "RevolvingFunds");

            migrationBuilder.DropTable(
                name: "FinancialBudgets");
        }
    }
}

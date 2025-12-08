using System;
using System.Collections.Generic;

namespace Application.Models.GenericTables
{
    // ===== BUDGET DTOs =====
    public class BudgetDto
    {
        public int Id { get; set; }
        public string? Particulars { get; set; }
        public decimal? ABAC { get; set; }
        public decimal? DAC { get; set; }
        public decimal? Sanctioned { get; set; }
        public decimal? Released { get; set; }
        public decimal? Expenditure { get; set; }
        public decimal? Balance { get; set; }
    }

    public class BudgetCreateDto
    {
        public string? Particulars { get; set; }
        public decimal? ABAC { get; set; }
        public decimal? DAC { get; set; }
        public decimal? Sanctioned { get; set; }
        public decimal? Released { get; set; }
        public decimal? Expenditure { get; set; }
        public decimal? Balance { get; set; }
    }

    public class BudgetUpdateDto
    {
        public int? Id { get; set; }  // Nullable: 0 or null = new, > 0 = update existing
        public string? Particulars { get; set; }
        public decimal? ABAC { get; set; }
        public decimal? DAC { get; set; }
        public decimal? Sanctioned { get; set; }
        public decimal? Released { get; set; }
        public decimal? Expenditure { get; set; }
        public decimal? Balance { get; set; }
    }

    // ===== REVOLVING FUND DTOs =====
    public class RevolvingFundDto
    {
        public int Id { get; set; }
        public string? YearMonth { get; set; }
        public decimal? OpeningBalance { get; set; }
        public decimal? Expenditure { get; set; }
        public decimal? Receipt { get; set; }
        public decimal? ClosingBalance { get; set; }
    }

    public class RevolvingFundCreateDto
    {
        public string? YearMonth { get; set; }
        public decimal? OpeningBalance { get; set; }
        public decimal? Expenditure { get; set; }
        public decimal? Receipt { get; set; }
        public decimal? ClosingBalance { get; set; }
    }

    public class RevolvingFundUpdateDto
    {
        public int? Id { get; set; }  // Nullable: 0 or null = new, > 0 = update existing
        public string? YearMonth { get; set; }
        public decimal? OpeningBalance { get; set; }
        public decimal? Expenditure { get; set; }
        public decimal? Receipt { get; set; }
        public decimal? ClosingBalance { get; set; }
    }

    // ===== BANK ACCOUNT DTOs =====
    public class BankAccountDto
    {
        public int Id { get; set; }
        public string? NameOfBank { get; set; }
        public string? LocationBranch { get; set; }
        public string? BranchCode { get; set; }
        public string? AccountName { get; set; }
        public string? AccountNumber { get; set; }
        public string? MICRNumber { get; set; }
        public string? IFSCCode { get; set; }
    }

    public class BankAccountCreateDto
    {
        public string? NameOfBank { get; set; }
        public string? LocationBranch { get; set; }
        public string? BranchCode { get; set; }
        public string? AccountName { get; set; }
        public string? AccountNumber { get; set; }
        public string? MICRNumber { get; set; }
        public string? IFSCCode { get; set; }
    }

    public class BankAccountUpdateDto
    {
        public int? Id { get; set; }  // Nullable: 0 or null = new, > 0 = update existing
        public string? NameOfBank { get; set; }
        public string? LocationBranch { get; set; }
        public string? BranchCode { get; set; }
        public string? AccountName { get; set; }
        public string? AccountNumber { get; set; }
        public string? MICRNumber { get; set; }
        public string? IFSCCode { get; set; }
    }

    // ===== FINANCIAL BUDGET DTOs =====
    public class FinancialBudgetDto
    {
        public int Id { get; set; }
        public int UnitLocationId { get; set; }
        public int OrganizationId { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public string FormStatus { get; set; } = "Draft";
        public string? FormStatusRemarks { get; set; }
        public DateTimeOffset? ApprovedAt { get; set; }
        public int? ApprovedById { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public int? CreatedById { get; set; }
    }

    public class FinancialBudgetCreateDto
    {
        public int UnitLocationId { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
    }

    public class FinancialBudgetUpdateDto
    {
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
    }

    /// <summary>
    /// Complete DTO with all child entities for hybrid insert
    /// </summary>
    public class FinancialBudgetCompleteDto
    {
        public FinancialBudgetDto? FinancialBudget { get; set; }
        public List<BudgetDto>? Budgets { get; set; }
        public List<RevolvingFundDto>? RevolvingFunds { get; set; }
        public List<BankAccountDto>? BankAccounts { get; set; }
    }

    /// <summary>
    /// Hybrid create DTO - creates parent and all children in one request
    /// </summary>
    public class FinancialBudgetHybridCreateDto
    {
        public int UnitLocationId { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }

        public List<BudgetCreateDto>? Budgets { get; set; }
        public List<RevolvingFundCreateDto>? RevolvingFunds { get; set; }
        public List<BankAccountCreateDto>? BankAccounts { get; set; }
    }

    /// <summary>
    /// Hybrid update DTO - updates parent and manages all children (create/update/delete) in one request
    /// Child items with Id = 0 or null will be created
    /// Child items with Id > 0 will be updated
    /// Child items not in the lists will be deleted (cascade)
    /// </summary>
    public class FinancialBudgetHybridUpdateDto
    {
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }

        public List<BudgetUpdateDto>? Budgets { get; set; }
        public List<RevolvingFundUpdateDto>? RevolvingFunds { get; set; }
        public List<BankAccountUpdateDto>? BankAccounts { get; set; }
    }
}

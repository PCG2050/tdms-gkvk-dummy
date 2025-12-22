using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Domain.Entities;

namespace Domain.Entities.GenericTables
{
    /// <summary>
    /// Main entity for Financial Status tracking
    /// Contains budgets, revolving funds, and bank account details
    /// </summary>
    public class FinancialBudget : AuditableBaseEntity
    {
        // ===== UNIT LOCATION AND ORGANIZATION REFERENCES =====
        [Required]
        public int UnitLocationId { get; set; }

        [Required]
        public int OrganizationId { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(UnitLocationId))]
        public OrganizationUnitLocation UnitLocation { get; set; } = null!;

        [JsonIgnore]
        public Organization Organization { get; set; } = null!;

        // ===== PERIOD =====
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }

        // ===== CHILD COLLECTIONS =====
        public ICollection<Budget> Budgets { get; set; } = new List<Budget>();
        public ICollection<RevolvingFund> RevolvingFunds { get; set; } = new List<RevolvingFund>();
        public ICollection<DetailsOfBankAccount> DetailsOfBankAccounts { get; set; } = new List<DetailsOfBankAccount>();

        // ===== STATUS TRACKING FIELDS =====
        [MaxLength(50)]
        public string FormStatus { get; set; } = "Draft";

        [MaxLength(1000)]
        public string? FormStatusRemarks { get; set; }

        // Approval tracking
        public DateTimeOffset? ApprovedAt { get; set; }
        public int? ApprovedById { get; set; }

        [JsonIgnore]
        public User? ApprovedBy { get; set; }
    }

    /// <summary>
    /// Budget details with sanctioned, released, expenditure, and balance amounts
    /// </summary>
    public class Budget : AuditableBaseEntity
    {
        [Required]
        public int FinancialBudgetId { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(FinancialBudgetId))]
        public FinancialBudget FinancialBudget { get; set; } = null!;

        [MaxLength(500)]
        public string? Particulars { get; set; }

        // ABAC and DAC are budget amounts, not strings
        public decimal? ABAC { get; set; }
        public decimal? DAC { get; set; }

        public decimal? Sanctioned { get; set; }
        public decimal? Released { get; set; }
        public decimal? Expenditure { get; set; }
        public decimal? Balance { get; set; }
        public decimal? PercentageOfExpenditure { get; set; }
    }

    /// <summary>
    /// Revolving fund tracking with opening/closing balance
    /// </summary>
    public class RevolvingFund : AuditableBaseEntity
    {
        [Required]
        public int FinancialBudgetId { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(FinancialBudgetId))]
        public FinancialBudget FinancialBudget { get; set; } = null!;

        [MaxLength(50)]
        public string? YearMonth { get; set; }

        public decimal? OpeningBalance { get; set; }
        public decimal? Expenditure { get; set; }
        public decimal? Receipt { get; set; }  // Fixed typo: Reciept → Receipt, changed to decimal
        public decimal? ClosingBalance { get; set; }
    }

    /// <summary>
    /// Bank account details for the unit
    /// </summary>
    public class DetailsOfBankAccount : AuditableBaseEntity
    {
        [Required]
        public int FinancialBudgetId { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(FinancialBudgetId))]
        public FinancialBudget FinancialBudget { get; set; } = null!;

        [MaxLength(200)]
        public string? NameOfBank { get; set; }

        [MaxLength(200)]
        public string? LocationBranch { get; set; }

        [MaxLength(50)]
        public string? BranchCode { get; set; }

        [MaxLength(200)]
        public string? AccountName { get; set; }

        [MaxLength(50)]
        public string? AccountNumber { get; set; }

        [MaxLength(50)]
        public string? MICRNumber { get; set; }

        [MaxLength(50)]
        public string? IFSCCode { get; set; }
    }
}

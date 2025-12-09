using Application.Models;
using Application.Models.DataTables;
using Application.Services.Common;

namespace Application.Interface.Services.DataTables
{
    public interface IFinancialBudgetService
    {
        // Parent entity operations
        Task<ServiceResult<FinancialBudgetDto>> CreateAsync(FinancialBudgetCreateDto dto);
        Task<ServiceResult<FinancialBudgetDto>> GetByIdAsync(int id);
        Task<ServiceResult<FinancialBudgetCompleteDto>> GetCompleteByIdAsync(int id);
        Task<ServiceResult<FinancialBudgetDto>> UpdateAsync(int id, FinancialBudgetUpdateDto dto);
        Task<ServiceResult> DeleteAsync(int id);

        // Pagination
        Task<PaginatedResult<FinancialBudgetDto>> GetPaginatedAsync(
            int pageNumber = 1,
            int pageSize = 10,
            DateOnly? startDate = null,
            DateOnly? endDate = null);

        Task<PaginatedResult<FinancialBudgetDto>> GetByStatusAsync(
            string status,
            int pageNumber = 1,
            int pageSize = 10);

        // Trainer History
        Task<PaginatedResult<TrainerHistoryItemDto>> GetTrainerHistoryAsync(
            int pageNumber = 1,
            int pageSize = 10);

        // Status management (no submit needed - Create/Update sets to Pending)
        Task<ServiceResult> ApproveAsync(int id, string? remarks = null);
        Task<ServiceResult> RejectAsync(int id, string remarks);

        // Child entity operations
        Task<ServiceResult<BudgetDto>> AddBudgetAsync(int financialBudgetId, BudgetCreateDto dto);
        Task<ServiceResult<RevolvingFundDto>> AddRevolvingFundAsync(int financialBudgetId, RevolvingFundCreateDto dto);
        Task<ServiceResult<BankAccountDto>> AddBankAccountAsync(int financialBudgetId, BankAccountCreateDto dto);

        Task<ServiceResult<BudgetDto>> UpdateBudgetAsync(int id, BudgetCreateDto dto);
        Task<ServiceResult<RevolvingFundDto>> UpdateRevolvingFundAsync(int id, RevolvingFundCreateDto dto);
        Task<ServiceResult<BankAccountDto>> UpdateBankAccountAsync(int id, BankAccountCreateDto dto);

        Task<ServiceResult> DeleteBudgetAsync(int id);
        Task<ServiceResult> DeleteRevolvingFundAsync(int id);
        Task<ServiceResult> DeleteBankAccountAsync(int id);

        // Hybrid endpoints
        Task<ServiceResult<FinancialBudgetCompleteDto>> CreateHybridAsync(FinancialBudgetHybridCreateDto dto);
        Task<ServiceResult<FinancialBudgetCompleteDto>> UpdateHybridAsync(int id, FinancialBudgetHybridUpdateDto dto);
    }
}

using Application.Models;
using Domain.Entities;

namespace Application.Interface.Repository.DataTables
{
    public interface IFinancialBudgetRepository
    {
        IQueryable<FinancialBudget> GetQueryable();
        Task<List<FinancialBudget>> GetAllAsync();
        Task<FinancialBudget?> GetByIdAsync(int id);
        Task<FinancialBudget?> GetWithDetailsAsync(int id);
        Task<FinancialBudget> CreateAsync(FinancialBudget entity);
        Task<FinancialBudget> UpdateAsync(FinancialBudget entity);
        Task<FinancialBudget> UpdateWithChildrenAsync(
            FinancialBudget parent,
            List<Budget>? budgets,
            List<RevolvingFund>? revolvingFunds,
            List<DetailsOfBankAccount>? bankAccounts);
        Task DeleteAsync(int id);

        Task<PaginatedResult<FinancialBudget>> GetPaginatedAsync(
            List<int> unitLocationIds,
            int pageNumber = 1,
            int pageSize = 10,
            DateOnly? startDate = null,
            DateOnly? endDate = null);

        Task<PaginatedResult<FinancialBudget>> GetByStatusAsync(
            List<int> unitLocationIds,
            string status,
            int pageNumber = 1,
            int pageSize = 10);

        // Child entity operations
        Task<Budget?> GetBudgetByIdAsync(int id);
        Task<RevolvingFund?> GetRevolvingFundByIdAsync(int id);
        Task<DetailsOfBankAccount?> GetBankAccountByIdAsync(int id);

        Task<Budget> AddBudgetAsync(Budget budget);
        Task<RevolvingFund> AddRevolvingFundAsync(RevolvingFund fund);
        Task<DetailsOfBankAccount> AddBankAccountAsync(DetailsOfBankAccount account);

        Task<Budget> UpdateBudgetAsync(Budget budget);
        Task<RevolvingFund> UpdateRevolvingFundAsync(RevolvingFund fund);
        Task<DetailsOfBankAccount> UpdateBankAccountAsync(DetailsOfBankAccount account);

        Task DeleteBudgetAsync(int id);
        Task DeleteRevolvingFundAsync(int id);
        Task DeleteBankAccountAsync(int id);
    }
}

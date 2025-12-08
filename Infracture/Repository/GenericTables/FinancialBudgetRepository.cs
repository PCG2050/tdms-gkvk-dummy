using Application.Interface.Repository.GenericTables;
using Application.Models;
using Domain.Entities.GenericTables;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.GenericTables
{
    public class FinancialBudgetRepository : IFinancialBudgetRepository
    {
        private readonly TdmsDbContext _context;

        public FinancialBudgetRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public IQueryable<FinancialBudget> GetQueryable()
        {
            return _context.FinancialBudgets.AsQueryable();
        }

        public async Task<FinancialBudget?> GetByIdAsync(int id)
        {
            return await _context.FinancialBudgets.FindAsync(id);
        }

        public async Task<FinancialBudget?> GetWithDetailsAsync(int id)
        {
            return await _context.FinancialBudgets
                .Include(f => f.Budgets)
                .Include(f => f.RevolvingFunds)
                .Include(f => f.DetailsOfBankAccounts)
                .Include(f => f.UnitLocation)
                    .ThenInclude(ul => ul.Unit)
                .Include(f => f.Organization)
                .Include(f => f.ApprovedBy)
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<FinancialBudget> CreateAsync(FinancialBudget entity)
        {
            _context.FinancialBudgets.Add(entity);
            await _context.SaveChangesAsync();
            return await GetWithDetailsAsync(entity.Id) ?? entity;
        }

        public async Task<FinancialBudget> UpdateAsync(FinancialBudget entity)
        {
            _context.FinancialBudgets.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<FinancialBudget> UpdateWithChildrenAsync(
            FinancialBudget parent,
            List<Budget>? budgets,
            List<RevolvingFund>? revolvingFunds,
            List<DetailsOfBankAccount>? bankAccounts)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var financialBudgetId = parent.Id;

                // Load existing entity with all children
                var existing = await GetWithDetailsAsync(financialBudgetId);
                if (existing == null)
                    throw new InvalidOperationException($"Financial budget with ID {financialBudgetId} not found");

                // 1. Update parent entity
                existing.StartDate = parent.StartDate;
                existing.EndDate = parent.EndDate;
                existing.FormStatus = parent.FormStatus;
                existing.UpdatedById = parent.UpdatedById;
                existing.UpdatedAt = parent.UpdatedAt;
                _context.FinancialBudgets.Update(existing);

                // 2. Process Budgets (Hybrid Pattern: Create/Update/Delete)
                if (budgets != null)
                {
                    var existingBudgets = existing.Budgets?.ToList() ?? new List<Budget>();
                    var incomingIds = budgets.Where(b => b.Id > 0).Select(b => b.Id).ToList();

                    // DELETE: Items in DB but not in incoming array
                    var budgetsToDelete = existingBudgets.Where(b => !incomingIds.Contains(b.Id)).ToList();
                    foreach (var budget in budgetsToDelete)
                    {
                        _context.Budgets.Remove(budget);
                    }

                    // CREATE or UPDATE
                    foreach (var budget in budgets)
                    {
                        if (budget.Id > 0)
                        {
                            // UPDATE existing
                            var existingBudget = existingBudgets.FirstOrDefault(b => b.Id == budget.Id);
                            if (existingBudget != null)
                            {
                                existingBudget.Particulars = budget.Particulars;
                                existingBudget.ABAC = budget.ABAC;
                                existingBudget.DAC = budget.DAC;
                                existingBudget.Sanctioned = budget.Sanctioned;
                                existingBudget.Released = budget.Released;
                                existingBudget.Expenditure = budget.Expenditure;
                                existingBudget.Balance = budget.Balance;
                                existingBudget.UpdatedById = budget.UpdatedById;
                                existingBudget.UpdatedAt = budget.UpdatedAt;
                                _context.Budgets.Update(existingBudget);
                            }
                        }
                        else
                        {
                            // CREATE new
                            budget.FinancialBudgetId = financialBudgetId;
                            _context.Budgets.Add(budget);
                        }
                    }
                }

                // 3. Process Revolving Funds (Hybrid Pattern)
                if (revolvingFunds != null)
                {
                    var existingFunds = existing.RevolvingFunds?.ToList() ?? new List<RevolvingFund>();
                    var incomingIds = revolvingFunds.Where(f => f.Id > 0).Select(f => f.Id).ToList();

                    // DELETE: Items in DB but not in incoming array
                    var fundsToDelete = existingFunds.Where(f => !incomingIds.Contains(f.Id)).ToList();
                    foreach (var fund in fundsToDelete)
                    {
                        _context.RevolvingFunds.Remove(fund);
                    }

                    // CREATE or UPDATE
                    foreach (var fund in revolvingFunds)
                    {
                        if (fund.Id > 0)
                        {
                            // UPDATE existing
                            var existingFund = existingFunds.FirstOrDefault(f => f.Id == fund.Id);
                            if (existingFund != null)
                            {
                                existingFund.YearMonth = fund.YearMonth;
                                existingFund.OpeningBalance = fund.OpeningBalance;
                                existingFund.Expenditure = fund.Expenditure;
                                existingFund.Receipt = fund.Receipt;
                                existingFund.ClosingBalance = fund.ClosingBalance;
                                existingFund.UpdatedById = fund.UpdatedById;
                                existingFund.UpdatedAt = fund.UpdatedAt;
                                _context.RevolvingFunds.Update(existingFund);
                            }
                        }
                        else
                        {
                            // CREATE new
                            fund.FinancialBudgetId = financialBudgetId;
                            _context.RevolvingFunds.Add(fund);
                        }
                    }
                }

                // 4. Process Bank Accounts (Hybrid Pattern)
                if (bankAccounts != null)
                {
                    var existingAccounts = existing.DetailsOfBankAccounts?.ToList() ?? new List<DetailsOfBankAccount>();
                    var incomingIds = bankAccounts.Where(a => a.Id > 0).Select(a => a.Id).ToList();

                    // DELETE: Items in DB but not in incoming array
                    var accountsToDelete = existingAccounts.Where(a => !incomingIds.Contains(a.Id)).ToList();
                    foreach (var account in accountsToDelete)
                    {
                        _context.DetailsOfBankAccounts.Remove(account);
                    }

                    // CREATE or UPDATE
                    foreach (var account in bankAccounts)
                    {
                        if (account.Id > 0)
                        {
                            // UPDATE existing
                            var existingAccount = existingAccounts.FirstOrDefault(a => a.Id == account.Id);
                            if (existingAccount != null)
                            {
                                existingAccount.NameOfBank = account.NameOfBank;
                                existingAccount.LocationBranch = account.LocationBranch;
                                existingAccount.BranchCode = account.BranchCode;
                                existingAccount.AccountName = account.AccountName;
                                existingAccount.AccountNumber = account.AccountNumber;
                                existingAccount.MICRNumber = account.MICRNumber;
                                existingAccount.IFSCCode = account.IFSCCode;
                                existingAccount.UpdatedById = account.UpdatedById;
                                existingAccount.UpdatedAt = account.UpdatedAt;
                                _context.DetailsOfBankAccounts.Update(existingAccount);
                            }
                        }
                        else
                        {
                            // CREATE new
                            account.FinancialBudgetId = financialBudgetId;
                            _context.DetailsOfBankAccounts.Add(account);
                        }
                    }
                }

                // Save all changes
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                // Return updated entity with all children
                return await GetWithDetailsAsync(financialBudgetId) ?? existing;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.FinancialBudgets.FindAsync(id);
            if (entity != null)
            {
                _context.FinancialBudgets.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<PaginatedResult<FinancialBudget>> GetPaginatedAsync(
            List<int> unitLocationIds,
            int pageNumber = 1,
            int pageSize = 10,
            DateOnly? startDate = null,
            DateOnly? endDate = null)
        {
            var query = _context.FinancialBudgets
                .Include(f => f.UnitLocation)
                    .ThenInclude(ul => ul.Unit)
                .Include(f => f.Organization)
                .Where(f => unitLocationIds.Contains(f.UnitLocationId))
                .AsQueryable();

            if (startDate.HasValue)
                query = query.Where(f => f.StartDate >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(f => f.EndDate <= endDate.Value);

            var totalItems = await query.CountAsync();
            var items = await query
                .OrderByDescending(f => f.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedResult<FinancialBudget>
            {
                Items = items,
                TotalItems = totalItems,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<PaginatedResult<FinancialBudget>> GetByStatusAsync(
            List<int> unitLocationIds,
            string status,
            int pageNumber = 1,
            int pageSize = 10)
        {
            var query = _context.FinancialBudgets
                .Include(f => f.UnitLocation)
                    .ThenInclude(ul => ul.Unit)
                .Include(f => f.Organization)
                .Where(f => unitLocationIds.Contains(f.UnitLocationId) && f.FormStatus == status);

            var totalItems = await query.CountAsync();
            var items = await query
                .OrderByDescending(f => f.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedResult<FinancialBudget>
            {
                Items = items,
                TotalItems = totalItems,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        // ===== CHILD ENTITY OPERATIONS =====

        public async Task<Budget?> GetBudgetByIdAsync(int id)
        {
            return await _context.Budgets.FindAsync(id);
        }

        public async Task<RevolvingFund?> GetRevolvingFundByIdAsync(int id)
        {
            return await _context.RevolvingFunds.FindAsync(id);
        }

        public async Task<DetailsOfBankAccount?> GetBankAccountByIdAsync(int id)
        {
            return await _context.DetailsOfBankAccounts.FindAsync(id);
        }

        public async Task<Budget> AddBudgetAsync(Budget budget)
        {
            _context.Budgets.Add(budget);
            await _context.SaveChangesAsync();
            return budget;
        }

        public async Task<RevolvingFund> AddRevolvingFundAsync(RevolvingFund fund)
        {
            _context.RevolvingFunds.Add(fund);
            await _context.SaveChangesAsync();
            return fund;
        }

        public async Task<DetailsOfBankAccount> AddBankAccountAsync(DetailsOfBankAccount account)
        {
            _context.DetailsOfBankAccounts.Add(account);
            await _context.SaveChangesAsync();
            return account;
        }

        public async Task<Budget> UpdateBudgetAsync(Budget budget)
        {
            _context.Budgets.Update(budget);
            await _context.SaveChangesAsync();
            return budget;
        }

        public async Task<RevolvingFund> UpdateRevolvingFundAsync(RevolvingFund fund)
        {
            _context.RevolvingFunds.Update(fund);
            await _context.SaveChangesAsync();
            return fund;
        }

        public async Task<DetailsOfBankAccount> UpdateBankAccountAsync(DetailsOfBankAccount account)
        {
            _context.DetailsOfBankAccounts.Update(account);
            await _context.SaveChangesAsync();
            return account;
        }

        public async Task DeleteBudgetAsync(int id)
        {
            var entity = await _context.Budgets.FindAsync(id);
            if (entity != null)
            {
                _context.Budgets.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteRevolvingFundAsync(int id)
        {
            var entity = await _context.RevolvingFunds.FindAsync(id);
            if (entity != null)
            {
                _context.RevolvingFunds.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteBankAccountAsync(int id)
        {
            var entity = await _context.DetailsOfBankAccounts.FindAsync(id);
            if (entity != null)
            {
                _context.DetailsOfBankAccounts.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}

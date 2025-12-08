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

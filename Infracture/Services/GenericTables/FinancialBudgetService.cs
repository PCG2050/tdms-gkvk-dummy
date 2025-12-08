using Application.Interface.Repository.GenericTables;
using Application.Interface.Services;
using Application.Interface.Services.GenericTables;
using Application.Models;
using Application.Models.GenericTables;
using Application.Services.Common;
using Domain.Entities.GenericTables;

namespace Infrastructure.Services.GenericTables
{
    public class FinancialBudgetService : IFinancialBudgetService
    {
        private readonly IFinancialBudgetRepository _repository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IEntityPermissionService _entityPermissionService;

        public FinancialBudgetService(
            IFinancialBudgetRepository repository,
            ICurrentUserService currentUserService,
            IEntityPermissionService entityPermissionService)
        {
            _repository = repository;
            _currentUserService = currentUserService;
            _entityPermissionService = entityPermissionService;
        }

        // ===== PARENT ENTITY OPERATIONS =====

        public async Task<ServiceResult<FinancialBudgetDto>> CreateAsync(FinancialBudgetCreateDto dto)
        {
            var entity = new FinancialBudget
            {
                UnitLocationId = dto.UnitLocationId,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                OrganizationId = _currentUserService.OrganizationId,
                FormStatus = "Pending",  // Set to Pending on create
                CreatedById = _currentUserService.UserId,
                CreatedAt = DateTimeOffset.UtcNow
            };

            var created = await _repository.CreateAsync(entity);
            var result = MapToDto(created);
            return ServiceResult<FinancialBudgetDto>.Success(result);
        }

        public async Task<ServiceResult<FinancialBudgetDto>> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                return ServiceResult<FinancialBudgetDto>.Failure("Financial budget not found", ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanViewForm(entity))
                return ServiceResult<FinancialBudgetDto>.Failure("Access denied", ServiceErrorStatus.FORBIDDEN);

            var dto = MapToDto(entity);
            return ServiceResult<FinancialBudgetDto>.Success(dto);
        }

        public async Task<ServiceResult<FinancialBudgetCompleteDto>> GetCompleteByIdAsync(int id)
        {
            var entity = await _repository.GetWithDetailsAsync(id);
            if (entity == null)
                return ServiceResult<FinancialBudgetCompleteDto>.Failure("Financial budget not found", ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanViewForm(entity))
                return ServiceResult<FinancialBudgetCompleteDto>.Failure("Access denied", ServiceErrorStatus.FORBIDDEN);

            var dto = MapToCompleteDto(entity);
            return ServiceResult<FinancialBudgetCompleteDto>.Success(dto);
        }

        public async Task<ServiceResult<FinancialBudgetDto>> UpdateAsync(int id, FinancialBudgetUpdateDto dto)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                return ServiceResult<FinancialBudgetDto>.Failure("Financial budget not found", ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanModifyForm(entity))
                return ServiceResult<FinancialBudgetDto>.Failure("Access denied", ServiceErrorStatus.FORBIDDEN);

            entity.StartDate = dto.StartDate;
            entity.EndDate = dto.EndDate;
            entity.FormStatus = "Pending";  // Set to Pending on update (even if approved)
            entity.UpdatedById = _currentUserService.UserId;
            entity.UpdatedAt = DateTimeOffset.UtcNow;

            var updated = await _repository.UpdateAsync(entity);
            var result = MapToDto(updated);
            return ServiceResult<FinancialBudgetDto>.Success(result);
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                return ServiceResult.Failure("Financial budget not found", ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanModifyForm(entity))
                return ServiceResult.Failure("Access denied", ServiceErrorStatus.FORBIDDEN);

            await _repository.DeleteAsync(id);
            return ServiceResult.Success();
        }

        // ===== PAGINATION =====

        public async Task<PaginatedResult<FinancialBudgetDto>> GetPaginatedAsync(
            int pageNumber = 1,
            int pageSize = 10,
            DateOnly? startDate = null,
            DateOnly? endDate = null)
        {
            var unitLocationIds = (await _currentUserService.MappedUnitLocationIds()).ToList();
            var result = await _repository.GetPaginatedAsync(unitLocationIds, pageNumber, pageSize, startDate, endDate);

            return new PaginatedResult<FinancialBudgetDto>
            {
                Items = result.Items.Select(MapToDto).ToList(),
                TotalItems = result.TotalItems,
                PageNumber = result.PageNumber,
                PageSize = result.PageSize
            };
        }

        public async Task<PaginatedResult<FinancialBudgetDto>> GetByStatusAsync(
            string status,
            int pageNumber = 1,
            int pageSize = 10)
        {
            var unitLocationIds = (await _currentUserService.MappedUnitLocationIds()).ToList();
            var result = await _repository.GetByStatusAsync(unitLocationIds, status, pageNumber, pageSize);

            return new PaginatedResult<FinancialBudgetDto>
            {
                Items = result.Items.Select(MapToDto).ToList(),
                TotalItems = result.TotalItems,
                PageNumber = result.PageNumber,
                PageSize = result.PageSize
            };
        }

        // ===== STATUS MANAGEMENT =====
        // Note: No SubmitForApproval needed - Create/Update sets to Pending

        public async Task<ServiceResult> ApproveAsync(int id, string? remarks = null)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                return ServiceResult.Failure("Financial budget not found", ServiceErrorStatus.NOTFOUND);

            if (_currentUserService.Role != Role.UNITHEAD && _currentUserService.Role != Role.ADMIN)
                return ServiceResult.Failure("Only Unit Heads and Admins can approve", ServiceErrorStatus.FORBIDDEN);

            if (entity.FormStatus != "Pending")
                return ServiceResult.Failure("Only pending entries can be approved", ServiceErrorStatus.INVALIDOPERATION);

            entity.FormStatus = "Approved";
            entity.FormStatusRemarks = remarks;
            entity.ApprovedById = _currentUserService.UserId;
            entity.ApprovedAt = DateTimeOffset.UtcNow;
            entity.UpdatedById = _currentUserService.UserId;
            entity.UpdatedAt = DateTimeOffset.UtcNow;

            await _repository.UpdateAsync(entity);
            return ServiceResult.Success();
        }

        public async Task<ServiceResult> RejectAsync(int id, string remarks)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                return ServiceResult.Failure("Financial budget not found", ServiceErrorStatus.NOTFOUND);

            if (_currentUserService.Role != Role.UNITHEAD && _currentUserService.Role != Role.ADMIN)
                return ServiceResult.Failure("Only Unit Heads and Admins can reject", ServiceErrorStatus.FORBIDDEN);

            if (entity.FormStatus != "Pending")
                return ServiceResult.Failure("Only pending entries can be rejected", ServiceErrorStatus.INVALIDOPERATION);

            if (string.IsNullOrWhiteSpace(remarks))
                return ServiceResult.Failure("Remarks are required for rejection", ServiceErrorStatus.BADREQUEST);

            entity.FormStatus = "Rejected";
            entity.FormStatusRemarks = remarks;
            entity.UpdatedById = _currentUserService.UserId;
            entity.UpdatedAt = DateTimeOffset.UtcNow;

            await _repository.UpdateAsync(entity);
            return ServiceResult.Success();
        }

        // ===== CHILD ENTITY OPERATIONS =====

        public async Task<ServiceResult<BudgetDto>> AddBudgetAsync(int financialBudgetId, BudgetCreateDto dto)
        {
            var parent = await _repository.GetByIdAsync(financialBudgetId);
            if (parent == null)
                return ServiceResult<BudgetDto>.Failure("Financial budget not found", ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanModifyForm(parent))
                return ServiceResult<BudgetDto>.Failure("Access denied", ServiceErrorStatus.FORBIDDEN);

            var budget = new Budget
            {
                FinancialBudgetId = financialBudgetId,
                Particulars = dto.Particulars,
                ABAC = dto.ABAC,
                DAC = dto.DAC,
                Sanctioned = dto.Sanctioned,
                Released = dto.Released,
                Expenditure = dto.Expenditure,
                Balance = dto.Balance,
                CreatedById = _currentUserService.UserId,
                CreatedAt = DateTimeOffset.UtcNow
            };

            var created = await _repository.AddBudgetAsync(budget);
            var result = MapToBudgetDto(created);
            return ServiceResult<BudgetDto>.Success(result);
        }

        public async Task<ServiceResult<RevolvingFundDto>> AddRevolvingFundAsync(int financialBudgetId, RevolvingFundCreateDto dto)
        {
            var parent = await _repository.GetByIdAsync(financialBudgetId);
            if (parent == null)
                return ServiceResult<RevolvingFundDto>.Failure("Financial budget not found", ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanModifyForm(parent))
                return ServiceResult<RevolvingFundDto>.Failure("Access denied", ServiceErrorStatus.FORBIDDEN);

            var fund = new RevolvingFund
            {
                FinancialBudgetId = financialBudgetId,
                YearMonth = dto.YearMonth,
                OpeningBalance = dto.OpeningBalance,
                Expenditure = dto.Expenditure,
                Receipt = dto.Receipt,
                ClosingBalance = dto.ClosingBalance,
                CreatedById = _currentUserService.UserId,
                CreatedAt = DateTimeOffset.UtcNow
            };

            var created = await _repository.AddRevolvingFundAsync(fund);
            var result = MapToRevolvingFundDto(created);
            return ServiceResult<RevolvingFundDto>.Success(result);
        }

        public async Task<ServiceResult<BankAccountDto>> AddBankAccountAsync(int financialBudgetId, BankAccountCreateDto dto)
        {
            var parent = await _repository.GetByIdAsync(financialBudgetId);
            if (parent == null)
                return ServiceResult<BankAccountDto>.Failure("Financial budget not found", ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanModifyForm(parent))
                return ServiceResult<BankAccountDto>.Failure("Access denied", ServiceErrorStatus.FORBIDDEN);

            var account = new DetailsOfBankAccount
            {
                FinancialBudgetId = financialBudgetId,
                NameOfBank = dto.NameOfBank,
                LocationBranch = dto.LocationBranch,
                BranchCode = dto.BranchCode,
                AccountName = dto.AccountName,
                AccountNumber = dto.AccountNumber,
                MICRNumber = dto.MICRNumber,
                IFSCCode = dto.IFSCCode,
                CreatedById = _currentUserService.UserId,
                CreatedAt = DateTimeOffset.UtcNow
            };

            var created = await _repository.AddBankAccountAsync(account);
            var result = MapToBankAccountDto(created);
            return ServiceResult<BankAccountDto>.Success(result);
        }

        public async Task<ServiceResult<BudgetDto>> UpdateBudgetAsync(int id, BudgetCreateDto dto)
        {
            // Implementation similar to Add but with Update logic
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<RevolvingFundDto>> UpdateRevolvingFundAsync(int id, RevolvingFundCreateDto dto)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<BankAccountDto>> UpdateBankAccountAsync(int id, BankAccountCreateDto dto)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult> DeleteBudgetAsync(int id)
        {
            await _repository.DeleteBudgetAsync(id);
            return ServiceResult.Success();
        }

        public async Task<ServiceResult> DeleteRevolvingFundAsync(int id)
        {
            await _repository.DeleteRevolvingFundAsync(id);
            return ServiceResult.Success();
        }

        public async Task<ServiceResult> DeleteBankAccountAsync(int id)
        {
            await _repository.DeleteBankAccountAsync(id);
            return ServiceResult.Success();
        }

        // ===== HYBRID CREATE - INSERT ALL DATA AT ONCE =====

        public async Task<ServiceResult<FinancialBudgetCompleteDto>> CreateHybridAsync(FinancialBudgetHybridCreateDto dto)
        {
            // Create parent entity
            var parent = new FinancialBudget
            {
                UnitLocationId = dto.UnitLocationId,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                OrganizationId = _currentUserService.OrganizationId,
                FormStatus = "Pending",  // Set to Pending on hybrid create
                CreatedById = _currentUserService.UserId,
                CreatedAt = DateTimeOffset.UtcNow
            };

            // Add child budgets
            if (dto.Budgets != null)
            {
                foreach (var budgetDto in dto.Budgets)
                {
                    parent.Budgets.Add(new Budget
                    {
                        Particulars = budgetDto.Particulars,
                        ABAC = budgetDto.ABAC,
                        DAC = budgetDto.DAC,
                        Sanctioned = budgetDto.Sanctioned,
                        Released = budgetDto.Released,
                        Expenditure = budgetDto.Expenditure,
                        Balance = budgetDto.Balance,
                        CreatedById = _currentUserService.UserId,
                        CreatedAt = DateTimeOffset.UtcNow
                    });
                }
            }

            // Add revolving funds
            if (dto.RevolvingFunds != null)
            {
                foreach (var fundDto in dto.RevolvingFunds)
                {
                    parent.RevolvingFunds.Add(new RevolvingFund
                    {
                        YearMonth = fundDto.YearMonth,
                        OpeningBalance = fundDto.OpeningBalance,
                        Expenditure = fundDto.Expenditure,
                        Receipt = fundDto.Receipt,
                        ClosingBalance = fundDto.ClosingBalance,
                        CreatedById = _currentUserService.UserId,
                        CreatedAt = DateTimeOffset.UtcNow
                    });
                }
            }

            // Add bank accounts
            if (dto.BankAccounts != null)
            {
                foreach (var accountDto in dto.BankAccounts)
                {
                    parent.DetailsOfBankAccounts.Add(new DetailsOfBankAccount
                    {
                        NameOfBank = accountDto.NameOfBank,
                        LocationBranch = accountDto.LocationBranch,
                        BranchCode = accountDto.BranchCode,
                        AccountName = accountDto.AccountName,
                        AccountNumber = accountDto.AccountNumber,
                        MICRNumber = accountDto.MICRNumber,
                        IFSCCode = accountDto.IFSCCode,
                        CreatedById = _currentUserService.UserId,
                        CreatedAt = DateTimeOffset.UtcNow
                    });
                }
            }

            // Save everything in one transaction
            var created = await _repository.CreateAsync(parent);
            var result = await _repository.GetWithDetailsAsync(created.Id);

            var completeDto = MapToCompleteDto(result!);
            return ServiceResult<FinancialBudgetCompleteDto>.Success(completeDto);
        }

        // ===== MAPPING HELPERS =====

        private FinancialBudgetDto MapToDto(FinancialBudget entity)
        {
            return new FinancialBudgetDto
            {
                Id = entity.Id,
                UnitLocationId = entity.UnitLocationId,
                OrganizationId = entity.OrganizationId,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
                FormStatus = entity.FormStatus,
                FormStatusRemarks = entity.FormStatusRemarks,
                ApprovedAt = entity.ApprovedAt,
                ApprovedById = entity.ApprovedById,
                CreatedAt = entity.CreatedAt,
                CreatedById = entity.CreatedById
            };
        }

        private FinancialBudgetCompleteDto MapToCompleteDto(FinancialBudget entity)
        {
            return new FinancialBudgetCompleteDto
            {
                FinancialBudget = MapToDto(entity),
                Budgets = entity.Budgets?.Select(MapToBudgetDto).ToList(),
                RevolvingFunds = entity.RevolvingFunds?.Select(MapToRevolvingFundDto).ToList(),
                BankAccounts = entity.DetailsOfBankAccounts?.Select(MapToBankAccountDto).ToList()
            };
        }

        private BudgetDto MapToBudgetDto(Budget entity)
        {
            return new BudgetDto
            {
                Id = entity.Id,
                Particulars = entity.Particulars,
                ABAC = entity.ABAC,
                DAC = entity.DAC,
                Sanctioned = entity.Sanctioned,
                Released = entity.Released,
                Expenditure = entity.Expenditure,
                Balance = entity.Balance
            };
        }

        private RevolvingFundDto MapToRevolvingFundDto(RevolvingFund entity)
        {
            return new RevolvingFundDto
            {
                Id = entity.Id,
                YearMonth = entity.YearMonth,
                OpeningBalance = entity.OpeningBalance,
                Expenditure = entity.Expenditure,
                Receipt = entity.Receipt,
                ClosingBalance = entity.ClosingBalance
            };
        }

        private BankAccountDto MapToBankAccountDto(DetailsOfBankAccount entity)
        {
            return new BankAccountDto
            {
                Id = entity.Id,
                NameOfBank = entity.NameOfBank,
                LocationBranch = entity.LocationBranch,
                BranchCode = entity.BranchCode,
                AccountName = entity.AccountName,
                AccountNumber = entity.AccountNumber,
                MICRNumber = entity.MICRNumber,
                IFSCCode = entity.IFSCCode
            };
        }
    }
}

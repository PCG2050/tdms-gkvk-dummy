using Application.Interface.Repository;
using Application.Interface.Repository.GenericTables;
using Application.Interface.Services;
using Application.Interface.Services.GenericTables;
using Application.Models;
using Application.Models.GenericTables;
using Application.Services.Common;
using Domain.Entities.GenericTables;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.GenericTables
{
    public class FinancialBudgetService : IFinancialBudgetService
    {
        private readonly IFinancialBudgetRepository _repository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IEntityPermissionService _entityPermissionService;
        private readonly IOrganizationUnitRepository _organizationUnitRepository;
        private readonly IUnitHeadAssignmentRepository _unitHeadAssignmentRepository;
        private readonly ITrainerAssignmentRepository _trainerAssignmentRepository;
        private readonly IUserRepository _userRepository;
        private readonly GenericTrainerHistoryService<FinancialBudget> _historyService;

        public FinancialBudgetService(
            IFinancialBudgetRepository repository,
            ICurrentUserService currentUserService,
            IEntityPermissionService entityPermissionService,
            IOrganizationUnitRepository organizationUnitRepository,
            IUnitHeadAssignmentRepository unitHeadAssignmentRepository,
            IUserRepository userRepository,
            ITrainerAssignmentRepository trainerAssignmentRepository)
        {
            _repository = repository;
            _currentUserService = currentUserService;
            _entityPermissionService = entityPermissionService;
            _organizationUnitRepository = organizationUnitRepository;
            _unitHeadAssignmentRepository = unitHeadAssignmentRepository;
            _trainerAssignmentRepository = trainerAssignmentRepository;
            _userRepository = userRepository;
            _historyService = new GenericTrainerHistoryService<FinancialBudget>(
                currentUserService,
                trainerAssignmentRepository,
                organizationUnitRepository,
                unitHeadAssignmentRepository,
                userRepository);
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

        // ===== TRAINER HISTORY =====

        public async Task<PaginatedResult<TrainerHistoryItemDto>> GetTrainerHistoryAsync(
            int pageNumber = 1,
            int pageSize = 10)
        {
            var query = _repository.GetQueryable()
                .Include(x => x.UnitLocation)
                    .ThenInclude(ul => ul.Unit);

            return await _historyService.GetTrainerHistoryAsync(
                query,
                getUnitLocationId: x => x.UnitLocationId,
                getTitleOrName: x => x.StartDate.HasValue && x.EndDate.HasValue
                    ? $"Financial Budget ({x.StartDate:yyyy-MM-dd} to {x.EndDate:yyyy-MM-dd})"
                    : "Financial Budget",
                getFormStatus: x => x.FormStatus,
                getRemarks: x => x.FormStatusRemarks ?? "-",
                pageNumber,
                pageSize);
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
            var entity = await _repository.GetBudgetByIdAsync(id);
            if (entity == null)
                return ServiceResult<BudgetDto>.Failure("Budget not found", ServiceErrorStatus.NOTFOUND);

            // Check permissions on parent entity
            var parent = await _repository.GetByIdAsync(entity.FinancialBudgetId);
            if (parent == null || !await _entityPermissionService.CanModifyForm(parent))
                return ServiceResult<BudgetDto>.Failure("Access denied", ServiceErrorStatus.FORBIDDEN);

            // Update entity
            entity.Particulars = dto.Particulars;
            entity.ABAC = dto.ABAC;
            entity.DAC = dto.DAC;
            entity.Sanctioned = dto.Sanctioned;
            entity.Released = dto.Released;
            entity.Expenditure = dto.Expenditure;
            entity.Balance = dto.Balance;
            entity.UpdatedById = _currentUserService.UserId;
            entity.UpdatedAt = DateTimeOffset.UtcNow;

            var updated = await _repository.UpdateBudgetAsync(entity);
            return ServiceResult<BudgetDto>.Success(MapToBudgetDto(updated));
        }

        public async Task<ServiceResult<RevolvingFundDto>> UpdateRevolvingFundAsync(int id, RevolvingFundCreateDto dto)
        {
            var entity = await _repository.GetRevolvingFundByIdAsync(id);
            if (entity == null)
                return ServiceResult<RevolvingFundDto>.Failure("Revolving fund not found", ServiceErrorStatus.NOTFOUND);

            // Check permissions on parent entity
            var parent = await _repository.GetByIdAsync(entity.FinancialBudgetId);
            if (parent == null || !await _entityPermissionService.CanModifyForm(parent))
                return ServiceResult<RevolvingFundDto>.Failure("Access denied", ServiceErrorStatus.FORBIDDEN);

            // Update entity
            entity.YearMonth = dto.YearMonth;
            entity.OpeningBalance = dto.OpeningBalance;
            entity.Expenditure = dto.Expenditure;
            entity.Receipt = dto.Receipt;
            entity.ClosingBalance = dto.ClosingBalance;
            entity.UpdatedById = _currentUserService.UserId;
            entity.UpdatedAt = DateTimeOffset.UtcNow;

            var updated = await _repository.UpdateRevolvingFundAsync(entity);
            return ServiceResult<RevolvingFundDto>.Success(MapToRevolvingFundDto(updated));
        }

        public async Task<ServiceResult<BankAccountDto>> UpdateBankAccountAsync(int id, BankAccountCreateDto dto)
        {
            var entity = await _repository.GetBankAccountByIdAsync(id);
            if (entity == null)
                return ServiceResult<BankAccountDto>.Failure("Bank account not found", ServiceErrorStatus.NOTFOUND);

            // Check permissions on parent entity
            var parent = await _repository.GetByIdAsync(entity.FinancialBudgetId);
            if (parent == null || !await _entityPermissionService.CanModifyForm(parent))
                return ServiceResult<BankAccountDto>.Failure("Access denied", ServiceErrorStatus.FORBIDDEN);

            // Update entity
            entity.NameOfBank = dto.NameOfBank;
            entity.LocationBranch = dto.LocationBranch;
            entity.BranchCode = dto.BranchCode;
            entity.AccountName = dto.AccountName;
            entity.AccountNumber = dto.AccountNumber;
            entity.MICRNumber = dto.MICRNumber;
            entity.IFSCCode = dto.IFSCCode;
            entity.UpdatedById = _currentUserService.UserId;
            entity.UpdatedAt = DateTimeOffset.UtcNow;

            var updated = await _repository.UpdateBankAccountAsync(entity);
            return ServiceResult<BankAccountDto>.Success(MapToBankAccountDto(updated));
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
            // Validate user has permission to create forms for this unit
            if (!await CanUserAccessUnitLocationAsync(dto.UnitLocationId))
                return ServiceResult<FinancialBudgetCompleteDto>.Failure(
                    "Access denied to unit location",
                    ServiceErrorStatus.FORBIDDEN);

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

        public async Task<ServiceResult<FinancialBudgetCompleteDto>> UpdateHybridAsync(int id, FinancialBudgetHybridUpdateDto dto)
        {
            // Validate entity exists and check permissions
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                return ServiceResult<FinancialBudgetCompleteDto>.Failure("Financial budget not found", ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanModifyForm(entity))
                return ServiceResult<FinancialBudgetCompleteDto>.Failure("Access denied", ServiceErrorStatus.FORBIDDEN);

            try
            {
                // Prepare parent entity for update
                var parentEntity = new FinancialBudget
                {
                    Id = id,
                    StartDate = dto.StartDate,
                    EndDate = dto.EndDate,
                    FormStatus = "Pending",  // Set to Pending on update
                    UpdatedById = _currentUserService.UserId,
                    UpdatedAt = DateTimeOffset.UtcNow
                };

                // Prepare child budgets (hybrid: mix of new and existing)
                var budgets = dto.Budgets?.Select(b =>
                {
                    var budget = new Budget
                    {
                        Id = b.Id ?? 0,  // 0 means new
                        Particulars = b.Particulars,
                        ABAC = b.ABAC,
                        DAC = b.DAC,
                        Sanctioned = b.Sanctioned,
                        Released = b.Released,
                        Expenditure = b.Expenditure,
                        Balance = b.Balance
                    };

                    if (budget.Id == 0)
                    {
                        budget.CreatedById = _currentUserService.UserId;
                        budget.CreatedAt = DateTimeOffset.UtcNow;
                    }
                    else
                    {
                        budget.UpdatedById = _currentUserService.UserId;
                        budget.UpdatedAt = DateTimeOffset.UtcNow;
                    }

                    return budget;
                }).ToList();

                // Prepare revolving funds
                var revolvingFunds = dto.RevolvingFunds?.Select(f =>
                {
                    var fund = new RevolvingFund
                    {
                        Id = f.Id ?? 0,
                        YearMonth = f.YearMonth,
                        OpeningBalance = f.OpeningBalance,
                        Expenditure = f.Expenditure,
                        Receipt = f.Receipt,
                        ClosingBalance = f.ClosingBalance
                    };

                    if (fund.Id == 0)
                    {
                        fund.CreatedById = _currentUserService.UserId;
                        fund.CreatedAt = DateTimeOffset.UtcNow;
                    }
                    else
                    {
                        fund.UpdatedById = _currentUserService.UserId;
                        fund.UpdatedAt = DateTimeOffset.UtcNow;
                    }

                    return fund;
                }).ToList();

                // Prepare bank accounts
                var bankAccounts = dto.BankAccounts?.Select(a =>
                {
                    var account = new DetailsOfBankAccount
                    {
                        Id = a.Id ?? 0,
                        NameOfBank = a.NameOfBank,
                        LocationBranch = a.LocationBranch,
                        BranchCode = a.BranchCode,
                        AccountName = a.AccountName,
                        AccountNumber = a.AccountNumber,
                        MICRNumber = a.MICRNumber,
                        IFSCCode = a.IFSCCode
                    };

                    if (account.Id == 0)
                    {
                        account.CreatedById = _currentUserService.UserId;
                        account.CreatedAt = DateTimeOffset.UtcNow;
                    }
                    else
                    {
                        account.UpdatedById = _currentUserService.UserId;
                        account.UpdatedAt = DateTimeOffset.UtcNow;
                    }

                    return account;
                }).ToList();

                // Repository handles transaction internally
                var updated = await _repository.UpdateWithChildrenAsync(
                    parentEntity,
                    budgets,
                    revolvingFunds,
                    bankAccounts);

                var resultDto = MapToCompleteDto(updated);
                return ServiceResult<FinancialBudgetCompleteDto>.Success(resultDto);
            }
            catch (Exception ex)
            {
                return ServiceResult<FinancialBudgetCompleteDto>.Failure(
                    $"Failed to update financial budget with children: {ex.Message}",
                    ServiceErrorStatus.INVALIDOPERATION);
            }
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

        // ===== HELPER METHODS FOR PERMISSION CHECKS =====

        private async Task<bool> CanUserAccessUnitLocationAsync(int unitLocationId)
        {
            var accessibleIds = await GetAccessibleUnitLocationIdsAsync();
            return accessibleIds.Contains(unitLocationId);
        }

        private async Task<List<int>> GetAccessibleUnitLocationIdsAsync()
        {
            if (_currentUserService.Role == Role.TRAINER)
                return await _trainerAssignmentRepository.GetUnitLocationIdsByTrainerIdAsync(_currentUserService.UserId);

            if (_currentUserService.Role == Role.UNITHEAD)
                return await _unitHeadAssignmentRepository.GetUnitLocationIdsByUnitHeadIdAsync(_currentUserService.UserId);

            if (_currentUserService.Role == Role.ADMIN)
                return await _organizationUnitRepository.GetUnitLocationIdsByOrganizationIdAsync(_currentUserService.OrganizationId);

            return new List<int>();
        }
    }
}

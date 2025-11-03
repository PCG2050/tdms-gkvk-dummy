using Application.Interface;
using Application.Interface.Repository.DataTables.FIU;
using Application.Interface.Services.Common;
using Application.Interface.Services.DataTables.FIU;
using Application.Mapper.DataTable.FIU;
using Application.Models;
using Application.Models.DataTables.FIU;
using Domain.Entities.Enum;
using Domain.Entities.FIU;

namespace Infrastructure.Services.DataTables.FIU
{
    public class FIUProgramActivityService : IFIUProgramActivityService
    {
        private readonly IFIUProgramActivityRepository _repository;
        private readonly FIUProgramActivityMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly IEntityPermissionService _entityPermissionService;

        public FIUProgramActivityService(
            IFIUProgramActivityRepository repository,
            FIUProgramActivityMapper mapper,
            ICurrentUserService currentUserService,
            IEntityPermissionService entityPermissionService)
        {
            _repository = repository;
            _mapper = mapper;
            _currentUserService = currentUserService;
            _entityPermissionService = entityPermissionService;
        }

        // ==========================================
        // CRUD OPERATIONS
        // ==========================================

        public async Task<ServiceResult<FIUProgramActivityDto>> AddAsync(FIUProgramActivityDto dto)
        {
            var entity = _mapper.MapToEntity(dto);

            // Set audit fields
            entity.CreatedById = _currentUserService.UserId;
            entity.CreatedAt = DateTimeOffset.UtcNow;
            entity.OrganizationId = _currentUserService.OrganizationId;
            entity.FormStatus = "Draft"; // Initial status

            var result = await _repository.AddAsync(entity);
            var resultDto = _mapper.MapToDto(result);

            return ServiceResult<FIUProgramActivityDto>.Success(resultDto);
        }

        public async Task<ServiceResult<FIUProgramActivityDto>> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
                return ServiceResult<FIUProgramActivityDto>.Failure(
                    "FIU Program Activity not found",
                    ServiceErrorStatus.NOTFOUND);

            // Check if user has permission to view
            if (!await _entityPermissionService.CanViewForm(entity))
                return ServiceResult<FIUProgramActivityDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            var dto = _mapper.MapToDto(entity);
            return ServiceResult<FIUProgramActivityDto>.Success(dto);
        }

        public async Task<IEnumerable<FIUProgramActivityDto>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();

            // Filter based on user permissions
            var accessibleEntities = new List<FIUProgramActivity>();
            foreach (var entity in entities)
            {
                if (await _entityPermissionService.CanViewForm(entity))
                    accessibleEntities.Add(entity);
            }

            return accessibleEntities.Select(_mapper.MapToDto);
        }

        public async Task<ServiceResult<FIUProgramActivityDto>> UpdateAsync(int id, FIUProgramActivityDto dto)
        {
            var existing = await _repository.GetByIdAsync(id);

            if (existing == null)
                return ServiceResult<FIUProgramActivityDto>.Failure(
                    "FIU Program Activity not found",
                    ServiceErrorStatus.NOTFOUND);

            // Check if user can modify
            if (!await _entityPermissionService.CanModifyForm(existing))
                return ServiceResult<FIUProgramActivityDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            // Cannot edit Approved entries
            if (existing.FormStatus == "Approved")
                return ServiceResult<FIUProgramActivityDto>.Failure(
                    "Cannot modify approved activities",
                    ServiceErrorStatus.INVALIDOPERATION);

            // Update fields from DTO
            existing.FIUActivitiesId = dto.FIUActivitiesId;
            existing.Number = dto.Number;
            existing.UploadMediaUrl = dto.UploadMediaUrl;
            existing.StartDate = dto.StartDate ?? existing.StartDate;
            existing.EndDate = dto.EndDate ?? existing.EndDate;
            existing.UpdatedById = _currentUserService.UserId;
            existing.UpdatedAt = DateTimeOffset.UtcNow;

            // Auto-reset to Draft if Pending or Rejected
            if (existing.FormStatus == "Pending" || existing.FormStatus == "Rejected")
            {
                string originalStatus = existing.FormStatus;
                existing.FormStatus = "Draft";
                existing.FormStatusRemarks = $"Edited by trainer after {originalStatus} status. Reset to Draft.";
                existing.ApprovedById = null;
                existing.ApprovedAt = null;
            }

            var updated = await _repository.UpdateAsync(existing);
            var resultDto = _mapper.MapToDto(updated);

            return ServiceResult<FIUProgramActivityDto>.Success(resultDto);
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
                return ServiceResult.Failure(
                    "FIU Program Activity not found",
                    ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanModifyForm(entity))
                return ServiceResult.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            // Can only delete Draft entries
            if (entity.FormStatus != "Draft")
                return ServiceResult.Failure(
                    "Only draft activities can be deleted",
                    ServiceErrorStatus.INVALIDOPERATION);

            await _repository.DeleteAsync(id);
            return ServiceResult.Success();
        }

        // ==========================================
        // FORM STATUS WORKFLOW
        // ==========================================

        public async Task<ServiceResult> SubmitForApprovalAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
                return ServiceResult.Failure(
                    "FIU Program Activity not found",
                    ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanModifyForm(entity))
                return ServiceResult.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            if (entity.FormStatus != "Draft" && entity.FormStatus != "Saved")
                return ServiceResult.Failure(
                    "Only draft activities can be submitted",
                    ServiceErrorStatus.INVALIDOPERATION);

            entity.FormStatus = "Pending";
            entity.UpdatedById = _currentUserService.UserId;
            entity.UpdatedAt = DateTimeOffset.UtcNow;

            await _repository.UpdateAsync(entity);
            return ServiceResult.Success();
        }

        public async Task<ServiceResult> ApproveAsync(int id, string? remarks = null)
        {
            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
                return ServiceResult.Failure(
                    "FIU Program Activity not found",
                    ServiceErrorStatus.NOTFOUND);

            // Only UnitHead or Admin can approve
            if (_currentUserService.Role != Role.UNITHEAD && _currentUserService.Role != Role.ADMIN)
                return ServiceResult.Failure(
                    "Only Unit Heads and Admins can approve activities",
                    ServiceErrorStatus.FORBIDDEN);

            if (!await _entityPermissionService.CanModifyForm(entity))
                return ServiceResult.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            if (entity.FormStatus != "Pending")
                return ServiceResult.Failure(
                    "Only pending activities can be approved",
                    ServiceErrorStatus.INVALIDOPERATION);

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
                return ServiceResult.Failure(
                    "FIU Program Activity not found",
                    ServiceErrorStatus.NOTFOUND);

            // Only UnitHead or Admin can reject
            if (_currentUserService.Role != Role.UNITHEAD && _currentUserService.Role != Role.ADMIN)
                return ServiceResult.Failure(
                    "Only Unit Heads and Admins can reject activities",
                    ServiceErrorStatus.FORBIDDEN);

            if (!await _entityPermissionService.CanModifyForm(entity))
                return ServiceResult.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            if (entity.FormStatus != "Pending")
                return ServiceResult.Failure(
                    "Only pending activities can be rejected",
                    ServiceErrorStatus.INVALIDOPERATION);

            if (string.IsNullOrWhiteSpace(remarks))
                return ServiceResult.Failure(
                    "Remarks are required for rejection",
                    ServiceErrorStatus.INVALIDOPERATION);

            entity.FormStatus = "Rejected";
            entity.FormStatusRemarks = remarks;
            entity.ApprovedById = _currentUserService.UserId;
            entity.ApprovedAt = DateTimeOffset.UtcNow;
            entity.UpdatedById = _currentUserService.UserId;
            entity.UpdatedAt = DateTimeOffset.UtcNow;

            await _repository.UpdateAsync(entity);
            return ServiceResult.Success();
        }

        // ==========================================
        // PAGINATION & FILTERING
        // ==========================================

        public async Task<PaginatedResult<FIUProgramActivityDto>> GetPaginatedAsync(
            int pageNumber = 1,
            int pageSize = 10,
            DateOnly? startDate = null,
            DateOnly? endDate = null,
            int? unitLocationId = null,
            string? searchTerm = null)
        {
            // Get trainer IDs based on current user's role
            var trainerIds = await GetAccessibleTrainerIds();

            var result = await _repository.GetPaginatedAsync(
                trainerIds,
                pageNumber,
                pageSize,
                startDate,
                endDate,
                unitLocationId,
                searchTerm);

            var dtoItems = result.Items.Select(_mapper.MapToDto).ToList();

            return new PaginatedResult<FIUProgramActivityDto>
            {
                Items = dtoItems,
                TotalItems = result.TotalItems,
                PageNumber = result.PageNumber,
                PageSize = result.PageSize
            };
        }

        public async Task<PaginatedResult<FIUProgramActivityDto>> GetByStatusAsync(
            string status,
            int pageNumber = 1,
            int pageSize = 10)
        {
            var trainerIds = await GetAccessibleTrainerIds();

            var result = await _repository.GetByStatusAsync(
                trainerIds,
                status,
                pageNumber,
                pageSize);

            var dtoItems = result.Items.Select(_mapper.MapToDto).ToList();

            return new PaginatedResult<FIUProgramActivityDto>
            {
                Items = dtoItems,
                TotalItems = result.TotalItems,
                PageNumber = result.PageNumber,
                PageSize = result.PageSize
            };
        }

        public async Task<Dictionary<string, int>> GetStatusSummaryAsync()
        {
            var trainerIds = await GetAccessibleTrainerIds();
            return await _repository.GetStatusSummaryAsync(trainerIds);
        }

        // ==========================================
        // HELPER METHODS
        // ==========================================

        private async Task<List<int>> GetAccessibleTrainerIds()
        {
            // This logic should be similar to what's in EEUReportController
            // Returns list of trainer IDs accessible to current user based on their role

            var userId = _currentUserService.UserId;
            var role = _currentUserService.Role;

            return role switch
            {
                Role.TRAINER => new List<int> { userId },
                Role.UNITHEAD => await GetTrainersCreatedByUnitHead(userId),
                Role.ADMIN => await GetTrainersUnderAdmin(userId),
                Role.SUPERADMIN => await GetAllTrainers(),
                _ => new List<int>()
            };
        }

        private async Task<List<int>> GetTrainersCreatedByUnitHead(int unitHeadId)
        {
            // Implementation would query Users table for trainers created by this UnitHead
            // This is a placeholder - actual implementation would use UserRepository
            return new List<int>();
        }

        private async Task<List<int>> GetTrainersUnderAdmin(int adminId)
        {
            // Implementation would query hierarchy: Admin → UnitHeads → Trainers
            // This is a placeholder - actual implementation would use UserRepository
            return new List<int>();
        }

        private async Task<List<int>> GetAllTrainers()
        {
            // Implementation would get all trainers
            // This is a placeholder - actual implementation would use UserRepository
            return new List<int>();
        }
    }
}
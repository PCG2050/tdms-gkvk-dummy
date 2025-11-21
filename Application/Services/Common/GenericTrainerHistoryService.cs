using Application.Interface;
using Application.Interface.Repository;
using Application.Models;
using Domain.Entities;
using Domain.Entities.Enum;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Common
{
    /// <summary>
    /// Generic service for handling trainer history across all data entry controllers
    /// Provides common pagination and filtering for trainer and unit head submissions
    /// </summary>
    /// <typeparam name="TEntity">The entity type (e.g., Publication, NominationReward)</typeparam>
    public class GenericTrainerHistoryService<TEntity> where TEntity : AuditableBaseEntity
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly ITrainerAssignmentRepository _trainerAssignmentRepository;
        private readonly IOrganizationUnitRepository _organizationUnitRepository;
        private readonly IUnitHeadAssignmentRepository _unitHeadAssignmentRepository;

        public GenericTrainerHistoryService(
            ICurrentUserService currentUserService,
            ITrainerAssignmentRepository trainerAssignmentRepository,
            IOrganizationUnitRepository organizationUnitRepository,
            IUnitHeadAssignmentRepository unitHeadAssignmentRepository)
        {
            _currentUserService = currentUserService;
            _trainerAssignmentRepository = trainerAssignmentRepository;
            _organizationUnitRepository = organizationUnitRepository;
            _unitHeadAssignmentRepository = unitHeadAssignmentRepository;
        }

        /// <summary>
        /// Get trainer or unit head submission history with pagination
        /// Filters by CreatedById and accessible unit locations
        /// </summary>
        public async Task<PaginatedResult<TrainerHistoryItemDto>> GetTrainerHistoryAsync(
            IQueryable<TEntity> query,
            Func<TEntity, int> getUnitLocationId,
            Func<TEntity, string?> getTitleOrName,
            Func<TEntity, string> getFormStatus,
            int pageNumber = 1,
            int pageSize = 10)
        {
            // Get user's accessible unit locations based on their role
            var userId = _currentUserService.UserId;
            var role = _currentUserService.Role;

            List<int> unitLocationIds;
            if (role == Role.TRAINER)
            {
                unitLocationIds = await _trainerAssignmentRepository
                    .GetUnitLocationIdsByTrainerIdAsync(userId);
            }
            else if (role == Role.UNITHEAD)
            {
                unitLocationIds = await _unitHeadAssignmentRepository
                    .GetUnitLocationIdsByUnitHeadIdAsync(userId);
            }
            else if (role == Role.ADMIN)
            {
                var orgUnits = await _organizationUnitRepository
                    .GetUnitLocationIdsByOrganizationIdAsync(_currentUserService.OrganizationId);
                unitLocationIds = orgUnits;
            }
            else
            {
                unitLocationIds = new List<int>();
            }

            // Materialize data first to avoid EF Core translation issues
            var allData = await query
                .Where(x => x.CreatedById == userId)
                .ToListAsync();

            // Filter by accessible locations in memory
            var filteredData = allData
                .Where(x => unitLocationIds.Contains(getUnitLocationId(x)))
                .ToList();

            // Get total count
            var totalCount = filteredData.Count;

            // Get paginated items
            var items = filteredData
                .OrderByDescending(x => x.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new TrainerHistoryItemDto
                {
                    Id = x.Id,
                    Title = getTitleOrName(x) ?? "Untitled",
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                    FormStatus = getFormStatus(x)
                })
                .ToList();

            return new PaginatedResult<TrainerHistoryItemDto>
            {
                Items = items,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        /// <summary>
        /// Get pending approvals for Unit Head with pagination
        /// Shows all entries in Pending status for unit locations assigned to Unit Head
        /// Optionally filter by creator ID
        /// </summary>
        public async Task<PaginatedResult<PendingApprovalItemDto>> GetPendingApprovalsAsync(
            IQueryable<TEntity> query,
            Func<TEntity, int> getUnitLocationId,
            Func<TEntity, string?> getTitleOrName,
            Func<TEntity, string> getFormStatus,
            Func<TEntity, int> getCreatedById,
            IUnitHeadAssignmentRepository unitHeadAssignmentRepository,
            int pageNumber = 1,
            int pageSize = 10,
            int? createdByIdFilter = null)
        {
            // Only Unit Heads and Admins can access this
            if (_currentUserService.Role != Role.UNITHEAD &&
                _currentUserService.Role != Role.ADMIN)
            {
                return new PaginatedResult<PendingApprovalItemDto>
                {
                    Items = new List<PendingApprovalItemDto>(),
                    TotalItems = 0,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };
            }

            // Get Unit Head's accessible locations
            List<int> unitLocationIds;
            if (_currentUserService.Role == Role.ADMIN)
            {
                var orgUnits = await _organizationUnitRepository.GetUnitLocationIdsByOrganizationIdAsync(_currentUserService.OrganizationId);
                unitLocationIds = orgUnits;
            }
            else
            {
                unitLocationIds = await unitHeadAssignmentRepository
                    .GetUnitLocationIdsByUnitHeadIdAsync(_currentUserService.UserId);
            }

            // Materialize data first
            var allPendingData = await query.ToListAsync();

            // Filter in memory
            var filteredData = allPendingData
                .Where(x => getFormStatus(x) == "Pending" &&
                           unitLocationIds.Contains(getUnitLocationId(x)))
                .ToList();

            // Apply optional createdById filter
            if (createdByIdFilter.HasValue)
            {
                filteredData = filteredData
                    .Where(x => getCreatedById(x) == createdByIdFilter.Value)
                    .ToList();
            }

            var totalCount = filteredData.Count;

            var items = filteredData
                .OrderByDescending(x => x.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new PendingApprovalItemDto
                {
                    Id = x.Id,
                    Title = getTitleOrName(x) ?? "Untitled",
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                    FormStatus = getFormStatus(x),
                    CreatedById = getCreatedById(x)
                })
                .ToList();

            return new PaginatedResult<PendingApprovalItemDto>
            {
                Items = items,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }
    }

    public class TrainerHistoryItemDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public string FormStatus { get; set; } = string.Empty;
    }

    public class PendingApprovalItemDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public string FormStatus { get; set; } = string.Empty;
        public int CreatedById { get; set; }
    }
}
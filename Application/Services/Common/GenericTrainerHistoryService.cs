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
    /// Provides common pagination and filtering for trainer submissions
    /// </summary>
    /// <typeparam name="TEntity">The entity type (e.g., Publication, NominationReward)</typeparam>
    public class GenericTrainerHistoryService<TEntity> where TEntity : AuditableBaseEntity
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly ITrainerAssignmentRepository _trainerAssignmentRepository;
        private readonly IOrganizationUnitRepository _organizationUnitRepository;
        private ICurrentUserService currentUserService;
        private ITrainerAssignmentRepository trainerAssignmentRepository;

        public GenericTrainerHistoryService(
            ICurrentUserService currentUserService,
            ITrainerAssignmentRepository trainerAssignmentRepository,
            IOrganizationUnitRepository organizationUnitRepository)
        {
            _currentUserService = currentUserService;
            _trainerAssignmentRepository = trainerAssignmentRepository;
            _organizationUnitRepository = organizationUnitRepository;
        }

       

        /// <summary>
        /// Get trainer's submission history with pagination
        /// Filters by CreatedById and accessible unit locations
        /// </summary>
        /// <param name="query">IQueryable of entities (already includes necessary data)</param>
        /// <param name="getUnitLocationId">Function to extract UnitLocationId from entity</param>
        /// <param name="getTitleOrName">Function to extract title/name from entity for display</param>
        /// <param name="getFormStatus">Function to extract FormStatus from entity</param>
        /// <param name="pageNumber">Page number (default: 1)</param>
        /// <param name="pageSize">Items per page (default: 20)</param>
        /// <returns>Paginated list of trainer's submissions with basic info</returns>
        public async Task<PaginatedResult<TrainerHistoryItemDto>> GetTrainerHistoryAsync(
            IQueryable<TEntity> query,
            Func<TEntity, int> getUnitLocationId,
            Func<TEntity, string?> getTitleOrName,
            Func<TEntity, string> getFormStatus,
            int pageNumber = 1,
            int pageSize = 10)
        {
            // Get trainer's accessible unit locations
            var userId = _currentUserService.UserId;
            var unitLocationIds = await _trainerAssignmentRepository
                .GetUnitLocationIdsByTrainerIdAsync(userId);

            // Filter by trainer and accessible locations
            var filteredQuery = query
                .Where(x => x.CreatedById == userId &&
                           unitLocationIds.Contains(getUnitLocationId(x)));

            // Get total count
            var totalCount = await filteredQuery.CountAsync();

            // Get paginated items
            var items = await filteredQuery
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
                .ToListAsync();

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
        /// </summary>
        /// <param name="query">IQueryable of entities</param>
        /// <param name="getUnitLocationId">Function to extract UnitLocationId from entity</param>
        /// <param name="getTitleOrName">Function to extract title/name from entity</param>
        /// <param name="getFormStatus">Function to extract FormStatus from entity</param>
        /// <param name="getCreatedById">Function to extract CreatedById from entity</param>
        /// <param name="pageNumber">Page number (default: 1)</param>
        /// <param name="pageSize">Items per page (default: 20)</param>
        /// <returns>Paginated list of pending approvals</returns>
        public async Task<PaginatedResult<PendingApprovalItemDto>> GetPendingApprovalsAsync(
            IQueryable<TEntity> query,
            Func<TEntity, int> getUnitLocationId,
            Func<TEntity, string?> getTitleOrName,
            Func<TEntity, string> getFormStatus,
            Func<TEntity, int> getCreatedById,
            IUnitHeadAssignmentRepository unitHeadAssignmentRepository,
            int pageNumber = 1,
            int pageSize = 10)
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
                // Admin sees all locations in their organization
                var orgUnits = await _organizationUnitRepository.GetUnitLocationIdsByOrganizationIdAsync(_currentUserService.OrganizationId);
                unitLocationIds = orgUnits;
            }
            else
            {
                // Unit Head sees only their assigned locations
                unitLocationIds = await unitHeadAssignmentRepository
                    .GetUnitLocationIdsByUnitHeadIdAsync(_currentUserService.UserId);
            }

            // Filter by Pending status and accessible locations
            var filteredQuery = query
                .Where(x => getFormStatus(x) == "Pending"  &&
                           unitLocationIds.Contains(getUnitLocationId(x)));

            var totalCount = await filteredQuery.CountAsync();

            var items = await filteredQuery
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
                .ToListAsync();

            return new PaginatedResult<PendingApprovalItemDto>
            {
                Items = items,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }
    }

    /// <summary>
    /// DTO for trainer history items
    /// </summary>
    public class TrainerHistoryItemDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public string FormStatus { get; set; } = string.Empty;
    }

    /// <summary>
    /// DTO for pending approval items (includes creator info)
    /// </summary>
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
using Application.Interface;
using Application.Interface.Repository;
using Application.Interface.Services.Common;
using Domain.Entities;
using Domain.Entities.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class EntityPermissionService : IEntityPermissionService
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly ITrainerAssignmentRepository _trainerAssignmentRepository;

        public EntityPermissionService(ICurrentUserService currentUserService, ITrainerAssignmentRepository trainerAssignmentRepository)
        {
            _currentUserService = currentUserService;
            _trainerAssignmentRepository = trainerAssignmentRepository;
        }
        public async Task<bool> CanModify<T>(T entity) where T : ReportEntryBaseEntity
        {
            try
            {
                Role role = _currentUserService.Role;
                int orgId = _currentUserService.OrganizationId;
                if (orgId != entity.OrganizationId) return false;
                if (role == Role.ADMIN) return true;
                if (role == Role.UNITHEAD)
                {
                    var unitLocationIds = await _currentUserService.MappedUnitLocationIds();
                    return unitLocationIds.Contains(entity.UnitLocationId);
                }
                if( role == Role.TRAINER)
                {
                    var unitLocationIds = await _currentUserService.MappedUnitLocationIds();
                    return unitLocationIds.Contains(entity.UnitLocationId) && _currentUserService.UserId == entity.CreatedById;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> CanUserAccessTableAsync(int userId, int tableDefinitionId)
        {
            // Implementation for checking if user can access a specific table
            // This would check user's unit assignments against table availability
            return true; // Implement based on your business logic
        }

        public async Task<bool> CanUserAccessUnitLocationAsync(int userId, int unitLocationId)
        {
            var assignments = await _trainerAssignmentRepository.GetByTrainerIdAsync(userId);
            return assignments.Any(a => a.UnitLocationId == unitLocationId);
        }
    }
}

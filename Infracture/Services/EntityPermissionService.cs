using Application.Interface;
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

        public EntityPermissionService(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
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
    }
}

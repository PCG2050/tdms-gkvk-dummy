//Temporary Fix for the Services

using Application.Interface;
using Application.Interface.Services.Common;
using Domain.Entities;

namespace Infrastructure.Services
{
    /// <summary>
    /// Temporary permissive implementation for EntityPermissionService.
    /// All permission checks return TRUE until role & status logic is finalized.
    /// </summary>
    public class EntityPermissionService : IEntityPermissionService
    {
        private readonly ICurrentUserService _currentUserService;

        public EntityPermissionService(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        public Task<bool> CanModify<T>(T entity) where T : ReportEntryBaseEntity
        {
            return Task.FromResult(true);
        }

        public Task<bool> CanView<T>(T entity) where T : ReportEntryBaseEntity
        {
            return Task.FromResult(true);
        }

        public Task<bool> CanDelete<T>(T entity) where T : ReportEntryBaseEntity
        {
            return Task.FromResult(true);
        }

        public Task<bool> CanViewForm<T>(T entity) where T : AuditableBaseEntity
        {
            return Task.FromResult(true);
        }

        public Task<bool> CanModifyForm<T>(T entity) where T : AuditableBaseEntity
        {
            return Task.FromResult(true);
        }

        public Task<bool> CanDeleteForm<T>(T entity) where T : AuditableBaseEntity
        {
            return Task.FromResult(true);
        }
    }
}










//using Application.Interface;
//using Application.Interface.Services.Common;
//using Domain.Entities;
//using Domain.Entities.Enum;

//namespace Infrastructure.Services
//{
//    /// <summary>
//    /// Centralized permission management for form/report entities.
//    /// Includes role-based logic and workflow-specific checks (approval/rejection).
//    /// </summary>
//    public class EntityPermissionService : IEntityPermissionService
//    {
//        private readonly ICurrentUserService _currentUserService;

//        public EntityPermissionService(ICurrentUserService currentUserService)
//        {
//            _currentUserService = currentUserService;
//        }

//        // ================================================================
//        // REPORT-BASED ENTITIES (ReportEntryBaseEntity)
//        // ================================================================

//        public async Task<bool> CanModify<T>(T entity) where T : ReportEntryBaseEntity
//        {
//            try
//            {
//                var role = _currentUserService.Role;
//                var orgId = _currentUserService.OrganizationId;

//                if (orgId != entity.OrganizationId)
//                    return false;

//                if (role == Role.ADMIN)
//                    return true;

//                if (role == Role.UNITHEAD)
//                {
//                    var unitIds = await _currentUserService.MappedUnitLocationIds();

//                    // UnitHead can modify all under their mapped units
//                    // including changing status from Pending → Approved/Rejected
//                    return unitIds.Contains(entity.UnitLocationId);
//                }

//                if (role == Role.TRAINER)
//                {
//                    var unitIds = await _currentUserService.MappedUnitLocationIds();
//                    return unitIds.Contains(entity.UnitLocationId)
//                        && _currentUserService.UserId == entity.CreatedById
//                        && entity.FormStatus?.Equals("Draft", StringComparison.OrdinalIgnoreCase) == true;
//                }

//                return false;
//            }
//            catch
//            {
//                return false;
//            }
//        }

//        public async Task<bool> CanView<T>(T entity) where T : ReportEntryBaseEntity
//        {
//            try
//            {
//                var role = _currentUserService.Role;
//                if (_currentUserService.OrganizationId != entity.OrganizationId)
//                    return false;

//                if (role == Role.ADMIN)
//                    return true;

//                if (role == Role.UNITHEAD)
//                {
//                    var unitIds = await _currentUserService.MappedUnitLocationIds();
//                    return unitIds.Contains(entity.UnitLocationId);
//                }

//                if (role == Role.TRAINER)
//                {
//                    var unitIds = await _currentUserService.MappedUnitLocationIds();
//                    return unitIds.Contains(entity.UnitLocationId)
//                        && _currentUserService.UserId == entity.CreatedById
//                        && entity.FormStatus?.Equals("Draft", StringComparison.OrdinalIgnoreCase) == true;
//                }

//                return false;
//            }
//            catch
//            {
//                return false;
//            }
//        }

//        public async Task<bool> CanDelete<T>(T entity) where T : ReportEntryBaseEntity
//        {
//            try
//            {
//                var role = _currentUserService.Role;
//                if (_currentUserService.OrganizationId != entity.OrganizationId)
//                    return false;

//                if (role == Role.ADMIN)
//                    return true;

//                if (role == Role.UNITHEAD)
//                {
//                    var unitIds = await _currentUserService.MappedUnitLocationIds();
//                    return unitIds.Contains(entity.UnitLocationId)
//                        && entity.FormStatus?.Equals("Draft", StringComparison.OrdinalIgnoreCase) == true;
//                }

//                if (role == Role.TRAINER)
//                {
//                    var unitIds = await _currentUserService.MappedUnitLocationIds();
//                    return unitIds.Contains(entity.UnitLocationId)
//                        && _currentUserService.UserId == entity.CreatedById
//                        && entity.FormStatus?.Equals("Draft", StringComparison.OrdinalIgnoreCase) == true;
//                }

//                return false;
//            }
//            catch
//            {
//                return false;
//            }
//        }

//        // ================================================================
//        // FORM-BASED ENTITIES (AuditableBaseEntity)
//        // ================================================================

//        public async Task<bool> CanViewForm<T>(T entity) where T : AuditableBaseEntity
//        {
//            try
//            {
//                var role = _currentUserService.Role;
//                if (_currentUserService.OrganizationId != entity.OrganizationId)
//                    return false;

//                if (role == Role.ADMIN)
//                    return true;

//                if (role == Role.UNITHEAD)
//                {
//                    var unitIds = await _currentUserService.MappedUnitLocationIds();
//                    return unitIds.Contains(entity.UnitLocationId);
//                }

//                if (role == Role.TRAINER)
//                {
//                    var unitIds = await _currentUserService.MappedUnitLocationIds();
//                    return unitIds.Contains(entity.UnitLocationId)
//                        && _currentUserService.UserId == entity.CreatedById
//                        && entity.FormStatus?.Equals("Draft", StringComparison.OrdinalIgnoreCase) == true;
//                }

//                return false;
//            }
//            catch
//            {
//                return false;
//            }
//        }

//        public async Task<bool> CanModifyForm<T>(T entity) where T : AuditableBaseEntity
//        {
//            try
//            {
//                var role = _currentUserService.Role;
//                if (_currentUserService.OrganizationId != entity.OrganizationId)
//                    return false;

//                if (role == Role.ADMIN)
//                    return true;

//                if (role == Role.UNITHEAD)
//                {
//                    var unitIds = await _currentUserService.MappedUnitLocationIds();

//                    // ✅ UnitHead can modify any mapped-unit form
//                    // including updating status Pending → Approved / Rejected
//                    return unitIds.Contains(entity.UnitLocationId);
//                }

//                if (role == Role.TRAINER)
//                {
//                    var unitIds = await _currentUserService.MappedUnitLocationIds();

//                    // Trainer: can modify only their own Draft
//                    return unitIds.Contains(entity.UnitLocationId)
//                        && _currentUserService.UserId == entity.CreatedById
//                        && entity.FormStatus?.Equals("Draft", StringComparison.OrdinalIgnoreCase) == true;
//                }

//                return false;
//            }
//            catch
//            {
//                return false;
//            }
//        }

//        public async Task<bool> CanDeleteForm<T>(T entity) where T : AuditableBaseEntity
//        {
//            try
//            {
//                var role = _currentUserService.Role;
//                if (_currentUserService.OrganizationId != entity.OrganizationId)
//                    return false;

//                if (role == Role.ADMIN)
//                    return true;

//                if (role == Role.UNITHEAD)
//                {
//                    var unitIds = await _currentUserService.MappedUnitLocationIds();

//                    // UnitHead can delete only if still Draft
//                    return unitIds.Contains(entity.UnitLocationId)
//                        && entity.FormStatus?.Equals("Draft", StringComparison.OrdinalIgnoreCase) == true;
//                }

//                if (role == Role.TRAINER)
//                {
//                    var unitIds = await _currentUserService.MappedUnitLocationIds();

//                    // Trainer can delete only their own Draft
//                    return unitIds.Contains(entity.UnitLocationId)
//                        && _currentUserService.UserId == entity.CreatedById
//                        && entity.FormStatus?.Equals("Draft", StringComparison.OrdinalIgnoreCase) == true;
//                }

//                return false;
//            }
//            catch
//            {
//                return false;
//            }
//        }
//    }
//}

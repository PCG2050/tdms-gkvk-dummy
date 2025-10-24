


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


/* Produciton EntityPermissionService changes */


//public class EntityPermissionService : IEntityPermissionService
//{
//    private readonly ICurrentUserService _currentUserService;
//    private readonly ITrainerAssignmentRepository _trainerAssignmentRepo;
//    private readonly IUnitHeadAssignmentRepository _unitHeadAssignmentRepo;

//    public EntityPermissionService(
//        ICurrentUserService currentUserService,
//        ITrainerAssignmentRepository trainerAssignmentRepo,
//        IUnitHeadAssignmentRepository unitHeadAssignmentRepo)
//    {
//        _currentUserService = currentUserService;
//        _trainerAssignmentRepo = trainerAssignmentRepo;
//        _unitHeadAssignmentRepo = unitHeadAssignmentRepo;
//    }

//    // ===== For ReportEntryBaseEntity (IBTVA, FTI, etc.) =====

//    public async Task<bool> CanModify<T>(T entity) where T : ReportEntryBaseEntity
//    {
//        var currentUserId = _currentUserService.UserId;
//        var currentUserRole = _currentUserService.Role;

//        // Admin can modify everything
//        if (currentUserRole == Role.ADMIN)
//            return true;

//        // UnitHead can modify in their units
//        if (currentUserRole == Role.UNITHEAD)
//        {
//            var unitHeadLocations = await _unitHeadAssignmentRepo
//                .GetUnitLocationIdsByUnitHeadIdAsync(currentUserId);
//            return unitHeadLocations.Contains(entity.UnitLocationId);
//        }

//        // Trainer can modify their own entries (if Draft or Rejected)
//        if (currentUserRole == Role.TRAINER)
//        {
//            bool isCreator = entity.CreatedById == currentUserId;
//            bool canEdit = entity.FormStatus == "Draft" || entity.FormStatus == "Rejected";
//            return isCreator && canEdit;
//        }

//        return false;
//    }

//    public async Task<bool> CanView<T>(T entity) where T : ReportEntryBaseEntity
//    {
//        var currentUserId = _currentUserService.UserId;
//        var currentUserRole = _currentUserService.Role;

//        // Admin can view everything
//        if (currentUserRole == Role.ADMIN)
//            return true;

//        // UnitHead can view in their units
//        if (currentUserRole == Role.UNITHEAD)
//        {
//            var unitHeadLocations = await _unitHeadAssignmentRepo
//                .GetUnitLocationIdsByUnitHeadIdAsync(currentUserId);
//            return unitHeadLocations.Contains(entity.UnitLocationId);
//        }

//        // Trainer can view their own entries
//        if (currentUserRole == Role.TRAINER)
//        {
//            return entity.CreatedById == currentUserId;
//        }

//        return false;
//    }

//    public async Task<bool> CanDelete<T>(T entity) where T : ReportEntryBaseEntity
//    {
//        var currentUserRole = _currentUserService.Role;

//        // Only Admin can delete
//        if (currentUserRole == Role.ADMIN)
//            return true;

//        // Trainers can delete their own Draft entries
//        if (currentUserRole == Role.TRAINER)
//        {
//            bool isCreator = entity.CreatedById == _currentUserService.UserId;
//            bool isDraft = entity.FormStatus == "Draft";
//            return isCreator && isDraft;
//        }

//        return false;
//    }

//    // ===== For AuditableBaseEntity (Publications, Nominations) =====

//    public Task<bool> CanViewForm<T>(T entity) where T : AuditableBaseEntity
//    {
//        // Similar logic but simpler
//        var currentUserRole = _currentUserService.Role;

//        if (currentUserRole == Role.ADMIN || currentUserRole == Role.UNITHEAD)
//            return Task.FromResult(true);

//        // Trainer can view their own
//        return Task.FromResult(entity.CreatedById == _currentUserService.UserId);
//    }

//    public Task<bool> CanModifyForm<T>(T entity) where T : AuditableBaseEntity
//    {
//        var currentUserRole = _currentUserService.Role;

//        if (currentUserRole == Role.ADMIN)
//            return Task.FromResult(true);

//        // Check if it has FormStatus property
//        var formStatusProp = typeof(T).GetProperty("FormStatus");
//        if (formStatusProp != null)
//        {
//            var status = formStatusProp.GetValue(entity) as string;
//            bool canEdit = status == "Draft" || status == "Rejected";
//            bool isCreator = entity.CreatedById == _currentUserService.UserId;
//            return Task.FromResult(isCreator && canEdit);
//        }

//        // If no FormStatus, creator can modify
//        return Task.FromResult(entity.CreatedById == _currentUserService.UserId);
//    }

//    public Task<bool> CanDeleteForm<T>(T entity) where T : AuditableBaseEntity
//    {
//        var currentUserRole = _currentUserService.Role;

//        if (currentUserRole == Role.ADMIN)
//            return Task.FromResult(true);

//        // Trainers can delete their own Draft entries
//        var formStatusProp = typeof(T).GetProperty("FormStatus");
//        if (formStatusProp != null)
//        {
//            var status = formStatusProp.GetValue(entity) as string;
//            bool isDraft = status == "Draft";
//            bool isCreator = entity.CreatedById == _currentUserService.UserId;
//            return Task.FromResult(isCreator && isDraft);
//        }

//        return Task.FromResult(false);
//    }
//}




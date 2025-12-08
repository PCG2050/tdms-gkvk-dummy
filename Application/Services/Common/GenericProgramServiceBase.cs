using Application.Interface.Repository;
using Application.Interface.Services;
using Application.Models;
using Domain.Entities;

namespace Application.Services.Common
{
    /// <summary>
    /// Generic base service for program management across all units (FTI, STU, ATIC, etc.)
    /// Contains common operations to reduce code duplication
    /// </summary>
    public abstract class GenericProgramServiceBase<TProgram> where TProgram : ReportEntryBaseEntity
    {
        protected readonly ICurrentUserService CurrentUserService;
        protected readonly IEntityPermissionService EntityPermissionService;

        protected GenericProgramServiceBase(
            ICurrentUserService currentUserService,
            IEntityPermissionService entityPermissionService)
        {
            CurrentUserService = currentUserService;
            EntityPermissionService = entityPermissionService;
        }

        // ============================
        // STATUS MANAGEMENT (Common across all units)
        // ============================

        /// <summary>
        /// Submit program for approval (Draft/Rejected → Pending)
        /// </summary>
        protected async Task<ServiceResult> SubmitForApprovalAsync(
            TProgram program,
            IGenericProgramRepository<TProgram> repository)
        {
            if (program == null)
                return ServiceResult.Failure("Program not found", ServiceErrorStatus.NOTFOUND);

            if (!await EntityPermissionService.CanModifyForm(program))
                return ServiceResult.Failure("Access denied", ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft" && program.FormStatus != "Rejected" && program.FormStatus != "Pending")
                return ServiceResult.Failure(
                    "Only draft or rejected programs can be submitted",
                    ServiceErrorStatus.INVALIDOPERATION);

            program.FormStatus = "Pending";
            program.UpdatedById = CurrentUserService.UserId;
            program.UpdatedAt = DateTimeOffset.UtcNow;

            await repository.UpdateAsync(program);
            return ServiceResult.Success();
        }

        /// <summary>
        /// Approve program (Pending → Approved)
        /// Only Unit Heads and Admins can approve
        /// </summary>
        protected async Task<ServiceResult> ApproveAsync(
            TProgram program,
            IGenericProgramRepository<TProgram> repository,
            string? remarks = null)
        {
            if (program == null)
                return ServiceResult.Failure("Program not found", ServiceErrorStatus.NOTFOUND);

            // Only UnitHead or Admin can approve
            if (CurrentUserService.Role != Role.UNITHEAD && CurrentUserService.Role != Role.ADMIN)
                return ServiceResult.Failure(
                    "Only Unit Heads and Admins can approve programs",
                    ServiceErrorStatus.FORBIDDEN);

            if (!await EntityPermissionService.CanModifyForm(program))
                return ServiceResult.Failure("Access denied", ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Pending")
                return ServiceResult.Failure(
                    "Only pending programs can be approved",
                    ServiceErrorStatus.INVALIDOPERATION);

            program.FormStatus = "Approved";
            program.FormStatusRemarks = remarks;
            program.ApprovedById = CurrentUserService.UserId;
            program.ApprovedAt = DateTimeOffset.UtcNow;
            program.UpdatedById = CurrentUserService.UserId;
            program.UpdatedAt = DateTimeOffset.UtcNow;

            await repository.UpdateAsync(program);
            return ServiceResult.Success();
        }

        /// <summary>
        /// Reject program (Pending → Rejected)
        /// Only Unit Heads and Admins can reject
        /// </summary>
        protected async Task<ServiceResult> RejectAsync(
            TProgram program,
            IGenericProgramRepository<TProgram> repository,
            string remarks)
        {
            if (program == null)
                return ServiceResult.Failure("Program not found", ServiceErrorStatus.NOTFOUND);

            // Only UnitHead or Admin can reject
            if (CurrentUserService.Role != Role.UNITHEAD && CurrentUserService.Role != Role.ADMIN)
                return ServiceResult.Failure(
                    "Only Unit Heads and Admins can reject programs",
                    ServiceErrorStatus.FORBIDDEN);

            if (!await EntityPermissionService.CanModifyForm(program))
                return ServiceResult.Failure("Access denied", ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Pending")
                return ServiceResult.Failure(
                    "Only pending programs can be rejected",
                    ServiceErrorStatus.INVALIDOPERATION);

            if (string.IsNullOrWhiteSpace(remarks))
                return ServiceResult.Failure(
                    "Remarks are required for rejection",
                    ServiceErrorStatus.BADREQUEST);

            program.FormStatus = "Rejected";
            program.FormStatusRemarks = remarks;
            program.UpdatedById = CurrentUserService.UserId;
            program.UpdatedAt = DateTimeOffset.UtcNow;

            await repository.UpdateAsync(program);
            return ServiceResult.Success();
        }

        // ============================
        // PERMISSION HELPERS (Common validation)
        // ============================

        /// <summary>
        /// Check if user can view a form
        /// </summary>
        protected async Task<bool> CanViewFormAsync(TProgram program)
        {
            return await EntityPermissionService.CanViewForm(program);
        }

        /// <summary>
        /// Check if user can modify a form
        /// </summary>
        protected async Task<bool> CanModifyFormAsync(TProgram program)
        {
            return await EntityPermissionService.CanModifyForm(program);
        }

        /// <summary>
        /// Validate form status allows modification
        /// </summary>
        protected bool CanModifyFormStatus(string? formStatus)
        {
            return formStatus == "Draft" || formStatus == "Rejected" || formStatus == "Pending";
        }

        // ============================
        // AUDIT HELPERS (Common entity setup)
        // ============================

        /// <summary>
        /// Set audit fields for new entity
        /// </summary>
        protected void SetCreateAuditFields(TProgram entity)
        {
            entity.OrganizationId = CurrentUserService.OrganizationId;
            entity.CreatedById = CurrentUserService.UserId;
            entity.CreatedAt = DateTimeOffset.UtcNow;
        }

        /// <summary>
        /// Set audit fields for updated entity
        /// </summary>
        protected void SetUpdateAuditFields(TProgram entity)
        {
            entity.UpdatedById = CurrentUserService.UserId;
            entity.UpdatedAt = DateTimeOffset.UtcNow;
        }

        // ============================
        // STATUS SUMMARY (Common query)
        // ============================

        /// <summary>
        /// Get status summary from repository
        /// </summary>
        protected async Task<Dictionary<string, int>> GetStatusSummaryAsync(
            IGenericProgramRepository<TProgram> repository,
            List<int> unitLocationIds)
        {
            return await repository.GetStatusSummaryAsync(unitLocationIds);
        }
    }
}

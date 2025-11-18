using Application.Models;
using Application.Models.DataTables;
using Application.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Services.DataTables
{
    public interface INominationRewardService
    {
        Task<ServiceResult<NominationRewardDto>> CreateAsync(NominationRewardDto createDto);

        Task<ServiceResult<NominationRewardDto>> UpdateAsync(int id, NominationRewardDto updateDto);

        Task<ServiceResult<NominationRewardDto>> GetByIdAsync(int id);

        Task<ServiceResult<CompleteNominationRewardDto>> GetCompleteByIdAsync(int id);

        Task<ServiceResult> DeleteAsync(int id);

        Task<ServiceResult> SubmitForApprovalAsync(int nominationRewardId);

        Task<ServiceResult> ApproveAsync(int nominationRewardId, string? remarks = null);

        Task<ServiceResult> RejectAsync(int nominationRewardId, string remarks);
    

        Task<PaginatedResult<NominationRewardDto>> GetPaginatedAsync(
            int pageNumber = 1,
            int pageSize = 10,
            DateOnly? startDate = null,
            DateOnly? endDate = null,
            int? unitLocationId = null,
            string? searchTerm = null
        );

        Task<PaginatedResult<NominationRewardDto>> GetByStatusAsync(
            string status,
            int pageNumber = 1,
            int pageSize = 10);

       

        Task<Dictionary<string, int>> GetStatusSummaryAsync();

        /// <summary>
        /// Get trainer's submission history with pagination
        /// </summary>
        Task<PaginatedResult<TrainerHistoryItemDto>> GetTrainerHistoryAsync(
            int pageNumber = 1,
            int pageSize = 10);

        /// <summary>
        /// Get pending approvals for Unit Head and Admin with pagination
        /// </summary>
        Task<PaginatedResult<PendingApprovalItemDto>> GetPendingApprovalsAsync(
            int pageNumber = 1,
            int pageSize = 10);

        /// <summary>
        /// Get nomination rewards by trainer ID (Unit Head and Admin only)
        /// </summary>
        Task<PaginatedResult<NominationRewardDto>> GetByTrainerAsync(
            int trainerId,
            int? unitLocationId = null,
            int pageNumber = 1,
            int pageSize = 10);

        /// <summary>
        /// Get unified history - own forms or trainer forms (for unit heads)
        /// </summary>
        Task<PaginatedResult<NominationRewardDto>> GetUnifiedHistoryAsync(
            int? trainerId = null,
            int? unitLocationId = null,
            int pageNumber = 1,
            int pageSize = 10);
    }
}

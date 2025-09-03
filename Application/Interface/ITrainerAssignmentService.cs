using Application.Models;

namespace Application.Interface
{
    public interface ITrainerAssignmentService
    {
        Task<ServiceResult<List<TrainerUnitWithLocationsDto>>> GetTrainerUnits(int trainerId);

        //new
        Task<ServiceResult> SyncTrainerAssignmentsByLocationAsync(BulkTrainerAssignmentByLocationDto request);
        Task<List<TrainerWithAssignmentsDto>> GetTrainersWithAssignmentsCreatedByCurrentUserAsync();
    }
}
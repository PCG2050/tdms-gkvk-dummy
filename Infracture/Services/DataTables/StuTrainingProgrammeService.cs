using Application.Interface;
using Application.Interface.Repository.DataTables;
using Application.Interface.Services.Common;
using Application.Interface.Services.DataTables;
using Application.Models;
using Application.Models.DataTables;
using Domain.Entities.STU;

namespace Infrastructure.Services.DataTables
{
    public class StuTrainingProgrammeService: IStuTrainingProgrammeService
    {
        private readonly IStuTrainingProgrammeRepository _stuTrainingProgrammeRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IEntityPermissionService _entityPermissionService;

        public StuTrainingProgrammeService(IStuTrainingProgrammeRepository stuTrainingProgrammeRepository, ICurrentUserService currentUserService, IEntityPermissionService entityPermissionService)
        {
            _stuTrainingProgrammeRepository = stuTrainingProgrammeRepository;
            _currentUserService = currentUserService;
            _entityPermissionService = entityPermissionService;
        }

        public async Task<ServiceResult<StuTrainingProgramme>> AddAsync(StuTrainingProgrammeCreateDto createDto)
        {
            int userId = _currentUserService.UserId;
            int orgId = _currentUserService.OrganizationId;
            //validation
            var entity = new StuTrainingProgramme
            {
                TrainingTitle = createDto.TrainingTitle,
                ParticipantCount = createDto.ParticipantCount,
                TrainingCount = createDto.TrainingCount,
                Duration = createDto.Duration,
                UnitLocationId = createDto.UnitLocationId,
                Attachements = createDto.Attachements,
                EndDate = createDto.EndDate,
                StartDate = createDto.StartDate,
                CreatedById = userId,
                OrganizationId = orgId
            };
            await _stuTrainingProgrammeRepository.AddAsync(entity);
            return ServiceResult<StuTrainingProgramme>.Success(entity);
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var activity = await _stuTrainingProgrammeRepository.GetAsync(id);
            if (activity is null) return ServiceResult.Failure("Activity not found", ServiceErrorStatus.NOTFOUND);
            if (!await _entityPermissionService.CanModify(activity)) return ServiceResult.Failure("User does not have permission to delete this item", ServiceErrorStatus.FORBIDDEN);
            await _stuTrainingProgrammeRepository.DeleteAsync(activity);
            return ServiceResult.Success();
        }

        public Task<PaginatedResult<StuTrainingProgrammeDto>> GetPaginatedItemsAsync(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<StuTrainingProgramme>> UpdateAsync(StuTrainingProgrammeUpdateDto updateDto)
        {
            var activity = await _stuTrainingProgrammeRepository.GetAsync(updateDto.Id);
            if (activity is null) return ServiceResult<StuTrainingProgramme>.Failure("Activity not found", ServiceErrorStatus.NOTFOUND);
            if (!await _entityPermissionService.CanModify(activity)) return ServiceResult<StuTrainingProgramme>.Failure("User does not have permission to delete this item", ServiceErrorStatus.FORBIDDEN);
            // modification
            if (updateDto.EndDate.HasValue) activity.EndDate = updateDto.EndDate.Value;
            if (updateDto.StartDate.HasValue) activity.StartDate = updateDto.StartDate.Value;
            if (updateDto.Duration.HasValue) activity.Duration = updateDto.Duration.Value;
            if(updateDto.ParticipantCount.HasValue) activity.ParticipantCount = updateDto.ParticipantCount.Value;
            if (updateDto.TrainingTitle is not null) activity.TrainingTitle = updateDto.TrainingTitle;
            if (updateDto.TrainingCount.HasValue) activity.TrainingCount = updateDto.TrainingCount.Value;
            if (updateDto.Attachements is not null) activity.Attachements = updateDto.Attachements;
            await _stuTrainingProgrammeRepository.UpdateAsync(activity);
            return ServiceResult<StuTrainingProgramme>.Success(activity);
        }

    }
}

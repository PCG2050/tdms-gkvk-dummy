using Application.Interface;
using Application.Interface.Repository.DataTables;
using Application.Interface.Services.Common;
using Application.Interface.Services.DataTables;
using Application.Models;
using Application.Models.DataTables;
using Domain.Entities.STU;

namespace Infrastructure.Services.DataTables
{
    public class StuSponsoredTrainingProgrammeService: IStuSponsoredTrainingProgrammeService
    {
        private readonly IStuSposoredTrainingProgrammeRepository _sposoredTrainingProgrammeRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IEntityPermissionService _entityPermissionService;

        public StuSponsoredTrainingProgrammeService(IStuSposoredTrainingProgrammeRepository sposoredTrainingProgrammeRepository, ICurrentUserService currentUserService, IEntityPermissionService entityPermissionService)
        {
            _sposoredTrainingProgrammeRepository = sposoredTrainingProgrammeRepository;
            _currentUserService = currentUserService;
            _entityPermissionService = entityPermissionService;
        }

        public async Task<ServiceResult<StuSponsoredTrainingProgramme>> AddAsync(StuSponsoredTrainingProgrammeCreateDto createDto)
        {
            int userId = _currentUserService.UserId;
            int orgId = _currentUserService.OrganizationId;
            //validation
            var entity = new StuSponsoredTrainingProgramme
            {
                SponsorOrganization = createDto.SponsoredOrganization,
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
            await _sposoredTrainingProgrammeRepository.AddAsync(entity);
            return ServiceResult<StuSponsoredTrainingProgramme>.Success(entity);
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var activity = await _sposoredTrainingProgrammeRepository.GetAsync(id);
            if (activity is null) return ServiceResult.Failure("Activity not found", ServiceErrorStatus.NOTFOUND);
            if (!await _entityPermissionService.CanModify(activity)) return ServiceResult.Failure("User does not have permission to delete this item", ServiceErrorStatus.FORBIDDEN);
            await _sposoredTrainingProgrammeRepository.DeleteAsync(activity);
            return ServiceResult.Success();
        }

        public Task<PaginatedResult<StuSponsoredTrainingProgrammeDto>> GetPaginatedItemsAsync(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<StuSponsoredTrainingProgramme>> UpdateAsync(StuSponsoredTrainingProgrammeUpdateDto updateDto)
        {
            var activity = await _sposoredTrainingProgrammeRepository.GetAsync(updateDto.Id);
            if (activity is null) return ServiceResult<StuSponsoredTrainingProgramme>.Failure("Activity not found", ServiceErrorStatus.NOTFOUND);
            if (!await _entityPermissionService.CanModify(activity)) return ServiceResult<StuSponsoredTrainingProgramme>.Failure("User does not have permission to delete this item", ServiceErrorStatus.FORBIDDEN);
            // modification
            if (updateDto.SponsorOrganization is not null) activity.SponsorOrganization = updateDto.SponsorOrganization;
            if (updateDto.EndDate.HasValue) activity.EndDate = updateDto.EndDate.Value;
            if (updateDto.StartDate.HasValue) activity.StartDate = updateDto.StartDate.Value;
            if (updateDto.Duration.HasValue) activity.Duration = updateDto.Duration.Value;
            if(updateDto.ParticipantCount.HasValue) activity.ParticipantCount = updateDto.ParticipantCount.Value;
            if (updateDto.TrainingTitle is not null) activity.TrainingTitle = updateDto.TrainingTitle;
            if (updateDto.TrainingCount.HasValue) activity.TrainingCount = updateDto.TrainingCount.Value;
            if (updateDto.Attachements is not null) activity.Attachements = updateDto.Attachements;
            await _sposoredTrainingProgrammeRepository.UpdateAsync(activity);
            return ServiceResult<StuSponsoredTrainingProgramme>.Success(activity);
        }

    }
}

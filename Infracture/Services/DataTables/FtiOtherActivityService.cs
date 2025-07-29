using Application.Interface;
using Application.Interface.Repository.DataTables;
using Application.Interface.Services.Common;
using Application.Interface.Services.DataTables;
using Application.Models;
using Application.Models.DataTables;
using Domain.Entities.FTI;

namespace Infrastructure.Services.DataTables
{
    public class FtiOtherActivityService : IFtiOtherActivityService
    {
        private readonly IFtiOtherActivitiesRepository _ftiOtherActivitiesRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IEntityPermissionService _entityPermissionService;

        public FtiOtherActivityService(IFtiOtherActivitiesRepository ftiOtherActivitiesRepository, ICurrentUserService currentUserService, IEntityPermissionService entityPermissionService)
        {
            _ftiOtherActivitiesRepository = ftiOtherActivitiesRepository;
            _currentUserService = currentUserService;
            _entityPermissionService = entityPermissionService;
        }

        public async Task<ServiceResult<FtiOtherActivity>> AddAsync(FtiOtherActivityCreateDto createDto)
        {
            int userId = _currentUserService.UserId;
            int orgId = _currentUserService.OrganizationId;
            //validation
            var entity = new FtiOtherActivity
            {
                ActivityDetails = createDto.ActivityDetails,
                Attachements = createDto.Assets,
                CreatedById = userId,
                OrganizationId = orgId,
                UnitLocationId = createDto.UnitLocationId
            };
            await _ftiOtherActivitiesRepository.AddAsync(entity);
            return ServiceResult<FtiOtherActivity>.Success(entity);
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var activity = await _ftiOtherActivitiesRepository.GetAsync(id);
            if (activity is null) return ServiceResult.Failure("Activity not found", ServiceErrorStatus.NOTFOUND);
            if (!await _entityPermissionService.CanModify(activity)) return ServiceResult.Failure("User does not have permission to delete this item", ServiceErrorStatus.FORBIDDEN);
            await _ftiOtherActivitiesRepository.DeleteAsync(activity);
            return ServiceResult.Success();
        }

        public Task<PaginatedResult<FtiOtherActivityDto>> GetPaginatedItemsAsync(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<FtiOtherActivity>> UpdateAsync(FtiOtherActivityUpdateDto updateDto)
        {
            var activity = await _ftiOtherActivitiesRepository.GetAsync(updateDto.Id);
            if (activity is null) return ServiceResult<FtiOtherActivity>.Failure("Activity not found", ServiceErrorStatus.NOTFOUND);
            if (!await _entityPermissionService.CanModify(activity)) return ServiceResult<FtiOtherActivity>.Failure("User does not have permission to delete this item", ServiceErrorStatus.FORBIDDEN);
            // modification
            if(updateDto.EndDate.HasValue)activity.EndDate = updateDto.EndDate.Value;
            if (updateDto.StartDate.HasValue) activity.StartDate = updateDto.StartDate.Value;
            if (updateDto.ActivityDetails is not null) activity.ActivityDetails = updateDto.ActivityDetails;
            if (updateDto.Attachements is not null) activity.Attachements = updateDto.Attachements;
            await _ftiOtherActivitiesRepository.UpdateAsync(activity);
            return ServiceResult<FtiOtherActivity>.Success(activity);
        }
    }
}

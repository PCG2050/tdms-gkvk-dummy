using Application.Interface;
using Application.Interface.Repository.DataTables;
using Application.Interface.Services.Common;
using Application.Interface.Services.DataTables;
using Application.Models.DataTables;
using Domain.Entities.FTI;

namespace Infrastructure.Services.DataTables
{
    public class FtiOtherActivityService : GenericTableService<FtiOtherActivity, FtiOtherActivityCreateDto, FtiOtherActivityUpdateDto, FtiOtherActivityDto>,IFtiOtherActivityService
    {
        public FtiOtherActivityService(IFtiOtherActivitiesRepository ftiOtherActivitiesRepository, ICurrentUserService currentUserService, IEntityPermissionService entityPermissionService)
            :base(ftiOtherActivitiesRepository,currentUserService,entityPermissionService)
        {
        }

        protected override Task<FtiOtherActivity> MapCreateDtoToEntityAsync(FtiOtherActivityCreateDto createDto)
        {
            var r = new FtiOtherActivity
            {
                ActivityDetails = createDto.ActivityDetails,
                Attachements = createDto.Assets,
                UnitLocationId = createDto.UnitLocationId
            };
            return Task.FromResult(r);
        }

        protected override Task MapUpdateDtoToEntityAsync(FtiOtherActivityUpdateDto updateDto, FtiOtherActivity entity)
        {
            if (updateDto.EndDate.HasValue) entity.EndDate = updateDto.EndDate.Value;
            if (updateDto.StartDate.HasValue) entity.StartDate = updateDto.StartDate.Value;
            if (updateDto.ActivityDetails is not null) entity.ActivityDetails = updateDto.ActivityDetails;
            if (updateDto.Attachements is not null) entity.Attachements = updateDto.Attachements;
            return Task.CompletedTask;
        }
    }
}

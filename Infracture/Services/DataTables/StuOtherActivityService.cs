using Application.Interface;
using Application.Interface.Repository.DataTables;
using Application.Interface.Services.Common;
using Application.Interface.Services.DataTables;
using Application.Models;
using Application.Models.DataTables;
using Domain.Entities.STU;

namespace Infrastructure.Services.DataTables
{
    public class StuOtherActivityService : GenericTableService<StuOtherActivity, OtherActivityCreateDto, OtherActivityUpdateDto, OtherActivityDto>,IStuOtherActivityService
    {
        public StuOtherActivityService(IStuOtherActivitiesRepository repository, ICurrentUserService currentUserService, IEntityPermissionService entityPermissionService) 
            : base(repository, currentUserService, entityPermissionService)
        {
        }

        protected override Task<StuOtherActivity> MapCreateDtoToEntityAsync(OtherActivityCreateDto createDto)
        {
            var entity = new StuOtherActivity
            {
                ActivityDetails = createDto.ActivityDetails,
                Attachements = createDto.Attachements,
                UnitLocationId = createDto.UnitLocationId
            };
            return Task.FromResult(entity);
        }

        protected override Task MapUpdateDtoToEntityAsync(OtherActivityUpdateDto updateDto, StuOtherActivity entity)
        {
            throw new NotImplementedException();
        }
    }
}

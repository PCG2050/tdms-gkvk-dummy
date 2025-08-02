using Application.Interface;
using Application.Interface.Repository.DataTables;
using Application.Interface.Services.Common;
using Application.Interface.Services.DataTables;
using Application.Models.DataTables;
using Domain.Entities.STU;

namespace Infrastructure.Services.DataTables
{
    public class DaesiProgrammeService : GenericTableService<DaesiProgramme, DaesiProgrammeCreateDto, DaesiProgrammeUpdateDto, DaesiProgrammeDto>, IDaesiProgrammeService
    {
        public DaesiProgrammeService(IDaesiProgrammeRepository repository, ICurrentUserService currentUserService, IEntityPermissionService entityPermissionService) : base(repository, currentUserService, entityPermissionService)
        {
        }

        protected override Task<DaesiProgramme> MapCreateDtoToEntityAsync(DaesiProgrammeCreateDto createDto)
        {
            var entity = new DaesiProgramme
            {
                NodalTrainingInstitute = createDto.NodalTrainingInstitute,
                Place = createDto.Place,
                UnitLocationId = createDto.UnitLocationId,
                BatchCount = createDto.BatchCount,
                DealerCount = createDto.DealerCount,
                Attachements = createDto.Attachements,
            };
            return Task.FromResult(entity);
        }

        protected override Task MapUpdateDtoToEntityAsync(DaesiProgrammeUpdateDto updateDto, DaesiProgramme entity)
        {
            if (updateDto.NodalTrainingInstitute is not null && updateDto.NodalTrainingInstitute != entity.NodalTrainingInstitute) entity.NodalTrainingInstitute = updateDto.NodalTrainingInstitute;
            if(updateDto.Place is not null && updateDto.Place != entity.Place) entity.Place = updateDto.Place;
            if (updateDto.StartDate.HasValue) entity.StartDate = updateDto.StartDate.Value;
            if (updateDto.EndDate.HasValue) entity.EndDate = updateDto.EndDate.Value;
            if (updateDto.DealerCount.HasValue) entity.DealerCount = updateDto.DealerCount.Value;
            if (updateDto.BatchCount.HasValue) entity.BatchCount = updateDto.BatchCount.Value;
            if (updateDto.Attachements is not null) entity.Attachements = updateDto.Attachements;
            return Task.CompletedTask;
        }
    }
}

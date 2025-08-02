using Application.Interface;
using Application.Interface.Repository.DataTables;
using Application.Interface.Services.Common;
using Application.Interface.Services.DataTables;
using Application.Models.DataTables;
using Domain.Entities.IBTVA;
using Infrastructure.Repository;

namespace Infrastructure.Services.DataTables
{
    public class IbtvaProgrammeService : GenericTableService<IbtvaProgramme, IbtvaProgrammeCreateDto, IbtvaProgrammeUpdateDto, IbtvaProgrammeDto>, IIbtvaProgrammeService
    {
        public IbtvaProgrammeService(IIbtvaProgrammeRepository repository, ICurrentUserService currentUserService, IEntityPermissionService entityPermissionService) : base(repository, currentUserService, entityPermissionService)
        {
        }

        protected override Task<IbtvaProgramme> MapCreateDtoToEntityAsync(IbtvaProgrammeCreateDto createDto)
        {
            var entity = new IbtvaProgramme
            {
                StartDate = createDto.StartDate,
                EndDate = createDto.EndDate,
                ParticipantCount = createDto.ParticipantCount,
                TrainingTitle = createDto.TrainingTitle,
                Attachements = createDto.Attachements,
                Duration = createDto.Duration,
            };
            return Task.FromResult(entity);
        }

        protected override Task MapUpdateDtoToEntityAsync(IbtvaProgrammeUpdateDto updateDto, IbtvaProgramme entity)
        {
            if (updateDto.StartDate is not null) entity.StartDate = updateDto.StartDate.Value;
            if (updateDto.EndDate is not null) entity.EndDate = updateDto.EndDate.Value;
            if (updateDto.TrainingTitle is not null) entity.TrainingTitle = updateDto.TrainingTitle;
            if (updateDto.Duration != null) entity.Duration = updateDto.Duration.Value;
            if (updateDto.ParticipantCount is not null) entity.ParticipantCount = updateDto.ParticipantCount.Value;
            if (updateDto.Attachements is not null) entity.Attachements = updateDto.Attachements;
            return Task.CompletedTask;
        }
    }
}

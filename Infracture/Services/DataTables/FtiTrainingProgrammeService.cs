using Application.Interface;
using Application.Interface.Repository.DataTables;
using Application.Interface.Services.Common;
using Application.Interface.Services.DataTables;
using Application.Models.DataTables;
using Domain.Entities.FTI;

namespace Infrastructure.Services.DataTables
{
    public class FtiTrainingProgrammeService : GenericTableService<FtiTrainingProgramme, FtiTrainingProgrammeCreateDto, FtiTrainingProgrammeUpdateDto, FtiTrainingProgrammeDto>, IFtiTrainingProgrammeService
    {
        public FtiTrainingProgrammeService(ICurrentUserService currentUserService, IFtiTrainingProgrammeRepository trainingProgrammeRepository, IEntityPermissionService entityPermissionService)
            : base(trainingProgrammeRepository, currentUserService, entityPermissionService)
        {
        }
        protected override Task<FtiTrainingProgramme> MapCreateDtoToEntityAsync(FtiTrainingProgrammeCreateDto createDto)
        {
            var entity = new FtiTrainingProgramme
            {
                TrainingTitle = createDto.TrainingTitle,
                StartDate = createDto.StartDate,
                EndDate = createDto.EndDate,
                Duration = createDto.Duration,
                TrainingCount = createDto.TrainingCount,
                ParticipantCount = createDto.ParticipantCount,
                CreatedById = _currentUserService.UserId,
                CreatedAt = DateTimeOffset.UtcNow,
                OrganizationId = _currentUserService.OrganizationId
            };
            return Task.FromResult(entity);
        }

        protected override Task MapUpdateDtoToEntityAsync(FtiTrainingProgrammeUpdateDto updateDto, FtiTrainingProgramme entity)
        {
            if (updateDto.StartDate is not null) entity.StartDate = updateDto.StartDate.Value;
            if (updateDto.EndDate is not null) entity.EndDate = updateDto.EndDate.Value;
            if (updateDto.Duration != null) entity.Duration = updateDto.Duration.Value;
            if (updateDto.TrainingCount is not null) entity.TrainingCount = updateDto.TrainingCount.Value;
            if (updateDto.ParticipantCount is not null) entity.ParticipantCount = updateDto.ParticipantCount.Value;
            return Task.CompletedTask;
        }
    }
}

//using Application.Interface;
//using Application.Interface.Repository.DataTables;
//using Application.Interface.Services.Common;
//using Application.Interface.Services.DataTables;
//using Application.Models.DataTables;
//using Domain.Entities.EEU;

//namespace Infrastructure.Services.DataTables
//{
//    public class EeuTrainingProgrammeService : GenericTableService<EeuTrainingProgramme, EeuTrainingProgrammeCreateDto, EeuTrainingProgrammeUpdateDto, EeuTrainingProgrammeDto>, IEeuTrainingProgrammeService
//    {
//        public EeuTrainingProgrammeService(ICurrentUserService currentUserService, IEeuTrainingProgrammeRepository trainingProgrammeRepository, IEntityPermissionService entityPermissionService)
//            : base(trainingProgrammeRepository, currentUserService, entityPermissionService)
//        {
//        }
//        protected override Task<EeuTrainingProgramme> MapCreateDtoToEntityAsync(EeuTrainingProgrammeCreateDto createDto)
//        {
//            var entity = new EeuTrainingProgramme
//            {
//                TrainingTitle = createDto.TrainingTitle,
//                StartDate = createDto.StartDate,
//                EndDate = createDto.EndDate,
//                Duration = createDto.Duration,
//                TrainingCount = createDto.TrainingCount,
//                ParticipantCount = createDto.ParticipantCount,
//                CreatedById = _currentUserService.UserId,
//                CreatedAt = DateTimeOffset.UtcNow,
//                OrganizationId = _currentUserService.OrganizationId
//            };
//            return Task.FromResult(entity);
//        }

//        protected override Task MapUpdateDtoToEntityAsync(EeuTrainingProgrammeUpdateDto updateDto, EeuTrainingProgramme entity)
//        {
//            if (updateDto.StartDate is not null) entity.StartDate = updateDto.StartDate.Value;
//            if (updateDto.EndDate is not null) entity.EndDate = updateDto.EndDate.Value;
//            if (updateDto.Duration != null) entity.Duration = updateDto.Duration.Value;
//            if (updateDto.TrainingCount is not null) entity.TrainingCount = updateDto.TrainingCount.Value;
//            if (updateDto.ParticipantCount is not null) entity.ParticipantCount = updateDto.ParticipantCount.Value;
//            return Task.CompletedTask;
//        }
//    }
//}

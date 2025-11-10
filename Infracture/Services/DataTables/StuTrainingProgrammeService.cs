//using Application.Interface;
//using Application.Interface.Repository.DataTables;
//using Application.Interface.Services.Common;
//using Application.Interface.Services.DataTables;
//using Application.Models;
//using Application.Models.DataTables;
//using Domain.Entities;
//using Domain.Entities.STU;
//using System.Diagnostics;
//using System.Security.Cryptography;

//namespace Infrastructure.Services.DataTables
//{
//    public class StuTrainingProgrammeService: GenericTableService<StuTrainingProgramme, StuTrainingProgrammeCreateDto, StuTrainingProgrammeUpdateDto, StuTrainingProgrammeDto>,IStuTrainingProgrammeService
//    {
//        public StuTrainingProgrammeService(IStuTrainingProgrammeRepository stuTrainingProgrammeRepository, ICurrentUserService currentUserService, IEntityPermissionService entityPermissionService)
//            :base(stuTrainingProgrammeRepository,currentUserService,entityPermissionService)
//        {
//        }

//        protected override Task<StuTrainingProgramme> MapCreateDtoToEntityAsync(StuTrainingProgrammeCreateDto createDto)
//        {
//            var entity = new StuTrainingProgramme
//            {
//                TrainingTitle = createDto.TrainingTitle,
//                ParticipantCount = createDto.ParticipantCount,
//                TrainingCount = createDto.TrainingCount,
//                Duration = createDto.Duration,
//                UnitLocationId = createDto.UnitLocationId,
//                Attachements = createDto.Attachements,
//                EndDate = createDto.EndDate,
//                StartDate = createDto.StartDate,
//            };
//            return Task.FromResult(entity);
//        }

//        protected override Task MapUpdateDtoToEntityAsync(StuTrainingProgrammeUpdateDto updateDto, StuTrainingProgramme entity)
//        {
//            if (updateDto.EndDate.HasValue) entity.EndDate = updateDto.EndDate.Value;
//            if (updateDto.StartDate.HasValue) entity.StartDate = updateDto.StartDate.Value;
//            if (updateDto.Duration.HasValue) entity.Duration = updateDto.Duration.Value;
//            if (updateDto.ParticipantCount.HasValue) entity.ParticipantCount = updateDto.ParticipantCount.Value;
//            if (updateDto.TrainingTitle is not null) entity.TrainingTitle = updateDto.TrainingTitle;
//            if (updateDto.TrainingCount.HasValue) entity.TrainingCount = updateDto.TrainingCount.Value;
//            if (updateDto.Attachements is not null) entity.Attachements = updateDto.Attachements;
//            return Task.CompletedTask;
//        }
//    }
//}

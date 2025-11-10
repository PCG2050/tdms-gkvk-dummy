//using Application.Interface;
//using Application.Interface.Repository.DataTables;
//using Application.Interface.Services.Common;
//using Application.Interface.Services.DataTables;
//using Application.Models.DataTables;
//using Domain.Entities.ASM;
//using Domain.Entities.NAEP;

//namespace Infrastructure.Services.DataTables
//{
//    public class NaepDetailsService : GenericTableService<NaepDetails, NaepDetailsCreateDto, NaepDetailsUpdateDto, NaepDetailsDto>, INaepDetailsService
//    {
//        public NaepDetailsService(INaepDetailsRepository repository, ICurrentUserService currentUserService, IEntityPermissionService entityPermissionService) : base(repository, currentUserService, entityPermissionService)
//        {
//        }

//        protected override Task<NaepDetails> MapCreateDtoToEntityAsync(NaepDetailsCreateDto createDto)
//        {
//            var entity = new NaepDetails
//            {
//                Particulars = createDto.Particulars,
//                Place = createDto.Place,
//                ParticipantCount = createDto.ParticipantCount,
//                ProgrammeCount = createDto.ProgrammeCount,
//                StartDate = createDto.StartDate,
//                EndDate = createDto.EndDate,
//                UnitLocationId = createDto.UnitLocationId,
//                Attachements = createDto.Attachements,
//            };
//            return Task.FromResult(entity);
//        }

//        protected override Task MapUpdateDtoToEntityAsync(NaepDetailsUpdateDto updateDto, NaepDetails entity)
//        {
//            if (updateDto.StartDate.HasValue) entity.StartDate = updateDto.StartDate.Value;
//            if (updateDto.EndDate.HasValue) entity.EndDate = updateDto.EndDate.Value;
//            if (updateDto.Particulars is not null ) entity.Particulars = updateDto.Particulars;
//            if (updateDto.Place is not null ) entity.Place = updateDto.Place;
//            if (updateDto.ParticipantCount.HasValue) entity.ParticipantCount = updateDto.ParticipantCount.Value;
//            if (updateDto.ProgrammeCount.HasValue) entity.ProgrammeCount = updateDto.ProgrammeCount.Value;
//            if (updateDto.Attachements is not null) entity.Attachements = updateDto.Attachements;
//            return Task.CompletedTask;
//        }
//    }
//}

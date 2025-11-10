//using Application.Interface;
//using Application.Interface.Repository.DataTables;
//using Application.Interface.Services.Common;
//using Application.Interface.Services.DataTables;
//using Application.Models.DataTables;
//using Domain.Entities.EEU;

//namespace Infrastructure.Services.DataTables
//{
//    public class EeuFldService : GenericTableService<EeuFLD, EeuFldCreateDto, EeuFldUpdateDto, EeuFldDto>, IEeuFldService
//    {
//        public EeuFldService(IEeuFldRepository repository, ICurrentUserService currentUserService, IEntityPermissionService entityPermissionService) : base(repository, currentUserService, entityPermissionService)
//        {
//        }

//        protected override Task<EeuFLD> MapCreateDtoToEntityAsync(EeuFldCreateDto createDto)
//        {
//            var entity = new EeuFLD
//            {
//                StartDate = createDto.StartDate,
//                EndDate = createDto.EndDate,
//                UnitLocationId = createDto.UnitLocationId,
//                Title = createDto.Title,
//                Crop = createDto.Crop,
//                Area = createDto.Area,
//                TrialMaleScStCount = createDto.TrialMaleScStCount,
//                TrialMaleGenCount = createDto.TrialMaleGenCount,
//                TrialFemaleScStCount = createDto.TrialFemaleScStCount,
//                TrailFemalGenCount = createDto.TrailFemalGenCount,
//                YieldCheck = createDto.YieldCheck,
//                YieldDemo = createDto.YieldDemo,
//                PercentIncreaseInYield = createDto.PercentIncreaseInYield,
//                Attachements = createDto.Attachements,
//            };
//            return Task.FromResult(entity);
//        }

//        protected override Task MapUpdateDtoToEntityAsync(EeuFldUpdateDto updateDto, EeuFLD entity)
//        {
//            if (updateDto.StartDate is not null) entity.StartDate = updateDto.StartDate.Value;
//            if (updateDto.EndDate is not null) entity.EndDate = updateDto.EndDate.Value;
//            if (updateDto.Title is not null) entity.Title = updateDto.Title;
//            if (updateDto.Crop is not null) entity.Crop = updateDto.Crop;
//            if (updateDto.Area.HasValue) entity.Area = updateDto.Area.Value;
//            if (updateDto.TrialMaleScStCount.HasValue) entity.TrialMaleScStCount = updateDto.TrialMaleScStCount.Value;
//            if (updateDto.TrialMaleGenCount.HasValue) entity.TrialMaleGenCount = updateDto.TrialMaleGenCount.Value;
//            if (updateDto.TrialFemaleScStCount.HasValue) entity.TrialFemaleScStCount = updateDto.TrialFemaleScStCount.Value;
//            if (updateDto.TrailFemalGenCount.HasValue) entity.TrailFemalGenCount = updateDto.TrailFemalGenCount.Value;
//            if (updateDto.YieldCheck.HasValue) entity.YieldCheck = updateDto.YieldCheck.Value;
//            if (updateDto.YieldDemo.HasValue) entity.YieldDemo = updateDto.YieldDemo.Value;
//            if (updateDto.PercentIncreaseInYield.HasValue) entity.PercentIncreaseInYield = updateDto.PercentIncreaseInYield.Value;
//            return Task.CompletedTask;
//        }
//    }
//}

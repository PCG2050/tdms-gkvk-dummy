//using Application.Interface;
//using Application.Interface.Repository.DataTables;
//using Application.Interface.Services.Common;
//using Application.Interface.Services.DataTables;
//using Application.Models;
//using Application.Models.DataTables;
//using Domain.Entities.FTI;
//using Infrastructure.Repository;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Infrastructure.Services.DataTables
//{
//    public class FtiOftService : GenericTableService<FtiOFT, FtiOftCreateDto, FtiOftUpdateDto, FtiOftDto>, IFtiOftService
//    {
//        public FtiOftService(IFtiOftRepository repository, ICurrentUserService currentUserService, IEntityPermissionService entityPermissionService) : base(repository, currentUserService, entityPermissionService)
//        {
//        }

//        protected override Task<FtiOFT> MapCreateDtoToEntityAsync(FtiOftCreateDto createDto)
//        {
//            var entity = new FtiOFT
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
//                YieldT1 = createDto.YieldT1,
//                YieldT2 = createDto.YieldT2,
//                PercentIncreaseInYield = createDto.PercentIncreaseInYield,
//                Attachements = createDto.Attachements,
//            };
//            return Task.FromResult(entity);
//        }

//        protected override Task MapUpdateDtoToEntityAsync(FtiOftUpdateDto updateDto, FtiOFT entity)
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
//            if (updateDto.YieldT1.HasValue) entity.YieldT1 = updateDto.YieldT1.Value;
//            if (updateDto.YieldT2.HasValue) entity.YieldT2 = updateDto.YieldT2.Value;
//            if (updateDto.PercentIncreaseInYield.HasValue) entity.PercentIncreaseInYield = updateDto.PercentIncreaseInYield.Value;
//            return Task.CompletedTask;
//        }
//    }
//}

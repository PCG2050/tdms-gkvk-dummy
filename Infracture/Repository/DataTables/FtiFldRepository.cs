//using Application.Interface.Repository.DataTables;
//using Application.Models.DataTables;
//using Domain.Entities.FTI;
//using Infrastructure.DbContext;

//namespace Infrastructure.Repository.DataTables
//{
//    public class FtiFldRepository : GenericRepository<FtiFLD, FtiFldDto>, IFtiFldRepository
//    {
//        public FtiFldRepository(TdmsDbContext context) : base(context)
//        {
//        }

//        protected override IQueryable<FtiFldDto> ProjectToDto(IQueryable<FtiFLD> query)
//        {
//            return query.Select(d => new FtiFldDto
//            {
//                Id = d.Id,
//                StartDate = d.StartDate,
//                EndDate = d.EndDate,
//                UnitLocationId = d.UnitLocationId,
//                Title = d.Title,
//                Crop = d.Crop,
//                Area = d.Area,
//                TrialMaleScStCount = d.TrialMaleScStCount,
//                TrialMaleGenCount = d.TrialMaleGenCount,
//                TrialFemaleScStCount = d.TrialFemaleScStCount,
//                TrailFemalGenCount = d.TrailFemalGenCount,
//                YieldCheck = d.YieldCheck,
//                YieldDemo = d.YieldDemo,
//                PercentIncreaseInYield = d.PercentIncreaseInYield,
//                Attachements = d.Attachements,
//                CreatedAt = d.CreatedAt,
//                UpdatedAt = d.UpdatedAt
//            });
//        }
//    }
//}

using Application.Interface.Repository.DataTables;
using Application.Models.DataTables;
using Domain.Entities.EEU;
using Infrastructure.DbContext;

namespace Infrastructure.Repository.DataTables
{
    public class EeuOftRepository : GenericRepository<EeuOFT, EeuOftDto>, IEeuOftRepository
    {
        public EeuOftRepository(TdmsDbContext context) : base(context)
        {
        }

        protected override IQueryable<EeuOftDto> ProjectToDto(IQueryable<EeuOFT> query)
        {
            return query.Select(d => new EeuOftDto
            {
                Id = d.Id,
                StartDate = d.StartDate,
                EndDate = d.EndDate,
                UnitLocationId = d.UnitLocationId,
                Title = d.Title,
                Crop = d.Crop,
                Area = d.Area,
                TrialMaleScStCount = d.TrialMaleScStCount,
                TrialMaleGenCount = d.TrialMaleGenCount,
                TrialFemaleScStCount = d.TrialFemaleScStCount,
                TrailFemalGenCount = d.TrailFemalGenCount,
                YieldT1 = d.YieldT1,
                YieldT2 = d.YieldT2,
                PercentIncreaseInYield = d.PercentIncreaseInYield,
                Attachements = d.Attachements,
                CreatedAt = d.CreatedAt,
                UpdatedAt = d.UpdatedAt
            });
        }
    }
}

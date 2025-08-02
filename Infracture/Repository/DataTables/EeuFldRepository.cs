using Application.Interface.Repository.DataTables;
using Application.Models.DataTables;
using Domain.Entities.EEU;
using Infrastructure.DbContext;

namespace Infrastructure.Repository.DataTables
{
    public class EeuFldRepository : GenericRepository<EeuFLD, EeuFldDto>, IEeuFldRepository
    {
        public EeuFldRepository(TdmsDbContext context) : base(context)
        {
        }

        protected override IQueryable<EeuFldDto> ProjectToDto(IQueryable<EeuFLD> query)
        {
            return query.Select(d => new EeuFldDto
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
                YieldCheck = d.YieldCheck,
                YieldDemo = d.YieldDemo,
                PercentIncreaseInYield = d.PercentIncreaseInYield,
                Attachements = d.Attachements,
                CreatedAt = d.CreatedAt,
                UpdatedAt = d.UpdatedAt
            });
        }
    }
}

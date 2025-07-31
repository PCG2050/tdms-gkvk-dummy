using Application.Interface.Repository.DataTables;
using Application.Models;
using Application.Models.DataTables;
using Domain.Entities.FTI;
using Infrastructure.DbContext;

namespace Infrastructure.Repository.DataTables
{
    public class FtiOtherActivitiesRepository : GenericRepository<FtiOtherActivity, FtiOtherActivityDto>,IFtiOtherActivitiesRepository
    {
        public FtiOtherActivitiesRepository(TdmsDbContext context):base(context)
        {
        }
        protected override IQueryable<FtiOtherActivityDto> ProjectToDto(IQueryable<FtiOtherActivity> query)
        {
            return query.Select(f => new FtiOtherActivityDto
            {
                Id = f.Id,
                ActivityDetails = f.ActivityDetails,
                CreatedAt = f.CreatedAt,
                EndDate = f.EndDate,
                StartDate = f.StartDate,
                UpdateAt = f.UpdatedAt
            });
        }
    }
}

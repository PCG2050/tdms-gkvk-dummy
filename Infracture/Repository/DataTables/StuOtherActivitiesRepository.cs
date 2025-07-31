using Application.Interface.Repository.DataTables;
using Application.Models.DataTables;
using Domain.Entities.STU;
using Infrastructure.DbContext;

namespace Infrastructure.Repository.DataTables
{
    public class StuOtherActivitiesRepository : GenericRepository<StuOtherActivity,StuOtherActivityDto>,IStuOtherActivitiesRepository
    {
        public StuOtherActivitiesRepository(TdmsDbContext context) : base(context)
        {
        }

        protected override IQueryable<StuOtherActivityDto> ProjectToDto(IQueryable<StuOtherActivity> query)
        {
            return query.Select(f => new StuOtherActivityDto
            {
                Id = f.Id,
                ActivityDetails = f.ActivityDetails,
                CreatedAt = f.CreatedAt,
                EndDate = f.EndDate,
                StartDate = f.StartDate,
                UpdateAt = f.UpdatedAt,
                Attachements = f.Attachements
            });
        }
    }
}

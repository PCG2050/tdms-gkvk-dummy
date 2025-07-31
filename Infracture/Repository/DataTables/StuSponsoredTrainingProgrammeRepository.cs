using Application.Interface.Repository.DataTables;
using Application.Models.DataTables;
using Domain.Entities.STU;
using Infrastructure.DbContext;

namespace Infrastructure.Repository.DataTables
{
    public class StuSponsoredTrainingProgrammeRepository :GenericRepository<StuSponsoredTrainingProgramme, StuSponsoredTrainingProgrammeDto> ,IStuSposoredTrainingProgrammeRepository
    {

        public StuSponsoredTrainingProgrammeRepository(TdmsDbContext context):base(context)
        {
        }

        protected override IQueryable<StuSponsoredTrainingProgrammeDto> ProjectToDto(IQueryable<StuSponsoredTrainingProgramme> query)
        {
            return query.Select(f => new StuSponsoredTrainingProgrammeDto
            {
                Id = f.Id,
                SponsoredOrganization = f.SponsorOrganization,
                TrainingTitle = f.TrainingTitle,
                ParticipantCount = f.ParticipantCount,
                TrainingCount = f.TrainingCount,
                Duration = f.Duration,
                UnitLocationId = f.UnitLocationId,
                Attachements = f.Attachements,
                CreatedAt = f.CreatedAt,
                EndDate = f.EndDate,
                StartDate = f.StartDate,
                UpdatedAt = f.UpdatedAt
            });
        }
    }
}

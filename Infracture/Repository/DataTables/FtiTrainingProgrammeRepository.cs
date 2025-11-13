using Application.Interface.Repository.DataTables;
using Application.Models.DataTables;
using Domain.Entities.FTI;
using Infrastructure.DbContext;

namespace Infrastructure.Repository.DataTables
{
    public class FtiTrainingProgrammeRepository : GenericRepository<FtiTrainingProgramme, FtiTrainingProgrammeDto>, IFtiTrainingProgrammeRepository
    {
        public FtiTrainingProgrammeRepository(TdmsDbContext context) : base(context)
        {
        }

        protected override IQueryable<FtiTrainingProgrammeDto> ProjectToDto(IQueryable<FtiTrainingProgramme> query)
        {
            return query.Select(f => new FtiTrainingProgrammeDto
            {
                Id = f.Id,
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

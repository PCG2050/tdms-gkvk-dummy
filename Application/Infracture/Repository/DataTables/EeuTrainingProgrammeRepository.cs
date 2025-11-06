using Application.Interface.Repository.DataTables;
using Application.Models.DataTables;
using Domain.Entities.EEU;
using Infrastructure.DbContext;

namespace Infrastructure.Repository.DataTables
{
    public class EeuTrainingProgrammeRepository : GenericRepository<EeuTrainingProgramme, EeuTrainingProgrammeDto>, IEeuTrainingProgrammeRepository
    {
        public EeuTrainingProgrammeRepository(TdmsDbContext context) : base(context)
        {
        }

        protected override IQueryable<EeuTrainingProgrammeDto> ProjectToDto(IQueryable<EeuTrainingProgramme> query)
        {
            return query.Select(f => new EeuTrainingProgrammeDto
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

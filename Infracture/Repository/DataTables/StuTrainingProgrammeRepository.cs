using Application.Interface.Repository.DataTables;
using Application.Models;
using Application.Models.DataTables;
using Domain.Entities.FTI;
using Domain.Entities.STU;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables
{
    public class StuTrainingProgrammeRepository : GenericRepository<StuTrainingProgramme, StuTrainingProgrammeDto>,IStuTrainingProgrammeRepository
    {
        public StuTrainingProgrammeRepository(TdmsDbContext context):base(context)
        {
        }

        protected override IQueryable<StuTrainingProgrammeDto> ProjectToDto(IQueryable<StuTrainingProgramme> query)
        {
            return query.Select(f => new StuTrainingProgrammeDto
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

using Application.Interface.Repository.DataTables;
using Application.Models.DataTables;
using Domain.Entities.ASM;
using Domain.Entities.NAEP;
using Infrastructure.DbContext;

namespace Infrastructure.Repository.DataTables
{
    public class NaepDetailsRepository : GenericRepository<NaepDetails, NaepDetailsDto>, INaepDetailsRepository
    {
        public NaepDetailsRepository(TdmsDbContext context) : base(context)
        {
        }

        protected override IQueryable<NaepDetailsDto> ProjectToDto(IQueryable<NaepDetails> query)
        {
            return query.Select(d => new NaepDetailsDto
            {
                Id = d.Id,
                StartDate = d.StartDate,
                EndDate = d.EndDate,
                UnitLocationId = d.UnitLocationId,
                Particulars = d.Particulars,
                Place = d.Place,
                ParticipantCount = d.ParticipantCount,
                ProgrammeCount = d.ProgrammeCount,
                Attachements = d.Attachements,
                CreatedAt = d.CreatedAt,
                UpdatedAt = d.UpdatedAt
            });
        }
    }
}

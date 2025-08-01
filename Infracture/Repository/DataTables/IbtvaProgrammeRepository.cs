using Application.Interface.Repository.DataTables;
using Application.Models.DataTables;
using Domain.Entities.IBTVA;
using Infrastructure.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.DataTables
{
    public class IbtvaProgrammeRepository : GenericRepository<IbtvaProgramme, IbtvaProgrammeDto>, IIbtvaProgrammeRepository
    {
        public IbtvaProgrammeRepository(TdmsDbContext context) : base(context)
        {
        }

        protected override IQueryable<IbtvaProgrammeDto> ProjectToDto(IQueryable<IbtvaProgramme> query)
        {
            return query.Select(p => new IbtvaProgrammeDto
            {
                Id = p.Id,
                TrainingTitle = p.TrainingTitle,
                StartDate = p.StartDate,
                EndDate = p.EndDate,
                Duration = p.Duration,
                ParticipantCount = p.ParticipantCount,
                Attachements = p.Attachements,
                CreatedAt = p.CreatedAt,
                UpdateAt = p.UpdatedAt
            });
        }
    }
}

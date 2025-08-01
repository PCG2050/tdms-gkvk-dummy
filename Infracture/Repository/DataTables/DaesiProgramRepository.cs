using Application.Interface.Repository.DataTables;
using Application.Models.DataTables;
using Domain.Entities.STU;
using Infrastructure.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.DataTables
{
    public class DaesiProgramRepository : GenericRepository<DaesiProgramme, DaesiProgrammeDto>, IDaesiProgrammeRepository
    {
        public DaesiProgramRepository(TdmsDbContext context) : base(context)
        {
        }

        protected override IQueryable<DaesiProgrammeDto> ProjectToDto(IQueryable<DaesiProgramme> query)
        {
            return query.Select(d => new DaesiProgrammeDto
            {
                Id = d.Id,
                StartDate = d.StartDate,
                EndDate = d.EndDate,
                NodalTrainingInstitute = d.NodalTrainingInstitute,
                Place = d.Place,
                UnitLocatioId = d.UnitLocationId,
                BatchCount = d.BatchCount,
                DealerCount = d.DealerCount,
                Attachements = d.Attachements,
                CreatedAt = d.CreatedAt,
                UpdatedAt = d.UpdatedAt
            });
        }
    }
}

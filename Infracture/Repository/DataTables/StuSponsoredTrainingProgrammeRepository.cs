using Application.Interface.Repository.DataTables;
using Application.Models;
using Application.Models.DataTables;
using Domain.Entities.STU;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables
{
    public class StuSponsoredTrainingProgrammeRepository : IStuSposoredTrainingProgrammeRepository
    {
        private readonly TdmsDbContext _context;

        public StuSponsoredTrainingProgrammeRepository(TdmsDbContext context)
        {
            _context = context;
        }
        public async Task<StuSponsoredTrainingProgramme> AddAsync(StuSponsoredTrainingProgramme activity)
        {
            await _context.StuSponsoredTrainingProgrammes.AddAsync(activity);
            await _context.SaveChangesAsync();
            return activity;
        }

        public async Task<bool> DeleteAsync(StuSponsoredTrainingProgramme activity)
        {
            _context.StuSponsoredTrainingProgrammes.Remove(activity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<StuSponsoredTrainingProgramme?> GetAsync(int id)
        {
            return await _context.StuSponsoredTrainingProgrammes.FindAsync(id);
        }

        public async Task<PaginatedResult<StuSponsoredTrainingProgrammeDto>> GetPaginatedItemsAsync(int organizationId, int pageNumber = 1, QueryFilter? queryFilter = null, int pageSize = 10)
        {
            var query = _context.StuSponsoredTrainingProgrammes.Where(x => x.OrganizationId == organizationId);
            if (queryFilter?.Filters.Count > 0)
            {
                var filters = queryFilter.Filters;
                if (filters.ContainsKey("TrainerId") && Int32.TryParse(filters["TrainerId"].ToString(), out int trainerId)) query = query.Where(x => x.CreatedById == trainerId);
            }
            var paginatedResult = new PaginatedResult<StuSponsoredTrainingProgrammeDto>
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            paginatedResult.TotalItems = await query.AsNoTracking().CountAsync();
            int offset = (pageNumber - 1) * pageSize;
            paginatedResult.Items = await query.Skip(offset).Take(pageSize)
                .Select(f => new StuSponsoredTrainingProgrammeDto
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
                }).ToListAsync();
            return paginatedResult;
        }

        public async Task<StuSponsoredTrainingProgramme> UpdateAsync(StuSponsoredTrainingProgramme activity)
        {
            if (_context.StuSponsoredTrainingProgrammes.Entry(activity).State == Microsoft.EntityFrameworkCore.EntityState.Modified)
                await _context.SaveChangesAsync();
            return activity;
        }
    }
}

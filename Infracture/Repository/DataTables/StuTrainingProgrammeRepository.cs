using Application.Interface.Repository.DataTables;
using Application.Models;
using Application.Models.DataTables;
using Domain.Entities.FTI;
using Domain.Entities.STU;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables
{
    public class StuTrainingProgrammeRepository : IStuTrainingProgrammeRepository
    {
        private readonly TdmsDbContext _context;

        public StuTrainingProgrammeRepository(TdmsDbContext context)
        {
            _context = context;
        }
        public async Task<StuTrainingProgramme> AddAsync(StuTrainingProgramme activity)
        {
            await _context.StuTrainingProgrammes.AddAsync(activity);
            await _context.SaveChangesAsync();
            return activity;
        }

        public async Task<bool> DeleteAsync(StuTrainingProgramme activity)
        {
            _context.StuTrainingProgrammes.Remove(activity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<StuTrainingProgramme?> GetAsync(int id)
        {
            return await _context.StuTrainingProgrammes.FindAsync(id);
        }

        public async Task<PaginatedResult<StuTrainingProgrammeDto>> GetPaginatedItemsAsync(int organizationId, int pageNumber = 1, QueryFilter? queryFilter = null, int pageSize = 10)
        {
            var query = _context.StuTrainingProgrammes.Where(x => x.OrganizationId == organizationId);
            if (queryFilter?.Filters.Count > 0)
            {
                var filters = queryFilter.Filters;
                if (filters.ContainsKey("TrainerId") && Int32.TryParse(filters["TrainerId"].ToString(), out int trainerId)) query = query.Where(x => x.CreatedById == trainerId);
            }
            var paginatedResult = new PaginatedResult<StuTrainingProgrammeDto>
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            paginatedResult.TotalItems = await query.AsNoTracking().CountAsync();
            int offset = (pageNumber - 1) * pageSize;
            paginatedResult.Items = await query.Skip(offset).Take(pageSize)
                .Select(f => new StuTrainingProgrammeDto
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
                }).ToListAsync();
            return paginatedResult;
        }

        public async Task<StuTrainingProgramme> UpdateAsync(StuTrainingProgramme activity)
        {
            if (_context.StuTrainingProgrammes.Entry(activity).State == Microsoft.EntityFrameworkCore.EntityState.Modified)
                await _context.SaveChangesAsync();
            return activity;
        }
    }
}

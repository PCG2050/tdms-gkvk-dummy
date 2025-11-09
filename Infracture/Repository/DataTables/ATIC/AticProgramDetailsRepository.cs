// AticProgramDetailsRepository.cs
using Application.Interface.Repository.DataTables.ATIC;
using Application.Models;
using Domain.Entities.ATIC;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.ATIC
{
    public class AticProgramDetailsRepository : IAticProgramDetailsRepository
    {
        private readonly TdmsDbContext _context;

        public AticProgramDetailsRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<List<AticProgramDetails>> GetAllAsync()
        {
            return await _context.AticProgramDetails
               .Include(p => p.ProgramContent!)
                    .ThenInclude(pc => pc.ResourcePersons)
                .Include(p => p.ProgramContent!)
                    .ThenInclude(pc => pc.TopicsCovered)
                .Include(p => p.ProgramContent!)
                    .ThenInclude(pc => pc.TeachingAids)
                .Include(p => p.AdvisoryServices)
                .Include(p => p.Reports)
                .Include(p => p.Recommendations)
                .ToListAsync();
        }
        public async Task<AticProgramDetails?> GetByIdAsync(int id)
        {
            return await _context.AticProgramDetails.FindAsync(id);
        }

        public async Task<AticProgramDetails?> GetWithDetailsAsync(int id)
        {
            return await _context.AticProgramDetails
                .Include(p => p.ProgramType)
                .Include(p => p.Category)
                .Include(p => p.Type)
                .Include(p => p.Theme)
                .Include(p => p.ThematicArea)
                .Include(p => p.Mode)
                .Include(p => p.Region)
                .Include(p => p.SourceOfFund)
                .Include(p => p.Status)
                .Include(p => p.Source)
                .Include(p => p.UnitLocation)
                    .ThenInclude(ul => ul.Unit)
                .Include(p => p.UnitLocation)
                    .ThenInclude(ul => ul.District)
                        .ThenInclude(d => d.State)
                .Include(p => p.Organization)
                .Include(p => p.CreatedBy)
                .Include(p => p.UpdatedBy)
                .Include(p => p.ApprovedBy)
                .Include(p => p.ParticipantDemographics)
                // Fix: Use correct nullability for ThenInclude chains
                .Include(p => p.ProgramContent!)
                    .ThenInclude(pc => pc.ResourcePersons)
                .Include(p => p.ProgramContent!)
                    .ThenInclude(pc => pc.TopicsCovered)
                .Include(p => p.ProgramContent!)
                    .ThenInclude(pc => pc.TeachingAids)
                .Include(p => p.AdvisoryServices)
                .Include(p => p.Reports)
                .Include(p => p.Recommendations)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<AticProgramDetails> CreateAsync(AticProgramDetails entity)
        {
            _context.AticProgramDetails.Add(entity);
            await _context.SaveChangesAsync();
            return await GetWithDetailsAsync(entity.Id) ?? entity;
        }

        public async Task<AticProgramDetails> UpdateAsync(AticProgramDetails entity)
        {
            _context.AticProgramDetails.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.AticProgramDetails.FindAsync(id);
            if (entity != null)
            {
                _context.AticProgramDetails.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<PaginatedResult<AticProgramDetails>> GetPaginatedAsync(
            List<int> unitLocationIds,
            int pageNumber = 1,
            int pageSize = 10,
            DateOnly? startDate = null,
            DateOnly? endDate = null,
            int? programTypeId = null,
            string? searchTerm = null)
        {
            var query = _context.AticProgramDetails
                .Include(p => p.ProgramType)
                .Include(p => p.Category)
                .Include(p => p.Mode)
                .Include(p => p.Region)
                .Include(p => p.UnitLocation)
                    .ThenInclude(ul => ul.Unit)
                .Include(p => p.UnitLocation)
                    .ThenInclude(ul => ul.District)
                        .ThenInclude(d => d.State)
                .Include(p => p.CreatedBy)
                .Include(p => p.ApprovedBy)
                .Where(p => unitLocationIds.Contains(p.UnitLocationId))
                .AsQueryable();

            if (startDate.HasValue)
                query = query.Where(p => p.StartDate >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(p => p.EndDate <= endDate.Value);

            if (programTypeId.HasValue)
                query = query.Where(p => p.ProgramTypeId == programTypeId.Value);

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var lowerSearch = searchTerm.ToLower();
                query = query.Where(p =>
                    p.Title.ToLower().Contains(lowerSearch) ||
                    (p.Location != null && p.Location.ToLower().Contains(lowerSearch)));
            }

            var totalItems = await query.CountAsync();
            var items = await query
                .OrderByDescending(p => p.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedResult<AticProgramDetails>(items, totalItems, pageNumber, pageSize);
        }

        public async Task<PaginatedResult<AticProgramDetails>> GetByStatusAsync(
            List<int> unitLocationIds,
            string status,
            int pageNumber = 1,
            int pageSize = 10)
        {
            var query = _context.AticProgramDetails
                .Include(p => p.ProgramType)
                .Include(p => p.Category)
                .Include(p => p.UnitLocation)
                    .ThenInclude(ul => ul.Unit)
                .Include(p => p.CreatedBy)
                .Include(p => p.ApprovedBy)
                .Where(p => unitLocationIds.Contains(p.UnitLocationId) &&
                           p.FormStatus == status);

            var totalItems = await query.CountAsync();
            var items = await query
                .OrderByDescending(p => p.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedResult<AticProgramDetails>(items, totalItems, pageNumber, pageSize);
        }

        public async Task<Dictionary<string, int>> GetStatusSummaryAsync(List<int> unitLocationIds)
        {
            return await _context.AticProgramDetails
                .Where(p => unitLocationIds.Contains(p.UnitLocationId))
                .GroupBy(p => p.FormStatus)
                .Select(g => new { Status = g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.Status, x => x.Count);
        }
    }
}
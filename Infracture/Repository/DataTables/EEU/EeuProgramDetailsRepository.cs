// EeuProgramDetailsRepository.cs
using Application.Interface.Repository.DataTables.DEU;
using Application.Models;
using Domain.Entities.DEU;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.EEU
{
    public class EeuProgramDetailsRepository : IEeuProgramDetailsRepository
    {
        private readonly TdmsDbContext _context;

        public EeuProgramDetailsRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public IQueryable<EeuProgramDetails> GetQueryable()
        {
            return _context.EeuProgramDetails.AsQueryable();
        }


        public async Task<List<EeuProgramDetails>> GetAllAsync()
        {
            return await _context.EeuProgramDetails
               .Include(p => p.ProgramContent!)
                    .ThenInclude(pc => pc.ResourcePersons)
                .Include(p => p.ProgramContent!)
                    .ThenInclude(pc => pc.TopicsCovered)
                .Include(p => p.ProgramContent!)
                    .ThenInclude(pc => pc.TeachingAids)
                .Include(p => p.AdvisoryServices)
                .Include(p => p.Reports)
                .Include(p => p.Recommendations)
                .AsSplitQuery()
                .ToListAsync();
        }

        public async Task<EeuProgramDetails?> GetByIdAsync(int id)
        {
            return await _context.EeuProgramDetails.FindAsync(id);
        }

        public async Task<EeuProgramDetails?> GetWithDetailsAsync(int id)
        {
            return await _context.EeuProgramDetails
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

        public async Task<EeuProgramDetails> CreateAsync(EeuProgramDetails entity)
        {
            _context.EeuProgramDetails.Add(entity);
            await _context.SaveChangesAsync();
            return await GetWithDetailsAsync(entity.Id) ?? entity;
        }

        public async Task<EeuProgramDetails> UpdateAsync(EeuProgramDetails entity)
        {
            _context.EeuProgramDetails.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.EeuProgramDetails.FindAsync(id);
            if (entity != null)
            {
                _context.EeuProgramDetails.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<PaginatedResult<EeuProgramDetails>> GetPaginatedAsync(
            List<int> unitLocationIds,
            int pageNumber = 1,
            int pageSize = 10,
            DateOnly? startDate = null,
            DateOnly? endDate = null,
            int? programTypeId = null,
            string? searchTerm = null)
        {
            var query = _context.EeuProgramDetails
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

            return new PaginatedResult<EeuProgramDetails>(items, totalItems, pageNumber, pageSize);
        }

        public async Task<PaginatedResult<EeuProgramDetails>> GetByStatusAsync(
            List<int> unitLocationIds,
            string status,
            int pageNumber = 1,
            int pageSize = 10)
        {
            var query = _context.EeuProgramDetails
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

            return new PaginatedResult<EeuProgramDetails>(items, totalItems, pageNumber, pageSize);
        }

        public async Task<Dictionary<string, int>> GetStatusSummaryAsync(List<int> unitLocationIds)
        {
            return await _context.EeuProgramDetails
                .Where(p => unitLocationIds.Contains(p.UnitLocationId))
                .GroupBy(p => p.FormStatus)
                .Select(g => new { Status = g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.Status, x => x.Count);
        }
    }
}
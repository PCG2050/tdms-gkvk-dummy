// IbtvaProgramDetailsRepository.cs
using Application.Interface.Repository.DataTables.IBTVA;
using Application.Models;
using Domain.Entities.IBTVA;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.IBTVA
{
    public class IbtvaProgramDetailsRepository : IIbtvaProgramDetailsRepository
    {
        private readonly TdmsDbContext _context;

        public IbtvaProgramDetailsRepository(TdmsDbContext context)
        {
            _context = context;
        }


        public async Task<List<IbtvaProgramDetails>> GetAllAsync()
        {
            return await _context.IbtvaProgramDetails
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
        public async Task<IbtvaProgramDetails?> GetByIdAsync(int id)
        {
            return await _context.IbtvaProgramDetails.FindAsync(id);
        }

        public async Task<IbtvaProgramDetails?> GetWithDetailsAsync(int id)
        {
            return await _context.IbtvaProgramDetails
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

        public async Task<IbtvaProgramDetails> CreateAsync(IbtvaProgramDetails entity)
        {
            _context.IbtvaProgramDetails.Add(entity);
            await _context.SaveChangesAsync();
            return await GetWithDetailsAsync(entity.Id) ?? entity;
        }

        public async Task<IbtvaProgramDetails> UpdateAsync(IbtvaProgramDetails entity)
        {
            _context.IbtvaProgramDetails.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.IbtvaProgramDetails.FindAsync(id);
            if (entity != null)
            {
                _context.IbtvaProgramDetails.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<PaginatedResult<IbtvaProgramDetails>> GetPaginatedAsync(
            List<int> unitLocationIds,
            int pageNumber = 1,
            int pageSize = 10,
            DateOnly? startDate = null,
            DateOnly? endDate = null,
            int? programTypeId = null,
            string? searchTerm = null)
        {
            var query = _context.IbtvaProgramDetails
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

            return new PaginatedResult<IbtvaProgramDetails>(items, totalItems, pageNumber, pageSize);
        }

        public async Task<PaginatedResult<IbtvaProgramDetails>> GetByStatusAsync(
            List<int> unitLocationIds,
            string status,
            int pageNumber = 1,
            int pageSize = 10)
        {
            var query = _context.IbtvaProgramDetails
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

            return new PaginatedResult<IbtvaProgramDetails>(items, totalItems, pageNumber, pageSize);
        }

        public async Task<Dictionary<string, int>> GetStatusSummaryAsync(List<int> unitLocationIds)
        {
            return await _context.IbtvaProgramDetails
                .Where(p => unitLocationIds.Contains(p.UnitLocationId))
                .GroupBy(p => p.FormStatus)
                .Select(g => new { Status = g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.Status, x => x.Count);
        }
    }
}
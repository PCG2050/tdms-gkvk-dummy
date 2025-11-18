using Application.Interface.Repository.DataTables;
using Application.Models;
using Domain.Entities.GenericTables;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.Publication_Repo
{
    public class PublicationRepository : IPublicationRepository
    {
        private readonly TdmsDbContext _context;

        public PublicationRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public IQueryable<Publication> GetQueryable()
        {
            return _context.Publications.AsQueryable();
        }

        public async Task<List<Publication>> GetAllAsync()
        {
            return await _context.Publications
               .Include(p => p.Category)
               .Include(p => p.Mode)
               .Include(p => p.Region)
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
                .Include(p => p.PublisherDetails)
                .Include(p => p.ExtensionLiteratures)
                .Include(p => p.PublicationKannadaNewsPapers)
                .Include(p => p.PublicationEnglishNewsPapers)
                .Include(p => p.PublicationKannadaMagazines)
                .Include(p => p.PublicationEnglishMagazines)
                .AsSplitQuery()
               .ToListAsync();
        }

        public async Task<Publication?> GetByIdAsync(int id)
        {
            return await _context.Publications.FindAsync(id);
        }

        public async Task<Publication?> GetWithDetailsAsync(int id)
        {
            return await _context.Publications
                .Include(p => p.Category)
                .Include(p => p.Mode)
                .Include(p => p.Region)
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
                .Include(p => p.PublisherDetails)
                .Include(p => p.ExtensionLiteratures)
                .Include(p => p.PublicationKannadaNewsPapers)
                .Include(p => p.PublicationEnglishNewsPapers)
                .Include(p => p.PublicationKannadaMagazines)
                .Include(p => p.PublicationEnglishMagazines)
                .AsSplitQuery()
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Publication> CreateAsync(Publication publication)
        {
            _context.Publications.Add(publication);
            await _context.SaveChangesAsync();
            return await GetWithDetailsAsync(publication.Id) ?? publication;
        }

        public async Task<Publication> UpdateAsync(Publication publication)
        {
            //_context.Publications.Update(publication);
            await _context.SaveChangesAsync();
            return publication;
        }

        public async Task DeleteAsync(int id)
        {
            var publication = await _context.Publications.FindAsync(id);
            if (publication != null)
            {
                _context.Publications.Remove(publication);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<PaginatedResult<Publication>> GetPaginatedAsync(
            List<int> unitLocationIds,
            int pageNumber = 1,
            int pageSize = 10,
            DateOnly? startDate = null,
            DateOnly? endDate = null,
            int? categoryId = null,
            string? searchTerm = null,
            int? createdById = null)
        {
            var query = _context.Publications
                .Include(p => p.Category)
                .Include(p => p.Mode)
                .Include(p => p.Region)
                .Include(p => p.Source)
                .Include(p => p.UnitLocation)
                    .ThenInclude(ul => ul.Unit)
                .Include(p => p.UnitLocation)
                    .ThenInclude(ul => ul.District)
                        .ThenInclude(d => d.State)
                .Include(p => p.Organization)
                .Include(p => p.CreatedBy)
                .Include(p => p.ApprovedBy)
                .Where(p => unitLocationIds.Contains(p.UnitLocationId))
                .AsQueryable();

            // Apply filters
            if (createdById.HasValue)
            {
                query = query.Where(p => p.CreatedById == createdById.Value);
            }
            if (startDate.HasValue)
                query = query.Where(p => p.StartDate >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(p => p.EndDate <= endDate.Value);

            if (categoryId.HasValue)
                query = query.Where(p => p.CategoryId == categoryId.Value);

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(p =>
                    p.Title != null && p.Title.Contains(searchTerm) ||
                    p.PublicationTitle != null && p.PublicationTitle.Contains(searchTerm) ||
                    p.PublicationJournalTitle != null && p.PublicationJournalTitle.Contains(searchTerm));
            }

            var totalCount = await query.CountAsync();
            var items = await query
                .OrderByDescending(p => p.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedResult<Publication>(items, totalCount, pageNumber, pageSize);
        }

        public async Task<PaginatedResult<Publication>> GetByStatusAsync(
            List<int> unitLocationIds,
            string status,
            int pageNumber = 1,
            int pageSize = 10,
            int? createdById = null)
        {
            var query = _context.Publications
                .Include(p => p.Category)
                .Include(p => p.Mode)
                .Include(p => p.Region)
                .Include(p => p.Source)
                .Include(p => p.UnitLocation)
                    .ThenInclude(ul => ul.Unit)
                .Include(p => p.UnitLocation)
                    .ThenInclude(ul => ul.District)
                .Include(p => p.Organization)
                .Include(p => p.CreatedBy)
                .Include(p => p.ApprovedBy)
                .Where(p => unitLocationIds.Contains(p.UnitLocationId) &&
                           p.FormStatus.ToLower() == status.ToLower());

            if (createdById.HasValue)
            {
                query = query.Where(p => p.CreatedById == createdById.Value);
            }

            var totalCount = await query.CountAsync();
            var items = await query
                .OrderByDescending(p => p.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedResult<Publication>(items, totalCount, pageNumber, pageSize);
        }

        public async Task<Dictionary<string, int>> GetStatusSummaryAsync(List<int> unitLocationIds, int? createdById = null)
        {
            var query = _context.Publications
        .Where(p => unitLocationIds.Contains(p.UnitLocationId));

            // ✅ ADD CREATEDBY FILTER FOR TRAINERS
            if (createdById.HasValue)
            {
                query = query.Where(p => p.CreatedById == createdById.Value);
            }

            var summary = await query
                .GroupBy(p => p.FormStatus)
                .Select(g => new { FormStatus = g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.FormStatus, x => x.Count);

            // Ensure all statuses are present
            var allStatuses = new[] { "Draft", "Pending", "Approved", "Rejected" };
            foreach (var status in allStatuses)
            {
                if (!summary.ContainsKey(status))
                    summary[status] = 0;
            }

            return summary;
        }

        // Many-to-many relationship management for newspapers and magazines
        public async Task UpdatePublicationNewspapersAndMagazinesAsync(
            int publicationId,
            List<int>? kannadaNewsPaperIds,
            List<int>? englishNewsPaperIds,
            List<int>? kannadaMagazineIds,
            List<int>? englishMagazineIds)
        {
            // Remove existing relationships
            var existingKannadaNewsPapers = _context.Set<Domain.Entities.Junction.PublicationKannadaNewsPaper>()
                .Where(x => x.PublicationId == publicationId);
            var existingEnglishNewsPapers = _context.Set<Domain.Entities.Junction.PublicationEnglishNewsPaper>()
                .Where(x => x.PublicationId == publicationId);
            var existingKannadaMagazines = _context.Set<Domain.Entities.Junction.PublicationKannadaMagazine>()
                .Where(x => x.PublicationId == publicationId);
            var existingEnglishMagazines = _context.Set<Domain.Entities.Junction.PublicationEnglishMagazine>()
                .Where(x => x.PublicationId == publicationId);

            _context.Set<Domain.Entities.Junction.PublicationKannadaNewsPaper>().RemoveRange(existingKannadaNewsPapers);
            _context.Set<Domain.Entities.Junction.PublicationEnglishNewsPaper>().RemoveRange(existingEnglishNewsPapers);
            _context.Set<Domain.Entities.Junction.PublicationKannadaMagazine>().RemoveRange(existingKannadaMagazines);
            _context.Set<Domain.Entities.Junction.PublicationEnglishMagazine>().RemoveRange(existingEnglishMagazines);

            // Add new relationships
            if (kannadaNewsPaperIds != null && kannadaNewsPaperIds.Any())
            {
                foreach (var id in kannadaNewsPaperIds)
                {
                    _context.Set<Domain.Entities.Junction.PublicationKannadaNewsPaper>().Add(
                        new Domain.Entities.Junction.PublicationKannadaNewsPaper
                        {
                            PublicationId = publicationId,
                            KannadaNewsPaperId = id,
                            CreatedAt = DateTimeOffset.UtcNow
                        });
                }
            }

            if (englishNewsPaperIds != null && englishNewsPaperIds.Any())
            {
                foreach (var id in englishNewsPaperIds)
                {
                    _context.Set<Domain.Entities.Junction.PublicationEnglishNewsPaper>().Add(
                        new Domain.Entities.Junction.PublicationEnglishNewsPaper
                        {
                            PublicationId = publicationId,
                            EnglishNewsPaperId = id,
                            CreatedAt = DateTimeOffset.UtcNow
                        });
                }
            }

            if (kannadaMagazineIds != null && kannadaMagazineIds.Any())
            {
                foreach (var id in kannadaMagazineIds)
                {
                    _context.Set<Domain.Entities.Junction.PublicationKannadaMagazine>().Add(
                        new Domain.Entities.Junction.PublicationKannadaMagazine
                        {
                            PublicationId = publicationId,
                            KannadaMagazineId = id,
                            CreatedAt = DateTimeOffset.UtcNow
                        });
                }
            }

            if (englishMagazineIds != null && englishMagazineIds.Any())
            {
                foreach (var id in englishMagazineIds)
                {
                    _context.Set<Domain.Entities.Junction.PublicationEnglishMagazine>().Add(
                        new Domain.Entities.Junction.PublicationEnglishMagazine
                        {
                            PublicationId = publicationId,
                            EnglishMagazineId = id,
                            CreatedAt = DateTimeOffset.UtcNow
                        });
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
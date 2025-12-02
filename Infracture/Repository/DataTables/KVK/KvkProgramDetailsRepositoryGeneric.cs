using Application.Interface.Repository.DataTables.KVK;
using Domain.Entities.KVK;
using Infrastructure.DbContext;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.KVK
{
    /// <summary>
    /// KVK Program Details Repository using generic base
    /// Only ~70 lines vs ~300 lines in old implementation!
    /// Includes KVK-specific Results entity in queries
    /// </summary>
    public class KvkProgramDetailsRepository : GenericProgramRepository<KvkProgramDetails>, IKvkProgramDetailsRepository
    {
        public KvkProgramDetailsRepository(TdmsDbContext context) : base(context)
        {
        }

        // ============================
        // 1. PROVIDE DBSET
        // ============================
        protected override DbSet<KvkProgramDetails> DbSet => Context.KvkProgramDetails;

        // ============================
        // 2. INCLUDE CHILD COLLECTIONS FOR GetAllAsync()
        // ============================
        protected override IQueryable<KvkProgramDetails> IncludeChildren(IQueryable<KvkProgramDetails> query)
        {
            return query
                // Standard child collections (same as other units)
                .Include(p => p.ProgramContent!)
                    .ThenInclude(pc => pc.ResourcePersons)
                .Include(p => p.ProgramContent!)
                    .ThenInclude(pc => pc.TopicsCovered)
                .Include(p => p.ProgramContent!)
                    .ThenInclude(pc => pc.TeachingAids)
                .Include(p => p.AdvisoryServices)
                .Include(p => p.Reports)
                .Include(p => p.Recommendations)
                .Include(p => p.ParticipantDemographics)

                // ⭐ KVK-SPECIFIC: Include Results (with nested children)
                .Include(p => p.Results!)
                    .ThenInclude(r => r.FldResults)  // Field results
                .Include(p => p.Results!)
                    .ThenInclude(r => r.OftResults); // On-farm trial results
        }

        // ============================
        // 3. INCLUDE MASTER DATA + CHILDREN FOR GetWithDetailsAsync()
        // ============================
        protected override IQueryable<KvkProgramDetails> IncludeDetails(IQueryable<KvkProgramDetails> query)
        {
            return base.IncludeDetails(query)  // Get common includes from base (UnitLocation, Organization, User tracking)

                // Master data foreign keys
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

                // Child collections
                .Include(p => p.ParticipantDemographics)
                .Include(p => p.ProgramContent!)
                    .ThenInclude(pc => pc.ResourcePersons)
                .Include(p => p.ProgramContent!)
                    .ThenInclude(pc => pc.TopicsCovered)
                .Include(p => p.ProgramContent!)
                    .ThenInclude(pc => pc.TeachingAids)
                .Include(p => p.AdvisoryServices)
                .Include(p => p.Reports)
                .Include(p => p.Recommendations)

                // ⭐ KVK-SPECIFIC: Include Results with nested children
                .Include(p => p.Results!)
                    .ThenInclude(r => r.FldResults)  // Field demonstration results
                .Include(p => p.Results!)
                    .ThenInclude(r => r.OftResults); // On-farm trial results
        }

        // ============================
        // 4. KVK-SPECIFIC SEARCH LOGIC
        // ============================
        protected override IQueryable<KvkProgramDetails> ApplySearchFilter(
            IQueryable<KvkProgramDetails> query,
            string searchTerm)
        {
            var lowerSearch = searchTerm.ToLower();
            return query.Where(p =>
                (p.Title != null && p.Title.ToLower().Contains(lowerSearch)) ||
                (p.Location != null && p.Location.ToLower().Contains(lowerSearch)) ||
                (p.OrganizerInstitutionName != null && p.OrganizerInstitutionName.ToLower().Contains(lowerSearch)));
        }

        // ============================
        // 5. PROGRAM TYPE FILTERING
        // ============================
        protected override IQueryable<KvkProgramDetails> ApplyProgramTypeFilter(
            IQueryable<KvkProgramDetails> query,
            int programTypeId)
        {
            return query.Where(p => p.ProgramTypeId == programTypeId);
        }

        // ============================
        // OPTIONAL: KVK-SPECIFIC METHODS
        // ============================
        // Add any KVK-specific query methods here if needed
        // Example:
        /*
        public async Task<List<KvkProgramDetails>> GetProgramsWithResultsAsync(int unitLocationId)
        {
            return await DbSet
                .Where(p => p.UnitLocationId == unitLocationId && p.Results != null)
                .Include(p => p.Results!)
                    .ThenInclude(r => r.FldResults)
                .Include(p => p.Results!)
                    .ThenInclude(r => r.OftResults)
                .ToListAsync();
        }
        */
    }
}

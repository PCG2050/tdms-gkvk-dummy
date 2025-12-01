using Application.Interface.Repository;
using Domain.Entities.FTI;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.FTI
{
    /// <summary>
    /// FTI Program Details Repository using generic base
    /// Only needs to specify FTI-specific configurations
    /// </summary>
    public class FtiProgramDetailsRepositoryGeneric : GenericProgramRepository<FtiProgramDetails>
    {
        public FtiProgramDetailsRepositoryGeneric(TdmsDbContext context) : base(context)
        {
        }

        // Provide the DbSet for FTI programs
        protected override DbSet<FtiProgramDetails> DbSet => Context.FtiProgramDetails;

        // Include FTI-specific child collections
        protected override IQueryable<FtiProgramDetails> IncludeChildren(IQueryable<FtiProgramDetails> query)
        {
            return query
                .Include(p => p.ProgramContent!)
                    .ThenInclude(pc => pc.ResourcePersons)
                .Include(p => p.ProgramContent!)
                    .ThenInclude(pc => pc.TopicsCovered)
                .Include(p => p.ProgramContent!)
                    .ThenInclude(pc => pc.TeachingAids)
                .Include(p => p.AdvisoryServices)
                .Include(p => p.Reports)
                .Include(p => p.Recommendations)
                .Include(p => p.ParticipantDemographics);
        }

        // Include FTI-specific master data and child entities
        protected override IQueryable<FtiProgramDetails> IncludeDetails(IQueryable<FtiProgramDetails> query)
        {
            return base.IncludeDetails(query)
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
                .Include(p => p.ParticipantDemographics)
                .Include(p => p.ProgramContent!)
                    .ThenInclude(pc => pc.ResourcePersons)
                .Include(p => p.ProgramContent!)
                    .ThenInclude(pc => pc.TopicsCovered)
                .Include(p => p.ProgramContent!)
                    .ThenInclude(pc => pc.TeachingAids)
                .Include(p => p.AdvisoryServices)
                .Include(p => p.Reports)
                .Include(p => p.Recommendations);
        }

        // FTI-specific search logic
        protected override IQueryable<FtiProgramDetails> ApplySearchFilter(IQueryable<FtiProgramDetails> query, string searchTerm)
        {
            var lowerSearch = searchTerm.ToLower();
            return query.Where(p =>
                (p.Title != null && p.Title.ToLower().Contains(lowerSearch)) ||
                (p.Location != null && p.Location.ToLower().Contains(lowerSearch)));
        }

        // FTI-specific program type filtering
        protected override IQueryable<FtiProgramDetails> ApplyProgramTypeFilter(IQueryable<FtiProgramDetails> query, int programTypeId)
        {
            return query.Where(p => p.ProgramTypeId == programTypeId);
        }
    }
}

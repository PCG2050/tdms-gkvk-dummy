// StuProgramDetailsRepository.cs
using Application.Interface.Repository.DataTables.STU;
using Domain.Entities.STU;
using Infrastructure.DbContext;
using Infrastructure.Repository.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.STU
{
    /// <summary>
    /// Repository implementation for StuProgramDetails.
    /// Extends the generic base repository and adds STU-specific query logic.
    /// </summary>
    public class StuProgramDetailsRepository : BaseReportEntryRepository<StuProgramDetails>, IStuProgramDetailsRepository
    {
        public StuProgramDetailsRepository(TdmsDbContext context) : base(context)
        {
        }

        // ============================
        // OVERRIDE: Entity-Specific Includes
        // ============================

        /// <summary>
        /// Adds STU-specific navigation properties to the query
        /// </summary>
        protected override IQueryable<StuProgramDetails> ApplyEntityIncludes(IQueryable<StuProgramDetails> query)
        {
            return query
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

        /// <summary>
        /// Implements search filtering for STU-specific fields
        /// </summary>
        protected override IQueryable<StuProgramDetails> ApplySearchFilter(IQueryable<StuProgramDetails> query, string searchTerm)
        {
            var lowerSearch = searchTerm.ToLower();
            return query.Where(p =>
                p.Title != null && p.Title.ToLower().Contains(lowerSearch) ||
                p.Location != null && p.Location.ToLower().Contains(lowerSearch));
        }
    }
}
using Domain.Entities.GenericProgram;

namespace Domain.Entities.KVK
{
    /// <summary>
    /// KVK Program Details using generic base
    /// Inherits all common program fields from ProgramDetailsBase
    /// Adds KVK-specific Results property
    /// </summary>
    public class KvkProgramDetails : ProgramDetailsBase<
        KvkProgramContentAndResources,  // TContent
        KvkParticipantDemographics,     // TDemographics
        KvkAdvisoryServices,            // TAdvisory
        KvkReport,                      // TReport
        KvkRecommendation>              // TRecommendation
    {
        // ============================
        // KVK-SPECIFIC FIELD
        // ============================
        // This is the ONLY difference between KVK and other units!
        // All other 40+ fields are inherited from ProgramDetailsBase

        /// <summary>
        /// KVK-specific Results entity
        /// Other units (FTI, STU, ATIC, etc.) don't have this
        /// </summary>
        public KvkResult? Results { get; set; }

        // That's it! Everything else is inherited:
        // - ProgramTypeId, CategoryId, ThemeId, etc. (40+ fields)
        // - ParticipantDemographics collection
        // - ProgramContent collection
        // - AdvisoryServices, Recommendations, Reports
    }
}

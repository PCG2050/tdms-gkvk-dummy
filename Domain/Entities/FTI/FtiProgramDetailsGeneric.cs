using Domain.Entities.GenericProgram;

namespace Domain.Entities.FTI
{
    /// <summary>
    /// FTI Program Details using generic base
    /// Inherits all common program fields from ProgramDetailsBase
    /// Only FTI-specific fields should be added here
    /// </summary>
    public class FtiProgramDetailsGeneric : ProgramDetailsBase<
        FtiProgramContentAndResources,  // TContent
        FtiParticipantDemographics,     // TDemographics
        FtiAdvisoryServices,            // TAdvisory
        FtiReport,                      // TReport
        FtiRecommendation>              // TRecommendation
    {
        // Currently no FTI-specific fields
        // All common fields are inherited from ProgramDetailsBase

        // If FTI has unique fields not shared by other units, add them here
        // For example:
        // public string? FtiSpecificField { get; set; }
    }
}

// IStuProgramDetailsRepository.cs

using Application.Interface.Repository.Common;

namespace Application.Interface.Repository.DataTables.STU
{
    /// <summary>
    /// Repository interface for StuProgramDetails.
    /// Extends the generic base repository with STU-specific methods.
    /// </summary>
    public interface IStuProgramDetailsRepository : IBaseReportEntryRepository<StuProgramDetails>
    {
        // Add any STU-specific repository methods here
        // For now, all required methods are inherited from IBaseReportEntryRepository
    }
}
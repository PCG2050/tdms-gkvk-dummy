using Application.Models.ComprehensiveReports;
using Application.Services;

namespace Application.Interface.Services.Reports
{
    /// <summary>
    /// Service for generating comprehensive multi-step reports with structured data
    /// for all report types (Program, Publication, Awards, Consultancy, Services, Financial)
    /// </summary>
    public interface IComprehensiveReportService
    {
        /// <summary>
        /// Generate comprehensive multi-step report with all sections
        /// </summary>
        /// <param name="request">Report request with unit location(s), month, year, and report type</param>
        /// <returns>Comprehensive report data with structured sections</returns>
        Task<ServiceResult<ComprehensiveReportResponse>> GenerateReportAsync(
            ComprehensiveReportRequest request);

        /// <summary>
        /// Generate comprehensive report for multiple unit locations (multi-unit report)
        /// </summary>
        /// <param name="request">Report request with multiple unit location IDs</param>
        /// <returns>Aggregated comprehensive report data across all unit locations</returns>
        Task<ServiceResult<ComprehensiveReportResponse>> GenerateMultiUnitReportAsync(
            ComprehensiveReportRequest request);

        /// <summary>
        /// Get available report types for a specific unit
        /// </summary>
        /// <param name="unitId">Unit ID to check available report types</param>
        /// <returns>List of available report type keys</returns>
        Task<ServiceResult<List<string>>> GetAvailableReportTypesAsync(int unitId);

        /// <summary>
        /// Get a preview of the report (limited rows per section)
        /// </summary>
        /// <param name="request">Report request</param>
        /// <param name="maxRowsPerSection">Maximum rows to return per section (default: 5)</param>
        /// <returns>Preview of comprehensive report data</returns>
        Task<ServiceResult<ComprehensiveReportResponse>> PreviewReportAsync(
            ComprehensiveReportRequest request,
            int maxRowsPerSection = 5);
    }
}

using Application.Models;
using Application.Services;

namespace Application.Interface.Services.Reports
{
    public interface IDynamicReportService
    {
        /// <summary>
        /// Generate a dynamic report with selected sections and columns
        /// </summary>
        /// <param name="request">Report request with selected sections and columns</param>
        /// <param name="previewMode">If true, limits rows for preview</param>
        /// <param name="maxRowsPerSection">Maximum rows per section (for preview)</param>
        /// <returns>Dynamic report data</returns>
        Task<ServiceResult<DynamicReportData>> GenerateDynamicReportAsync(
            DynamicReportRequest request,
            bool previewMode = false,
            int maxRowsPerSection = int.MaxValue);
    }
}

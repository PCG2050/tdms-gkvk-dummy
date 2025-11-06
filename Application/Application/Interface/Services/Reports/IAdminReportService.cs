using Application.Models;
using Application.Models.Reports;
using System.Threading.Tasks;

namespace Application.Interface.Services.Reports
{
    public interface IAdminReportService
    {
        /// <summary>
        /// Get filter options (units, locations, years) for report generation
        /// </summary>
        Task<ServiceResult<ReportFilterOptionsDto>> GetFilterOptionsAsync();

        /// <summary>
        /// Generate comprehensive admin report with all approved entries
        /// </summary>
        Task<ServiceResult<AdminReportResponseDto>> GenerateReportAsync(AdminReportFilterDto filter);
    }
}
using Application.Interface.Services.Reports;
using Application.Models;
using Application.Models.Reports;
using Application.Services.Reports;
using Domain.Entities.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApi.Controllers.Reports
{
    [ApiController]
    [Route("api/admin/reports")]
    [Authorize(Roles = $"{RoleString.Admin},{RoleString.UnitHead}")]
    public class AdminReportController : ControllerBase
    {
        private readonly IAdminReportService _service;
        private readonly IDynamicReportService _dynamicReportService;

        public AdminReportController(
            IAdminReportService service,
            IDynamicReportService dynamicReportService)
        {
            _service = service;
            _dynamicReportService = dynamicReportService;
        }

        // ========================================
        // EXISTING ENDPOINTS (Legacy)
        // ========================================

        /// <summary>
        /// Get filter options (units, locations, years)
        /// </summary>
        [HttpGet("filter-options")]        
        public async Task<IActionResult> GetFilterOptions()
        {
            var result = await _service.GetFilterOptionsAsync();
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

        /// <summary>
        /// Generate report (returns JSON for Angular to display)
        /// LEGACY: Use /dynamic/generate for new dynamic reports
        /// </summary>
        [HttpPost("generate")]
        public async Task<IActionResult> GenerateReport([FromBody] AdminReportFilterDto filter)
        {
            var result = await _service.GenerateReportAsync(filter);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

        // ========================================
        // NEW DYNAMIC REPORT ENDPOINTS
        // ========================================

        /// <summary>
        /// Get report configuration (available sections and columns) for a unit
        /// GET /api/admin/reports/dynamic/configuration?unitId=3
        /// </summary>
        [HttpGet("dynamic/configuration")]
        public IActionResult GetReportConfiguration([FromQuery] int unitId)
        {
            var configuration = ReportColumnRegistry.GetConfigurationForUnit(unitId);
            return Ok(configuration);
        }

        /// <summary>
        /// Generate dynamic report with selected sections and columns
        /// POST /api/admin/reports/dynamic/generate
        /// </summary>
        [HttpPost("dynamic/generate")]
        public async Task<IActionResult> GenerateDynamicReport([FromBody] DynamicReportRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (request.Sections == null || !request.Sections.Any())
                return BadRequest(new { message = "At least one section must be selected" });

            var result = await _dynamicReportService.GenerateDynamicReportAsync(request);

            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(result.Data);
        }

        /// <summary>
        /// Preview report data (first 5 rows per section) for quick review
        /// POST /api/admin/reports/dynamic/preview
        /// </summary>
        [HttpPost("dynamic/preview")]
        public async Task<IActionResult> PreviewDynamicReport([FromBody] DynamicReportRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Generate preview with limited rows
            var result = await _dynamicReportService.GenerateDynamicReportAsync(request, previewMode: true, maxRowsPerSection: 5);

            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(result.Data);
        }
    }
}
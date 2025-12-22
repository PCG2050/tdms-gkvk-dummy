using Application.Interface.Services.Reports;
using Application.Models;
using Application.Models.ComprehensiveReports;
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
        private readonly IComprehensiveReportService _comprehensiveReportService;

        public AdminReportController(
            IAdminReportService service,
            IDynamicReportService dynamicReportService,
            IComprehensiveReportService comprehensiveReportService)
        {
            _service = service;
            _dynamicReportService = dynamicReportService;
            _comprehensiveReportService = comprehensiveReportService;
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

        // ========================================
        // COMPREHENSIVE REPORT ENDPOINTS
        // ========================================

        /// <summary>
        /// Generate comprehensive multi-step report with all sections
        /// POST /api/admin/reports/comprehensive/generate
        /// </summary>
        [HttpPost("comprehensive/generate")]
        public async Task<IActionResult> GenerateComprehensiveReport([FromBody] ComprehensiveReportRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (request.UnitLocationIds == null || !request.UnitLocationIds.Any())
                return BadRequest(new { message = "At least one unit location must be specified" });

            if (string.IsNullOrEmpty(request.ReportType))
                return BadRequest(new { message = "Report type is required" });

            var result = await _comprehensiveReportService.GenerateReportAsync(request);

            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(result.Data);
        }

        /// <summary>
        /// Generate comprehensive multi-unit report (aggregates data from multiple unit locations)
        /// POST /api/admin/reports/comprehensive/multi-unit
        /// </summary>
        [HttpPost("comprehensive/multi-unit")]
        public async Task<IActionResult> GenerateMultiUnitReport([FromBody] ComprehensiveReportRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (request.UnitLocationIds == null || request.UnitLocationIds.Count < 2)
                return BadRequest(new { message = "Multi-unit report requires at least 2 unit locations" });

            var result = await _comprehensiveReportService.GenerateMultiUnitReportAsync(request);

            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(result.Data);
        }

        /// <summary>
        /// Preview comprehensive report (limited rows per section)
        /// POST /api/admin/reports/comprehensive/preview
        /// </summary>
        [HttpPost("comprehensive/preview")]
        public async Task<IActionResult> PreviewComprehensiveReport([FromBody] ComprehensiveReportRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _comprehensiveReportService.PreviewReportAsync(request, maxRowsPerSection: 5);

            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(result.Data);
        }

        /// <summary>
        /// Get available report types for a specific unit
        /// GET /api/admin/reports/comprehensive/report-types?unitId=10
        /// </summary>
        [HttpGet("comprehensive/report-types")]
        public async Task<IActionResult> GetAvailableReportTypes([FromQuery] int unitId)
        {
            var result = await _comprehensiveReportService.GetAvailableReportTypesAsync(unitId);

            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(result.Data);
        }
    }
}
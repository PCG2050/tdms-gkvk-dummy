using Application.Constants;
using Application.Interface.Services.Reports;
using Application.Models.Reports;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Reports
{
    /// <summary>
    /// Controller for generating comprehensive multi-step reports
    /// with support for monthly, quarterly, and yearly periods
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/comprehensive-report")]
    public class ComprehensiveReportController : ControllerBase
    {
        private readonly IComprehensiveReportService _reportService;
        private readonly ILogger<ComprehensiveReportController> _logger;

        public ComprehensiveReportController(
            IComprehensiveReportService reportService,
            ILogger<ComprehensiveReportController> logger)
        {
            _reportService = reportService;
            _logger = logger;
        }

        /// <summary>
        /// Get report configuration for a specific unit
        /// Returns available sections and fields
        /// </summary>
        /// <param name="unitId">Unit ID to get configuration for</param>
        /// <returns>Report configuration with sections and fields</returns>
        [HttpGet("configuration")]
        [ProducesResponseType(typeof(ReportConfigurationResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetConfiguration([FromQuery] int unitId)
        {
            if (unitId <= 0)
                return BadRequest(new { message = "Invalid unit ID" });

            var result = await _reportService.GetReportConfigurationAsync(unitId);

            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(result.Data);
        }

        /// <summary>
        /// Generate comprehensive report for selected unit locations and period
        /// Supports monthly, quarterly, and yearly reports
        /// </summary>
        /// <param name="request">Report generation request</param>
        /// <returns>Comprehensive report data</returns>
        [HttpPost("generate")]
        [ProducesResponseType(typeof(ComprehensiveReportResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GenerateReport([FromBody] ComprehensiveReportRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _logger.LogInformation("Generating comprehensive report for {PeriodType} {Period} {Year}",
                request.PeriodType,
                request.Month ?? request.Quarter,
                request.Year);

            var result = await _reportService.GenerateReportAsync(request);

            if (!result.IsSuccess)
            {
                if (result.ErrorStatus == ServiceErrorStatus.FORBIDDEN)
                    return Forbid();

                return BadRequest(new { message = result.ErrorMessage });
            }

            return Ok(result.Data);
        }

        /// <summary>
        /// Generate consolidated report for all locations of a unit type
        /// Admin only - requires ADMIN role
        /// </summary>
        /// <param name="request">Report request with unit ID</param>
        /// <returns>Consolidated report across all unit locations</returns>
        [HttpPost("all-units")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(ComprehensiveReportResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GenerateAllUnitsReport([FromBody] ComprehensiveReportRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!request.UnitId.HasValue)
                return BadRequest(new { message = "UnitId is required for all-units report" });

            _logger.LogInformation("Generating all-units report for Unit {UnitId}, {PeriodType} {Period} {Year}",
                request.UnitId,
                request.PeriodType,
                request.Month ?? request.Quarter,
                request.Year);

            var result = await _reportService.GenerateReportAsync(request);

            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(result.Data);
        }

        /// <summary>
        /// Get list of available report types for a unit
        /// </summary>
        /// <param name="unitId">Unit ID to check</param>
        /// <returns>List of available report type keys</returns>
        [HttpGet("available-types")]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAvailableReportTypes([FromQuery] int unitId)
        {
            if (unitId <= 0)
                return BadRequest(new { message = "Invalid unit ID" });

            var result = await _reportService.GetAvailableReportTypesAsync(unitId);

            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(result.Data);
        }

        /// <summary>
        /// Preview report with limited rows per section
        /// Useful for testing configuration before full generation
        /// </summary>
        /// <param name="request">Report request</param>
        /// <param name="maxRows">Maximum rows per section (default: 5)</param>
        /// <returns>Preview of report with limited data</returns>
        [HttpPost("preview")]
        [ProducesResponseType(typeof(ComprehensiveReportResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> PreviewReport(
            [FromBody] ComprehensiveReportRequest request,
            [FromQuery] int maxRows = 5)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _logger.LogInformation("Generating report preview with max {MaxRows} rows per section", maxRows);

            var result = await _reportService.PreviewReportAsync(request, maxRows);

            if (!result.IsSuccess)
            {
                if (result.ErrorStatus == ServiceErrorStatus.FORBIDDEN)
                    return Forbid();

                return BadRequest(new { message = result.ErrorMessage });
            }

            return Ok(result.Data);
        }
    }
}

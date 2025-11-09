using Application.Interface.Services.Reports;
using Application.Models.Reports;
using Domain.Entities.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApi.Controllers.Reports
{
    [ApiController]
    [Route("api/admin/reports")]
    [Authorize(Roles = RoleString.Admin)]
    public class AdminReportController : ControllerBase
    {
        private readonly IAdminReportService _service;

        public AdminReportController(IAdminReportService service)
        {
            _service = service;
        }

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
        /// </summary>
        [HttpPost("generate")]
        public async Task<IActionResult> GenerateReport([FromBody] AdminReportFilterDto filter)
        {
            var result = await _service.GenerateReportAsync(filter);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }
    }
}
using Infrastructure.DbContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers.DataTables.ATIC
{
    [Route("api/[controller]")]
    [ApiController]
    public class AticReportController : ControllerBase
    {
        private readonly TdmsDbContext _context;
        public AticReportController(TdmsDbContext context)
        {
            _context = context;
        }

        // a) Sale of Inputs (Main Monthly Report)
        [HttpGet("MonthlyReports")]
        public async Task<IActionResult> GetMonthlyReports(int unitId, int year, int month)
        {
            // 1️⃣ Get raw DB data (simple filter only)
            var list = await _context.Services
                .Include(s => s.QuantityUnit)
                .Where(s => s.OtherCategory == "FarmProduce" || s.OtherCategory == "TechnologyProductProduce" )
                .OrderBy(s => s.Id)
                .ToListAsync();

            // 2️⃣ Perform projection in memory (EF-safe)
            var reports = list.Select((s, index) => new
            {
                SlNo = index + 1,
                Particulars = s.CropPlantProductName,
                Unit = s.QuantityUnit?.Name ?? "",
                Quantity = s.Number,
                Amount = s.AmountGenerated,
                Category = s.OtherCategory,
                Theme = s.OtherTheme
            }).ToList();

            return Ok(reports);
        }

        // b) Extension Activities (Theme Report)
        [HttpGet("MonthlyThemeReports")]
        public async Task<IActionResult> GetMonthlyThemeReports(int unitId, int year, int month)
        {
            // 1️⃣ Load data + Category join
            var list = await _context.Services
                .Include(s => s.Category)
                .Where(s =>
                    (s.OtherTheme == "ATIC" && s.OtherCategory == null) ||
                    s.OtherCategory == "Visitors")
                .OrderBy(s => s.Id)
                .ToListAsync();

            // 2️⃣ Project safely in memory
            var reports = list.Select((s, index) => new
            {
                SlNo = index + 1,
                Title = s.TitleOfActivityConducted,
                Quantity = s.Number,
                Category = s.OtherCategory ?? s.Category?.Name ?? "",
                Amount = s.AmountGenerated
            }).ToList();

            return Ok(reports);
        }
    }
}

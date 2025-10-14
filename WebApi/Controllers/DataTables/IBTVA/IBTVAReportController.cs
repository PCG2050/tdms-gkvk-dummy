using Domain.Entities.MasterData;
using Infrastructure.DbContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers.DataTables.IBTVA
{
    [Route("api/[controller]")]
    [ApiController]
    public class IBTVAReportController : ControllerBase
    {
        private readonly TdmsDbContext _context;

        public IBTVAReportController(TdmsDbContext context)
        {
            _context = context;
        }

        // =======================================================
        // SINGLE ENDPOINT: Combined Monthly Report
        // =======================================================
        [HttpGet("MonthlyReport")]
        public async Task<IActionResult> GetMonthlyReport(int unitId, int year, int month)
        {
            // ---------- (A) TRAINING PROGRAMMES ----------
            var trainingPrograms = await _context.IbtvaProgramDetails
                .Where(p => p.CreatedById == 81 &&
                            p.StartDate.HasValue && p.StartDate.Value.Year == year &&
                            p.StartDate.HasValue && p.StartDate.Value.Month == month)
                .Select(p => new
                {
                    p.Id,
                    p.Title,
                    Date = (p.StartDate.HasValue && p.EndDate.HasValue && p.StartDate.Value != DateTime.MinValue && p.EndDate.Value != DateTime.MinValue)
                        ? $"{p.StartDate.Value:dd-MM-yyyy} to {p.EndDate.Value:dd-MM-yyyy}"
                        : (p.StartDate.HasValue ? p.StartDate.Value.ToString("dd-MM-yyyy") : ""),
                    p.Duration,
                    // 🟢 Fetch participants from related IbtvaParticipantDemographics table
                    NoOfParticipants = _context.IbtvaParticipantDemographics
                             .Where(d => d.IbtvaProgramDetailsId == p.Id )
                             .Sum(d => d.Total ?? 0),                   


                    Category = !string.IsNullOrEmpty(p.CategoryOther)
                        ? p.CategoryOther
                        : (p.Category != null ? p.Category.Name : "Training Programmes")
                })
                .OrderBy(p => p.Id)
                .ToListAsync();

            // ---------- (B) SERVICES (Bakery & Value-Added Products) ----------
            // 1. Define the start and end as DateTimeOffset (as you did)
            var startDateTime = new DateTimeOffset(year, month, 1, 0, 0, 0, TimeSpan.Zero);
            var endDateTime = startDateTime.AddMonths(1);

            // 2. CONVERT these to DateOnly for the comparison (This is the critical step)
            // Use the ToDateOnly() method on the DateTimeOffset.Date property to get a DateOnly object.
            var startDateOnly = DateOnly.FromDateTime(startDateTime.Date);
            var endDateOnly = DateOnly.FromDateTime(endDateTime.Date);

            var serviceData = await _context.Services
              .Include(s => s.Theme)
              .Where(s => s.CreatedById == 81 &&
                          s.Component == "IBT&VA" &&
                          s.Date >= startDateOnly &&
                          s.Date < endDateOnly)
              .Select(s => new
              {
                  Theme = s.OtherTheme ?? s.Theme.Name,
                  Product = s.CropPlantProductName,
                  Quantity = s.Number,
                  Amount = s.AmountGenerated
              })
              .ToListAsync();

           

            var production = serviceData
                .GroupBy(s => s.Theme)
                .Select(g => new
                {
                    Theme = g.Key,
                    Items = g.Select(x => new
                    {
                        x.Product,
                        x.Quantity,
                        x.Amount
                    }).ToList(),
                    TotalAmount = g.Sum(x => x.Amount)
                })
                .OrderBy(g => g.Theme)
                .ToList();

            // ---------- (C) SUMMARY ----------
            var totalParticipants = trainingPrograms.Sum(p => p.NoOfParticipants);
            var totalProductionValue = production.Sum(p => p.TotalAmount);

            // ---------- (D) FINAL COMBINED RESULT ----------
            return Ok(new
            {
                UnitId = unitId,
                UnitName = "Institute of Baking Technology and Value Addition (IBT & VA)",
                TrainingPrograms = trainingPrograms,
                Production = production,
                Summary = new
                {
                    TotalParticipants = totalParticipants,
                    TotalProductionValue = totalProductionValue
                }
            });
        }

        // -------------------------------------------
        // 2️⃣ Theme Summary (optional)
        // -------------------------------------------
        [HttpGet("MonthlyThemeReports")]
        public async Task<IActionResult> GetMonthlyThemeReports(int unitId, int year, int month)
        {
            var programs = await _context.IbtvaProgramDetails
                .Include(p => p.Category)
                .Where(p => p.CreatedById == 81 &&
                            p.StartDate.HasValue && p.StartDate.Value.Year == year &&
                            p.StartDate.HasValue && p.StartDate.Value.Month == month)
                .ToListAsync();

            var grouped = programs
                .GroupBy(p => p.Category?.Name ?? p.CategoryOther ?? "Other")
                .Select(g => new
                {
                    Category = g.Key,
                    CompletedPrograms = g.Count(p => p.EndDate.HasValue && p.EndDate.Value < DateTime.Now),
                    OngoingPrograms = g.Count(p => p.StartDate.HasValue && p.StartDate.Value <= DateTime.Now &&
                                                   (!p.EndDate.HasValue || p.EndDate.Value == default || p.EndDate.Value >= DateTime.Now)),
                    StartedPrograms = g.Count(p => p.StartDate.HasValue && p.StartDate.Value.Month == month),
                    //TotalParticipants = g.Sum(p => p.Reports !=null ? p.Reports.Count : 0),
                    UnitId = unitId
                })
                .OrderBy(x => x.Category)
                .ToList();

            return Ok(grouped);
        }
    }
}

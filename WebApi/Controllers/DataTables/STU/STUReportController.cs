using Infrastructure.DbContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace WebApi.Controllers.DataTables.STU
{
    [Route("api/[controller]")]
    [ApiController]
    public class STUReportController : ControllerBase
    {
        private readonly TdmsDbContext _context;
        public STUReportController(TdmsDbContext context)
        {
            _context = context;
        }

        [HttpGet("AllReports")]
        public async Task<ActionResult<IEnumerable<object>>> GetAllSTUReports()
        {
            var query = from prog in _context.STUProgramDetails
                        join theme in _context.Themes
                        on prog.Theme equals theme.Id
                        join demo in _context.STUParticipantDemographics
                        on prog.Id equals demo.STUProgramDetailsID
                        select new
                        {
                            prog.BatchNo,
                            prog.TPNo,
                            Theme = theme.Name,
                            StartDate = prog.StartDate, 
                            Participants = demo.Total
                        };

            var reports = await query
                .OrderBy(x => x.StartDate)
                .ToListAsync();

            // Format the result
            var result = reports.Select((r, index) => new
            {
                SlNo = index + 1,
                Week = r.StartDate.HasValue
                        ? System.Globalization.CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(
                            r.StartDate.Value,
                            System.Globalization.CalendarWeekRule.FirstFourDayWeek,
                            DayOfWeek.Monday)
                        : (int?)null, // null if StartDate is null
                Date = r.StartDate.HasValue
                        ? r.StartDate.Value.ToString("dd-MM-yyyy")
                        : string.Empty,
                NoOfParticipants = r.Participants,
                Batch = r.BatchNo,
                TPNo = r.TPNo,
                Theme = r.Theme
            });

            return Ok(result);
        }

        [HttpGet("Last3MonthsThemeReportByLocation")]
        public async Task<IActionResult> GetLast3MonthsLocationReport()
        {
            var today = DateTime.UtcNow;

            // Start date: 3 months ago from the start of the current month
            var startDate = new DateTime(today.Year, today.Month, 1).AddMonths(-2);
            var endDate = new DateTime(today.Year, today.Month,
                                       DateTime.DaysInMonth(today.Year, today.Month));

            // Load participants
            var participants = await _context.STUParticipantDemographics.ToListAsync();

            // Load programs in the last 3 months
            var programs = await _context.STUProgramDetails
                .Where(p => p.StartDate <= endDate)
                .Select(p => new
                {
                    p.Id,
                    p.StartDate,
                    p.EndDate,
                    p.Location
                })
                .ToListAsync();

            var programWithParticipants = programs.Select(p => new
            {
                p.Id,
                p.StartDate,
                p.EndDate,
                p.Location,
                TotalParticipants = participants
                    .Where(d => d.STUProgramDetailsID == p.Id)
                    .Sum(d => d.Total ?? 0)
            }).ToList();

            // Group by location only
            var groupedByLocation = programWithParticipants
                .GroupBy(p => p.Location)
                .Select(g => new
                {
                    Location = g.Key,
                    CompletedBatches = g.Count(p => p.EndDate != null && p.EndDate < startDate),
                    OngoingBatches = g.Count(p => p.EndDate != null &&
                                                  p.EndDate >= startDate &&
                                                  p.EndDate <= endDate),
                    StartedBatches = g.Count(p => p.StartDate >= startDate && p.StartDate <= endDate),
                    TotalParticipants = g
                        .Where(p => p.StartDate <= endDate &&
                                    (p.EndDate == null || p.EndDate >= startDate))
                        .Sum(p => p.TotalParticipants)
                })
                .OrderBy(r => r.Location)
                .ToList();

            return Ok(groupedByLocation);
        }


    }
}

public class MonthWiseLocationReportDto
{
    public string Month { get; set; }
    public string Location { get; set; }
    public int CompletedBatches { get; set; }
    public int OngoingBatches { get; set; }
    public int StartedBatches { get; set; }
    public int TotalParticipants { get; set; }
}




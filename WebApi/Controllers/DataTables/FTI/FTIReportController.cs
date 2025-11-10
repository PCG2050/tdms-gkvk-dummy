//using Infrastructure.DbContext;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace WebApi.Controllers.DataTables.FTI
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class FTIReportController : ControllerBase
//    {
//        private readonly TdmsDbContext _context;
//        public FTIReportController(TdmsDbContext context)
//        {
//            _context = context;
//        }
//        // Main monthly report (no OrgUnitId filter)
//        [HttpGet("MonthlyReports")]
//        public async Task<ActionResult<IEnumerable<object>>> GetMonthlyReports(int unitId, int year, int month)
//        {
//            // ✅ unitId is just passed along; you can map it on Angular side
//            var query = from prog in _context.FTIProgramDetails
//                        join demo in _context.FTIParticipantDemographics
//                            on prog.Id equals demo.FTIProgramDetailsID
//                        join cat in _context.ProgramCategories
//                            on prog.CategoryId equals cat.Id into catJoin
//                        from cat in catJoin.DefaultIfEmpty()
//                        join collab in _context.Collaborators
//                            on prog.CollaboratorId equals collab.Id into collabJoin
//                        from collab in collabJoin.DefaultIfEmpty()
//                        where prog.StartDate.HasValue &&
//                              prog.StartDate.Value.Year == year &&
//                              prog.StartDate.Value.Month == month
//                        select new
//                        {
//                            prog.Id,
//                            prog.StartDate,
//                            prog.EndDate,
//                            prog.Title,
//                            prog.Duration,
//                            Participants = demo.Total,
//                            Category = prog.CategoryId == 37 ? prog.CategoryOther : cat.Name,
//                            Collaboration = collab.Name == "Other" ? prog.CollaboratorOther : collab.Name
//                        };

//            var reports = await query
//                .GroupBy(x => x.Id)
//                .Select(g => new
//                {
//                    ProgramId = g.Key,
//                    StartDate = g.First().StartDate,
//                    EndDate = g.First().EndDate,
//                    g.First().Title,
//                    g.First().Duration,
//                    NoOfTrainees = g.Sum(x => x.Participants ?? 0),
//                    g.First().Category,
//                    g.First().Collaboration
//                })
//                .OrderBy(x => x.StartDate)
//                .ToListAsync();

//            var result = reports.Select((r, index) => new
//            {
//                SlNo = index + 1,
//                Date = r.StartDate.HasValue && r.EndDate.HasValue
//                    ? $"{r.StartDate.Value:dd-MM-yyyy} to {r.EndDate.Value:dd-MM-yyyy}"
//                    : string.Empty,
//                r.Title,
//                r.Duration,
//                NoOfTrainees = r.NoOfTrainees,
//                r.Category,
//                r.Collaboration,
//                UnitId = unitId // ✅ keep unitId so frontend knows what it asked for
//            });

//            return Ok(result);
//        }

//        // Monthly summary grouped by Category/Collaboration
//        [HttpGet("MonthlyThemeReports")]
//        public async Task<IActionResult> GetMonthlyThemeReports(int unitId, int year, int month)
//        {
//            var participants = await _context.FTIParticipantDemographics.ToListAsync();

//            var programs = await _context.FTIProgramDetails
//                .Where(p => p.StartDate.HasValue &&
//                            p.StartDate.Value.Year == year &&
//                            p.StartDate.Value.Month == month)
//                .Select(p => new
//                {
//                    p.Id,
//                    p.StartDate,
//                    p.EndDate,
//                    Category = p.CategoryId == 37 ? p.CategoryOther : p.Category.Name,
//                    Collaboration = p.Collaborator.Name == "Other"
//                        ? p.CollaboratorOther
//                        : p.Collaborator.Name
//                })
//                .ToListAsync();

//            var programWithParticipants = programs.Select(p => new
//            {
//                p.Id,
//                p.StartDate,
//                p.EndDate,
//                p.Category,
//                p.Collaboration,
//                TotalParticipants = participants
//                    .Where(d => d.FTIProgramDetailsID == p.Id)
//                    .Sum(d => d.Total ?? 0)
//            }).ToList();

//            var grouped = programWithParticipants
//                .GroupBy(p => new { p.Category, p.Collaboration })
//                .Select(g => new
//                {
//                    Category = g.Key.Category,
//                    Collaboration = g.Key.Collaboration,
//                    CompletedPrograms = g.Count(p => p.EndDate != null && p.EndDate.Value.ToDateTime(TimeOnly.MinValue) < DateTime.UtcNow),
//                    OngoingPrograms = g.Count(p => p.StartDate != null && p.StartDate.Value.ToDateTime(TimeOnly.MinValue) <= DateTime.UtcNow &&
//                                                   (p.EndDate == null || p.EndDate.Value.ToDateTime(TimeOnly.MinValue) >= DateTime.UtcNow)),
//                    StartedPrograms = g.Count(p => p.StartDate != null &&
//                                                   p.StartDate.Value.Year == year &&
//                                                   p.StartDate.Value.Month == month),
//                    TotalParticipants = g.Sum(p => p.TotalParticipants),
//                    UnitId = unitId // ✅ carry back the manual unitId
//                })
//                .OrderBy(r => r.Category)
//                .ToList();

//            return Ok(grouped);
//        }
//    }




//public class FtiReportDto
//    {
//        public int SlNo { get; set; }
//        public string Date { get; set; } = string.Empty;
//        public string Title { get; set; } = string.Empty;
//        public string Duration { get; set; } = string.Empty;
//        public int NoOfTrainees { get; set; }
//        public string Category { get; set; } = string.Empty;
//        public string Collaboration { get; set; } = string.Empty;
//    }

//    public class FtiThemeReportDto
//    {
//        public string Category { get; set; } = string.Empty;
//        public string Collaboration { get; set; } = string.Empty;
//        public int CompletedPrograms { get; set; }
//        public int OngoingPrograms { get; set; }
//        public int StartedPrograms { get; set; }
//        public int TotalParticipants { get; set; }
//    }
//}

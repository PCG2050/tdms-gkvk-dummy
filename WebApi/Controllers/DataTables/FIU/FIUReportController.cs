//using Infrastructure.DbContext;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace WebApi.Controllers.DataTables.FIU
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class FIUReportController : ControllerBase
//    {
//        private readonly TdmsDbContext _context;
//        public FIUReportController(TdmsDbContext context)
//        {
//            _context = context;
//        }

//        // ------------------------
//        // MAIN MONTHLY REPORT
//        // ------------------------
//        [HttpGet("MonthlyReports")]
//        public async Task<ActionResult<IEnumerable<object>>> GetMonthlyReports(int unitId, int year, int month)
//        {
//            var query = from prog in _context.FIUProgramActivities
//                        join act in _context.FIUActivities
//                            on prog.FIUActivitiesId equals act.Id
//                        where prog.CreatedAt.Year == year && prog.CreatedAt.Month == month
//                        group prog by new { prog.FIUActivitiesId, act.Name } into g                        
//                        select new
//                        {
//                            Activity = g.Key.Name,
//                            Total = g.Sum(x => x.Number ?? 0)
//                        };
           
//            var reports = await query
//                .OrderBy(x => x.Activity)
//                .ToListAsync();

//            var result = reports.Select((r, index) => new
//            {
//                SlNo = index + 1,
//                Activity = r.Activity,
//                No = r.Total,
//                UnitId = unitId
//            });

//            return Ok(result);
//        }
         
//        // ------------------------
//        // OTHER ACTIVITIES REPORT
//        // ------------------------
//        [HttpGet("OtherActivities")]
//        public async Task<ActionResult<IEnumerable<object>>> GetOtherActivities(int unitId, int year, int month)
//        {
//            var query = _context.FIUOtherActivities
//                .Where(o => o.CreatedAt.Year == year && o.CreatedAt.Month == month)
//                .Select(o => new
//                {
//                    o.Title,
//                    o.Description,
//                    o.UploadMediaUrl
//                });

//            var result = await query.ToListAsync();

//            return Ok(result.Select((r, index) => new
//            {
//                SlNo = index + 1,
//                r.Title,
//                r.Description,
//                r.UploadMediaUrl,
//                UnitId = unitId
//            }));
//        }
//    }

//    // ------------------------
//    // DTOs (optional)
//    // ------------------------
//    public class FiuMonthlyReportDto
//    {
//        public int SlNo { get; set; }
//        public string Activity { get; set; } = string.Empty;
//        public int? No { get; set; }
//        public int UnitId { get; set; }
//    }

//    public class FiuOtherActivityDto
//    {
//        public int SlNo { get; set; }
//        public string Title { get; set; } = string.Empty;
//        public string Description { get; set; } = string.Empty;
//        public string? UploadMediaUrl { get; set; }
//        public int UnitId { get; set; }
//    }
//}

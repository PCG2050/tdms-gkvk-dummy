//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace WebApi.Controllers.DataTables.ASM
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class ASMReportController : ControllerBase
//    {
//        private readonly TdmsDbContext _context;
//    }

//    [HttpGet("MonthlyReport")]
//    public async Task<IActionResult> GetMonthlyReport(int unitId, int year, int month)
//        {
//            var trainerId = 81;


//            var startDateOnly = new DateOnly(year, month, 1);
//            var endDateOnly = startDateOnly.AddMonths(1);

//            var visitors = await _context.ASMVisitorDetails
//                .Include(v.)
//        }
//}

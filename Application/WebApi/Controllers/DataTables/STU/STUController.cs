//using Microsoft.AspNetCore.Mvc;

//namespace WebApi.Controllers.DataTables.STU
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class STUController : ControllerBase
//    {
//        [HttpGet]
//        public ActionResult Index()
//        {
//            var routes = new Dictionary<string, string>
//            {
//                {
//                    "Training Programme Overview",
//                    Url.Action(nameof(StuTrainingProgrammeController.GetEntries),nameof(StuTrainingProgrammeController))??string.Empty
//                },
//                {
//                    "Sponsored Training Programme Overview",
//                    Url.Action(nameof(StuSponsoredTrainingProgrammeController.GetEntries))??string.Empty
//                },
//                {
//                    "Any Other Activities",
//                    Url.Action(nameof(StuOtherActivityController.GetEntries))??string.Empty
//                }
//            };
//            return Ok(routes);
//        }
//    }
//}

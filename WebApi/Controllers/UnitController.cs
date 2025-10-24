

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]
    public class UnitController : ControllerBase
    {
        private readonly IUnitService _unitService;

        public UnitController(IUnitService unitService)
        {
            _unitService = unitService;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult> GetAllUnits()
        {
            var units = await _unitService.GetMainUnitsAsync();
            return Ok(units.Select(x => new
            {
                x.Id,
                x.Name
            }));
        }
    }
}

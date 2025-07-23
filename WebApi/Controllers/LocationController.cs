using Application.Interface;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _locationService;

        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }
        [HttpGet("states")]
        public async Task<ActionResult> Index()
        {
            return Ok(await _locationService.GetAllStatesAsync());
        }
        [HttpGet("states/{stateId}/districts")]
        public async Task<ActionResult> StateDistricts(int stateId)
        {
            return Ok(await _locationService.GetStateDistrictsAsync(stateId));
        }
    }
}

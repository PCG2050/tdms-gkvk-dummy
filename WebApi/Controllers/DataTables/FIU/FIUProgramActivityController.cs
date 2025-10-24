using Application.Interface.Services.DataTables.FIU;
using Application.Models.DataTables.FIU;

namespace WebApi.Controllers.DataTables.FIU
{
    [Route("api/[controller]")]
    [ApiController]
    public class FIUProgramActivityController : ControllerBase
    {
        private readonly IFIUProgramActivityService _service;

        public FIUProgramActivityController(IFIUProgramActivityService service)
        {
            _service = service;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] FIUProgramActivityDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _service.AddAsync(dto);
            return Ok(result);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}

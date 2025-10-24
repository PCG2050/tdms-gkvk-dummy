using Application.Interface.Services.DataTables.ASM;
using Application.Models.DataTables.ASM;

namespace WebApi.Controllers.DataTables.ASM
{
    [Route("api/[controller]")]
    [ApiController]
    public class ASMVisitorDetailsController : ControllerBase
    {
        private readonly IASMVisitorDetailsService _service;

        public ASMVisitorDetailsController(IASMVisitorDetailsService service)
        {
            _service = service;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] ASMVisitorDetailsDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

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
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ASMVisitorDetailsDto dto)
        {
            var result = await _service.UpdateAsync(id, dto);
            return Ok(result);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success) return NotFound();
            return Ok(new { message = "Deleted successfully" });
        }
    }
}

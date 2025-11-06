using Application.Interface.Services.DataTables.FTI;
using Application.Models.DataTables.FTI;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.DataTables.FTI
{
    [ApiController]
    [Route("api/[controller]")]
    public class FTIProgramController : ControllerBase
    {
        private readonly IFTIService _service;

        public FTIProgramController(IFTIService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var programs = await _service.GetAllProgramsAsync();
            return Ok(programs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var program = await _service.GetProgramByIdAsync(id);
            if (program == null) return NotFound();
            return Ok(program);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FTIProgramDetailsDto dto)
        {
            var created = await _service.AddProgramAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] FTIProgramDetailsDto dto)
        {
            if (id != dto.Id) return BadRequest();
            var updated = await _service.UpdateProgramAsync(dto);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteProgramAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}

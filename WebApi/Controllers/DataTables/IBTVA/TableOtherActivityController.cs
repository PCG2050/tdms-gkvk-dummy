using Application.Interface.Services;
using Application.Interface.Services.DataTables;
using Application.Models;
using Application.Models.DataTables;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.DataTables.IBTVA
{
    [ApiController]
    [Route("api/[controller]")]
    public class TableOtherActivityController : ControllerBase
    {
        private readonly ITableOtherActivityService _service;

        public TableOtherActivityController(ITableOtherActivityService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var activities = await _service.GetAllAsync();
            return Ok(activities);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var activity = await _service.GetByIdAsync(id);
            if (activity == null) return NotFound();
            return Ok(activity);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TableOtherActivityCreateDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TableOtherActivityUpdateDto dto)
        {
            var updated = await _service.UpdateAsync(id, dto);
            if (!updated) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}

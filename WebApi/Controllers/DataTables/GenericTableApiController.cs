using Application.Interface;
using Application.Interface.Services.Common;
using Application.Models;
using Domain.Entities;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.DataTables
{
    public abstract class GenericTableApiController<TEntity, TCreateDto, TUpdateDto, TDto> : ControllerBase
    where TEntity : ReportEntryBaseEntity
    where TCreateDto : class
    where TUpdateDto : class, IUpdateDto
    where TDto : class
    {
        protected readonly IGenericTableService<TEntity, TCreateDto, TUpdateDto, TDto> _service;

        protected GenericTableApiController(IGenericTableService<TEntity, TCreateDto, TUpdateDto, TDto> service)
        {
            _service = service;
        }

        [HttpGet]
        public virtual async Task<IActionResult> GetEntries([FromQuery] PaginationRequest paginationRequest)
        {
            var result = await _service.GetPaginatedItemsAsync(paginationRequest.PageNumber, paginationRequest.PageSize);
            return Ok(result);
        }

        [HttpPost]
        public virtual async Task<IActionResult> AddEntry(TCreateDto createDto)
        {
            var result = await _service.AddAsync(createDto);

            if (!result.IsSuccess)
                return ServiceResponseToActionResult.Error(result.ErrorMessage, result.ErrorStatus);

            return Ok(result);
        }

        [HttpPatch("{id}")]
        public virtual async Task<IActionResult> UpdateEntry(int id, TUpdateDto updateDto)
        {
            // Ensure the ID in the route matches the DTO
            updateDto.Id = id;

            var result = await _service.UpdateAsync(updateDto);

            if (!result.IsSuccess)
                return ServiceResponseToActionResult.Error(result.ErrorMessage, result.ErrorStatus);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> DeleteEntry(int id)
        {
            var result = await _service.DeleteAsync(id);

            if (!result.IsSuccess)
                return ServiceResponseToActionResult.Error(result.ErrorMessage, result.ErrorStatus);

            return NoContent();
        }
    }
}

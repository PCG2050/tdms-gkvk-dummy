using Application.Models;
using Application.Models.DataTables;
using Domain.Entities;
using Domain.Entities.FTI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Services.Common
{
    public interface IGenericTableService<TEntity,TCreateDto,TUpdateDto,TDto>
        where TEntity:ReportEntryBaseEntity
        where TCreateDto:class
        where TUpdateDto:IUpdateDto
        where TDto:class
    {
        Task<ServiceResult<TEntity>> AddAsync(TCreateDto createDto);
        Task<ServiceResult<TEntity>> UpdateAsync(TUpdateDto updateDto);
        Task<ServiceResult> DeleteAsync(int id);
        Task<PaginatedResult<TDto>> GetPaginatedItemsAsync(int pageNumber, int pageSize);
    }
}

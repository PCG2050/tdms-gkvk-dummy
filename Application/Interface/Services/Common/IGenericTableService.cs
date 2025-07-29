using Application.Models;
using Application.Models.DataTables;
using Domain.Entities.FTI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Services.Common
{
    public interface IGenericTableService<T>
    {
        Task<ServiceResult<T>> AddAsync(FtiOtherActivityCreateDto createDto);
        Task<ServiceResult<T>> UpdateAsync(FtiOtherActivityUpdateDto updateDto);
        Task<ServiceResult> DeleteAsync(int id);
    }
}

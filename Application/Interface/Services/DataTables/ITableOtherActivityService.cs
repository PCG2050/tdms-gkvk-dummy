using Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Services.DataTables
{
    public interface ITableOtherActivityService
    {
        Task<List<TableOtherActivityDto>> GetAllAsync();
        Task<TableOtherActivityDto?> GetByIdAsync(int id);
        Task<TableOtherActivityDto> CreateAsync(TableOtherActivityCreateDto dto);
        Task<bool> UpdateAsync(int id, TableOtherActivityUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}

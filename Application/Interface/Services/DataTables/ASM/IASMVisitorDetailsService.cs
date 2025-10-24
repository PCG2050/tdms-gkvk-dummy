using Application.Models.DataTables.ASM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Services.DataTables.ASM
{
    public interface IASMVisitorDetailsService
    {
        Task<ASMVisitorDetailsDto> AddAsync(ASMVisitorDetailsDto dto);
        Task<IEnumerable<ASMVisitorDetailsDto>> GetAllAsync();
        Task<ASMVisitorDetailsDto?> GetByIdAsync(int id);
        Task<ASMVisitorDetailsDto> UpdateAsync(int id, ASMVisitorDetailsDto dto);
        Task<bool> DeleteAsync(int id);
    }
}

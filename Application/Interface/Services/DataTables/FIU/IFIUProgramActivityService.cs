using Application.Models.DataTables.FIU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Services.DataTables.FIU
{
    public interface IFIUProgramActivityService
    {
        Task<FIUProgramActivityDto> AddAsync(FIUProgramActivityDto dto);
        Task<IEnumerable<FIUProgramActivityDto>> GetAllAsync();
        Task<FIUProgramActivityDto?> GetByIdAsync(int id);
    }
}

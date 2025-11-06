using Application.Models.DataTables.FTI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Services.DataTables.FTI
{
    public interface IFTIService
    {
        Task<List<FTIProgramDetailsDto>> GetAllProgramsAsync();
        Task<FTIProgramDetailsDto?> GetProgramByIdAsync(int id);
        Task<FTIProgramDetailsDto> AddProgramAsync(FTIProgramDetailsDto dto);
        Task<FTIProgramDetailsDto> UpdateProgramAsync(FTIProgramDetailsDto dto);

        Task<bool> DeleteProgramAsync(int id);
    }
}

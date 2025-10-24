using Domain.Entities.FTI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Repository.DataTables.FTI
{
    public interface IFTIRepository
    {
        Task<List<FTIProgramDetails>> GetAllProgramsAsync();
        Task<FTIProgramDetails?> GetProgramByIdAsync(int id);
        Task<FTIProgramDetails> AddProgramAsync(FTIProgramDetails entity);
        Task<FTIProgramDetails> UpdateProgramAsync(FTIProgramDetails entity);
        Task<bool> DeleteProgramAsync(int id);
    }
}

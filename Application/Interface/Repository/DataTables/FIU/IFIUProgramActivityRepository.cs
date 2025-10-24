using Domain.Entities.FIU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Repository.DataTables.FIU
{
    public interface IFIUProgramActivityRepository
    {
        Task<FIUProgramActivity> AddAsync(FIUProgramActivity entity);
        Task<IEnumerable<FIUProgramActivity>> GetAllAsync();
        Task<FIUProgramActivity?> GetByIdAsync(int id);
    }
}

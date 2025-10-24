using Domain.Entities.ASM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Repository.DataTables.ASM
{
    public interface IASMVisitorDetailsRepository
    {
        Task<ASMVisitorDetails> AddAsync(ASMVisitorDetails entity);
        Task<IEnumerable<ASMVisitorDetails>> GetAllAsync();
        Task<ASMVisitorDetails?> GetByIdAsync(int id);
        Task<ASMVisitorDetails> UpdateAsync(ASMVisitorDetails entity);
        Task<bool> DeleteAsync(int id);
    }
}

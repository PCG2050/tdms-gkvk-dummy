using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Repository.DataTables.KVK
{
    public interface IKvkResultRepository
    {
        Task<KvkResult?> GetByIdAsync(int id);
        Task<KvkResult?> GetByProgramIdAsync(int programId);
        Task<KvkResult?> GetWithDetailsAsync(int id);
        Task<KvkResult> CreateAsync(KvkResult entity);
        Task<KvkResult> UpdateAsync(KvkResult entity);
        Task DeleteAsync(int id);
    }
}

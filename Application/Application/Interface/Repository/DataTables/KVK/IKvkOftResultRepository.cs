using Domain.Entities.KVK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Repository.DataTables.KVK
{
    public interface IKvkOftResultRepository
    {
        Task<KvkOftResult> CreateAsync(KvkOftResult entity);
        Task<KvkOftResult?> GetByIdAsync(int id);
        Task<List<KvkOftResult>> GetByResultIdAsync(int resultId);
        Task<KvkOftResult> UpdateAsync(KvkOftResult entity);
        Task DeleteAsync(int id);
    }
}

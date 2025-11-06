using Domain.Entities.KVK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Repository.DataTables.KVK
{
    public interface IKvkFldResultRepository
    {
        Task<KvkFldResult> CreateAsync(KvkFldResult entity);
        Task<KvkFldResult?> GetByIdAsync(int id);
        Task<List<KvkFldResult>> GetByResultIdAsync(int resultId);
        Task<KvkFldResult> UpdateAsync(KvkFldResult entity);
        Task DeleteAsync(int id);
    }
}

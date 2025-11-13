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

        /// <summary>
        /// Creates KvkResult with all child entities (FldResults and OftResults) in a single transaction
        /// </summary>
        Task<KvkResult> CreateWithChildrenAsync(
            KvkResult parent,
            List<KvkFldResult>? fldResults,
            List<KvkOftResult>? oftResults);

        /// <summary>
        /// Updates KvkResult with all child entities using Hybrid Pattern:
        /// - Items WITH Id: UPDATE existing
        /// - Items WITHOUT Id: CREATE new
        /// - Items in DB but NOT in arrays: DELETE
        /// All operations within a single transaction
        /// </summary>
        Task<KvkResult> UpdateWithChildrenAsync(
            KvkResult parent,
            List<KvkFldResult>? fldResults,
            List<KvkOftResult>? oftResults);
    }
}

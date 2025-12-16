using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Repository.DataTables.EEU
{
    public interface IEeuResultRepository
    {
        Task<EeuResult?> GetByIdAsync(int id);
        Task<EeuResult?> GetByProgramIdAsync(int programId);
        Task<EeuResult?> GetWithDetailsAsync(int id);
        Task<EeuResult> CreateAsync(EeuResult entity);
        Task<EeuResult> UpdateAsync(EeuResult entity);
        Task DeleteAsync(int id);

        /// <summary>
        /// Creates EeuResult with all child entities (FldResults and OftResults) in a single transaction
        /// </summary>
        Task<EeuResult> CreateWithChildrenAsync(
            EeuResult parent,
            List<EeuFldResult>? fldResults,
            List<EeuOftResult>? oftResults);

        /// <summary>
        /// Updates EeuResult with all child entities using Hybrid Pattern:
        /// - Items WITH Id: UPDATE existing
        /// - Items WITHOUT Id: CREATE new
        /// - Items in DB but NOT in arrays: DELETE
        /// All operations within a single transaction
        /// </summary>
        Task<EeuResult> UpdateWithChildrenAsync(
            EeuResult parent,
            List<EeuFldResult>? fldResults,
            List<EeuOftResult>? oftResults);
    }
}

using Domain.Entities.EEU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Repository.DataTables.EEU
{
    public interface IEeuFldResultRepository
    {
        Task<EeuFldResult> CreateAsync(EeuFldResult entity);
        Task<EeuFldResult?> GetByIdAsync(int id);
        Task<List<EeuFldResult>> GetByResultIdAsync(int resultId);
        Task<EeuFldResult> UpdateAsync(EeuFldResult entity);
        Task DeleteAsync(int id);
    }
}

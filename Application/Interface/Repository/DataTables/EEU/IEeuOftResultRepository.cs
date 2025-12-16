using Domain.Entities.EEU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Repository.DataTables.EEU
{
    public interface IEeuOftResultRepository
    {
        Task<EeuOftResult> CreateAsync(EeuOftResult entity);
        Task<EeuOftResult?> GetByIdAsync(int id);
        Task<List<EeuOftResult>> GetByResultIdAsync(int resultId);
        Task<EeuOftResult> UpdateAsync(EeuOftResult entity);
        Task DeleteAsync(int id);
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Repository.DataTables.ConsultSocialMedia
{
    public interface IModeAndOutreachRepository
    {
        Task<TableModeAndOutreach?> GetByIdAsync(int id);
        Task<List<TableModeAndOutreach>> GetByConsultingServiceIdAsync(int consultingServiceId);
        Task<TableModeAndOutreach> CreateAsync(TableModeAndOutreach entity);
        Task<TableModeAndOutreach> UpdateAsync(TableModeAndOutreach entity);
        Task DeleteAsync(int id);
    }
}

using Domain.Entities.GenericTables.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Repository.DataTables.TblService
{
    public interface ITableHostelRepository
    {
        Task<TableHostel?> GetTableHostelByIdAsync(int id);
        Task<List<TableHostel>> GetTableHostelsByServiceIdAsync(int serviceId);
        Task<TableHostel> CreateTableHostelAsync(TableHostel entity);
        Task<TableHostel> UpdateTableHostelAsync(TableHostel entity);
        Task DeleteTableHostelAsync(int id);
    }
}

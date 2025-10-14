using Domain.Entities.GenericTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Repository.DataTables
{
    public interface ITableOtherActivityRepository
    {
        Task<TableOtherActivity?> GetByIdAsync(int id);
        Task<List<TableOtherActivity>> GetAllAsync();
        Task AddAsync(TableOtherActivity entity);
        Task UpdateAsync(TableOtherActivity entity);
        Task DeleteAsync(TableOtherActivity entity);
        Task SaveChangesAsync();
    }
}

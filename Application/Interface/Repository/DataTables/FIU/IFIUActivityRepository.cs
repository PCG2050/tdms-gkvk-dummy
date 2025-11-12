using Domain.Entities.FIU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Repository.DataTables.FIU
{

    public interface IFIUActivityRepository
    {
        Task<List<FIUActivity>> GetAllActiveAsync();
        Task<FIUActivity?> GetByIdAsync(int id);
        Task<List<FIUActivity>> GetByCategoryAsync(string category);
    }
}

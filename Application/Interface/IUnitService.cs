using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IUnitService
    {
        Task<IEnumerable<Unit>> GetMainUnitsAsync();
        Task<IEnumerable<Unit>> GetUnitsByIds(ICollection<int> ids);
    }
}

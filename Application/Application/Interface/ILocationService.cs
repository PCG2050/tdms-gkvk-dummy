using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface ILocationService
    {
        Task<IEnumerable<State>> GetAllStatesAsync();
        Task<IEnumerable<District>> GetStateDistrictsAsync(int stateId);
    }
}

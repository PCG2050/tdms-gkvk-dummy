using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Repository
{
    public interface IStateRepository
    {
        Task<IEnumerable<State>> GetAllStatesAsync();
        Task<State?> GetStateAsync(int id);
    }
}

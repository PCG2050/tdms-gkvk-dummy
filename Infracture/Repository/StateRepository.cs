using Application.Interface.Repository;
using Domain.Entities;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class StateRepository : IStateRepository
    {
        private readonly TdmsDbContext _context;

        public StateRepository(TdmsDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<State>> GetAllStatesAsync()
        {
            return await _context.States.ToListAsync();
        }

        public async Task<State?> GetStateAsync(int id)
        {
            return await _context.States.FindAsync(id);
        }
    }
    public class DistrictRepository : IDistrictRepository
    {
        private readonly TdmsDbContext _context;

        public DistrictRepository(TdmsDbContext context)
        {
            _context = context;
        }
        public async Task<District?> GetDistrict(int id)
        {
            return await _context.Districts.FindAsync(id);
        }

        public async Task<IEnumerable<District>> GetStateDistrictsAsync(int stateId)
        {
            return await _context.Districts.Where(d=> d.StateId == stateId).ToListAsync();
        }
    }
}

using Application.Interface;
using Application.Interface.Repository;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class LocationService : ILocationService
    {
        private readonly IStateRepository _stateRepository;
        private readonly IDistrictRepository _districtRepository;

        public LocationService(IStateRepository stateRepository, IDistrictRepository districtRepository)
        {
            _stateRepository = stateRepository;
            _districtRepository = districtRepository;
        }
        public async Task<IEnumerable<State>> GetAllStatesAsync()
        {
            return await _stateRepository.GetAllStatesAsync();
        }

        public async Task<IEnumerable<District>> GetStateDistrictsAsync(int stateId)
        {
            return await _districtRepository.GetStateDistrictsAsync(stateId);
        }
    }
}

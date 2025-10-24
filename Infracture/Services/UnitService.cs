

namespace Infrastructure.Services
{
    public class UnitService : IUnitService
    {
        private readonly IUnitRepository _unitRepository;

        public UnitService(IUnitRepository unitRepository)
        {
            _unitRepository = unitRepository;
        }

        public async Task<IEnumerable<Unit>> GetMainUnitsAsync()
        {
            return await _unitRepository.GetAllMainUnitsAsync();
        }

        public async Task<IEnumerable<Unit>> GetUnitsByIds(ICollection<int> ids)
        {
            return await _unitRepository.GetUnitByIdsAsync(ids);
        }
    }
}

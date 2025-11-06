using Domain.Entities;

namespace Application.Interface.Repository
{
    public interface IDistrictRepository
    {
        Task<IEnumerable<District>> GetStateDistrictsAsync(int stateId);
        Task<District?> GetDistrict(int id);
    }
}

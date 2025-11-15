// IFTIAdvisoryServicesRepository.cs
using Domain.Entities.FTI;

namespace Application.Interface.Repository.DataTables.FTI
{
    public interface IFTIAdvisoryServicesRepository
    {
        Task<FTIAdvisoryServices?> GetByIdAsync(int id);
        Task<FTIAdvisoryServices?> GetByProgramIdAsync(int programId);
        Task<FTIAdvisoryServices> CreateAsync(FTIAdvisoryServices entity);
        Task<FTIAdvisoryServices> UpdateAsync(FTIAdvisoryServices entity);
        Task DeleteAsync(int id);
    }
}

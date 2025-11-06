// IIbtvaAdvisoryServicesRepository.cs
using Domain.Entities.IBTVA;

namespace Application.Interface.Repository.DataTables.IBTVA
{
    public interface IIbtvaAdvisoryServicesRepository
    {
        Task<IbtvaAdvisoryServices?> GetByIdAsync(int id);
        Task<IbtvaAdvisoryServices?> GetByProgramIdAsync(int programId);
        Task<IbtvaAdvisoryServices> CreateAsync(IbtvaAdvisoryServices entity);
        Task<IbtvaAdvisoryServices> UpdateAsync(IbtvaAdvisoryServices entity);
        Task DeleteAsync(int id);
    }
}
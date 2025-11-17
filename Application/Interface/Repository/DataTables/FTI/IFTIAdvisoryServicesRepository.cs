// IFtiAdvisoryServicesRepository.cs
using Domain.Entities.DEU;
using Domain.Entities.FTI;

namespace Application.Interface.Repository.DataTables.FTI
{
    public interface IFtiAdvisoryServicesRepository
    {
        Task<FtiAdvisoryServices?> GetByIdAsync(int id);
        Task<FtiAdvisoryServices?> GetByProgramIdAsync(int programId);
        Task<FtiAdvisoryServices> CreateAsync(FtiAdvisoryServices entity);
        Task<FtiAdvisoryServices> UpdateAsync(FtiAdvisoryServices entity);
        Task DeleteAsync(int id);
    }
}

// INaepAdvisoryServicesRepository.cs
using Domain.Entities.NAEP;

namespace Application.Interface.Repository.DataTables.NAEP
{
    public interface INaepAdvisoryServicesRepository
    {
        Task<NaepAdvisoryServices?> GetByIdAsync(int id);
        Task<NaepAdvisoryServices?> GetByProgramIdAsync(int programId);
        Task<NaepAdvisoryServices> CreateAsync(NaepAdvisoryServices entity);
        Task<NaepAdvisoryServices> UpdateAsync(NaepAdvisoryServices entity);
        Task DeleteAsync(int id);
    }
}
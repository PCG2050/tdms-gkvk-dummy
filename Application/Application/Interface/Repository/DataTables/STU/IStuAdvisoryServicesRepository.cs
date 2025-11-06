// IStuAdvisoryServicesRepository.cs
using Domain.Entities.STU;

namespace Application.Interface.Repository.DataTables.STU
{
    public interface IStuAdvisoryServicesRepository
    {
        Task<StuAdvisoryServices?> GetByIdAsync(int id);
        Task<StuAdvisoryServices?> GetByProgramIdAsync(int programId);
        Task<StuAdvisoryServices> CreateAsync(StuAdvisoryServices entity);
        Task<StuAdvisoryServices> UpdateAsync(StuAdvisoryServices entity);
        Task DeleteAsync(int id);
    }
}
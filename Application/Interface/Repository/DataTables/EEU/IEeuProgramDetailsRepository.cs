// IEeuProgramDetailsRepository.cs

using Domain.Entities.ASM;
using Domain.Entities.EEU;

namespace Application.Interface.Repository.DataTables.EEU
{
    public interface IEeuProgramDetailsRepository
    {
        IQueryable<EeuProgramDetails> GetQueryable();
        Task<List<EeuProgramDetails>> GetAllAsync();
        Task<EeuProgramDetails?> GetByIdAsync(int id);
        Task<EeuProgramDetails?> GetWithDetailsAsync(int id);
        Task<EeuProgramDetails> CreateAsync(EeuProgramDetails entity);
        Task<EeuProgramDetails> UpdateAsync(EeuProgramDetails entity);
        Task DeleteAsync(int id);
        Task<List<EeuProgramDetails>> GetByCreatedByIdAsync(int createdById);
        Task<List<EeuProgramDetails>> GetByUnitLocationIdsAsync(List<int> unitLocationIds);

        Task<Dictionary<string, int>> GetStatusSummaryAsync(List<int> unitLocationIds);
    }
}
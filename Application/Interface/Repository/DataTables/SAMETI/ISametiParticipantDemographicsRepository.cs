
using Domain.Entities.SAMETI;
namespace Application.Interface.Repository.DataTables.SAMETI
{
    public interface ISametiParticipantDemographicsRepository
    {
        Task<SametiParticipantDemographics?> GetByIdAsync(int id);
        Task<List<SametiParticipantDemographics>> GetByProgramIdAsync(int programId);
        Task<SametiParticipantDemographics> CreateAsync(SametiParticipantDemographics entity);
        Task<SametiParticipantDemographics> UpdateAsync(SametiParticipantDemographics entity);
        Task DeleteAsync(int id);
    }
}
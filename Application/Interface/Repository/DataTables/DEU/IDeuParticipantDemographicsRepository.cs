

namespace Application.Interface.Repository.DataTables.DEU
{
    public interface IDeuParticipantDemographicsRepository
    {
        Task<DeuParticipantDemographics?> GetByIdAsync(int id);
        Task<List<DeuParticipantDemographics>> GetByProgramIdAsync(int programId);
        Task<DeuParticipantDemographics> CreateAsync(DeuParticipantDemographics entity);
        Task<DeuParticipantDemographics> UpdateAsync(DeuParticipantDemographics entity);
        Task DeleteAsync(int id);
    }
}
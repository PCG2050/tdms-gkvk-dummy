

namespace Application.Interface.Repository.DataTables.EEU
{
    public interface IEeuParticipantDemographicsRepository
    {
        Task<EeuParticipantDemographics?> GetByIdAsync(int id);
        Task<List<EeuParticipantDemographics>> GetByProgramIdAsync(int programId);
        Task<EeuParticipantDemographics> CreateAsync(EeuParticipantDemographics entity);
        Task<EeuParticipantDemographics> UpdateAsync(EeuParticipantDemographics entity);
        Task DeleteAsync(int id);
    }
}
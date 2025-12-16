

using Domain.Entities.EEU;

namespace Application.Interface.Repository.DataTables.EEU
{
    public interface IEeuParticipantDemographicsRepository
    {
        Task<EeuParticipantDemographics> CreateAsync(EeuParticipantDemographics entity);
        Task<EeuParticipantDemographics?> GetByIdAsync(int id);
        Task<List<EeuParticipantDemographics>> GetByProgramIdAsync(int programId);
        Task<EeuParticipantDemographics> UpdateAsync(EeuParticipantDemographics entity);
        Task DeleteAsync(int id);
    }
}
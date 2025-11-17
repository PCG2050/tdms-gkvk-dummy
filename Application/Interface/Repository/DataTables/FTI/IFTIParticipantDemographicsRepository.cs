

using Domain.Entities.FTI;

namespace Application.Interface.Repository.DataTables.FTI
{
    public interface IFtiParticipantDemographicsRepository
    {
        Task<FtiParticipantDemographics?> GetByIdAsync(int id);
        Task<List<FtiParticipantDemographics>> GetByProgramIdAsync(int programId);
        Task<FtiParticipantDemographics> CreateAsync(FtiParticipantDemographics entity);
        Task<FtiParticipantDemographics> UpdateAsync(FtiParticipantDemographics entity);
        Task DeleteAsync(int id);
    }
}



namespace Application.Interface.Repository.DataTables.ATIC
{
    public interface IAticParticipantDemographicsRepository
    {
        Task<AticParticipantDemographics?> GetByIdAsync(int id);
        Task<List<AticParticipantDemographics>> GetByProgramIdAsync(int programId);
        Task<AticParticipantDemographics> CreateAsync(AticParticipantDemographics entity);
        Task<AticParticipantDemographics> UpdateAsync(AticParticipantDemographics entity);
        Task DeleteAsync(int id);
    }
}
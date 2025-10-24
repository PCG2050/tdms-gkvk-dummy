

namespace Application.Interface.Repository.DataTables.DEU
{
    public interface INaepParticipantDemographicsRepository
    {
        Task<NaepParticipantDemographics?> GetByIdAsync(int id);
        Task<List<NaepParticipantDemographics>> GetByProgramIdAsync(int programId);
        Task<NaepParticipantDemographics> CreateAsync(NaepParticipantDemographics entity);
        Task<NaepParticipantDemographics> UpdateAsync(NaepParticipantDemographics entity);
        Task DeleteAsync(int id);
    }
}
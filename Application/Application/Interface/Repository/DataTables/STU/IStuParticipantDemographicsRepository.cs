

namespace Application.Interface.Repository.DataTables.STU
{
    public interface IStuParticipantDemographicsRepository
    {
        Task<StuParticipantDemographics?> GetByIdAsync(int id);
        Task<List<StuParticipantDemographics>> GetByProgramIdAsync(int programId);
        Task<StuParticipantDemographics> CreateAsync(StuParticipantDemographics entity);
        Task<StuParticipantDemographics> UpdateAsync(StuParticipantDemographics entity);
        Task DeleteAsync(int id);
    }
}
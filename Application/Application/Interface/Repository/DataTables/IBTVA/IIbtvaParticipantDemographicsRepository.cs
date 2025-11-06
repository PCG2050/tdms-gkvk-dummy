

namespace Application.Interface.Repository.DataTables.IBTVA
{
    public interface IIbtvaParticipantDemographicsRepository
    {
        Task<IbtvaParticipantDemographics?> GetByIdAsync(int id);
        Task<List<IbtvaParticipantDemographics>> GetByProgramIdAsync(int programId);
        Task<IbtvaParticipantDemographics> CreateAsync(IbtvaParticipantDemographics entity);
        Task<IbtvaParticipantDemographics> UpdateAsync(IbtvaParticipantDemographics entity);
        Task DeleteAsync(int id);
    }
}
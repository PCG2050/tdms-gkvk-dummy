

namespace Application.Interface.Repository.DataTables.FTI
{
    public interface IFTIParticipantDemographicsRepository
    {
        Task<FTIParticipantDemographics?> GetByIdAsync(int id);
        Task<List<FTIParticipantDemographics>> GetByProgramIdAsync(int programId);
        Task<FTIParticipantDemographics> CreateAsync(FTIParticipantDemographics entity);
        Task<FTIParticipantDemographics> UpdateAsync(FTIParticipantDemographics entity);
        Task DeleteAsync(int id);
    }
}

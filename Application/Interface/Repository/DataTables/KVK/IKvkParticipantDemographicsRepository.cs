

using Domain.Entities.KVK;

namespace Application.Interface.Repository.DataTables.KVK
{
    public interface IKvkParticipantDemographicsRepository
    {
        Task<KvkParticipantDemographics> CreateAsync(KvkParticipantDemographics entity);
        Task<KvkParticipantDemographics?> GetByIdAsync(int id);
        Task<List<KvkParticipantDemographics>> GetByProgramIdAsync(int programId);
        Task<KvkParticipantDemographics> UpdateAsync(KvkParticipantDemographics entity);
        Task DeleteAsync(int id);
    }
}
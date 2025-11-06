// IIbtvaTeachingAidsRepository.cs
using Domain.Entities.IBTVA;

namespace Application.Interface.Repository.DataTables.IBTVA
{
    public interface IIbtvaTeachingAidsRepository
    {
        Task<IbtvaTeachingAidsDeveloped?> GetByIdAsync(int id);
        Task<List<IbtvaTeachingAidsDeveloped>> GetByContentIdAsync(int contentId);
        Task<IbtvaTeachingAidsDeveloped> CreateAsync(IbtvaTeachingAidsDeveloped entity);
        Task<IbtvaTeachingAidsDeveloped> UpdateAsync(IbtvaTeachingAidsDeveloped entity);
        Task DeleteAsync(int id);
    }
}
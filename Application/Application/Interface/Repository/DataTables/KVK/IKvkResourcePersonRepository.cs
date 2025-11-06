// IEeuResourcePersonRepository.cs
using Domain.Entities.EEU;
using Domain.Entities.KVK;

namespace Application.Interface.Repository.DataTables.KVK
{
    public interface IKvkResourcePersonRepository
    {
        Task<KvkResourcePerson> CreateAsync(KvkResourcePerson entity);
        Task<KvkResourcePerson?> GetByIdAsync(int id);
        Task<List<KvkResourcePerson>> GetByContentIdAsync(int contentId);
        Task<KvkResourcePerson> UpdateAsync(KvkResourcePerson entity);
        Task DeleteAsync(int id);
    }
}
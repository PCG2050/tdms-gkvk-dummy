


namespace Application.Interface.Repository.DataTables.ATIC
{
    public interface IAticResourcePersonRepository
    {
        Task<AticResourcePerson?> GetByIdAsync(int id);
        Task<List<AticResourcePerson>> GetByContentIdAsync(int contentId);
        Task<AticResourcePerson> CreateAsync(AticResourcePerson entity);
        Task<AticResourcePerson> UpdateAsync(AticResourcePerson entity);
        Task DeleteAsync(int id);
    }
}
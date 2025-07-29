using Domain.Entities.FTI;

namespace Application.Interface.Repository.DataTables
{
    public interface IDataTableRepositoryActions<T>
    {
        Task<T?> GetAsync(int id);
        Task<T> AddAsync(T activity);
        Task<bool> DeleteAsync(T activity);
        Task<T> UpdateAsync(T activity);
    }
}
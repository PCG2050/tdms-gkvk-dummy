using Application.Constants;
using Application.Models;
using Domain.Entities;
namespace Infrastructure.Repository
{
    public interface IGenericRepository<TEntity, TDto>
    where TEntity : ReportEntryBaseEntity
    where TDto : class
    {
        Task<TEntity> AddAsync(TEntity entity);
        Task<bool> DeleteAsync(TEntity entity);
        Task<TEntity?> GetAsync(int id);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<PaginatedResult<TDto>> GetPaginatedItemsAsync(int organizationId, int pageNumber = Constants.PAGINATION_PAGE_NUMBER_DEFAULT, QueryFilter? queryFilter = null, int pageSize = Constants.PAGINATION_PAGE_SIZE_DEFAULT);
    }
}

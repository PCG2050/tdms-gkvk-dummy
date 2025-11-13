namespace Application.Interface.Repository
{
    public interface IPagination<T>
    {
        Task<PaginatedResult<T>> GetPaginatedItemsAsync(int organizationId,int pageNumber = Constants.Constants.PAGINATION_PAGE_NUMBER_DEFAULT, QueryFilter? queryFilter = null, int pageSize = Constants.Constants.PAGINATION_PAGE_SIZE_DEFAULT);
    }
}

using Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Repository
{
    public interface IPagination<T>
    {
        Task<PaginatedResult<T>> GetPaginatedItemsAsync(int organizationId,int pageNumber = Constants.PAGINATION_PAGE_NUMBER_DEFAULT, QueryFilter? queryFilter = null, int pageSize = Constants.PAGINATION_PAGE_SIZE_DEFAULT);
    }
}

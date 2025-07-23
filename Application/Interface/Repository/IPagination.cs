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
        Task<PaginatedResult<T>> GetItemsAsync(int pageNumber = Constants.PAGINATION_PAGEN_NUMBER_DEFAULT, QueryFilter? queryFilter = null, int pageSize = Constants.PAGINATION_PAGE_SIZE_DEFAULT);
    }
}

using Application.Models;
using Application.Models.DataTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Services.Common
{
    public interface IGenericPaginationService<T>
    {
        Task<PaginatedResult<T>> GetPaginatedItemsAsync(int pageNumber, int pageSize);
    }
}

using Application.Models;
using Domain.Entities.Publications;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Repository.DataTables
{
    public interface IPublicationRepository
    {
        Task<Publication?> GetByIdAsync(int id);
        Task<Publication?> GetWithDetailsAsync(int id);
        Task<Publication> CreateAsync(Publication publication);
        Task<Publication> UpdateAsync(Publication publication);
        Task DeleteAsync(int id);
        Task<PaginatedResult<Publication>> GetPaginatedAsync(
            List<int> unitLocationIds,
            int pageNumber = 1,
            int pageSize = 10,
            DateOnly? startDate = null,
            DateOnly? endDate = null);
        Task<bool> ExistsAsync(int id);

        // Master data methods
        Task<List<Category>> GetCategoriesAsync();
        Task<List<Source>> GetSourcesAsync();
        Task<List<Mode>> GetModesAsync();
        Task<List<Region>> GetRegionsAsync();
        Task<List<ExtensionLiterature>> GetExtensionLiteraturesAsync();
    }
}

using Application.Models;
using Application.Models.DataTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Services.DataTables
{
    public interface IPublicationService
    {
        Task<ServiceResult<PublicationDto>> CreateAsync(PublicationCreateDto createDto);
        Task<ServiceResult<PublicationDto>> UpdateAsync(int id, PublicationUpdateDto updateDto);
        Task<ServiceResult> DeleteAsync(int id);
        Task<ServiceResult<PublicationDto>> GetByIdAsync(int id);
        Task<ServiceResult<PaginatedResult<PublicationDto>>> GetPaginatedAsync(
            int? unitLocationId = null,
            int pageNumber = 1,
            int pageSize = 10,
            DateOnly? startDate = null,
            DateOnly? endDate = null);

        // Master data endpoints
        Task<ServiceResult<List<CategoryDto>>> GetCategoriesAsync();
        Task<ServiceResult<List<SourceDto>>> GetSourcesAsync();
        Task<ServiceResult<List<ModeDto>>> GetModesAsync();
        Task<ServiceResult<List<RegionDto>>> GetRegionsAsync();
        Task<ServiceResult<List<ExtensionLiteratureDto>>> GetExtensionLiteraturesAsync();
    }
}

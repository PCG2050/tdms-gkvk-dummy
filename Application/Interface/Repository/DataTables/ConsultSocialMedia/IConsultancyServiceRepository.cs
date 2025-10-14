using Application.Models;
using Domain.Entities.GenericTables.ConsultingAndSocialMediaService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Repository.DataTables.ConsultSocialMedia
{
    public interface IConsultingServiceRepository
    {
        Task<ConsultingAndSocialMediaService?> GetByIdAsync(int id);
        Task<ConsultingAndSocialMediaService?> GetWithDetailsAsync(int id);
        Task<ConsultingAndSocialMediaService> CreateAsync(ConsultingAndSocialMediaService entity);
        Task<ConsultingAndSocialMediaService> UpdateAsync(ConsultingAndSocialMediaService entity);
        Task DeleteAsync(int id);

        Task<PaginatedResult<ConsultingAndSocialMediaService>> GetPaginatedAsync(
            List<int> unitLocationIds,
            int pageNumber = 1,
            int pageSize = 10,
            DateOnly? startDate = null,
            DateOnly? endDate = null,
            int? categoryId = null,
            string? searchTerm = null);

        Task<PaginatedResult<ConsultingAndSocialMediaService>> GetByStatusAsync(
            List<int> unitLocationIds,
            string status,
            int pageNumber = 1,
            int pageSize = 10);

        Task<Dictionary<string, int>> GetStatusSummaryAsync(List<int> unitLocationIds);
    }
}

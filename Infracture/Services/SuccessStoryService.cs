using Application.Interface.Repository.DataTables.STU;
using Application.Interface.Repository.DataTables.NAEP;
using Application.Interface.Repository.DataTables.FTI;
using Application.Interface.Repository.DataTables.DEU;
using Application.Interface.Repository.DataTables.EEU;
using Application.Interface.Repository.DataTables.ATIC;
using Application.Interface.Repository.DataTables.IBTVA;
using Application.Interface.Repository.DataTables.KVK;
using Application.Interface.Services;
using Application.Models;
using Application.Services.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class SuccessStoryService : ISuccessStoryService
    {
        private readonly IStuProgramDetailsRepository _stuRepository;
        private readonly INaepProgramDetailsRepository _naepRepository;
        private readonly IFtiProgramDetailsRepository _ftiRepository;
        private readonly IDeuProgramDetailsRepository _deuRepository;
        private readonly IEeuProgramDetailsRepository _eeuRepository;
        private readonly IAticProgramDetailsRepository _aticRepository;
        private readonly IIbtvaProgramDetailsRepository _ibtvaRepository;
        private readonly IKvkProgramDetailsRepository _kvkRepository;

        public SuccessStoryService(
            IStuProgramDetailsRepository stuRepository,
            INaepProgramDetailsRepository naepRepository,
            IFtiProgramDetailsRepository ftiRepository,
            IDeuProgramDetailsRepository deuRepository,
            IEeuProgramDetailsRepository eeuRepository,
            IAticProgramDetailsRepository aticRepository,
            IIbtvaProgramDetailsRepository ibtvaRepository,
            IKvkProgramDetailsRepository kvkRepository)
        {
            _stuRepository = stuRepository;
            _naepRepository = naepRepository;
            _ftiRepository = ftiRepository;
            _deuRepository = deuRepository;
            _eeuRepository = eeuRepository;
            _aticRepository = aticRepository;
            _ibtvaRepository = ibtvaRepository;
            _kvkRepository = kvkRepository;
        }

        public async Task<PaginatedResult<SuccessStoryDto>> GetSuccessStoriesAsync(
            DateOnly? startDate = null,
            DateOnly? endDate = null,
            int pageNumber = 1,
            int pageSize = 20)
        {
            var allStories = new List<SuccessStoryDto>();

            // Get success stories from all programs
            var stuStories = await GetStuSuccessStoriesAsync(startDate, endDate);
            allStories.AddRange(stuStories);

            var naepStories = await GetNaepSuccessStoriesAsync(startDate, endDate);
            allStories.AddRange(naepStories);

            var ftiStories = await GetFtiSuccessStoriesAsync(startDate, endDate);
            allStories.AddRange(ftiStories);

            var deuStories = await GetDeuSuccessStoriesAsync(startDate, endDate);
            allStories.AddRange(deuStories);

            var eeuStories = await GetEeuSuccessStoriesAsync(startDate, endDate);
            allStories.AddRange(eeuStories);

            var aticStories = await GetAticSuccessStoriesAsync(startDate, endDate);
            allStories.AddRange(aticStories);

            var ibtvaStories = await GetIbtvaSuccessStoriesAsync(startDate, endDate);
            allStories.AddRange(ibtvaStories);

            var kvkStories = await GetKvkSuccessStoriesAsync(startDate, endDate);
            allStories.AddRange(kvkStories);

            // Order by date descending and paginate
            var totalCount = allStories.Count;
            var paginatedStories = allStories
                .OrderByDescending(x => x.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new PaginatedResult<SuccessStoryDto>(
                paginatedStories,
                totalCount,
                pageNumber,
                pageSize);
        }

        private async Task<List<SuccessStoryDto>> GetStuSuccessStoriesAsync(
            DateOnly? startDate,
            DateOnly? endDate)
        {
            var query = _stuRepository.GetQueryable()
                .Include(x => x.Recommendations)
                .Include(x => x.CreatedBy)
                .Include(x => x.UnitLocation)
                    .ThenInclude(ul => ul.Unit)
                .Where(x => x.FormStatus == "Approved")
                .Where(x => x.Recommendations != null && !string.IsNullOrEmpty(x.Recommendations.SuccessStories));

            if (startDate.HasValue)
                query = query.Where(x => x.StartDate >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(x => x.EndDate <= endDate.Value);

            var programs = await query.ToListAsync();

            return programs
                .Where(p => p.Recommendations != null && !string.IsNullOrEmpty(p.Recommendations.SuccessStories))
                .Select(p => new SuccessStoryDto
                {
                    Id = p.Id,
                    ProgramType = "STU",
                    Title = p.Title ?? "STU Program",
                    StartDate = p.StartDate,
                    EndDate = p.EndDate,
                    UserId = p.CreatedById ?? 0,
                    UserName = $"{p.CreatedBy?.FirstName} {p.CreatedBy?.LastName}".Trim(),
                    ProfileImageUrl = p.CreatedBy?.ProfileImageUrl,
                    Department = p.UnitLocation?.Unit?.Name ?? "Unknown",
                    Position = p.CreatedBy?.Role.ToString() ?? "Unknown",
                    SuccessStoryContent = p.Recommendations!.SuccessStories!,
                    CreatedAt = p.CreatedAt
                }).ToList();
        }

        private async Task<List<SuccessStoryDto>> GetNaepSuccessStoriesAsync(
            DateOnly? startDate,
            DateOnly? endDate)
        {
            var query = _naepRepository.GetQueryable()
                .Include(x => x.Recommendations)
                .Include(x => x.CreatedBy)
                .Include(x => x.UnitLocation)
                    .ThenInclude(ul => ul.Unit)
                .Where(x => x.FormStatus == "Approved")
                .Where(x => x.Recommendations != null && !string.IsNullOrEmpty(x.Recommendations.SuccessStories));

            if (startDate.HasValue)
                query = query.Where(x => x.StartDate >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(x => x.EndDate <= endDate.Value);

            var programs = await query.ToListAsync();

            return programs
                .Where(p => p.Recommendations != null && !string.IsNullOrEmpty(p.Recommendations.SuccessStories))
                .Select(p => new SuccessStoryDto
                {
                    Id = p.Id,
                    ProgramType = "NAEP",
                    Title = p.Title ?? "NAEP Program",
                    StartDate = p.StartDate,
                    EndDate = p.EndDate,
                    UserId = p.CreatedById ?? 0,
                    UserName = $"{p.CreatedBy?.FirstName} {p.CreatedBy?.LastName}".Trim(),
                    ProfileImageUrl = p.CreatedBy?.ProfileImageUrl,
                    Department = p.UnitLocation?.Unit?.Name ?? "Unknown",
                    Position = p.CreatedBy?.Role.ToString() ?? "Unknown",
                    SuccessStoryContent = p.Recommendations!.SuccessStories!,
                    CreatedAt = p.CreatedAt
                }).ToList();
        }

        private async Task<List<SuccessStoryDto>> GetFtiSuccessStoriesAsync(
            DateOnly? startDate,
            DateOnly? endDate)
        {
            var query = _ftiRepository.GetQueryable()
                .Include(x => x.Recommendations)
                .Include(x => x.CreatedBy)
                .Include(x => x.UnitLocation)
                    .ThenInclude(ul => ul.Unit)
                .Where(x => x.FormStatus == "Approved")
                .Where(x => x.Recommendations != null && !string.IsNullOrEmpty(x.Recommendations.SuccessStories));

            if (startDate.HasValue)
                query = query.Where(x => x.StartDate >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(x => x.EndDate <= endDate.Value);

            var programs = await query.ToListAsync();

            return programs
                .Where(p => p.Recommendations != null && !string.IsNullOrEmpty(p.Recommendations.SuccessStories))
                .Select(p => new SuccessStoryDto
                {
                    Id = p.Id,
                    ProgramType = "FTI",
                    Title = p.Title ?? "FTI Program",
                    StartDate = p.StartDate,
                    EndDate = p.EndDate,
                    UserId = p.CreatedById ?? 0,
                    UserName = $"{p.CreatedBy?.FirstName} {p.CreatedBy?.LastName}".Trim(),
                    ProfileImageUrl = p.CreatedBy?.ProfileImageUrl,
                    Department = p.UnitLocation?.Unit?.Name ?? "Unknown",
                    Position = p.CreatedBy?.Role.ToString() ?? "Unknown",
                    SuccessStoryContent = p.Recommendations!.SuccessStories!,
                    CreatedAt = p.CreatedAt
                }).ToList();
        }

        private async Task<List<SuccessStoryDto>> GetDeuSuccessStoriesAsync(
            DateOnly? startDate,
            DateOnly? endDate)
        {
            var query = _deuRepository.GetQueryable()
                .Include(x => x.Recommendations)
                .Include(x => x.CreatedBy)
                .Include(x => x.UnitLocation)
                    .ThenInclude(ul => ul.Unit)
                .Where(x => x.FormStatus == "Approved")
                .Where(x => x.Recommendations != null && !string.IsNullOrEmpty(x.Recommendations.SuccessStories));

            if (startDate.HasValue)
                query = query.Where(x => x.StartDate >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(x => x.EndDate <= endDate.Value);

            var programs = await query.ToListAsync();

            return programs
                .Where(p => p.Recommendations != null && !string.IsNullOrEmpty(p.Recommendations.SuccessStories))
                .Select(p => new SuccessStoryDto
                {
                    Id = p.Id,
                    ProgramType = "DEU",
                    Title = p.Title ?? "DEU Program",
                    StartDate = p.StartDate,
                    EndDate = p.EndDate,
                    UserId = p.CreatedById ?? 0,
                    UserName = $"{p.CreatedBy?.FirstName} {p.CreatedBy?.LastName}".Trim(),
                    ProfileImageUrl = p.CreatedBy?.ProfileImageUrl,
                    Department = p.UnitLocation?.Unit?.Name ?? "Unknown",
                    Position = p.CreatedBy?.Role.ToString() ?? "Unknown",
                    SuccessStoryContent = p.Recommendations!.SuccessStories!,
                    CreatedAt = p.CreatedAt
                }).ToList();
        }

        private async Task<List<SuccessStoryDto>> GetEeuSuccessStoriesAsync(
            DateOnly? startDate,
            DateOnly? endDate)
        {
            var query = _eeuRepository.GetQueryable()
                .Include(x => x.Recommendations)
                .Include(x => x.CreatedBy)
                .Include(x => x.UnitLocation)
                    .ThenInclude(ul => ul.Unit)
                .Where(x => x.FormStatus == "Approved")
                .Where(x => x.Recommendations != null && !string.IsNullOrEmpty(x.Recommendations.SuccessStories));

            if (startDate.HasValue)
                query = query.Where(x => x.StartDate >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(x => x.EndDate <= endDate.Value);

            var programs = await query.ToListAsync();

            return programs
                .Where(p => p.Recommendations != null && !string.IsNullOrEmpty(p.Recommendations.SuccessStories))
                .Select(p => new SuccessStoryDto
                {
                    Id = p.Id,
                    ProgramType = "EEU",
                    Title = p.Title ?? "EEU Program",
                    StartDate = p.StartDate,
                    EndDate = p.EndDate,
                    UserId = p.CreatedById ?? 0,
                    UserName = $"{p.CreatedBy?.FirstName} {p.CreatedBy?.LastName}".Trim(),
                    ProfileImageUrl = p.CreatedBy?.ProfileImageUrl,
                    Department = p.UnitLocation?.Unit?.Name ?? "Unknown",
                    Position = p.CreatedBy?.Role.ToString() ?? "Unknown",
                    SuccessStoryContent = p.Recommendations!.SuccessStories!,
                    CreatedAt = p.CreatedAt
                }).ToList();
        }

        private async Task<List<SuccessStoryDto>> GetAticSuccessStoriesAsync(
            DateOnly? startDate,
            DateOnly? endDate)
        {
            var query = _aticRepository.GetQueryable()
                .Include(x => x.Recommendations)
                .Include(x => x.CreatedBy)
                .Include(x => x.UnitLocation)
                    .ThenInclude(ul => ul.Unit)
                .Where(x => x.FormStatus == "Approved")
                .Where(x => x.Recommendations != null && !string.IsNullOrEmpty(x.Recommendations.SuccessStories));

            if (startDate.HasValue)
                query = query.Where(x => x.StartDate >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(x => x.EndDate <= endDate.Value);

            var programs = await query.ToListAsync();

            return programs
                .Where(p => p.Recommendations != null && !string.IsNullOrEmpty(p.Recommendations.SuccessStories))
                .Select(p => new SuccessStoryDto
                {
                    Id = p.Id,
                    ProgramType = "ATIC",
                    Title = p.Title ?? "ATIC Program",
                    StartDate = p.StartDate,
                    EndDate = p.EndDate,
                    UserId = p.CreatedById ?? 0,
                    UserName = $"{p.CreatedBy?.FirstName} {p.CreatedBy?.LastName}".Trim(),
                    ProfileImageUrl = p.CreatedBy?.ProfileImageUrl,
                    Department = p.UnitLocation?.Unit?.Name ?? "Unknown",
                    Position = p.CreatedBy?.Role.ToString() ?? "Unknown",
                    SuccessStoryContent = p.Recommendations!.SuccessStories!,
                    CreatedAt = p.CreatedAt
                }).ToList();
        }

        private async Task<List<SuccessStoryDto>> GetIbtvaSuccessStoriesAsync(
            DateOnly? startDate,
            DateOnly? endDate)
        {
            var query = _ibtvaRepository.GetQueryable()
                .Include(x => x.Recommendations)
                .Include(x => x.CreatedBy)
                .Include(x => x.UnitLocation)
                    .ThenInclude(ul => ul.Unit)
                .Where(x => x.FormStatus == "Approved")
                .Where(x => x.Recommendations != null && !string.IsNullOrEmpty(x.Recommendations.SuccessStories));

            if (startDate.HasValue)
                query = query.Where(x => x.StartDate >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(x => x.EndDate <= endDate.Value);

            var programs = await query.ToListAsync();

            return programs
                .Where(p => p.Recommendations != null && !string.IsNullOrEmpty(p.Recommendations.SuccessStories))
                .Select(p => new SuccessStoryDto
                {
                    Id = p.Id,
                    ProgramType = "IBTVA",
                    Title = p.Title ?? "IBTVA Program",
                    StartDate = p.StartDate,
                    EndDate = p.EndDate,
                    UserId = p.CreatedById ?? 0,
                    UserName = $"{p.CreatedBy?.FirstName} {p.CreatedBy?.LastName}".Trim(),
                    ProfileImageUrl = p.CreatedBy?.ProfileImageUrl,
                    Department = p.UnitLocation?.Unit?.Name ?? "Unknown",
                    Position = p.CreatedBy?.Role.ToString() ?? "Unknown",
                    SuccessStoryContent = p.Recommendations!.SuccessStories!,
                    CreatedAt = p.CreatedAt
                }).ToList();
        }

        private async Task<List<SuccessStoryDto>> GetKvkSuccessStoriesAsync(
            DateOnly? startDate,
            DateOnly? endDate)
        {
            var query = _kvkRepository.GetQueryable()
                .Include(x => x.Recommendations)
                .Include(x => x.CreatedBy)
                .Include(x => x.UnitLocation)
                    .ThenInclude(ul => ul.Unit)
                .Where(x => x.FormStatus == "Approved")
                .Where(x => x.Recommendations != null && !string.IsNullOrEmpty(x.Recommendations.SuccessStories));

            if (startDate.HasValue)
                query = query.Where(x => x.StartDate >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(x => x.EndDate <= endDate.Value);

            var programs = await query.ToListAsync();

            return programs
                .Where(p => p.Recommendations != null && !string.IsNullOrEmpty(p.Recommendations.SuccessStories))
                .Select(p => new SuccessStoryDto
                {
                    Id = p.Id,
                    ProgramType = "KVK",
                    Title = p.Title ?? "KVK Program",
                    StartDate = p.StartDate,
                    EndDate = p.EndDate,
                    UserId = p.CreatedById ?? 0,
                    UserName = $"{p.CreatedBy?.FirstName} {p.CreatedBy?.LastName}".Trim(),
                    ProfileImageUrl = p.CreatedBy?.ProfileImageUrl,
                    Department = p.UnitLocation?.Unit?.Name ?? "Unknown",
                    Position = p.CreatedBy?.Role.ToString() ?? "Unknown",
                    SuccessStoryContent = p.Recommendations!.SuccessStories!,
                    CreatedAt = p.CreatedAt
                }).ToList();
        }
    }
}

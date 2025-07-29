using Application.Interface;
using Application.Interface.Repository.DataTables;
using Application.Interface.Services.DataTables;
using Application.Models;
using Application.Models.DataTables;
using Domain.Entities.FTI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.DataTables
{
    public class FtiOtherActivityService : IFtiOtherActivityService
    {
        private readonly IFtiOtherActivitiesRepository _ftiOtherActivitiesRepository;
        private readonly ICurrentUserService _currentUserService;

        public FtiOtherActivityService(IFtiOtherActivitiesRepository ftiOtherActivitiesRepository, ICurrentUserService currentUserService)
        {
            _ftiOtherActivitiesRepository = ftiOtherActivitiesRepository;
            _currentUserService = currentUserService;
        }

        public async Task<ServiceResult<FtiOtherActivity>> AddAsync(FtiOtherActivityCreateDto createDto)
        {
            int userId = _currentUserService.UserId;
            int orgId = _currentUserService.OrganizationId;
            //validation
            var entity = new FtiOtherActivity
            {
                ActivityDetails = createDto.ActivityDetails,
                Attachements = createDto.Assets,
                CreatedById = userId,
                OrganizationId = orgId,
                UnitLocationId = createDto.UnitLocationId
            };
            await _ftiOtherActivitiesRepository.AddAsync(entity);
            return ServiceResult<FtiOtherActivity>.Success(entity);
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var activity = await _ftiOtherActivitiesRepository.GetAsync(id);
            if (activity is null) return ServiceResult.Failure("Activity not found", ServiceErrorStatus.NOTFOUND);
            await _ftiOtherActivitiesRepository.DeleteAsync(activity);
            return ServiceResult.Success();
        }

        public Task<PaginatedResult<FtiOtherActivityDto>> GetPaginatedItemsAsync(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<FtiOtherActivity>> UpdateAsync(FitOtherActivityUpdateDto updateDto)
        {
            throw new NotImplementedException();
        }
    }
}

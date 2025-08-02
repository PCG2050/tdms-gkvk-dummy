using Application.Interface;
using Application.Interface.Repository.DataTables;
using Application.Interface.Services.Common;
using Application.Interface.Services.DataTables;
using Application.Models.DataTables;
using Domain.Entities.ATIC;

namespace Infrastructure.Services.DataTables
{
    public class AticAdvisoryServiceService : GenericTableService<AticAdvisoryService, AticAdvisoryServiceCreateDto, AticAdvisoryServiceUpdateDto, AticAdvisoryServiceDto>, IAticAdvisoryServiceService
    {
        public AticAdvisoryServiceService(IAticAdvisoryServiceRepository repository, ICurrentUserService currentUserService, IEntityPermissionService entityPermissionService) : base(repository, currentUserService, entityPermissionService)
        {
        }

        protected override Task<AticAdvisoryService> MapCreateDtoToEntityAsync(AticAdvisoryServiceCreateDto createDto)
        {
            var entity = new AticAdvisoryService
            {
                ServiceType = createDto.ServiceType,
                ServiceCount = createDto.ServiceCount,
                BeneficiaryCount = createDto.ServiceCount,
                StartDate = createDto.StartDate,
                EndDate = createDto.EndDate,
                UnitLocationId = createDto.UnitLocationId,
                Attachements = createDto.Attachements,
            };
            return Task.FromResult(entity);
        }

        protected override Task MapUpdateDtoToEntityAsync(AticAdvisoryServiceUpdateDto updateDto, AticAdvisoryService entity)
        {
            if (updateDto.ServiceType is not null && updateDto.ServiceType != entity.ServiceType) entity.ServiceType = updateDto.ServiceType;
            if (updateDto.StartDate.HasValue) entity.StartDate = updateDto.StartDate.Value;
            if (updateDto.EndDate.HasValue) entity.EndDate = updateDto.EndDate.Value;
            if (updateDto.ServiceCount.HasValue) entity.ServiceCount = updateDto.ServiceCount.Value;
            if (updateDto.BeneficiaryCount.HasValue) entity.BeneficiaryCount = updateDto.BeneficiaryCount.Value;
            if (updateDto.Attachements is not null) entity.Attachements = updateDto.Attachements;
            return Task.CompletedTask;
        }
    }
}

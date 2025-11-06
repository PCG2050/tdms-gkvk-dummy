using Application.Interface;
using Application.Interface.Repository.DataTables;
using Application.Interface.Services.Common;
using Application.Interface.Services.DataTables;
using Application.Models.DataTables;
using Domain.Entities.ASM;

namespace Infrastructure.Services.DataTables
{
    public class AsmVisitService : GenericTableService<AsmVisit, AsmVisitCreateDto, AsmVisitUpdateDto, AsmVisitDto>, IAsmVisitService
    {
        public AsmVisitService(IAsmVisitRepository repository, ICurrentUserService currentUserService, IEntityPermissionService entityPermissionService) : base(repository, currentUserService, entityPermissionService)
        {
        }

        protected override Task<AsmVisit> MapCreateDtoToEntityAsync(AsmVisitCreateDto createDto)
        {
            var entity = new AsmVisit
            {
                OrganizationName = createDto.OrganizationName,
                VisitorCount = createDto.VisitorCount,
                StartDate = createDto.StartDate,
                EndDate = createDto.EndDate,
                UnitLocationId = createDto.UnitLocationId,
                Attachements = createDto.Attachements,
            };
            return Task.FromResult(entity);
        }

        protected override Task MapUpdateDtoToEntityAsync(AsmVisitUpdateDto updateDto, AsmVisit entity)
        {
            if (updateDto.StartDate.HasValue) entity.StartDate = updateDto.StartDate.Value;
            if (updateDto.EndDate.HasValue) entity.EndDate = updateDto.EndDate.Value;
            if (updateDto.OrganizationName is not null && updateDto.OrganizationName != entity.OrganizationName) entity.OrganizationName = updateDto.OrganizationName;
            if (updateDto.VisitorCount.HasValue) entity.VisitorCount = updateDto.VisitorCount.Value;
            if (updateDto.Attachements is not null) entity.Attachements = updateDto.Attachements;
            return Task.CompletedTask;
        }
    }
}

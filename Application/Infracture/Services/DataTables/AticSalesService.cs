using Application.Interface;
using Application.Interface.Repository.DataTables;
using Application.Interface.Services.Common;
using Application.Interface.Services.DataTables;
using Application.Models.DataTables;
using Domain.Entities.ATIC;

namespace Infrastructure.Services.DataTables
{
    public class AticSalesService : GenericTableService<AticSales, AticSalesCreateDto, AticSalesUpdateDto, AticSalesDto>, IAticSalesService
    {
        public AticSalesService(IAticSalesRepository repository, ICurrentUserService currentUserService, IEntityPermissionService entityPermissionService) : base(repository, currentUserService, entityPermissionService)
        {
        }

        protected override Task<AticSales> MapCreateDtoToEntityAsync(AticSalesCreateDto createDto)
        {
            var entity = new AticSales
            {
                Details = createDto.Details,
                Quantity = createDto.Quantity,
                QuantityType = createDto.QuantityType,
                StartDate = createDto.StartDate,
                EndDate = createDto.EndDate,
                UnitLocationId = createDto.UnitLocationId,
                Attachements = createDto.Attachements,
            };
            return Task.FromResult(entity);
        }

        protected override Task MapUpdateDtoToEntityAsync(AticSalesUpdateDto updateDto, AticSales entity)
        {
            if (updateDto.StartDate.HasValue) entity.StartDate = updateDto.StartDate.Value;
            if (updateDto.EndDate.HasValue) entity.EndDate = updateDto.EndDate.Value;
            if (updateDto.Details is not null) entity.Details = updateDto.Details;
            if (updateDto.Quantity.HasValue) entity.Quantity = updateDto.Quantity.Value;
            if (updateDto.QuantityType.HasValue) entity.QuantityType = updateDto.QuantityType.Value;
            if (updateDto.Attachements is not null) entity.Attachements = updateDto.Attachements;
            return Task.CompletedTask;
        }
    }
}

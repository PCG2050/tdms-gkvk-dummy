using Application.Interface.Repository.DataTables;
using Application.Models.DataTables;
using Domain.Entities.ATIC;
using Domain.Entities.STU;
using Infrastructure.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.DataTables
{
    public class AticSalesRepository : GenericRepository<AticSales, AticSalesDto>, IAticSalesRepository
    {
        public AticSalesRepository(TdmsDbContext context) : base(context)
        {
        }

        protected override IQueryable<AticSalesDto> ProjectToDto(IQueryable<AticSales> query)
        {
            return query.Select(d => new AticSalesDto
            {
                Id = d.Id,
                StartDate = d.StartDate,
                EndDate = d.EndDate,
                UnitLocationId = d.UnitLocationId,
                Details = d.Details,
                Quantity = d.Quantity,
                QuantityType = d.QuantityType,
                Attachements = d.Attachements,
                CreatedAt = d.CreatedAt,
                UpdatedAt = d.UpdatedAt
            });
        }
    }
}

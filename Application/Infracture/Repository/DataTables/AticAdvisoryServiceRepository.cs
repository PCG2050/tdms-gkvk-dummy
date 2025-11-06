//using Application.Interface.Repository.DataTables;
//using Application.Models.DataTables;
//using Domain.Entities.ATIC;
//using Domain.Entities.STU;
//using Infrastructure.DbContext;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Infrastructure.Repository.DataTables
//{
//    public class AticAdvisoryServiceRepository : GenericRepository<AticAdvisoryServices, AticAdvisoryServiceDto>, IAticAdvisoryServiceRepository
//    {
//        public AticAdvisoryServiceRepository(TdmsDbContext context) : base(context)
//        {
//        }

//        protected override IQueryable<AticAdvisoryServiceDto> ProjectToDto(IQueryable<AticAdvisoryServices> query)
//        {
//            return query.Select(d => new AticAdvisoryServiceDto
//            {
//                Id = d.Id,
//                StartDate = d.StartDate,
//                EndDate = d.EndDate,
//                UnitLocationId = d.UnitLocationId,
//                ServiceType = d.ServiceType,
//                BeneficiaryCount = d.BeneficiaryCount,
//                ServiceCount = d.ServiceCount,
//                Attachements = d.Attachements,
//                CreatedAt = d.CreatedAt,
//                UpdatedAt = d.UpdatedAt
//            });
//        }
//    }
//}

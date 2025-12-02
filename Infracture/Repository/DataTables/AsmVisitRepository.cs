//using Application.Interface.Repository.DataTables;
//using Application.Models.DataTables;
//using Domain.Entities.ASM;
//using Infrastructure.DbContext;

//namespace Infrastructure.Repository.DataTables
//{
//    public class AsmVisitRepository : GenericRepository<AsmVisit, AsmVisitDto>, IAsmVisitRepository
//    {
//        public AsmVisitRepository(TdmsDbContext context) : base(context)
//        {
//        }

//        protected override IQueryable<AsmVisitDto> ProjectToDto(IQueryable<AsmVisit> query)
//        {
//            return query.Select(d => new AsmVisitDto
//            {
//                Id = d.Id,
//                StartDate = d.StartDate,
//                EndDate = d.EndDate,
//                UnitLocationId = d.UnitLocationId,
//                OrganizationName = d.OrganizationName,
//                VisitorCount = d.VisitorCount,
//                Attachements = d.Attachements,
//                CreatedAt = d.CreatedAt,
//                UpdatedAt = d.UpdatedAt
//            });
//        }
//    }
//}

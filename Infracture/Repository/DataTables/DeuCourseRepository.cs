//using Application.Interface.Repository.DataTables;
//using Application.Models.DataTables;
//using Domain.Entities.DEU;
//using Infrastructure.DbContext;

//namespace Infrastructure.Repository.DataTables
//{
//    public class DeuCourseRepository : GenericRepository<DeuCourse, DeuCourseDto>, IDeuCourseRepository
//    {
//        public DeuCourseRepository(TdmsDbContext context) : base(context)
//        {
//        }

//        protected override IQueryable<DeuCourseDto> ProjectToDto(IQueryable<DeuCourse> query)
//        {
//            return query.Select(d => new DeuCourseDto
//            {
//                Id = d.Id,
//                StartDate = d.StartDate,
//                EndDate = d.EndDate,
//                UnitLocationId = d.UnitLocationId,
//                Type = d.Type,
//                Name = d.Name,
//                CandidateAdmittedCount = d.CandidateAdmittedCount,
//                CandidateAttendedExamCount = d.CandidateAttendedExamCount,
//                CandidatePassedCount = d.CandidatePassedCount,
//                Attachements = d.Attachements,
//                CreatedAt = d.CreatedAt,
//                UpdatedAt = d.UpdatedAt
//            });
//        }
//    }
//}

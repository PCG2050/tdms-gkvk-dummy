using Application.Interface;
using Application.Interface.Repository.DataTables;
using Application.Interface.Services.Common;
using Application.Interface.Services.DataTables;
using Application.Models.DataTables;
using Domain.Entities.DEU;

namespace Infrastructure.Services.DataTables
{
    public class DeuCourseService : GenericTableService<DeuCourse, DeuCourseCreateDto, DeuCourseUpdateDto, DeuCourseDto>, IDeuCourseService
    {
        public DeuCourseService(IDeuCourseRepository repository, ICurrentUserService currentUserService, IEntityPermissionService entityPermissionService) : base(repository, currentUserService, entityPermissionService)
        {
        }

        protected override Task<DeuCourse> MapCreateDtoToEntityAsync(DeuCourseCreateDto createDto)
        {
            var entity = new DeuCourse
            {
                Type = createDto.Type,
                Name = createDto.Name,
                CandidateAdmittedCount = createDto.CandidateAdmittedCount,
                CandidateAttendedExamCount = createDto.CandidateAttendedExamCount,
                CandidatePassedCount = createDto.CandidatePassedCount,
                StartDate = createDto.StartDate,
                EndDate = createDto.EndDate,
                UnitLocationId = createDto.UnitLocationId,
                Attachements = createDto.Attachements,
            };
            return Task.FromResult(entity);
        }

        protected override Task MapUpdateDtoToEntityAsync(DeuCourseUpdateDto updateDto, DeuCourse entity)
        {
            if (updateDto.StartDate.HasValue) entity.StartDate = updateDto.StartDate.Value;
            if (updateDto.EndDate.HasValue) entity.EndDate = updateDto.EndDate.Value;
            if (updateDto.Type.HasValue) entity.Type = updateDto.Type.Value;
            if (updateDto.Name is not null && updateDto.Name != entity.Name) entity.Name = updateDto.Name;
            if (updateDto.CandidateAdmittedCount.HasValue) entity.CandidateAdmittedCount = updateDto.CandidateAdmittedCount.Value;
            if (updateDto.CandidateAttendedExamCount.HasValue) entity.CandidateAttendedExamCount = updateDto.CandidateAttendedExamCount.Value;
            if (updateDto.CandidatePassedCount.HasValue) entity.CandidatePassedCount = updateDto.CandidatePassedCount.Value;
            if (updateDto.Attachements is not null) entity.Attachements = updateDto.Attachements;
            return Task.CompletedTask;
        }
    }
}

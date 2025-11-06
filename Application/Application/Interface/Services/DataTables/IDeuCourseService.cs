using Application.Interface.Services.Common;
using Application.Models.DataTables;
using Domain.Entities.DEU;

namespace Application.Interface.Services.DataTables
{
    public interface IDeuCourseService : IGenericTableService<DeuCourse, DeuCourseCreateDto, DeuCourseUpdateDto, DeuCourseDto>
    {
    }
}

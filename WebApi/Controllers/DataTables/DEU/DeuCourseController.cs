//using Application.Interface.Services.DataTables;
//using Application.Models.DataTables;
//using Domain.Entities.DEU;
//using Domain.Entities.Enum;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;

//namespace WebApi.Controllers.DataTables.DEU
//{
//    [ApiController]
//    [Authorize(Roles = $"{RoleString.Admin},{RoleString.UnitHead},{RoleString.Trainer}")]
//    [Route("api/DEU/courses")]
//    public class DeuCourseController : GenericTableApiController<DeuCourse, DeuCourseCreateDto, DeuCourseUpdateDto, DeuCourseDto>
//    {
//        public DeuCourseController(IDeuCourseService service) : base(service)
//        {

//        }
//    }
//}

//using Application.Interface.Services.DataTables;
//using Application.Models;
//using Application.Models.DataTables;
//using Domain.Entities.Enum;
//using Domain.Entities.STU;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;

//namespace WebApi.Controllers.DataTables.STU
//{
//    [ApiController]
//    [Route("api/STU/training-programme")]
//    [Authorize(Roles = $"{RoleString.Admin},{RoleString.UnitHead},{RoleString.Trainer}")]
//    public class StuTrainingProgrammeController : GenericTableApiController<StuTrainingProgramme,StuTrainingProgrammeCreateDto,StuTrainingProgrammeUpdateDto,StuTrainingProgrammeDto>
//    {
//        public StuTrainingProgrammeController(IStuTrainingProgrammeService trainingProgrammeService)
//            : base(trainingProgrammeService)
//        {
//        }
//    }
//}

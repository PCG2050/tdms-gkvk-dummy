//using Application.Interface.Services.DataTables;
//using Application.Models.DataTables;
//using Domain.Entities.Enum;
//using Domain.Entities.STU;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;

//namespace WebApi.Controllers.DataTables.STU
//{
//    [ApiController]
//    [Route("api/STU/sponsored-training-programme")]
//    [Authorize(Roles = $"{RoleString.Admin},{RoleString.UnitHead},{RoleString.Trainer}")]
//    public class StuSponsoredTrainingProgrammeController : GenericTableApiController<StuSponsoredTrainingProgramme,StuSponsoredTrainingProgrammeCreateDto,StuSponsoredTrainingProgrammeUpdateDto,StuSponsoredTrainingProgrammeDto>
//    {
//        public StuSponsoredTrainingProgrammeController(IStuSponsoredTrainingProgrammeService trainingProgrammeService)
//            : base(trainingProgrammeService)
//        {
//        }
//    }
//}

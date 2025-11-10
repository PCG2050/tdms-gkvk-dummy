//using Application.Interface.Services.DataTables;
//using Application.Models.DataTables;
//using Domain.Entities.EEU;
//using Domain.Entities.Enum;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;

//namespace WebApi.Controllers.DataTables.EEU
//{
//    [ApiController]
//    [Authorize(Roles = $"{RoleString.Admin},{RoleString.UnitHead},{RoleString.Trainer}")]
//    [Route("api/EEU/training-programme")]
//    public class EeuTrainingProgrammeController : GenericTableApiController<EeuTrainingProgramme, EeuTrainingProgrammeCreateDto, EeuTrainingProgrammeUpdateDto, EeuTrainingProgrammeDto>
//    {
//        public EeuTrainingProgrammeController(IEeuTrainingProgrammeService service) : base(service)
//        {
//        }
//    }
//}

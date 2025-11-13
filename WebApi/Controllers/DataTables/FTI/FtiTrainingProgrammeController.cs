using Application.Interface.Services.DataTables;
using Application.Models.DataTables;
using Domain.Entities.FTI;
using Domain.Entities.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.DataTables.FTI
{
    [ApiController]
    [Authorize(Roles = $"{RoleString.Admin},{RoleString.UnitHead},{RoleString.Trainer}")]
    [Route("api/FTI/training-programme")]
    public class FtiTrainingProgrammeController : GenericTableApiController<FtiTrainingProgramme, FtiTrainingProgrammeCreateDto, FtiTrainingProgrammeUpdateDto, FtiTrainingProgrammeDto>
    {
        public FtiTrainingProgrammeController(IFtiTrainingProgrammeService service) : base(service)
        {
        }
    }
}

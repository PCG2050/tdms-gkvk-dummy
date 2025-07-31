using Application.Interface;
using Application.Interface.Services.DataTables;
using Application.Models;
using Domain.Entities.Enum;
using Domain.Entities.FTI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.DataTables.FTI
{
    
    [Route("api/FTI/training-programme")]
    [Authorize(Roles = $"{RoleString.Admin},{RoleString.UnitHead},{RoleString.Trainer}")]
    public class FtiTrainingProgrammeController : GenericTableApiController<FtiTrainingProgram, FtiTrainingProgrammeCreateDto, FtiTrainingProgrammeUpdateDto, FtiTrainingProgram>
    {
        public FtiTrainingProgrammeController(IFtiTrainingProgrammeService trainingProgrammeService, IFtiOtherActivityService otherActivityService)
            : base(trainingProgrammeService)
        {
        }
    }
}

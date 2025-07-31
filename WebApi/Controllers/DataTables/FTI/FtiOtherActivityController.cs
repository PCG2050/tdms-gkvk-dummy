using Application;
using Application.Interface;
using Application.Interface.Services;
using Application.Interface.Services.DataTables;
using Application.Models.DataTables;
using Domain.Entities.Enum;
using Domain.Entities.FTI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.DataTables
{
    [ApiController]
    [Route("api/FTI/other-activities")]
    [Authorize(Roles = $"{RoleString.Admin},{RoleString.UnitHead},{RoleString.Trainer}")]
    public class FtiOtherActivityController : GenericTableApiController<FtiOtherActivity, OtherActivityCreateDto, OtherActivityUpdateDto, OtherActivityDto>
    {
        public FtiOtherActivityController(IFtiOtherActivityService otherActivityService):
            base(otherActivityService)
        {
        }
    }
}

using Application.Interface.Services.DataTables;
using Application.Models.DataTables;
using Domain.Entities.ASM;
using Domain.Entities.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.DataTables.ASM
{
    [ApiController]
    [Authorize(Roles = $"{RoleString.Admin},{RoleString.UnitHead},{RoleString.Trainer}")]
    [Route("api/ASM/visits")]
    public class AsmVisitController : GenericTableApiController<AsmVisit, AsmVisitCreateDto, AsmVisitUpdateDto, AsmVisitDto>
    {
        public AsmVisitController(IAsmVisitService service) : base(service)
        {
        }
    }
}

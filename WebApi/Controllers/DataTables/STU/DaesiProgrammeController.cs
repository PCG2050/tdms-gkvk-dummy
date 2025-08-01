using Application.Interface.Services.DataTables;
using Application.Models.DataTables;
using Domain.Entities.Enum;
using Domain.Entities.STU;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.DataTables.STU
{
    [ApiController]
    [Authorize(Roles = $"{RoleString.Admin},{RoleString.UnitHead},{RoleString.Trainer}")]
    [Route("api/STU/daesi-programme")]
    public class DaesiProgrammeController : GenericTableApiController<DaesiProgramme, DaesiProgrammeCreateDto, DaesiProgrammeUpdateDto, DaesiProgrammeDto>
    {
        public DaesiProgrammeController(IDaesiProgrammeService service) : base(service)
        {
        }
    }
}

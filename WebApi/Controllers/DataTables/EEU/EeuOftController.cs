using Application.Interface.Services.DataTables;
using Application.Models.DataTables;
using Domain.Entities.EEU;
using Domain.Entities.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.DataTables.EEU
{
    [ApiController]
    [Authorize(Roles = $"{RoleString.Admin},{RoleString.UnitHead},{RoleString.Trainer}")]
    [Route("api/EEU/OFT")]
    public class EeuOftController : GenericTableApiController<EeuOFT, EeuOftCreateDto, EeuOftUpdateDto, EeuOftDto>
    {
        public EeuOftController(IEeuOftService service) : base(service)
        {
        }
    }
}

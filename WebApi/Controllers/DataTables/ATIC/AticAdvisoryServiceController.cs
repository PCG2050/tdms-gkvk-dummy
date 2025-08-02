using Application.Interface.Services.DataTables;
using Application.Models.DataTables;
using Domain.Entities.ATIC;
using Domain.Entities.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.DataTables.ATIC
{
    [ApiController]
    [Authorize(Roles = $"{RoleString.Admin},{RoleString.UnitHead},{RoleString.Trainer}")]
    [Route("api/ATIC/advisory-services")]
    public class AticAdvisoryServiceController : GenericTableApiController<AticAdvisoryService, AticAdvisoryServiceCreateDto, AticAdvisoryServiceUpdateDto, AticAdvisoryServiceDto>
    {
        public AticAdvisoryServiceController(IAticAdvisoryServiceService service) : base(service)
        {
        }
    }
}

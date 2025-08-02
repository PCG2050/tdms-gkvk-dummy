using Application.Interface.Services.DataTables;
using Application.Models.DataTables;
using Domain.Entities.Enum;
using Domain.Entities.NAEP;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.DataTables.NAEP
{
    [ApiController]
    [Authorize(Roles = $"{RoleString.Admin},{RoleString.UnitHead},{RoleString.Trainer}")]
    [Route("api/NAEP/details")]
    public class NaepDetailsController : GenericTableApiController<NaepDetails, NaepDetailsCreateDto, NaepDetailsUpdateDto, NaepDetailsDto>
    {
        public NaepDetailsController(INaepDetailsService service) : base(service)
        {
        }
    }
}

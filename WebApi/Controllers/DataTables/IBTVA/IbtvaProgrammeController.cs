using Application.Interface.Services.DataTables;
using Application.Models.DataTables;
using Domain.Entities.IBTVA;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.DataTables.IBTVA
{
    [ApiController]
    [Route("api/")]
    public class IbtvaProgrammeController : GenericTableApiController<IbtvaProgramme, IbtvaProgrammeCreateDto, IbtvaProgrammeUpdateDto, IbtvaProgrammeDto>
    {
        public IbtvaProgrammeController(IIbtvaProgrammeService service) : base(service)
        {
        }
    }
}

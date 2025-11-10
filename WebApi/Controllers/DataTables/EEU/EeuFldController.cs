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
//    [Route("api/EEU/FLD")]
//    public class EeuFldController : GenericTableApiController<EeuFLD, EeuFldCreateDto, EeuFldUpdateDto, EeuFldDto>
//    {
//        public EeuFldController(IEeuFldService service) : base(service)
//        {
//        }
//    }
//}

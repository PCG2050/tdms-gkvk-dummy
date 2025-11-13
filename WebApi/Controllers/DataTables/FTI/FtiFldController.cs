//using Application.Interface.Services.DataTables;
//using Application.Models.DataTables;
//using Domain.Entities.FTI;
//using Domain.Entities.Enum;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;

//namespace WebApi.Controllers.DataTables.FTI
//{
//    [ApiController]
//    [Authorize(Roles = $"{RoleString.Admin},{RoleString.UnitHead},{RoleString.Trainer}")]
//    [Route("api/FTI/FLD")]
//    public class FtiFldController : GenericTableApiController<FtiFLD, FtiFldCreateDto, FtiFldUpdateDto, FtiFldDto>
//    {
//        public FtiFldController(IFtiFldService service) : base(service)
//        {
//        }
//    }
//}

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
//    [Route("api/FTI/OFT")]
//    public class FtiOftController : GenericTableApiController<FtiOFT, FtiOftCreateDto, FtiOftUpdateDto, FtiOftDto>
//    {
//        public FtiOftController(IFtiOftService service) : base(service)
//        {
//        }
//    }
//}

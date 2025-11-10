//using Application.Interface.Services.DataTables;
//using Application.Models.DataTables;
//using Domain.Entities.ATIC;
//using Domain.Entities.Enum;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;

//namespace WebApi.Controllers.DataTables.ATIC
//{
//    [ApiController]
//    [Authorize(Roles = $"{RoleString.Admin},{RoleString.UnitHead},{RoleString.Trainer}")]
//    [Route("api/ATIC/sales")]
//    public class AticSalesController : GenericTableApiController<AticSales, AticSalesCreateDto, AticSalesUpdateDto, AticSalesDto>
//    {
//        public AticSalesController(IAticSalesService service) : base(service)
//        {
//        }
//    }
//}

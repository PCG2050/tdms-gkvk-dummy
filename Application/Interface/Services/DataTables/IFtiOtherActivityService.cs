using Application.Interface.Services.Common;
using Application.Models;
using Application.Models.DataTables;
using Domain.Entities.FTI;

namespace Application.Interface.Services.DataTables
{
    public interface IFtiOtherActivityService:IGenericTableService<FtiOtherActivity,FtiOtherActivityCreateDto,FtiOtherActivityUpdateDto>,IGenericPaginationService<FtiOtherActivityDto>
    {
    }
}

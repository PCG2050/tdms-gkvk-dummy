using Application.Interface.Services.Common;
using Application.Models.DataTables;
using Domain.Entities.ATIC;
using Domain.Entities.STU;

namespace Application.Interface.Services.DataTables
{
    public interface IAticAdvisoryServiceService : IGenericTableService<AticAdvisoryService, AticAdvisoryServiceCreateDto, AticAdvisoryServiceUpdateDto, AticAdvisoryServiceDto>
    {
    }
}

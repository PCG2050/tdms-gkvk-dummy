using Application.Interface.Services.Common;
using Application.Models.DataTables;
using Domain.Entities.IBTVA;

namespace Application.Interface.Services.DataTables
{
    public interface IIbtvaProgrammeService:IGenericTableService<IbtvaProgramme, IbtvaProgrammeCreateDto, IbtvaProgrammeUpdateDto, IbtvaProgrammeDto>
    {
    }
}

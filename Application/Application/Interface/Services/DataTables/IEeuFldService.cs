using Application.Interface.Services.Common;
using Application.Models.DataTables;
using Domain.Entities.EEU;

namespace Application.Interface.Services.DataTables
{
    public interface IEeuFldService : IGenericTableService<EeuFLD, EeuFldCreateDto, EeuFldUpdateDto, EeuFldDto>
    {
    }
}

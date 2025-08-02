using Application.Interface.Services.Common;
using Application.Models.DataTables;
using Domain.Entities.EEU;

namespace Application.Interface.Services.DataTables
{
    public interface IEeuOftService : IGenericTableService<EeuOFT, EeuOftCreateDto, EeuOftUpdateDto, EeuOftDto>
    {
    }
}

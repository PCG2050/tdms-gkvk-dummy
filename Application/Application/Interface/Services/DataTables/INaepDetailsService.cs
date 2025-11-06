using Application.Interface.Services.Common;
using Application.Models.DataTables;
using Domain.Entities.NAEP;

namespace Application.Interface.Services.DataTables
{
    public interface INaepDetailsService : IGenericTableService<NaepDetails, NaepDetailsCreateDto, NaepDetailsUpdateDto, NaepDetailsDto>
    {
    }
}

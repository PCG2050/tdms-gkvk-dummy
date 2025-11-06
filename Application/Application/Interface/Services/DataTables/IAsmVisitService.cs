using Application.Interface.Services.Common;
using Application.Models.DataTables;
using Domain.Entities.ASM;

namespace Application.Interface.Services.DataTables
{
    public interface IAsmVisitService : IGenericTableService<AsmVisit, AsmVisitCreateDto, AsmVisitUpdateDto, AsmVisitDto>
    {
    }
}

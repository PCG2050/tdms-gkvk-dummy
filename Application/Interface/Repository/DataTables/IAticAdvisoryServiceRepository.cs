using Application.Models.DataTables;
using Domain.Entities.ATIC;
using Domain.Entities.STU;
using Infrastructure.Repository;

namespace Application.Interface.Repository.DataTables
{
    public interface IAticAdvisoryServiceRepository : IGenericRepository<AticAdvisoryService, AticAdvisoryServiceDto>
    {
    }
}

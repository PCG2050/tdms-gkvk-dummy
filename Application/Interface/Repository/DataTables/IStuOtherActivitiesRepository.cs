using Application.Models.DataTables;
using Domain.Entities.STU;

namespace Application.Interface.Repository.DataTables
{
    public interface IStuOtherActivitiesRepository:IDataTableRepositoryActions<StuOtherActivity>,IPagination<StuOtherActivityDto>
    {
    }
}

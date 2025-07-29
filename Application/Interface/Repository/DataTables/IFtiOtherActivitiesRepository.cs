using Application.Models.DataTables;
using Domain.Entities.FTI;

namespace Application.Interface.Repository.DataTables
{
    public interface IFtiOtherActivitiesRepository:IDataTableRepositoryActions<FtiOtherActivity>,IPagination<FtiOtherActivityDto>
    {
    }
}

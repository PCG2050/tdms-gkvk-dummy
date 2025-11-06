using Application.Models.DataTables;
using Domain.Entities.FTI;
using Infrastructure.Repository;

namespace Application.Interface.Repository.DataTables
{
    public interface IFtiOtherActivitiesRepository:IGenericRepository<FtiOtherActivity, OtherActivityDto>
    {
    }
}

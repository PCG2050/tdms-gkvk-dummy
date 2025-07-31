using Application.Interface.Services.Common;
using Application.Models.DataTables;
using Domain.Entities.STU;
using Infrastructure.Repository;

namespace Application.Interface.Repository.DataTables
{
    public interface IStuOtherActivitiesRepository:IGenericRepository<StuOtherActivity,StuOtherActivityDto>//,IPagination<StuOtherActivityDto>//IDataTableRepositoryActions<StuOtherActivity>
    {
    }
}

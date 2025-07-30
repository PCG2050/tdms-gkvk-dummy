using Application.Models.DataTables;
using Domain.Entities.FTI;
using Domain.Entities.STU;

namespace Application.Interface.Repository.DataTables
{
    public interface IStuSposoredTrainingProgrammeRepository:IDataTableRepositoryActions<StuSponsoredTrainingProgramme>,IPagination<StuSponsoredTrainingProgrammeDto>
    {
    }
}

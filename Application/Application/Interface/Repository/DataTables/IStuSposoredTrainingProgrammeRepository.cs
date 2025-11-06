using Application.Models.DataTables;
using Domain.Entities.FTI;
using Domain.Entities.STU;
using Infrastructure.Repository;

namespace Application.Interface.Repository.DataTables
{
    public interface IStuSposoredTrainingProgrammeRepository:IGenericRepository<StuSponsoredTrainingProgramme, StuSponsoredTrainingProgrammeDto>
    {
    }
}

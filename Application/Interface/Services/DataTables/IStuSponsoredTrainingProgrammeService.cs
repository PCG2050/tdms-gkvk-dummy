using Application.Interface.Services.Common;
using Application.Models;
using Application.Models.DataTables;
using Domain.Entities.FTI;
using Domain.Entities.STU;

namespace Application.Interface.Services.DataTables
{
    public interface IStuSponsoredTrainingProgrammeService : IGenericTableService<StuSponsoredTrainingProgramme, StuSponsoredTrainingProgrammeCreateDto, StuSponsoredTrainingProgrammeUpdateDto,StuSponsoredTrainingProgrammeDto>
    {
    }
}

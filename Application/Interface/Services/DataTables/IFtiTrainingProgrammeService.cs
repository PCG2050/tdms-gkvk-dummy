using Application.Interface.Services.Common;
using Application.Models.DataTables;
using Domain.Entities.FTI;

namespace Application.Interface.Services.DataTables
{
    public interface IFtiTrainingProgrammeService : IGenericTableService<FtiTrainingProgramme, FtiTrainingProgrammeCreateDto, FtiTrainingProgrammeUpdateDto, FtiTrainingProgrammeDto>
    {
    }
}

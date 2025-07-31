using Application.Interface.Services.Common;
using Application.Models;
using Domain.Entities.FTI;

namespace Application.Interface
{
    public interface IFtiTrainingProgrammeService:IGenericTableService<FtiTrainingProgram,FtiTrainingProgrammeCreateDto,FtiTrainingProgrammeUpdateDto,FtiTrainingProgram>
    {
    }
}

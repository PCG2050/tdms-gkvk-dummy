using Domain.Entities.FTI;

namespace Application.Interface.Repository.DataTables
{
    public interface IFtiTrainingProgrammeRepository:IDataTableRepositoryActions<FtiTrainingProgram>,IPagination<FtiTrainingProgram>
    {
        Task DeleteAsync(int id);
        Task<FtiTrainingProgram?> GetItemAsync(int id);
    }
}

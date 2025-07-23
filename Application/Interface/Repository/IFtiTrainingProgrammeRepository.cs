using Domain.Entities.FTI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Repository
{
    public interface IFtiTrainingProgrammeRepository:IPagination<FtiTrainingProgram>
    {
        Task AddAsync(FtiTrainingProgram ftiTrainingProgram);
        Task DeleteAsync(int id);
        Task<FtiTrainingProgram?> GetItemAsync(int id);
        Task UpdateAsync(FtiTrainingProgram ftiTrainingProgram);
    }
}

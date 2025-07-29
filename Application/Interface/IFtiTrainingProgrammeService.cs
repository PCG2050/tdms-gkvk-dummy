using Application.Models;
using Domain.Entities.FTI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IFtiTrainingProgrammeService
    {
        Task<PaginatedResult<FtiTrainingProgram>> GetPaginatedItemsAsync(int pageNumber = Constants.PAGINATION_PAGE_NUMBER_DEFAULT, int pageSize = Constants.PAGINATION_PAGE_SIZE_DEFAULT);
        Task<FtiTrainingProgram> AddAsync(CreateFtiTrainingProgrammeEntryDto createDto);
        Task UpdateAsync(UpdateTrainingProgrammeEntryDto updateDto);
        Task DeleteAsync(int id);
        Task DeleteAsync(FtiTrainingProgram entity);
    }
}

// IFtiProgramDetailsRepository.cs
using Application.Interface.Repository;
using Application.Models;
using Domain.Entities.FTI;

namespace Application.Interface.Repository.DataTables.FTI
{
    /// <summary>
    /// FTI Program Details Repository Interface
    /// Extends generic repository with FTI-specific operations
    /// </summary>
    public interface IFtiProgramDetailsRepository : IGenericProgramRepository<FtiProgramDetailsGeneric>
    {
        // All methods are inherited from IGenericProgramRepository<FtiProgramDetailsGeneric>
        // Add any FTI-specific repository methods here if needed in the future
    }
}

namespace Application.Interface.Repository.DataTables.TblService
{
    public interface IRevolvingFundRepository
    {
        Task<RevolvingFundStatus?> GetRevolvingFundStatusByIdAsync(int id);
        Task<List<RevolvingFundStatus>> GetRevolvingFundStatusesByServiceIdAsync(int serviceId);
        Task<RevolvingFundStatus> CreateRevolvingFundStatusAsync(RevolvingFundStatus entity);
        Task<RevolvingFundStatus> UpdateRevolvingFundStatusAsync(RevolvingFundStatus entity);
        Task DeleteRevolvingFundStatusAsync(int id);
    }
}


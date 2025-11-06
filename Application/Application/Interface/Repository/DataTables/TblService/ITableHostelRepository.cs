


namespace Application.Interface.Repository.DataTables.TblService
{
    public interface ITableHostelRepository
    {
        Task<TableHostel?> GetTableHostelByIdAsync(int id);
        Task<List<TableHostel>> GetTableHostelsByServiceIdAsync(int serviceId);
        Task<TableHostel> CreateTableHostelAsync(TableHostel entity);
        Task<TableHostel> UpdateTableHostelAsync(TableHostel entity);
        Task DeleteTableHostelAsync(int id);
    }
}

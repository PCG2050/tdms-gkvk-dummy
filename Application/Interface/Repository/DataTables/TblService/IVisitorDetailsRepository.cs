using Domain.Entities.GenericTables.Service;
using System;
using System.Collections.Generic;
namespace Application.Interface.Repository.DataTables.TblService
{
    public interface IVisitorDetailsRepository
    {
        Task<VisitorDetail?> GetVisitorDetailByIdAsync(int id);
        Task<List<VisitorDetail>> GetVisitorDetailsByServiceIdAsync(int serviceId);
        Task<VisitorDetail> CreateVisitorDetailAsync(VisitorDetail entity);
        Task<VisitorDetail> UpdateVisitorDetailAsync(VisitorDetail entity);
        Task DeleteVisitorDetailAsync(int id);
    }
}

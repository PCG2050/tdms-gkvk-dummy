using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Services.Common
{
    public interface IEntityPermissionService
    {
        Task<bool> CanModify<T>(T entity) where T: ReportEntryBaseEntity;

        Task<bool> CanView<T>(T entity) where T : ReportEntryBaseEntity;
        Task<bool> CanDelete<T>(T entity) where T : ReportEntryBaseEntity;
        Task<bool> CanViewForm<T>(T entity) where T : AuditableBaseEntity;
        Task<bool> CanModifyForm<T>(T entity) where T : AuditableBaseEntity;
        Task<bool> CanDeleteForm<T>(T entity) where T : AuditableBaseEntity;




    }
}

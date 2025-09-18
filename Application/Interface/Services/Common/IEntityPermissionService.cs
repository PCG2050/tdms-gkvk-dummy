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

        Task<bool> CanUserAccessUnitLocationAsync(int userId, int unitLocationId);

        Task<bool> CanUserAccessTableAsync(int userId, int tableDefinitionId);
    }
}

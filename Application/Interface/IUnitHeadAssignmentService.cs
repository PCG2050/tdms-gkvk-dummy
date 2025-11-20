using Application.Interface.Repository;
using Application.Models;
using Domain.Entities.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IUnitHeadAssignmentService
    {
        Task<ServiceResult<List<UnitWithLocationsDto>>> GetUnitHeadUnits(int unitHeadId);
        Task<ServiceResult<UnitHeadStatisticsDto>> GetUnitHeadStatisticsAsync(int unitHeadId);
    }
}

using Application.Models.DataTables.FIU;
using Domain.Entities.FIU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapper.DataTable.FIU
{
    [Mapper]
    public partial class FIUProgramActivityMapper
    {
        public partial FIUProgramActivity MapToEntity(FIUProgramActivityDto dto);
        public partial FIUProgramActivityDto MapToDto(FIUProgramActivity entity);
    }
}

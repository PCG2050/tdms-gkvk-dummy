using Application.Models.DataTables.ASM;
using Domain.Entities.ASM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapper.DataTable.ASM
{
    [Mapper]
    public partial class ASMVisitorDetailsMapper
    {
        public partial ASMVisitorDetails MapToEntity(ASMVisitorDetailsDto dto);
        public partial ASMVisitorDetailsDto MapToDto(ASMVisitorDetails entity);
    }
}

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

        [MapperIgnoreTarget(nameof(ASMVisitorDetails.Id))]
        [MapperIgnoreTarget(nameof(ASMVisitorDetails.UnitLocationId))]
        [MapperIgnoreTarget(nameof(ASMVisitorDetails.OrganizationId))]
        [MapperIgnoreTarget(nameof(ASMVisitorDetails.FormStatus))]
        [MapperIgnoreTarget(nameof(ASMVisitorDetails.FormStatusRemarks))]
        [MapperIgnoreTarget(nameof(ASMVisitorDetails.ApprovedAt))]
        [MapperIgnoreTarget(nameof(ASMVisitorDetails.ApprovedById))]
        [MapperIgnoreTarget(nameof(ASMVisitorDetails.CreatedAt))]
        [MapperIgnoreTarget(nameof(ASMVisitorDetails.CreatedById))]
        [MapperIgnoreTarget(nameof(ASMVisitorDetails.UpdatedAt))]
        [MapperIgnoreTarget(nameof(ASMVisitorDetails.UpdatedById))]
        public partial void MapUpdateDtoToEntity(ASMVisitorDetailsUpdateDto dto, ASMVisitorDetails entity);
    }
}

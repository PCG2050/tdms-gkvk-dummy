using Application.Models;
using Domain.Entities.GenericTables;
using Riok.Mapperly.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapper
{
    [Mapper]
    public partial class TableOtherActivityMapper
    {
        public partial TableOtherActivityDto MapToDto(TableOtherActivity entity);
        public partial TableOtherActivity MapToEntity(TableOtherActivityCreateDto dto);
        public partial void MapToExistingEntity(TableOtherActivityUpdateDto dto, TableOtherActivity entity);
    }
}

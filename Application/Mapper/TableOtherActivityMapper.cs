

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

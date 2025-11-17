using Application.Models.DataTables;
using Domain.Entities.GenericTables;

namespace Application.Mapper
{
    /// <summary>
    /// Mapper for TableOtherActivity entity and DTOs
    /// Handles conversion between domain entities and data transfer objects
    /// </summary>
    public class TableOtherActivityMapper
    {
        /// <summary>
        /// Map CreateDto to Entity
        /// Used when creating a new activity
        /// </summary>
        public TableOtherActivity MapToEntity(TableOtherActivityCreateDto dto)
        {
            return new TableOtherActivity
            {
                UnitLocationId = dto.UnitLocationId,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Title = dto.Title,
                Description = dto.Description,
                UploadPath = dto.UploadPath,
                FormStatus = "Draft" // Always start as Draft
            };
        }

        /// <summary>
        /// Map Entity to Dto
        /// Includes related entity names for display
        /// </summary>
        public TableOtherActivityDto MapToDto(TableOtherActivity entity)
        {
            return new TableOtherActivityDto
            {
                Id = entity.Id,
                UnitLocationId = entity.UnitLocationId,
                OrganizationId = entity.OrganizationId,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
                Title = entity.Title,
                Description = entity.Description,
                UploadPath = entity.UploadPath,
                FormStatus = entity.FormStatus,
                FormStatusRemarks = entity.FormStatusRemarks,
                ApprovedAt = entity.ApprovedAt,
                ApprovedById = entity.ApprovedById,
                CreatedAt = entity.CreatedAt,
                CreatedById = (int)entity.CreatedById,
                UpdatedAt = entity.UpdatedAt,
                UpdatedById = entity.UpdatedById,


                CreatedByName = entity.CreatedBy?.FirstName,
                ApprovedByName = entity.ApprovedBy?.LastName
            };
        }

        /// <summary>
        /// Map UpdateDto to existing Entity
        /// Only updates provided (non-null) fields
        /// </summary>
        public void MapToExistingEntity(TableOtherActivityUpdateDto dto, TableOtherActivity entity)
        {
            if (dto.StartDate.HasValue)
                entity.StartDate = dto.StartDate;

            if (dto.EndDate.HasValue)
                entity.EndDate = dto.EndDate;

            if (!string.IsNullOrWhiteSpace(dto.Title))
                entity.Title = dto.Title;

            if (dto.Description != null) // Allow clearing description
                entity.Description = dto.Description;

            if (dto.UploadPath != null) // Allow clearing upload path
                entity.UploadPath = dto.UploadPath;
        }

        /// <summary>
        /// Map list of entities to list of DTOs
        /// </summary>
        public List<TableOtherActivityDto> MapToDtoList(List<TableOtherActivity> entities)
        {
            return entities.Select(MapToDto).ToList();
        }
    }
}
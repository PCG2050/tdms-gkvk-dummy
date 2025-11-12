

using Application.Models.DataTables.FIU;
using Application.Models.DataTables.FIU.Application.Models.FIU;
using Domain.Entities.FIU;

namespace Application.Mapper.Datatable.FIU
{
    public partial class FIUProgramActivityMapper
    {
        public FIUProgramActivity MapCreateDtoToEntity(FIUProgramActivityCreateDto dto)
        {
            return new FIUProgramActivity
            {
                UnitLocationId = dto.UnitLocationId,
                FIUActivitiesId = dto.FIUActivitiesId,
                Number = dto.Number,
                UploadMediaUrl = dto.UploadMediaUrl,
                Remarks = dto.Remarks
            };
        }

        public void MapUpdateDtoToEntity(FIUProgramActivityUpdateDto dto, FIUProgramActivity entity)
        {
            entity.FIUActivitiesId = dto.FIUActivitiesId;
            entity.Number = dto.Number;
            entity.UploadMediaUrl = dto.UploadMediaUrl;
            entity.Remarks = dto.Remarks;
        }

        public FIUProgramActivityResponseDto MapEntityToResponseDto(FIUProgramActivity entity)
        {
            return new FIUProgramActivityResponseDto
            {
                Id = entity.Id,
                UnitLocationId = entity.UnitLocationId,
                UnitLocationName = entity.UnitLocation?.Unit?.Name ?? string.Empty,
                DistrictName = entity.UnitLocation?.District?.Name ?? string.Empty,
                StateName = entity.UnitLocation?.District?.State?.Name ?? string.Empty,
                OrganizationId = entity.OrganizationId,
                OrganizationName = entity.Organization?.Name ?? string.Empty,
                FIUActivitiesId = entity.FIUActivitiesId,
                ActivityName = entity.FIUActivity?.ActivityName ?? string.Empty,
                ActivityCategory = entity.FIUActivity?.ActivityCategory ?? string.Empty,
                UnitOfMeasurement = entity.FIUActivity?.UnitOfMeasurement,
                Number = entity.Number,
                UploadMediaUrl = entity.UploadMediaUrl,
                Remarks = entity.Remarks,
                FormStatus = entity.FormStatus,
                FormStatusRemarks = entity.FormStatusRemarks,
                SubmittedAt = entity.SubmittedAt,
                ApprovedAt = entity.ApprovedAt,
                CreatedById = entity.CreatedById ?? 0,
                CreatedByName = $"{entity.CreatedBy?.FirstName} {entity.CreatedBy?.LastName}".Trim(),
                CreatedAt = entity.CreatedAt,
                ApprovedById = entity.ApprovedById,
                ApprovedByName = entity.ApprovedBy != null
                    ? $"{entity.ApprovedBy.FirstName} {entity.ApprovedBy.LastName}".Trim()
                    : null,
                UpdatedById = entity.UpdatedById,
                UpdatedByName = entity.UpdatedBy != null
                    ? $"{entity.UpdatedBy.FirstName} {entity.UpdatedBy.LastName}".Trim()
                    : null,
                UpdatedAt = entity.UpdatedAt
            };
        }

        public FIUActivityDto MapActivityEntityToDto(FIUActivity entity)
        {
            return new FIUActivityDto
            {
                Id = entity.Id,
                ActivityName = entity.ActivityName,
                ActivityDescription = entity.ActivityDescription,
                ActivityCategory = entity.ActivityCategory,
                UnitOfMeasurement = entity.UnitOfMeasurement,
                RequiresMediaUpload = entity.RequiresMediaUpload,
                IsActive = entity.IsActive,
                DisplayOrder = entity.DisplayOrder
            };
        }
    }
}
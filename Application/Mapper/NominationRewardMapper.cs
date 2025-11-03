using Application.Models.DataTables;
using Domain.Entities.GenericTables;

namespace Application.Mapper
{
    [Mapper]
    public partial class NominationRewardMapper
    {
        // ----------------------------
        // Entity → DTO Mappings (Partial methods remain unchanged, assuming the generator handles all scalar/FK fields)
        // ----------------------------
        public partial NominationRewardDto MapToDto(NominationReward entity);
        public partial NominationRewardIFSFarmerDto MapToDto(NominationRewardIFSFarmer entity);
        public partial NominationRewardIFSEnterpreneurDto MapToDto(NominationRewardIFSEnterpreneur entity);
        public partial NominationRewardFarmerInnovationDto MapToDto(NominationRewardFarmerInnovation entity);
        public partial NominationRewardEntrepreneurInnovationDto MapToDto(NominationRewardEntrepreneurInnovation entity);
        public partial NominationRewardOrganicFarmerDto MapToDto(NominationRewardOrganicFarmer entity);
        public partial NominationRewardOrganicEntrepreneurDto MapToDto(NominationRewardOrganicEntrepreneur entity);

        // ----------------------------
        // DTO → Entity Mappings (Partial methods remain unchanged)
        // ----------------------------
        public partial NominationReward MapToEntity(NominationRewardDto dto);
        public partial NominationRewardIFSFarmer MapToEntity(NominationRewardIFSFarmerDto dto);
        public partial NominationRewardIFSEnterpreneur MapToEntity(NominationRewardIFSEnterpreneurDto dto);
        public partial NominationRewardFarmerInnovation MapToEntity(NominationRewardFarmerInnovationDto dto);
        public partial NominationRewardEntrepreneurInnovation MapToEntity(NominationRewardEntrepreneurInnovationDto dto);
        public partial NominationRewardOrganicFarmer MapToEntity(NominationRewardOrganicFarmerDto dto);
        public partial NominationRewardOrganicEntrepreneur MapToEntity(NominationRewardOrganicEntrepreneurDto dto);

        // ----------------------------
        // Manual Complete Mapping (Updated to include new fields)
        // ----------------------------
        public CompleteNominationRewardDto MapToCompleteDto(NominationReward entity)
        {
            return new CompleteNominationRewardDto
            {
                Id = entity.Id,
                UnitLocationId = entity.UnitLocationId,
                OrganizationId = entity.OrganizationId,
                AwardName = entity.AwardName,
                SpecificContributionTitle = entity.SpecificContributionTitle,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
                FormStatus = entity.FormStatus,
                FormStatusRemarks = entity.FormStatusRemarks,

                // Navigation DTOs
                UnitLocationName = entity.UnitLocation?.Unit?.Name,
                OrganizationName = entity.Organization?.Name,
                TypeName = entity.Type?.Name,
                RegionName = entity.Region?.Name,

                // Child mappings
                IFSFarmers = entity.NominationRewardIFSFarmers.Select(MapToDto).ToList(),
                IFSEntrepreneurs = entity.NominationRewardIFSEntrepreneurs.Select(MapToDto).ToList(),
                FarmerInnovations = entity.NominationRewardFarmerInnovations.Select(MapToDto).ToList(),
                EntrepreneurInnovations = entity.NominationRewardEntrepreneurInnovations.Select(MapToDto).ToList(),
                OrganicFarmers = entity.NominationRewardOrganicFarmers.Select(MapToDto).ToList(),
                OrganicEntrepreneurs = entity.NominationRewardOrganicEntrepreneurs.Select(MapToDto).ToList()
            };
        }

        // ----------------------------
        // Update Mapping (Updated to include all fields for update logic)
        // ----------------------------
        public void MapUpdateDtoToEntity(NominationRewardDto dto, NominationReward entity)
        {
            // Update Foreign Keys
            entity.UnitLocationId = dto.UnitLocationId; // [Required]
            entity.OrganizationId = dto.OrganizationId;
            entity.TypeId = dto.TypeId;
            entity.RegionId = dto.RegionId;
            entity.ContributionId = dto.ContributionId;
            entity.ModeId = dto.ModeId;
            entity.NominationCategoryId = dto.NominationCategoryId;
            entity.InstitutionPositionId = dto.InstitutionPositionId;
            entity.ApprovedById = dto.ApprovedById;

            // Update Date/String/Other Fields
            entity.StartDate = dto.StartDate;
            entity.EndDate = dto.EndDate;
            entity.OtherRegion = dto.OtherRegion;
            entity.AwardName = dto.AwardName;
            entity.OtherContribution = dto.OtherContribution;
            entity.AwardingAgency = dto.AwardingAgency;
            entity.SpecificContributionTitle = dto.SpecificContributionTitle;
            entity.OrganizerInstitutionName = dto.OrganizerInstitutionName;
            entity.OrganizerInstituteAddress = dto.OrganizerInstituteAddress;
            entity.AwardApplicationDate = dto.AwardApplicationDate;
            entity.AwardFilePath = dto.AwardFilePath;
            entity.AwardEventTitle = dto.AwardEventTitle;
            entity.AwardEventDate = dto.AwardEventDate;
            entity.SanctionLetterDate = dto.SanctionLetterDate;
            entity.SanctionLetterFilePath = dto.SanctionLetterFilePath;
            entity.PaperDate = dto.PaperDate;
            entity.PaperFilePath = dto.PaperFilePath;
            entity.AwardReceivingPhoto = dto.AwardReceivingPhoto;
            entity.AwardReceivingCertificate = dto.AwardReceivingCertificate;
            entity.InstitutionBoardName = dto.InstitutionBoardName;
            entity.InstitutionName = dto.InstitutionName;
            entity.InstitutionDesignation = dto.InstitutionDesignation;
            entity.InstitutionAddress = dto.InstitutionAddress;
            entity.PositionFrom = dto.PositionFrom;
            entity.PositionTo = dto.PositionTo;
            entity.DurationDays = dto.DurationDays;
            entity.NominationDate = dto.NominationDate;
            entity.NominationLetterPath = dto.NominationLetterPath;

            // Status/Tracking Fields
            entity.FormStatus = dto.FormStatus; // Not nullable, ensure it's set
            entity.FormStatusRemarks = dto.FormStatusRemarks;
            entity.ApprovedAt = dto.ApprovedAt;

            // Update children (Clear and Re-add logic is correct for full collection replacement)
            entity.NominationRewardIFSFarmers.Clear();
            if (dto.IFSFarmers != null)
                foreach (var child in dto.IFSFarmers)
                    entity.NominationRewardIFSFarmers.Add(MapToEntity(child));

            entity.NominationRewardIFSEntrepreneurs.Clear();
            if (dto.IFSEntrepreneurs != null)
                foreach (var child in dto.IFSEntrepreneurs)
                    entity.NominationRewardIFSEntrepreneurs.Add(MapToEntity(child));

            entity.NominationRewardFarmerInnovations.Clear();
            if (dto.FarmerInnovations != null)
                foreach (var child in dto.FarmerInnovations)
                    entity.NominationRewardFarmerInnovations.Add(MapToEntity(child));

            entity.NominationRewardEntrepreneurInnovations.Clear();
            if (dto.EntrepreneurInnovations != null)
                foreach (var child in dto.EntrepreneurInnovations)
                    entity.NominationRewardEntrepreneurInnovations.Add(MapToEntity(child));

            entity.NominationRewardOrganicFarmers.Clear();
            if (dto.OrganicFarmers != null)
                foreach (var child in dto.OrganicFarmers)
                    entity.NominationRewardOrganicFarmers.Add(MapToEntity(child));

            entity.NominationRewardOrganicEntrepreneurs.Clear();
            if (dto.OrganicEntrepreneurs != null)
                foreach (var child in dto.OrganicEntrepreneurs)
                    entity.NominationRewardOrganicEntrepreneurs.Add(MapToEntity(child));
        }

        public partial NominationRewardDto MapToDtoWithDetails(NominationReward entity);
    }
}
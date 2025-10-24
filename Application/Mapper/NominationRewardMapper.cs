
namespace Application.Mapper
{
    [Mapper]
    public partial class NominationRewardMapper
    {
        // ----------------------------
        // Entity → DTO Mappings
        // ----------------------------
        public partial NominationRewardDto MapToDto(NominationReward entity);
        public partial NominationRewardIFSFarmerDto MapToDto(NominationRewardIFSFarmer entity);
        public partial NominationRewardIFSEnterpreneurDto MapToDto(NominationRewardIFSEnterpreneur entity);
        public partial NominationRewardFarmerInnovationDto MapToDto(NominationRewardFarmerInnovation entity);
        public partial NominationRewardEntrepreneurInnovationDto MapToDto(NominationRewardEntrepreneurInnovation entity);
        public partial NominationRewardOrganicFarmerDto MapToDto(NominationRewardOrganicFarmer entity);
        public partial NominationRewardOrganicEntrepreneurDto MapToDto(NominationRewardOrganicEntrepreneur entity);

        // ----------------------------
        // DTO → Entity Mappings
        // ----------------------------
        public partial NominationReward MapToEntity(NominationRewardDto dto);
        public partial NominationRewardIFSFarmer MapToEntity(NominationRewardIFSFarmerDto dto);
        public partial NominationRewardIFSEnterpreneur MapToEntity(NominationRewardIFSEnterpreneurDto dto);
        public partial NominationRewardFarmerInnovation MapToEntity(NominationRewardFarmerInnovationDto dto);
        public partial NominationRewardEntrepreneurInnovation MapToEntity(NominationRewardEntrepreneurInnovationDto dto);
        public partial NominationRewardOrganicFarmer MapToEntity(NominationRewardOrganicFarmerDto dto);
        public partial NominationRewardOrganicEntrepreneur MapToEntity(NominationRewardOrganicEntrepreneurDto dto);

        // ----------------------------
        // Manual Complete Mapping
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
        // Update Mapping
        // ----------------------------
        public void MapUpdateDtoToEntity(NominationRewardDto dto, NominationReward entity)
        {
            if (dto.AwardName != null) entity.AwardName = dto.AwardName;
            if (dto.SpecificContributionTitle != null) entity.SpecificContributionTitle = dto.SpecificContributionTitle;
            if (dto.StartDate.HasValue) entity.StartDate = dto.StartDate;
            if (dto.EndDate.HasValue) entity.EndDate = dto.EndDate;
            if (dto.FormStatus != null) entity.FormStatus = dto.FormStatus;
            if (dto.FormStatusRemarks != null) entity.FormStatusRemarks = dto.FormStatusRemarks;

            // Update children
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

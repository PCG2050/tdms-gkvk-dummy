using Application.Models.DataTables;
using Domain.Entities.GenericTables;
using System.Linq;

namespace Application.Mapper
{
    public class NominationRewardMapper
    {
        // Parent mappings
        public NominationRewardDto MapToDto(NominationReward entity)
        {
            return new NominationRewardDto
            {
                Id = entity.Id,
                UnitLocationId = entity.UnitLocationId,
                OrganizationId = entity.OrganizationId,
                TypeId = entity.TypeId,
                RegionId = entity.RegionId,
                ContributionId = entity.ContributionId,
                ModeId = entity.ModeId,
                NominationCategoryId = entity.NominationCategoryId,
                PositionId = entity.PositionId,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
                OtherRegion = entity.OtherRegion,
                AwardName = entity.AwardName,
                OtherContribution = entity.OtherContribution,
                AwardingAgency = entity.AwardingAgency,
                SpecificContributionTitle = entity.SpecificContributionTitle,
                OrganizerInstitutionName = entity.OrganizerInstitutionName,
                OrganizerInstituteAddress = entity.OrganizerInstituteAddress,
                AwardApplicationDate = entity.AwardApplicationDate,
                AwardFilePath = entity.AwardFilePath,
                AwardEventTitle = entity.AwardEventTitle,
                AwardEventDate = entity.AwardEventDate,
                SanctionLetterDate = entity.SanctionLetterDate,
                SanctionLetterFilePath = entity.SanctionLetterFilePath,
                PaperDate = entity.PaperDate,
                PaperFilePath = entity.PaperFilePath,
                AwardReceivingPhoto = entity.AwardReceivingPhoto,
                AwardReceivingCertificate = entity.AwardReceivingCertificate,
                InstitutionBoardName = entity.InstitutionBoardName,
                InstitutionName = entity.InstitutionName,
                InstitutionDesignation = entity.InstitutionDesignation,
                InstitutionAddress = entity.InstitutionAddress,
                PositionFrom = entity.PositionFrom,
                PositionTo = entity.PositionTo,
                DurationDays = entity.DurationDays,
                NominationDate = entity.NominationDate,
                NominationLetterPath = entity.NominationLetterPath,
                FormStatus = entity.FormStatus,
                FormStatusRemarks = entity.FormStatusRemarks,
                ApprovedAt = entity.ApprovedAt,
                ApprovedById = entity.ApprovedById
            };
        }

        public NominationReward MapToEntity(NominationRewardDto dto)
        {
            var entity = new NominationReward
            {
                UnitLocationId = dto.UnitLocationId,
                OrganizationId = dto.OrganizationId,
                TypeId = dto.TypeId,
                RegionId = dto.RegionId,
                ContributionId = dto.ContributionId,
                ModeId = dto.ModeId,
                NominationCategoryId = dto.NominationCategoryId,
                PositionId = dto.PositionId,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                OtherRegion = dto.OtherRegion,
                AwardName = dto.AwardName,
                OtherContribution = dto.OtherContribution,
                AwardingAgency = dto.AwardingAgency,
                SpecificContributionTitle = dto.SpecificContributionTitle,
                OrganizerInstitutionName = dto.OrganizerInstitutionName,
                OrganizerInstituteAddress = dto.OrganizerInstituteAddress,
                AwardApplicationDate = dto.AwardApplicationDate,
                AwardFilePath = dto.AwardFilePath,
                AwardEventTitle = dto.AwardEventTitle,
                AwardEventDate = dto.AwardEventDate,
                SanctionLetterDate = dto.SanctionLetterDate,
                SanctionLetterFilePath = dto.SanctionLetterFilePath,
                PaperDate = dto.PaperDate,
                PaperFilePath = dto.PaperFilePath,
                AwardReceivingPhoto = dto.AwardReceivingPhoto,
                AwardReceivingCertificate = dto.AwardReceivingCertificate,
                InstitutionBoardName = dto.InstitutionBoardName,
                InstitutionName = dto.InstitutionName,
                InstitutionDesignation = dto.InstitutionDesignation,
                InstitutionAddress = dto.InstitutionAddress,
                PositionFrom = dto.PositionFrom,
                PositionTo = dto.PositionTo,
                DurationDays = dto.DurationDays,
                NominationDate = dto.NominationDate,
                NominationLetterPath = dto.NominationLetterPath,
                FormStatus = dto.FormStatus,
                FormStatusRemarks = dto.FormStatusRemarks,
                ApprovedAt = dto.ApprovedAt,
                ApprovedById = dto.ApprovedById
            };

            // Add children
            if (dto.IFSFarmers != null)
                foreach (var c in dto.IFSFarmers)
                    entity.NominationRewardIFSFarmers.Add(MapToEntity(c));

            if (dto.IFSEntrepreneurs != null)
                foreach (var c in dto.IFSEntrepreneurs)
                    entity.NominationRewardIFSEntrepreneurs.Add(MapToEntity(c));

            if (dto.FarmerInnovations != null)
                foreach (var c in dto.FarmerInnovations)
                    entity.NominationRewardFarmerInnovations.Add(MapToEntity(c));

            if (dto.EntrepreneurInnovations != null)
                foreach (var c in dto.EntrepreneurInnovations)
                    entity.NominationRewardEntrepreneurInnovations.Add(MapToEntity(c));

            if (dto.OrganicFarmers != null)
                foreach (var c in dto.OrganicFarmers)
                    entity.NominationRewardOrganicFarmers.Add(MapToEntity(c));

            if (dto.OrganicEntrepreneurs != null)
                foreach (var c in dto.OrganicEntrepreneurs)
                    entity.NominationRewardOrganicEntrepreneurs.Add(MapToEntity(c));

            return entity;
        }

        // Child mappings
        public NominationRewardIFSFarmer MapToEntity(NominationRewardIFSFarmerDto dto) =>
            new() { NameAddress = dto.NameAddress, Phone = dto.Phone, ComponentOfIFS = dto.ComponentOfIFS };

        public NominationRewardIFSEnterpreneur MapToEntity(NominationRewardIFSEnterpreneurDto dto) =>
            new() { NameAddress = dto.NameAddress, Phone = dto.Phone, ComponentOfIFS = dto.ComponentOfIFS };

        public NominationRewardFarmerInnovation MapToEntity(NominationRewardFarmerInnovationDto dto) =>
            new() { Type = dto.Type, NameAddress = dto.NameAddress, PhoneNumber = dto.PhoneNumber, DetailsOfInnovation = dto.DetailsOfInnovation };

        public NominationRewardEntrepreneurInnovation MapToEntity(NominationRewardEntrepreneurInnovationDto dto) =>
            new() { Type = dto.Type, NameAddress = dto.NameAddress, PhoneNumber = dto.PhoneNumber, DetailsOfInnovation = dto.DetailsOfInnovation };

        public NominationRewardOrganicFarmer MapToEntity(NominationRewardOrganicFarmerDto dto) =>
            new() { NameAddress = dto.NameAddress, PhoneNumber = dto.PhoneNumber, CropsGrown = dto.CropsGrown };

        public NominationRewardOrganicEntrepreneur MapToEntity(NominationRewardOrganicEntrepreneurDto dto) =>
            new() { NameAddress = dto.NameAddress, PhoneNumber = dto.PhoneNumber, CropsGrown = dto.CropsGrown };

        public NominationRewardIFSFarmerDto MapToDto(NominationRewardIFSFarmer entity) =>
            new() { Id = entity.Id, NameAddress = entity.NameAddress, Phone = entity.Phone, ComponentOfIFS = entity.ComponentOfIFS };

        public NominationRewardIFSEnterpreneurDto MapToDto(NominationRewardIFSEnterpreneur entity) =>
            new() { Id = entity.Id, NameAddress = entity.NameAddress, Phone = entity.Phone, ComponentOfIFS = entity.ComponentOfIFS };

        public NominationRewardFarmerInnovationDto MapToDto(NominationRewardFarmerInnovation entity) =>
            new() { Id = entity.Id, Type = entity.Type, NameAddress = entity.NameAddress, PhoneNumber = entity.PhoneNumber, DetailsOfInnovation = entity.DetailsOfInnovation };

        public NominationRewardEntrepreneurInnovationDto MapToDto(NominationRewardEntrepreneurInnovation entity) =>
            new() { Id = entity.Id, Type = entity.Type, NameAddress = entity.NameAddress, PhoneNumber = entity.PhoneNumber, DetailsOfInnovation = entity.DetailsOfInnovation };

        public NominationRewardOrganicFarmerDto MapToDto(NominationRewardOrganicFarmer entity) =>
            new() { Id = entity.Id, NameAddress = entity.NameAddress, PhoneNumber = entity.PhoneNumber, CropsGrown = entity.CropsGrown };

        public NominationRewardOrganicEntrepreneurDto MapToDto(NominationRewardOrganicEntrepreneur entity) =>
            new() { Id = entity.Id, NameAddress = entity.NameAddress, PhoneNumber = entity.PhoneNumber, CropsGrown = entity.CropsGrown };

        public NominationRewardDto MapToDtoWithDetails(NominationReward entity)
        {
            var dto = MapToDto(entity);
            dto.UnitLocationName = entity.UnitLocation?.Unit?.Name;
            dto.OrganizationName = entity.Organization?.Name;
            dto.TypeName = entity.Type?.Name;
            dto.RegionName = entity.Region?.Name;
            dto.IFSFarmers = entity.NominationRewardIFSFarmers?.Select(MapToDto).ToList() ?? new();
            dto.IFSEntrepreneurs = entity.NominationRewardIFSEntrepreneurs?.Select(MapToDto).ToList() ?? new();
            dto.FarmerInnovations = entity.NominationRewardFarmerInnovations?.Select(MapToDto).ToList() ?? new();
            dto.EntrepreneurInnovations = entity.NominationRewardEntrepreneurInnovations?.Select(MapToDto).ToList() ?? new();
            dto.OrganicFarmers = entity.NominationRewardOrganicFarmers?.Select(MapToDto).ToList() ?? new();
            dto.OrganicEntrepreneurs = entity.NominationRewardOrganicEntrepreneurs?.Select(MapToDto).ToList() ?? new();
            return dto;
        }

        public CompleteNominationRewardDto MapToCompleteDto(NominationReward entity) =>
            new()
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
                IFSFarmers = entity.NominationRewardIFSFarmers?.Select(MapToDto).ToList() ?? new(),
                IFSEntrepreneurs = entity.NominationRewardIFSEntrepreneurs?.Select(MapToDto).ToList() ?? new(),
                FarmerInnovations = entity.NominationRewardFarmerInnovations?.Select(MapToDto).ToList() ?? new(),
                EntrepreneurInnovations = entity.NominationRewardEntrepreneurInnovations?.Select(MapToDto).ToList() ?? new(),
                OrganicFarmers = entity.NominationRewardOrganicFarmers?.Select(MapToDto).ToList() ?? new(),
                OrganicEntrepreneurs = entity.NominationRewardOrganicEntrepreneurs?.Select(MapToDto).ToList() ?? new()
            };

        public void MapUpdateDtoToEntity(NominationRewardDto dto, NominationReward entity)
        {
            // Update all parent fields
            entity.TypeId = dto.TypeId;
            entity.RegionId = dto.RegionId;
            entity.ContributionId = dto.ContributionId;
            entity.ModeId = dto.ModeId;
            entity.NominationCategoryId = dto.NominationCategoryId;
            entity.PositionId = dto.PositionId;
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
            entity.FormStatus = dto.FormStatus;
            entity.FormStatusRemarks = dto.FormStatusRemarks;
            entity.ApprovedAt = dto.ApprovedAt;
            entity.ApprovedById = dto.ApprovedById;

            // Update child collections
            entity.NominationRewardIFSFarmers.Clear();
            if (dto.IFSFarmers != null)
                foreach (var c in dto.IFSFarmers)
                    entity.NominationRewardIFSFarmers.Add(MapToEntity(c));

            entity.NominationRewardIFSEntrepreneurs.Clear();
            if (dto.IFSEntrepreneurs != null)
                foreach (var c in dto.IFSEntrepreneurs)
                    entity.NominationRewardIFSEntrepreneurs.Add(MapToEntity(c));

            entity.NominationRewardFarmerInnovations.Clear();
            if (dto.FarmerInnovations != null)
                foreach (var c in dto.FarmerInnovations)
                    entity.NominationRewardFarmerInnovations.Add(MapToEntity(c));

            entity.NominationRewardEntrepreneurInnovations.Clear();
            if (dto.EntrepreneurInnovations != null)
                foreach (var c in dto.EntrepreneurInnovations)
                    entity.NominationRewardEntrepreneurInnovations.Add(MapToEntity(c));

            entity.NominationRewardOrganicFarmers.Clear();
            if (dto.OrganicFarmers != null)
                foreach (var c in dto.OrganicFarmers)
                    entity.NominationRewardOrganicFarmers.Add(MapToEntity(c));

            entity.NominationRewardOrganicEntrepreneurs.Clear();
            if (dto.OrganicEntrepreneurs != null)
                foreach (var c in dto.OrganicEntrepreneurs)
                    entity.NominationRewardOrganicEntrepreneurs.Add(MapToEntity(c));
        }

        // ===== HYBRID DTO MAPPERS =====

        // Map Create DTOs to entities (no Id field)
        public NominationRewardIFSFarmer MapToEntity(NominationRewardIFSFarmerCreateDto dto) =>
            new() { NameAddress = dto.NameAddress, Phone = dto.Phone, ComponentOfIFS = dto.ComponentOfIFS };

        public NominationRewardIFSEnterpreneur MapToEntity(NominationRewardIFSEnterpreneurCreateDto dto) =>
            new() { NameAddress = dto.NameAddress, Phone = dto.Phone, ComponentOfIFS = dto.ComponentOfIFS };

        public NominationRewardFarmerInnovation MapToEntity(NominationRewardFarmerInnovationCreateDto dto) =>
            new() { Type = dto.Type, NameAddress = dto.NameAddress, PhoneNumber = dto.PhoneNumber, DetailsOfInnovation = dto.DetailsOfInnovation };

        public NominationRewardEntrepreneurInnovation MapToEntity(NominationRewardEntrepreneurInnovationCreateDto dto) =>
            new() { Type = dto.Type, NameAddress = dto.NameAddress, PhoneNumber = dto.PhoneNumber, DetailsOfInnovation = dto.DetailsOfInnovation };

        public NominationRewardOrganicFarmer MapToEntity(NominationRewardOrganicFarmerCreateDto dto) =>
            new() { NameAddress = dto.NameAddress, PhoneNumber = dto.PhoneNumber, CropsGrown = dto.CropsGrown };

        public NominationRewardOrganicEntrepreneur MapToEntity(NominationRewardOrganicEntrepreneurCreateDto dto) =>
            new() { NameAddress = dto.NameAddress, PhoneNumber = dto.PhoneNumber, CropsGrown = dto.CropsGrown };

        // Map Update DTOs to entities (with Id field)
        public NominationRewardIFSFarmer MapToEntity(NominationRewardIFSFarmerUpdateDto dto) =>
            new() { Id = dto.Id ?? 0, NameAddress = dto.NameAddress, Phone = dto.Phone, ComponentOfIFS = dto.ComponentOfIFS };

        public NominationRewardIFSEnterpreneur MapToEntity(NominationRewardIFSEnterpreneurUpdateDto dto) =>
            new() { Id = dto.Id ?? 0, NameAddress = dto.NameAddress, Phone = dto.Phone, ComponentOfIFS = dto.ComponentOfIFS };

        public NominationRewardFarmerInnovation MapToEntity(NominationRewardFarmerInnovationUpdateDto dto) =>
            new() { Id = dto.Id ?? 0, Type = dto.Type, NameAddress = dto.NameAddress, PhoneNumber = dto.PhoneNumber, DetailsOfInnovation = dto.DetailsOfInnovation };

        public NominationRewardEntrepreneurInnovation MapToEntity(NominationRewardEntrepreneurInnovationUpdateDto dto) =>
            new() { Id = dto.Id ?? 0, Type = dto.Type, NameAddress = dto.NameAddress, PhoneNumber = dto.PhoneNumber, DetailsOfInnovation = dto.DetailsOfInnovation };

        public NominationRewardOrganicFarmer MapToEntity(NominationRewardOrganicFarmerUpdateDto dto) =>
            new() { Id = dto.Id ?? 0, NameAddress = dto.NameAddress, PhoneNumber = dto.PhoneNumber, CropsGrown = dto.CropsGrown };

        public NominationRewardOrganicEntrepreneur MapToEntity(NominationRewardOrganicEntrepreneurUpdateDto dto) =>
            new() { Id = dto.Id ?? 0, NameAddress = dto.NameAddress, PhoneNumber = dto.PhoneNumber, CropsGrown = dto.CropsGrown };
    }
}
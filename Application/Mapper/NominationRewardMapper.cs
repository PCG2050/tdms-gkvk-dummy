using Application.Models.DataTables;
using Domain.Entities.GenericTables;
using System.Linq;

namespace Application.Mapper
{
    public class NominationRewardMapper
    {
        // ===== PARENT MAPPINGS =====
        public NominationRewardDto MapToDto(NominationReward entity)
        {
            return new NominationRewardDto
            {
                Id = entity.Id,
                UnitLocationId = entity.UnitLocationId,
                OrganizationId = entity.OrganizationId,
                TypeId = entity.TypeId,
                RegionId = entity.RegionId,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
                OtherType = entity.OtherType,
                OtherRegion = entity.OtherRegion,
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
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                OtherType = dto.OtherType,
                OtherRegion = dto.OtherRegion,
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

            if (dto.Achievements != null)
                foreach (var c in dto.Achievements)
                    entity.Achievements.Add(MapToEntity(c));

            if (dto.AwardRecognitions != null)
                foreach (var c in dto.AwardRecognitions)
                    entity.AwardRecognitions.Add(MapToEntity(c));

            if (dto.UniversitySanctionLetterPaperPosters != null)
                foreach (var c in dto.UniversitySanctionLetterPaperPosters)
                    entity.UniversitySanctionLetterPaperPosters.Add(MapToEntity(c));

            if (dto.AwardPhotos != null)
                foreach (var c in dto.AwardPhotos)
                    entity.AwardPhotos.Add(MapToEntity(c));

            return entity;
        }

        // ===== CHILD MAPPINGS (Original 6) =====
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

        // ===== CHILD MAPPINGS (New 4) =====
        public Achievement MapToEntity(AchievementDto dto) =>
            new() { Name = dto.Name, Phone = dto.Phone, AchievementDetail = dto.AchievementDetail };

        public AwardRecognition MapToEntity(AwardRecognitionDto dto) =>
            new() { AwardName = dto.AwardName, ContributionId = dto.ContributionId, OtherContribution = dto.OtherContribution, AwardingAgency = dto.AwardingAgency, InstitutionName = dto.InstitutionName, InstitutionAddress = dto.InstitutionAddress };

        public UniversitySanctionLetterPaperPoster MapToEntity(UniversitySanctionLetterPaperPosterDto dto) =>
            new() { SanctionLetterDate = dto.SanctionLetterDate, SanctionLetterFilePath = dto.SanctionLetterFilePath, PaperDate = dto.PaperDate, PaperFilePath = dto.PaperFilePath };

        public AwardPhoto MapToEntity(AwardPhotoDto dto) =>
            new() { AwardReceivingPhoto = dto.AwardReceivingPhoto, AwardReceivingCertificate = dto.AwardReceivingCertificate };

        public AchievementDto MapToDto(Achievement entity) =>
            new() { Id = entity.Id, Name = entity.Name, Phone = entity.Phone, AchievementDetail = entity.AchievementDetail };

        public AwardRecognitionDto MapToDto(AwardRecognition entity) =>
            new() { Id = entity.Id, AwardName = entity.AwardName, ContributionId = entity.ContributionId, OtherContribution = entity.OtherContribution, AwardingAgency = entity.AwardingAgency, InstitutionName = entity.InstitutionName, InstitutionAddress = entity.InstitutionAddress };

        public UniversitySanctionLetterPaperPosterDto MapToDto(UniversitySanctionLetterPaperPoster entity) =>
            new() { Id = entity.Id, SanctionLetterDate = entity.SanctionLetterDate, SanctionLetterFilePath = entity.SanctionLetterFilePath, PaperDate = entity.PaperDate, PaperFilePath = entity.PaperFilePath };

        public AwardPhotoDto MapToDto(AwardPhoto entity) =>
            new() { Id = entity.Id, AwardReceivingPhoto = entity.AwardReceivingPhoto, AwardReceivingCertificate = entity.AwardReceivingCertificate };

        // ===== DETAILED DTO WITH NAVIGATION =====
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
            dto.Achievements = entity.Achievements?.Select(MapToDto).ToList() ?? new();
            dto.AwardRecognitions = entity.AwardRecognitions?.Select(MapToDto).ToList() ?? new();
            dto.UniversitySanctionLetterPaperPosters = entity.UniversitySanctionLetterPaperPosters?.Select(MapToDto).ToList() ?? new();
            dto.AwardPhotos = entity.AwardPhotos?.Select(MapToDto).ToList() ?? new();
            return dto;
        }

        public CompleteNominationRewardDto MapToCompleteDto(NominationReward entity) =>
            new()
            {
                Id = entity.Id,
                UnitLocationId = entity.UnitLocationId,
                OrganizationId = entity.OrganizationId,
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
                OrganicEntrepreneurs = entity.NominationRewardOrganicEntrepreneurs?.Select(MapToDto).ToList() ?? new(),
                Achievements = entity.Achievements?.Select(MapToDto).ToList() ?? new(),
                AwardRecognitions = entity.AwardRecognitions?.Select(MapToDto).ToList() ?? new(),
                UniversitySanctionLetterPaperPosters = entity.UniversitySanctionLetterPaperPosters?.Select(MapToDto).ToList() ?? new(),
                AwardPhotos = entity.AwardPhotos?.Select(MapToDto).ToList() ?? new()
            };

        public void MapUpdateDtoToEntity(NominationRewardDto dto, NominationReward entity)
        {
            // Update all parent fields
            entity.TypeId = dto.TypeId;
            entity.RegionId = dto.RegionId;
            entity.StartDate = dto.StartDate;
            entity.EndDate = dto.EndDate;
            entity.OtherType = dto.OtherType;
            entity.OtherRegion = dto.OtherRegion;
            entity.FormStatus = dto.FormStatus;
            entity.FormStatusRemarks = dto.FormStatusRemarks;
            entity.ApprovedAt = dto.ApprovedAt;
            entity.ApprovedById = dto.ApprovedById;

            // Update child collections - clear and add new
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

            entity.Achievements.Clear();
            if (dto.Achievements != null)
                foreach (var c in dto.Achievements)
                    entity.Achievements.Add(MapToEntity(c));

            entity.AwardRecognitions.Clear();
            if (dto.AwardRecognitions != null)
                foreach (var c in dto.AwardRecognitions)
                    entity.AwardRecognitions.Add(MapToEntity(c));

            entity.UniversitySanctionLetterPaperPosters.Clear();
            if (dto.UniversitySanctionLetterPaperPosters != null)
                foreach (var c in dto.UniversitySanctionLetterPaperPosters)
                    entity.UniversitySanctionLetterPaperPosters.Add(MapToEntity(c));

            entity.AwardPhotos.Clear();
            if (dto.AwardPhotos != null)
                foreach (var c in dto.AwardPhotos)
                    entity.AwardPhotos.Add(MapToEntity(c));
        }

        // ===== HYBRID DTO MAPPERS =====

        // Map Create DTOs to entities (no Id field) - Original 6
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

        // Map Create DTOs to entities (no Id field) - New 4
        public Achievement MapToEntity(AchievementCreateDto dto) =>
            new() { Name = dto.Name, Phone = dto.Phone, AchievementDetail = dto.AchievementDetail };

        public AwardRecognition MapToEntity(AwardRecognitionCreateDto dto) =>
            new() { AwardName = dto.AwardName, ContributionId = dto.ContributionId, OtherContribution = dto.OtherContribution, AwardingAgency = dto.AwardingAgency, InstitutionName = dto.InstitutionName, InstitutionAddress = dto.InstitutionAddress };

        public UniversitySanctionLetterPaperPoster MapToEntity(UniversitySanctionLetterPaperPosterCreateDto dto) =>
            new() { SanctionLetterDate = dto.SanctionLetterDate, SanctionLetterFilePath = dto.SanctionLetterFilePath, PaperDate = dto.PaperDate, PaperFilePath = dto.PaperFilePath };

        public AwardPhoto MapToEntity(AwardPhotoCreateDto dto) =>
            new() { AwardReceivingPhoto = dto.AwardReceivingPhoto, AwardReceivingCertificate = dto.AwardReceivingCertificate };

        // Map Update DTOs to entities (with Id field) - Original 6
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

        // Map Update DTOs to entities (with Id field) - New 4
        public Achievement MapToEntity(AchievementUpdateDto dto) =>
            new() { Id = dto.Id ?? 0, Name = dto.Name, Phone = dto.Phone, AchievementDetail = dto.AchievementDetail };

        public AwardRecognition MapToEntity(AwardRecognitionUpdateDto dto) =>
            new() { Id = dto.Id ?? 0, AwardName = dto.AwardName, ContributionId = dto.ContributionId, OtherContribution = dto.OtherContribution, AwardingAgency = dto.AwardingAgency, InstitutionName = dto.InstitutionName, InstitutionAddress = dto.InstitutionAddress };

        public UniversitySanctionLetterPaperPoster MapToEntity(UniversitySanctionLetterPaperPosterUpdateDto dto) =>
            new() { Id = dto.Id ?? 0, SanctionLetterDate = dto.SanctionLetterDate, SanctionLetterFilePath = dto.SanctionLetterFilePath, PaperDate = dto.PaperDate, PaperFilePath = dto.PaperFilePath };

        public AwardPhoto MapToEntity(AwardPhotoUpdateDto dto) =>
            new() { Id = dto.Id ?? 0, AwardReceivingPhoto = dto.AwardReceivingPhoto, AwardReceivingCertificate = dto.AwardReceivingCertificate };
    }
}

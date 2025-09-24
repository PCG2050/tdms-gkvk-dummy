using System;
using System.Collections.Generic;
using Domain.Entities.MasterData;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.GenericTables
{

        public class NominationReward : ReportEntryBaseEntity
        {
            
            // === Foreign Keys & Navigation Properties ===
            public int? TypeId { get; set; }
            public NominationType? Type { get; set; }

            public int? RegionId { get; set; }
            public Region? Region { get; set; }

            public int? ContributionId { get; set; }
            public Contribution? Contribution { get; set; }

            public int? ModeId { get; set; }
            public Mode? Mode { get; set; }

            public int? NominationCategoryId { get; set; }
            public NominationCategory? NominationCategory { get; set; }

            public int? InstitutionPositionId { get; set; }
            public Position? InstitutionPosition { get; set; }

            // === Other Fields ===
            public string? OtherRegion { get; set; }
            public string? AwardName { get; set; }
            public string? OtherContribution { get; set; }
            public string? AwardingAgency { get; set; }
            public string? SpecificContributionTitle { get; set; }

            public string? OrganizerInstitutionName { get; set; }
            public string? OrganizerInstituteAddress { get; set; }

            public DateOnly? AwardApplicationDate { get; set; }
            public string? AwardFilePath { get; set; }
            public string? AwardEventTitle { get; set; }
            public DateOnly? AwardEventDate { get; set; }
            public DateOnly? SanctionLetterDate { get; set; }
            public string? SanctionLetterFilePath { get; set; }
            public DateOnly? PaperDate { get; set; }
            public string? PaperFilePath { get; set; }
            public string? AwardReceivingPhoto { get; set; }
            public string? AwardReceivingCertificate { get; set; }

            public string? InstitutionBoardName { get; set; }
            public string? InstitutionName { get; set; }
            public string? InstitutionDesignation { get; set; }
            public string? InstitutionAddress { get; set; }

            public DateOnly? PositionFrom { get; set; }
            public DateOnly? PositionTo { get; set; }
            public int? DurationDays { get; set; }

            public DateOnly? NominationDate { get; set; }
            public string? NominationLetterPath { get; set; }

            // === Child Collections ===
            public ICollection<NominationRewardIFSFarmer> NominationRewardIFSFarmers { get; set; } = [];
            public ICollection<NominationRewardFarmerInnovation> NominationRewardFarmerInnovations { get; set; } = [];
            public ICollection<NominationRewardOrganicFarmer> NominationRewardOrganicFarmers { get; set; } = [];
            public ICollection<NominationRewardIFSEnterpreneur> NominationRewardIFSEntrepreneurs { get; set; } = [];
            public ICollection<NominationRewardEntrepreneurInnovation> NominationRewardEntrepreneurInnovations { get; set; } = [];
            public ICollection<NominationRewardOrganicEntrepreneur> NominationRewardOrganicEntrepreneurs { get; set; } = [];
        }

        // === Child Entities ===
        public class NominationRewardIFSFarmer : AuditableBaseEntity
        {
            public string? NameAddress { get; set; }
            public string? Phone { get; set; }
            public string? ComponentOfIFS { get; set; }

            public int NominationRewardId { get; set; }
            public NominationReward NominationReward { get; set; } = default!;
        }

        public class NominationRewardFarmerInnovation : AuditableBaseEntity
        {
            public string? Type { get; set; }
            public string? NameAddress { get; set; }
            public int? PhoneNumber { get; set; }
            public string? DetailsOfInnovation { get; set; }

            public int NominationRewardId { get; set; }
            public NominationReward NominationReward { get; set; } = default!;
        }

        public class NominationRewardOrganicFarmer : AuditableBaseEntity
        {
            public string? NameAddress { get; set; }
            public int? PhoneNumber { get; set; }
            public string? CropsGrown { get; set; }
            public int NominationRewardId { get; set; }
            public NominationReward NominationReward { get; set; } = default!;
        }

        public class NominationRewardIFSEnterpreneur : AuditableBaseEntity
        {
            public string? NameAddress { get; set; }
            public string? Phone { get; set; }
            public string? ComponentOfIFS { get; set; }

            public int NominationRewardId { get; set; }
            public NominationReward NominationReward { get; set; } = default!;
        }

        public class NominationRewardEntrepreneurInnovation : AuditableBaseEntity
        {
            public string? Type { get; set; }
            public string? NameAddress { get; set; }
            public int? PhoneNumber { get; set; }
            public string? DetailsOfInnovation { get; set; }

            public int NominationRewardId { get; set; }

            public NominationReward NominationReward { get; set; } = default!;
        }

        public class NominationRewardOrganicEntrepreneur : AuditableBaseEntity
        {
            public string? NameAddress { get; set; }
            public int? PhoneNumber { get; set; }
            public string? CropsGrown { get; set; }

            public int NominationRewardId { get; set; }
            public NominationReward NominationReward { get; set; } = default!;
        }

    
    
}

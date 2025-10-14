using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.DataTables
{
    public class NominationRewardDto
    {
        public int Id { get; set; }
        public int UnitLocationId { get; set; }
        public int OrganizationId { get; set; }

        public string? AwardName { get; set; }
        public string? SpecificContributionTitle { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }

        public string FormStatus { get; set; } = "Draft";
        public string? FormStatusRemarks { get; set; }

        // Navigation DTOs
        public string? UnitLocationName { get; set; }
        public string? OrganizationName { get; set; }
        public string? TypeName { get; set; }
        public string? RegionName { get; set; }

        // Child Collections
        public List<NominationRewardIFSFarmerDto> IFSFarmers { get; set; } = new();
        public List<NominationRewardIFSEnterpreneurDto> IFSEntrepreneurs { get; set; } = new();
        public List<NominationRewardFarmerInnovationDto> FarmerInnovations { get; set; } = new();
        public List<NominationRewardEntrepreneurInnovationDto> EntrepreneurInnovations { get; set; } = new();
        public List<NominationRewardOrganicFarmerDto> OrganicFarmers { get; set; } = new();
        public List<NominationRewardOrganicEntrepreneurDto> OrganicEntrepreneurs { get; set; } = new();
    }

    public class NominationRewardIFSFarmerDto
    {
        public int Id { get; set; }
        public string? NameAddress { get; set; }
        public string? Phone { get; set; }
    }

    public class NominationRewardIFSEnterpreneurDto
    {
        public int Id { get; set; }
        public string? NameAddress { get; set; }
        public string? Phone { get; set; }
    }

    public class NominationRewardFarmerInnovationDto
    {
        public int Id { get; set; }
        public string? Type { get; set; }
        public string? NameAddress { get; set; }
        public string? DetailsOfInnovation { get; set; }
    }

    public class NominationRewardEntrepreneurInnovationDto
    {
        public int Id { get; set; }
        public string? Type { get; set; }
        public string? NameAddress { get; set; }
        public string? DetailsOfInnovation { get; set; }
    }

    public class NominationRewardOrganicFarmerDto
    {
        public int Id { get; set; }
        public string? NameAddress { get; set; }
        public string? CropsGrown { get; set; }
    }

    public class NominationRewardOrganicEntrepreneurDto
    {
        public int Id { get; set; }
        public string? NameAddress { get; set; }
        public string? CropsGrown { get; set; }
    }
    public class CompleteNominationRewardDto
    {
        public int Id { get; set; }
        public int UnitLocationId { get; set; }
        public int OrganizationId { get; set; }

        public string? AwardName { get; set; }
        public string? SpecificContributionTitle { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }

        public string FormStatus { get; set; } = "Draft";
        public string? FormStatusRemarks { get; set; }

        // Navigation DTOs
        public string? UnitLocationName { get; set; }
        public string? OrganizationName { get; set; }
        public string? TypeName { get; set; }
        public string? RegionName { get; set; }

        // All child collections
        public List<NominationRewardIFSFarmerDto> IFSFarmers { get; set; } = new();
        public List<NominationRewardIFSEnterpreneurDto> IFSEntrepreneurs { get; set; } = new();
        public List<NominationRewardFarmerInnovationDto> FarmerInnovations { get; set; } = new();
        public List<NominationRewardEntrepreneurInnovationDto> EntrepreneurInnovations { get; set; } = new();
        public List<NominationRewardOrganicFarmerDto> OrganicFarmers { get; set; } = new();
        public List<NominationRewardOrganicEntrepreneurDto> OrganicEntrepreneurs { get; set; } = new();
    }
}

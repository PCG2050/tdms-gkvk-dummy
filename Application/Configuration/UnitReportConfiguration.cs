using System;
using System.Collections.Generic;
using Application.Constants;
using Domain.Entities.FTI;
using Domain.Entities.STU;
using Domain.Entities.FIU;
using Domain.Entities.IBTVA;
using Domain.Entities.ATIC;
using Domain.Entities.DEU;
using Domain.Entities.ASM;
using Domain.Entities.NAEP;
using Domain.Entities.EEU;
using Domain.Entities.KVK;
using Domain.Entities.SAMETI;

namespace Application.Configuration
{
    /// <summary>
    /// Configuration for a unit's reporting capabilities
    /// </summary>
    public class UnitReportConfiguration
    {
        public int UnitId { get; set; }
        public required string UnitName { get; set; }
        public required string EntityNamespace { get; set; }

        // Entity types
        public Type? ProgramDetailsType { get; set; }
        public Type? ReportType { get; set; }
        public Type? ParticipantDemographicsType { get; set; }
        public Type? AdvisoryServicesType { get; set; }
        public Type? ProgramContentType { get; set; }
        public Type? RecommendationType { get; set; }

        // Special entity types
        public Type? TrainingProgrammeType { get; set; }
        public Type? ResultType { get; set; }
        public Type? SalesType { get; set; }
        public Type? VisitorDetailsType { get; set; }
        public Type? ProgramActivityType { get; set; }
        public Type? OtherActivityType { get; set; }

        // Unit capabilities
        public bool HasResults { get; set; }
        public bool IsSalesUnit { get; set; }
        public bool IsVisitorUnit { get; set; }
        public bool IsActivityUnit { get; set; }

        // Unique fields specific to this unit
        public List<string> UniqueFields { get; set; } = new();

        // Available entity types for reporting
        public List<string> AvailableEntities { get; set; } = new();
    }

    /// <summary>
    /// Static configuration provider for all units
    /// </summary>
    public static class UnitConfigurations
    {
        private static readonly Dictionary<int, UnitReportConfiguration> _configurations = new()
        {
            // ===== FTI - Farmers Training Institute =====
            {
                UnitConstants.FTI_UNIT_ID,
                new UnitReportConfiguration
                {
                    UnitId = UnitConstants.FTI_UNIT_ID,
                    UnitName = "Farmers Training Institute",
                    EntityNamespace = "Domain.Entities.FTI",
                    ProgramDetailsType = typeof(FtiProgramDetails),
                    ReportType = typeof(FtiReport),
                    ParticipantDemographicsType = typeof(FtiParticipantDemographics),
                    AdvisoryServicesType = typeof(FtiAdvisoryServices),
                    ProgramContentType = typeof(FtiProgramContentAndResources),
                    RecommendationType = typeof(FtiRecommendation),
                    TrainingProgrammeType = typeof(FtiTrainingProgram),
                    OtherActivityType = typeof(FtiOtherActivity),
                    UniqueFields = new List<string> { "TPNo", "BatchNo" },
                    AvailableEntities = new List<string>
                    {
                        "ProgramDetails", "Report", "ParticipantDemographics",
                        "AdvisoryServices", "TrainingProgrammes", "OtherActivities"
                    },
                    HasResults = false,
                    IsSalesUnit = false,
                    IsVisitorUnit = false,
                    IsActivityUnit = false
                }
            },

            // ===== STU - Staff Training Unit =====
            {
                UnitConstants.STU_UNIT_ID,
                new UnitReportConfiguration
                {
                    UnitId = UnitConstants.STU_UNIT_ID,
                    UnitName = "Staff Training Unit",
                    EntityNamespace = "Domain.Entities.STU",
                    ProgramDetailsType = typeof(StuProgramDetails),
                    ReportType = typeof(StuReport),
                    ParticipantDemographicsType = typeof(StuParticipantDemographics),
                    AdvisoryServicesType = typeof(StuAdvisoryServices),
                    ProgramContentType = typeof(StuProgramContentAndResources),
                    RecommendationType = typeof(StuRecommendation),
                    OtherActivityType = typeof(StuOtherActivity),
                    UniqueFields = new List<string> { "TPNo", "BatchNo" },
                    AvailableEntities = new List<string>
                    {
                        "ProgramDetails", "Report", "ParticipantDemographics",
                        "AdvisoryServices", "OtherActivities"
                    },
                    HasResults = false,
                    IsSalesUnit = false,
                    IsVisitorUnit = false,
                    IsActivityUnit = false
                }
            },

            // ===== FIU - Farm Information Unit =====
            {
                UnitConstants.FIU_UNIT_ID,
                new UnitReportConfiguration
                {
                    UnitId = UnitConstants.FIU_UNIT_ID,
                    UnitName = "Farm Information Unit",
                    EntityNamespace = "Domain.Entities.FIU",
                    ProgramActivityType = typeof(FIUProgramActivity),
                    OtherActivityType = typeof(FIUOtherActivity),
                    UniqueFields = new List<string> { "Number", "UploadMediaUrl", "FIUActivitiesId" },
                    AvailableEntities = new List<string>
                    {
                        "ProgramActivities", "OtherActivities"
                    },
                    HasResults = false,
                    IsSalesUnit = false,
                    IsVisitorUnit = false,
                    IsActivityUnit = true
                }
            },

            // ===== IBTVA - Institute of Baking Technology and Value Addition =====
            {
                UnitConstants.IBTVA_UNIT_ID,
                new UnitReportConfiguration
                {
                    UnitId = UnitConstants.IBTVA_UNIT_ID,
                    UnitName = "Institute of Baking Technology and Value Addition",
                    EntityNamespace = "Domain.Entities.IBTVA",
                    ProgramDetailsType = typeof(IbtvaProgramDetails),
                    ReportType = typeof(IbtvaReport),
                    ParticipantDemographicsType = typeof(IbtvaParticipantDemographics),
                    AdvisoryServicesType = typeof(IbtvaAdvisoryServices),
                    ProgramContentType = typeof(IbtvaProgramContentAndResources),
                    RecommendationType = typeof(IbtvaRecommendation),
                    TrainingProgrammeType = typeof(IbtvaProgramme),
                    OtherActivityType = typeof(IbtavOtherActivity),
                    UniqueFields = new List<string> { "TPNo", "BatchNo" },
                    AvailableEntities = new List<string>
                    {
                        "ProgramDetails", "Report", "ParticipantDemographics",
                        "AdvisoryServices", "TrainingProgrammes", "OtherActivities"
                    },
                    HasResults = false,
                    IsSalesUnit = false,
                    IsVisitorUnit = false,
                    IsActivityUnit = false
                }
            },

            // ===== ATIC - Agricultural Technology Information Centre =====
            {
                UnitConstants.ATIC_UNIT_ID,
                new UnitReportConfiguration
                {
                    UnitId = UnitConstants.ATIC_UNIT_ID,
                    UnitName = "Agricultural Technology Information Centre",
                    EntityNamespace = "Domain.Entities.ATIC",
                    ProgramDetailsType = typeof(AticProgramDetails),
                    ReportType = typeof(AticReport),
                    ParticipantDemographicsType = typeof(AticParticipantDemographics),
                    AdvisoryServicesType = typeof(AticAdvisoryServices),
                    ProgramContentType = typeof(AticProgramContentAndResources),
                    RecommendationType = typeof(AticRecommendation),
                    SalesType = typeof(AticSales),
                    OtherActivityType = typeof(AticOtherActivity),
                    UniqueFields = new List<string> { "TPNo", "BatchNo" },
                    AvailableEntities = new List<string>
                    {
                        "ProgramDetails", "Report", "ParticipantDemographics",
                        "AdvisoryServices", "Sales", "OtherActivities"
                    },
                    HasResults = false,
                    IsSalesUnit = true,
                    IsVisitorUnit = false,
                    IsActivityUnit = false
                }
            },

            // ===== DEU - Distance Education Unit =====
            {
                UnitConstants.DEU_UNIT_ID,
                new UnitReportConfiguration
                {
                    UnitId = UnitConstants.DEU_UNIT_ID,
                    UnitName = "Distance Education Unit",
                    EntityNamespace = "Domain.Entities.DEU",
                    ProgramDetailsType = typeof(DeuProgramDetails),
                    ReportType = typeof(DeuReport),
                    ParticipantDemographicsType = typeof(DeuParticipantDemographics),
                    AdvisoryServicesType = typeof(DeuAdvisoryServices),
                    ProgramContentType = typeof(DeuProgramContentAndResources),
                    RecommendationType = typeof(DeuRecommendation),
                    OtherActivityType = typeof(DeuOtherActivity),
                    UniqueFields = new List<string> { "TPNo", "BatchNo" },
                    AvailableEntities = new List<string>
                    {
                        "ProgramDetails", "Report", "ParticipantDemographics",
                        "AdvisoryServices", "OtherActivities"
                    },
                    HasResults = false,
                    IsSalesUnit = false,
                    IsVisitorUnit = false,
                    IsActivityUnit = false
                }
            },

            // ===== ASM - Agricultural Sciences Museum =====
            {
                UnitConstants.ASM_UNIT_ID,
                new UnitReportConfiguration
                {
                    UnitId = UnitConstants.ASM_UNIT_ID,
                    UnitName = "Agricultural Sciences Museum",
                    EntityNamespace = "Domain.Entities.ASM",
                    ReportType = typeof(AsmReport),
                    VisitorDetailsType = typeof(ASMVisitorDetails),
                    OtherActivityType = typeof(ASMOtherActivity),
                    UniqueFields = new List<string>
                    {
                        "InstituteName", "FarmersCount", "StudentsCount", "PublicCount"
                    },
                    AvailableEntities = new List<string>
                    {
                        "Report", "VisitorDetails", "OtherActivities"
                    },
                    HasResults = false,
                    IsSalesUnit = false,
                    IsVisitorUnit = true,
                    IsActivityUnit = false
                }
            },

            // ===== NAEP - National Agriculture Extension Project =====
            {
                UnitConstants.NAEP_UNIT_ID,
                new UnitReportConfiguration
                {
                    UnitId = UnitConstants.NAEP_UNIT_ID,
                    UnitName = "National Agriculture Extension Project",
                    EntityNamespace = "Domain.Entities.NAEP",
                    ProgramDetailsType = typeof(NaepProgramDetails),
                    ReportType = typeof(NaepReport),
                    ParticipantDemographicsType = typeof(NaepParticipantDemographics),
                    AdvisoryServicesType = typeof(NaepAdvisoryServices),
                    ProgramContentType = typeof(NaepProgramContentAndResources),
                    RecommendationType = typeof(NaepRecommendation),
                    UniqueFields = new List<string> { "TPNo", "BatchNo" },
                    AvailableEntities = new List<string>
                    {
                        "ProgramDetails", "Report", "ParticipantDemographics",
                        "AdvisoryServices"
                    },
                    HasResults = false,
                    IsSalesUnit = false,
                    IsVisitorUnit = false,
                    IsActivityUnit = false
                }
            },

            // ===== EEU - Extension Education Units =====
            {
                UnitConstants.EEU_UNIT_ID,
                new UnitReportConfiguration
                {
                    UnitId = UnitConstants.EEU_UNIT_ID,
                    UnitName = "Extension Education Units",
                    EntityNamespace = "Domain.Entities.EEU",
                    ProgramDetailsType = typeof(EeuProgramDetails),
                    ReportType = typeof(EeuReport),
                    ParticipantDemographicsType = typeof(EeuParticipantDemographics),
                    AdvisoryServicesType = typeof(EeuAdvisoryServices),
                    ProgramContentType = typeof(EeuProgramContentAndResources),
                    RecommendationType = typeof(EeuRecommendation),
                    ResultType = typeof(EeuResult),
                    TrainingProgrammeType = typeof(EeuTrainingProgramme),
                    OtherActivityType = typeof(EeuOtherActivity),
                    UniqueFields = new List<string>
                    {
                        "T01", "T02", "T03", "T04", "T05", "StageOfCrop",
                        "NoOfDemos", "NoOfTrails", "NoOfChecks", "NoOfVisits"
                    },
                    AvailableEntities = new List<string>
                    {
                        "ProgramDetails", "Report", "ParticipantDemographics",
                        "AdvisoryServices", "Results", "TrainingProgrammes", "OtherActivities"
                    },
                    HasResults = true,
                    IsSalesUnit = false,
                    IsVisitorUnit = false,
                    IsActivityUnit = false
                }
            },

            // ===== KVK - Krishi Vigyan Kendras =====
            {
                UnitConstants.KVK_UNIT_ID,
                new UnitReportConfiguration
                {
                    UnitId = UnitConstants.KVK_UNIT_ID,
                    UnitName = "Krishi Vigyan Kendras",
                    EntityNamespace = "Domain.Entities.KVK",
                    ProgramDetailsType = typeof(KvkProgramDetails),
                    ReportType = typeof(KvkReport),
                    ParticipantDemographicsType = typeof(KvkParticipantDemographics),
                    AdvisoryServicesType = typeof(KvkAdvisoryServices),
                    ProgramContentType = typeof(KvkProgramContentAndResources),
                    RecommendationType = typeof(KvkRecommendation),
                    ResultType = typeof(KvkResult),
                    UniqueFields = new List<string>
                    {
                        "T01", "T02", "T03", "T04", "T05", "StageOfCrop",
                        "NoOfDemos", "NoOfTrails", "NoOfChecks", "NoOfVisits"
                    },
                    AvailableEntities = new List<string>
                    {
                        "ProgramDetails", "Report", "ParticipantDemographics",
                        "AdvisoryServices", "Results"
                    },
                    HasResults = true,
                    IsSalesUnit = false,
                    IsVisitorUnit = false,
                    IsActivityUnit = false
                }
            },

            // ===== SAMETI - State Agricultural Management and Extension Training Institutes =====
            {
                UnitConstants.SAMETI_UNIT_ID,
                new UnitReportConfiguration
                {
                    UnitId = UnitConstants.SAMETI_UNIT_ID,
                    UnitName = "State Agricultural Management and Extension Training Institutes",
                    EntityNamespace = "Domain.Entities.SAMETI",
                    ProgramDetailsType = typeof(SametiProgramDetails),
                    ReportType = typeof(SametiReport),
                    ParticipantDemographicsType = typeof(SametiParticipantDemographics),
                    AdvisoryServicesType = typeof(SametiAdvisoryServices),
                    ProgramContentType = typeof(SametiProgramContentAndResources),
                    RecommendationType = typeof(SametiRecommendation),
                    UniqueFields = new List<string> { "TPNo", "BatchNo" },
                    AvailableEntities = new List<string>
                    {
                        "ProgramDetails", "Report", "ParticipantDemographics",
                        "AdvisoryServices"
                    },
                    HasResults = false,
                    IsSalesUnit = false,
                    IsVisitorUnit = false,
                    IsActivityUnit = false
                }
            }
        };

        /// <summary>
        /// Get configuration for a specific unit
        /// </summary>
        public static UnitReportConfiguration GetConfiguration(int unitId)
        {
            if (_configurations.TryGetValue(unitId, out var config))
            {
                return config;
            }

            throw new ArgumentException($"No configuration found for unit ID {unitId}");
        }

        /// <summary>
        /// Get all unit configurations
        /// </summary>
        public static IEnumerable<UnitReportConfiguration> GetAllConfigurations()
        {
            return _configurations.Values;
        }

        /// <summary>
        /// Check if a unit exists
        /// </summary>
        public static bool UnitExists(int unitId)
        {
            return _configurations.ContainsKey(unitId);
        }

        /// <summary>
        /// Get configurations for multiple units
        /// </summary>
        public static IEnumerable<UnitReportConfiguration> GetConfigurations(IEnumerable<int> unitIds)
        {
            foreach (var unitId in unitIds)
            {
                if (_configurations.TryGetValue(unitId, out var config))
                {
                    yield return config;
                }
            }
        }
    }
}

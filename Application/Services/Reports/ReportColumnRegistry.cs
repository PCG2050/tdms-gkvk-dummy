using Application.Models;

namespace Application.Services.Reports
{
    /// <summary>
    /// Central registry of all available report sections and their columns
    /// </summary>
    public static class ReportColumnRegistry
    {
        // ========================================
        // SECTION KEYS (Constants)
        // ========================================
        public const string PROGRAMS = "programs";
        public const string PUBLICATIONS = "publications";
        public const string NOMINATIONS = "nominations";
        public const string CONSULTANCIES = "consultancies";
        public const string SERVICES = "services";
        public const string FINANCIAL_STATUS = "financialStatus";
        public const string OTHER_ACTIVITIES = "otherActivities";
        public const string FIU_ACTIVITIES = "fiuActivities";
        public const string ASM_ACTIVITIES = "asmActivities";

     

        // ========================================
        // GET CONFIGURATION FOR UNIT
        // ========================================

        /// <summary>
        /// Get report configuration for a specific unit
        /// </summary>
        public static ReportConfiguration GetConfigurationForUnit(int unitId)
        {
            var config = new ReportConfiguration();

            // FIU Unit - Special sections
            if (unitId == UnitConstants.FIU_UNIT_ID)
            {
                config.AvailableSections = new List<SectionDefinition>
                {
                    GetFIUActivitiesSection(),
                    GetOtherActivitiesSection()
                };
                config.DefaultSections = new List<string> { FIU_ACTIVITIES, OTHER_ACTIVITIES };
            }
            // ASM Unit - Special sections
            else if (unitId == UnitConstants.ASM_UNIT_ID)
            {
                config.AvailableSections = new List<SectionDefinition>
                {
                    GetASMActivitiesSection(),
                    GetOtherActivitiesSection()
                };
                config.DefaultSections = new List<string> { ASM_ACTIVITIES, OTHER_ACTIVITIES };
            }
            // All other units - Standard sections
            else
            {
                config.AvailableSections = new List<SectionDefinition>
                {
                    GetProgramsSection(),
                    GetPublicationsSection(),
                    GetNominationsSection(),
                    GetConsultanciesSection(),
                    GetServicesSection(),
                    GetFinancialStatusSection(),
                    GetOtherActivitiesSection()
                };
                config.DefaultSections = new List<string>
                {
                    PROGRAMS,
                    PUBLICATIONS,
                    SERVICES
                };
            }

            return config;
        }

        // ========================================
        // SECTION DEFINITIONS
        // ========================================

        private static SectionDefinition GetProgramsSection()
        {
            return new SectionDefinition
            {
                SectionKey = PROGRAMS,
                DisplayName = "PROGRAMS",
                Icon = "calendar",
                Group = "Core Activities",
                AvailableColumns = new List<ColumnDefinition>
                {
                    // ===== STEPPER 1: Program Organized =====
                    new() { Key = "title", DisplayName = "Title", DataType = "string", DefaultSelected = true, Group = "program_organized", Width = 25 },
                    new() { Key = "startDate", DisplayName = "Start Date", DataType = "date", DefaultSelected = true, Group = "program_organized", Width = 12 },
                    new() { Key = "endDate", DisplayName = "End Date", DataType = "date", DefaultSelected = true, Group = "program_organized", Width = 12 },
                    new() { Key = "programType", DisplayName = "Program Type", DataType = "string", DefaultSelected = true, Group = "program_organized", Width = 15 },
                    new() { Key = "category", DisplayName = "Category", DataType = "string", DefaultSelected = false, Group = "program_organized", Width = 12 },
                    new() { Key = "type", DisplayName = "Type", DataType = "string", DefaultSelected = false, Group = "program_organized", Width = 12 },
                    new() { Key = "theme", DisplayName = "Theme", DataType = "string", DefaultSelected = false, Group = "program_organized", Width = 15 },
                    new() { Key = "sponsoredOrganization", DisplayName = "Sponsored Organization", DataType = "string", DefaultSelected = false, Group = "program_organized", Width = 20 },
                    new() { Key = "collaborator", DisplayName = "Collaborator", DataType = "string", DefaultSelected = false, Group = "program_organized", Width = 15 },
                    new() { Key = "collaborativeProgram", DisplayName = "Collaborative Program", DataType = "boolean", DefaultSelected = false, Group = "program_organized", Width = 10 },
                    new() { Key = "mode", DisplayName = "Mode", DataType = "string", DefaultSelected = false, Group = "program_organized", Width = 10 },
                    new() { Key = "region", DisplayName = "Region", DataType = "string", DefaultSelected = false, Group = "program_organized", Width = 12 },
                    new() { Key = "duration", DisplayName = "Duration (days)", DataType = "number", DefaultSelected = true, Group = "program_organized", Width = 8 },
                    new() { Key = "tpNo", DisplayName = "TP No", DataType = "string", DefaultSelected = false, Group = "program_organized", Width = 10 },
                    new() { Key = "location", DisplayName = "Location", DataType = "string", DefaultSelected = false, Group = "program_organized", Width = 15 },
                    new() { Key = "sourceOfFund", DisplayName = "Source of Fund", DataType = "string", DefaultSelected = false, Group = "program_organized", Width = 15 },
                    new() { Key = "totalOutlay", DisplayName = "Total Outlay", DataType = "currency", DefaultSelected = false, Group = "program_organized", Width = 12 },

                    // ===== STEPPER 2: Participant Demographics =====
                    new() { Key = "participantCategory", DisplayName = "Participant Category", DataType = "string", DefaultSelected = true, Group = "demographics", Width = 20 },
                    new() { Key = "maleTotal", DisplayName = "Male Total", DataType = "number", DefaultSelected = true, Group = "demographics", Width = 8 },
                    new() { Key = "femaleTotal", DisplayName = "Female Total", DataType = "number", DefaultSelected = true, Group = "demographics", Width = 8 },
                    new() { Key = "grandTotal", DisplayName = "Grand Total", DataType = "number", DefaultSelected = true, Group = "demographics", Width = 8 },
                    new() { Key = "stayedInHostel", DisplayName = "Stayed in Hostel", DataType = "number", DefaultSelected = false, Group = "demographics", Width = 8 },

                    // ===== STEPPER 3: Program Content & Resources =====
                    new() { Key = "resourcePersons", DisplayName = "Resource Persons", DataType = "string", DefaultSelected = true, Group = "resource_person", Width = 30, IsArray = true },
                    new() { Key = "topicsCovered", DisplayName = "Topics Covered", DataType = "string", DefaultSelected = true, Group = "topics", Width = 30, IsArray = true },
                    new() { Key = "teachingAids", DisplayName = "Teaching Aids", DataType = "string", DefaultSelected = false, Group = "teaching_aids", Width = 25, IsArray = true },

                    // ===== STEPPER 4: Advisory Services =====
                    new() { Key = "noOfFacebookSMS", DisplayName = "No. of Facebook SMS", DataType = "number", DefaultSelected = false, Group = "advisory_services", Width = 8 },
                    new() { Key = "noOfWhatsAppSMS", DisplayName = "No. of WhatsApp SMS", DataType = "number", DefaultSelected = false, Group = "advisory_services", Width = 8 },
                    new() { Key = "noOfWhatsAppQueries", DisplayName = "No. of WhatsApp Queries", DataType = "number", DefaultSelected = false, Group = "advisory_services", Width = 8 },
                    new() { Key = "noOfPhoneCalls", DisplayName = "No. of Phone Calls", DataType = "number", DefaultSelected = false, Group = "advisory_services", Width = 8 },
                    new() { Key = "noOfFaceToFaceDiscussions", DisplayName = "No. of Face-to-Face Discussions", DataType = "number", DefaultSelected = false, Group = "advisory_services", Width = 8 },
                    new() { Key = "noOfEmailsSent", DisplayName = "No. of Emails Sent", DataType = "number", DefaultSelected = false, Group = "advisory_services", Width = 8 },
                    new() { Key = "noOfBeneficiaries", DisplayName = "No. of Beneficiaries", DataType = "number", DefaultSelected = false, Group = "advisory_services", Width = 8 },
                    new() { Key = "criticalInputsDistributed", DisplayName = "Critical Inputs Distributed", DataType = "string", DefaultSelected = false, Group = "advisory_services", Width = 25, IsArray = true },

                    // ===== STEPPER 5: Results (KVK/EEU only) =====
                    new() { Key = "fldResults", DisplayName = "FLD Results", DataType = "string", DefaultSelected = false, Group = "fld_results", Width = 30, IsArray = true },
                    new() { Key = "oftResults", DisplayName = "OFT Results", DataType = "string", DefaultSelected = false, Group = "oft_results", Width = 30, IsArray = true },

                    // ===== STEPPER 6: Report =====
                    new() { Key = "reportingDate", DisplayName = "Reporting Date", DataType = "date", DefaultSelected = true, Group = "report", Width = 12 },
                    new() { Key = "significantOutcome", DisplayName = "Significant Outcome", DataType = "string", DefaultSelected = true, Group = "report", Width = 30 },
                    new() { Key = "geoTaggedPhoto", DisplayName = "Geo-Tagged Photo", DataType = "string", DefaultSelected = false, Group = "report", Width = 20 },

                    // ===== STEPPER 7: Recommendations =====
                    new() { Key = "problemsIdentified", DisplayName = "Problems Identified", DataType = "string", DefaultSelected = false, Group = "recommendations", Width = 25 },
                    new() { Key = "recommendations", DisplayName = "Recommendations", DataType = "string", DefaultSelected = false, Group = "recommendations", Width = 25 },
                    new() { Key = "actionTaken", DisplayName = "Action Taken", DataType = "string", DefaultSelected = false, Group = "recommendations", Width = 25 },
                    new() { Key = "significantAchievement", DisplayName = "Significant Achievement", DataType = "string", DefaultSelected = false, Group = "recommendations", Width = 25 },
                    new() { Key = "successStories", DisplayName = "Success Stories", DataType = "string", DefaultSelected = false, Group = "recommendations", Width = 25 },
                    new() { Key = "outcome", DisplayName = "Outcome", DataType = "string", DefaultSelected = false, Group = "recommendations", Width = 25 },

                    // Legacy fields for backward compatibility
                    new() { Key = "dateFrom", DisplayName = "Date From", DataType = "date", DefaultSelected = false, Group = "program_organized", Width = 12 },
                    new() { Key = "dateTo", DisplayName = "Date To", DataType = "date", DefaultSelected = false, Group = "program_organized", Width = 12 },
                    new() { Key = "participants", DisplayName = "Participants", DataType = "number", DefaultSelected = false, Group = "demographics", Width = 8 },
                    new() { Key = "status", DisplayName = "Status", DataType = "string", DefaultSelected = false, Group = "program_organized", Width = 12 }
                },
                DefaultColumns = new List<string> { "title", "startDate", "endDate", "programType", "duration", "grandTotal" }
            };
        }

        private static SectionDefinition GetPublicationsSection()
        {
            return new SectionDefinition
            {
                SectionKey = PUBLICATIONS,
                DisplayName = "PUBLICATIONS",
                Icon = "book",
                Group = "Core Activities",
                AvailableColumns = new List<ColumnDefinition>
                {
                    // ===== STEPPER 1: Publications =====
                    new() { Key = "title", DisplayName = "Title", DataType = "string", DefaultSelected = true, Group = "publication_info", Width = 30 },
                    new() { Key = "category", DisplayName = "Category", DataType = "string", DefaultSelected = true, Group = "publication_info", Width = 15 },
                    new() { Key = "mode", DisplayName = "Mode", DataType = "string", DefaultSelected = true, Group = "publication_info", Width = 12 },
                    new() { Key = "region", DisplayName = "Region", DataType = "string", DefaultSelected = true, Group = "publication_info", Width = 12 },
                    new() { Key = "publicationYear", DisplayName = "Publication Year", DataType = "number", DefaultSelected = false, Group = "publication_info", Width = 10 },
                    new() { Key = "publicationDate", DisplayName = "Publication Date", DataType = "date", DefaultSelected = false, Group = "publication_info", Width = 12 },

                    // ===== STEPPER 2: Author =====
                    new() { Key = "publisherName", DisplayName = "Name", DataType = "string", DefaultSelected = true, Group = "author", Width = 20 },
                    new() { Key = "publisherInstitution", DisplayName = "Institution", DataType = "string", DefaultSelected = true, Group = "author", Width = 25 },
                    new() { Key = "publisherAddress", DisplayName = "Address", DataType = "string", DefaultSelected = true, Group = "author", Width = 25 },

                    // ===== STEPPER 3: Extension Literature (ARRAY FIELDS) =====
                    new() { Key = "extensionLiterature", DisplayName = "Publication Sold or Distributed", DataType = "array", DefaultSelected = true, Group = "extension_literature", IsArray = true, Width = 60 },
                    new() { Key = "extensionLiterature.amountPerCopy", DisplayName = "Amount Per Copy", DataType = "currency", DefaultSelected = false, Group = "extension_literature", Width = 12 },
                    new() { Key = "extensionLiterature.numberOfCopies", DisplayName = "Copies Sold/Distributed", DataType = "number", DefaultSelected = false, Group = "extension_literature", Width = 12 },
                    new() { Key = "extensionLiterature.totalAmount", DisplayName = "Amount Generated", DataType = "currency", DefaultSelected = false, Group = "extension_literature", Width = 12 }
                },
                DefaultColumns = new List<string> { "title", "category", "mode", "region", "publisherName", "extensionLiterature" }
            };
        }

        private static SectionDefinition GetNominationsSection()
        {
            return new SectionDefinition
            {
                SectionKey = NOMINATIONS,
                DisplayName = "NOMINATION & REWARDS",
                Icon = "award",
                Group = "Core Activities",
                AvailableColumns = new List<ColumnDefinition>
                {
                    // ===== STEPPER 1: Basic Info =====
                    new() { Key = "type", DisplayName = "Type", DataType = "string", DefaultSelected = true, Group = "award_basic", Width = 15 },
                    new() { Key = "region", DisplayName = "Region", DataType = "string", DefaultSelected = true, Group = "award_basic", Width = 12 },

                    // ===== STEPPER 2: Achievements (ARRAY FIELDS) =====
                    new() { Key = "achievements", DisplayName = "Achievements", DataType = "array", DefaultSelected = true, Group = "achievements", IsArray = true, Width = 50 },
                    new() { Key = "achievements.name", DisplayName = "Name", DataType = "string", DefaultSelected = false, Group = "achievements", Width = 20 },
                    new() { Key = "achievements.achievementDetail", DisplayName = "Achievement Details", DataType = "string", DefaultSelected = false, Group = "achievements", Width = 30 },

                    // ===== STEPPER 3: Awards (ARRAY FIELDS) =====
                    new() { Key = "awards", DisplayName = "Awards", DataType = "array", DefaultSelected = true, Group = "awards", IsArray = true, Width = 50 },
                    new() { Key = "awards.awardingAgency", DisplayName = "Awarding Agency", DataType = "string", DefaultSelected = false, Group = "awards", Width = 20 },
                    new() { Key = "awards.institutionName", DisplayName = "Institution", DataType = "string", DefaultSelected = false, Group = "awards", Width = 20 },
                    new() { Key = "awards.contribution", DisplayName = "Contribution", DataType = "string", DefaultSelected = false, Group = "awards", Width = 15 }
                },
                DefaultColumns = new List<string> { "type", "region", "achievements", "awards" }
            };
        }

        private static SectionDefinition GetConsultanciesSection()
        {
            return new SectionDefinition
            {
                SectionKey = CONSULTANCIES,
                DisplayName = "CONSULTANCY / SOCIAL MEDIA SERVICES",
                Icon = "briefcase",
                Group = "Services",
                AvailableColumns = new List<ColumnDefinition>
                {
                    // ===== STEPPER 1: Consultancy Details =====
                    new() { Key = "category", DisplayName = "Category", DataType = "string", DefaultSelected = true, Group = "consultancy_info", Width = 15 },
                    new() { Key = "title", DisplayName = "Title", DataType = "string", DefaultSelected = true, Group = "consultancy_info", Width = 30 },
                    new() { Key = "relatedToDiscipline", DisplayName = "Related to Discipline", DataType = "string", DefaultSelected = true, Group = "consultancy_info", Width = 20 },
                    new() { Key = "noOfBeneficiaries", DisplayName = "Total No of Beneficiaries", DataType = "number", DefaultSelected = true, Group = "consultancy_info", Width = 12 },
                    new() { Key = "date", DisplayName = "Date", DataType = "date", DefaultSelected = false, Group = "consultancy_info", Width = 12 },
                    new() { Key = "location", DisplayName = "Location", DataType = "string", DefaultSelected = false, Group = "consultancy_info", Width = 15 },

                    // ===== Beneficiaries breakdown (optional detailed fields) =====
                    new() { Key = "male_SC", DisplayName = "Male SC", DataType = "number", DefaultSelected = false, Group = "consultancy_info", Width = 8 },
                    new() { Key = "male_ST", DisplayName = "Male ST", DataType = "number", DefaultSelected = false, Group = "consultancy_info", Width = 8 },
                    new() { Key = "male_OBC", DisplayName = "Male OBC", DataType = "number", DefaultSelected = false, Group = "consultancy_info", Width = 8 },
                    new() { Key = "male_GEN", DisplayName = "Male GEN", DataType = "number", DefaultSelected = false, Group = "consultancy_info", Width = 8 },
                    new() { Key = "female_SC", DisplayName = "Female SC", DataType = "number", DefaultSelected = false, Group = "consultancy_info", Width = 8 },
                    new() { Key = "female_ST", DisplayName = "Female ST", DataType = "number", DefaultSelected = false, Group = "consultancy_info", Width = 8 },
                    new() { Key = "female_OBC", DisplayName = "Female OBC", DataType = "number", DefaultSelected = false, Group = "consultancy_info", Width = 8 },
                    new() { Key = "female_GEN", DisplayName = "Female GEN", DataType = "number", DefaultSelected = false, Group = "consultancy_info", Width = 8 }
                },
                DefaultColumns = new List<string> { "category", "title", "relatedToDiscipline", "noOfBeneficiaries" }
            };
        }

        private static SectionDefinition GetServicesSection()
        {
            return new SectionDefinition
            {
                SectionKey = SERVICES,
                DisplayName = "SERVICES / FACILITIES",
                Icon = "settings",
                Group = "Services",
                AvailableColumns = new List<ColumnDefinition>
                {
                    // ===== STEPPER 1: Service Details =====
                    new() { Key = "category", DisplayName = "Category", DataType = "string", DefaultSelected = true, Group = "service_info", Width = 15 },
                    new() { Key = "theme", DisplayName = "Theme", DataType = "string", DefaultSelected = true, Group = "service_info", Width = 20 },
                    new() { Key = "sourceOfFund", DisplayName = "Source of Funds/Grants", DataType = "string", DefaultSelected = true, Group = "service_info", Width = 20 },
                    new() { Key = "component", DisplayName = "Components", DataType = "string", DefaultSelected = true, Group = "service_info", Width = 20 },
                    new() { Key = "quantityUnit", DisplayName = "Units", DataType = "string", DefaultSelected = true, Group = "service_info", Width = 10 },
                    new() { Key = "number", DisplayName = "Number", DataType = "number", DefaultSelected = true, Group = "service_info", Width = 8 },
                    new() { Key = "amountGenerated", DisplayName = "Amount Generated", DataType = "currency", DefaultSelected = true, Group = "service_info", Width = 12 },

                    // ===== STEPPER 2: Visitors (ARRAY FIELDS - aggregated/summed) =====
                    new() { Key = "visitorDetails", DisplayName = "Visitors", DataType = "array", DefaultSelected = true, Group = "visitors", IsArray = true, Width = 50 },
                    new() { Key = "visitorDetails.male_Total", DisplayName = "Male", DataType = "number", DefaultSelected = false, Group = "visitors", Width = 8 },
                    new() { Key = "visitorDetails.female_Total", DisplayName = "Female", DataType = "number", DefaultSelected = false, Group = "visitors", Width = 8 },
                    new() { Key = "visitorDetails.total", DisplayName = "Total", DataType = "number", DefaultSelected = false, Group = "visitors", Width = 8 },

                    // ===== STEPPER 3: Accommodation (ARRAY FIELDS - aggregated/summed) =====
                    new() { Key = "hostels", DisplayName = "Accommodation", DataType = "array", DefaultSelected = true, Group = "accommodation", IsArray = true, Width = 50 },
                    new() { Key = "hostels.maleTotal", DisplayName = "Male", DataType = "number", DefaultSelected = false, Group = "accommodation", Width = 8 },
                    new() { Key = "hostels.femaleTotal", DisplayName = "Female", DataType = "number", DefaultSelected = false, Group = "accommodation", Width = 8 },
                    new() { Key = "hostels.grandTotal", DisplayName = "Total", DataType = "number", DefaultSelected = false, Group = "accommodation", Width = 8 },
                    new() { Key = "hostels.numberOfDaysStayed", DisplayName = "No of Days Stayed", DataType = "number", DefaultSelected = false, Group = "accommodation", Width = 10 },
                    new() { Key = "hostels.purpose", DisplayName = "Purpose", DataType = "string", DefaultSelected = false, Group = "accommodation", Width = 20 },
                    new() { Key = "hostels.amountGenerated", DisplayName = "Amount", DataType = "currency", DefaultSelected = false, Group = "accommodation", Width = 12 }
                },
                DefaultColumns = new List<string> { "category", "theme", "sourceOfFund", "component", "number", "amountGenerated" }
            };
        }

        private static SectionDefinition GetFinancialStatusSection()
        {
            return new SectionDefinition
            {
                SectionKey = FINANCIAL_STATUS,
                DisplayName = "FINANCIAL STATUS",
                Icon = "dollar-sign",
                Group = "Financial",
                AvailableColumns = new List<ColumnDefinition>
                {
                    // ===== STEPPER 1: Budget (ARRAY FIELDS - aggregated/summed) =====
                    new() { Key = "budgets", DisplayName = "Budget Details", DataType = "array", DefaultSelected = true, Group = "budget", IsArray = true, Width = 50 },
                    new() { Key = "budgets.particulars", DisplayName = "Particulars", DataType = "string", DefaultSelected = false, Group = "budget", Width = 25 },
                    new() { Key = "budgets.sanctioned", DisplayName = "Sanctioned", DataType = "currency", DefaultSelected = false, Group = "budget", Width = 12 },
                    new() { Key = "budgets.released", DisplayName = "Released", DataType = "currency", DefaultSelected = false, Group = "budget", Width = 12 },
                    new() { Key = "budgets.expenditure", DisplayName = "Expenditure", DataType = "currency", DefaultSelected = false, Group = "budget", Width = 12 },
                    new() { Key = "budgets.balance", DisplayName = "Balance", DataType = "currency", DefaultSelected = false, Group = "budget", Width = 12 },

                    // ===== STEPPER 2: Revolving Fund (ARRAY FIELDS - aggregated/summed) =====
                    new() { Key = "revolvingFunds", DisplayName = "Revolving Fund Details", DataType = "array", DefaultSelected = true, Group = "revolving_fund", IsArray = true, Width = 50 },
                    new() { Key = "revolvingFunds.yearMonth", DisplayName = "Year/Month", DataType = "string", DefaultSelected = false, Group = "revolving_fund", Width = 12 },
                    new() { Key = "revolvingFunds.openingBalance", DisplayName = "Opening Balance", DataType = "currency", DefaultSelected = false, Group = "revolving_fund", Width = 12 },
                    new() { Key = "revolvingFunds.receipt", DisplayName = "Receipt", DataType = "currency", DefaultSelected = false, Group = "revolving_fund", Width = 12 },
                    new() { Key = "revolvingFunds.expenditure", DisplayName = "Expenditure", DataType = "currency", DefaultSelected = false, Group = "revolving_fund", Width = 12 },
                    new() { Key = "revolvingFunds.closingBalance", DisplayName = "Closing Balance", DataType = "currency", DefaultSelected = false, Group = "revolving_fund", Width = 12 }
                },
                DefaultColumns = new List<string> { "budgets", "revolvingFunds" }
            };
        }

        private static SectionDefinition GetOtherActivitiesSection()
        {
            return new SectionDefinition
            {
                SectionKey = OTHER_ACTIVITIES,
                DisplayName = "OTHER ACTIVITIES",
                Icon = "list",
                Group = "Other",
                AvailableColumns = new List<ColumnDefinition>
                {
                    // String fields - auto-calculated (description and title will be sized appropriately)
                    new() { Key = "title", DisplayName = "Title", DataType = "string", DefaultSelected = true, Group = "Basic Info" },
                    new() { Key = "description", DisplayName = "Description", DataType = "string", DefaultSelected = true, Group = "Details" }
                },
                DefaultColumns = new List<string> { "title", "description" }
            };
        }

        private static SectionDefinition GetFIUActivitiesSection()
        {
            return new SectionDefinition
            {
                SectionKey = FIU_ACTIVITIES,
                DisplayName = "FIU MEDIA ACTIVITIES",
                Icon = "radio",
                Group = "FIU Specific",
                AvailableColumns = new List<ColumnDefinition>
                {
                    // String fields - auto-calculated
                    new() { Key = "activityName", DisplayName = "Activity", DataType = "string", DefaultSelected = true, Group = "Basic Info" },

                    // Fixed-size fields
                    new() { Key = "count", DisplayName = "No.", DataType = "number", DefaultSelected = true, Group = "Statistics", Width = 8 },
                    new() { Key = "date", DisplayName = "Date", DataType = "date", DefaultSelected = false, Group = "Details", Width = 12 }
                },
                DefaultColumns = new List<string> { "activityName", "count" }
            };
        }

        private static SectionDefinition GetASMActivitiesSection()
        {
            return new SectionDefinition
            {
                SectionKey = ASM_ACTIVITIES,
                DisplayName = "ASM VISITOR STATISTICS",
                Icon = "users",
                Group = "ASM Specific",
                AvailableColumns = new List<ColumnDefinition>
                {
                    // String fields - auto-calculated
                    new() { Key = "particulars", DisplayName = "Particulars", DataType = "string", DefaultSelected = true, Group = "Basic Info" },

                    // Fixed-size fields
                    new() { Key = "noOfVisitors", DisplayName = "No. of visitors", DataType = "number", DefaultSelected = true, Group = "Statistics", Width = 10 },
                    new() { Key = "date", DisplayName = "Date", DataType = "date", DefaultSelected = false, Group = "Details", Width = 12 }
                },
                DefaultColumns = new List<string> { "particulars", "noOfVisitors" }
            };
        }

        // ========================================
        // HELPER: Get Section Definition by Key
        // ========================================

        public static SectionDefinition? GetSectionDefinition(string sectionKey)
        {
            return sectionKey switch
            {
                PROGRAMS => GetProgramsSection(),
                PUBLICATIONS => GetPublicationsSection(),
                NOMINATIONS => GetNominationsSection(),
                CONSULTANCIES => GetConsultanciesSection(),
                SERVICES => GetServicesSection(),
                OTHER_ACTIVITIES => GetOtherActivitiesSection(),
                FIU_ACTIVITIES => GetFIUActivitiesSection(),
                ASM_ACTIVITIES => GetASMActivitiesSection(),
                _ => null
            };
        }
    }
}

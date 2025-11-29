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
        public const string OTHER_ACTIVITIES = "otherActivities";
        public const string FIU_ACTIVITIES = "fiuActivities";
        public const string ASM_ACTIVITIES = "asmActivities";

        // ========================================
        // UNIT IDS (Constants)
        // ========================================
        public const int FIU_UNIT_ID = 3;
        public const int ASM_UNIT_ID = 7;

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
            if (unitId == FIU_UNIT_ID)
            {
                config.AvailableSections = new List<SectionDefinition>
                {
                    GetFIUActivitiesSection(),
                    GetOtherActivitiesSection()
                };
                config.DefaultSections = new List<string> { FIU_ACTIVITIES, OTHER_ACTIVITIES };
            }
            // ASM Unit - Special sections
            else if (unitId == ASM_UNIT_ID)
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
                    new() { Key = "programType", DisplayName = "Program Type", DataType = "string", DefaultSelected = true, Group = "Basic Info", Width = 15 },
                    new() { Key = "title", DisplayName = "Title", DataType = "string", DefaultSelected = true, Group = "Basic Info", Width = 30 },
                    new() { Key = "dateFrom", DisplayName = "Date From", DataType = "date", DefaultSelected = true, Group = "Dates", Width = 12 },
                    new() { Key = "dateTo", DisplayName = "Date To", DataType = "date", DefaultSelected = true, Group = "Dates", Width = 12 },
                    new() { Key = "duration", DisplayName = "Duration (days)", DataType = "number", DefaultSelected = true, Group = "Details", Width = 10 },
                    new() { Key = "participants", DisplayName = "Participants", DataType = "number", DefaultSelected = true, Group = "Statistics", Width = 12 },
                    new() { Key = "status", DisplayName = "Status", DataType = "string", DefaultSelected = false, Group = "Details", Width = 10 }
                },
                DefaultColumns = new List<string> { "programType", "title", "dateFrom", "dateTo", "duration", "participants" }
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
                    new() { Key = "category", DisplayName = "Category", DataType = "string", DefaultSelected = true, Group = "Basic Info", Width = 25 },
                    new() { Key = "title", DisplayName = "Title", DataType = "string", DefaultSelected = true, Group = "Basic Info", Width = 50 },
                    new() { Key = "pages", DisplayName = "Pages", DataType = "number", DefaultSelected = true, Group = "Details", Width = 15 },
                    new() { Key = "publishedDate", DisplayName = "Published Date", DataType = "date", DefaultSelected = false, Group = "Details", Width = 15 }
                },
                DefaultColumns = new List<string> { "category", "title", "pages" }
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
                    new() { Key = "type", DisplayName = "Type", DataType = "string", DefaultSelected = true, Group = "Basic Info", Width = 20 },
                    new() { Key = "awardName", DisplayName = "Award Name", DataType = "string", DefaultSelected = true, Group = "Basic Info", Width = 35 },
                    new() { Key = "category", DisplayName = "Category", DataType = "string", DefaultSelected = true, Group = "Details", Width = 25 },
                    new() { Key = "date", DisplayName = "Date", DataType = "date", DefaultSelected = true, Group = "Details", Width = 15 }
                },
                DefaultColumns = new List<string> { "type", "awardName", "category", "date" }
            };
        }

        private static SectionDefinition GetConsultanciesSection()
        {
            return new SectionDefinition
            {
                SectionKey = CONSULTANCIES,
                DisplayName = "CONSULTANCY SERVICES",
                Icon = "briefcase",
                Group = "Services",
                AvailableColumns = new List<ColumnDefinition>
                {
                    new() { Key = "category", DisplayName = "Category", DataType = "string", DefaultSelected = true, Group = "Basic Info", Width = 30 },
                    new() { Key = "title", DisplayName = "Title", DataType = "string", DefaultSelected = true, Group = "Basic Info", Width = 40 },
                    new() { Key = "date", DisplayName = "Date", DataType = "date", DefaultSelected = true, Group = "Details", Width = 15 },
                    new() { Key = "clientName", DisplayName = "Client", DataType = "string", DefaultSelected = false, Group = "Details", Width = 25 }
                },
                DefaultColumns = new List<string> { "category", "title", "date" }
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
                    new() { Key = "category", DisplayName = "Category", DataType = "string", DefaultSelected = true, Group = "Basic Info", Width = 20 },
                    new() { Key = "theme", DisplayName = "Theme", DataType = "string", DefaultSelected = true, Group = "Basic Info", Width = 20 },
                    new() { Key = "unit", DisplayName = "Unit", DataType = "string", DefaultSelected = true, Group = "Details", Width = 15 },
                    new() { Key = "quantity", DisplayName = "Quantity", DataType = "number", DefaultSelected = true, Group = "Statistics", Width = 15 },
                    new() { Key = "amount", DisplayName = "Amount", DataType = "currency", DefaultSelected = true, Group = "Financial", Width = 15 },
                    new() { Key = "status", DisplayName = "Status", DataType = "string", DefaultSelected = false, Group = "Details", Width = 10 }
                },
                DefaultColumns = new List<string> { "category", "theme", "unit", "quantity", "amount" }
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
                    new() { Key = "title", DisplayName = "Title", DataType = "string", DefaultSelected = true, Group = "Basic Info", Width = 40 },
                    new() { Key = "description", DisplayName = "Description", DataType = "string", DefaultSelected = true, Group = "Details", Width = 60 }
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
                    new() { Key = "activityName", DisplayName = "Activity", DataType = "string", DefaultSelected = true, Group = "Basic Info", Width = 60 },
                    new() { Key = "count", DisplayName = "No.", DataType = "number", DefaultSelected = true, Group = "Statistics", Width = 20 },
                    new() { Key = "date", DisplayName = "Date", DataType = "date", DefaultSelected = false, Group = "Details", Width = 20 }
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
                    new() { Key = "particulars", DisplayName = "Particulars", DataType = "string", DefaultSelected = true, Group = "Basic Info", Width = 60 },
                    new() { Key = "noOfVisitors", DisplayName = "No. of visitors", DataType = "number", DefaultSelected = true, Group = "Statistics", Width = 30 },
                    new() { Key = "date", DisplayName = "Date", DataType = "date", DefaultSelected = false, Group = "Details", Width = 20 }
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

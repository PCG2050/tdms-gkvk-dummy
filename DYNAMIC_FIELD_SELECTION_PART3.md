# Dynamic Field Selection - Part 3: Additional Sections & Implementation

## Additional Section Configurations

### Publications Section

```csharp
private SectionConfiguration BuildPublicationsSection()
{
    var fields = new List<FieldMetadata>
    {
        new()
        {
            FieldKey = "id",
            DisplayName = "ID",
            DataType = FieldDataType.Integer,
            IsRequired = true,
            IsDefaultSelected = true,
            Category = "Identification",
            ColumnWidth = "5%",
            DisplayOrder = 1,
            IsSortable = true,
            IsAggregatable = false
        },
        new()
        {
            FieldKey = "title",
            DisplayName = "Publication Title",
            DataType = FieldDataType.String,
            IsRequired = true,
            IsDefaultSelected = true,
            Category = "Basic",
            ColumnWidth = "25%",
            Description = "Title of the publication",
            DisplayOrder = 2,
            IsSortable = true,
            IsAggregatable = false
        },
        new()
        {
            FieldKey = "publicationType",
            DisplayName = "Type",
            DataType = FieldDataType.Enum,
            IsRequired = false,
            IsDefaultSelected = true,
            Category = "Basic",
            ColumnWidth = "10%",
            Description = "Type of publication (Journal, Book, Report, etc.)",
            DisplayOrder = 3,
            IsSortable = true,
            IsAggregatable = false
        },
        new()
        {
            FieldKey = "authors",
            DisplayName = "Authors",
            DataType = FieldDataType.String,
            IsRequired = false,
            IsDefaultSelected = true,
            Category = "Basic",
            ColumnWidth = "15%",
            Description = "List of authors",
            DisplayOrder = 4,
            IsSortable = true,
            IsAggregatable = false
        },
        new()
        {
            FieldKey = "publishedDate",
            DisplayName = "Published Date",
            DataType = FieldDataType.Date,
            IsRequired = false,
            IsDefaultSelected = true,
            Category = "Basic",
            ColumnWidth = "10%",
            Description = "Publication date",
            DisplayOrder = 5,
            IsSortable = true,
            IsAggregatable = false,
            FormatString = "dd/MM/yyyy"
        },
        new()
        {
            FieldKey = "publisher",
            DisplayName = "Publisher",
            DataType = FieldDataType.String,
            IsRequired = false,
            IsDefaultSelected = false,
            Category = "Publishing",
            ColumnWidth = "12%",
            Description = "Publisher name",
            DisplayOrder = 10,
            IsSortable = true,
            IsAggregatable = false
        },
        new()
        {
            FieldKey = "issn",
            DisplayName = "ISSN/ISBN",
            DataType = FieldDataType.String,
            IsRequired = false,
            IsDefaultSelected = false,
            Category = "Publishing",
            ColumnWidth = "10%",
            Description = "ISSN or ISBN number",
            DisplayOrder = 11,
            IsSortable = false,
            IsAggregatable = false
        },
        new()
        {
            FieldKey = "doi",
            DisplayName = "DOI",
            DataType = FieldDataType.String,
            IsRequired = false,
            IsDefaultSelected = false,
            Category = "Publishing",
            ColumnWidth = "12%",
            Description = "Digital Object Identifier",
            DisplayOrder = 12,
            IsSortable = false,
            IsAggregatable = false
        },
        new()
        {
            FieldKey = "journalName",
            DisplayName = "Journal Name",
            DataType = FieldDataType.String,
            IsRequired = false,
            IsDefaultSelected = false,
            Category = "Publishing",
            ColumnWidth = "15%",
            Description = "Name of the journal",
            DisplayOrder = 13,
            IsSortable = true,
            IsAggregatable = false
        },
        new()
        {
            FieldKey = "volume",
            DisplayName = "Volume",
            DataType = FieldDataType.String,
            IsRequired = false,
            IsDefaultSelected = false,
            Category = "Publishing",
            ColumnWidth = "6%",
            Description = "Journal volume",
            DisplayOrder = 14,
            IsSortable = false,
            IsAggregatable = false
        },
        new()
        {
            FieldKey = "issue",
            DisplayName = "Issue",
            DataType = FieldDataType.String,
            IsRequired = false,
            IsDefaultSelected = false,
            Category = "Publishing",
            ColumnWidth = "6%",
            Description = "Journal issue",
            DisplayOrder = 15,
            IsSortable = false,
            IsAggregatable = false
        },
        new()
        {
            FieldKey = "pages",
            DisplayName = "Pages",
            DataType = FieldDataType.String,
            IsRequired = false,
            IsDefaultSelected = false,
            Category = "Publishing",
            ColumnWidth = "8%",
            Description = "Page range",
            DisplayOrder = 16,
            IsSortable = false,
            IsAggregatable = false
        },
        new()
        {
            FieldKey = "abstract",
            DisplayName = "Abstract",
            DataType = FieldDataType.String,
            IsRequired = false,
            IsDefaultSelected = false,
            Category = "Content",
            ColumnWidth = "20%",
            Description = "Publication abstract",
            DisplayOrder = 20,
            IsSortable = false,
            IsAggregatable = false
        },
        new()
        {
            FieldKey = "keywords",
            DisplayName = "Keywords",
            DataType = FieldDataType.String,
            IsRequired = false,
            IsDefaultSelected = false,
            Category = "Content",
            ColumnWidth = "15%",
            Description = "Keywords or tags",
            DisplayOrder = 21,
            IsSortable = false,
            IsAggregatable = false
        },
        new()
        {
            FieldKey = "impactFactor",
            DisplayName = "Impact Factor",
            DataType = FieldDataType.Decimal,
            IsRequired = false,
            IsDefaultSelected = false,
            Category = "Metrics",
            ColumnWidth = "8%",
            Description = "Journal impact factor",
            DisplayOrder = 30,
            IsSortable = true,
            IsAggregatable = false,
            FormatString = "0.000"
        },
        new()
        {
            FieldKey = "citationCount",
            DisplayName = "Citations",
            DataType = FieldDataType.Integer,
            IsRequired = false,
            IsDefaultSelected = false,
            Category = "Metrics",
            ColumnWidth = "6%",
            Description = "Number of citations",
            DisplayOrder = 31,
            IsSortable = true,
            IsAggregatable = true
        },
        new()
        {
            FieldKey = "pdfUrl",
            DisplayName = "PDF Link",
            DataType = FieldDataType.Url,
            IsRequired = false,
            IsDefaultSelected = false,
            Category = "Links",
            ColumnWidth = "8%",
            Description = "Link to PDF file",
            DisplayOrder = 40,
            IsSortable = false,
            IsAggregatable = false
        },
        new()
        {
            FieldKey = "webUrl",
            DisplayName = "Web Link",
            DataType = FieldDataType.Url,
            IsRequired = false,
            IsDefaultSelected = false,
            Category = "Links",
            ColumnWidth = "8%",
            Description = "Link to publication website",
            DisplayOrder = 41,
            IsSortable = false,
            IsAggregatable = false
        },
        new()
        {
            FieldKey = "formStatus",
            DisplayName = "Status",
            DataType = FieldDataType.Enum,
            IsRequired = true,
            IsDefaultSelected = true,
            Category = "Status",
            ColumnWidth = "8%",
            Description = "Publication entry status",
            DisplayOrder = 50,
            IsSortable = true,
            IsAggregatable = false
        }
    };

    var categories = new List<FieldCategory>
    {
        new()
        {
            CategoryKey = "Identification",
            DisplayName = "Identification",
            DisplayOrder = 1,
            IsCollapsedByDefault = false
        },
        new()
        {
            CategoryKey = "Basic",
            DisplayName = "Basic Information",
            DisplayOrder = 2,
            IsCollapsedByDefault = false
        },
        new()
        {
            CategoryKey = "Publishing",
            DisplayName = "Publishing Details",
            DisplayOrder = 3,
            IsCollapsedByDefault = false
        },
        new()
        {
            CategoryKey = "Content",
            DisplayName = "Content Details",
            DisplayOrder = 4,
            IsCollapsedByDefault = true
        },
        new()
        {
            CategoryKey = "Metrics",
            DisplayName = "Impact Metrics",
            DisplayOrder = 5,
            IsCollapsedByDefault = true
        },
        new()
        {
            CategoryKey = "Links",
            DisplayName = "Document Links",
            DisplayOrder = 6,
            IsCollapsedByDefault = true
        },
        new()
        {
            CategoryKey = "Status",
            DisplayName = "Status",
            DisplayOrder = 7,
            IsCollapsedByDefault = false
        }
    };

    return new SectionConfiguration
    {
        SectionKey = "publications",
        DisplayName = "Publications",
        Description = "Research publications, journals, books, and reports",
        IconClass = "bi-journal-text",
        DisplayOrder = 10,
        AvailableFields = fields,
        RequiredFields = new List<string> { "id", "title", "formStatus" },
        DefaultSelectedFields = new List<string>
        {
            "id", "title", "publicationType", "authors", "publishedDate", "formStatus"
        },
        MaxFieldsAllowed = 0,
        SupportsPagination = true,
        SupportsSorting = true,
        SupportsFiltering = true,
        FieldCategories = categories
    };
}
```

### Advisory Services Section

```csharp
private SectionConfiguration BuildAdvisoryServicesSection(int unitId)
{
    var fields = new List<FieldMetadata>
    {
        new()
        {
            FieldKey = "id",
            DisplayName = "ID",
            DataType = FieldDataType.Integer,
            IsRequired = true,
            IsDefaultSelected = true,
            Category = "Identification",
            ColumnWidth = "5%",
            DisplayOrder = 1,
            IsSortable = true,
            IsAggregatable = false
        },
        new()
        {
            FieldKey = "programTitle",
            DisplayName = "Program Title",
            DataType = FieldDataType.String,
            IsRequired = false,
            IsDefaultSelected = true,
            Category = "Basic",
            ColumnWidth = "15%",
            Description = "Associated program title",
            DisplayOrder = 2,
            IsSortable = true,
            IsAggregatable = false
        },
        new()
        {
            FieldKey = "noOfFacebookSMS",
            DisplayName = "Facebook Messages",
            DataType = FieldDataType.Integer,
            IsRequired = false,
            IsDefaultSelected = true,
            Category = "Social Media",
            ColumnWidth = "8%",
            Description = "Number of Facebook messages",
            DisplayOrder = 10,
            IsSortable = true,
            IsAggregatable = true
        },
        new()
        {
            FieldKey = "noOfWhatsappGroups",
            DisplayName = "WhatsApp Groups",
            DataType = FieldDataType.Integer,
            IsRequired = false,
            IsDefaultSelected = true,
            Category = "Social Media",
            ColumnWidth = "8%",
            Description = "Number of WhatsApp groups",
            DisplayOrder = 11,
            IsSortable = true,
            IsAggregatable = true
        },
        new()
        {
            FieldKey = "noOfWhatsappSMS",
            DisplayName = "WhatsApp Messages",
            DataType = FieldDataType.Integer,
            IsRequired = false,
            IsDefaultSelected = true,
            Category = "Social Media",
            ColumnWidth = "8%",
            Description = "Number of WhatsApp messages sent",
            DisplayOrder = 12,
            IsSortable = true,
            IsAggregatable = true
        },
        new()
        {
            FieldKey = "noOfAnsweredWhatsappQueries",
            DisplayName = "WhatsApp Queries Answered",
            DataType = FieldDataType.Integer,
            IsRequired = false,
            IsDefaultSelected = false,
            Category = "Social Media",
            ColumnWidth = "8%",
            Description = "Number of WhatsApp queries answered",
            DisplayOrder = 13,
            IsSortable = true,
            IsAggregatable = true
        },
        new()
        {
            FieldKey = "noOfSMSSentToRegisteredFarmers",
            DisplayName = "SMS Sent",
            DataType = FieldDataType.Integer,
            IsRequired = false,
            IsDefaultSelected = true,
            Category = "Communication",
            ColumnWidth = "8%",
            Description = "SMS sent to registered farmers",
            DisplayOrder = 20,
            IsSortable = true,
            IsAggregatable = true
        },
        new()
        {
            FieldKey = "noOfPhoneCalls",
            DisplayName = "Phone Calls",
            DataType = FieldDataType.Integer,
            IsRequired = false,
            IsDefaultSelected = true,
            Category = "Communication",
            ColumnWidth = "8%",
            Description = "Number of phone calls made",
            DisplayOrder = 21,
            IsSortable = true,
            IsAggregatable = true
        },
        new()
        {
            FieldKey = "noOfEmailsSent",
            DisplayName = "Emails Sent",
            DataType = FieldDataType.Integer,
            IsRequired = false,
            IsDefaultSelected = false,
            Category = "Communication",
            ColumnWidth = "8%",
            Description = "Number of emails sent",
            DisplayOrder = 22,
            IsSortable = true,
            IsAggregatable = true
        },
        new()
        {
            FieldKey = "noOfFaceToFaceDiscussions",
            DisplayName = "Face-to-Face Discussions",
            DataType = FieldDataType.Integer,
            IsRequired = false,
            IsDefaultSelected = true,
            Category = "Direct Interaction",
            ColumnWidth = "8%",
            Description = "Number of face-to-face discussions",
            DisplayOrder = 30,
            IsSortable = true,
            IsAggregatable = true
        },
        new()
        {
            FieldKey = "noOfGroupDiscussions",
            DisplayName = "Group Discussions",
            DataType = FieldDataType.Integer,
            IsRequired = false,
            IsDefaultSelected = true,
            Category = "Direct Interaction",
            ColumnWidth = "8%",
            Description = "Number of group discussions conducted",
            DisplayOrder = 31,
            IsSortable = true,
            IsAggregatable = true
        },
        new()
        {
            FieldKey = "noOfNewspaperCoverage",
            DisplayName = "Newspaper Coverage",
            DataType = FieldDataType.Integer,
            IsRequired = false,
            IsDefaultSelected = false,
            Category = "Media",
            ColumnWidth = "8%",
            Description = "Number of newspaper coverages",
            DisplayOrder = 40,
            IsSortable = true,
            IsAggregatable = true
        },
        new()
        {
            FieldKey = "noOfBeneficiaries",
            DisplayName = "Beneficiaries",
            DataType = FieldDataType.Integer,
            IsRequired = false,
            IsDefaultSelected = true,
            Category = "Impact",
            ColumnWidth = "8%",
            Description = "Total number of beneficiaries",
            DisplayOrder = 50,
            IsSortable = true,
            IsAggregatable = true
        },
        new()
        {
            FieldKey = "totalContacts",
            DisplayName = "Total Contacts",
            DataType = FieldDataType.Integer,
            IsRequired = false,
            IsDefaultSelected = true,
            Category = "Impact",
            ColumnWidth = "8%",
            Description = "Total number of contacts made",
            DisplayOrder = 51,
            IsSortable = true,
            IsAggregatable = true
        }
    };

    var categories = new List<FieldCategory>
    {
        new()
        {
            CategoryKey = "Identification",
            DisplayName = "Identification",
            DisplayOrder = 1,
            IsCollapsedByDefault = false
        },
        new()
        {
            CategoryKey = "Basic",
            DisplayName = "Basic Information",
            DisplayOrder = 2,
            IsCollapsedByDefault = false
        },
        new()
        {
            CategoryKey = "Social Media",
            DisplayName = "Social Media Outreach",
            DisplayOrder = 3,
            IsCollapsedByDefault = false
        },
        new()
        {
            CategoryKey = "Communication",
            DisplayName = "Communication Channels",
            DisplayOrder = 4,
            IsCollapsedByDefault = false
        },
        new()
        {
            CategoryKey = "Direct Interaction",
            DisplayName = "Direct Interactions",
            DisplayOrder = 5,
            IsCollapsedByDefault = false
        },
        new()
        {
            CategoryKey = "Media",
            DisplayName = "Media Coverage",
            DisplayOrder = 6,
            IsCollapsedByDefault = true
        },
        new()
        {
            CategoryKey = "Impact",
            DisplayName = "Impact Metrics",
            DisplayOrder = 7,
            IsCollapsedByDefault = false
        }
    };

    return new SectionConfiguration
    {
        SectionKey = "advisoryServices",
        DisplayName = "Advisory Services",
        Description = "Extension advisory services and outreach activities",
        IconClass = "bi-chat-dots",
        DisplayOrder = 3,
        AvailableFields = fields,
        RequiredFields = new List<string> { "id" },
        DefaultSelectedFields = fields
            .Where(f => f.IsDefaultSelected)
            .Select(f => f.FieldKey)
            .ToList(),
        MaxFieldsAllowed = 0,
        SupportsPagination = true,
        SupportsSorting = true,
        SupportsFiltering = false,
        FieldCategories = categories
    };
}
```

### Sales Section (ATIC Only)

```csharp
private SectionConfiguration BuildSalesSection(int unitId)
{
    var fields = new List<FieldMetadata>
    {
        new()
        {
            FieldKey = "id",
            DisplayName = "ID",
            DataType = FieldDataType.Integer,
            IsRequired = true,
            IsDefaultSelected = true,
            Category = "Basic",
            ColumnWidth = "5%",
            DisplayOrder = 1,
            IsSortable = true,
            IsAggregatable = false
        },
        new()
        {
            FieldKey = "details",
            DisplayName = "Product/Service Details",
            DataType = FieldDataType.String,
            IsRequired = true,
            IsDefaultSelected = true,
            Category = "Basic",
            ColumnWidth = "30%",
            Description = "Details of the product or service sold",
            DisplayOrder = 2,
            IsSortable = true,
            IsAggregatable = false
        },
        new()
        {
            FieldKey = "quantityType",
            DisplayName = "Quantity Type",
            DataType = FieldDataType.Enum,
            IsRequired = true,
            IsDefaultSelected = true,
            Category = "Basic",
            ColumnWidth = "10%",
            Description = "Type of quantity measurement (Number, Kilogram, Unit)",
            DisplayOrder = 3,
            IsSortable = true,
            IsAggregatable = false
        },
        new()
        {
            FieldKey = "quantity",
            DisplayName = "Quantity",
            DataType = FieldDataType.Decimal,
            IsRequired = true,
            IsDefaultSelected = true,
            Category = "Basic",
            ColumnWidth = "10%",
            Description = "Quantity sold",
            DisplayOrder = 4,
            IsSortable = true,
            IsAggregatable = true,
            FormatString = "#,##0.00"
        },
        new()
        {
            FieldKey = "startDate",
            DisplayName = "Start Date",
            DataType = FieldDataType.Date,
            IsRequired = false,
            IsDefaultSelected = true,
            Category = "Period",
            ColumnWidth = "10%",
            Description = "Sales period start date",
            DisplayOrder = 10,
            IsSortable = true,
            IsAggregatable = false,
            FormatString = "dd/MM/yyyy"
        },
        new()
        {
            FieldKey = "endDate",
            DisplayName = "End Date",
            DataType = FieldDataType.Date,
            IsRequired = false,
            IsDefaultSelected = true,
            Category = "Period",
            ColumnWidth = "10%",
            Description = "Sales period end date",
            DisplayOrder = 11,
            IsSortable = true,
            IsAggregatable = false,
            FormatString = "dd/MM/yyyy"
        },
        new()
        {
            FieldKey = "formStatus",
            DisplayName = "Status",
            DataType = FieldDataType.Enum,
            IsRequired = true,
            IsDefaultSelected = true,
            Category = "Status",
            ColumnWidth = "8%",
            Description = "Entry status",
            DisplayOrder = 20,
            IsSortable = true,
            IsAggregatable = false
        }
    };

    return new SectionConfiguration
    {
        SectionKey = "sales",
        DisplayName = "Sales",
        Description = "Product and service sales records",
        IconClass = "bi-cart",
        DisplayOrder = 20,
        AvailableFields = fields,
        RequiredFields = new List<string> { "id", "details", "quantityType", "quantity", "formStatus" },
        DefaultSelectedFields = fields.Select(f => f.FieldKey).ToList(),
        MaxFieldsAllowed = 0,
        SupportsPagination = true,
        SupportsSorting = true,
        SupportsFiltering = false,
        FieldCategories = new List<FieldCategory>
        {
            new() { CategoryKey = "Basic", DisplayName = "Basic Information", DisplayOrder = 1, IsCollapsedByDefault = false },
            new() { CategoryKey = "Period", DisplayName = "Time Period", DisplayOrder = 2, IsCollapsedByDefault = false },
            new() { CategoryKey = "Status", DisplayName = "Status", DisplayOrder = 3, IsCollapsedByDefault = false }
        }
    };
}
```

### Visitor Details Section (ASM Only)

```csharp
private SectionConfiguration BuildVisitorDetailsSection(int unitId)
{
    var fields = new List<FieldMetadata>
    {
        new()
        {
            FieldKey = "id",
            DisplayName = "ID",
            DataType = FieldDataType.Integer,
            IsRequired = true,
            IsDefaultSelected = true,
            Category = "Basic",
            ColumnWidth = "5%",
            DisplayOrder = 1,
            IsSortable = true,
            IsAggregatable = false
        },
        new()
        {
            FieldKey = "instituteName",
            DisplayName = "Institute Name",
            DataType = FieldDataType.String,
            IsRequired = false,
            IsDefaultSelected = true,
            Category = "Basic",
            ColumnWidth = "20%",
            Description = "Name of visiting institute",
            DisplayOrder = 2,
            IsSortable = true,
            IsAggregatable = false
        },
        new()
        {
            FieldKey = "farmersCount",
            DisplayName = "Farmers",
            DataType = FieldDataType.Integer,
            IsRequired = false,
            IsDefaultSelected = true,
            Category = "Visitors",
            ColumnWidth = "8%",
            Description = "Number of farmer visitors",
            DisplayOrder = 10,
            IsSortable = true,
            IsAggregatable = true
        },
        new()
        {
            FieldKey = "studentsCount",
            DisplayName = "Students",
            DataType = FieldDataType.Integer,
            IsRequired = false,
            IsDefaultSelected = true,
            Category = "Visitors",
            ColumnWidth = "8%",
            Description = "Number of student visitors",
            DisplayOrder = 11,
            IsSortable = true,
            IsAggregatable = true
        },
        new()
        {
            FieldKey = "publicCount",
            DisplayName = "Public",
            DataType = FieldDataType.Integer,
            IsRequired = false,
            IsDefaultSelected = true,
            Category = "Visitors",
            ColumnWidth = "8%",
            Description = "Number of general public visitors",
            DisplayOrder = 12,
            IsSortable = true,
            IsAggregatable = true
        },
        new()
        {
            FieldKey = "totalVisitors",
            DisplayName = "Total Visitors",
            DataType = FieldDataType.Integer,
            IsRequired = true,
            IsDefaultSelected = true,
            Category = "Visitors",
            ColumnWidth = "8%",
            Description = "Total number of visitors",
            DisplayOrder = 13,
            IsSortable = true,
            IsAggregatable = true
        },
        new()
        {
            FieldKey = "submittedDate",
            DisplayName = "Submitted Date",
            DataType = FieldDataType.Date,
            IsRequired = false,
            IsDefaultSelected = true,
            Category = "Period",
            ColumnWidth = "10%",
            Description = "Submission date",
            DisplayOrder = 20,
            IsSortable = true,
            IsAggregatable = false,
            FormatString = "dd/MM/yyyy"
        },
        new()
        {
            FieldKey = "startDate",
            DisplayName = "Visit Start Date",
            DataType = FieldDataType.Date,
            IsRequired = false,
            IsDefaultSelected = false,
            Category = "Period",
            ColumnWidth = "10%",
            Description = "Visit start date",
            DisplayOrder = 21,
            IsSortable = true,
            IsAggregatable = false,
            FormatString = "dd/MM/yyyy"
        },
        new()
        {
            FieldKey = "endDate",
            DisplayName = "Visit End Date",
            DataType = FieldDataType.Date,
            IsRequired = false,
            IsDefaultSelected = false,
            Category = "Period",
            ColumnWidth = "10%",
            Description = "Visit end date",
            DisplayOrder = 22,
            IsSortable = true,
            IsAggregatable = false,
            FormatString = "dd/MM/yyyy"
        },
        new()
        {
            FieldKey = "formStatus",
            DisplayName = "Status",
            DataType = FieldDataType.Enum,
            IsRequired = true,
            IsDefaultSelected = true,
            Category = "Status",
            ColumnWidth = "8%",
            Description = "Entry status",
            DisplayOrder = 30,
            IsSortable = true,
            IsAggregatable = false
        }
    };

    return new SectionConfiguration
    {
        SectionKey = "visitorDetails",
        DisplayName = "Visitor Details",
        Description = "Museum visitor statistics and details",
        IconClass = "bi-people-fill",
        DisplayOrder = 1,
        AvailableFields = fields,
        RequiredFields = new List<string> { "id", "totalVisitors", "formStatus" },
        DefaultSelectedFields = fields
            .Where(f => f.IsDefaultSelected)
            .Select(f => f.FieldKey)
            .ToList(),
        MaxFieldsAllowed = 0,
        SupportsPagination = true,
        SupportsSorting = true,
        SupportsFiltering = false,
        FieldCategories = new List<FieldCategory>
        {
            new() { CategoryKey = "Basic", DisplayName = "Basic Information", DisplayOrder = 1, IsCollapsedByDefault = false },
            new() { CategoryKey = "Visitors", DisplayName = "Visitor Counts", DisplayOrder = 2, IsCollapsedByDefault = false },
            new() { CategoryKey = "Period", DisplayName = "Time Period", DisplayOrder = 3, IsCollapsedByDefault = false },
            new() { CategoryKey = "Status", DisplayName = "Status", DisplayOrder = 4, IsCollapsedByDefault = false }
        }
    };
}
```

---

## Implementation Steps

### Phase 1: Backend Foundation (Week 1)

#### Step 1.1: Create Models
```bash
# Create the new models in Application/Models/DynamicReporting/
- FieldMetadata.cs
- SectionConfiguration.cs (enhanced)
- DynamicReportConfigurationResponse.cs
- ValidationResult.cs
```

#### Step 1.2: Create Configuration Service
```bash
# Create service interface and implementation
- Application/Interface/IDynamicReportConfigurationService.cs
- Infracture/Services/DynamicReportConfigurationService.cs
```

#### Step 1.3: Enhance Existing Models
```bash
# Update existing models to support field selection
- Update DynamicReportRequest.cs to include selectedFields
- Update SectionRequest to have List<string> selectedFields
```

#### Step 1.4: Create Controller Endpoints
```bash
# Add new endpoints to DynamicReportController
- GET /api/admin/reports/dynamic/configuration
- POST /api/admin/reports/dynamic/validate-selection
- POST /api/admin/reports/dynamic/templates (save)
- GET /api/admin/reports/dynamic/templates/{id} (load)
```

### Phase 2: Frontend Foundation (Week 2)

#### Step 2.1: Create TypeScript Models
```bash
# In gkvk-loginportal repo
src/app/pages/reports/models/
- field-selector.models.ts
- report-builder-state.model.ts
```

#### Step 2.2: Create Field Selector Component
```bash
src/app/pages/reports/components/field-selector/
- field-selector.component.ts
- field-selector.component.html
- field-selector.component.scss
```

#### Step 2.3: Create Section Config Component
```bash
src/app/pages/reports/components/section-config/
- section-config.component.ts
- section-config.component.html
- section-config.component.scss
```

#### Step 2.4: Enhance Reports Service
```typescript
// Add methods to dynamic-report.service.ts
- getConfiguration(unitId: number, includeTemplates: boolean)
- validateFieldSelection(unitId, sectionKey, selectedFields)
- saveTemplate(template)
- getTemplate(templateId)
```

### Phase 3: Integration (Week 3)

#### Step 3.1: Update Reports Component
- Add stepper/wizard UI
- Integrate field selector
- Add template management
- Update PDF generation to respect field selection

#### Step 3.2: Update Report Generation
- Modify backend to only include selected fields in queries
- Update DTO mapping to respect field selection
- Optimize queries based on selected fields

#### Step 3.3: Add Export Support
- Update PDF export to use selected fields
- Add Excel export with selected fields
- Add CSV export with selected fields

### Phase 4: Testing & Polish (Week 4)

#### Step 4.1: Testing
- Unit tests for validation logic
- Integration tests for API endpoints
- E2E tests for field selection workflow
- Performance testing with large datasets

#### Step 4.2: Documentation
- API documentation
- User guide for field selection
- Developer guide for adding new sections

#### Step 4.3: Polish
- Loading indicators
- Error handling
- Responsive design
- Accessibility improvements

---

## Usage Examples

### Example 1: Generate Report with Selected Fields

**Frontend Request:**
```typescript
const request: DynamicReportRequest = {
  unitLocationId: 123,
  month: 3,
  year: 2024,
  sections: [
    {
      sectionKey: 'programDetails',
      selectedFields: [
        'id',
        'title',
        'startDate',
        'endDate',
        'location',
        'programType',
        'mode',
        'totalParticipants',
        'formStatus'
      ],
      sortBy: 'startDate',
      sortDirection: 'desc'
    },
    {
      sectionKey: 'participantDemographics',
      selectedFields: [
        'id',
        'programTitle',
        'male_SC',
        'male_ST',
        'male_OBC',
        'male_GEN',
        'female_SC',
        'female_ST',
        'female_OBC',
        'female_GEN',
        'totalMale',
        'totalFemale',
        'total'
      ]
    }
  ]
};

const report = await this.dynamicReportService.generateReport(request);
```

### Example 2: Save Custom Template

```typescript
const template = {
  name: "Monthly Training Summary",
  description: "Standard monthly report with key metrics",
  unitId: 1, // FTI
  sections: [
    {
      sectionKey: 'programDetails',
      selectedFields: ['id', 'title', 'startDate', 'location', 'totalParticipants'],
      sortBy: 'startDate'
    },
    {
      sectionKey: 'advisoryServices',
      selectedFields: ['noOfWhatsappGroups', 'noOfPhoneCalls', 'noOfBeneficiaries']
    }
  ],
  isPublic: true
};

const templateId = await this.dynamicReportService.saveTemplate(template);
```

### Example 3: Load and Use Template

```typescript
// Load template
const template = await this.dynamicReportService.getTemplate(templateId);

// Apply to report builder
this.applyTemplate(template);

// Generate report
const report = await this.dynamicReportService.generateReport({
  unitLocationId: this.selectedUnitLocationId,
  month: this.selectedMonth,
  year: this.selectedYear,
  sections: template.sections
});
```

---

## Benefits Summary

### For Users
1. **Customization**: Choose exactly which fields to include
2. **Efficiency**: Save custom templates for recurring reports
3. **Clarity**: See only relevant data
4. **Performance**: Smaller reports load faster
5. **Flexibility**: Different reports for different purposes

### For Developers
1. **Maintainability**: No hardcoded field lists
2. **Extensibility**: Easy to add new fields
3. **Consistency**: Unified field metadata system
4. **Reusability**: Template system reduces duplication
5. **Scalability**: Backend-driven configuration

### For Organization
1. **Standardization**: Consistent reporting across units
2. **Audit Trail**: Track which fields are commonly used
3. **Training**: Templates help new users
4. **Insights**: Analytics on field usage
5. **Compliance**: Ensure required fields are always included

---

## Next Steps

1. Review and approve this design
2. Set up development branches
3. Begin Phase 1 implementation (backend)
4. Parallel Phase 2 implementation (frontend)
5. Integration testing
6. User acceptance testing
7. Deployment

---

## Questions to Address

1. Should we support custom field formulas? (e.g., calculated fields)
2. Should we allow users to reorder fields?
3. Should we support conditional field visibility?
4. Should we add field-level permissions?
5. Should we support exporting field selection configurations?

---

**Document Version**: 1.0
**Last Updated**: 2025-12-16
**Status**: Design Complete - Ready for Implementation

# Dynamic Field Selection - Design Document

## Overview

This document extends the existing GKVK dynamic report system to support **dynamic field selection** for all 11 units. Users can now select specific fields within each section (Programs, Publications, Participants, Advisory Services, etc.) to create fully customizable reports.

---

## Current System vs Enhanced System

### Current System
- Users select **sections** (e.g., "Program Details", "Participants")
- All columns in selected sections are included
- Backend controls available sections per unit

### Enhanced System ✨
- Users select **sections** (same as before)
- Users also select **specific fields/columns** within each section
- Field-level customization for fine-grained control
- Backend provides field metadata (label, type, required, etc.)
- Smart defaults and recommendations

---

## Architecture Changes

### Frontend Changes (Angular)

```
src/app/pages/reports/
├── models/
│   ├── dynamic-report.models.ts (ENHANCED)
│   ├── field-selector.models.ts (NEW)
│   └── report-field-metadata.ts (NEW)
├── components/
│   ├── reports.component.ts (ENHANCED)
│   ├── reports.component.html (ENHANCED)
│   ├── field-selector/ (NEW COMPONENT)
│   │   ├── field-selector.component.ts
│   │   ├── field-selector.component.html
│   │   └── field-selector.component.scss
│   └── section-config/ (NEW COMPONENT)
│       ├── section-config.component.ts
│       ├── section-config.component.html
│       └── section-config.component.scss
└── services/
    └── dynamic-report.service.ts (ENHANCED)
```

### Backend Changes (ASP.NET Core)

```
Application/
├── Models/DynamicReporting/
│   ├── FieldMetadata.cs (NEW)
│   ├── SectionConfiguration.cs (ENHANCED)
│   └── DynamicReportRequest.cs (ENHANCED)
├── Services/
│   ├── IDynamicReportConfigurationService.cs (NEW)
│   └── DynamicReportConfigurationService.cs (NEW)
└── Controllers/
    └── DynamicReportController.cs (ENHANCED)
```

---

## Data Models

### Backend Models (C#)

#### FieldMetadata.cs
```csharp
public class FieldMetadata
{
    /// <summary>
    /// Unique field key (e.g., "title", "startDate", "totalParticipants")
    /// </summary>
    public required string FieldKey { get; set; }

    /// <summary>
    /// Display name for the field (e.g., "Program Title", "Start Date")
    /// </summary>
    public required string DisplayName { get; set; }

    /// <summary>
    /// Data type of the field
    /// </summary>
    public FieldDataType DataType { get; set; }

    /// <summary>
    /// Whether this field is required (cannot be deselected)
    /// </summary>
    public bool IsRequired { get; set; }

    /// <summary>
    /// Whether this field is selected by default
    /// </summary>
    public bool IsDefaultSelected { get; set; }

    /// <summary>
    /// Category/group for organizing fields in UI
    /// </summary>
    public string? Category { get; set; }

    /// <summary>
    /// Recommended column width (in percentage or pixels)
    /// </summary>
    public string? ColumnWidth { get; set; }

    /// <summary>
    /// Field description or help text
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Display order in the UI
    /// </summary>
    public int DisplayOrder { get; set; }

    /// <summary>
    /// Whether this field can be used for sorting
    /// </summary>
    public bool IsSortable { get; set; }

    /// <summary>
    /// Whether this field can be aggregated (for summary rows)
    /// </summary>
    public bool IsAggregatable { get; set; }

    /// <summary>
    /// Format string for display (e.g., "dd/MM/yyyy" for dates)
    /// </summary>
    public string? FormatString { get; set; }
}

public enum FieldDataType
{
    String,
    Integer,
    Decimal,
    Date,
    DateTime,
    Boolean,
    Currency,
    Percentage,
    Enum,
    File,
    Url
}
```

#### SectionConfiguration.cs (Enhanced)
```csharp
public class SectionConfiguration
{
    /// <summary>
    /// Unique section key (e.g., "programDetails", "participants")
    /// </summary>
    public required string SectionKey { get; set; }

    /// <summary>
    /// Display name for the section
    /// </summary>
    public required string DisplayName { get; set; }

    /// <summary>
    /// Section description
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Icon class for UI (e.g., "bi-file-text", "bi-people")
    /// </summary>
    public string? IconClass { get; set; }

    /// <summary>
    /// Display order in the UI
    /// </summary>
    public int DisplayOrder { get; set; }

    /// <summary>
    /// Available fields in this section
    /// </summary>
    public required List<FieldMetadata> AvailableFields { get; set; }

    /// <summary>
    /// Fields that are always included (cannot be deselected)
    /// </summary>
    public List<string> RequiredFields { get; set; } = new();

    /// <summary>
    /// Fields selected by default
    /// </summary>
    public List<string> DefaultSelectedFields { get; set; } = new();

    /// <summary>
    /// Maximum number of fields that can be selected (0 = unlimited)
    /// </summary>
    public int MaxFieldsAllowed { get; set; } = 0;

    /// <summary>
    /// Whether this section supports pagination
    /// </summary>
    public bool SupportsPagination { get; set; } = true;

    /// <summary>
    /// Whether this section supports sorting
    /// </summary>
    public bool SupportsSorting { get; set; } = true;

    /// <summary>
    /// Whether this section supports filtering
    /// </summary>
    public bool SupportsFiltering { get; set; } = false;

    /// <summary>
    /// Field categories for organizing fields in UI
    /// </summary>
    public List<FieldCategory>? FieldCategories { get; set; }
}

public class FieldCategory
{
    public required string CategoryKey { get; set; }
    public required string DisplayName { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsCollapsedByDefault { get; set; }
}
```

#### DynamicReportConfigurationResponse.cs (Enhanced)
```csharp
public class DynamicReportConfigurationResponse
{
    /// <summary>
    /// Unit information
    /// </summary>
    public required UnitInfo Unit { get; set; }

    /// <summary>
    /// Available sections with their field configurations
    /// </summary>
    public required List<SectionConfiguration> Sections { get; set; }

    /// <summary>
    /// Global settings for this unit
    /// </summary>
    public ReportGlobalSettings GlobalSettings { get; set; } = new();

    /// <summary>
    /// Saved report templates (if any)
    /// </summary>
    public List<ReportTemplate>? SavedTemplates { get; set; }
}

public class ReportGlobalSettings
{
    public int MaxSectionsAllowed { get; set; } = 10;
    public bool AllowSaveAsTemplate { get; set; } = true;
    public bool AllowExport { get; set; } = true;
    public List<string> ExportFormats { get; set; } = new() { "PDF", "Excel", "CSV" };
    public int DefaultPageSize { get; set; } = 50;
    public List<int> PageSizeOptions { get; set; } = new() { 10, 25, 50, 100, 200 };
}

public class ReportTemplate
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required List<TemplateSectionConfig> Sections { get; set; }
    public bool IsPublic { get; set; }
    public int CreatedByUserId { get; set; }
    public string? CreatedByName { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}

public class TemplateSectionConfig
{
    public required string SectionKey { get; set; }
    public required List<string> SelectedFields { get; set; }
    public string? SortBy { get; set; }
    public string? SortDirection { get; set; }
}
```

### Frontend Models (TypeScript)

#### field-selector.models.ts
```typescript
export interface FieldMetadata {
  fieldKey: string;
  displayName: string;
  dataType: FieldDataType;
  isRequired: boolean;
  isDefaultSelected: boolean;
  category?: string;
  columnWidth?: string;
  description?: string;
  displayOrder: number;
  isSortable: boolean;
  isAggregatable: boolean;
  formatString?: string;
}

export enum FieldDataType {
  String = 'String',
  Integer = 'Integer',
  Decimal = 'Decimal',
  Date = 'Date',
  DateTime = 'DateTime',
  Boolean = 'Boolean',
  Currency = 'Currency',
  Percentage = 'Percentage',
  Enum = 'Enum',
  File = 'File',
  Url = 'Url'
}

export interface SectionConfiguration {
  sectionKey: string;
  displayName: string;
  description?: string;
  iconClass?: string;
  displayOrder: number;
  availableFields: FieldMetadata[];
  requiredFields: string[];
  defaultSelectedFields: string[];
  maxFieldsAllowed: number;
  supportsPagination: boolean;
  supportsSorting: boolean;
  supportsFiltering: boolean;
  fieldCategories?: FieldCategory[];
}

export interface FieldCategory {
  categoryKey: string;
  displayName: string;
  displayOrder: number;
  isCollapsedByDefault: boolean;
}

export interface DynamicReportConfigurationResponse {
  unit: UnitInfo;
  sections: SectionConfiguration[];
  globalSettings: ReportGlobalSettings;
  savedTemplates?: ReportTemplate[];
}

export interface ReportGlobalSettings {
  maxSectionsAllowed: number;
  allowSaveAsTemplate: boolean;
  allowExport: boolean;
  exportFormats: string[];
  defaultPageSize: number;
  pageSizeOptions: number[];
}

export interface ReportTemplate {
  id: number;
  name: string;
  description?: string;
  sections: TemplateSectionConfig[];
  isPublic: boolean;
  createdByUserId: number;
  createdByName?: string;
  createdAt: string;
}

export interface TemplateSectionConfig {
  sectionKey: string;
  selectedFields: string[];
  sortBy?: string;
  sortDirection?: string;
}

// UI State Models
export interface SectionSelectionState {
  sectionKey: string;
  isSelected: boolean;
  selectedFields: Set<string>;
  sortBy?: string;
  sortDirection?: 'asc' | 'desc';
  pageSize?: number;
}

export interface ReportBuilderState {
  unitId: number;
  unitLocationId: number;
  month: number;
  year: number;
  selectedSections: Map<string, SectionSelectionState>;
  isPreviewMode: boolean;
  currentStep: 'unit-selection' | 'section-selection' | 'field-selection' | 'preview' | 'generate';
}
```

---

## API Endpoints

### Enhanced Endpoints

#### 1. Get Report Configuration with Field Metadata
```
GET /api/admin/reports/dynamic/configuration
Query Parameters:
  - unitId: number (required)
  - includeTemplates: boolean (optional, default: false)

Response: DynamicReportConfigurationResponse
```

#### 2. Validate Field Selection
```
POST /api/admin/reports/dynamic/validate-selection
Body: {
  unitId: number,
  sectionKey: string,
  selectedFields: string[]
}

Response: {
  isValid: boolean,
  errors: string[],
  warnings: string[]
}
```

#### 3. Save Report Template
```
POST /api/admin/reports/dynamic/templates
Body: {
  name: string,
  description?: string,
  unitId: number,
  sections: TemplateSectionConfig[],
  isPublic: boolean
}

Response: {
  templateId: number,
  message: string
}
```

#### 4. Load Report Template
```
GET /api/admin/reports/dynamic/templates/{templateId}

Response: ReportTemplate
```

#### 5. Generate Report with Field Selection
```
POST /api/admin/reports/dynamic/generate
Body: {
  unitLocationId: number,
  month: number,
  year: number,
  sections: SectionRequest[]  // Enhanced with field selection
}

SectionRequest (Enhanced): {
  sectionKey: string,
  selectedFields: string[],  // NEW: specific fields to include
  sortBy?: string,
  sortDirection?: string,
  pageSize?: number,
  pageNumber?: number
}

Response: DynamicReportData (with only selected fields)
```

---

## Frontend Implementation

### Component Structure

#### 1. Reports Component (Enhanced)
```typescript
// reports.component.ts
export class ReportsComponent implements OnInit {
  // State management
  reportBuilderState: ReportBuilderState;
  configuration: DynamicReportConfigurationResponse | null = null;

  // Stepper
  currentStep: 'unit-selection' | 'section-selection' | 'field-selection' | 'preview' | 'generate' = 'unit-selection';

  // Available data
  units: Unit[] = [];
  unitLocations: UnitLocation[] = [];
  months = this.generateMonths();
  years = this.generateYears();

  // Selected values
  selectedUnitId: number | null = null;
  selectedUnitLocationId: number | null = null;
  selectedMonth: number = new Date().getMonth() + 1;
  selectedYear: number = new Date().getFullYear();

  // Section and field selection
  selectedSections: Map<string, SectionSelectionState> = new Map();

  // Preview data
  previewData: DynamicReportData | null = null;

  // Templates
  availableTemplates: ReportTemplate[] = [];
  selectedTemplateId: number | null = null;

  constructor(
    private dynamicReportService: DynamicReportService,
    private toastr: ToastrService
  ) {}

  async ngOnInit() {
    await this.loadUnits();
  }

  // Step 1: Unit Selection
  async onUnitSelected(unitId: number) {
    this.selectedUnitId = unitId;
    await this.loadConfiguration();
    this.currentStep = 'section-selection';
  }

  // Step 2: Section Selection
  async loadConfiguration() {
    this.configuration = await this.dynamicReportService.getConfiguration(
      this.selectedUnitId!,
      true // include templates
    );
    this.availableTemplates = this.configuration.savedTemplates || [];
  }

  toggleSection(sectionKey: string) {
    if (this.selectedSections.has(sectionKey)) {
      this.selectedSections.delete(sectionKey);
    } else {
      const section = this.configuration!.sections.find(s => s.sectionKey === sectionKey)!;
      this.selectedSections.set(sectionKey, {
        sectionKey,
        isSelected: true,
        selectedFields: new Set(section.defaultSelectedFields),
        pageSize: this.configuration!.globalSettings.defaultPageSize
      });
    }
  }

  // Step 3: Field Selection
  proceedToFieldSelection() {
    if (this.selectedSections.size === 0) {
      this.toastr.warning('Please select at least one section');
      return;
    }
    this.currentStep = 'field-selection';
  }

  onFieldSelectionChanged(sectionKey: string, selectedFields: Set<string>) {
    const state = this.selectedSections.get(sectionKey);
    if (state) {
      state.selectedFields = selectedFields;
    }
  }

  // Step 4: Preview
  async generatePreview() {
    try {
      const request = this.buildReportRequest(true); // preview mode
      this.previewData = await this.dynamicReportService.generateReport(request, true);
      this.currentStep = 'preview';
    } catch (error) {
      this.toastr.error('Failed to generate preview');
    }
  }

  // Step 5: Generate
  async generateReport() {
    try {
      const request = this.buildReportRequest(false);
      const reportData = await this.dynamicReportService.generateReport(request, false);
      this.downloadPdf(reportData);
      this.toastr.success('Report generated successfully');
    } catch (error) {
      this.toastr.error('Failed to generate report');
    }
  }

  private buildReportRequest(isPreview: boolean): DynamicReportRequest {
    const sections: SectionRequest[] = Array.from(this.selectedSections.values()).map(state => ({
      sectionKey: state.sectionKey,
      selectedFields: Array.from(state.selectedFields),
      sortBy: state.sortBy,
      sortDirection: state.sortDirection,
      pageSize: isPreview ? 5 : state.pageSize
    }));

    return {
      unitLocationId: this.selectedUnitLocationId!,
      month: this.selectedMonth,
      year: this.selectedYear,
      sections
    };
  }

  // Template Management
  async loadTemplate(templateId: number) {
    const template = await this.dynamicReportService.getTemplate(templateId);
    this.applyTemplate(template);
  }

  private applyTemplate(template: ReportTemplate) {
    this.selectedSections.clear();
    template.sections.forEach(sectionConfig => {
      this.selectedSections.set(sectionConfig.sectionKey, {
        sectionKey: sectionConfig.sectionKey,
        isSelected: true,
        selectedFields: new Set(sectionConfig.selectedFields),
        sortBy: sectionConfig.sortBy,
        sortDirection: sectionConfig.sortDirection as 'asc' | 'desc'
      });
    });
    this.currentStep = 'field-selection';
  }

  async saveAsTemplate() {
    const name = prompt('Enter template name:');
    if (!name) return;

    const template = {
      name,
      description: '',
      unitId: this.selectedUnitId!,
      sections: Array.from(this.selectedSections.values()).map(state => ({
        sectionKey: state.sectionKey,
        selectedFields: Array.from(state.selectedFields),
        sortBy: state.sortBy,
        sortDirection: state.sortDirection
      })),
      isPublic: false
    };

    await this.dynamicReportService.saveTemplate(template);
    this.toastr.success('Template saved successfully');
  }
}
```

#### 2. Field Selector Component (New)
```typescript
// field-selector.component.ts
import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-field-selector',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './field-selector.component.html',
  styleUrls: ['./field-selector.component.scss']
})
export class FieldSelectorComponent implements OnInit {
  @Input() sectionConfig!: SectionConfiguration;
  @Input() initialSelectedFields: Set<string> = new Set();
  @Output() selectionChanged = new EventEmitter<Set<string>>();

  selectedFields: Set<string> = new Set();
  fieldsByCategory: Map<string, FieldMetadata[]> = new Map();
  collapsedCategories: Set<string> = new Set();
  searchTerm: string = '';

  ngOnInit() {
    this.selectedFields = new Set(this.initialSelectedFields);
    this.organizeFieldsByCategory();
    this.initializeCollapsedState();
  }

  private organizeFieldsByCategory() {
    this.fieldsByCategory.clear();

    // Group fields by category
    this.sectionConfig.availableFields.forEach(field => {
      const category = field.category || 'General';
      if (!this.fieldsByCategory.has(category)) {
        this.fieldsByCategory.set(category, []);
      }
      this.fieldsByCategory.get(category)!.push(field);
    });

    // Sort fields within each category
    this.fieldsByCategory.forEach(fields => {
      fields.sort((a, b) => a.displayOrder - b.displayOrder);
    });
  }

  private initializeCollapsedState() {
    if (this.sectionConfig.fieldCategories) {
      this.sectionConfig.fieldCategories
        .filter(cat => cat.isCollapsedByDefault)
        .forEach(cat => this.collapsedCategories.add(cat.categoryKey));
    }
  }

  toggleField(fieldKey: string) {
    if (this.isFieldRequired(fieldKey)) return;

    if (this.selectedFields.has(fieldKey)) {
      this.selectedFields.delete(fieldKey);
    } else {
      if (this.canAddMoreFields()) {
        this.selectedFields.add(fieldKey);
      }
    }

    this.selectionChanged.emit(new Set(this.selectedFields));
  }

  toggleCategory(categoryKey: string) {
    if (this.collapsedCategories.has(categoryKey)) {
      this.collapsedCategories.delete(categoryKey);
    } else {
      this.collapsedCategories.add(categoryKey);
    }
  }

  selectAll() {
    this.sectionConfig.availableFields.forEach(field => {
      this.selectedFields.add(field.fieldKey);
    });
    this.selectionChanged.emit(new Set(this.selectedFields));
  }

  deselectAll() {
    // Keep only required fields
    const newSelection = new Set<string>();
    this.sectionConfig.requiredFields.forEach(key => newSelection.add(key));
    this.selectedFields = newSelection;
    this.selectionChanged.emit(new Set(this.selectedFields));
  }

  selectDefaults() {
    this.selectedFields = new Set(this.sectionConfig.defaultSelectedFields);
    this.selectionChanged.emit(new Set(this.selectedFields));
  }

  isFieldRequired(fieldKey: string): boolean {
    return this.sectionConfig.requiredFields.includes(fieldKey);
  }

  isFieldSelected(fieldKey: string): boolean {
    return this.selectedFields.has(fieldKey);
  }

  canAddMoreFields(): boolean {
    if (this.sectionConfig.maxFieldsAllowed === 0) return true;
    return this.selectedFields.size < this.sectionConfig.maxFieldsAllowed;
  }

  get filteredFields(): FieldMetadata[] {
    if (!this.searchTerm) return this.sectionConfig.availableFields;

    const term = this.searchTerm.toLowerCase();
    return this.sectionConfig.availableFields.filter(field =>
      field.displayName.toLowerCase().includes(term) ||
      field.fieldKey.toLowerCase().includes(term) ||
      field.description?.toLowerCase().includes(term)
    );
  }

  getFieldIcon(dataType: FieldDataType): string {
    const icons: Record<FieldDataType, string> = {
      String: 'bi-text-left',
      Integer: 'bi-123',
      Decimal: 'bi-calculator',
      Date: 'bi-calendar-date',
      DateTime: 'bi-clock',
      Boolean: 'bi-check-square',
      Currency: 'bi-currency-dollar',
      Percentage: 'bi-percent',
      Enum: 'bi-list-ul',
      File: 'bi-file-earmark',
      Url: 'bi-link-45deg'
    };
    return icons[dataType] || 'bi-question-circle';
  }
}
```

---

## UI/UX Design

### Step-by-Step Wizard

```
┌─────────────────────────────────────────────────────────────┐
│  Dynamic Report Builder                                      │
├─────────────────────────────────────────────────────────────┤
│                                                              │
│  Steps:  ① Unit  →  ② Sections  →  ③ Fields  →  ④ Preview  │
│                                                              │
└─────────────────────────────────────────────────────────────┘
```

### Step 3: Field Selection UI

```html
<!-- Field Selection Step -->
<div class="field-selection-container">
  <!-- Section Tabs -->
  <ul class="nav nav-tabs mb-3">
    <li class="nav-item" *ngFor="let sectionKey of selectedSections.keys()">
      <a class="nav-link"
         [class.active]="activeSectionTab === sectionKey"
         (click)="activeSectionTab = sectionKey">
        {{ getSectionDisplayName(sectionKey) }}
        <span class="badge bg-primary ms-2">
          {{ getSelectedFieldCount(sectionKey) }}
        </span>
      </a>
    </li>
  </ul>

  <!-- Active Section Field Selector -->
  <app-field-selector
    [sectionConfig]="getSectionConfig(activeSectionTab)"
    [initialSelectedFields]="getSelectedFields(activeSectionTab)"
    (selectionChanged)="onFieldSelectionChanged(activeSectionTab, $event)">
  </app-field-selector>
</div>
```

### Field Selector Component Template

```html
<!-- field-selector.component.html -->
<div class="field-selector">
  <!-- Toolbar -->
  <div class="toolbar mb-3">
    <div class="row">
      <div class="col-md-6">
        <input type="text"
               class="form-control"
               [(ngModel)]="searchTerm"
               placeholder="Search fields...">
      </div>
      <div class="col-md-6 text-end">
        <button class="btn btn-sm btn-outline-primary me-2"
                (click)="selectAll()">
          Select All
        </button>
        <button class="btn btn-sm btn-outline-secondary me-2"
                (click)="deselectAll()">
          Deselect All
        </button>
        <button class="btn btn-sm btn-outline-info"
                (click)="selectDefaults()">
          Reset to Defaults
        </button>
      </div>
    </div>
    <div class="mt-2">
      <small class="text-muted">
        Selected: {{ selectedFields.size }} /
        {{ sectionConfig.maxFieldsAllowed || 'Unlimited' }}
      </small>
    </div>
  </div>

  <!-- Field List by Category -->
  <div class="field-categories">
    <div *ngFor="let category of fieldsByCategory | keyvalue"
         class="category-group mb-3">
      <!-- Category Header -->
      <div class="category-header"
           (click)="toggleCategory(category.key)">
        <i class="bi"
           [class.bi-chevron-down]="!collapsedCategories.has(category.key)"
           [class.bi-chevron-right]="collapsedCategories.has(category.key)"></i>
        <strong>{{ category.key }}</strong>
        <span class="badge bg-secondary ms-2">
          {{ category.value.length }}
        </span>
      </div>

      <!-- Fields in Category -->
      <div *ngIf="!collapsedCategories.has(category.key)"
           class="category-fields mt-2">
        <div *ngFor="let field of category.value"
             class="field-item"
             [class.selected]="isFieldSelected(field.fieldKey)"
             [class.required]="isFieldRequired(field.fieldKey)">
          <div class="form-check">
            <input class="form-check-input"
                   type="checkbox"
                   [id]="'field-' + field.fieldKey"
                   [checked]="isFieldSelected(field.fieldKey)"
                   [disabled]="isFieldRequired(field.fieldKey)"
                   (change)="toggleField(field.fieldKey)">
            <label class="form-check-label"
                   [for]="'field-' + field.fieldKey">
              <i class="bi {{ getFieldIcon(field.dataType) }} me-2"></i>
              <strong>{{ field.displayName }}</strong>
              <span *ngIf="isFieldRequired(field.fieldKey)"
                    class="badge bg-danger ms-2">Required</span>
              <br>
              <small class="text-muted">{{ field.description }}</small>
            </label>
          </div>
        </div>
      </div>
    </div>
  </div>
</div>
```

---

(Continued in next file...)

# TDMS GKVK Unit Models Documentation

## Overview

This document provides comprehensive documentation for all 11 organizational units in the TDMS (Training Data Management System) for GKVK. Each unit has its own set of entity models for tracking programs, activities, participants, and reports.

### Table of Contents
1. [FTI - Farmers Training Institute](#1-fti---farmers-training-institute)
2. [STU - Staff Training Unit](#2-stu---staff-training-unit)
3. [FIU - Farm Information Unit](#3-fiu---farm-information-unit)
4. [IBTVA - Institute of Baking Technology and Value Addition](#4-ibtva---institute-of-baking-technology-and-value-addition)
5. [ATIC - Agricultural Technology Information Centre](#5-atic---agricultural-technology-information-centre)
6. [DEU - Distance Education Unit](#6-deu---distance-education-unit)
7. [ASM - Agricultural Sciences Museum](#7-asm---agricultural-sciences-museum)
8. [NAEP - National Agriculture Extension Project](#8-naep---national-agriculture-extension-project)
9. [EEU - Extension Education Units](#9-eeu---extension-education-units)
10. [KVK - Krishi Vigyan Kendras](#10-kvk---krishi-vigyan-kendras)
11. [SAMETI - State Agricultural Management and Extension Training Institutes](#11-sameti---state-agricultural-management-and-extension-training-institutes)

---

## Base Entities

### AuditableBaseEntity
**Location:** `Domain/Entities/AuditableBaseEntity.cs`

All entities inherit from this base class for audit tracking.

**Properties:**
- `Id` (int) - Primary key
- `CreatedById` (int?) - User who created the record
- `CreatedAt` (DateTimeOffset) - Creation timestamp
- `UpdatedById` (int?) - User who last updated the record
- `UpdatedAt` (DateTimeOffset?) - Last update timestamp
- `CreatedBy` (User?) - Navigation to creator user
- `UpdatedBy` (User?) - Navigation to updater user

### ReportEntryBaseEntity
**Location:** `Domain/Entities/ReportEntryBaseEntity.cs`

Extends `AuditableBaseEntity` and adds common fields for all report entries.

**Properties:**
- All properties from `AuditableBaseEntity`
- `StartDate` (DateOnly) - Program/activity start date
- `EndDate` (DateOnly) - Program/activity end date
- `Attachements` (string?) - File attachments
- `UnitLocationId` (int) - Foreign key to unit location
- `OrganizationId` (int) - Foreign key to organization
- `UnitLocation` (OrganizationUnitLocation) - Navigation property
- `Organization` (Organization) - Navigation property
- `FormStatus` (string) - Status tracking ("Draft", "Pending", "Approved", "Rejected")
- `FormStatusRemarks` (string?) - Status remarks/comments
- `ApprovedAt` (DateTimeOffset?) - Approval timestamp
- `ApprovedById` (int?) - User who approved
- `ApprovedBy` (User?) - Navigation to approver user

---

## 1. FTI - Farmers Training Institute

**Unit ID:** 1
**Namespace:** `Domain.Entities.FTI`

### 1.1 Main Entities

#### FtiProgramDetails
**Location:** `Domain/Entities/FTI/FTIProgramDetails.cs`
**Base:** `ReportEntryBaseEntity`

**Purpose:** Core entity for tracking training programs conducted by FTI.

**Properties:**
- All properties from `ReportEntryBaseEntity`
- `ProgramTypeId` (int?) - Type of program
- `CategoryId` (int?) - Program category
- `CategoryOther` (string?) - Other category description
- `TypeId` (int?) - Information type
- `TypeOther` (string?) - Other type description
- `ThemeId` (int?) - Program theme
- `ThemeOther` (string?) - Other theme description
- `ThematicAreaId` (int?) - Thematic area
- `ThematicAreaOther` (string?) - Other thematic area
- `SponsoredOrganization` (int?) - Sponsor organization ID
- `SponsoredOrganizationName` (string?) - Sponsor name
- `Title` (string?) - Program title
- `ModeId` (int?) - Mode of delivery
- `Duration` (string?) - Program duration
- `RegionId` (int?) - Region
- `RegionOther` (string?) - Other region
- `TPNo` (int?) - Training program number
- `Location` (string?) - Location details
- `SourceOfFundId` (int?) - Funding source
- `Funds` (int?) - Fund amount
- `StatusId` (int?) - Program status
- `TotalOutlayRs` (decimal?) - Total outlay in Rupees
- `Copi` (string?) - Co-PI details
- `PiAddress` (string?) - PI address
- `BatchNo` (int?) - Batch number
- `Area` (decimal?) - Area covered
- `OrganizerBroucherFile` (string?) - Organizer brochure file
- `OrganizerInstitutionName` (string?) - Organizer institution
- `OrganizerInstitutionAddress` (string?) - Institution address
- `SourceId` (int?) - Information source
- `OtherSourceOfInformation` (string?) - Other source details
- `SourceOfTitle` (string?) - Source of title
- `ProposalDate` (DateOnly?) - Proposal submission date
- `ProposalUploadFile` (string?) - Proposal file
- `UniversitySanctionLetterDate` (DateOnly?) - University sanction date
- `UniversitySanctionLetterUploadFile` (string?) - Sanction letter file
- `ProjectSanctionDate` (DateOnly?) - Project sanction date
- `ProjectSanctionFile` (string?) - Project sanction file
- `UniImplDate` (DateOnly?) - University implementation date
- `UniImplLetterFile` (string?) - Implementation letter
- `FundReleaseYear` (string?) - Fund release year
- `FundAmount` (double?) - Fund amount
- `FundReleaseDate` (DateOnly?) - Fund release date
- `FundReleaseFile` (string?) - Fund release file
- `FundsSanctionLetterDate` (DateOnly?) - Funds sanction date
- `FundsSanctionLetterUploadFile` (string?) - Sanction letter
- `ReportingVideo` (string?) - Video file/URL

**Navigation Properties:**
- `ParticipantDemographics` (ICollection<FtiParticipantDemographics>)
- `ProgramContent` (ICollection<FtiProgramContentAndResources>)
- `AdvisoryServices` (FtiAdvisoryServices)
- `Recommendations` (FtiRecommendation)
- `Reports` (FtiReport)

#### FtiReport
**Location:** `Domain/Entities/FTI/FTIReport.cs`
**Base:** `AuditableBaseEntity`

**Purpose:** Tracks progress reports for FTI programs.

**Properties:**
- All properties from `AuditableBaseEntity`
- `FtiProgramDetailsId` (int?) - Foreign key to program details
- `ProgramDetails` (FtiProgramDetails?) - Navigation property
- `ReportingYear` (string?) - Year of reporting (4 chars)
- `ReportDate` (DateOnly?) - Report date
- `ProgressReport` (string?) - Progress report text
- `GeoTaggedPhoto` (string?) - Geo-tagged photo URL
- `ReportingVideo` (string?) - Video URL
- `Outcome` (string?) - Outcome description
- `TestingCompletionDate` (DateOnly?) - Testing completion date
- `TestingCompletionLetter` (string?) - Testing completion file
- `ProjectCompletionDate` (DateOnly?) - Project completion date
- `ProjectCompletionLetter` (string?) - Completion certificate
- `TypeOfReport` (string?) - Report type
- `SpclReport` (string?) - Special remarks
- `UnitLocationId` (int?)
- `OrganizationId` (int?)

### 1.2 Child Entities

#### FtiParticipantDemographics
**Location:** `Domain/Entities/FTI/FTIParticipantDemographics.cs`
**Base:** `AuditableBaseEntity`

**Purpose:** Tracks demographic information of program participants.

**Properties:**
- `FtiProgramDetailsId` (int?)
- `ParticipantId` (int?) - Participant category
- `Male_SC/ST/OBC/GEN` (int?) - Male participants by category
- `SC/ST/OBC/GEN_Male_StayedInHostel` (int?) - Male hostel residents
- `Female_SC/ST/OBC/GEN` (int?) - Female participants by category
- `SC/ST/OBC/GEN_Female_StayedInHostel` (int?) - Female hostel residents
- `Total` (int?) - Total participants
- `UnitLocationId` (int?)
- `OrganizationId` (int?)

#### FtiProgramContentAndResources
**Location:** `Domain/Entities/FTI/FTIProgramContentAndResources.cs`
**Base:** `AuditableBaseEntity`

**Purpose:** Container for program content and resources.

**Properties:**
- `FtiProgramDetailsId` (int?)
- `UnitLocationId` (int?)
- `OrganizationId` (int?)

**Navigation Properties:**
- `ResourcePersons` (ICollection<FtiResourcePerson>)
- `TopicsCovered` (ICollection<FtiTopicsCoveredInClass>)
- `TeachingAids` (ICollection<FtiTeachingAidsDeveloped>)

#### FtiAdvisoryServices
**Location:** `Domain/Entities/FTI/FTIAdvisoryServices.cs`
**Base:** `AuditableBaseEntity`

**Purpose:** Tracks advisory service metrics.

**Properties:**
- `FtiProgramDetailsId` (int?)
- `NoOfFacebookSMS` (int)
- `NoOfSMSSentToRegisteredFarmers` (int)
- `NoOfWhatsappGroups` (int)
- `NoOfWhatsappSMS` (int)
- `NoOfAnsweredWhatsappQueries` (int)
- `NoOfPhoneCalls` (int)
- `NoOfFaceToFaceDiscussions` (int)
- `NoOfGroupDiscussions` (int)
- `NoOfEmailsSent` (int)
- `NoOfNewspaperCoverage` (int)
- `NoOfBeneficiaries` (int)
- `UnitLocationId` (int?)
- `OrganizationId` (int?)

#### FtiRecommendation
**Location:** `Domain/Entities/FTI/FTIRecommendation.cs`
**Base:** `AuditableBaseEntity`

**Purpose:** Stores recommendations from the program.

#### FtiResourcePerson
**Location:** `Domain/Entities/FTI/FTIResourcePerson.cs`

**Purpose:** Tracks resource persons involved in training.

#### FtiTopicsCoveredInClass
**Location:** `Domain/Entities/FTI/FTITopicsCoveredInClass.cs`

**Purpose:** Lists topics covered in training sessions.

#### FtiTeachingAidsDeveloped
**Location:** `Domain/Entities/FTI/FTITeachingAidsDeveloped.cs`

**Purpose:** Documents teaching aids developed.

### 1.3 Special Entities

#### FtiTrainingProgram
**Location:** `Domain/Entities/FTI/FtiTrainingProgram.cs`
**Base:** `ReportEntryBaseEntity`

**Purpose:** Tracks training programs conducted.

**Properties:**
- All properties from `ReportEntryBaseEntity`
- `OrganisationName` (string) - Organization name
- `TrainingTitle` (string) - Training title
- `Duration` (TimeSpan) - Duration
- `TrainingCount` (int) - Number of trainings
- `ParticipantCount` (int) - Number of participants

#### FtiOtherActivity
**Location:** `Domain/Entities/FTI/FtiOtherActivity.cs`

**Purpose:** Tracks other miscellaneous activities.

---

## 2. STU - Staff Training Unit

**Unit ID:** 2
**Namespace:** `Domain.Entities.STU`

### 2.1 Main Entities

#### StuProgramDetails
**Location:** `Domain/Entities/STU/STUProgramDetails.cs`
**Base:** `ReportEntryBaseEntity`

**Purpose:** Core entity for tracking staff training programs.

**Properties:** Same structure as `FtiProgramDetails` with STU-specific navigation properties.

**Navigation Properties:**
- `ParticipantDemographics` (ICollection<StuParticipantDemographics>)
- `ProgramContent` (ICollection<StuProgramContentAndResources>)
- `AdvisoryServices` (StuAdvisoryServices)
- `Recommendations` (StuRecommendation)
- `Reports` (StuReport)

#### StuReport
**Location:** `Domain/Entities/STU/STUReport.cs`
**Base:** `AuditableBaseEntity`

**Purpose:** Tracks progress reports for STU programs.

**Properties:** Same structure as `FtiReport`.

### 2.2 Child Entities

All child entities follow the same pattern as FTI:
- `StuParticipantDemographics`
- `StuProgramContentAndResources`
- `StuAdvisoryServices`
- `StuRecommendation`
- `StuResourcePerson`
- `StuTopicsCoveredInClass`
- `StuTeachingAidsDeveloped`

### 2.3 Special Entities

#### StuTrainingProgramme
**Purpose:** Tracks STU training programmes (currently commented out in codebase).

#### StuSponsoredTrainingProgramme
**Purpose:** Tracks sponsored training programmes (currently commented out in codebase).

#### StuOtherActivity
**Location:** `Domain/Entities/STU/StuOtherActivity.cs`

**Purpose:** Tracks other STU activities.

#### DaesiProgramme
**Location:** `Domain/Entities/STU/DaesiProgramme.cs`

**Purpose:** Tracks DAESI (Diploma in Agricultural Extension Services for Input Dealers) programs.

#### DaesiOtherActivity
**Location:** `Domain/Entities/STU/DaesiOtherActivity.cs`

**Purpose:** Tracks other DAESI activities.

---

## 3. FIU - Farm Information Unit

**Unit ID:** 3
**Namespace:** `Domain.Entities.FIU`

### 3.1 Main Entities

#### FiuProgramme
**Location:** `Domain/Entities/FIU/FiuProgramme.cs`
**Base:** `ReportEntryBaseEntity`

**Purpose:** Main entity for FIU programs.

**Note:** The FIU unit has a unique structure focused on media coordination and activities rather than training programs.

#### FIUProgramActivity
**Location:** `Domain/Entities/FIU/FIUProgramActivity.cs`
**Base:** `AuditableBaseEntity`

**Purpose:** Tracks individual program activities with media uploads.

**Properties:**
- All properties from `AuditableBaseEntity`
- `UnitLocationId` (int)
- `OrganizationId` (int)
- `FIUActivitiesId` (int) - Foreign key to activity type
- `Number` (int) - Activity count (1-100000)
- `UploadMediaUrl` (string?) - Media file URL
- `Remarks` (string?) - Activity remarks
- `FormStatus` (string) - Status tracking
- `FormStatusRemarks` (string?)
- `SubmittedAt` (DateTimeOffset?)
- `ApprovedAt` (DateTimeOffset?)
- `ApprovedById` (int?)

**Navigation Properties:**
- `UnitLocation` (OrganizationUnitLocation)
- `Organization` (Organization)
- `FIUActivity` (FIUActivity)
- `ApprovedBy` (User?)

### 3.2 Master Data

#### FIUActivity
**Location:** `Domain/Entities/FIU/FIUActivity.cs`

**Purpose:** Master data table for FIU media coordination activity types.

**Properties:**
- `Id` (int)
- `ActivityName` (string) - Activity name
- `ActivityDescription` (string?)
- `ActivityCategory` (string) - Category
- `UnitOfMeasurement` (string?) - Measurement unit
- `RequiresMediaUpload` (bool) - Whether media upload is required
- `IsActive` (bool) - Active status
- `DisplayOrder` (int) - Display order

**Navigation Properties:**
- `ProgramActivities` (ICollection<FIUProgramActivity>)

### 3.3 Special Entities

#### FiuProgrammeType
**Location:** `Domain/Entities/FIU/FiuProgrammeType.cs`

**Purpose:** Defines types of FIU programmes.

#### FIUOtherActivity
**Location:** `Domain/Entities/FIU/FIUOtherActivity.cs`

**Purpose:** Tracks other FIU activities.

---

## 4. IBTVA - Institute of Baking Technology and Value Addition

**Unit ID:** 4
**Namespace:** `Domain.Entities.IBTVA`

### 4.1 Main Entities

#### IbtvaProgramDetails
**Location:** `Domain/Entities/IBTVA/IbtvaProgramDetails.cs`
**Base:** `ReportEntryBaseEntity`

**Purpose:** Core entity for IBTVA training and value addition programs.

**Properties:** Same structure as `FtiProgramDetails` with IBTVA-specific navigation properties.

**Navigation Properties:**
- `ParticipantDemographics` (ICollection<IbtvaParticipantDemographics>)
- `ProgramContent` (ICollection<IbtvaProgramContentAndResources>)
- `AdvisoryServices` (IbtvaAdvisoryServices)
- `Recommendations` (IbtvaRecommendation)
- `Reports` (IbtvaReport)

#### IbtvaReport
**Location:** `Domain/Entities/IBTVA/IbtvaReport.cs`
**Base:** `AuditableBaseEntity`

**Purpose:** Tracks progress reports for IBTVA programs.

**Properties:** Same structure as `FtiReport`.

### 4.2 Child Entities

All child entities follow the same pattern:
- `IbtvaParticipantDemographics`
- `IbtvaProgramContentAndResources`
- `IbtvaAdvisoryServices`
- `IbtvaRecommendation`
- `IbtvaResourcePerson`
- `IbtvaTopicsCoveredInClass`
- `IbtvaTeachingAidsDeveloped`

### 4.3 Special Entities

#### IbtvaProgramme
**Location:** `Domain/Entities/IBTVA/IbtvaProgramme.cs`
**Base:** `ReportEntryBaseEntity`

**Purpose:** Tracks IBTVA training programmes.

**Properties:**
- All properties from `ReportEntryBaseEntity`
- `TrainingTitle` (string) - Training title
- `Duration` (TimeSpan) - Duration
- `ParticipantCount` (int) - Number of participants

#### IbtavOtherActivity
**Location:** `Domain/Entities/IBTVA/IbtavOtherActivity.cs`

**Purpose:** Tracks other IBTVA activities.

---

## 5. ATIC - Agricultural Technology Information Centre

**Unit ID:** 5
**Namespace:** `Domain.Entities.ATIC`

### 5.1 Main Entities

#### AticProgramDetails
**Location:** `Domain/Entities/ATIC/AticProgramDetails.cs`
**Base:** `ReportEntryBaseEntity`

**Purpose:** Core entity for ATIC programs and information dissemination.

**Properties:** Same structure as `FtiProgramDetails` with ATIC-specific navigation properties.

**Navigation Properties:**
- `ParticipantDemographics` (ICollection<AticParticipantDemographics>)
- `ProgramContent` (ICollection<AticProgramContentAndResources>)
- `AdvisoryServices` (AticAdvisoryServices)
- `Recommendations` (AticRecommendation)
- `Reports` (AticReport)

#### AticReport
**Location:** `Domain/Entities/ATIC/AticReport.cs`
**Base:** `AuditableBaseEntity`

**Purpose:** Tracks progress reports for ATIC programs.

**Properties:** Same structure as `FtiReport`.

### 5.2 Child Entities

All child entities follow the same pattern:
- `AticParticipantDemographics`
- `AticProgramContentAndResources`
- `AticAdvisoryServices`
- `AticRecommendation`
- `AticResourcePerson`
- `AticTopicsCoveredInClass`
- `AticTeachingAidsDeveloped`

### 5.3 Unique Entities

#### AticSales
**Location:** `Domain/Entities/ATIC/AticSales.cs`
**Base:** `ReportEntryBaseEntity`

**Purpose:** Tracks ATIC product/material sales.

**Properties:**
- All properties from `ReportEntryBaseEntity`
- `Details` (string) - Sale details
- `QuantityType` (AticSalesQuantityType) - Type of quantity measurement
- `Quantity` (double) - Quantity sold

**Enum: AticSalesQuantityType**
- `NUMBER` - Count in numbers
- `KILOGRAM` - Weight in kilograms
- `UNIT` - In units

#### AticOtherActivity
**Location:** `Domain/Entities/ATIC/AticOtherActivity.cs`

**Purpose:** Tracks other ATIC activities.

---

## 6. DEU - Distance Education Unit

**Unit ID:** 6
**Namespace:** `Domain.Entities.DEU`

### 6.1 Main Entities

#### DeuProgramDetails
**Location:** `Domain/Entities/DEU/DeuProgramDetails.cs`
**Base:** `ReportEntryBaseEntity`

**Purpose:** Core entity for distance education programs.

**Properties:** Same structure as `FtiProgramDetails` with DEU-specific navigation properties.

**Navigation Properties:**
- `ParticipantDemographics` (ICollection<DeuParticipantDemographics>)
- `ProgramContent` (ICollection<DeuProgramContentAndResources>)
- `AdvisoryServices` (DeuAdvisoryServices)
- `Recommendations` (DeuRecommendation)
- `Reports` (DeuReport)

#### DeuReport
**Location:** `Domain/Entities/DEU/DeuReport.cs`
**Base:** `AuditableBaseEntity`

**Purpose:** Tracks progress reports for DEU programs.

**Properties:** Same structure as `FtiReport`.

### 6.2 Child Entities

All child entities follow the same pattern:
- `DeuParticipantDemographics`
- `DeuProgramContentAndResources`
- `DeuAdvisoryServices`
- `DeuRecommendation`
- `DeuResourcePerson`
- `DeuTopicsCoveredInClass`
- `DeuTeachingAidsDeveloped`

### 6.3 Special Entities

#### DeuCourse
**Purpose:** Tracks diploma and certificate courses (currently commented out in codebase).

**Properties (when active):**
- `Type` (DeuCourseType) - DIPLOMA or CERTIFICATE
- `Name` (string) - Course name
- `CandidateAdmittedCount` (int)
- `CandidateAttendedExamCount` (int)
- `CandidatePassedCount` (int)

#### DeuOtherActivity
**Location:** `Domain/Entities/DEU/DeuOtherActivity.cs`

**Purpose:** Tracks other DEU activities.

---

## 7. ASM - Agricultural Sciences Museum

**Unit ID:** 7
**Namespace:** `Domain.Entities.ASM`

### 7.1 Main Entities

#### AsmReport
**Location:** `Domain/Entities/ASM/ASMReport.cs`
**Base:** `ReportEntryBaseEntity`

**Purpose:** Simple visitor tracking report.

**Properties:**
- All properties from `ReportEntryBaseEntity`
- `InstituteName` (string?) - Visiting institution name
- `NoOfFarmers` (int?) - Number of farmer visitors
- `NoOfStudents` (int?) - Number of student visitors
- `NoOfPublic` (int?) - Number of public visitors

**Note:** ASM has a simpler model structure focused on visitor tracking.

### 7.2 Supporting Entities

#### ASMVisitorDetails
**Location:** `Domain/Entities/ASM/ASMVisitorDetails.cs`
**Base:** `AuditableBaseEntity`

**Purpose:** Detailed visitor information.

**Properties:**
- All properties from `AuditableBaseEntity`
- `InstituteName` (string?) - Institution name
- `FarmersCount` (int) - Farmer count
- `StudentsCount` (int) - Student count
- `PublicCount` (int) - Public count
- `SubmittedDate` (DateOnly) - Submission date
- `UnitLocationId` (int)
- `OrganizationId` (int)
- `StartDate` (DateOnly?)
- `EndDate` (DateOnly?)
- `FormStatus` (string) - Status tracking
- `FormStatusRemarks` (string?)
- `ApprovedAt` (DateTimeOffset?)
- `ApprovedById` (int?)

**Navigation Properties:**
- `UnitLocation` (OrganizationUnitLocation)
- `Organization` (Organization)
- `ApprovedBy` (User)

#### AsmVisit
**Location:** `Domain/Entities/ASM/AsmVisit.cs`

**Purpose:** Tracks individual museum visits.

#### ASMOtherActivity
**Location:** `Domain/Entities/ASM/ASMOtherActivity.cs`

**Purpose:** Tracks other ASM activities.

---

## 8. NAEP - National Agriculture Extension Project

**Unit ID:** 8
**Namespace:** `Domain.Entities.NAEP`

### 8.1 Main Entities

#### NaepProgramDetails
**Location:** `Domain/Entities/NAEP/NaepProgramDetails.cs`
**Base:** `ReportEntryBaseEntity`

**Purpose:** Core entity for NAEP extension programs.

**Properties:** Same structure as `FtiProgramDetails` with NAEP-specific navigation properties.

**Navigation Properties:**
- `ParticipantDemographics` (ICollection<NaepParticipantDemographics>)
- `ProgramContent` (ICollection<NaepProgramContentAndResources>)
- `AdvisoryServices` (NaepAdvisoryServices)
- `Recommendations` (NaepRecommendation)
- `Reports` (NaepReport)

#### NaepReport
**Location:** `Domain/Entities/NAEP/NaepReport.cs`
**Base:** `AuditableBaseEntity`

**Purpose:** Tracks progress reports for NAEP programs.

**Properties:** Same structure as `FtiReport`.

### 8.2 Child Entities

All child entities follow the same pattern:
- `NaepParticipantDemographics`
- `NaepProgramContentAndResources`
- `NaepAdvisoryServices`
- `NaepRecommendation`
- `NaepResourcePerson`
- `NaepTopicsCoveredInClass`
- `NaepTeachingAidsDeveloped`

### 8.3 Special Entities

#### NaepDetails
**Location:** `Domain/Entities/NAEP/NaepDetails.cs`

**Purpose:** Additional NAEP program details.

---

## 9. EEU - Extension Education Units

**Unit ID:** 9
**Namespace:** `Domain.Entities.EEU`

### 9.1 Main Entities

#### EeuProgramDetails
**Location:** `Domain/Entities/EEU/EeuProgramDetails.cs`
**Base:** `ReportEntryBaseEntity`

**Purpose:** Core entity for EEU extension programs.

**Properties:** All properties from `FtiProgramDetails` plus additional EEU-specific fields:
- `T01` to `T05` (string?) - Technology fields
- `StageOfCrop` (string?) - Crop stage
- `NoOfDemos` (int?) - Number of demonstrations
- `NoOfTrails` (int?) - Number of trials
- `NoOfChecks` (int?) - Number of checks
- `NoOfVisits` (int?) - Number of visits
- `ParticipatedAsId` (int?) - Participation role
- `ParticipantFileUpload` (string?) - Participant file

**Navigation Properties:**
- `ParticipantDemographics` (ICollection<EeuParticipantDemographics>)
- `ProgramContent` (ICollection<EeuProgramContentAndResources>)
- `AdvisoryServices` (EeuAdvisoryServices)
- `Recommendations` (EeuRecommendation)
- `Results` (EeuResult) - **Unique to EEU/KVK**
- `Reports` (EeuReport)

#### EeuReport
**Location:** `Domain/Entities/EEU/EeuReport.cs`
**Base:** `AuditableBaseEntity`

**Purpose:** Tracks progress reports for EEU programs.

**Properties:** Same structure as `FtiReport`.

### 9.2 Child Entities

All child entities follow the same pattern:
- `EeuParticipantDemographics`
- `EeuProgramContentAndResources`
- `EeuAdvisoryServices`
- `EeuRecommendation`
- `EeuResourcePerson`
- `EeuTopicsCoveredInClass`
- `EeuTeachingAidsDeveloped`

### 9.3 Unique Entities

#### EeuResult
**Location:** `Domain/Entities/EEU/EeuResult.cs`

**Purpose:** Container for FLD and OFT results.

**Navigation Properties:**
- `FldResults` (ICollection<EeuFldResult>)
- `OftResults` (ICollection<EeuOftResult>)

#### EeuFldResult
**Location:** `Domain/Entities/EEU/EeuFldResult.cs`

**Purpose:** Tracks Front Line Demonstration (FLD) results.

#### EeuOftResult
**Location:** `Domain/Entities/EEU/EeuOftResult.cs`

**Purpose:** Tracks On Farm Trial (OFT) results.

#### EeuFLD
**Location:** `Domain/Entities/EEU/EeuFLD.cs`

**Purpose:** Front Line Demonstration details.

#### EeuOFT
**Location:** `Domain/Entities/EEU/EeuOFT.cs`

**Purpose:** On Farm Trial details.

#### EeuTrainingProgramme
**Location:** `Domain/Entities/EEU/EeuTrainingProgramme.cs`
**Base:** `ReportEntryBaseEntity`

**Purpose:** Tracks EEU training programmes.

**Properties:**
- All properties from `ReportEntryBaseEntity`
- `TrainingTitle` (string) - Training title
- `Duration` (TimeSpan) - Duration
- `TrainingCount` (int) - Number of trainings
- `ParticipantCount` (int) - Number of participants

#### Additional EEU Entities
- `EeuFieldDay` - Field day activities
- `EeuFieldVisit` - Field visit tracking
- `EeuFarmerScientistInteraction` - Farmer-scientist interactions
- `EeuOtherActivity` - Other extension activities

---

## 10. KVK - Krishi Vigyan Kendras

**Unit ID:** 10
**Namespace:** `Domain.Entities.KVK`

### 10.1 Main Entities

#### KvkProgramDetails
**Location:** `Domain/Entities/KVK/KvkProgramDetails.cs`
**Base:** `ReportEntryBaseEntity`

**Purpose:** Core entity for KVK programs and activities.

**Properties:** All properties from `FtiProgramDetails` plus additional KVK-specific fields (same as EEU):
- `T01` to `T05` (string?) - Technology fields
- `StageOfCrop` (string?) - Crop stage
- `NoOfDemos` (int?) - Number of demonstrations
- `NoOfTrails` (int?) - Number of trials
- `NoOfChecks` (int?) - Number of checks
- `NoOfVisits` (int?) - Number of visits
- `ParticipatedAsId` (int?) - Participation role
- `ParticipantFileUpload` (string?) - Participant file

**Navigation Properties:**
- `ParticipantDemographics` (ICollection<KvkParticipantDemographics>)
- `ProgramContent` (ICollection<KvkProgramContentAndResources>)
- `AdvisoryServices` (KvkAdvisoryServices)
- `Recommendations` (KvkRecommendation)
- `Results` (KvkResult) - **Unique to EEU/KVK**
- `Reports` (KvkReport)

#### KvkReport
**Location:** `Domain/Entities/KVK/KvkReport.cs`
**Base:** `AuditableBaseEntity`

**Purpose:** Tracks progress reports for KVK programs.

**Properties:** Similar to `FtiReport` with some commented-out fields:
- All properties from `AuditableBaseEntity`
- `KvkProgramDetailsId` (int?)
- `ReportDate` (DateOnly?)
- `ProgressReport` (string?)
- `GeoTaggedPhoto` (string?)
- `Outcome` (string?)
- `TestingCompletionDate` (DateOnly?)
- `TestingCompletionLetter` (string?)
- `ProjectCompletionDate` (DateOnly?)
- `ProjectCompletionLetter` (string?)
- `TypeOfReport` (string?)
- `SpclReport` (string?)
- `UnitLocationId` (int?)
- `OrganizationId` (int?)

**Note:** `ReportingYear` and `ReportingVideo` are commented out.

### 10.2 Child Entities

All child entities follow the same pattern:
- `KvkParticipantDemographics`
- `KvkProgramContentAndResources`
- `KvkAdvisoryServices`
- `KvkRecommendation`
- `KvkResourcePerson`
- `KvkTopicsCoveredInClass`
- `KvkTeachingAidsDeveloped`

### 10.3 Unique Entities

#### KvkResult
**Location:** `Domain/Entities/KVK/KvkResult.cs`
**Base:** `AuditableBaseEntity`

**Purpose:** Container for FLD and OFT results with Excel upload.

**Properties:**
- All properties from `AuditableBaseEntity`
- `KvkProgramDetailsId` (int)
- `UploadExcelUrl` (string?) - Excel file upload
- `UnitLocationId` (int?)
- `OrganizationId` (int?)

**Navigation Properties:**
- `ProgramDetails` (KvkProgramDetails)
- `FldResults` (ICollection<KvkFldResult>)
- `OftResults` (ICollection<KvkOftResult>)

#### KvkFldResult
**Location:** `Domain/Entities/KVK/KvkFldResult.cs`

**Purpose:** Tracks Front Line Demonstration results.

#### KvkOftResult
**Location:** `Domain/Entities/KVK/KvkOftResult.cs`

**Purpose:** Tracks On Farm Trial results.

#### Additional KVK Entities
- `KvkFieldDay` - Field day activities
- `KvkFieldVisit` - Field visit tracking
- `KvkFarmerScientistInteraction` - Farmer-scientist interactions
- `TableKVKProgramDetails` - Legacy table (if applicable)

---

## 11. SAMETI - State Agricultural Management and Extension Training Institutes

**Unit ID:** 11
**Namespace:** `Domain.Entities.SAMETI`

**Note:** SAMETI is the newest unit added to the system (added December 2024).

### 11.1 Main Entities

#### SametiProgramDetails
**Location:** `Domain/Entities/SAMETI/SAMETIProgramDetails.cs`
**Base:** `ReportEntryBaseEntity`

**Purpose:** Core entity for SAMETI training and management programs.

**Properties:** Same structure as `FtiProgramDetails` with SAMETI-specific navigation properties.

**Navigation Properties:**
- `ParticipantDemographics` (ICollection<SametiParticipantDemographics>)
- `ProgramContent` (ICollection<SametiProgramContentAndResources>)
- `AdvisoryServices` (SametiAdvisoryServices)
- `Recommendations` (SametiRecommendation)
- `Reports` (SametiReport)

#### SametiReport
**Location:** `Domain/Entities/SAMETI/SAMETIReport.cs`
**Base:** `AuditableBaseEntity`

**Purpose:** Tracks progress reports for SAMETI programs.

**Properties:** Same structure as `FtiReport`.

### 11.2 Child Entities

All child entities follow the same pattern:
- `SametiParticipantDemographics`
- `SametiProgramContentAndResources`
- `SametiAdvisoryServices`
- `SametiRecommendation`
- `SametiResourcePerson`
- `SametiTopicsCoveredInClass`
- `SametiTeachingAidsDeveloped`

---

## Common Patterns Across Units

### Standard Program Flow

Most units (FTI, STU, IBTVA, ATIC, DEU, NAEP, EEU, KVK, SAMETI) follow this pattern:

1. **ProgramDetails** (Main entity)
   - Contains all program/project information
   - Links to all child entities

2. **ParticipantDemographics** (Collection)
   - Tracks participant information
   - Demographic breakdown by gender and category

3. **ProgramContentAndResources** (Collection)
   - Container for:
     - ResourcePersons
     - TopicsCoveredInClass
     - TeachingAidsDeveloped

4. **AdvisoryServices** (Single)
   - Tracks advisory service metrics
   - Social media, SMS, calls, etc.

5. **Recommendations** (Single)
   - Program recommendations

6. **Report** (Single)
   - Progress reporting
   - Outcomes and completion status

### Units with Unique Structures

#### FIU (Farm Information Unit)
- Activity-based model
- Focus on media coordination
- Uses `FIUActivity` master data and `FIUProgramActivity` transaction records

#### ASM (Agricultural Sciences Museum)
- Visitor-focused model
- Simpler structure
- Tracks visitor counts by category

#### EEU & KVK
- Include `Results` entity
- Track FLD (Front Line Demonstration) and OFT (On Farm Trial) results
- Additional fields for technology tracking

### Entity Relationships

```
ProgramDetails (1)
├── ParticipantDemographics (Many)
├── ProgramContentAndResources (Many)
│   ├── ResourcePersons (Many)
│   ├── TopicsCoveredInClass (Many)
│   └── TeachingAidsDeveloped (Many)
├── AdvisoryServices (1)
├── Recommendations (1)
├── Results (1) [Only EEU & KVK]
│   ├── FldResults (Many)
│   └── OftResults (Many)
└── Reports (1)
```

---

## Master Data References

Most unit entities reference these common master data tables:

- `ProgramType` - Type of program
- `ProgramCategory` - Program category
- `InfoType` - Information type
- `Theme` - Program theme
- `ThematicArea` - Thematic area
- `Mode` - Mode of delivery
- `Region` - Geographic region
- `SourceOfFund` - Funding source
- `Status` - Program status
- `ParticipatedSource` - Source of participants
- `Participant` - Participant role/type
- `ParticipantDealer` - Participant dealer category

---

## Status Tracking

All units implement consistent status tracking:

- **FormStatus** values:
  - `Draft` - Initial state
  - `Pending` - Submitted for approval
  - `Approved` - Approved by authority
  - `Rejected` - Rejected

- **Approval Fields**:
  - `ApprovedAt` - Timestamp
  - `ApprovedById` - Approver user ID
  - `ApprovedBy` - Navigation to User

---

## File Upload Fields

Common file upload fields across units:

- `Attachements` - General attachments
- `GeoTaggedPhoto` - Geo-tagged photos
- `ReportingVideo` - Video files
- `ProposalUploadFile` - Proposal documents
- `UniversitySanctionLetterUploadFile` - Sanction letters
- `ProjectSanctionFile` - Project sanction documents
- `UniImplLetterFile` - Implementation letters
- `FundReleaseFile` - Fund release documents
- `FundsSanctionLetterUploadFile` - Fund sanction letters
- `OrganizerBroucherFile` - Organizer brochures
- `TestingCompletionLetter` - Testing completion documents
- `ProjectCompletionLetter` - Completion certificates
- `UploadMediaUrl` - Media files (FIU)
- `UploadExcelUrl` - Excel files (KVK Results)

---

## Notes for Dynamic Report Generation

When building dynamic reports for these units:

1. **Common Fields**: All units share base fields from `ReportEntryBaseEntity` and `AuditableBaseEntity`

2. **Unit-Specific Fields**: Each unit has unique child entities and navigation properties

3. **Participant Data**: Most units track demographics in a standardized format (SC/ST/OBC/GEN categories)

4. **Advisory Metrics**: Standard advisory service tracking across training-focused units

5. **Results Tracking**: Only EEU and KVK have FLD/OFT result tracking

6. **Special Cases**:
   - FIU uses activity-based tracking
   - ASM uses visitor-based tracking
   - ATIC includes sales tracking

7. **Master Data**: All units reference common master data tables for dropdowns and lookups

8. **File Management**: Standardized file upload fields across all units

---

## Database Table Naming Conventions

- Main entities typically use unit prefix (e.g., `FtiProgramDetails`, `KvkReport`)
- Child entities follow same prefix pattern
- All inherit from base entity classes for consistency
- Foreign keys use standard naming: `{Entity}Id`

---

## Entity Locations

All entity models are located in:
- **Main Location**: `/Domain/Entities/{UnitName}/`
- **Application Copy**: `/Application/Domain/Entities/{UnitName}/`

Base entities are in:
- `/Domain/Entities/AuditableBaseEntity.cs`
- `/Domain/Entities/ReportEntryBaseEntity.cs`

---

## Version Information

- **Document Version**: 1.0
- **Last Updated**: 2025-12-16
- **Latest Unit Added**: SAMETI (Unit ID: 11) - Added December 2024
- **Total Units**: 11

---

## Related Documentation

- See `CLAUDE.md` for development notes and bug fixes
- See `UnitConstants.cs` for unit ID constants
- See migration files in `/Infracture/Migrations/` for schema history

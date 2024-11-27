# Models

## Raw Data

Below is a reference list of tables, showing the expected structure and data types for each input file used in the data pipeline. This table outlines the raw data format that the pipeline consumes, demonstrating the required columns and data types for each file to ensure consistent data ingestion.

### gias

| Column Name                | Data Type |
|----------------------------|-----------|
| URN                        | Int64     |
| UKPRN                      | Int64     |
| LA (code)                  | Int64     |
| LA (name)                  | string    |
| EstablishmentNumber        | Int64     |
| EstablishmentName          | string    |
| TypeOfEstablishment (code) | Int64     |
| TypeOfEstablishment (name) | string    |
| OpenDate                   | string    |
| CloseDate                  | string    |
| PhaseOfEducation (code)    | Int64     |
| PhaseOfEducation (name)    | string    |
| Boarders (code)            | Int64     |
| Boarders (name)            | string    |
| NurseryProvision (name)    | string    |
| OfficialSixthForm (code)   | Int64     |
| OfficialSixthForm (name)   | string    |
| AdmissionsPolicy (code)    | Int64     |
| AdmissionsPolicy (name)    | string    |
| OfstedLastInsp             | string    |
| Postcode                   | string    |
| SchoolWebsite              | string    |
| TelephoneNum               | string    |
| GOR (name)                 | string    |
| OfstedRating (name)        | string    |
| MSOA (code)                | string    |
| LSOA (code)                | string    |
| StatutoryLowAge            | Int64     |
| StatutoryHighAge           | Int64     |
| Street                     | string    |
| Locality                   | string    |
| Address3                   | string    |
| Town                       | string    |
| County (name)              | string    |

### gias_links

| Column Name         | Data Type |
|---------------------|-----------|
| URN                 | Int64     |
| LinkURN             | Int64     |
| LinkName            | string    |
| LinkType            | string    |
| LinkEstablishedDate | string    |

### maintained_schools_master_list

| Column Name                                                     | Data Type |
|-----------------------------------------------------------------|-----------|
| URN                                                             | Int64     |
| School Name                                                     | string    |
| LAEstab                                                         | string    |
| Phase                                                           | string    |
| Overall Phase                                                   | string    |
| Type                                                            | string    |
| Period covered by return (months)                               | Int64     |
| Did Not Supply flag                                             | string    |
| Lead school in federation                                       | string    |
| London Weighting                                                | string    |
| PFI                                                             | string    |
| I01  Funds delegated by the LA                                  | float     |
| I02  Funding for 6th form students                              | float     |
| I06  Other government grants                                    | float     |
| I07  Other grants and payments                                  | float     |
| I08  Income from facilities and services                        | float     |
| I09  Income from catering                                       | float     |
| I10  Receipts from supply teacher insurance claims              | float     |
| I13  Donations and or private funds                             | float     |
| Total Income   I01 to I18                                       | float     |
| CI04 Direct revenue financing                                   | float     |
| E01  Teaching Staff                                             | float     |
| E02  Supply teaching staff                                      | float     |
| E03 Education support staff                                     | float     |
| E04  Premises staff                                             | float     |
| E05 Administrative and clerical staff                           | float     |
| E06 Catering staff                                              | float     |
| E07  Cost of other staff                                        | float     |
| E08  Indirect employee expenses                                 | float     |
| E09  Development and training                                   | float     |
| E10  Supply teacher insurance                                   | float     |
| E11  Staff related insurance                                    | float     |
| E12  Building maintenance and improvement                       | float     |
| E13  Grounds maintenance and improvement                        | float     |
| E14  Cleaning and caretaking                                    | float     |
| E15  Water and sewerage                                         | float     |
| E16  Energy                                                     | float     |
| E17  Rates                                                      | float     |
| E18  Other occupation costs                                     | float     |
| E19  Learning resources (not ICT equipment)                     | float     |
| E20  ICT learning resources                                     | float     |
| E21  Exam fees                                                  | float     |
| E22 Administrative supplies                                     | float     |
| E23  Other insurance premiums                                   | float     |
| E24  Special facilities                                         | float     |
| E25  Catering supplies                                          | float     |
| E26 Agency supply teaching staff                                | float     |
| E27  Bought in professional services - curriculum               | float     |
| E28a  Bought in professional services - other (except PFI)      | float     |
| E28b Bought in professional services - other (PFI)              | float     |
| E29  Loan interest                                              | float     |
| E30 Direct revenue financing (revenue contributions to capital) | float     |
| E31  Community focused school staff                             | float     |
| E32 Community focused school costs                              | float     |
| Total Expenditure  E01 to E32                                   | float     |
| Revenue Reserve   B01 plus B02 plus B06                         | float     |
| Direct Grant                                                    | float     |
| Targeted Grants                                                 | float     |
| Community Grants                                                | float     |
| Self Generated Funding                                          | float     |

### cdc

| Column Name | Data Type |
|-------------|-----------|
| URN         | Int64     |
| GIFA        | float     |
| Block Age   | string    |

### sen

| Column Name           | Data Type |
|-----------------------|-----------|
| URN                   | Int64     |
| Total pupils          | float     |
| EHC plan              | float     |
| SEN support           | float     |
| EHC_Primary_need_spld | float     |
| SUP_Primary_need_spld | float     |
| EHC_Primary_need_mld  | float     |
| SUP_Primary_need_mld  | float     |
| EHC_Primary_need_sld  | float     |
| SUP_Primary_need_sld  | float     |
| EHC_Primary_need_pmld | float     |
| SUP_Primary_need_pmld | float     |
| EHC_Primary_need_semh | float     |
| SUP_Primary_need_semh | float     |
| EHC_Primary_need_slcn | float     |
| SUP_Primary_need_slcn | float     |
| EHC_Primary_need_hi   | float     |
| SUP_Primary_need_hi   | float     |
| EHC_Primary_need_vi   | float     |
| SUP_Primary_need_vi   | float     |
| EHC_Primary_need_msi  | float     |
| SUP_Primary_need_msi  | float     |
| EHC_Primary_need_pd   | float     |
| SUP_Primary_need_pd   | float     |
| EHC_Primary_need_asd  | float     |
| SUP_Primary_need_asd  | float     |
| EHC_Primary_need_oth  | float     |
| SUP_Primary_need_oth  | float     |

### census_workforce

| Column Name                                                                                                                                                                           | Data Type |
|---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|-----------|
| URN                                                                                                                                                                                   | Int64     |
| Total Number of Non-Classroom-based School Support Staff, (Other school support staff plus Administrative staff plus Technicians and excluding Auxiliary staff (Full-Time Equivalent) | float     |
| Total Number of Non Classroom-based School Support Staff, Excluding Auxiliary Staff (Headcount)                                                                                       | float     |
| Teachers with Qualified Teacher Status (%) (Headcount)                                                                                                                                | float     |
| Total Number of Teaching Assistants (Full-Time Equivalent)                                                                                                                            | float     |
| Total Number of Teaching Assistants (Headcount)                                                                                                                                       | float     |
| Total School Workforce (Full-Time Equivalent)                                                                                                                                         | float     |
| Total Number of Teachers (Full-Time Equivalent)                                                                                                                                       | float     |
| Total Number of Teachers (Headcount)                                                                                                                                                  | float     |
| Total Number of Teachers in the Leadership Group (Headcount)                                                                                                                          | float     |
| Total Number of Teachers in the Leadership Group (Full-time Equivalent)                                                                                                               | float     |
| Total Number of Auxiliary Staff (Full-Time Equivalent)                                                                                                                                | float     |
| Total Number of Auxiliary Staff (Headcount)                                                                                                                                           | float     |
| Total School Workforce (Headcount)                                                                                                                                                    | float     |

### census_pupils

| Column Name                                            | Data Type |
|--------------------------------------------------------|-----------|
| URN                                                    | Int64     |
| % of pupils known to be eligible for free school meals | float     |
| headcount of pupils                                    | float     |
| fte pupils                                             | float     |
| ward_name                                              | string    |
| Number of early year pupils (years E1 and E2)          | float     |
| Number of nursery pupils (years N1 and N2)             | float     |
| Full time boys Year group 12                           | float     |
| Full time girls Year group 12                          | float     |
| Full time boys Year group 13                           | float     |
| Full time girls Year group 13                          | float     |
| number_of_dual_subsidiary_registrations                | float     |

### cfr

| Column Name | Data Type |
|-------------|-----------|
| URN         | Int64     |

### ks2

| Column Name | Data Type |
|-------------|-----------|
| URN         | Int64     |
| READPROG    | string    |
| WRITPROG    | string    |
| MATPROG     | string    |

### ks4

| Column Name | Data Type |
|-------------|-----------|
| URN         | Int64     |
| ATT8SCR     | float     |
| P8MEA       | float     |
| P8_BANDING  | string    |

### aar

| Column Name                                                             | Data Type |
|-------------------------------------------------------------------------|-----------|
| LA                                                                      | Int64     |
| Estab                                                                   | Int64     |
| URN                                                                     | Int64     |
| ACADEMYUPIN                                                             | Int64     |
| ACADEMYTRUSTSTATUS                                                      | string    |
| Company_Number                                                          | string    |
| Date joined or opened if in period:                                     | string    |
| Date left or closed if in period:                                       | string    |
| BNCH11110T (EFA Revenue Grants)                                         | float     |
| BNCH11131 (DfE Family Revenue Grants)                                   | float     |
| BNCH11141 (SEN)                                                         | float     |
| BNCH11142 (Other Revenue)                                               | float     |
| BNCH11151 (Other Government Revenue Grants)                             | float     |
| BNCH11161 (Government source (non-grant))                               | float     |
| BNCH11162 (Academies)                                                   | float     |
| BNCH11163 (Non- Government)                                             | float     |
| BNCH11123-BAI011-A (Academies - Income)                                 | float     |
| BNCH11201 (Income from facilities and services)                         | float     |
| BNCH11202 (Income from catering)                                        | float     |
| BNCH11203 (Receipts from supply teacher insurance claims)               | float     |
| BNCH11300T (Voluntary income)                                           | float     |
| BNCH11204 (Other income - revenue)                                      | float     |
| BNCH11205 (Other Income from facilities and services)                   | float     |
| BNCH11400T (Investment income)                                          | float     |
| BNCH21706 (Administrative supplies - non educational)                   | float     |
| BNCH21106 (Catering staff)                                              | float     |
| BNCH21701 (Catering supplies)                                           | float     |
| BNCH21707 (Direct revenue financing (Revenue contributions to capital)) | float     |
| BNCH21602 (ICT learning resources)                                      | float     |
| BNCH21603 (Examination fees)                                            | float     |
| BNCH21601 (Learning resources (not ICT equipment))                      | float     |
| BNCH21104 (Administrative and clerical staff)                           | float     |
| BNCH21107 (Other staff)                                                 | float     |
| BNCH44001 (Closing Balance (Restricted and Unrestricted Funds))         | float     |
| BNCH21702 (Professional Services - non-curriculum)                      | float     |
| BNCH21703 (Auditor costs)                                               | float     |
| BNCH21301 (Maintenance of premises)                                     | float     |
| BNCH21405 (Grounds maintenance)                                         | float     |
| BNCH21201 (Indirect employee expenses)                                  | float     |
| BNCH21801 (Interest charges for Loan and bank)                          | float     |
| BNCH21705 (Other insurance premiums)                                    | float     |
| BNCH21802 (PFI Charges)                                                 | float     |
| BNCH21404 (Rent and rates)                                              | float     |
| BNCH21501 (Special facilities)                                          | float     |
| BNCH21202 (Staff development and training)                              | float     |
| BNCH21203 (Staff-related insurance)                                     | float     |
| BNCH21204 (Supply teacher insurance)                                    | float     |
| BNCH21401 (Cleaning and caretaking)                                     | float     |
| BNCH21406 (Other occupation costs)                                      | float     |
| BNCH21105 (Premises staff)                                              | float     |
| BNCH21101 (Teaching staff)                                              | float     |
| BNCH21102 (Supply teaching staff - extra note in guidance)              | float     |
| BNCH21103 (Education support staff)                                     | float     |
| BNCH21604 (Educational Consultancy)                                     | float     |
| BNCH21606 (Agency supply teaching staff)                                | float     |
| BNCH21403 (Energy)                                                      | float     |
| BNCH21402 (Water and sewerage)                                          | float     |
| Valid To                                                                | string    |

### aar_cs

| Column Name                                                             | Data Type |
|-------------------------------------------------------------------------|-----------|
| Lead_UPIN                                                               | Int64     |
| Company_Number                                                          | string    |
| Company_Name                                                            | string    |
| BNCH11110T (EFA Revenue Grants)                                         | float     |
| BNCH11131 (DfE Family Revenue Grants)                                   | float     |
| BNCH11141 (SEN)                                                         | float     |
| BNCH11142 (Other Revenue)                                               | float     |
| BNCH11151 (Other Government Revenue Grants)                             | float     |
| BNCH11161 (Government source (non-grant))                               | float     |
| BNCH11162 (Academies)                                                   | float     |
| BNCH11163 (Non- Government)                                             | float     |
| BNCH11123-BTI011-A (MAT Central services - Income)                      | float     |
| BNCH11201 (Income from facilities and services)                         | float     |
| BNCH11202 (Income from catering)                                        | float     |
| BNCH11203 (Receipts from supply teacher insurance claims)               | float     |
| BNCH11300T (Voluntary income)                                           | float     |
| BNCH11204 (Other income - revenue)                                      | float     |
| BNCH11205 (Other Income from facilities and services)                   | float     |
| BNCH11400T (Investment income)                                          | float     |
| BNCH21706 (Administrative supplies - non educational)                   | float     |
| BNCH21106 (Catering staff)                                              | float     |
| BNCH21701 (Catering supplies)                                           | float     |
| BNCH21707 (Direct revenue financing (Revenue contributions to capital)) | float     |
| BNCH21602 (ICT learning resources)                                      | float     |
| BNCH21603 (Examination fees)                                            | float     |
| BNCH21601 (Learning resources (not ICT equipment))                      | float     |
| BNCH21104 (Administrative and clerical staff)                           | float     |
| BNCH21703 (Auditor costs)                                               | float     |
| BNCH21107 (Other staff)                                                 | float     |
| BNCH44001CS (Closing Balance (Restricted and Unrestricted Funds))       | float     |
| BNCH21702 (Professional Services - non-curriculum)                      | float     |
| BNCH21301 (Maintenance of premises)                                     | float     |
| BNCH21405 (Grounds maintenance)                                         | float     |
| BNCH21201 (Indirect employee expenses)                                  | float     |
| BNCH21801 (Interest charges for Loan and bank)                          | float     |
| BNCH21705 (Other insurance premiums)                                    | float     |
| BNCH21802 (PFI Charges)                                                 | float     |
| BNCH21404 (Rent and rates)                                              | float     |
| BNCH21501 (Special facilities)                                          | float     |
| BNCH21202 (Staff development and training)                              | float     |
| BNCH21203 (Staff-related insurance)                                     | float     |
| BNCH21204 (Supply teacher insurance)                                    | float     |
| BNCH21401 (Cleaning and caretaking)                                     | float     |
| BNCH21406 (Other occupation costs)                                      | float     |
| BNCH21105 (Premises staff)                                              | float     |
| BNCH21101 (Teaching staff)                                              | float     |
| BNCH21102 (Supply teaching staff - extra note in guidance)              | float     |
| BNCH21103 (Education support staff)                                     | float     |
| BNCH21604 (Educational Consultancy)                                     | float     |
| BNCH21606 (Agency supply teaching staff)                                | float     |
| BNCH21403 (Energy)                                                      | float     |
| BNCH21402 (Water and sewerage)                                          | float     |

### BFR_SOFA

| Column Name | Data Type |
|-------------|-----------|
| TrustUPIN   | Int64     |
| Title       | string    |
| EFALineNo   | Int64     |
| Y1P1        | float     |
| Y1P2        | float     |
| Y2P1        | float     |
| Y2P2        | float     |

### BFR_3Y

| Column Name | Data Type |
|-------------|-----------|
| TrustUPIN   | Int64     |
| EFALineNo   | Int64     |
| Y2          | float     |
| Y3          | float     |
| Y4          | float     |

## SQL

The following Entity-Relationship Diagram (ERD) represents the core data structures and relationships within the system, specifically detailing the tables, fields, and key associations among them.

```mermaid
erDiagram

    BudgetForecastReturn {
        nvarchar RunType
        nvarchar RunId
        int Year
        nvarchar CompanyNumber
        nvarchar Category
        decimal Value
        decimal TotalPupils
    }

    BudgetForecastReturnMetric {
        nvarchar RunType
        nvarchar RunId
        int Year
        nvarchar CompanyNumber
        nvarchar Metric
        decimal Value
    }

    ComparatorSet {
        nvarchar RunType
        nvarchar RunId
        nvarchar URN
        nvarchar Pupil
        nvarchar Building
    }

    CompletedPipelineRun {
        int Id
        nvarchar OrchestrationId
        datetimeoffset CompletedAt
        nvarchar Message
    }

    Financial {
        nvarchar RunType
        nvarchar RunId
        nvarchar URN
        smallint PeriodCoveredByReturn
        nvarchar FinancialPosition
        nvarchar TrustPosition
        nvarchar EstablishmentType
        decimal TotalPupils
        decimal TotalInternalFloorArea
        decimal TotalIncome
        decimal TotalExpenditure
        decimal InYearBalance
        decimal RevenueReserve
        decimal TotalGrantFunding
        decimal TotalSelfGeneratedFunding
        decimal DirectRevenueFinancing
        decimal DirectGrants
        decimal PrePost16Funding
        decimal OtherDfeGrants
        decimal OtherIncomeGrants
        decimal GovernmentSource
        decimal CommunityGrants
        decimal Academies
        decimal IncomeFacilitiesServices
        decimal IncomeCateringServices
        decimal DonationsVoluntaryFunds
        decimal ReceiptsSupplyTeacherInsuranceClaims
        decimal InvestmentIncome
        decimal OtherSelfGeneratedIncome
        decimal TotalTeachingSupportStaffCosts
        decimal TeachingStaffCosts
        decimal SupplyTeachingStaffCosts
        decimal EducationalConsultancyCosts
        decimal EducationSupportStaffCosts
        decimal AgencySupplyTeachingStaffCosts
        decimal TotalNonEducationalSupportStaffCosts
        decimal AdministrativeClericalStaffCosts
        decimal AuditorsCosts
        decimal OtherStaffCosts
        decimal ProfessionalServicesNonCurriculumCosts
        decimal TotalEducationalSuppliesCosts
        decimal ExaminationFeesCosts
        decimal LearningResourcesNonIctCosts
        decimal LearningResourcesIctCosts
        decimal TotalPremisesStaffServiceCosts
        decimal CleaningCaretakingCosts
        decimal MaintenancePremisesCosts
        decimal OtherOccupationCosts
        decimal PremisesStaffCosts
        decimal TotalUtilitiesCosts
        decimal EnergyCosts
        decimal WaterSewerageCosts
        decimal AdministrativeSuppliesNonEducationalCosts
        decimal TotalGrossCateringCosts
        decimal TotalNetCateringCostsCosts
        decimal CateringStaffCosts
        decimal CateringSuppliesCosts
        decimal TotalOtherCosts
        decimal DirectRevenueFinancingCosts
        decimal GroundsMaintenanceCosts
        decimal IndirectEmployeeExpenses
        decimal InterestChargesLoanBank
        decimal OtherInsurancePremiumsCosts
        decimal PrivateFinanceInitiativeCharges
        decimal RentRatesCosts
        decimal SpecialFacilitiesCosts
        decimal StaffDevelopmentTrainingCosts
        decimal StaffRelatedInsuranceCosts
        decimal SupplyTeacherInsurableCosts
        decimal CommunityFocusedSchoolStaff
        decimal CommunityFocusedSchoolCosts
        decimal TotalIncomeCS
        decimal TotalExpenditureCS
        decimal InYearBalanceCS
        decimal TotalGrantFundingCS
        decimal TotalSelfGeneratedFundingCS
        decimal DirectRevenueFinancingCS
        decimal DirectGrantsCS
        decimal PrePost16FundingCS
        decimal OtherDfeGrantsCS
        decimal OtherIncomeGrantsCS
        decimal GovernmentSourceCS
        decimal CommunityGrantsCS
        decimal AcademiesCS
        decimal IncomeFacilitiesServicesCS
        decimal IncomeCateringServicesCS
        decimal DonationsVoluntaryFundsCS
        decimal ReceiptsSupplyTeacherInsuranceClaimsCS
        decimal InvestmentIncomeCS
        decimal OtherSelfGeneratedIncomeCS
        decimal TotalTeachingSupportStaffCostsCS
        decimal TeachingStaffCostsCS
        decimal SupplyTeachingStaffCostsCS
        decimal EducationalConsultancyCostsCS
        decimal EducationSupportStaffCostsCS
        decimal AgencySupplyTeachingStaffCostsCS
        decimal TotalNonEducationalSupportStaffCostsCS
        decimal AdministrativeClericalStaffCostsCS
        decimal AuditorsCostsCS
        decimal OtherStaffCostsCS
        decimal ProfessionalServicesNonCurriculumCostsCS
        decimal TotalEducationalSuppliesCostsCS
        decimal ExaminationFeesCostsCS
        decimal LearningResourcesNonIctCostsCS
        decimal LearningResourcesIctCostsCS
        decimal TotalPremisesStaffServiceCostsCS
        decimal CleaningCaretakingCostsCS
        decimal MaintenancePremisesCostsCS
        decimal OtherOccupationCostsCS
        decimal PremisesStaffCostsCS
        decimal TotalUtilitiesCostsCS
        decimal EnergyCostsCS
        decimal WaterSewerageCostsCS
        decimal AdministrativeSuppliesNonEducationalCostsCS
        decimal TotalGrossCateringCostsCS
        decimal TotalNetCateringCostsCostsCS
        decimal CateringStaffCostsCS
        decimal CateringSuppliesCostsCS
        decimal TotalOtherCostsCS
        decimal DirectRevenueFinancingCostsCS
        decimal GroundsMaintenanceCostsCS
        decimal IndirectEmployeeExpensesCS
        decimal InterestChargesLoanBankCS
        decimal OtherInsurancePremiumsCostsCS
        decimal PrivateFinanceInitiativeChargesCS
        decimal RentRatesCostsCS
        decimal SpecialFacilitiesCostsCS
        decimal StaffDevelopmentTrainingCostsCS
        decimal StaffRelatedInsuranceCostsCS
        decimal SupplyTeacherInsurableCostsCS
        decimal TargetedGrants
    }

    FinancialPlan {
        nvarchar URN
        smallint Year
        nvarchar Input
        nvarchar DeploymentPlan
        datetimeoffset Created
        nvarchar CreatedBy
        datetimeoffset UpdatedAt
        nvarchar UpdatedBy
        bit IsComplete
        int Version
        decimal TeacherContactRatio
        nvarchar ContactRatioRating
        decimal InYearBalance
        nvarchar InYearBalancePercentIncomeRating
        decimal AverageClassSize
        nvarchar AverageClassSizeRating
    }

    LocalAuthority {
        nvarchar Code
        nvarchar Name
    }

    MetricRAG {
        nvarchar RunType
        nvarchar RunId
        nvarchar URN
        nvarchar Category
        nvarchar SubCategory
        decimal Value
        decimal Median
        decimal DiffMedian
        decimal PercentDiff
        decimal Percentile
        decimal Decile
        nvarchar RAG
    }

    NonFinancial {
        nvarchar RunType
        nvarchar RunId
        nvarchar URN
        nvarchar EstablishmentType
        decimal TotalInternalFloorArea
        decimal BuildingAverageAge
        decimal TotalPupils
        decimal TotalPupilsSixthForm
        decimal TotalPupilsNursery
        decimal WorkforceHeadcount
        decimal WorkforceFTE
        decimal TeachersHeadcount
        decimal TeachersFTE
        decimal SeniorLeadershipHeadcount
        decimal SeniorLeadershipFTE
        decimal TeachingAssistantHeadcount
        decimal TeachingAssistantFTE
        decimal NonClassroomSupportStaffHeadcount
        decimal NonClassroomSupportStaffFTE
        decimal AuxiliaryStaffHeadcount
        decimal AuxiliaryStaffFTE
        decimal PercentTeacherWithQualifiedStatus
        decimal PercentFreeSchoolMeals
        decimal PercentSpecialEducationNeeds
        decimal PercentWithEducationalHealthCarePlan
        decimal PercentWithoutEducationalHealthCarePlan
        decimal KS2Progress
        decimal KS4Progress
        decimal PredictedPercentChangePupils3To5Years
        decimal PercentWithVI
        decimal PercentWithSPLD
        decimal PercentWithSLD
        decimal PercentWithSLCN
        decimal PercentWithSEMH
        decimal PercentWithPMLD
        decimal PercentWithPD
        decimal PercentWithOTH
        decimal PercentWithMSI
        decimal PercentWithMLD
        decimal PercentWithHI
        decimal PercentWithASD
    }

    Parameters {
        nvarchar Name
        nvarchar Value
    }

    School {
        nvarchar URN
        nvarchar SchoolName
        nvarchar TrustCompanyNumber
        nvarchar TrustName
        nvarchar FederationLeadURN
        nvarchar FederationLeadName
        nvarchar LACode
        nvarchar LAName
        nvarchar LondonWeighting
        nvarchar FinanceType
        nvarchar OverallPhase
        nvarchar SchoolType
        bit HasSixthForm
        bit HasNursery
        bit IsPFISchool
        date OfstedDate
        nvarchar OfstedDescription
        nvarchar Telephone
        nvarchar Website
        nvarchar AddressStreet
        nvarchar AddressLocality
        nvarchar AddressLine3
        nvarchar AddressTown
        nvarchar AddressCounty
        nvarchar AddressPostcode
    }

    Trust {
        nvarchar CompanyNumber
        nvarchar TrustName
        nvarchar CFOName
        nvarchar CFOEmail
        date OpenDate
        nvarchar UID
        nvarchar TrustUPIN
    }

    TrustFinancial {
        nvarchar CompanyNumber
        nvarchar RunType
        nvarchar RunId
        nvarchar TrustPosition
        decimal TotalPupils
        decimal TotalInternalFloorArea
        decimal TotalIncome
        decimal TotalExpenditure
        decimal InYearBalance
        decimal RevenueReserve
        decimal TotalGrantFunding
        decimal TotalSelfGeneratedFunding
        decimal DirectGrants
        decimal PrePost16Funding
        decimal TargetedGrants
        decimal OtherDfeGrants
        decimal OtherIncomeGrants
        decimal GovernmentSource
        decimal CommunityGrants
        decimal Academies
        decimal IncomeFacilitiesServices
        decimal IncomeCateringServices
        decimal DonationsVoluntaryFunds
        decimal ReceiptsSupplyTeacherInsuranceClaims
        decimal InvestmentIncome
        decimal OtherSelfGeneratedIncome
        decimal TotalTeachingSupportStaffCosts
        decimal TeachingStaffCosts
        decimal SupplyTeachingStaffCosts
        decimal EducationalConsultancyCosts
        decimal EducationSupportStaffCosts
        decimal AgencySupplyTeachingStaffCosts
        decimal TotalNonEducationalSupportStaffCosts
        decimal AdministrativeClericalStaffCosts
        decimal AuditorsCosts
        decimal OtherStaffCosts
        decimal ProfessionalServicesNonCurriculumCosts
        decimal TotalEducationalSuppliesCosts
        decimal ExaminationFeesCosts
        decimal LearningResourcesNonIctCosts
        decimal LearningResourcesIctCosts
        decimal TotalPremisesStaffServiceCosts
        decimal CleaningCaretakingCosts
        decimal MaintenancePremisesCosts
        decimal OtherOccupationCosts
        decimal PremisesStaffCosts
        decimal TotalUtilitiesCosts
        decimal EnergyCosts
        decimal WaterSewerageCosts
        decimal AdministrativeSuppliesNonEducationalCosts
        decimal TotalGrossCateringCosts
        decimal TotalNetCateringCostsCosts
        decimal CateringStaffCosts
        decimal CateringSuppliesCosts
        decimal TotalOtherCosts
        decimal DirectRevenueFinancingCosts
        decimal GroundsMaintenanceCosts
        decimal IndirectEmployeeExpenses
        decimal InterestChargesLoanBank
        decimal OtherInsurancePremiumsCosts
        decimal PrivateFinanceInitiativeCharges
        decimal RentRatesCosts
        decimal SpecialFacilitiesCosts
        decimal StaffDevelopmentTrainingCosts
        decimal StaffRelatedInsuranceCosts
        decimal SupplyTeacherInsurableCosts
        decimal TotalIncomeCS
        decimal TotalExpenditureCS
        decimal InYearBalanceCS
        decimal TotalGrantFundingCS
        decimal TotalSelfGeneratedFundingCS
        decimal DirectRevenueFinancingCS
        decimal DirectGrantsCS
        decimal PrePost16FundingCS
        decimal OtherDfeGrantsCS
        decimal OtherIncomeGrantsCS
        decimal GovernmentSourceCS
        decimal CommunityGrantsCS
        decimal AcademiesCS
        decimal IncomeFacilitiesServicesCS
        decimal IncomeCateringServicesCS
        decimal DonationsVoluntaryFundsCS
        decimal ReceiptsSupplyTeacherInsuranceClaimsCS
        decimal InvestmentIncomeCS
        decimal OtherSelfGeneratedIncomeCS
        decimal TotalTeachingSupportStaffCostsCS
        decimal TeachingStaffCostsCS
        decimal SupplyTeachingStaffCostsCS
        decimal EducationalConsultancyCostsCS
        decimal EducationSupportStaffCostsCS
        decimal AgencySupplyTeachingStaffCostsCS
        decimal TotalNonEducationalSupportStaffCostsCS
        decimal AdministrativeClericalStaffCostsCS
        decimal AuditorsCostsCS
        decimal OtherStaffCostsCS
        decimal ProfessionalServicesNonCurriculumCostsCS
        decimal TotalEducationalSuppliesCostsCS
        decimal ExaminationFeesCostsCS
        decimal LearningResourcesNonIctCostsCS
        decimal LearningResourcesIctCostsCS
        decimal TotalPremisesStaffServiceCostsCS
        decimal CleaningCaretakingCostsCS
        decimal MaintenancePremisesCostsCS
        decimal OtherOccupationCostsCS
        decimal PremisesStaffCostsCS
        decimal TotalUtilitiesCostsCS
        decimal EnergyCostsCS
        decimal WaterSewerageCostsCS
        decimal AdministrativeSuppliesNonEducationalCostsCS
        decimal TotalGrossCateringCostsCS
        decimal TotalNetCateringCostsCostsCS
        decimal CateringStaffCostsCS
        decimal CateringSuppliesCostsCS
        decimal TotalOtherCostsCS
        decimal DirectRevenueFinancingCostsCS
        decimal GroundsMaintenanceCostsCS
        decimal IndirectEmployeeExpensesCS
        decimal InterestChargesLoanBankCS
        decimal OtherInsurancePremiumsCostsCS
        decimal PrivateFinanceInitiativeChargesCS
        decimal RentRatesCostsCS
        decimal SpecialFacilitiesCostsCS
        decimal StaffDevelopmentTrainingCostsCS
        decimal StaffRelatedInsuranceCostsCS
        decimal SupplyTeacherInsurableCostsCS
    }

    TrustHistory {
        int Id
        nvarchar CompanyNumber
        date EventDate
        nvarchar EventName
        smallint AcademicYear
        nvarchar SchoolURN
        nvarchar SchoolName
    }

    UserData {
        nvarchar Id
        nvarchar Type
        nvarchar UserId
        nvarchar OrganisationType
        nvarchar OrganisationId
        nvarchar Status
        datetimeoffset Expiry
    }

    UserDefinedSchoolComparatorSet {
        nvarchar RunType
        nvarchar RunId
        nvarchar URN
        nvarchar Set
    }

    UserDefinedTrustComparatorSet {
        nvarchar RunType
        nvarchar RunId
        nvarchar CompanyNumber
        nvarchar Set
    }

    CustomDataSchool {
        nvarchar Id
        nvarchar URN
        nvarchar Data
    }

    SchemaVersions {
        int Id
        nvarchar ScriptName
        datetime Applied
    }

    School |o..o{ FinancialPlan : ""
    School |o..o| UserDefinedSchoolComparatorSet : ""
    School |o..o{ ComparatorSet : ""
    School |o..o{ NonFinancial : ""
    School |o..o{ Financial : ""
    School }o..o| Trust : ""
    School }o..o| LocalAuthority : ""

    Trust |o..o{ BudgetForecastReturn : ""
    Trust |o..o{ BudgetForecastReturnMetric : ""
    Trust |o..o{ TrustHistory : ""
    Trust |o..o{ TrustFinancial : ""
    Trust |o..o{ UserDefinedTrustComparatorSet : ""    
```

## Element Transformative Flow

This section looks at each item listed in the SQL data model which is generated as part of the data pipeline. It traces each element back through their variable names within the pre-processing pipeline, to their source in the raw data file. In cases where the value is computed, or has undergone some logical process, we highlight the computation in the `notes on transformation` column. The UserDefinedSchoolComparatorSet, Parameters, SchemaVersions, CompletedPipelineRun, UserData, CustomDataSchool and FinancialPlan tables are not detailed in this section, as they are not generated by the data pipeline itself, but through another means.

It is also key to note that within the service, metrics such as "cost per pupil" are computed. However this has been ommited from this section to enable a more suscinct translation of the data schema. In cases where a "cost per pupil" or "cost per m2" unit is presented, this is simply the given metric divided by the `TotalPupils`, or the `GrossInternalFloorArea`, all of which can be found from the information below.

### Financial

| raw file name                  | raw column name                                            | pre-processing column name                                                                                                                                               | sql table column name                                                                   | notes on transformation                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    |
|--------------------------------|------------------------------------------------------------|--------------------------------------------------------------------------------------------------------------------------------------------------------------------------|-----------------------------------------------------------------------------------------|------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| N/A - defined in pipeline run  |                                                            |                                                                                                                                                                          | RunType                                                                                 |                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            |
| N/A - defined in pipeline run  |                                                            |                                                                                                                                                                          | RunId                                                                                   |                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            |
| gias                           | URN                                                        | URN                                                                                                                                                                      | URN                                                                                     |                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            |
| maintained_schools_master_list | Total Expenditure  E01 to E32                              | Total Expenditure                                                                                                                                                        | TotalExpenditure                                                                        | For Maintained Schools total expenditure E30 Direct revenue financing (revenue contributions to capital) is not included in this summation                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 |
| N/A - computed                 |                                                            | Total Expenditure / Total Expenditure_CS                                                                                                                                 | TotalExpenditure / TotalExpenditureCS                                                   | Academies and Central Services total expenditure is computed as BNCH21101 (Teaching staff) + BNCH21102 (Supply teaching staff - extra note in guidance) + BNCH21103 (Education support staff) + BNCH21104 (Administrative and clerical staff) + BNCH21105 (Premises staff) + BNCH21106 (Catering staff) + BNCH21107 (Other staff) + BNCH21201 (Indirect employee expenses) + BNCH21202 (Staff development and training) + BNCH21203 (Staff-related insurance) + BNCH21204 (Supply teacher insurance) + BNCH21301 (Maintenance of premises) + BNCH21405 (Grounds maintenance) + BNCH21401 (Cleaning and caretaking) + BNCH21402 (Water and sewerage) + BNCH21403 (Energy) + BNCH21404 (Rent and rates) + BNCH21406 (Other occupation costs) + BNCH21501 (Special facilities) + BNCH21601 (Learning resources (not ICT equipment)) + BNCH21602 (ICT learning resources) + BNCH21603 (Examination fees) + BNCH21604 (Educational Consultancy) + BNCH21706 (Administrative supplies - non educational) + BNCH21606 (Agency supply teaching staff) + BNCH21701 (Catering supplies) + BNCH21705 (Other insurance premiums) + BNCH21702 (Professional Services - non-curriculum) + BNCH21703 (Auditor costs) + BNCH21801 (Interest charges for Loan and bank) + BNCH21802 (PFI Charges) - BNCH21707 (Direct revenue financing (Revenue contributions to capital)) |
| N/A - computed                 |                                                            |                                                                                                                                                                          | TotalTeachingSupportStaffCosts                                                          | For Maintaiend Schools, Academies and Central Services, computed as the sum of Teaching and Teaching support, staff_Teaching staff, Teaching and Teaching support staff_Supply teaching staff, Teaching and Teaching support staff_Educational consultancy, Teaching and Teaching support staff_Education support staff, Teaching and Teaching support staff_Agency supply teaching staff                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  |
| maintained_schools_master_list | E01  Teaching Staff                                        | Teaching and Teaching support staff_Teaching staff                                                                                                                       | TeachingStaffCosts                                                                      |                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            |
| aar / arr_cs                   | BNCH21101 (Teaching staff)                                 | Teaching and Teaching support staff_Teaching staff / Teaching and Teaching support staff_Teaching staff_CS                                                               | TeachingStaffCosts / TeachingStaffCostsCS                                               |                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            |
| maintained_schools_master_list | E02  Supply teaching staff                                 | Teaching and Teaching support staff_Supply teaching staff                                                                                                                | SupplyTeachingStaffCosts                                                                |                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            |
| aar / arr_cs                   | BNCH21102 (Supply teaching staff - extra note in guidance) | Teaching and Teaching support staff_Supply teaching staff / Teaching and Teaching support staff_Supply teaching staff_CS                                                 | SupplyTeachingStaffCosts / SupplyTeachingStaffCostsCS                                   |                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            |
| maintained_schools_master_list | E03 Education support staff                                | Teaching and Teaching support staff_Educational consultancy                                                                                                              | EducationSupportStaffCosts                                                              |                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            |
| aar / arr_cs                   | BNCH21604 (Educational Consultancy)                        | Teaching and Teaching support staff_Educational consultancy / Teaching and Teaching support staff_Educational consultancy_CS                                             | EducationSupportStaffCosts / EducationSupportStaffCostsCS                               |                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            |
| maintained_schools_master_list | E26 Agency supply teaching staff                           | Teaching and Teaching support staff_Agency supply teaching staff                                                                                                         | AgencySupplyTeachingStaffCosts                                                          |                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            |
| aar / arr_cs                   | BNCH21606 (Agency supply teaching staff)                   | Teaching and Teaching support staff_Agency supply teaching staff / Teaching and Teaching support staff_Agency supply teaching staff_CS                                   | AgencySupplyTeachingStaffCosts / AgencySupplyTeachingStaffCostsCS                       |                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            |
| maintained_schools_master_list | E05 Administrative and clerical staff                      | Non-educational support staff and services_Administrative and clerical staff                                                                                             | AdministrativeClericalStaffCosts                                                        |                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            |
| aar / aar_cs                   | BNCH21104 (Administrative and clerical staff)              | Non-educational support staff and services_Administrative and clerical staff / Non-educational support staff and services_Administrative and clerical staff_CS           | AdministrativeClericalStaffCosts / AdministrativeClericalStaffCosts_CS                  |                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            |
| maintained_schools_master_list | E07  Cost of other staff                                   | Non-educational support staff and services_Other staff                                                                                                                   | OtherStaffCosts                                                                         |                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            |
| aar / aar_cs                   | BNCH21107 (Other staff)                                    | Non-educational support staff and services_Other staff / Non-educational support staff and services_Other staff_CS                                                       | OtherStaffCosts / OtherStaffCosts_CS                                                    |                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            |
| N/A - computed                 |                                                            |                                                                                                                                                                          | TotalNonEducationalSupportStaffCosts / TotalNonEducationalSupportStaffCostsCS           | For Maintaiend Schools, Academies and Central Services, computed as the sum of Non-educational support staff and services_Administrative and clerical staff, Non-educational support staff and services_Other staff, Non-educational support staff and services_Professional services (non-curriculum)                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     |
| maintained_schools_master_list | E28a  Bought in professional services - other (except PFI) | Non-educational support staff and services_Professional services (non-curriculum)                                                                                        | ProfessionalServicesNonCurriculumCosts                                                  |                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            |
| aar / aar_cs                   | BNCH21702 (Professional Services - non-curriculum)         | Non-educational support staff and services_Professional services (non-curriculum) / Non-educational support staff and services_Professional services (non-curriculum)_CS | ProfessionalServicesNonCurriculumCosts / ProfessionalServicesNonCurriculumCostsCS       |                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            |
| N/A - computed                 |                                                            |                                                                                                                                                                          | TotalEducationalSuppliesCosts / TotalEducationalSuppliesCostsCS                         | For Maintaiend Schools, Academies and Central Services, computed as the sum of Educational supplies_Examination fees, Educational supplies_Learning resources (not ICT equipment)                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          |
| maintained_schools_master_list | E21  Exam fees                                             | Educational supplies_Examination fees                                                                                                                                    | ExaminationFeesCosts                                                                    |                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            |
| aar / aar_cs                   | BNCH21603 (Examination fees)                               | Educational supplies_Examination fees / Educational supplies_Examination fees_CS                                                                                         | ExaminationFeesCosts / ExaminationFeesCostsCS                                           |                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            |
| maintained_schools_master_list | E19  Learning resources (not ICT equipment)                | Educational supplies_Learning resources (not ICT equipment)                                                                                                              | LearningResourcesNonIctCosts                                                            |                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            |
| aar / aar_cs                   | BNCH21601 (Learning resources (not ICT equipment))         | Educational supplies_Learning resources (not ICT equipment) / Educational supplies_Learning resources (not ICT equipment)_CS                                             | LearningResourcesNonIctCosts / LearningResourcesNonIctCostsCS                           |                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            |
| maintained_schools_master_list | E20  ICT learning resources                                | Educational ICT_ICT learning resources                                                                                                                                   | LearningResourcesIctCosts                                                               |                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            |
| aar / aar_cs                   | BNCH21602 (ICT learning resources)                         | Educational ICT_ICT learning resources / Educational ICT_ICT learning resources_CS                                                                                       | LearningResourcesIctCosts / LearningResourcesIctCostsCS                                 |                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            |
| N/A - computed                 |                                                            |                                                                                                                                                                          | TotalPremisesStaffServiceCosts / TotalPremisesStaffServiceCostsCS                       | For Maintaiend Schools, Academies and Central Services, computed as the sum of Premises staff and services_Cleaning and caretaking, Premises staff and services_Maintenance of premises, Premises staff and services_Other occupation costs, Premises staff and services_Premises staff                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    |
| maintained_schools_master_list | E14  Cleaning and caretaking                               | Premises staff and services_Cleaning and caretaking                                                                                                                      | CleaningCaretakingCosts                                                                 |                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            |
| aar / aar_cs                   | BNCH21401 (Cleaning and caretaking)                        | Premises staff and services_Cleaning and caretaking / Premises staff and services_Cleaning and caretaking_CS                                                             | CleaningCaretakingCosts / CleaningCaretakingCostsCS                                     |                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            |
| maintained_schools_master_list | E12  Building maintenance and improvement                  | Premises staff and services_Maintenance of premises                                                                                                                      | MaintenancePremisesCosts                                                                |                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            |
| aar / aar_cs                   | BNCH21301 (Maintenance of premises)                        | Premises staff and services_Maintenance of premises / Premises staff and services_Maintenance of premises_CS                                                             | MaintenancePremisesCosts / MaintenancePremisesCostsCS                                   |                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            |
| maintained_schools_master_list | E18  Other occupation costs                                | Premises staff and services_Other occupation costs                                                                                                                       | OtherOccupationCosts                                                                    |                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            |
| aar / aar_cs                   | BNCH21406 (Other occupation costs)                         | Premises staff and services_Other occupation costs / Premises staff and services_Other occupation costs_CS                                                               | OtherOccupationCosts / OtherOccupationCostsCS                                           |                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            |
| maintained_schools_master_list | E04  Premises staff                                        | Premises staff and services_Premises staff                                                                                                                               | PremisesStaffCosts                                                                      |                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            |
| aar / aar_cs                   | BNCH21105 (Premises staff)                                 | Premises staff and services_Premises staff / Premises staff and services_Premises staff_CS                                                                               | PremisesStaffCosts / PremisesStaffCostsCS                                               |                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            |
| N/A - computed                 |                                                            |                                                                                                                                                                          | TotalUtilitiesCosts / TotalUtilitiesCostsCS                                             | For Maintaiend Schools, Academies and Central Services, computed as the sum of Utilities_Energy, Utilities_Water and sewerage                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              |
| maintained_schools_master_list | E16  Energy                                                | Utilities_Energy                                                                                                                                                         | EnergyCosts                                                                             |                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            |
| aar / aar_cs                   | BNCH21403 (Energy)                                         | Utilities_Energy / Utilities_Energy_CS                                                                                                                                   | EnergyCosts / EnergyCostsCS                                                             |                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            |
| maintained_schools_master_list | E15  Water and sewerage                                    | Utilities_Water and sewerage                                                                                                                                             | WaterSewerageCosts                                                                      |                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            |
| aar / aar_cs                   | BNCH21402 (Water and sewerage)                             | Utilities_Water and sewerage / Utilities_Water and sewerage_CS                                                                                                           | WaterSewerageCosts / WaterSewerageCostsCS                                               |                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            |
| maintained_schools_master_list | E22 Administrative supplies                                | Administrative supplies_Administrative supplies (non educational)                                                                                                        | AdministrativeSuppliesNonEducationalCosts                                               |                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            |
| aar / aar_cs                   | BNCH21706 (Administrative supplies - non educational)      | Administrative supplies_Administrative supplies (non educational) / Administrative supplies_Administrative supplies (non educational)_CS                                 | AdministrativeSuppliesNonEducationalCosts / AdministrativeSuppliesNonEducationalCostsCS |                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            |
| N/A - computed                 |                                                            |                                                                                                                                                                          | TotalGrossCateringCosts / TotalGrossCateringCostsCS                                     | For Maintaiend Schools, Academies and Central Services, computed as the sum of Catering staff and supplies_Catering staff, Catering staff and supplies_Catering supplies                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   |
| maintained_schools_master_list | E06 Catering staff                                         | Catering staff and supplies_Catering staff                                                                                                                               | CateringStaffCosts                                                                      |                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            |
| aar / aar_cs                   | BNCH21106 (Catering staff)                                 | Catering staff and supplies_Catering staff / Catering staff and supplies_Catering staff_CS                                                                               | CateringStaffCosts / CateringStaffCostsCS                                               |                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            |
| maintained_schools_master_list | E25  Catering supplies                                     | Catering staff and supplies_Catering supplies                                                                                                                            | CateringSuppliesCosts                                                                   |                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            |
| maintained_schools_master_list | BNCH21701 (Catering supplies)                              | Catering staff and supplies_Catering supplies / Catering staff and supplies_Catering supplies_CS                                                                         | CateringSuppliesCosts / CateringSuppliesCostsCS                                         |                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            |
|N/A - computed|   |   |TotalOtherCosts / TotalOtherCostsCS  |For Maintaiend Schools, Academies and Central Services, computed as the sum of,Other costs_Other insurance premiums,Other costs_Direct revenue financing,Other costs_Grounds maintenance,Other costs_Indirect employee expense,Other costs_Interest charges for loan and bank,Other costs_PFI charges,Other costs_Rent and rates,Other costs_Special facilities,Other costs_Staff development and training,Other costs_Staff-related insurance,Other costs_Supply teacher insurance |
|maintained_schools_master_list | E23  Other insurance premiums  |     Other costs_Other insurance premiums    |OtherInsurancePremiumsCosts  ||  
|aar / aar_cs | BNCH21705 (Other insurance premiums)  |     Other costs_Other insurance premiums / Other costs_Other insurance premiums_CS    |OtherInsurancePremiumsCosts / OtherInsurancePremiumsCostsCS  ||  
|maintained_schools_master_list | E30 Direct revenue financing (revenue contributions to capital)  |     Other costs_Direct revenue financing    |DirectRevenueFinancingCosts  ||  
|aar / aar_cs | BNCH21707 (Direct revenue financing (Revenue contributions to capital))  |     Other costs_Direct revenue financing / Other costs_Direct revenue financing_CS    |DirectRevenueFinancingCosts / DirectRevenueFinancingCostsCS  ||  
|maintained_schools_master_list | E13  Grounds maintenance and improvement  |     Other costs_Grounds maintenance    |GroundsMaintenanceCosts  ||  
|aar / aar_cs | BNCH21405 (Grounds maintenance)  |     Other costs_Grounds maintenance / Other costs_Grounds maintenance_CS    |GroundsMaintenanceCosts / GroundsMaintenanceCostsCS  ||  
|maintained_schools_master_list | E08  Indirect employee expenses  |     Other costs_Indirect employee expenses    |IndirectEmployeeExpenses  ||  
|aar / aar_cs | BNCH21201 (Indirect employee expenses)  |     Other costs_Indirect employee expenses / Other costs_Indirect employee expenses_CS    |IndirectEmployeeExpenses / IndirectEmployeeExpensesCS  ||  
|maintained_schools_master_list | E29  Loan interest  |     Other costs_Interest charges for loan and bank    |InterestChargesLoanBank  ||  
|aar / aar_cs | BNCH21801 (Interest charges for Loan and bank)  |     Other costs_Interest charges for loan and bank / Other costs_Interest charges for loan and bank_CS    |InterestChargesLoanBank / InterestChargesLoanBankCS  ||  
|maintained_schools_master_list | E28b Bought in professional services - other (PFI)  |     Other costs_PFI charges    |PrivateFinanceInitiativeCharges  ||  
|aar / aar_cs | BNCH21802 (PFI Charges)  |     Other costs_PFI charges / Other costs_PFI charges_CS    |PrivateFinanceInitiativeCharges / PrivateFinanceInitiativeChargesCS  ||  
|maintained_schools_master_list | E17  Rates  |     Other costs_Rent and rates    |RentRatesCosts  ||  
|aar / aar_cs | BNCH21404 (Rent and rates)  |     Other costs_Rent and rates / Other costs_Rent and rates_CS    | RentRatesCosts / RentRatesCostsCS  ||  
|maintained_schools_master_list | E24  Special facilities  |     Other costs_Special facilities    |SpecialFacilitiesCosts  ||  
|aar / aar_cs | BNCH21501 (Special facilities)  |     Other costs_Special facilities / Other costs_Special facilities_CS    |SpecialFacilitiesCosts / SpecialFacilitiesCostsCS  ||  
|maintained_schools_master_list | E09  Development and training  |     Other costs_Staff development and training    |StaffDevelopmentTrainingCosts  ||  
|aar / aar_cs | BNCH21202 (Staff development and training)  |     Other costs_Staff development and training / Other costs_Staff development and training_CS    |StaffDevelopmentTrainingCosts / StaffDevelopmentTrainingCostsCS  ||  
|maintained_schools_master_list | E11  Staff related insurance  |     Other costs_Staff-related insurance    |StaffRelatedInsuranceCosts  ||  
|aar / aar_cs | BNCH21203 (Staff-related insurance)  |     Other costs_Staff-related insurance / Other costs_Staff-related insurance_CS    |StaffRelatedInsuranceCosts / StaffRelatedInsuranceCostsCS  ||  
|maintained_schools_master_list | E10  Supply teacher insurance  |     Other costs_Supply teacher insurance    |SupplyTeacherInsurableCosts  ||  
|aar / aar_cs | BNCH21204 (Supply teacher insurance)  |     Other costs_Supply teacher insurance / Other costs_Supply teacher insurance_CS    |SupplyTeacherInsurableCosts / SupplyTeacherInsurableCostsCS  ||  
|maintained_schools_master_list | E31  Community focused school staff  |     Other costs_School staff    |CommunityFocusedSchoolStaff  | Not provided for Academies and Central Services |  
|maintained_schools_master_list | E32 Community focused school costs  |     Other costs_School costs    |CommunityFocusedSchoolCosts  | Not provided for Academies and Central Services |  
|maintained_schools_master_list | E27  Bought in professional services - curriculum  |     Teaching and Teaching support staff_Educational consultancy    |EducationalConsultancyCosts  ||  
|aar / aar_cs | BNCH21604 (Educational Consultancy)  |     Teaching and Teaching support staff_Educational consultancy / Teaching and Teaching support staff_Educational consultancy_CS    |EducationalConsultancyCosts / EducationalConsultancyCostsCS  ||  
|N/A - computed|   |   |TotalNonEducationalSupportStaffCosts / TotalNonEducationalSupportStaffCostsCS  |For Maintaiend Schools, Academies and Central Services, computed as the sum of Non-educational support staff and services_Administrative and clerical staff, Non-educational support staff and services_Other staff, Non-educational support staff and services_Professional services (non-curriculum)    |
|maintained_schools_master_list | E23  Other insurance premiums  |     Other costs_Other insurance premiums    |OtherInsurancePremiumsCosts  ||  
|aar / aar_cs | BNCH21705 (Other insurance premiums)  |     Other costs_Other insurance premiums / Other costs_Other insurance premiums_CS    |OtherInsurancePremiumsCosts / OtherInsurancePremiumsCostsCS  ||  
|  maintained_schools_master_list   | Direct Grant  |     Income_Direct grants    | DirectGrants  |  Not provided for Academies and Central Services |
|     N/A - computed |    |     Income_Pre Post 16    |  PrePost16Funding  |   Computed from the sum of I01  Funds delegated by the LA, I02  Funding for 6th form students  |
|     aar / aar_cs |  BNCH11142 (Other Revenue)  |     Income_Pre Post 16 / Income_Pre Post 16_CS    |  PrePost16Funding / PrePost16FundingCS  |     |
|     maintained_schools_master_list  | I06  Other government grants  |     Income_Other DFE grants    |OtherDfeGrants  |  Not provided for Academies and Central Services  |
|     maintained_schools_master_list  | I07  Other grants and payments  |     Income_Other grants    |OtherIncomeGrants  |    |
|     aar / aar_cs  | BNCH11151 (Other Government Revenue Grants)  |     Income_Other grants / Income_Other grants_CS    |OtherIncomeGrants / OtherIncomeGrantsCS  |    |
|     aar / aar_cs  | BNCH11161 (Government source (non-grant))  |     Income_Government source / Income_Government source_CS    | GovernmentSource / GovernmentSourceCS  |  Not provided for Maintained Schools  |
|     maintained_schools_master_list  | Community Grants  |     Income_Other Revenue Income    |CommunityGrants  |  Not provided for Academies and Central Services  |
|     aar / aar_cs  | BNCH11162 (Academies)  |     Income_Academies / Income_Academies_CS    | Academies / AcademiesCS  |  Not provided for Maintained Schools  |
|     maintained_schools_master_list  | Self Generated Funding  |     Income_Total self generated funding    |TotalSelfGeneratedFunding  |  Not provided for Academies and Central Services  |
|     maintained_schools_master_list  | I08  Income from facilities and services  |     Income_Facilities and services    |IncomeFacilitiesServices  |  Not provided for Academies and Central Services  |
|     maintained_schools_master_list  | I09  Income from catering  |     Income_Catering services    |IncomeCateringServices  |    |
|     aar / aar_cs  | BNCH11202 (Income from catering)  |     Income_Catering services / Income_Catering services_CS    |IncomeCateringServices / IncomeCateringServicesCS  |    |
|     maintained_schools_master_list  | I13  Donations and or private funds  |     Income_Donations and voluntary funds    |DonationsVoluntaryFunds  |    |
|     aar / aar_cs  | BNCH11300T (Voluntary income)  |     Income_Donations and voluntary funds / Income_Donations and voluntary funds_CS    |DonationsVoluntaryFunds / DonationsVoluntaryFundsCS  |    |
|     maintained_schools_master_list  | I10  Receipts from supply teacher insurance claims  |     Income_Receipts supply teacher insurance    |ReceiptsSupplyTeacherInsuranceClaims  |    |
|     aar / aar_cs  | BNCH11203 (Receipts from supply teacher insurance claims)  |     Income_Receipts supply teacher insurance / Income_Receipts supply teacher insurance_CS    |ReceiptsSupplyTeacherInsuranceClaims / ReceiptsSupplyTeacherInsuranceClaimsCS  |    |
|     aar / aar_cs  | BNCH11400T (Investment income)  |     Income_Investment income / Income_Investment income_CS    |InvestmentIncome / InvestmentIncomeCS  |  Not provided for Maintained Schools  |
|     aar / aar_cs  | BNCH11204 (Other income - revenue)  |     Income_Other self-generated income / Income_Other self-generated income_CS    |OtherSelfGeneratedIncome / OtherSelfGeneratedIncomeCS  |  Not provided for Maintained Schools  |
|     N/A - computed |   |    In Year Balance    | InYearBalance  | Computed as (Total Income   I01 to I18) - (Total Expenditure  E01 to E32)   |
|     N/A - computed |   |    In Year Balance_CS/In Year Balance_CS    | InYearBalance / InYearBalance_CS  | Computed as (BNCH11110T (EFA Revenue Grants)) - (BNCH20000T (Total Costs))   |
|     maintained_schools_master_list  | Revenue Reserve   B01 plus B02 plus B06  |    Revenue reserve    | RevenueReserve  |    |
|     aar / aar_cs  | BNCH44001CS (Closing Balance (Restricted and Unrestricted Funds))  |    Revenue reserve / Revenue reserve_CS    | RevenueReserve / RevenueReserve_CS  |    |
|     N/A - computed |   |  Income_Total grant funding | TotalGrantFunding / TotalGrantFundingCS | For Maintaiend Schools, Academies and Central Services, computed as the sum of Direct Grant, Community Grants, Targeted Grants   |
|     maintained_schools_master_list  | Targeted Grants  |    Targeted Grants    |  | Only used in TotalGrantFunding computation   |
|     aar / aar_cs  | BNCH11141 (SEN)  |     Targeted Grants   |  | Only used in TotalGrantFunding computation   |
|     maintained_schools_master_list  | Total Income   I01 to I18  |    Total Income    | TotalIncome |    |
|     N/A - computed |   |    Total Income / Total Income_CS    | TotalIncome / TotalIncomeCS |  Computed as (Academies): Income_Total grant funding + Income_Total self generated funding - BNCH21707 (Direct revenue financing (Revenue contributions to capital)) + BNCH11123-BAI011-A (Academies - Income), (Central Services): Income_Total grant funding + Income_Total self generated funding - BNCH21707 (Direct revenue financing (Revenue contributions to capital)) + BNCH11123-BTI011-A (MAT Central services - Income)  |
|     maintained_schools_master_list  | Period covered by return (months)  |    Period covered by return    | PeriodCoveredByReturn | |
|     N/A - computed  |   |        | PeriodCoveredByReturn |  Mapped based on Academies Date joined or opened if in period and Date left or closed if in period  |
|     N/A - computed |   |    Financial Position    | FinancialPosition  | Assigned based off the In Year Balance value   |
|     N/A - computed |   |    Trust Financial Position    | TrustPosition  | Assigned based off the Trust Balance   |
|     gias | TypeOfEstablishment (name)  |    TypeOfEstablishment (name)    | EstablishmentType  |    |
|     aar / aar_cs  | BNCH21703 (Auditor costs)  |    Non-educational support staff and services_Audit cost / Non-educational support staff and services_Audit cost_CS    | AuditorCosts / AuditorsCostsCS  |  Not provided for Maintained Schools   |
|     aar / aar_cs  | BNCH20000T (Total Costs)  |        |   |  Only used in InYearBalance computation   |

### Non-financial

| raw file name                 | raw column name                                                                                                                                                                       | pre-processing column name                                              | sql table column name                   | notes on transformation                                                                                                                                      |
|-------------------------------|---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|-------------------------------------------------------------------------|-----------------------------------------|--------------------------------------------------------------------------------------------------------------------------------------------------------------|
| N/A - defined in pipeline run |                                                                                                                                                                                       |                                                                         | RunType                                 |                                                                                                                                                              |
| N/A - defined in pipeline run |                                                                                                                                                                                       |                                                                         | RunId                                   |                                                                                                                                                              |
| gias                          | URN                                                                                                                                                                                   | URN                                                                     | URN                                     |                                                                                                                                                              |
| gias                          | TypeOfEstablishment (name)                                                                                                                                                            | TypeOfEstablishment (name)                                              | EstablishmentType                       |                                                                                                                                                              |
| census_pupils                 | fte pupils                                                                                                                                                                            | Number of pupils                                                        | TotalPupils                             | Also includes the addition of Pupil Dual Registrations                                                                                                       |
| census_pupils                 | number_of_dual_subsidiary_registrations                                                                                                                                               | Pupil Dual Registrations                                                | N/A - only used in computation          |                                                                                                                                                              |
| census_pupils                 | % of pupils known to be eligible for free school meals                                                                                                                                | Percentage Free school meals                                            | PercentFreeSchoolMeals                  |                                                                                                                                                              |
| sen                           | Total pupils                                                                                                                                                                          |                                                                         | N/A - only used in computation          |                                                                                                                                                              |
| sen                           | SEN support                                                                                                                                                                           |                                                                         | N/A - only used in computation          |                                                                                                                                                              |
| census_pupils                 | EHC plan                                                                                                                                                                              |                                                                         | N/A - only used in computation          |                                                                                                                                                              |
| N/A - computed                |                                                                                                                                                                                       | Percentage SEN                                                          | PercentSpecialEducationNeeds            | Percentage SEN computed as ((EHC plan + SEN support) / Total pupils) * 100.0                                                                                 |
| cdc                           | GIFA                                                                                                                                                                                  | Total Internal Floor Area                                               | TotalInternalFloorArea                  | TotalInternalFloorArea is the aggregated sum of the floor area of each building associated with a single URN                                                 |
| census_workforce              | Total School Workforce (Full-Time Equivalent)                                                                                                                                         | Total School Workforce (Full-Time Equivalent)                           | WorkforceFTE                            |                                                                                                                                                              |
| census_workforce              | Total Number of Teachers (Full-Time Equivalent)                                                                                                                                       | Total Number of Teachers (Full-Time Equivalent)                         | TeachersFTE                             |                                                                                                                                                              |
| census_workforce              | Teachers with Qualified Teacher Status (%) (Headcount)                                                                                                                                | Teachers with Qualified Teacher Status (%) (Headcount)                  | PercentTeacherWithQualifiedStatus       |                                                                                                                                                              |
| census_workforce              | Total Number of Teachers in the Leadership Group (Full-time Equivalent)                                                                                                               | Total Number of Teachers in the Leadership Group (Full-time Equivalent) | SeniorLeadershipFTE                     |                                                                                                                                                              |
| census_workforce              | Total Number of Teaching Assistants (Full-Time Equivalent)                                                                                                                            | Total Number of Teaching Assistants (Full-Time Equivalent)              | TeachingAssistantFTE                    |                                                                                                                                                              |
| census_workforce              | Total Number of Non-Classroom-based School Support Staff, (Other school support staff plus Administrative staff plus Technicians and excluding Auxiliary staff (Full-Time Equivalent) | NonClassroomSupportStaffFTE                                             | NonClassroomSupportStaffFTE             |                                                                                                                                                              |
| census_workforce              | Total Number of Auxiliary Staff (Full-Time Equivalent)                                                                                                                                | Total Number of Auxiliary Staff (Full-Time Equivalent)                  | AuxiliaryStaffFTE                       |                                                                                                                                                              |
| census_workforce              | Total School Workforce (Headcount)                                                                                                                                                    | Total School Workforce (Headcount)                                      | WorkforceHeadcount                      |                                                                                                                                                              |
| cdc                           | Block Age                                                                                                                                                                             | Indicative Age / Building Age                                           | BuildingAverageAge                      | Building Age is computed from as the mean Indicative Age of all blocks aggregated by URN. The Indicative age is the integer representation of the Block Age. |
| N/A - computed                |                                                                                                                                                                                       | TotalPupilsSixthForm                                                    | TotalPupilsSixthForm                    | Computed as the sum of Full time boys Year group 12, Full time boys Year group 13, Full time girls Year group 12, Full time girls Year group 13              |
| cdc                           | Full time boys Year group 12                                                                                                                                                          |                                                                         | N/A - only used in computation          |                                                                                                                                                              |
| cdc                           | Full time boys Year group 13                                                                                                                                                          |                                                                         | N/A - only used in computation          |                                                                                                                                                              |
| cdc                           | Full time girls Year group 12                                                                                                                                                         |                                                                         | N/A - only used in computation          |                                                                                                                                                              |
| cdc                           | Full time girls Year group 13                                                                                                                                                         |                                                                         | N/A - only used in computation          |                                                                                                                                                              |
| N/A - computed                |                                                                                                                                                                                       | TotalPupilsNursery                                                      | TotalPupilsNursery                      | Computed as the sum of Number of early year pupils (years E1 and E2),Number of nursery pupils (years N1 and N2)                                              |
| census_pupils                 | Number of early year pupils (years E1 and E2)                                                                                                                                         | Number of early year pupils (years E1 and E2)                           | N/A - only used in computation          |                                                                                                                                                              |
| census_pupils                 | Number of nursery pupils (years N1 and N2)                                                                                                                                            | Number of nursery pupils (years N1 and N2)                              | N/A - only used in computation          |                                                                                                                                                              |
| workforce_census              | Total Number of Teachers (Headcount)                                                                                                                                                  | Total Number of Teachers (Headcount)                                    | TeachersHeadcount                       |                                                                                                                                                              |
| census_workforce              | Total Number of Teachers in the Leadership Group (Headcount)                                                                                                                          | Total Number of Teachers in the Leadership Group (Headcount)            | SeniorLeadershipHeadcount               |                                                                                                                                                              |
| census_workforce              | Total Number of Teaching Assistants (Headcount)                                                                                                                                       | Total Number of Teaching Assistants (Headcount)                         | TeachingAssistantHeadcount              |                                                                                                                                                              |
| census_workforce              | Total Number of Non Classroom-based School Support Staff, Excluding Auxiliary Staff (Headcount)                                                                                       | NonClassroomSupportStaffHeadcount                                       | NonClassroomSupportStaffHeadcount       |                                                                                                                                                              |
| census_workforce              | Total Number of Auxiliary Staff (Headcount)                                                                                                                                           | Total Number of Auxiliary Staff (Headcount)                             | AuxiliaryStaffHeadcount                 |                                                                                                                                                              |
| N/A - computed                |                                                                                                                                                                                       | Percentage with EHC                                                     | PercentWithEducationalHealthCarePlan    | Computed as EHC plan / Total Pupils                                                                                                                          |
| N/A - computed                |                                                                                                                                                                                       | Percentage without EHC                                                  | PercentWithoutEducationalHealthCarePlan | Computed as Percentage SEN - Percentage with EHC                                                                                                             |
| N/A - computed                |                                                                                                                                                                                       | KS2Progress                                                             | KS2Progress                             | Computed as the sum of READPROG, MATPROG, WRITPROG                                                                                                           |
| ks2                           | READPROG                                                                                                                                                                              |                                                                         | N/A - only used in computation          |                                                                                                                                                              |
| ks2                           | MATPROG                                                                                                                                                                               |                                                                         | N/A - only used in computation          |                                                                                                                                                              |
| ks2                           | WRITPROG                                                                                                                                                                              |                                                                         | N/A - only used in computation          |                                                                                                                                                              |
| ks4                           | P8MEA                                                                                                                                                                                 | Progress8Measure                                                        | KS4Progress                             |                                                                                                                                                              |
| N/A - undefined               | N/A                                                                                                                                                                                   | N/A                                                                     | PredictedPercentChangePupils3To5Years   |                                                                                                                                                              |
| N/A - computed                |                                                                                                                                                                                       | Percentage Primary Need VI                                              | PercentWithVI                           | Computed as Primary Need VI / Total Pupils                                                                                                                   |
| sen                           | Primary Need VI                                                                                                                                                                       |                                                                         | N/A - only used in computation          |                                                                                                                                                              |
| N/A - computed                |                                                                                                                                                                                       | Percentage Primary Need SPLD                                            | PercentWithSPLD                         | Computed as Primary Need SPLD / Total Pupils                                                                                                                 |
| sen                           | Primary Need SPLD                                                                                                                                                                     |                                                                         | N/A - only used in computation          |                                                                                                                                                              |
| N/A - computed                |                                                                                                                                                                                       | Percentage Primary Need SLD                                             | PercentWithSLD                          | Computed as Primary Need SLD / Total Pupils                                                                                                                  |
| sen                           | Primary Need SLD                                                                                                                                                                      |                                                                         | N/A - only used in computation          |                                                                                                                                                              |
| N/A - computed                |                                                                                                                                                                                       | Percentage Primary Need SLCN                                            | PercentWithSLCN                         | Computed as Primary Need SLCN / Total Pupils                                                                                                                 |
| sen                           | Primary Need SLCN                                                                                                                                                                     |                                                                         | N/A - only used in computation          |                                                                                                                                                              |
| N/A - computed                |                                                                                                                                                                                       | Percentage Primary Need SEMH                                            | PercentWithSEMH                         | Computed as Primary Need SEMH / Total Pupils                                                                                                                 |
| sen                           | Primary Need SEMH                                                                                                                                                                     |                                                                         | N/A - only used in computation          |                                                                                                                                                              |
| N/A - computed                |                                                                                                                                                                                       | Percentage Primary Need PMLD                                            | PercentWithPMLD                         | Computed as Primary Need PMLD / Total Pupils                                                                                                                 |
| sen                           | Primary Need PMLD                                                                                                                                                                     |                                                                         | N/A - only used in computation          |                                                                                                                                                              |
| N/A - computed                |                                                                                                                                                                                       | Percentage Primary Need PD                                              | PercentWithPD                           | Computed as Primary Need PD / Total Pupils                                                                                                                   |
| sen                           | Primary Need PD                                                                                                                                                                       |                                                                         | N/A - only used in computation          |                                                                                                                                                              |
| N/A - computed                |                                                                                                                                                                                       | Percentage Primary Need OTH                                             | PercentWithOTH                          | Computed as Primary Need OTH / Total Pupils                                                                                                                  |
| sen                           | Primary Need OTH                                                                                                                                                                      |                                                                         | N/A - only used in computation          |                                                                                                                                                              |
| N/A - computed                |                                                                                                                                                                                       | Percentage Primary Need MSI                                             | PercentWithMSI                          | Computed as Primary Need MSI / Total Pupils                                                                                                                  |
| sen                           | Primary Need MSI                                                                                                                                                                      |                                                                         | N/A - only used in computation          |                                                                                                                                                              |
| N/A - computed                |                                                                                                                                                                                       | Percentage Primary Need MLD                                             | PercentWithMLD                          | Computed as Primary Need MLD / Total Pupils                                                                                                                  |
| sen                           | Primary Need MLD                                                                                                                                                                      |                                                                         | N/A - only used in computation          |                                                                                                                                                              |
| N/A - computed                |                                                                                                                                                                                       | Percentage Primary Need HI                                              | PercentWithHI                           | Computed as Primary Need HI / Total Pupils                                                                                                                   |
| sen                           | Primary Need HI                                                                                                                                                                       |                                                                         | N/A - only used in computation          |                                                                                                                                                              |
| N/A - computed                |                                                                                                                                                                                       | Percentage Primary Need ASD                                             | PercentWithASD                          | Computed as Primary Need ASD / Total Pupils                                                                                                                  |
| sen                           | Primary Need ASD                                                                                                                                                                      |                                                                         | N/A - only used in computation          |                                                                                                                                                              |

### School

| raw file name                  | raw column name             | pre-processing column name  | sql table column name | notes on transformation                                                                                 |
|--------------------------------|-----------------------------|-----------------------------|-----------------------|---------------------------------------------------------------------------------------------------------|
| gias                           | URN                         | URN                         | URN                   |                                                                                                         |
| gias                           | Street                      | Street                      | AddressStreet         |                                                                                                         |
| gias                           | Locality                    | Locality                    | AddressLocality       |                                                                                                         |
| gias                           | Address3                    | Address3                    | AddressLine3          |                                                                                                         |
| gias                           | Town                        | Town                        | AddressTown           |                                                                                                         |
| gias                           | County (name)               | County (name)               | AddressCounty         |                                                                                                         |
| gias                           | Postcode                    | Postcode                    | AddressPostcode       |                                                                                                         |
| gias                           | TelephoneNum                | TelephoneNum                | Telephone             |                                                                                                         |
| gias                           | LA (name)                   | LA Name                     | LAName                |                                                                                                         |
| gias                           | SchoolWebsite               | SchoolWebsite               | Website               |                                                                                                         |
| gias                           | EstablishmentName           | EstablishmentName           | SchoolName            |                                                                                                         |
| academies_master_list          | Company Registration Number | Company Registration Number | TrustCompanyNumber    |                                                                                                         |
| academies_master_list          | Academy Trust Name          | Trust Name                  | TrustName             |                                                                                                         |
| N/A - undefined                | N/A                         | N/A                         | FederaitonLeadURN     |                                                                                                         |
| gias                           | LA (code)                   | LA Code                     | LACode                |                                                                                                         |
| maintained_schools_master_list | London Weighting            | London Weighting            | LondonWeighting       |                                                                                                         |
| N/A - computed                 |                             | London Weighting            | LondonWeighting       | Assinged via a mapping based off the academies LA and Estab values                                      |
| N/A - computed                 |                             | Finance Type                | FinanceType           | Set during the preprocessing pipeline during the respective academy / maintained schools run            |
| maintained_schools_master_list | Overall Phase               | Overall Phase               | OverallPhase          |                                                                                                         |
| N/A - computed                 |                             | Overall Phase               | OverallPhase          | Assinged via a mapping based off the academies TypeOfEstablishment (code) and Type of Provision - Phase |
| gias                           | TypeOfEstablishment (name)  | TypeOfEstablishment (name)  | SchoolType            |                                                                                                         |
| gias                           | OfficialSixthForm (name)    | Has Sixth Form              | HasSixthForm          | Assinged through boolean logic operation                                                                |
| gias                           | NurseryProvision (name)     | Has Nursery                 | HasNursery            | Assigned through boolean logic operation                                                                |
| maintained_schools_master_list | PFI                         | Is PFI                      | IsPFISchool           | Assigned through boolean logic operation                                                                |
| aar                            | PFI School                  | Is PFI                      | IsPFISchool           | Assigned through boolean logic operation                                                                |
| gias                           | OfstedLastInsp              | OfstedLastInsp              | OfstedDate            |                                                                                                         |
| gias                           | OfstedRating (name)         | OfstedRating (name)         | OfstedDescription     |                                                                                                         |

### MetricRAG

| raw file name                 | raw column name | pre-processing column name | sql table column name | notes on transformation                                                                                                                                                 |
|-------------------------------|-----------------|----------------------------|-----------------------|-------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| N/A - defined in pipeline run |                 |                            | RunType               |                                                                                                                                                                         |
| N/A - defined in pipeline run |                 |                            | RunId                 |                                                                                                                                                                         |
| gias                          | URN             | URN                        | URN                   |                                                                                                                                                                         |
| N/A - defined in pipeline run |                 | Category                   | Category              |                                                                                                                                                                         |
| N/A - defined in pipeline run |                 | Subcategory                | Subcategory           |                                                                                                                                                                         |
| N/A - computed                |                 | Value                      | Value                 | The numerical value for the specific Category, for a given URN                                                                                                          |
| N/A - computed                |                 | Median                     | Median                | Computed from the median of a series of Category values for a comparator set                                                                                            |
| N/A - computed                |                 | DiffMedian                 | DiffMedian            | Computed as the difference between the Value for a given URN and the Median for the entire comparator set, for a given Catagory                                         |
| N/A - computed                |                 | PercentDiff                | PercentDiff           | Computed as the DiffMedian / Median * 100, set to 0 for erroneous values.                                                                                               |
| N/A - computed                |                 | Percentile                 | Percentile            | Computed by ranking the series, then dividing the Rank index of the Value, for the desired URN, by the length of the set and multiplying by 100 to give the percentile. |
| N/A - computed                |                 | Decile                     | Decile                | Given as the integer value of  Percentile / 10                                                                                                                          |
| N/A - defined in pipeline run |                 | RAG                        | RAG                   | Determined by mapping the Decile to a predefined list of RAG statuses in `config.py`                                                                                    |

### ComparatorSet

| raw file name                 | raw column name | pre-processing column name | sql table column name | notes on transformation                                                                                                                                       |
|-------------------------------|-----------------|----------------------------|-----------------------|---------------------------------------------------------------------------------------------------------------------------------------------------------------|
| N/A - defined in pipeline run |                 |                            | RunType               |                                                                                                                                                               |
| N/A - defined in pipeline run |                 |                            | RunId                 |                                                                                                                                                               |
| gias                          | URN             | URN                        | URN                   |                                                                                                                                                               |
| N/A - computed                |                 | Pupil                      | Pupil                 | A comparator set list of URNs for the top 30 schools determined by the pupil characteristic euclidean distance caluclation outlined in `3_Data-Processing`    |
| N/A - computed                |                 | Building                   | Building              | A comparator set list of URNs for the top 30 schools determined by the building characteristic euclidean distance caluclation outlined in `3_Data-Processing` |

### Trust

| raw file name         | raw column name             | pre-processing column name  | sql table column name          | notes on transformation                                         |
|-----------------------|-----------------------------|-----------------------------|--------------------------------|-----------------------------------------------------------------|
| academies_master_list | Company Registration Number | Company Registration Number | CompanyNumber                  |                                                                 |
| academies_master_list | Academy Trust Name          | Trust Name                  | TrustName                      |                                                                 |
| N/A - computed        |                             | CFO Name                    | CFOName                        | Based on the string concatenation of Title, Forename 1, Surname |
| cfo                   | Title                       | Title                       | N/A - only used in computation |                                                                 |
| cfo                   | Forename 1                  | Forename 1                  | N/A - only used in computation |                                                                 |
| cfo                   | Surname                     | Surname                     | N/A - only used in computation |                                                                 |
| cfo                   | Direct email address        | CFO Email                   | CFOEmail                       |                                                                 |
| gias                  | OpenDate                    | OpenDate                    | OpenDate                       | Converted to Datetime in pipeline                               |
| academy_master_list   | Academy Trust UPIN          | Academy Trust UPIN          | TrustUPIN                      |                                                                 |

### LocalAuthority

| raw file name | raw column name | pre-processing column name | sql table column name | notes on transformation |
|---------------|-----------------|----------------------------|-----------------------|-------------------------|
| gias          | LA (code)       | LA Code                    | Code                  |                         |
| gias          | LA (name)       | LA Name                    | Name                  |                         |

### BudgetForecastReturnMetric

| raw file name                 | raw column name             | pre-processing column name | sql table column name | notes on transformation                                                                                                            |
|-------------------------------|-----------------------------|----------------------------|-----------------------|------------------------------------------------------------------------------------------------------------------------------------|
| N/A - defined in pipeline run |                             |                            | RunType               |                                                                                                                                    |
| N/A - defined in pipeline run |                             |                            | RunId                 |                                                                                                                                    |
| N/A - defined in pipeline run |                             |                            | Year                  |                                                                                                                                    |
| academies_master_list         | Company Registration Number | CompanyNumber              | CompanyNumber         | Determined by merging the bfr data with the academies data on the Trust UPIN                                                       |
| N/A - defined in pipeline run |                             | Metric                     | Metric                |                                                                                                                                    |
| N/A - computed                |                             | Value                      | Value                 | The numerical value for a given metric, for a given Trust UPIN. Determined by unpivoting the year forecast columns in the raw data |

### BudgetForecastReturn

| raw file name                 | raw column name             | pre-processing column name | sql table column name | notes on transformation                                                                                                                   |
|-------------------------------|-----------------------------|----------------------------|-----------------------|-------------------------------------------------------------------------------------------------------------------------------------------|
| N/A - defined in pipeline run |                             |                            | RunType               |                                                                                                                                           |
| N/A - defined in pipeline run |                             |                            | RunId                 |                                                                                                                                           |
| N/A - defined in pipeline run |                             |                            | Year                  |                                                                                                                                           |
| academies_master_list         | Company Registration Number | CompanyNumber              | CompanyNumber         | Determined by merging the bfr data with the academies data on the Trust UPIN                                                              |
| bfr_SOFA_raw                  | Category                    | Category                   | Category              | Mapped to a given metric provided in the BFR data, e.g. Revenue Reserves                                                                  |
| N/A - computed                |                             | Value                      | Value                 | The numerical value for the specific Category, for a given Trust UPIN. Determined by unpivoting the year forecast columns in the raw data |
| N/A - computed                |                             | Value                      | TotalPupils           | Taken as a value from the "Pupil numbers" Catagory and aggregated based on the TrustUPIN                                                  |

### TrustHistory

| raw file name   | raw column name | pre-processing column name | sql table column name | notes on transformation |
|-----------------|-----------------|----------------------------|-----------------------|-------------------------|
| N/A - undefined | N/A             | N/A                        | Id                    |                         |
| N/A - undefined | N/A             | N/A                        | CompanyNumber         |                         |
| N/A - undefined | N/A             | N/A                        | EventDate             |                         |
| N/A - undefined | N/A             | N/A                        | EventName             |                         |
| N/A - undefined | N/A             | N/A                        | AcademicYear          |                         |
| N/A - undefined | N/A             | N/A                        | SchoolURN             |                         |
| N/A - undefined | N/A             | N/A                        | SchoolName            |                         |

<!-- Leave the rest of this page blank -->
\newpage

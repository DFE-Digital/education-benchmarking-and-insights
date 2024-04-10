using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace Platform.Domain;

[ExcludeFromCodeCoverage]
public record SchoolTrustFinancialDataObject : QueryableFinancesDataObject
{

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.SCHOOL_NAME)]
    public string? SchoolName { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.FINANCE_TYPE)]
    public string? FinanceType { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.COMPANY_NUMBER)]
    public int CompanyNumber { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.UID)]
    public int Uid { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.TRUST_COMPANY_NAME)]
    public string? TrustOrCompanyName { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.OTHER_INCOME)]
    public decimal OtherIncome { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.SEN)]
    public decimal Sen { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.OTHER_DFE_GRANTS)]
    public decimal OtherDfeGrants { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.OTHER_INCOME_GRANTS)]
    public decimal OtherIncomeGrants { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.GOVERNMENT_SOURCE)]
    public decimal GovernmentSource { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.ACADEMIES)]
    public decimal Academies { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.NON_GOVERNMENT)]
    public decimal NonGovernment { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.INCOME_FROM_FACILITIES)]
    public decimal IncomeFromFacilities { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.INCOME_FROM_CATERING)]
    public decimal IncomeFromCatering { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.RECEIPTS_FROM_SUPPLY)]
    public decimal ReceiptsFromSupply { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.RECEIPTS_FROM_OTHER)]
    public decimal ReceiptsFromOther { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.DONATIONS)]
    public decimal Donations { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.OTHER_SELF_GENERATED)]
    public decimal OtherSelfGenerated { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.INVESTMENT_INCOME)]
    public decimal InvestmentIncome { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.TEACHING_STAFF)]
    public decimal TeachingStaff { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.SUPPLY_TEACHING_STAFF)]
    public decimal SupplyTeachingStaff { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.EDUCATION_SUPPORT_STAFF)]
    public decimal EducationSupportStaff { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.ADMIN_CLERIC_STAFF)]
    public decimal AdministrativeClericalStaff { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.PREMISES_STAFF)]
    public decimal PremisesStaff { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.CATERING_STAFF)]
    public decimal CateringStaff { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.OTHER_STAFF)]
    public decimal OtherStaff { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.INDIRECT_EMPLOYEE_EXPENSES)]
    public decimal IndirectEmployeeExpenses { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.STAFF_DEV)]
    public decimal StaffDevelopment { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.STAFF_INSURANCE)]
    public decimal StaffInsurance { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.SUPPLY_TEACHER_INSURANCE)]
    public decimal SupplyTeacherInsurance { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.BUILDING_GROUNDS)]
    public decimal BuildingGroundsMaintenance { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.CLEANING)]
    public decimal CleaningCaretaking { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.WATER_SEWERAGE)]
    public decimal WaterSewerage { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.ENERGY)]
    public decimal Energy { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.RATES)]
    public decimal Rates { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.RENT_RATES)]
    public decimal RentRates { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.OTHER_OCCUPATION)]
    public decimal OtherOccupationCosts { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.SPECIAL_FACILITIES)]
    public decimal SpecialFacilities { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.LEARNING_RESOURCES)]
    public decimal LearningResources { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.ICT_LEARNING_RESOURCES)]
    public decimal IctLearningResources { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.EXAM_FEES)]
    public decimal ExaminationFees { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.EDUCATIONAL_CONSULTANCY)]
    public decimal EducationalConsultancy { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.ADMIN_SUPPLIES)]
    public decimal AdministrativeSupplies { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.AGENCY_TEACH_STAFF)]
    public decimal AgencyTeachingStaff { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.CATERING_SUPPLIES)]
    public decimal CateringSupplies { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.OTHER_INSURANCE)]
    public decimal OtherInsurancePremiums { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.LEGAL_PROFESSIONAL)]
    public decimal LegalProfessional { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.AUDITOR_COSTS)]
    public decimal AuditorCosts { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.INTEREST_CHARGES)]
    public decimal InterestCharges { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.DIRECT_REVENUE)]
    public decimal DirectRevenue { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.PFI_CHARGES)]
    public decimal PfiCharges { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.IN_YEAR_BALANCE)]
    public decimal InYearBalance { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.GRANT_FUNDING)]
    public decimal GrantFunding { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.DIRECT_GRANT)]
    public decimal DirectGrant { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.COMMUNITY_GRANTS)]
    public decimal CommunityGrants { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.TARGETED_GRANTS)]
    public decimal TargetedGrants { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.SELF_GENERATED_FUNDING)]
    public decimal SelfGeneratedFunding { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.TOTAL_INCOME)]
    public decimal TotalIncome { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.SUPPLY_STAFF)]
    public decimal SupplyStaff { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.OTHER_STAFF_COSTS)]
    public decimal OtherStaffCosts { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.STAFF_TOTAL)]
    public decimal StaffTotal { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.MAINTENANCE_IMPROVEMENT)]
    public decimal MaintenanceImprovement { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.GROUNDS_MAINTENANCE_IMPROVEMENT)]
    public decimal GroundsMaintenanceImprovement { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.BUILDING_MAINTENANCE_IMPROVEMENT)]
    public decimal BuildingMaintenanceImprovement { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.PREMISES)]
    public decimal Premises { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.CATERING_EXP)]
    public decimal CateringExp { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.OCCUPATION)]
    public decimal Occupation { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.SUPPLIES_SERVICES)]
    public decimal SuppliesServices { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.EDUCATIONAL_SUPPLIES)]
    public decimal EducationalSupplies { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.BROUGHT_IN_SERVICES)]
    public decimal BroughtProfessionalServices { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.BOUGHT_IN_OTHER)]
    public decimal BoughtInOther { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.BOUGHT_IN_NOT_PFI)]
    public decimal BoughtInNotPfi { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.COMM_FOCUSED_STAFF)]
    public decimal CommunityFocusedStaff { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.COMM_FOCUSED_SCHOOL)]
    public decimal CommunityFocusedSchoolCosts { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.PRE_POST_16_FUNDING)]
    public decimal PrePost16Funding { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.COMMUNITY_FOCUSED)]
    public decimal CommunityFocusedFunding { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.ADDITIONAL_GRANT)]
    public decimal AdditionalGrant { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.PUPIL_FOCUSED_FUNDING)]
    public decimal PupilFocusedFunding { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.PUPIL_PREMIUM)]
    public decimal PupilPremium { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.ESG)]
    public decimal Esg { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.COST_OF_FINANCE)]
    public decimal CostOfFinance { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.FUNDING_MINORITY)]
    public decimal FundingMinority { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.COMMUNITY_EXP)]
    public decimal CommunityExpenditure { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.COMM_FOCUSED_SCHOOL_FACILITIES)]
    public decimal CommFocusedSchoolFacilities { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.CONTRIBUTIONS_TO_VISITS)]
    public decimal ContributionsToVisits { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.TOTAL_EXP)]
    public decimal TotalExpenditure { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.REVENUE_RESERVE)]
    public decimal RevenueReserve { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.NO_PUPILS)]
    public decimal NoPupils { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.NO_TEACHERS)]
    public decimal NoTeachers { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.MEMBER_COUNT)]
    public int SchoolCount { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.PERIOD_COVERED_BY_RETURN)]
    public int PeriodCoveredByReturn { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.PARTIAL_YEARS_PRESENT)]
    public bool PartialYearsPresent { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.SCHOOL_OVERALL_PHASE)]
    public string? OverallPhase { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.SCHOOL_PHASE)]
    public string? Phase { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.SCHOOL_OVERALL_PHASE_BREAKDOWN)]
    public Dictionary<string, int>? OverallPhaseBreakdown { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.DNS)]
    public bool DidNotSubmit { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.MAT_SAT)]
    public string? MatsatCentralServices { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.WORKFORCE_PRESENT)]
    public bool WorkforcePresent { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.ADMISSION_POLICY)]
    public string? AdmissionPolicy { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.GENDER)]
    public string? Gender { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.SCHOOL_TYPE)]
    public string? Type { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.URBAN_RURAL)]
    public string? UrbanRural { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.REGION)]
    public string? Region { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.LONDON_BOROUGH)]
    public string? LondonBorough { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.LONDON_WEIGHT)]
    public string? LondonWeight { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.PERCENTAGE_FSM)]
    public decimal PercentageFsm { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.PERCENTAGE_OF_PUPILS_WITH_SEN)]
    public decimal PercentagePupilsWsen { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.PERCENTAGE_OF_PUPILS_WITHOUT_SEN)]
    public decimal PercentagePupilsWosen { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.PERCENTAGE_OF_PUPILS_WITH_EAL)]
    public decimal PercentagePupilsWeal { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.PERCENTAGE_BOARDERS)]
    public decimal PercentageBoarders { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.PFI)]
    public string? Pfi { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.HAS_6_FORM)]
    public string? Has6Form { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.NUMBER_IN_6_FORM)]
    public decimal NumberIn6Form { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.HIGHEST_AGE_PUPILS)]
    public int HighestAgePupils { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.ADMIN_STAFF)]
    public decimal AdminStaff { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.FULL_TIME_OTHER)]
    public decimal FullTimeOther { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.FULL_TIME_OTHER_HC)]
    public decimal FullTimeOtherHeadCount { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.PERCENTAGE_QUALIFIED_TEACHERS)]
    public decimal PercentageQualifiedTeachers { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.LOWEST_AGE_PUPILS)]
    public int LowestAgePupils { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.FULL_TIME_TA)]
    public decimal FullTimeTa { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.FULL_TIME_TA_HEADCOUNT)]
    public decimal FullTimeTaHeadcount { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.WORKFORCE_TOTAL)]
    public decimal WorkforceTotal { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.TEACHERS_TOTAL)]
    public decimal TeachersTotal { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.NUMBER_TEACHERS_HEADCOUNT)]
    public decimal NumberTeachersHeadcount { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.NUMBER_TEACHERS_IN_LEADERSHIP)]
    public decimal NumberTeachersInLeadershipHeadcount { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.TEACHERS_LEADER)]
    public decimal TeachersLeader { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.AUX_STAFF)]
    public decimal AuxStaff { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.AUX_STAFF_HC)]
    public decimal AuxStaffHeadcount { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.WORKFORCE_HEADCOUNT)]
    public decimal WorkforceHeadcount { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.KS2_ACTUAL)]
    public decimal Ks2Actual { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.KS2_PROGRESS)]
    public decimal Ks2Progress { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.AVERAGE_ATTAINMENT)]
    public decimal AverageAttainment { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.PROGRESS_8_MEASURE)]
    public decimal Progress8Measure { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.PROGRESS_8_BANDING)]
    public decimal Progress8Banding { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.OFSTED_RATING_NAME)]
    public string? OfstedRatingName { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.SPECIFIC_LEARNING_DIFFICULTY)]
    public decimal SpecificLearningDiff { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.MODERATE_LEARNING_DIFFICULTY)]
    public decimal ModerateLearningDiff { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.SEVERE_LEARNING_DIFFICULTY)]
    public decimal SevereLearningDiff { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.PROF_LEARNING_DIFFICULTY)]
    public decimal ProfLearningDiff { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.SOCIAL_HEALTH)]
    public decimal SocialHealth { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.SPEECH_NEEDS)]
    public decimal SpeechNeeds { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.HEARING_IMPAIRMENT)]
    public decimal HearingImpairment { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.VISUAL_IMPAIRMENT)]
    public decimal VisualImpairment { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.MULTI_SENSORY_IMPAIRMENT)]
    public decimal MultiSensoryImpairment { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.PHYSICAL_DISABILITY)]
    public decimal PhysicalDisability { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.AUTISTIC_DISORDER)]
    public decimal AutisticDisorder { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.OTHER_LEARNING_DIFF)]
    public decimal OtherLearningDiff { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.LA)]
    public int La { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.OFSTED_RATING)]
    public decimal OfstedRating { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.IS_PLACEHOLDER)]
    public bool IsPlaceholder { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.RR_TO_INCOME)]
    public decimal RrPerIncomePercentage { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.GRANT_FUNDING_PP)]
    public decimal PerPupilGrantFunding { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.TOTAL_EXP_PP)]
    public decimal PerPupilTotalExpenditure { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.TEACHERS_MAIN_PAY)]
    public decimal PerTeachersOnMainPay { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.TEACHERS_UPPER_LEADING_PAY)]
    public decimal PerTeachersOnUpperOrLeadingPay { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.TEACHERS_LEADERSHIP_PAY)]
    public decimal PerTeachersOnLeadershipPay { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.IS_FEDERATION)]
    public bool IsFederation { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.IS_PART_OF_FEDERATION)]
    public bool IsPartOfFederation { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.FEDERATION_UID)]
    public long FederationUid { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.FEDERATION_NAME)]
    public string? FederationName { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.INTEREST_LOANS_BANKING)]
    public decimal InterestLoansAndBanking { get; set; }

    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.DIRECT_REVENUE_FINANCING)]
    public decimal DirectRevenueFinancing { get; set; }
}
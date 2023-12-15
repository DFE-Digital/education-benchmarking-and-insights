using System.Collections.Generic;
using Newtonsoft.Json;

namespace EducationBenchmarking.Platform.Api.School.Db;

public class SchoolTrustFinancialDataObject
    {
        [JsonProperty(PropertyName = FieldNames.URN)]
        public long? URN{ get; set; }

        [JsonProperty(PropertyName = FieldNames.SCHOOL_NAME)]
        public string SchoolName { get; set; }

        [JsonProperty(PropertyName = FieldNames.FINANCE_TYPE)]
        public string FinanceType { get; set; }

        [JsonProperty(PropertyName = FieldNames.COMPANY_NUMBER)]
        public int? CompanyNumber { get; set; }

        [JsonProperty(PropertyName = FieldNames.UID)]
        public int? UID { get; set; }

        [JsonProperty(PropertyName = FieldNames.TRUST_COMPANY_NAME)]
        public string TrustOrCompanyName { get; set; }

        [JsonProperty(PropertyName = FieldNames.OTHER_INCOME)]
        public decimal? OtherIncome { get; set; }

        [JsonProperty(PropertyName = FieldNames.SEN)]
        public decimal? SEN { get; set; }

        [JsonProperty(PropertyName = FieldNames.OTHER_DFE_GRANTS)]
        public decimal? OtherDfeGrants { get; set; }

        [JsonProperty(PropertyName = FieldNames.OTHER_INCOME_GRANTS)]
        public decimal? OtherIncomeGrants { get; set; }

        [JsonProperty(PropertyName = FieldNames.GOVERNMENT_SOURCE)]
        public decimal? GovernmentSource { get; set; }

        [JsonProperty(PropertyName = FieldNames.ACADEMIES)]
        public decimal? Academies { get; set; }

        [JsonProperty(PropertyName = FieldNames.NON_GOVERNMENT)]
        public decimal? NonGoverntment { get; set; }

        [JsonProperty(PropertyName = FieldNames.INCOME_FROM_FACILITIES)]
        public decimal? IncomeFromFacilities { get; set; }

        [JsonProperty(PropertyName = FieldNames.INCOME_FROM_CATERING)]
        public decimal? IncomeFromCatering { get; set; }

        [JsonProperty(PropertyName = FieldNames.RECEIPTS_FROM_SUPPLY)]
        public decimal? ReceiptsFromSupply { get; set; }

        [JsonProperty(PropertyName = FieldNames.RECEIPTS_FROM_OTHER)]
        public decimal? ReceiptsFromOther { get; set; }

        [JsonProperty(PropertyName = FieldNames.DONATIONS)]
        public decimal? Donations { get; set; }

        [JsonProperty(PropertyName = FieldNames.OTHER_SELF_GENERATED)]
        public decimal? OtherSelfGenerated { get; set; }

        [JsonProperty(PropertyName = FieldNames.INVESTMENT_INCOME)]
        public decimal? Investmentincome { get; set; }

        [JsonProperty(PropertyName = FieldNames.TEACHING_STAFF)]
        public decimal? TeachingStaff { get; set; }

        [JsonProperty(PropertyName = FieldNames.SUPPLY_TEACHING_STAFF)]
        public decimal? SupplyTeachingStaff { get; set; }

        [JsonProperty(PropertyName = FieldNames.EDUCATION_SUPPORT_STAFF)]
        public decimal? EducationSupportStaff { get; set; }

        [JsonProperty(PropertyName = FieldNames.ADMIN_CLERIC_STAFF)]
        public decimal? AdministrativeClericalStaff { get; set; }

        [JsonProperty(PropertyName = FieldNames.PREMISES_STAFF)]
        public decimal? PremisesStaff { get; set; }

        [JsonProperty(PropertyName = FieldNames.CATERING_STAFF)]
        public decimal? CateringStaff { get; set; }

        [JsonProperty(PropertyName = FieldNames.OTHER_STAFF)]
        public decimal? OtherStaff { get; set; }

        [JsonProperty(PropertyName = FieldNames.INDIRECT_EMPLOYEE_EXPENSES)]
        public decimal? IndirectEmployeeExpenses { get; set; }

        [JsonProperty(PropertyName = FieldNames.STAFF_DEV)]
        public decimal? StaffDevelopment { get; set; }

        [JsonProperty(PropertyName = FieldNames.STAFF_INSURANCE)]
        public decimal? StaffInsurance { get; set; }

        [JsonProperty(PropertyName = FieldNames.SUPPLY_TEACHER_INSURANCE)]
        public decimal? SupplyTeacherInsurance { get; set; }

        [JsonProperty(PropertyName = FieldNames.BUILDING_GROUNDS)]
        public decimal? BuildingGroundsMaintenance { get; set; }

        [JsonProperty(PropertyName = FieldNames.CLEANING)]
        public decimal? CleaningCaretaking { get; set; }

        [JsonProperty(PropertyName = FieldNames.WATER_SEWERAGE)]
        public decimal? WaterSewerage { get; set; }

        [JsonProperty(PropertyName = FieldNames.ENERGY)]
        public decimal? Energy { get; set; }

        [JsonProperty(PropertyName = FieldNames.RATES)]
        public decimal? Rates { get; set; }

        [JsonProperty(PropertyName = FieldNames.RENT_RATES)]
        public decimal? RentRates { get; set; }

        [JsonProperty(PropertyName = FieldNames.OTHER_OCCUPATION)]
        public decimal? OtherOccupationCosts { get; set; }

        [JsonProperty(PropertyName = FieldNames.SPECIAL_FACILITIES)]
        public decimal? Specialfacilities { get; set; }

        [JsonProperty(PropertyName = FieldNames.LEARNING_RESOURCES)]
        public decimal? LearningResources { get; set; }

        [JsonProperty(PropertyName = FieldNames.ICT_LEARNING_RESOURCES)]
        public decimal? ICTLearningResources { get; set; }

        [JsonProperty(PropertyName = FieldNames.EXAM_FEES)]
        public decimal? ExaminationFees { get; set; }

        [JsonProperty(PropertyName = FieldNames.EDUCATIONAL_CONSULTANCY)]
        public decimal? EducationalConsultancy { get; set; }

        [JsonProperty(PropertyName = FieldNames.ADMIN_SUPPLIES)]
        public decimal? AdministrativeSupplies { get; set; }

        [JsonProperty(PropertyName = FieldNames.AGENCY_TEACH_STAFF)]
        public decimal? AgencyTeachingStaff { get; set; }

        [JsonProperty(PropertyName = FieldNames.CATERING_SUPPLIES)]
        public decimal? CateringSupplies { get; set; }

        [JsonProperty(PropertyName = FieldNames.OTHER_INSURANCE)]
        public decimal? OtherInsurancePremiums { get; set; }

        [JsonProperty(PropertyName = FieldNames.LEGAL_PROFESSIONAL)]
        public decimal? LegalProfessional { get; set; }

        [JsonProperty(PropertyName = FieldNames.AUDITOR_COSTS)]
        public decimal? AuditorCosts { get; set; }

        [JsonProperty(PropertyName = FieldNames.INTEREST_CHARGES)]
        public decimal? InterestCharges { get; set; }

        [JsonProperty(PropertyName = FieldNames.DIRECT_REVENUE)]
        public decimal? DirectRevenue { get; set; }

        [JsonProperty(PropertyName = FieldNames.PFI_CHARGES)]
        public decimal? PFICharges { get; set; }

        [JsonProperty(PropertyName = FieldNames.IN_YEAR_BALANCE)]
        public decimal? InYearBalance { get; set; }

        [JsonProperty(PropertyName = FieldNames.GRANT_FUNDING)]
        public decimal? GrantFunding { get; set; }

        [JsonProperty(PropertyName = FieldNames.DIRECT_GRANT)]
        public decimal? DirectGrant { get; set; }

        [JsonProperty(PropertyName = FieldNames.COMMUNITY_GRANTS)]
        public decimal? CommunityGrants { get; set; }

        [JsonProperty(PropertyName = FieldNames.TARGETED_GRANTS)]
        public decimal? TargetedGrants { get; set; }

        [JsonProperty(PropertyName = FieldNames.SELF_GENERATED_FUNDING)]
        public decimal? SelfGeneratedFunding { get; set; }

        [JsonProperty(PropertyName = FieldNames.TOTAL_INCOME)]
        public decimal? TotalIncome { get; set; }

        [JsonProperty(PropertyName = FieldNames.SUPPLY_STAFF)]
        public decimal? SupplyStaff { get; set; }

        [JsonProperty(PropertyName = FieldNames.OTHER_STAFF_COSTS)]
        public decimal? OtherStaffCosts { get; set; }

        [JsonProperty(PropertyName = FieldNames.STAFF_TOTAL)]
        public decimal? StaffTotal { get; set; }

        [JsonProperty(PropertyName = FieldNames.MAINTENANCE_IMPROVEMENT)]
        public decimal? MaintenanceImprovement { get; set; }

        [JsonProperty(PropertyName = FieldNames.GROUNDS_MAINTENANCE_IMPROVEMENT)]
        public decimal? GroundsMaintenanceImprovement { get; set; }

        [JsonProperty(PropertyName = FieldNames.BUILDING_MAINTENANCE_IMPROVEMENT)]
        public decimal? BuildingMaintenanceImprovement { get; set; }

        [JsonProperty(PropertyName = FieldNames.PREMISES)]
        public decimal? Premises { get; set; }

        [JsonProperty(PropertyName = FieldNames.CATERING_EXP)]
        public decimal? CateringExp { get; set; }

        [JsonProperty(PropertyName = FieldNames.OCCUPATION)]
        public decimal? Occupation { get; set; }

        [JsonProperty(PropertyName = FieldNames.SUPPLIES_SERVICES)]
        public decimal? SuppliesServices { get; set; }

        [JsonProperty(PropertyName = FieldNames.EDUCATIONAL_SUPPLIES)]
        public decimal? EducationalSupplies { get; set; }

        [JsonProperty(PropertyName = FieldNames.BROUGHT_IN_SERVICES)]
        public decimal? BroughtProfessionalServices { get; set; }

        [JsonProperty(PropertyName = FieldNames.BOUGHT_IN_OTHER)]
        public decimal? BoughtInOther { get; set; }        
        
        [JsonProperty(PropertyName = FieldNames.BOUGHT_IN_NOT_PFI)]
        public decimal? BoughtInNotPFI { get; set; }

        [JsonProperty(PropertyName = FieldNames.COMM_FOCUSED_STAFF)]
        public decimal? CommunityFocusedStaff { get; set; }

        [JsonProperty(PropertyName = FieldNames.COMM_FOCUSED_SCHOOL)]
        public decimal? CommunityFocusedSchoolCosts { get; set; }

        [JsonProperty(PropertyName = FieldNames.PRE_POST_16_FUNDING)]
        public decimal? PrePost16Funding { get; set; }

        [JsonProperty(PropertyName = FieldNames.COMMUNITY_FOCUSED)]
        public decimal? CommunityFocusedFunding { get; set; }

        [JsonProperty(PropertyName = FieldNames.ADDITIONAL_GRANT)]
        public decimal? AdditionalGrant { get; set; }

        [JsonProperty(PropertyName = FieldNames.PUPIL_FOCUSED_FUNDING)]
        public decimal? PupilFocusedFunding { get; set; }

        [JsonProperty(PropertyName = FieldNames.PUPIL_PREMIUM)]
        public decimal? PupilPremium { get; set; }

        [JsonProperty(PropertyName = FieldNames.ESG)]
        public decimal? ESG { get; set; }

        [JsonProperty(PropertyName = FieldNames.COST_OF_FINANCE)]
        public decimal? CostOfFinance { get; set; }

        [JsonProperty(PropertyName = FieldNames.FUNDING_MINORITY)]
        public decimal? FundingMinority { get; set; }

        [JsonProperty(PropertyName = FieldNames.COMMUNITY_EXP)]
        public decimal? CommunityExpenditure { get; set; }
        
        [JsonProperty(PropertyName = FieldNames.COMM_FOCUSED_SCHOOL_FACILITIES)]
        public decimal? CommFocusedSchoolFacilities { get; set; }

        [JsonProperty(PropertyName = FieldNames.CONTRIBUTIONS_TO_VISITS)]
        public decimal? ContributionsToVisits { get; set; }

        [JsonProperty(PropertyName = FieldNames.TOTAL_EXP)]
        public decimal? TotalExpenditure { get; set; }

        [JsonProperty(PropertyName = FieldNames.REVENUE_RESERVE)]
        public decimal? RevenueReserve { get; set; }

        [JsonProperty(PropertyName = FieldNames.NO_PUPILS)]
        public decimal? NoPupils { get; set; }

        [JsonProperty(PropertyName = FieldNames.NO_TEACHERS)]
        public decimal? NoTeachers { get; set; }

        [JsonProperty(PropertyName = FieldNames.MEMBER_COUNT)]
        public int? SchoolCount { get; set; }

        [JsonProperty(PropertyName = FieldNames.PERIOD_COVERED_BY_RETURN)]
        public int? PeriodCoveredByReturn { get; set; }

        [JsonProperty(PropertyName = FieldNames.PARTIAL_YEARS_PRESENT)]
        public bool? PartialYearsPresent { get; set; }

        [JsonProperty(PropertyName = FieldNames.SCHOOL_OVERALL_PHASE)]
        public string OverallPhase { get; set; }

        [JsonProperty(PropertyName = FieldNames.SCHOOL_PHASE)]
        public string Phase { get; set; }

        [JsonProperty(PropertyName = FieldNames.SCHOOL_OVERALL_PHASE_BREAKDOWN)]
        public Dictionary<string, int> OverallPhaseBreakdown { get; set; }

        [JsonProperty(PropertyName = FieldNames.DNS)]
        public bool DidNotSubmit { get; set; }

        [JsonProperty(PropertyName = FieldNames.MAT_SAT)]
        public string MATSATCentralServices { get; set; }

        [JsonProperty(PropertyName = FieldNames.WORKFORCE_PRESENT)]
        public bool WorkforcePresent { get; set; }

        [JsonProperty(PropertyName = FieldNames.ADMISSION_POLICY)]
        public string AdmissionPolicy { get; set; }

        [JsonProperty(PropertyName = FieldNames.GENDER)]
        public string Gender { get; set; }

        [JsonProperty(PropertyName = FieldNames.SCHOOL_TYPE)]
        public string Type { get; set; }

        [JsonProperty(PropertyName = FieldNames.URBAN_RURAL)]
        public string UrbanRural { get; set; }

        [JsonProperty(PropertyName = FieldNames.REGION)]
        public string Region { get; set; }

        [JsonProperty(PropertyName = FieldNames.LONDON_BOROUGH)]
        public string LondonBorough { get; set; }

        [JsonProperty(PropertyName = FieldNames.LONDON_WEIGHT)]
        public string LondonWeight { get; set; }

        [JsonProperty(PropertyName = FieldNames.PERCENTAGE_FSM)]
        public decimal? PercentageFSM { get; set; }

        [JsonProperty(PropertyName = FieldNames.PERCENTAGE_OF_PUPILS_WITH_SEN)]
        public decimal? PercentagePupilsWSEN { get; set; }

        [JsonProperty(PropertyName = FieldNames.PERCENTAGE_OF_PUPILS_WITHOUT_SEN)]
        public decimal? PercentagePupilsWOSEN { get; set; }

        [JsonProperty(PropertyName = FieldNames.PERCENTAGE_OF_PUPILS_WITH_EAL)]
        public decimal? PercentagePupilsWEAL { get; set; }

        [JsonProperty(PropertyName = FieldNames.PERCENTAGE_BOARDERS)]
        public decimal? PercentageBoarders { get; set; }

        [JsonProperty(PropertyName = FieldNames.PFI)]
        public string PFI { get; set; }

        [JsonProperty(PropertyName = FieldNames.HAS_6_FORM)]
        public string Has6Form { get; set; }

        [JsonProperty(PropertyName = FieldNames.NUMBER_IN_6_FORM)]
        public decimal? NumberIn6Form { get; set; }

        [JsonProperty(PropertyName = FieldNames.HIGHEST_AGE_PUPILS)]
        public int? HighestAgePupils { get; set; }

        [JsonProperty(PropertyName = FieldNames.ADMIN_STAFF)]
        public decimal? AdminStaff { get; set; }

        [JsonProperty(PropertyName = FieldNames.FULL_TIME_OTHER)]
        public decimal? FullTimeOther { get; set; }

        [JsonProperty(PropertyName = FieldNames.FULL_TIME_OTHER_HC)]
        public decimal? FullTimeOtherHeadCount { get; set; }

        [JsonProperty(PropertyName = FieldNames.PERCENTAGE_QUALIFIED_TEACHERS)]
        public decimal? PercentageQualifiedTeachers { get; set; }

        [JsonProperty(PropertyName = FieldNames.LOWEST_AGE_PUPILS)]
        public int? LowestAgePupils { get; set; }

        [JsonProperty(PropertyName = FieldNames.FULL_TIME_TA)]
        public decimal? FullTimeTA { get; set; }

        [JsonProperty(PropertyName = FieldNames.FULL_TIME_TA_HEADCOUNT)]
        public decimal? FullTimeTAHeadcount { get; set; }

        [JsonProperty(PropertyName = FieldNames.WORKFORCE_TOTAL)]
        public decimal? WorkforceTotal { get; set; }

        [JsonProperty(PropertyName = FieldNames.TEACHERS_TOTAL)]
        public decimal? TeachersTotal { get; set; }

        [JsonProperty(PropertyName = FieldNames.NUMBER_TEACHERS_HEADCOUNT)]
        public decimal? NumberTeachersHeadcount { get; set; }

        [JsonProperty(PropertyName = FieldNames.NUMBER_TEACHERS_IN_LEADERSHIP)]
        public decimal? NumberTeachersInLeadershipHeadcount { get; set; }

        [JsonProperty(PropertyName = FieldNames.TEACHERS_LEADER)]
        public decimal? TeachersLeader { get; set; }

        [JsonProperty(PropertyName = FieldNames.AUX_STAFF)]
        public decimal? AuxStaff { get; set; }

        [JsonProperty(PropertyName = FieldNames.AUX_STAFF_HC)]
        public decimal? AuxStaffHeadcount { get; set; }

        [JsonProperty(PropertyName = FieldNames.WORKFORCE_HEADCOUNT)]
        public decimal? WorkforceHeadcount { get; set; }

        [JsonProperty(PropertyName = FieldNames.KS2_ACTUAL)]
        public decimal? Ks2Actual { get; set; }

        [JsonProperty(PropertyName = FieldNames.KS2_PROGRESS)]
        public decimal? Ks2Progress { get; set; }

        [JsonProperty(PropertyName = FieldNames.AVERAGE_ATTAINMENT)]
        public decimal? AverageAttainment { get; set; }

        [JsonProperty(PropertyName = FieldNames.PROGRESS_8_MEASURE)]
        public decimal? Progress8Measure { get; set; }

        [JsonProperty(PropertyName = FieldNames.PROGRESS_8_BANDING)]
        public decimal? Progress8Banding { get; set; }

        [JsonProperty(PropertyName = FieldNames.OFSTED_RATING_NAME)]
        public string OfstedRatingName { get; set; }

        [JsonProperty(PropertyName = FieldNames.SPECIFIC_LEARNING_DIFFICULTY)]
        public decimal? SpecificLearningDiff { get; set; }

        [JsonProperty(PropertyName = FieldNames.MODERATE_LEARNING_DIFFICULTY)]
        public decimal? ModerateLearningDiff { get; set; }

        [JsonProperty(PropertyName = FieldNames.SEVERE_LEARNING_DIFFICULTY)]
        public decimal? SevereLearningDiff { get; set; }

        [JsonProperty(PropertyName = FieldNames.PROF_LEARNING_DIFFICULTY)]
        public decimal? ProfLearningDiff { get; set; }

        [JsonProperty(PropertyName = FieldNames.SOCIAL_HEALTH)]
        public decimal? SocialHealth { get; set; }

        [JsonProperty(PropertyName = FieldNames.SPEECH_NEEDS)]
        public decimal? SpeechNeeds { get; set; }

        [JsonProperty(PropertyName = FieldNames.HEARING_IMPAIRMENT)]
        public decimal? HearingImpairment { get; set; }

        [JsonProperty(PropertyName = FieldNames.VISUAL_IMPAIRMENT)]
        public decimal? VisualImpairment { get; set; }

        [JsonProperty(PropertyName = FieldNames.MULTI_SENSORY_IMPAIRMENT)]
        public decimal? MultiSensoryImpairment { get; set; }

        [JsonProperty(PropertyName = FieldNames.PHYSICAL_DISABILITY)]
        public decimal? PhysicalDisability { get; set; }

        [JsonProperty(PropertyName = FieldNames.AUTISTIC_DISORDER)]
        public decimal? AutisticDisorder { get; set; }

        [JsonProperty(PropertyName = FieldNames.OTHER_LEARNING_DIFF)]
        public decimal? OtherLearningDiff { get; set; }

        [JsonProperty(PropertyName = FieldNames.LA)]
        public int? LA  { get; set; }

        [JsonProperty(PropertyName = FieldNames.OFSTED_RATING)]
        public decimal? OfstedRating { get; set; }

        [JsonProperty(PropertyName = FieldNames.IS_PLACEHOLDER)]
        public bool IsPlaceholder { get; set; }

        [JsonProperty(PropertyName = FieldNames.RR_TO_INCOME)]
        public decimal? RRPerIncomePercentage { get; set; }

        [JsonProperty(PropertyName = FieldNames.GRANT_FUNDING_PP)]
        public decimal? PerPupilGrantFunding { get; set; }

        [JsonProperty(PropertyName = FieldNames.TOTAL_EXP_PP)]
        public decimal? PerPupilTotalExpenditure { get; set; }

        [JsonProperty(PropertyName = FieldNames.TEACHERS_MAIN_PAY)]
        public decimal? PerTeachersOnMainPay { get; set; }

        [JsonProperty(PropertyName = FieldNames.TEACHERS_UPPER_LEADING_PAY)]
        public decimal? PerTeachersOnUpperOrLeadingPay { get; set; }

        [JsonProperty(PropertyName = FieldNames.TEACHERS_LEADERSHIP_PAY)]
        public decimal? PerTeachersOnLeadershipPay { get; set; }

        [JsonProperty(PropertyName = FieldNames.IS_FEDERATION)]
        public bool IsFederation { get; set; }

        [JsonProperty(PropertyName = FieldNames.IS_PART_OF_FEDERATION)]
        public bool IsPartOfFederation { get; set; }

        [JsonProperty(PropertyName = FieldNames.FEDERATION_UID)]
        public long? FederationUid { get; set; }

        [JsonProperty(PropertyName = FieldNames.FEDERATION_NAME)]
        public string FederationName { get; set; }

        [JsonProperty(PropertyName = FieldNames.INTEREST_LOANS_BANKING)]
        public decimal? InterestLoansAndBanking { get; set; }

        [JsonProperty(PropertyName = FieldNames.DIRECT_REVENUE_FINANCING)]
        public decimal? DirectRevenueFinancing { get; set; }
    }
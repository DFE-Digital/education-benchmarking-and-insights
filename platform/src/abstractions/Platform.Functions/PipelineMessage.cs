using System.Diagnostics.CodeAnalysis;
using JsonSubTypes;
using Newtonsoft.Json;

namespace Platform.Functions;

[ExcludeFromCodeCoverage]
public record PipelineFinishMessage
{
    public string? JobId { get; set; }
    public string? RunId { get; set; }
    public bool Success { get; set; }
}

[ExcludeFromCodeCoverage]
public record PipelineStartMessage
{
    public string? JobId { get; set; } = Guid.NewGuid().ToString();
    public string? Type { get; set; } // Pipeline job type : default / comparator-set / custom-data
    public string? RunType { get; set; }  // Data context : default / custom
    public string? RunId { get; set; } // year or id for comparator-set / custom-data
    public int? Year { get; set; } // Needed for when custom data or comparator set
    public string? URN { get; set; }
    public Payload? Payload { get; set; } // null for default
}

[JsonConverter(typeof(JsonSubtypes), "Kind")]
public abstract record Payload
{
    [JsonProperty("Kind")]
    public virtual string? Kind { get; }
};

public record ComparatorSetPayload : Payload
{
    public override string Kind => nameof(ComparatorSetPayload);
    public string[] Set { get; set; } = [];
}

public record CustomDataPayload : Payload
{
    public override string Kind => nameof(CustomDataPayload);

    public decimal? AdministrativeSuppliesNonEducationalCosts { get; set; }
    public decimal? CateringStaffCosts { get; set; }
    public decimal? CateringSuppliesCosts { get; set; }
    public decimal? ExaminationFeesCosts { get; set; }
    public decimal? LearningResourcesNonIctCosts { get; set; }
    public decimal? LearningResourcesIctCosts { get; set; }
    public decimal? AdministrativeClericalStaffCosts { get; set; }
    public decimal? AuditorsCosts { get; set; }
    public decimal? OtherStaffCosts { get; set; }
    public decimal? ProfessionalServicesNonCurriculumCosts { get; set; }
    public decimal? CleaningCaretakingCosts { get; set; }
    public decimal? MaintenancePremisesCosts { get; set; }
    public decimal? OtherOccupationCosts { get; set; }
    public decimal? PremisesStaffCosts { get; set; }
    public decimal? AgencySupplyTeachingStaffCosts { get; set; }
    public decimal? EducationSupportStaffCosts { get; set; }
    public decimal? EducationalConsultancyCosts { get; set; }
    public decimal? SupplyTeachingStaffCosts { get; set; }
    public decimal? TeachingStaffCosts { get; set; }
    public decimal? EnergyCosts { get; set; }
    public decimal? WaterSewerageCosts { get; set; }
    public decimal? DirectRevenueFinancingCosts { get; set; }
    public decimal? GroundsMaintenanceCosts { get; set; }
    public decimal? IndirectEmployeeExpenses { get; set; }
    public decimal? InterestChargesLoanBank { get; set; }
    public decimal? OtherInsurancePremiumsCosts { get; set; }
    public decimal? PrivateFinanceInitiativeCharges { get; set; }
    public decimal? RentRatesCosts { get; set; }
    public decimal? SpecialFacilitiesCosts { get; set; }
    public decimal? StaffDevelopmentTrainingCosts { get; set; }
    public decimal? StaffRelatedInsuranceCosts { get; set; }
    public decimal? SupplyTeacherInsurableCosts { get; set; }
    public decimal? TotalIncome { get; set; }
    public decimal? RevenueReserve { get; set; }
    public decimal? TotalPupils { get; set; }
    public decimal? PercentFreeSchoolMeals { get; set; }
    public decimal? PercentSpecialEducationNeeds { get; set; }
    public decimal? TotalInternalFloorArea { get; set; }
    public decimal? WorkforceFTE { get; set; }
    public decimal? TeachersFTE { get; set; }
    public decimal? PercentTeacherWithQualifiedStatus { get; set; }
    public decimal? SeniorLeadershipFTE { get; set; }
    public decimal? TeachingAssistantFTE { get; set; }
    public decimal? NonClassroomSupportStaffFTE { get; set; }
    public decimal? AuxiliaryStaffFTE { get; set; }
    public decimal? WorkforceHeadcount { get; set; }
}
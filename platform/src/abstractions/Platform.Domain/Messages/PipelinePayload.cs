using System.Diagnostics.CodeAnalysis;
using JsonSubTypes;
using Newtonsoft.Json;

// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace Platform.Domain.Messages;

[ExcludeFromCodeCoverage]
[JsonConverter(typeof(JsonSubtypes), "_type")]
public record PipelinePayload
{
    /// <summary>
    ///     Used to discriminate type during deserialization (not used by data pipeline).
    /// </summary>
    [JsonProperty("_type")]
    public virtual string? Type { get; }

    /// <summary>
    ///     Data pipeline has a dependency on <see cref="Pipeline.PayloadKind">specific values</see> for `kind`.
    /// </summary>
    [JsonProperty(nameof(Kind))]
    public virtual string? Kind { get; }
}

[ExcludeFromCodeCoverage]
public record ComparatorSetPipelinePayload : PipelinePayload
{
    public override string Type => nameof(ComparatorSetPipelinePayload);
    public override string Kind => Pipeline.PayloadKind.ComparatorSetPayload;

    public string[] Set { get; set; } = [];
}

[ExcludeFromCodeCoverage]
public record CustomDataPipelinePayload : PipelinePayload
{
    public override string Type => nameof(CustomDataPipelinePayload);
    public override string Kind => Pipeline.PayloadKind.CustomDataPayload;

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
    public decimal? TotalExpenditure { get; set; }
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
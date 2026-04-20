using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Platform.Domain;

namespace Platform.Api.School.Features.Comparators.Models;

[ExcludeFromCodeCoverage]
/// <summary>
/// Represents the criteria used to identify similar schools for benchmarking.
/// </summary>
public record ComparatorsRequest
{
    /// <summary>
    /// The finance types to include (e.g., Academy, Maintained).
    /// </summary>
    public CharacteristicList? FinanceType { get; set; }
    /// <summary>
    /// The educational phases to include (e.g., Primary, Secondary).
    /// </summary>
    public CharacteristicList? OverallPhase { get; set; }
    /// <summary>
    /// The Local Authority names to filter by.
    /// </summary>
    public CharacteristicList? LAName { get; set; }
    /// <summary>
    /// The school positions or regions.
    /// </summary>
    public CharacteristicList? SchoolPosition { get; set; }
    /// <summary>
    /// Whether to include Private Finance Initiative (PFI) schools.
    /// </summary>
    public CharacteristicValueBool? IsPFISchool { get; set; }
    /// <summary>
    /// The London Weighting categories.
    /// </summary>
    public CharacteristicList? LondonWeighting { get; set; }
    /// <summary>
    /// The Ofsted rating descriptions.
    /// </summary>
    public CharacteristicList? OfstedDescription { get; set; }
    /// <summary>
    /// The range for the total number of pupils.
    /// </summary>
    public CharacteristicRange? TotalPupils { get; set; }
    /// <summary>
    /// The range for the average age of the school buildings.
    /// </summary>
    public CharacteristicRange? BuildingAverageAge { get; set; }
    /// <summary>
    /// The range for the total internal floor area.
    /// </summary>
    public CharacteristicRange? TotalInternalFloorArea { get; set; }
    /// <summary>
    /// The range for the percentage of pupils eligible for Free School Meals.
    /// </summary>
    public CharacteristicRange? PercentFreeSchoolMeals { get; set; }
    /// <summary>
    /// The range for the percentage of pupils with Special Educational Needs.
    /// </summary>
    public CharacteristicRange? PercentSpecialEducationNeeds { get; set; }
    /// <summary>
    /// The range for the total number of pupils in the sixth form.
    /// </summary>
    public CharacteristicRange? TotalPupilsSixthForm { get; set; }
    /// <summary>
    /// The range for KS2 progress scores.
    /// </summary>
    public CharacteristicRange? KS2Progress { get; set; }
    /// <summary>
    /// The range for KS4 progress scores.
    /// </summary>
    public CharacteristicRange? KS4Progress { get; set; }
    /// <summary>
    /// The range for the number of schools in the trust.
    /// </summary>
    public CharacteristicRange? SchoolsInTrust { get; set; }
    /// <summary>
    /// The range for the percentage of pupils with Visual Impairment.
    /// </summary>
    public CharacteristicRange? PercentWithVI { get; set; }
    /// <summary>
    /// The range for the percentage of pupils with Specific Learning Difficulty.
    /// </summary>
    public CharacteristicRange? PercentWithSPLD { get; set; }
    /// <summary>
    /// The range for the percentage of pupils with Severe Learning Difficulty.
    /// </summary>
    public CharacteristicRange? PercentWithSLD { get; set; }
    /// <summary>
    /// The range for the percentage of pupils with Speech, Language and Communication Needs.
    /// </summary>
    public CharacteristicRange? PercentWithSLCN { get; set; }
    /// <summary>
    /// The range for the percentage of pupils with Social, Emotional and Mental Health needs.
    /// </summary>
    public CharacteristicRange? PercentWithSEMH { get; set; }
    /// <summary>
    /// The range for the percentage of pupils with Profound and Multiple Learning Difficulty.
    /// </summary>
    public CharacteristicRange? PercentWithPMLD { get; set; }
    /// <summary>
    /// The range for the percentage of pupils with Physical Disability.
    /// </summary>
    public CharacteristicRange? PercentWithPD { get; set; }
    /// <summary>
    /// The range for the percentage of pupils with other SEN needs.
    /// </summary>
    public CharacteristicRange? PercentWithOTH { get; set; }
    /// <summary>
    /// The range for the percentage of pupils with Multi-Sensory Impairment.
    /// </summary>
    public CharacteristicRange? PercentWithMSI { get; set; }
    /// <summary>
    /// The range for the percentage of pupils with Moderate Learning Difficulty.
    /// </summary>
    public CharacteristicRange? PercentWithMLD { get; set; }
    /// <summary>
    /// The range for the percentage of pupils with Hearing Impairment.
    /// </summary>
    public CharacteristicRange? PercentWithHI { get; set; }
    /// <summary>
    /// The range for the percentage of pupils with Autistic Spectrum Disorder.
    /// </summary>
    public CharacteristicRange? PercentWithASD { get; set; }

    public string FilterExpression(string urn) => new List<string>()
        .NotValueFilter("URN", urn)
        .NotNullValueFilter("PeriodCoveredByReturn")
        .RangeFilter(nameof(TotalPupils), TotalPupils)
        .RangeFilter(nameof(BuildingAverageAge), BuildingAverageAge)
        .RangeFilter(nameof(TotalInternalFloorArea), TotalInternalFloorArea)
        .RangeFilter(nameof(TotalPupilsSixthForm), TotalPupilsSixthForm)
        .RangeFilter(nameof(PercentFreeSchoolMeals), PercentFreeSchoolMeals)
        .RangeFilter(nameof(PercentSpecialEducationNeeds), PercentSpecialEducationNeeds)
        .RangeFilter(nameof(KS2Progress), KS2Progress)
        .RangeFilter(nameof(KS4Progress), KS4Progress)
        .RangeFilter(nameof(SchoolsInTrust), SchoolsInTrust)
        .RangeFilter(nameof(IsPFISchool), IsPFISchool)
        .RangeFilter(nameof(PercentWithVI), PercentWithVI)
        .RangeFilter(nameof(PercentWithSPLD), PercentWithSPLD)
        .RangeFilter(nameof(PercentWithSLD), PercentWithSLD)
        .RangeFilter(nameof(PercentWithSLCN), PercentWithSLCN)
        .RangeFilter(nameof(PercentWithSEMH), PercentWithSEMH)
        .RangeFilter(nameof(PercentWithPMLD), PercentWithPMLD)
        .RangeFilter(nameof(PercentWithPD), PercentWithPD)
        .RangeFilter(nameof(PercentWithOTH), PercentWithOTH)
        .RangeFilter(nameof(PercentWithMSI), PercentWithMSI)
        .RangeFilter(nameof(PercentWithMLD), PercentWithMLD)
        .RangeFilter(nameof(PercentWithHI), PercentWithHI)
        .RangeFilter(nameof(PercentWithASD), PercentWithASD)
        .ValuesFilter(nameof(FinanceType), FinanceType)
        .ValuesFilter(nameof(OverallPhase), OverallPhase)
        .ValuesFilter(nameof(LAName), LAName)
        .ValuesFilter(nameof(SchoolPosition), SchoolPosition)
        .ValuesFilter(nameof(LondonWeighting), LondonWeighting)
        .ValuesFilter(nameof(OfstedDescription), OfstedDescription)
        .BuildFilter();
}
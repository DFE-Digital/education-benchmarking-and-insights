using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.School.Features.Comparators.Models;

[ExcludeFromCodeCoverage]
/// <summary>
/// Represents a school and its characteristics for similarity comparison.
/// </summary>
public record ComparatorDto
{
    /// <summary>
    /// The 6-digit Unique Reference Number of the school.
    /// </summary>
    public string? URN { get; set; }
    /// <summary>
    /// The finance type of the school (e.g., Academy, Maintained).
    /// </summary>
    public string? FinanceType { get; set; }
    /// <summary>
    /// The educational phase of the school (e.g., Primary, Secondary).
    /// </summary>
    public string? OverallPhase { get; set; }
    /// <summary>
    /// The name of the Local Authority where the school is located.
    /// </summary>
    public string? LAName { get; set; }
    /// <summary>
    /// The total number of pupils at the school.
    /// </summary>
    public double? TotalPupils { get; set; }
    /// <summary>
    /// The percentage of pupils eligible for Free School Meals.
    /// </summary>
    public double? PercentFreeSchoolMeals { get; set; }
    /// <summary>
    /// The percentage of pupils with Special Educational Needs.
    /// </summary>
    public double? PercentSpecialEducationNeeds { get; set; }
    /// <summary>
    /// The London Weighting category for the school.
    /// </summary>
    public string? LondonWeighting { get; set; }
    /// <summary>
    /// The average age of the school buildings.
    /// </summary>
    public double? BuildingAverageAge { get; set; }
    /// <summary>
    /// The total internal floor area of the school.
    /// </summary>
    public double? TotalInternalFloorArea { get; set; }
    /// <summary>
    /// The Ofsted rating description for the school.
    /// </summary>
    public string? OfstedDescription { get; set; }
    /// <summary>
    /// The number of schools in the trust (if applicable).
    /// </summary>
    public int? SchoolsInTrust { get; set; }
    /// <summary>
    /// Whether the school is a Private Finance Initiative (PFI) school.
    /// </summary>
    public bool? IsPFISchool { get; set; }
    /// <summary>
    /// The total number of pupils in the sixth form (if applicable).
    /// </summary>
    public double? TotalPupilsSixthForm { get; set; }
    /// <summary>
    /// The KS2 progress score for the school.
    /// </summary>
    public double? KS2Progress { get; set; }
    /// <summary>
    /// The KS4 progress score for the school.
    /// </summary>
    public double? KS4Progress { get; set; }
    /// <summary>
    /// The percentage of pupils with Visual Impairment.
    /// </summary>
    public double? PercentWithVI { get; set; }
    /// <summary>
    /// The percentage of pupils with Specific Learning Difficulty.
    /// </summary>
    public double? PercentWithSPLD { get; set; }
    /// <summary>
    /// The percentage of pupils with Severe Learning Difficulty.
    /// </summary>
    public double? PercentWithSLD { get; set; }
    /// <summary>
    /// The percentage of pupils with Speech, Language and Communication Needs.
    /// </summary>
    public double? PercentWithSLCN { get; set; }
    /// <summary>
    /// The percentage of pupils with Social, Emotional and Mental Health needs.
    /// </summary>
    public double? PercentWithSEMH { get; set; }
    /// <summary>
    /// The percentage of pupils with Profound and Multiple Learning Difficulty.
    /// </summary>
    public double? PercentWithPMLD { get; set; }
    /// <summary>
    /// The percentage of pupils with Physical Disability.
    /// </summary>
    public double? PercentWithPD { get; set; }
    /// <summary>
    /// The percentage of pupils with other SEN needs.
    /// </summary>
    public double? PercentWithOTH { get; set; }
    /// <summary>
    /// The percentage of pupils with Multi-Sensory Impairment.
    /// </summary>
    public double? PercentWithMSI { get; set; }
    /// <summary>
    /// The percentage of pupils with Moderate Learning Difficulty.
    /// </summary>
    public double? PercentWithMLD { get; set; }
    /// <summary>
    /// The percentage of pupils with Hearing Impairment.
    /// </summary>
    public double? PercentWithHI { get; set; }
    /// <summary>
    /// The percentage of pupils with Autistic Spectrum Disorder.
    /// </summary>
    public double? PercentWithASD { get; set; }
    /// <summary>
    /// The school position or region.
    /// </summary>
    public string? SchoolPosition { get; set; }
}
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Platform.Api.School.Features.Details.Models;

[ExcludeFromCodeCoverage]
public record SchoolCharacteristicResponse
{
    /// <summary>
    /// The unique reference number for the school.
    /// </summary>
    public string? URN { get; set; }
    
    /// <summary>
    /// The name of the school.
    /// </summary>
    public string? SchoolName { get; set; }
    
    /// <summary>
    /// The town of the school's address.
    /// </summary>
    public string? AddressTown { get; set; }
    
    /// <summary>
    /// The postcode of the school's address.
    /// </summary>
    public string? AddressPostcode { get; set; }
    
    /// <summary>
    /// The finance type of the school (e.g., Academy, Maintained).
    /// </summary>
    public string? FinanceType { get; set; }
    
    /// <summary>
    /// The overall phase of education (e.g., Primary, Secondary).
    /// </summary>
    public string? OverallPhase { get; set; }
    
    /// <summary>
    /// The name of the local authority.
    /// </summary>
    public string? LAName { get; set; }
    
    /// <summary>
    /// The total number of pupils enrolled in the school.
    /// </summary>
    public decimal? TotalPupils { get; set; }
    
    /// <summary>
    /// The percentage of pupils eligible for free school meals.
    /// </summary>
    public decimal? PercentFreeSchoolMeals { get; set; }
    
    /// <summary>
    /// The percentage of pupils with special educational needs.
    /// </summary>
    public decimal? PercentSpecialEducationNeeds { get; set; }
    
    /// <summary>
    /// The London weighting area of the school.
    /// </summary>
    public string? LondonWeighting { get; set; }
    
    /// <summary>
    /// The average age of the school buildings.
    /// </summary>
    public decimal? BuildingAverageAge { get; set; }
    
    /// <summary>
    /// The total internal floor area of the school buildings in square meters.
    /// </summary>
    public decimal? TotalInternalFloorArea { get; set; }
    
    /// <summary>
    /// The description of the school's Ofsted rating.
    /// </summary>
    public string? OfstedDescription { get; set; }
    
    /// <summary>
    /// The number of schools in the trust the school belongs to (if applicable).
    /// </summary>
    public int? SchoolsInTrust { get; set; }
    
    /// <summary>
    /// Indicates whether the school is a Private Finance Initiative (PFI) school.
    /// </summary>
    public bool? IsPFISchool { get; set; }
    
    /// <summary>
    /// The total number of pupils in the school's sixth form.
    /// </summary>
    public decimal? TotalPupilsSixthForm { get; set; }
    
    /// <summary>
    /// The school's Key Stage 2 progress score.
    /// </summary>
    public decimal? KS2Progress { get; set; }
    
    /// <summary>
    /// The school's Key Stage 4 progress score.
    /// </summary>
    public decimal? KS4Progress { get; set; }
    
    /// <summary>
    /// The school's Key Stage 4 progress banding.
    /// </summary>
    public string? KS4ProgressBanding { get; set; }
    
    /// <summary>
    /// Percentage of pupils with Visual Impairment (VI).
    /// </summary>
    public decimal? PercentWithVI { get; set; }
    
    /// <summary>
    /// Percentage of pupils with Specific Learning Difficulty (SpLD).
    /// </summary>
    public decimal? PercentWithSPLD { get; set; }
    
    /// <summary>
    /// Percentage of pupils with Severe Learning Difficulty (SLD).
    /// </summary>
    public decimal? PercentWithSLD { get; set; }
    
    /// <summary>
    /// Percentage of pupils with Speech, Language and Communication Needs (SLCN).
    /// </summary>
    public decimal? PercentWithSLCN { get; set; }
    
    /// <summary>
    /// Percentage of pupils with Social, Emotional and Mental Health (SEMH).
    /// </summary>
    public decimal? PercentWithSEMH { get; set; }
    
    /// <summary>
    /// Percentage of pupils with Profound and Multiple Learning Difficulty (PMLD).
    /// </summary>
    public decimal? PercentWithPMLD { get; set; }
    
    /// <summary>
    /// Percentage of pupils with Physical Disability (PD).
    /// </summary>
    public decimal? PercentWithPD { get; set; }
    
    /// <summary>
    /// Percentage of pupils with other special educational needs (OTH).
    /// </summary>
    public decimal? PercentWithOTH { get; set; }
    
    /// <summary>
    /// Percentage of pupils with Multi-Sensory Impairment (MSI).
    /// </summary>
    public decimal? PercentWithMSI { get; set; }
    
    /// <summary>
    /// Percentage of pupils with Moderate Learning Difficulty (MLD).
    /// </summary>
    public decimal? PercentWithMLD { get; set; }
    
    /// <summary>
    /// Percentage of pupils with Hearing Impairment (HI).
    /// </summary>
    public decimal? PercentWithHI { get; set; }
    
    /// <summary>
    /// Percentage of pupils with Autistic Spectrum Disorder (ASD).
    /// </summary>
    public decimal? PercentWithASD { get; set; }

    /// <summary>
    /// The position of the school relative to others (if applicable).
    /// </summary>
    public string? SchoolPosition { get; set; }
    
    /// <summary>
    /// The period covered by the financial return in months.
    /// </summary>
    public int? PeriodCoveredByReturn { get; set; }

    /// <summary>
    /// The formatted address of the school.
    /// </summary>
    public string Address => string.Join(", ", new List<string?>
    {
        AddressTown,
        AddressPostcode
    }.Where(x => !string.IsNullOrEmpty(x)));
}
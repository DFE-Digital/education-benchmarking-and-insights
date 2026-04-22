// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UnusedMember.Global

using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.LocalAuthority.Features.EducationHealthCarePlans.Models;

/// <summary>
/// Represents the education, health and care plans data for a local authority.
/// </summary>
[ExcludeFromCodeCoverage]
public record EducationHealthCarePlansResponse
{
    /// <summary>
    /// The three-digit local authority code.
    /// </summary>
    public string? Code { get; set; }

    /// <summary>
    /// The name of the local authority.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// The population aged 2 to 18 in the local authority.
    /// </summary>
    public double? Population2To18 { get; set; }

    /// <summary>
    /// The total number of pupils in the local authority.
    /// </summary>
    public decimal? TotalPupils { get; set; }

    /// <summary>
    /// The total number or percentage of education, health and care plans.
    /// </summary>
    public decimal? Total { get; set; }

    /// <summary>
    /// The number or percentage of plans in mainstream schools.
    /// </summary>
    public decimal? Mainstream { get; set; }

    /// <summary>
    /// The number or percentage of plans in resourced provision.
    /// </summary>
    public decimal? Resourced { get; set; }

    /// <summary>
    /// The number or percentage of plans in special schools.
    /// </summary>
    public decimal? Special { get; set; }

    /// <summary>
    /// The number or percentage of plans in independent schools.
    /// </summary>
    public decimal? Independent { get; set; }

    /// <summary>
    /// The number or percentage of plans in hospital schools.
    /// </summary>
    public decimal? Hospital { get; set; }

    /// <summary>
    /// The number or percentage of plans in post-16 settings.
    /// </summary>
    public decimal? Post16 { get; set; }

    /// <summary>
    /// The number or percentage of plans in other settings.
    /// </summary>
    public decimal? Other { get; set; }
}

/// <summary>
/// Represents the education, health and care plans data for a specific year and local authority.
/// </summary>
[ExcludeFromCodeCoverage]
public record EducationHealthCarePlansYearResponse : EducationHealthCarePlansResponse
{
    /// <summary>
    /// The academic or financial year.
    /// </summary>
    public int? Year { get; set; }
}

/// <summary>
/// Represents historical education, health and care plans data over a range of years.
/// </summary>
[ExcludeFromCodeCoverage]
public record EducationHealthCarePlansYearHistory
{
    /// <summary>
    /// The start year of the history range.
    /// </summary>
    public int? StartYear { get; set; }

    /// <summary>
    /// The end year of the history range.
    /// </summary>
    public int? EndYear { get; set; }

    /// <summary>
    /// The historical plans data.
    /// </summary>
    public EducationHealthCarePlansYearResponse[]? Plans { get; set; } = [];
}
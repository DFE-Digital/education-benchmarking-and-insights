// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UnusedMember.Global

using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.LocalAuthority.Features.EducationHealthCarePlans.Models;

[ExcludeFromCodeCoverage]
public record EducationHealthCarePlansResponse
{
    public string? Code { get; set; }
    public string? Name { get; set; }
    public double? Population2To18 { get; set; }
    public decimal? TotalPupils { get; set; }
    public decimal? Total { get; set; }
    public decimal? Mainstream { get; set; }
    public decimal? Resourced { get; set; }
    public decimal? Special { get; set; }
    public decimal? Independent { get; set; }
    public decimal? Hospital { get; set; }
    public decimal? Post16 { get; set; }
    public decimal? Other { get; set; }
}

[ExcludeFromCodeCoverage]
public record EducationHealthCarePlansYearResponse : EducationHealthCarePlansResponse
{
    public int? Year { get; set; }
}

[ExcludeFromCodeCoverage]
public record EducationHealthCarePlansYearHistory
{
    public int? StartYear { get; set; }
    public int? EndYear { get; set; }
    public EducationHealthCarePlansYearResponse[]? Plans { get; set; } = [];
}
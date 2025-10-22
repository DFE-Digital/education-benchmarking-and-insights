using System.Diagnostics.CodeAnalysis;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedMember.Global

namespace Platform.Api.School.Features.Search.Models;

[ExcludeFromCodeCoverage]
public record SchoolSummaryResponse
{
    public string? URN { get; set; }
    public string? SchoolName { get; set; }
    public string? AddressStreet { get; set; }
    public string? AddressLocality { get; set; }
    public string? AddressLine3 { get; set; }
    public string? AddressTown { get; set; }
    public string? AddressCounty { get; set; }
    public string? AddressPostcode { get; set; }
    public string? OverallPhase { get; set; }
    public int? PeriodCoveredByReturn { get; set; }
    public double? TotalPupils { get; set; }
}
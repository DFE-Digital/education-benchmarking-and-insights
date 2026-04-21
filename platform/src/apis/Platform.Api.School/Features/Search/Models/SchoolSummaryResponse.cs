using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.School.Features.Search.Models;


[ExcludeFromCodeCoverage]
public record SchoolSummaryResponse
{
    /// <summary>The Unique Reference Number of the school.</summary>
    public string? URN { get; set; }
    /// <summary>The name of the school.</summary>
    public string? SchoolName { get; set; }
    /// <summary>The street part of the school's address.</summary>
    public string? AddressStreet { get; set; }
    /// <summary>The locality part of the school's address.</summary>
    public string? AddressLocality { get; set; }
    /// <summary>The third line of the school's address.</summary>
    public string? AddressLine3 { get; set; }
    /// <summary>The town or city of the school's address.</summary>
    public string? AddressTown { get; set; }
    /// <summary>The county of the school's address.</summary>
    public string? AddressCounty { get; set; }
    /// <summary>The postcode of the school's address.</summary>
    public string? AddressPostcode { get; set; }
    /// <summary>The overall education phase of the school (e.g., Primary, Secondary).</summary>
    public string? OverallPhase { get; set; }
    /// <summary>The number of months covered by the financial return.</summary>
    public int? PeriodCoveredByReturn { get; set; }
    /// <summary>The total number of pupils enrolled at the school.</summary>
    public double? TotalPupils { get; set; }
}
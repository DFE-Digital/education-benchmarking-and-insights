using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedMember.Global

namespace Platform.Api.Establishment.Features.Schools.Models;

[ExcludeFromCodeCoverage]
public record SchoolSummary
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

    public string Address => string.Join(", ", new List<string?> { AddressStreet, AddressLocality, AddressLine3, AddressTown, AddressCounty, AddressPostcode }.Where(x => !string.IsNullOrEmpty(x)));
}
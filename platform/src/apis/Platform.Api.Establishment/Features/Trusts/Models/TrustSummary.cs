using System.Diagnostics.CodeAnalysis;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedMember.Global

namespace Platform.Api.Establishment.Features.Trusts.Models;

[ExcludeFromCodeCoverage]
public record TrustSummary
{
    public string? CompanyNumber { get; set; }
    public string? TrustName { get; set; }
    public double? TotalPupils { get; set; }
    public double? SchoolsInTrust { get; set; }
}
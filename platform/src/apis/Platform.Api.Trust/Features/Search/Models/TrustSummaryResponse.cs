using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.Trust.Features.Search.Models;

[ExcludeFromCodeCoverage]
public record TrustSummaryResponse
{
    public string? CompanyNumber { get; set; }
    public string? TrustName { get; set; }
    public double? TotalPupils { get; set; }
    public double? SchoolsInTrust { get; set; }
}
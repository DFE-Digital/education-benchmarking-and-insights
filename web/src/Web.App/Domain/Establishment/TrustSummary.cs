using System.Diagnostics.CodeAnalysis;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace Web.App.Domain;

[ExcludeFromCodeCoverage]
public record TrustSummary
{
    public string? CompanyNumber { get; set; }
    public string? TrustName { get; set; }
    public double? TotalPupils { get; set; }
    public double? SchoolsInTrust { get; set; }
}
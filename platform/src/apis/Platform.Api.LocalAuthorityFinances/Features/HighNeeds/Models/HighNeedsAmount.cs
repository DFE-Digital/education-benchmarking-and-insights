// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace Platform.Api.LocalAuthorityFinances.Features.HighNeeds.Models;

public record HighNeedsAmount
{
    public decimal? TotalPlaceFunding { get; set; }
    public decimal? TopUpFundingMaintained { get; set; }
    public decimal? TopUpFundingNonMaintained { get; set; }
    public decimal? SenServices { get; set; }
    public decimal? AlternativeProvisionServices { get; set; }
    public decimal? HospitalServices { get; set; }
    public decimal? OtherHealthServices { get; set; }
}
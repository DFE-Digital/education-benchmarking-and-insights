// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UnusedMember.Global
namespace Web.App.Domain.LocalAuthorities;

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